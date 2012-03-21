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

namespace CardSaul
{
    class HiloServidor: SpinThreadSocket
    {
        dynamic data = new ExpandoObject();

        public HiloServidor(SpinDispatcher padre, dynamic parametros, string name)
            : base(padre, name, (object)parametros.Communications)
        {
           
        }

        public override void SendMessage(string mensajeAEnviar)
        {
           dynamic mens = new ExpandoObject();
           mens.COMMessage = mensajeAEnviar;
           _server.SetData(mens);
        }

        public void SendMessage(byte[] mensajeAEnviar)
        {
            dynamic mens = new ExpandoObject();
            mens.COMMessage = mensajeAEnviar;
            _server.SetData(mens);
        }

        public override void Initializate()
        {
            if (!_serverStarted)
            {
                _serverStarted = true;
                try
                {
                    _server.Start();
                }
                catch(Exception ex)
                {
                    Trace.WriteLine(ex.Message);
                }
            }
            _WakeUpThreadEvent = Events["SocketData"];

        }

        public override void FunctionToExecuteByThread()
        {

        }
    }
}
