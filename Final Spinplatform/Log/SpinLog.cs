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
            dynamic _data = new ExpandoObject();
            readonly object _locker = new object();

            #endregion

            #region Implementación de interface

            /// <summary>
            /// Inicializa el Modulo LOG.
            /// </summary>
            /// <param name="obj">
            /// Campos Obligatorios:  \n
            /// LOGModuleObjetive (string) \n
            /// LOGAplicationTraceLevel (int) \n
            /// LOGElementTraceLevel (int) \n
            /// </param>
            public void Init(dynamic obj)
            {
                _data = obj.Log;

                Assembly assembly = Assembly.GetExecutingAssembly();
                ObjectHandle ManipularObjeto = AppDomain.CurrentDomain.CreateInstance(assembly.FullName, _data.LOGModuleObjetive);
                _data.LOGModule = (ISpinPlatformInterface)ManipularObjeto.Unwrap();
                _data.LOGModule.Init(obj);
            }

            /// <summary>
            /// Obtiene datos del Modulo Log. 
            /// </summary>
            /// <param name="data">
            /// Variable dinamica donde guardar los resultados: \n
            /// LOGLastLog (List(string)) resultados de "IOGetLastMessage" \n
            /// LOGAllLog (List(string)) resultados de "IOGetAllMessages" \n
            /// </param>
            /// <param name="parameters">
            /// "LOGGetLastMessage" - Obtiene el último mensaje de log almacenado \n
            /// "LOGGetAllMessages"  - Obtiene todos los mensajes de log almacenados \n
            /// </param>
            public void GetData(ref dynamic data, params string[] parameters)
            {
                data.LOGReturnedData = parameters;
                try
                {
                    foreach (string valor in parameters)
                    {
                        switch (valor)
                        {
                            case "LOGGetLastMessage":
                                GetData(ref data);
                                string linea = data.Log.Response[data.Log.Response.Count - 1];
                                data.Log.Response.Clear();
                                data.Log.Response.Add(linea);
                                break;
                            case "LOGGetAllMessages":
                                GetData(ref data);
                                break;
                            default:
                                data.LOGErrors = "Wrong Query";
                                break;
                        }
                    }
                    data.LOGErrors = "";
                }
                catch (Exception ex)
                {
                    data.LOGErrors = ex.Message;
                    //Ademas se lanzaria la excepcion oportuna
                }
            }

            /// <summary>
            /// Guarda datos del módulo Log \n
            /// </summary>
            /// <param name="data">
            /// Variable dinamica de donde obtener los datos a establecer \n
            /// Message (List(string)) Lista de mensajes de log a guardar \n
            /// </param>
            /// <param name="parameters">
            /// "WriteLine" - Guarda una nueva línea en el log \n
            /// </param>
            public void SetData(ref dynamic data, params string[] parameters)
            {
                try
                {
                    foreach (string valor in parameters)
                    {
                        switch (valor)
                        {
                            case "WriteLine":
                                SetData(ref data);
                                break;
                            default:
                                data.LOGErrors = "Wrong Query";
                                break;
                        }
                    }
                    data.LOGErrors = "";
                }
                catch (Exception ex)
                {
                    data.LOGErrors = ex.Message;
                    //Ademas se lanzaria la excepcion oportuna
                }
            }

            /// <summary>
            /// No es necesario su uso
            /// </summary>
            public void Start()
            {
            }

            /// <summary>
            /// No es necesario su uso
            /// </summary>
            public void Stop()
            {
            }

            public event Dispatcher.ResultEventHandler NewResultEvent;

            #endregion

            #region Constructores
            public SpinLog(): base() { }
            #endregion

            #region Métodos

            void GetData(ref dynamic data)
            {
            lock (_locker)
                {
                    _data.LOGModule.Start();

                    try
                    {
                        _data.LOGModule.GetData(ref data, "Get");
                    }
                    catch (Exception ex)
                    {
                        throw new SpinException(ex.Message, ex);
                    }

                    _data.LOGModule.Stop();
                }
            }

            void SetData(ref dynamic data)
            {
                if ((data as IDictionary<string, object>).ContainsKey("Message"))
                {
                    if (_data.LOGElementTraceLevel >= _data.LOGAplicationTraceLevel)
                    {
                        //We write to log
                        lock (_locker)
                        {
                            _data.LOGModule.Start();
                            try
                            {
                                data.Message[0] = DateTime.Now + ": " + data.Message[0];
                                _data.LOGModule.SetData(ref data, "Set");
                            }
                            catch (Exception ex)
                            {
                                throw new SpinException(ex.Message, ex);
                            }
                            _data.LOGModule.Stop();

                        }
                    }
                }
            }

            #endregion

        }
    }
}