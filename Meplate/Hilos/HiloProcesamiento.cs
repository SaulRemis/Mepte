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
        CProcesamiento _Proc;
        public HiloProcesamiento(SpinDispatcher padre, string name, CArchivos arch)
            : base(name)
        {
            _Proc = new CProcesamiento(arch);
            _Padre = padre;
        }
        public override void FunctionToExecuteByThread()
        { 
        }
        public override void Initializate()
        {
            _WakeUpThreadEvent = _Events["ChapaMedida"];
        }
    }
}
