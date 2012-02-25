using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpinPlatform
{
    interface ISpinPlatformInterface
    {
        void Start();
        void Stop();
        object GetData(object parameters);
        void Init(object parameters);
        void SetData(object paramenters);
        event Dispatcher.ResultEventHandler NewResultEvent; 
    }
}
