﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using SpinPlatform.Data;


namespace SpinPlatform
{

    namespace Dispatcher  
    {
        /// <summary>
        /// Delegate to send event with args
        /// </summary>
        /// <param name="sender"> who send?</param>
        /// <param name="res">what to send</param>
        public delegate void ResultEventHandler(object sender, Data.DataEventArgs res);
        public enum SpinDispatcherStatus { Starting, Stopped, Stopping, Running } ;
        abstract public class SpinDispatcher : ISpinPlatformInterface
        {

            protected Dictionary<string, object> _DispatcherSharedMemory;
            protected Dictionary<string, AutoResetEvent> _Events;
            protected SpinDispatcherStatus Status;


            public Dictionary<string, SpinThread> _DispatcherThreads;
            public event ResultEventHandler NewResultEvent;  // evento para enviar nuevos resultados


            public SpinDispatcher()
            {
                _DispatcherSharedMemory = new Dictionary<string, object>();
                _DispatcherThreads = new Dictionary<string, SpinThread>();
                _Events = new Dictionary<string, AutoResetEvent>();
                Status = SpinDispatcherStatus.Stopped;

            }

            public virtual void Start()
            {
                Status = SpinDispatcherStatus.Starting;
                foreach (KeyValuePair<string, SpinThread> item in _DispatcherThreads)
                {
                     item.Value.Start();
                }
                Status = SpinDispatcherStatus.Running;
            }
            public virtual void Init(dynamic obj)
            {
            }
            abstract public void SetData(ref dynamic obj, params string[] parameters);
           
            public virtual void Stop()
            {
                if (Status == SpinDispatcherStatus.Running || Status == SpinDispatcherStatus.Starting)
                {
                    Status = SpinDispatcherStatus.Stopping;
                    //recorro todos los hilos
                    foreach (KeyValuePair<string, SpinThread> item in _DispatcherThreads)
                    {
                        item.Value.Stop();
                        item.Value.Join();

                    }
                    Status = SpinDispatcherStatus.Stopped; 
                }
            }

            abstract public void GetData(ref dynamic obj, params string[] parameters);
            protected void CreateEvent(string event_description, AutoResetEvent evento, params string[] threads)
            {
                _Events.Add(event_description, evento);
                foreach (string thread in threads)
                {
                    ((SpinThread)_DispatcherThreads[thread]).Events.Add(event_description, evento);

                }
            }
            protected void ConnectMemory(string memory, object sharedMemory, params string[] threads)
            {
                _DispatcherSharedMemory.Add(memory, sharedMemory);

                foreach (string thread in threads)
                {
                    ((SpinThread)_DispatcherThreads[thread]).SharedMemory.Add(memory, _DispatcherSharedMemory[memory]);

                }
            }
           
             protected void SetEvent(DataEventArgs args)
            {

                if (NewResultEvent != null )  // Por si nadie escucha el evento o esta en proceso de parar
                {
                    NewResultEvent(this, args);
                }
            }
        }
    }

 
}
