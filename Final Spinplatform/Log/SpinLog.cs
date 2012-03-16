using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;
using SpinPlatform.Errors;
using SpinPlatform.IO;
using SpinPlatform.Data;
using System.Reflection;
using System.Runtime.Remoting;
using System.Xml.Linq;
using System.Linq;
using System.Dynamic;
using SpinPlatform.Config;

namespace SpinPlatform
{
    namespace Log
    {
        [Serializable()]
        public class SpinLog : ISpinPlatformInterface
        {
            #region Variables
            //parametros
            string _moduletoWorkWithType;
            dynamic _moduletoWorkWith; //indicates which module we will use to write-read (to make it generic)



            Mutex mMutex = new Mutex(); //synchronization
            #endregion

            #region Implementación de interface

            public void Init(dynamic obj)
            {
                _moduletoWorkWithType = obj.IOModuleObjetive;
                Assembly assembly = Assembly.GetExecutingAssembly();
                ObjectHandle ManipularObjeto = AppDomain.CurrentDomain.CreateInstance(assembly.FullName, _moduletoWorkWithType);
                _moduletoWorkWith = (ISpinPlatformInterface)ManipularObjeto.Unwrap();
                _moduletoWorkWith.Init(obj); 
            }

            public object GetData(dynamic obj)
            {
                if (obj.IOGetLastMessage || obj.IOGetAllMessages)
                {
                    //We read log
                    LockObject();
                    _moduletoWorkWith.Start();
                    
                    try
                    {
                        obj = _moduletoWorkWith.GetData(obj);
                    }
                    catch (Exception ex)
                    {
                        throw new SpinException(ex.Message, ex);
                    }

                    _moduletoWorkWith.Stop();
                    UnlockObject();

                    if (obj.IOGetLastMessage)
                    {
                        string linea = obj.Message[obj.IOMessage.Count - 1];
                        obj.IOMessage.Clear();
                        obj.IOMessage.Add(linea);
                    }
                }
                else
                {
                    obj.IOMessage.Clear();
                    obj.IOMessage.Add("We couldn´t find any needed data (check booleans) in object: SpinLogData");
                }
                return obj;
            }

            public void SetData(dynamic obj)
            {
                if (obj.IOLogElementLevel >= obj.IOLogAplicationLevel && obj.IOMessage.Count > 0)
                {
                    //We write to log
                    LockObject();
                    _moduletoWorkWith.Start();
                     try
                     {
                         _moduletoWorkWith.SetData(obj);
                     }
                     catch (Exception ex)
                     {
                         throw new SpinException(ex.Message, ex);
                     }
                     _moduletoWorkWith.Stop();

                    UnlockObject();
                }
            }

            public void Start()
            {
            }

            public void Stop()
            {
            }

            public event Dispatcher.ResultEventHandler NewResultEvent;

            #endregion

            #region Constructores
            public SpinLog(): base() { }
            #endregion

            #region Lock/Unlock mutex

            public void LockObject()
            {
                mMutex.WaitOne();
            }

            public void UnlockObject()
            {
                mMutex.ReleaseMutex();
            }

            #endregion

        }
    }
}