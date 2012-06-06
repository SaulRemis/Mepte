using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using SpinPlatform.Errors;
using System.Net;
using SpinPlatform.Dispatcher;
using SpinPlatform.Data;
using System.Dynamic;
using System.Threading;

namespace SpinPlatform.Comunicaciones
{
    [Serializable()]
    public class SpinCOM : SpinDispatcher
    {
        /// <summary>
        /// Class of CLIENTE-SERVER communication
        /// </summary>
        #region Variables

        private Socket _socketEscucha = null;
        private Socket _socketDatos = null;
        private int BytesEnviados = 0;
        private dynamic _data = new ExpandoObject();

        // Buferes de recepcion y envio
        private Byte[] _buferRecibe;
        private Byte[] _buferEnvia;

        // Direccion local y punto final de conexion
        private IPAddress _direccionLocal;
        private IPEndPoint _extremoFinal;


        #endregion

        #region Implementación de interface

        /// <summary>
        /// Inicializa el Modulo de Comunicaciones.
        /// </summary>
        /// <param name="initData">
        /// Campos Obligatorios:  \n
        /// COMThread (thread) \n
        /// COMThreadName (string) \n
        /// COMSocketType (string) \n
        /// COMPort (string) \n
        /// COMIP (string) \n
        /// COMBufferSize (int) \n
        /// COMTryconnectiontimes (int) \n
        /// </param>
        public override void Init(dynamic initData)
        {
            _data = initData;

            if (_data.COMSocketType == "SERVER" && _data.COMIP=="")
                _direccionLocal = GetServerIp();
            else
                _direccionLocal = IPAddress.Parse(_data.COMIP); //Direccion de bucle
            _extremoFinal = new IPEndPoint(_direccionLocal, Int32.Parse(_data.COMPort));

            _buferRecibe = new Byte[Int32.Parse(_data.COMBufferSize)];
            _buferEnvia = new Byte[Int32.Parse(_data.COMBufferSize)];

            //Preparo hilos y shareddata
            _data.COMSocket = _socketDatos;

            _DispatcherThreads.Add("Comunicaciones", new HiloComunicaciones(_data, "Comunicaciones"));
            _DispatcherThreads.Add(_data.COMThreadName, _data.COMThread);

            ConnectMemory("SocketReader", new SharedData<Byte[]>(10), _data.COMThreadName, "Comunicaciones");
            CreateEvent("SocketData", new AutoResetEvent(false), _data.COMThreadName, "Comunicaciones");

            if (_data.COMSocketType == "SERVER")
            {
                _DispatcherThreads.Add("ComunicacionesAccept", new HiloComunicacionesAccept(_data, "ComunicacionesAccept"));
            }
        }

        /// <summary>
        /// Envía datos por el socket \n
        /// </summary>
        /// <param name="obj">
        /// Variable dinamica de donde obtener los datos a establecer \n
        /// COMMessage: Este objeto puede ser o bien de tipo Byte[] o de tipo String. Por el socket viaja en tipo byte \n
        /// </param>
        /// <param name="parameters">
        /// "EnviarMensaje" - Envía datos por el socket \n
        /// </param>
        public override void SetData(ref dynamic obj, params string[] parameters)
        {

            try
            {
                foreach (string parameter in parameters)
                {
                    switch (parameter)
                    {
                        case "EnviarMensaje":
                            EnviarDatos(obj);
                            break;
                        default:
                            obj.COMErrors = "Wrong Query";
                            break;
                    }
                }
                obj.COMErrors = "";
            }
            catch (Exception ex)
            {
                obj.COMErrors = ex.Message;
                SpinException.GetException("SpinCom:: " + ex.Message, ex);
            }
        }


        /// <summary>
        /// Para el Módulo de  COMUNICACIONES
        /// </summary>
        public void Start()
        {
            Status = SpinDispatcherStatus.Starting;
            switch ((string)_data.COMSocketType)
            {
                case "SERVER":
                    // Crear un socket para escuchar peticiones
                    _socketEscucha = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
                    ((HiloComunicacionesAccept)_DispatcherThreads["ComunicacionesAccept"]).Start(_extremoFinal, _socketDatos, _socketEscucha, _data.COMSocketType, (HiloComunicaciones)_DispatcherThreads["Comunicaciones"]);

                    break;
                case "CLIENT":
                    _socketDatos = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
                    _socketDatos.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, 70);
                    // Conexion con el servidor
                    int _connectTry = 0;
                    while (_connectTry < Int32.Parse(_data.COMTryconnectiontimes) )
                    {
                        try
                        {

                            _socketDatos.Connect(_extremoFinal);
                            _connectTry = Int32.Parse(_data.COMTryconnectiontimes) + 1; //Conectado
                            // Recibir respuesta del servidor
                            ((HiloComunicaciones)_DispatcherThreads["Comunicaciones"]).Start(_socketDatos, _socketEscucha, _data.COMSocketType);

                        }
                        catch
                        {
                            _connectTry++;
                        }
                    }
                    if (_connectTry == Int32.Parse(_data.COMTryconnectiontimes))
                    {
                        throw new SpinException("SpinCOM:: Couldn´t connect to server: " + _data.COMIP, new Exception());
                    }
                    break;

            }
        }


