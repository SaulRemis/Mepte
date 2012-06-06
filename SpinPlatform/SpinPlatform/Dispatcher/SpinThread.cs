using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace SpinPlatform
{
    public abstract class SpinThread
    {
        #region Private variables
        protected ManualResetEvent _StopEvent;    ///< Event used to stop the thread.
        protected Thread _MainThread;             ///< Handle of the thread.
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
            /// This function MUST be overrided in derived classes to define custom functionality
            /// </summary>
            public abstract void FunctionToExecuteByThread();
            


            /// <summary>
            /// This function MUST be overrided in derived classes to define start actions
            /// </summary>
            public virtual void Start(){
                _StopEvent.Reset(); //reset stop event   
                _MainThread = new Thread(new ThreadStart(EntryFunctionThread)); // Create thread
                _MainThread.Name = _Name;
                _MainThread.Start();  //The thread is started and will call EntryFunctionThread
    }

                
                /// <summary>
            /// To get the Name of the thread
            /// </summary>
            /// <returns> THe by defect name of the thread</returns>
            public string GetName()
            {
                return _MainThread.Name;
            }


            public  void Join()
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
            
            public abstract void Initializate();
            
            public abstract void Closing();
            
            /// <summary>
            /// The main thread will call this function when it starts.
            /// </summary>
            public abstract void EntryFunctionThread();
            

            #endregion




    }
}
