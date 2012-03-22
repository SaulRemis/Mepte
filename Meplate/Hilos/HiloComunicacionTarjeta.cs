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

namespace Meplate
{
    class ComunicacionTarjeta: SpinThreadSocket
    {
          
        public ComunicacionTarjeta(SpinDispatcher padre, string name,dynamic parametros)
            : base(padre, name, (object)parametros.ComunicacionTarjeta)
        {           
        }

        public override void FunctionToExecuteByThread()
        {

            while (((SharedData<Byte[]>)SharedMemory["SocketReader"]).Elementos > 0)
            {
               
                Byte[] val = (Byte[])((SharedData<Byte[]>)SharedMemory["SocketReader"]).Pop();
                string mensaje = Encoding.ASCII.GetString(val);
                short messageid = short.Parse(mensaje.Substring(16,2));
                Trace.WriteLine("New message arrived: MessageID->" + messageid);
                switch (messageid)
                {
                    case 21:
                        Events["ComenzarMedida"].Set();
                        ((ComunicacionOP)((Meplate)_Padre)._DispatcherThreads["ComunicacionOP"]).SendMessage("21");
                        break;
                    case 22:
                        ((ComunicacionOP)((Meplate)_Padre)._DispatcherThreads["ComunicacionOP"]).SendMessage("22");
                        break;
                    case 23:
                        Events["FinalizarMedida"].Set();
                        ((ComunicacionOP)((Meplate)_Padre)._DispatcherThreads["ComunicacionOP"]).SendMessage("23");
                        break;
                    case 24:
                        ((ComunicacionOP)((Meplate)_Padre)._DispatcherThreads["ComunicacionOP"]).SendMessage("24");
                        break;
                    case 26:
                       
                        Tarjeta valor = new Tarjeta(BitConverter.ToInt16(val, 18), BitConverter.ToInt16(val, 20));
                        ((SharedData<Tarjeta>)SharedMemory["Velocidad"]).Set(0, valor);
                        break;
                }
            }
        }
       
    }
}
