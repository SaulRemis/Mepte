using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpinPlatform
{
    public interface ISpinPlatformInterface
    {
        void Start();
        void Stop();
        object GetData(object parameters);
        void Init(object parameters);
        void SetData(object parameters);
        event Dispatcher.ResultEventHandler NewResultEvent; 
    }
}
