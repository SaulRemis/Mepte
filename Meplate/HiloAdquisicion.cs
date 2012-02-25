using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpinPlatform.Dispatcher;

namespace Meplate
{
    class HiloAdquisicion : SpinThread
    {
        public HiloAdquisicion(string name)
            : base(name)
        {
            _MillisecondsToSleep = 0;
        }

        public override void FunctionToExecuteByThread()
        {
            
        }
        public override void Initializate()
        {
            

        }

    }
}
