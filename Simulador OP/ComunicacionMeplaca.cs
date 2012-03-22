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


        public ComunicacionMeplaca(SpinDispatcher padre, dynamic parametros, string name)
            : base(padre, name, (object) parametros.Communications)
        {
          
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
            mens.COMMessage = mensajeAEnviar + data.Data.FORMPlate + data.Data.FORMWidth + data.Data.FORMLength;
            _server.SetData(mens, "EnviarMensaje");
        }

        public override void FunctionToExecuteByThread()
        {

            while (((SharedData<Byte[]>)SharedMemory["SocketReader"]).Elementos > 0)
            {
                Message valor;
                string mensaje = Encoding.ASCII.GetString((Byte[])((SharedData<Byte[]>)SharedMemory["SocketReader"]).Pop());
                String messageid = mensaje.Substring(0, 2);
                try
                {
                    switch (messageid)
                    {
                        case "M1":
                             SendMessage("M9");
                             valor = new Message(messageid,"","");
                            ((SharedData<Message>)SharedMemory["ResultadosOP"]).Add(valor);
                             Events["Resultados"].Set();
                             break;
                        case "M4":
                              valor = new Message(messageid,"","");
                            ((SharedData<Message>)SharedMemory["ResultadosOP"]).Add(valor);
                             Events["Resultados"].Set();
                             break;
                        case "M2":
                             valor = new Message(messageid, "", "");
                             ((SharedData<Message>)SharedMemory["ResultadosOP"]).Add(valor);
                             Events["Resultados"].Set();
                             break;
                        case "M3":
                              valor = new Message(messageid, "", "");
                             ((SharedData<Message>)SharedMemory["ResultadosOP"]).Add(valor);
                             Events["Resultados"].Set();
                             break;
                        case "M5":
                              valor = new Message(messageid, mensaje.Substring(2, 16), mensaje.Substring(18, 350));
                            ((SharedData<Message>)SharedMemory["ResultadosOP"]).Add(valor);
                             Events["Resultados"].Set();
                             break;
                        default:
                            break;
                    }
                    
                   
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex.Message);
                }
            }
        }
    }
}
