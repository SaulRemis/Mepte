using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Dynamic;
using SpinPlatform;
using SpinPlatform.Data;
using SpinPlatform.Dispatcher;

namespace CardSaul
{
    class HiloProductor : SpinThreadWhile
    {
        CardSaul _Padre;
        dynamic aux;
        double messagecounter = 1000;
        public HiloProductor(string name)
            : base(name)
        {
            _MillisecondsToSleep = 1 * 1000;
            aux = new ExpandoObject();
        }

        public HiloProductor(CardSaul padre, string name)
            : base(name)
        {
             _Padre = padre;
             _MillisecondsToSleep = 1 * 1000;
             aux = new ExpandoObject();
        }

        public override void FunctionToExecuteByThread()
        {
            HiloServidor hilotemp= (HiloServidor)_Padre._DispatcherThreads["HiloServidor"];
            SpinPlatform.Comunicaciones.SpinCOM temp=hilotemp._server;
            temp.GetData(ref aux, "EstadoSocket");

            if (aux.COMSocketDatosConnected)
            {
                dynamic data = new ExpandoObject();
               ((CardSaul)_Padre).GetData(ref data, "HILOProductor");

                short speed = (short)(data.HILOProductorValue );
                short avance = (short)(data.HILOProductorValue );
                short id=26;

                byte[] rv = new byte[(Encoding.ASCII.GetBytes("$TARJETA0022" + messagecounter.ToString())).Length + (BitConverter.GetBytes(id)).Length + (BitConverter.GetBytes(speed)).Length + (BitConverter.GetBytes(avance)).Length];
                System.Buffer.BlockCopy((Encoding.ASCII.GetBytes("$TARJETA0022" + messagecounter.ToString() )), 0, rv, 0, (Encoding.ASCII.GetBytes("$TARJETA0022" + messagecounter.ToString())).Length);
                System.Buffer.BlockCopy((BitConverter.GetBytes(id)), 0, rv, (Encoding.ASCII.GetBytes("$TARJETA0022" + messagecounter.ToString())).Length, (BitConverter.GetBytes(id)).Length);
                System.Buffer.BlockCopy((BitConverter.GetBytes(speed)), 0, rv, (Encoding.ASCII.GetBytes("$TARJETA0022" + messagecounter.ToString())).Length + (BitConverter.GetBytes(id)).Length, (BitConverter.GetBytes(speed)).Length);
                System.Buffer.BlockCopy((BitConverter.GetBytes(avance)), 0, rv, (Encoding.ASCII.GetBytes("$TARJETA0022" + messagecounter.ToString() )).Length + 2*(BitConverter.GetBytes(speed)).Length, (BitConverter.GetBytes(avance)).Length);


                ((HiloServidor)((CardSaul)_Padre)._DispatcherThreads["HiloServidor"]).SendMessage(rv);
                ((SharedData<String>)SharedMemory["ConsProd"]).Add(avance.ToString());
                messagecounter++;
                if (messagecounter == 3276)
                    messagecounter = 1000;
                Events["NuevaMedida"].Set();
            }
        }
    }
}
