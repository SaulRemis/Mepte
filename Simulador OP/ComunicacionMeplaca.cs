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

namespace OPSaul
{
    class ComunicacionMeplaca: SpinThreadSocket
    {
        dynamic data = new ExpandoObject();

        public ComunicacionMeplaca(string name)
            : base(name)
        {
        }

        public ComunicacionMeplaca(dynamic padre, dynamic parametros, string name)
            : base(name)
        {
            data.COMThread = this;
            data.COMThreadName = "ComunicacionMeplaca";
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

        public override void SendMessage(string mensajeAEnviar)
        {
            data.FORMGetData = true;
            data = ((OPSaul)_Padre).GetData(data);
            dynamic mens = new ExpandoObject();
            mens.COMMessage = mensajeAEnviar + ((OPSaul)_Padre).data.FORMWidth +((OPSaul)_Padre).data.FORMLength;
            _server.SetData(mens);
        }

        public override void FunctionToExecuteByThread()
        {

            while (((SharedData<Byte[]>)SharedMemory["SocketReader"]).Elementos > 0)
            {
                string mensaje = Encoding.ASCII.GetString((Byte[])((SharedData<Byte[]>)SharedMemory["SocketReader"]).Pop());
                try
                {
                    String messageid =mensaje.Substring(0, 2);
                    Message valor = new Message(messageid, mensaje.Substring(2, 16), mensaje.Substring(17, 350));
                    Trace.WriteLine("\nOPSAUL:: New message arrived: MessageID->" + messageid);
                    if (messageid.Equals("M1"))
                    {
                        SendMessage("M9" + mensaje.Substring(2, 16));
                    }
                    ((SharedData<Message>)SharedMemory["ResultadosOP"]).Set(0, valor);
                    Events["Resultados"].Set();
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex.Message);
                }
            }
        }
    }
}
