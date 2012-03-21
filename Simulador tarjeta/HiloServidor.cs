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

        public HiloServidor(string name)
            : base(name)
        {
        }

        public HiloServidor(dynamic padre, dynamic parametros, string name)
            : base(name)
        {
            data.COMThread = this;
            data.COMThreadName = "HiloServidor";
            data.COMSocketType = parametros.Communications.socketType;
            data.COMPort = parametros.Communications.port;
            data.COMIP = parametros.Communications.ip;
            data.COMBufferSize = parametros.Communications.buffersize;

            _Padre = padre;
            _server = new SpinCOM();
            _server.Init(data);
   
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
