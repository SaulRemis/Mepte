using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace SpinPlatform
{
    namespace Dispatcher
    {
        public class SpinThreadEvent : SpinThread
        {
            protected AutoResetEvent _WakeUpThreadEvent;
            /// <summary>
            /// Constructor of the SpinThread Class
            /// </summary>
            public SpinThreadEvent(string name)
                : base(name)
            {

                _MillisecondsToSleep = 0;

            }


            /// <summary>
            /// Constructor of SpinThread Class with the Priority of the Thread
            /// </summary>
            /// <param name="priority">  Priority of the Thread</param>
            public SpinThreadEvent(ThreadPriority priority, string name)
                : base(name, priority, 0)
            {


            }
            override public void EntryFunctionThread()
            {
                Initializate();
                while (_WakeUpThreadEvent.WaitOne())
                {
                    if (_StopEvent.WaitOne(0, true)) break;

                    FunctionToExecuteByThread();

                    if (_StopEvent.WaitOne(0, true)) break;
                }
                Closing();
                Trace.WriteLine("Sale el hilo procesamiento");
            }
            public override bool Stop()
            {
                _StopEvent.Set();
                _WakeUpThreadEvent.Set();
                return true;

            }


        }
    }
}
