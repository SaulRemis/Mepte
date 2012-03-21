using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpinPlatform
{
    interface ISpinPlatformInterface2
    {
        void Start();
        void Stop();
        dynamic GetData(params string[] parameters);
        void Init(object parameters);
        void SetData(dynamic data, params string[] parameters);
        event Dispatcher.ResultEventHandler NewResultEvent; 
    }
}
