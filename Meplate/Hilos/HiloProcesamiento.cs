using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpinPlatform.Dispatcher;

namespace Meplate
{
    class HiloProcesamiento: SpinThreadEvent
    {
        SpinDispatcher _Padre;
        public HiloProcesamiento(SpinDispatcher padre, string name)
            : base(name)
        {
             _Padre = padre;
        }
        public override void FunctionToExecuteByThread()
        { 
        }
        public override void Initializate()
        { 
        }
    }
}
