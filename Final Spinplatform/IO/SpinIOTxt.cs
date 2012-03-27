using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using SpinPlatform.Errors;
using System.Dynamic;


namespace SpinPlatform
{
    namespace IO
    {
        internal class SpinIOTxt : ISpinPlatformInterface
        {
            /// <summary>
            /// Class to work with txt files
            /// </summary>
            
            #region Variables
            dynamic _data = new ExpandoObject();
            static Dictionary<string, StreamWriter> filesWrite = new Dictionary<string, StreamWriter>(); //objeto encargado de la  escritura oportunas
            static Dictionary<string, StreamReader> filesRead = new Dictionary<string, StreamReader>(); //objeto encargado de la lectura oportunas
            #endregion

            #region Implementación de interface

            /// <summary>
            /// Inicializa el Modulo IOTXT.
            /// </summary>
            /// <param name="obj">
            /// Campos Obligatorios:  \n
            /// IOTXTRW (bool) \n
            /// IOTXTFilePath (string) \n
            /// IOTXTNoOverrideFile (bool) \n
            /// </param>
            public void Init(dynamic obj)
            {
                _data = obj.IOTxt;

                if (_data.IOTXTRW)
                {
                    if (!filesWrite.ContainsKey(_data.IOTXTFilePath))
                        filesWrite.Add(_data.IOTXTFilePath, openFileWriter(_data.IOTXTFilePath, _data.IOTXTNoOverrideFile));
                }
                else
                {
                    if (!filesRead.ContainsKey(_data.IOTXTFilePath))
                        filesRead.Add(_data.IOTXTFilePath, openFileReader(_data.IOTXTFilePath, _data.IOTXTNoOverrideFile));
                }
            }

            /// <summary>
            /// No es necesario su uso
            /// </summary>
            public  void Start()
            {
            }


            /// <summary>
            /// Obtiene datos del Modulo IOTXT. 
            /// </summary>
            /// <param name="data">
            /// Variable dinamica donde guardar los resultados: \n
            /// Response (List(string)) resultados de "Get" \n
            /// </param>
            /// <param name="parameters">
            /// "Get" - Obtiene el documento específico \n
            /// </param>
            public void GetData(ref dynamic data, params string[] parameters)
            {
                data.IOTXTReturnedData = parameters;
                try
                {
                    foreach (string valor in parameters)
                    {
                        switch (valor)
                        {
                            case "Get":
                                GetValues(ref data);
                                break;
                            default:
                                data.IOTXTErrors = "Wrong Query";
                                break;
                        }
                    }
                    data.IOTXTErrors = "";
                }
                catch (Exception ex)
                {
                    data.IOTXTErrors = ex.Message;
                    //Ademas se lanzaria la excepcion oportuna
                }
            }

            /// <summary>
            /// Guarda texto a archivo \n
            /// </summary>
            /// <param name="data">
            /// Variable dinamica de donde obtener los datos a establecer \n
            /// Message (List(string)) Lista de mensajes a guardar \n
            /// </param>
            /// <param name="parameters">
            /// "Set" - Guarda el texto \n
            /// </param>
            public void SetData(ref dynamic data, params string[] parameters)
            {
                try
                {
                    foreach (string valor in parameters)
                    {
                        switch (valor)
                        {
                            case "Set":
                                SetValues(ref data);
                                break;
                            default:
                                break;
                        }
                    }
                    data.IOTXTErrors = "";
                }
                catch (Exception ex)
                {
                    data.IOTXTErrors = ex.Message;
                    //Ademas se lanzaria la excepcion oportuna
                }
            }


            /// <summary>
            /// Para el Módulo IOTXT
            /// </summary>
            public void Stop()
            {                 
                Dictionary<string, StreamWriter> copia = new Dictionary<string, StreamWriter>(filesWrite);
                foreach (KeyValuePair<string, StreamWriter> entrada in copia)
                {
                     filesWrite[entrada.Key].Close();
                     filesWrite.Remove(entrada.Key);
                }
                Dictionary<string, StreamReader> copiaRead = new Dictionary<string, StreamReader>(filesRead);
                foreach (KeyValuePair<string, StreamReader> entrada in copiaRead)
                {
                    filesRead[entrada.Key].Close();
                    filesRead.Remove(entrada.Key);
                } 
            }

            public event Dispatcher.ResultEventHandler NewResultEvent;

            #endregion

            #region Métodos

            internal void GetValues(ref dynamic data)
            {
                List<string> lineas = ReadFile(_data.IOTXTFilePath, _data.IOTXTNoOverrideFile);
                data.Response = lineas;
            }

            internal void SetValues(ref dynamic data)
            {
                addMessageToFile(_data.IOTXTFilePath, _data.IOTXTNoOverrideFile, data.Message);
            }

            internal StreamReader openFileReader(string path, bool optionOverride)
            {
                try
                {
                    // Con el buleano a false sobreescribe, a true añade
                    StreamReader sReader = new StreamReader(path, optionOverride);
                    return sReader;
                }
                catch (Exception ex)
                {
                    throw new SpinException(SpinExceptionConstants.SPIN_ERROR__FILE_NO_FOUND + ": " + path, ex);
                }
            }

            internal StreamWriter openFileWriter(string path, bool optionOverride)
            {
                try
                {
                    // Con el buleano a false sobreescribe, a true añade
                    StreamWriter sWriter = new StreamWriter(path, optionOverride);
                    return sWriter;
                }
                catch (Exception ex)
                {
                    throw new SpinException(SpinExceptionConstants.SPIN_ERROR__FILE_NO_FOUND + ": "+ path, ex);
                }
            }

            internal void addMessageToFile(string path, bool isOverride, List<string> listMessages)
            {
                if (!filesWrite.ContainsKey(path))
                    filesWrite.Add(path, openFileWriter(path,isOverride));
                for (int i = 0; i < listMessages.Count; i++)
                    filesWrite[path].WriteLine(listMessages[i]);
                
                filesWrite[path].Close();
                filesWrite.Remove(path);
            }

            internal List<string> ReadFile(string path, bool isOverride)
            {
                List<string> listMessages = new List<string>();
                if (!filesRead.ContainsKey(path))
                    filesRead.Add(path, openFileReader(path, isOverride));
                
                string linea="Start";
                while (linea != null)
                {
                    linea = filesRead[path].ReadLine();
                    if (linea != null)
                        listMessages.Add(linea);
                }

                filesRead[path].Close();
                filesRead.Remove(path);
                return listMessages;
            }


            internal void DeleteFileIfExists(string path)
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }

            #endregion
        }
    }
}
