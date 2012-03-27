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
        public class SpinThreadWhile : SpinThread
        {

            protected int _MillisecondsToSleep;       ///< The milliseconds that the thread will be sleept each time that FunctionToExecuteByThread is executed.
   

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
            public SpinThreadWhile(string name)
            {
                _MainThread = new Thread(new ThreadStart(EntryFunctionThread)); // Create thread
                _StopEvent = new ManualResetEvent(false);
                _SharedMemory = new Dictionary<string, object>();
                _Events = new Dictionary<string, AutoResetEvent>();
                _Priority = ThreadPriority.Highest;
                _MillisecondsToSleep = 10;
                _Name = name;


            }

            /// <summary>
            /// Constructor of SpinThread Class with the Priority of the Thread and the milliseconds to wait
            /// </summary>
            /// <param name="priority">Priority of the Thread</param>
            /// <param name="milliseconds">milliseconds to wait</param>
            public SpinThreadWhile(string name, ThreadPriority priority, int milliseconds)
            {
              
                _StopEvent = new ManualResetEvent(false);
                _SharedMemory = new Dictionary<string, object>();
                _Events = new Dictionary<string, AutoResetEvent>();
                _Priority = priority;
                _MillisecondsToSleep = milliseconds;
                _Name = name;

            }
            /// <summary>
            /// To set the _MillisecondsToSleep
            /// </summary>
            public int MillisecondsToSleep
            {
                set { _MillisecondsToSleep = value; }
            }

           
         

            public override void Initializate()
            {
            }

            public override void Closing()
            {
                Trace.WriteLine("Saliendo del spinthreadwhile");
            }
            /// <summary>
            /// The main thread will call this function when it starts.
            /// </summary>
            public override void EntryFunctionThread()
            {
                Initializate();
                do
                {
                    FunctionToExecuteByThread();
                } while (!_StopEvent.WaitOne(_MillisecondsToSleep, true));
                 //Trace.WriteLine("Estado del evento stop al salir del hilo: " + _StopEvent.WaitOne(0, true));
                Closing();
            }
            #endregion






        }
    }

}
