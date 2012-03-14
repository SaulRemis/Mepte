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
        public class SpinThread
        {
            #region Private variables
            protected ManualResetEvent _StopEvent;    ///< Event used to stop the thread.
            protected Thread _MainThread;             ///< Handle of the thread.
            protected int _MillisecondsToSleep;       ///< The milliseconds that the thread will be sleept each time that FunctionToExecuteByThread is executed.
            protected ThreadPriority _Priority;       ///< Priority of the thread    
            protected string _Name;       ///< Priority of the thread  
            protected Dictionary<string, object> _SharedMemory;
            protected Dictionary<string, AutoResetEvent> _Events;

            public Dictionary<string, AutoResetEvent> Events
            {
                get { return _Events; }
                set { _Events = value; }
            }
            public Dictionary<string, object> SharedMemory
            {
                get { return _SharedMemory; }
                set { _SharedMemory = value; }
            }


            #endregion

            #region Public functions
            /// <summary>
            /// This function should be overrided in derived class to define custom functionality
            /// </summary>
            public virtual void FunctionToExecuteByThread()
            {
                Console.WriteLine("Function called by thread");
            }


            /// <summary>
            /// Constructor of the SpinThread Class
            /// </summary>
            public SpinThread(string name)
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
            public SpinThread(string name, ThreadPriority priority, int milliseconds)
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

            /// <summary>
            /// To start the thread
            /// </summary>
            public void Start()
            {
                _StopEvent.Reset(); //reset stop event   
                _MainThread = new Thread(new ThreadStart(EntryFunctionThread)); // Create thread
                _MainThread.Name = _Name;
                _MainThread.Start();  //The thread is started and will call EntryFunctionThread

            }

            /// <summary>
            /// To get the Name of the thread
            /// </summary>
            /// <returns> THe by defect name of the thread</returns>
            string GetName()
            {
                return _MainThread.Name;

            }

            public virtual void Join()
            {
                _MainThread.Join();
            }

            /// <summary>
            /// To stop the thread
            /// </summary>
            /// <returns>True when the thread ends</returns>
            public virtual bool Stop()
            {
                _StopEvent.Set();

                return true;

            }

            public virtual void Initializate()
            {
            }

            public virtual void Closing()
            {
            }
            /// <summary>
            /// The main thread will call this function when it starts.
            /// </summary>
            public virtual void EntryFunctionThread()
            {
                Initializate();
                do
                {
                    FunctionToExecuteByThread();
                } while (!_StopEvent.WaitOne(_MillisecondsToSleep, true));
                 Closing();
            }
            #endregion






        }
    }

}