        /// <summary>
        /// Obtiene datos del Socket. 
        /// </summary>
        /// <param name="Data">
        /// Variable dinamica donde guardar los resultados: \n
        /// COMSocketDatosConnected (bool) determina si el socket de datos está conectado \n
        /// COMSocketEscuchaConnected (bool) determina si el socket de escucha está conectado \n
        /// </param>
        /// <param name="parameters">
        /// "EstadoSocket" - Obtiene elestado de los sockets \n
        /// </param>
        public override void GetData(ref dynamic Data, params string[] parameters)
        {
            Data.COMReturnedData = parameters;
            try
            {
                foreach (string parameter in parameters)
                {
                    switch (parameter)
                    {
                        case "EstadoSocket":
                            Data.COMSocketDatosConnected = !((HiloComunicaciones)_DispatcherThreads["Comunicaciones"])._socketClosed;
                            if (_socketEscucha != null)
                                Data.COMSocketEscuchaConnected = _socketEscucha.IsBound;
                            else
                                Data.COMSocketEscuchaConnected = false;
                            break;
                        default:
                            Data.COMErrors = "Wrong Query";
                            break;
                    }
                }
                Data.COMErrors = "";
            }
            catch (Exception ex)
            {
                Data.COMErrors = ex.Message;
            }

        }

        /// <summary>
        /// Para el Módulo de  COMUNICACIONES
        /// </summary>
        public override void Stop()
            {

                if (Status==SpinDispatcherStatus.Running)
                {
                    Status = SpinDispatcherStatus.Stopping;
                    //recorro todos los hilos
                    foreach (KeyValuePair<string, SpinThread> item in _DispatcherThreads)
                    {

                        item.Value.Stop();
                        item.Value.Join();

                    }
                    Status = SpinDispatcherStatus.Stopped; 
                }
                if (Status == SpinDispatcherStatus.Starting && _data.COMSocketType=="SERVER")
                {
                    Status = SpinDispatcherStatus.Stopping;
                    _DispatcherThreads["ComunicacionesAccept"].Stop();
                    _DispatcherThreads["ComunicacionesAccept"].Join();

                    Status = SpinDispatcherStatus.Stopped; 
                
                }
                if (Status == SpinDispatcherStatus.Starting && _data.COMSocketType == "CLIENT" && !((HiloComunicaciones)_DispatcherThreads["Comunicaciones"])._socketClosed)
                {
                    Status = SpinDispatcherStatus.Stopping;
                    _DispatcherThreads["Comunicaciones"].Stop();
                    _DispatcherThreads["Comunicaciones"].Join();

                    Status = SpinDispatcherStatus.Stopped;

                }
            }
      
        #endregion

        #region Métodos
        private IPAddress GetServerIp()
        {
            string strHostName = "";
            try
            {
                // Getting Ip address of local machine...
                // First get the host name of local machine.
                strHostName = Dns.GetHostName();
                Console.WriteLine("Local Machine's Host Name: " + strHostName);

                // Then using host name, get the IP address list..
                IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);
                IPAddress[] addr = ipEntry.AddressList;
                foreach (IPAddress dir in addr)
                {
                    if(dir.AddressFamily.ToString()=="InterNetwork")
                        return dir;
                }
                return null;

            }
            catch (SocketException se)
            {
                Console.WriteLine("{0} ({1})", se.Message, strHostName);
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: {0}.", ex.Message);
                return null;
            }
        }
        public void PrepareEvent(string thread)
        {
            dynamic data = new ExpandoObject();
            switch (thread)
            {
                case "Comunicaciones":
                    data.COMGetSocketLine = true;
                    break;
                default:
                    break;
            }

            data = GetData(data);
            DataEventArgs args = new DataEventArgs(data);
            if (Status == SpinDispatcherStatus.Running)  // Por si nadie escucha el evento o esta en proceso de parar
            {
                SetEvent(args);
            }

        }

        private void EnviarDatos(dynamic obj)
        {
            if ((obj.COMMessage).GetType() == typeof(String))
                _buferEnvia = Encoding.ASCII.GetBytes(obj.COMMessage);
            else
                _buferEnvia = obj.COMMessage;

            if (((HiloComunicaciones)_DispatcherThreads["Comunicaciones"])._socketDatos != null)
            {
                BytesEnviados = ((HiloComunicaciones)_DispatcherThreads["Comunicaciones"])._socketDatos.Send(_buferEnvia);

                Console.Write(BytesEnviados);
                Console.Write(" Bytes enviados --> ");
                Console.WriteLine(Encoding.ASCII.GetString(_buferEnvia));
            }
            else
            {
                Console.Write(" Socket de datos cerrado,no se ha mandado el mensaje");
            }
        }

        #endregion
    }
}
