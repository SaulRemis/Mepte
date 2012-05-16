using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;
using SpinPlatform.Dispatcher;
using SpinPlatform.Data;

namespace CardSaul
{
    class HiloConsumidor:SpinThreadEvent
    {
        CardSaul _Padre;
        public HiloConsumidor(string name) : base(name)      {}
        public HiloConsumidor(CardSaul padre, string name)
            : base(name)
        {
             _Padre = padre;
        }

        public override void FunctionToExecuteByThread()
        {
            while (((SharedData<String>)SharedMemory["ConsProd"]).Elementos > 0)
            {
                String temp = (String)((SharedData<String>)SharedMemory["ConsProd"]).Pop();
              //  Trace.WriteLine("Sent new data: " + temp + " quedan " + ((SharedData<String>)SharedMemory["ConsProd"]).Elementos);
                ((SharedData<String>)SharedMemory["Resultados"]).Set(0,temp);

                _Padre.PrepareEvent(_Name);
              
            }
        }
        public override void Initializate()
        {
            _WakeUpThreadEvent = Events["NuevaMedida"];

        }
  
    }
}