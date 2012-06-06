using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpinPlatform
{
    namespace Data
    {
        public class DataEventArgs : EventArgs
        {
            public object DataArgs;
            public DataEventArgs(object results)
            {
                this.DataArgs = results;

            }
        }
    }
}
