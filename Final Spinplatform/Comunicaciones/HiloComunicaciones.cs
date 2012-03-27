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
    class HiloComunicaciones :SpinThreadWhile
    {
        #region Variables

        public Socket _socketDatos = null;
        private Socket _socketEscucha = null;
        private bool _socketClosed = false;
        private int BytesRecibidos=0;
        private int _bufferSize = 0;
        private String _socketType = null;

        // Buferes de recepcion y envio
        private Byte[] _buferRecibe;

        #endregion

        public HiloComunicaciones(string name) : base(name)      {}
        public HiloComunicaciones(dynamic parametros, string name)
            : base(name)
        {
            _bufferSize = Int32.Parse(parametros.COMBufferSize);
            _socketDatos = parametros.COMSocket;
            _buferRecibe = new Byte[_bufferSize];
        }

        override public void FunctionToExecuteByThread()
        {
            try
            {
                if (!_socketClosed)
                {
                        // Recibir mensaje
                        _buferRecibe = new Byte[_bufferSize];
                        BytesRecibidos = _socketDatos.Receive(_buferRecibe);
         
                        Console.Write(BytesRecibidos);
                        Console.Write(" Bytes recibidos <-- ");
                        Console.WriteLine(Encoding.ASCII.GetString(_buferRecibe));

                        //PROCESAR INFO el server envia un evento hacia atrás por si se quiere hacer algo con la info que llegó
                        ((SharedData<Byte[]>)SharedMemory["SocketReader"]).Add(_buferRecibe);
                        Events["SocketData"].Set();
                   
                }
                else
                {
                    _StopEvent.Set();
                }
            }
            catch (SocketException socketEx)
            {
                    //Error por cerrar el socket desde la clase padre
                    _socketClosed = true;
                    Console.WriteLine("Socket close: "+socketEx.Message);
                    _StopEvent.Set();

                    Events["SocketData"].Set();
            }
        }

        public void Start(Socket socket,Socket socketEscucha, String socketType)
        {
            try
            {
                _socketType = socketType;
                _socketEscucha = socketEscucha;
                _socketDatos = socket;
                _StopEvent.Reset(); //reset stop event 
                _MainThread = new Thread(new ThreadStart(EntryFunctionThread)); // Create thread
                _MainThread.Name = _Name;
                _MainThread.Start();  //The thread is started and will call EntryFunctionThread
            }
            catch (Exception ex)
            {
                throw SpinException.GetException(SpinExceptionConstants.SPIN_ERROR_THREAD_STARTING, ex);
            }

        }


        public override bool Stop()
        {
            try
            {
                _StopEvent.Set();
                _socketClosed = true;
                if (_socketEscucha != null)
                    _socketEscucha.Close();
                if (_socketDatos != null)
                    _socketDatos.Close();               
                return true;
            }
            catch (Exception ex)
            {
                throw SpinException.GetException(SpinExceptionConstants.SPIN_ERROR_THREAD_ABORTING, ex);
            }
        }
     
    }
}
