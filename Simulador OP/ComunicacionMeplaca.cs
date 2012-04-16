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
        OPSaul _Padre;


        public ComunicacionMeplaca(OPSaul padre, dynamic parametros, string name)
            : base(padre, name, (object) parametros.Communications)
        {
            _Padre = padre;
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
          ((OPSaul)_Padre).GetData(ref data, "FORMGetData");
            dynamic mens = new ExpandoObject();
            mens.COMMessage = mensajeAEnviar + data.Data.FORMPlate + data.Data.FORMWidth + data.Data.FORMLength + data.Data.FORMThickness + data.Data.FORMTol1 + data.Data.FORMTol2;
            _server.SetData(ref mens, "EnviarMensaje");
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
                             valor = new Message(messageid,"","","","9.5");
                            ((SharedData<Message>)SharedMemory["ResultadosUI"]).Add(valor);
                            _Padre.PrepareEvent(_Name);
                             break;
                        case "M4":
                             valor = new Message(messageid, "", "", "", "9.5");
                             ((SharedData<Message>)SharedMemory["ResultadosUI"]).Add(valor);
                             Events["Resultados"].Set();
                             break;
                        case "M2":
                             valor = new Message(messageid, "", "", "", "9.5");
                             ((SharedData<Message>)SharedMemory["ResultadosUI"]).Add(valor);
                            _Padre.PrepareEvent(_Name);
                             break;
                        case "M3":
                             valor = new Message(messageid, "", "", "", "9.5");
                            ((SharedData<Message>)SharedMemory["ResultadosUI"]).Add(valor);
                            _Padre.PrepareEvent(_Name);
                             break;
                        case "M5":
                              valor = new Message(messageid, mensaje.Substring(2, 16), mensaje.Substring(18, 350),mensaje.Substring(368,1),mensaje.Substring(369,3));
                            ((SharedData<Message>)SharedMemory["ResultadosUI"]).Add(valor);
                            _Padre.PrepareEvent(_Name);
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
