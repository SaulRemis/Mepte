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

                #region Public functions
            /// <summary>
            /// This function should be overrided in derived class to define custom functionality
            /// </summary>
            public override void FunctionToExecuteByThread()
            {
                Console.WriteLine("Function called by thread");
            }


            /// <summary>
            /// Constructor of the SpinThread Class
            /// </summary>
            public SpinThreadEvent(string name)
            {

                _MainThread = new Thread(new ThreadStart(EntryFunctionThread)); // Create thread
                _StopEvent = new ManualResetEvent(false);
                _SharedMemory = new Dictionary<string, object>();
                _Events = new Dictionary<string, AutoResetEvent>();
                _Priority = ThreadPriority.Highest;
                _Name = name;

            }


            /// <summary>
            /// Constructor of SpinThread Class with the Priority of the Thread
            /// </summary>
            /// <param name="priority">  Priority of the Thread</param>
            public SpinThreadEvent(ThreadPriority priority, string name)
            {
                _StopEvent = new ManualResetEvent(false);
                _SharedMemory = new Dictionary<string, object>();
                _Events = new Dictionary<string, AutoResetEvent>();
                _Priority = priority;

                _Name = name;

            }

          

          
           

            public override void Initializate()
            {
            }

            public override void Closing()
            {
            }


            public override void EntryFunctionThread()
            {
                Initializate();

                while (_WakeUpThreadEvent.WaitOne())
                {
                    if (_StopEvent.WaitOne(0, true))
                        break;

                    FunctionToExecuteByThread();

                    if (_StopEvent.WaitOne(0, true))
                        break;
                }

                // _WakeUpThreadEvent.Reset();
                Closing();


            }
            public override bool Stop()
            {
                _StopEvent.Set();
                _WakeUpThreadEvent.Set();
                return true;

            }

                #endregion
        }
    }
}
