using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;
using SpinPlatform.Dispatcher;
using SpinPlatform.Data;

namespace OPSaul
{
    class HiloConsumidor : SpinThreadEvent
    {
        OPSaul _Padre;
        public HiloConsumidor(string name) : base(name) { }
        public HiloConsumidor(OPSaul padre, string name)
            : base(name)
        {
            _Padre = padre;
        }

        public override void FunctionToExecuteByThread()
        {
            while (((SharedData<Message>)SharedMemory["ResultadosOP"]).Elementos > 0)
            {
                Message temp = (Message)((SharedData<Message>)SharedMemory["ResultadosOP"]).Pop();
                Trace.WriteLine("There is new data in share memory: quedan " + ((SharedData<Message>)SharedMemory["ResultadosOP"]).Elementos);
                ((SharedData<Message>)SharedMemory["ResultadosUI"]).Set(0,temp);
                _Padre.PrepareEvent(_Name);
            }
        }
        public override void Initializate()
        {
            _WakeUpThreadEvent = Events["Resultados"];
        }
    }
}
