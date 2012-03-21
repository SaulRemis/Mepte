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

        public ComunicacionTarjeta(string name)
            : base(name)
        {
        }

        public ComunicacionTarjeta(dynamic padre, string name,dynamic parametros)
            : base(name)
        {
            parametros.ComunicacionTarjeta.COMThread = this;
            parametros.ComunicacionTarjeta.COMThreadName = "ComunicacionTarjeta";
            
            _Padre = padre;
            _server = new SpinCOM();
            _server.Init(parametros.ComunicacionTarjeta);
        }

        public override void FunctionToExecuteByThread()
        {

            while (((SharedData<Byte[]>)SharedMemory["SocketReader"]).Elementos > 0)
            {
               
                Byte[] mensaje = (Byte[])((SharedData<Byte[]>)SharedMemory["SocketReader"]).Pop();
                short messageid = BitConverter.ToInt16(new Byte[] { mensaje[9], mensaje[10] },0);
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
                        string vel = BitConverter.ToString(mensaje, 18, 2);
                        string avance=BitConverter.ToString(mensaje,20, 2);

                        Tarjeta valor = new Tarjeta(double.Parse(vel),double.Parse(avance));
                        ((SharedData<Tarjeta>)SharedMemory["Velocidad"]).Set(0, valor);
                        break;
                }
            }
        }
        public override void Closing()
        {
            Trace.WriteLine("ADRI:   saliendo  del HILO COMUNICACION TARJETA");
        }
    }
}
