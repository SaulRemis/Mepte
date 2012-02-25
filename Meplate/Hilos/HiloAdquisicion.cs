using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpinPlatform.Dispatcher;

namespace Meplate
{
    class HiloAdquisicion : SpinThread
    {
        CMeplaca _Meplaca; 
        public HiloAdquisicion(string name, CArchivos arch)
            : base(name)
        {
            _Meplaca = new CMeplaca(arch);
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
