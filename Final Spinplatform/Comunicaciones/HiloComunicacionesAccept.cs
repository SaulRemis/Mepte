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
    class HiloComunicacionesAccept : SpinThreadWhile
    {
        #region Variables

        public Socket _socketDatos = null;
        private Socket _socketEscucha = null;
        private IPEndPoint _extremoFinal;
        private bool _started = false;
        private bool _socketClosed = false;
        private HiloComunicaciones _comunicaciones;
        #endregion

        public HiloComunicacionesAccept(string name) : base(name)      {}
        public HiloComunicacionesAccept(dynamic parametros, string name)
            : base(name)
        {
            _socketDatos = parametros.COMSocket;
        }

        public void Start(IPEndPoint extremoFinal, Socket socket, Socket socketEscucha, String socketType, HiloComunicaciones Comunicaciones)
        {
            try
            {
                _comunicaciones = Comunicaciones;
                _extremoFinal = extremoFinal;
                _socketEscucha = socketEscucha;
                _socketDatos = socket;
                // Registrar el socket y ponerlo escuchar peticiones
                _socketEscucha.Bind(_extremoFinal); // Se registra el socket
                _socketEscucha.Listen(5); // 5 indica la longitud máxima de la cola de conexiones pendientes

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

        override public void FunctionToExecuteByThread()
        {
            try
            {
                // Crear un socket para escuchar peticiones    
                if (!_socketClosed)
                {
                    Console.WriteLine("\nAceptando una peticion ... ");
                    _socketDatos = _socketEscucha.Accept(); // Se bloquea en este punto hasta que llegue una petición

                    if (_started)
                    {
                        try
                        {
                            //Aqui me cargo el hilo con una nueva conexión
                            _comunicaciones._socketDatos.Shutdown(SocketShutdown.Both);
                            _comunicaciones._socketDatos.Disconnect(true);
                            _comunicaciones.Stop();
                            _comunicaciones.Join();
                        }
                        catch (Exception ex)
                        {
                            SpinException.GetException("SpinCOM:: " + ex.Message, ex);
                        }
                    }
                    _comunicaciones.Start(_socketDatos, _socketEscucha, "SERVER");
                    _started = true;
                }
                else
                {
                    _StopEvent.Set();
                }
            }
            catch (SocketException socketEx)
            {
                if (socketEx.ErrorCode == 10004)
                {
                    //Error por cerrar el socket desde la clase padre
                    _socketClosed = true;
                    if (_started)
                    {
                        _comunicaciones.Stop();
                        _comunicaciones.Join();
                    }
                    Console.WriteLine("Socket closed");
                }
            }
        }

        public override bool Stop()
        {
            try
            {
                //if (_started)
                //{
                //    _comunicaciones.Stop();
                //    _comunicaciones.Join();
                //}
                //if (_socketEscucha != null)
                //{
                //    _socketEscucha.Close();
                //}
                if (_socketEscucha != null)
                    _socketEscucha.Close();
                if(_socketDatos!=null)
                    _socketDatos.Close();
                _StopEvent.Set();
                return true;
            }
            catch (Exception ex)
            {
                throw SpinException.GetException(SpinExceptionConstants.SPIN_ERROR_THREAD_ABORTING, ex);
            }
        }
    }
}
