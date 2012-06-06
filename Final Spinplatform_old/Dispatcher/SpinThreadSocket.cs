using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using SpinPlatform.Dispatcher;
using SpinPlatform.Data;
using System.Dynamic;
using SpinPlatform.Comunicaciones;
using System.Diagnostics;


namespace SpinPlatform
{
    namespace Dispatcher
    {

        public class SpinThreadSocket : SpinThreadEvent
        {
            public SpinDispatcher _Padre;
            public SpinCOM _server;
            dynamic data = new ExpandoObject();
            public bool _serverStarted = false, _starting = false;

            public SpinThreadSocket(SpinDispatcher padre, string name)
                : base(name)
            {
                _Padre = padre;

            }

            public SpinThreadSocket(SpinDispatcher padre, string name, dynamic parametros)
                : base(name)
            {
                _Padre = padre;
                _Priority = ThreadPriority.BelowNormal;

                parametros.COMThread = this;
                parametros.COMThreadName = name;

                _server = new SpinCOM();
                _server.Init(parametros);
            }


            public override void Initializate()
            {

                if (!_serverStarted)
                {
                    try
                    {
                        _starting = true;
                        _server.Start();
                        _starting =false ;
                        _serverStarted = true;

                    }
                    catch (Exception ex)
                    {
                        _starting = false;
                        Trace.WriteLine(ex.Message);
                    }
                }
                _WakeUpThreadEvent = Events["SocketData"];

            }

            public override bool Stop()
            {
                while (_starting)
                {
                    Thread.Sleep(10);
                }
                 _server.Stop();
                _StopEvent.Set();
                _WakeUpThreadEvent.Set();



                return true;

            }

            public virtual void SendMessage(string message)
            {
            }
        }
    }
}
