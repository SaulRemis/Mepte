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
using System.Diagnostics;

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
        private String _socketType = null;
        private String _ip = null;
        private int _port;
        private int _bufferSize;
        private int BytesEnviados = 0;

        // Buferes de recepcion y envio
        private Byte[] _buferRecibe;
        private Byte[] _buferEnvia;

        // Direccion local y punto final de conexion
        private IPAddress _direccionLocal;
        private IPEndPoint _extremoFinal;
        #endregion

        #region Implementación de interface

        public override void Init(dynamic initData)
        {
            /// <summary>
            /// Se encarga de preparar el módulo de comunicación (tanto cliente como servidor)
            /// En concreto el objeto de entrada debe disponer de los siguientes campos:
            /// (OBLIGATORIO)COMThread: Hilo al que llegarán los eventos de datos en el socket y que compartirá una memoria con este módulo
            /// (OBLIGATORIO)COMThreadName: Nombre del hilo superior
            /// (OBLIGATORIO)COMSocketType: Indica si es tipo "SERVER" o tipo "CLIENT" 
            /// (OBLIGATORIO)COMPort: Puerto de conexión
            /// (OBLIGATORIO)COMIP: IP del servidor
            /// (OBLIGATORIO)COMBufferSize: Tamaño del buffer de recepción y envío de datos
            /// </summary>
            
            //guardo parametros
            _socketType = initData.COMSocketType;
            _port = Int32.Parse(initData.COMPort);
            _ip = initData.COMIP;
            _bufferSize = Int32.Parse(initData.COMBufferSize);
            _direccionLocal = IPAddress.Parse(_ip); //Direccion de bucle
            _extremoFinal = new IPEndPoint(_direccionLocal, _port);

            _buferRecibe = new Byte[_bufferSize];
            _buferEnvia = new Byte[_bufferSize];

            //Preparo hilos y shareddata
            initData.COMSocket = _socketDatos;

            _DispatcherThreads.Add("Comunicaciones", new HiloComunicaciones(initData,"Comunicaciones"));
            _DispatcherThreads.Add(initData.COMThreadName,initData.COMThread);

            ConnectMemory("SocketReader", new SharedData<Byte[]>(10), initData.COMThreadName, "Comunicaciones");
            CreateEvent("SocketData", new AutoResetEvent(false), initData.COMThreadName, "Comunicaciones");

            if (_socketType == "SERVER")
            {
                _DispatcherThreads.Add("ComunicacionesAccept", new HiloComunicacionesAccept(initData, "ComunicacionesAccept"));
            }
        }
        public override void SetData(dynamic obj)
        {
            /// <summary>
            /// Se encarga de enviar mensajes desde el hilo principal a través del socket abierto a quien esté escuchando si hay alguien 
            /// En concreto el objeto de entrada debe disponer de los siguientes campos:
            /// (OBLIGATORIO)COMMessage: Este objeto puede ser o bien de tipo Byte[] o de tipo String. Por el socket viaja en tipo byte
            /// </summary>
            
            try
            {
                if ((obj.COMMessage).GetType() == typeof(String))
                    _buferEnvia = Encoding.ASCII.GetBytes(obj.COMMessage);
                else
                    _buferEnvia = obj.COMMessage;
                BytesEnviados = ((HiloComunicaciones)_DispatcherThreads["Comunicaciones"])._socketDatos.Send(_buferEnvia);

                Console.Write(BytesEnviados);
                Console.Write(" Bytes enviados --> ");
                Console.WriteLine(Encoding.ASCII.GetString(_buferEnvia));
            }
            catch (Exception ex)
            {
                SpinException.GetException("SpinCom:: "+ex.Message,ex);
            }
        }
        public void Start()
        {
            /// <summary>
            /// Se encarga de inicializar los socket y ponerlo en escucha o en el caso del cliente realizar una conexión
            /// No necesita datos de entrada
            /// </summary>
            
            Status = SpinDispatcherStatus.Starting;
            switch (_socketType)
            {
                case "SERVER":
                    // Crear un socket para escuchar peticiones
                    _socketEscucha = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
                    ((HiloComunicacionesAccept)_DispatcherThreads["ComunicacionesAccept"]).Start(_extremoFinal, _socketDatos, _socketEscucha, _socketType, (HiloComunicaciones)_DispatcherThreads["Comunicaciones"]);
                    break;
                case "CLIENT":
                    _socketDatos = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
                    // Conexion con el servidor
                    try
                    {
                        _socketDatos.Connect(_extremoFinal);
                        Status = SpinDispatcherStatus.Running;
                        // Recibir respuesta del servidor
                        ((HiloComunicaciones)_DispatcherThreads["Comunicaciones"]).Start( _socketDatos, _socketEscucha, _socketType);         
                    }
                    catch(Exception ex)
                    {
                        SpinException.GetException("SpinCOM:: "+ex.Message,ex);
                    }
                    break;

            }
        }
        public override object GetData(object parameters)
        {
            return parameters;
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
        public void Stop()
        {
            try
            {
                
                Status = SpinDispatcherStatus.Stopping;
                //paro hilos
                if (_socketType=="SERVER")
                {
                    if (((HiloComunicacionesAccept)_DispatcherThreads["ComunicacionesAccept"])._socketDatos != null)
                    {
                        ((HiloComunicacionesAccept)_DispatcherThreads["ComunicacionesAccept"])._socketDatos.Shutdown(SocketShutdown.Both);
                        ((HiloComunicacionesAccept)_DispatcherThreads["ComunicacionesAccept"])._socketDatos.Disconnect(false);
                        ((HiloComunicacionesAccept)_DispatcherThreads["ComunicacionesAccept"])._socketDatos.Close();
                    }
                    ((HiloComunicacionesAccept)_DispatcherThreads["ComunicacionesAccept"]).Stop();
                    ((HiloComunicacionesAccept)_DispatcherThreads["ComunicacionesAccept"]).Join();
                }
                if (_socketDatos != null)
                {
                        _socketDatos.Shutdown(SocketShutdown.Both);
                        _socketDatos.Disconnect(false);
                        _socketDatos.Close();
                }
                if (_socketEscucha != null)
                {
                        _socketEscucha.Close();
                }
                Status = SpinDispatcherStatus.Stopped;
                Trace.WriteLine("saliendo  del MODULO DE SOCKETS");
            }
            catch (Exception ex)
            {
                SpinException.GetException(ex.Message,ex);
            }
        }

        #endregion

        #region Métodos

       

        #endregion
    }
}
