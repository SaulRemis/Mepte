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
            public object _Padre;
            public SpinCOM _server;
            dynamic data = new ExpandoObject();
            bool _serverStarted = false;
            public SpinThreadSocket(string name)
                : base(name)
            {
            }

            public SpinThreadSocket(dynamic padre,dynamic parametros, string name)
                : base(name)
            {
                data.COMThread = this;
                data.COMThreadName = "SpinThreadSocket";
                data.COMSocketType = parametros.Communications.socketType;
                data.COMPort = parametros.Communications.port;
                data.COMIP = parametros.Communications.ip;
                data.COMBufferSize = parametros.Communications.buffersize;

                _Padre = padre;
                _server = new SpinCOM();
                _server.Init(data);
            }

            public override void Initializate()
            {
                if (!_serverStarted)
                {
                    _serverStarted = true;
                    _server.Start();
                }
                _WakeUpThreadEvent = Events["SocketData"];

            }

            public override bool Stop()
            {
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
