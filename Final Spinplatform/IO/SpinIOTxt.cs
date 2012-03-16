using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using SpinPlatform.Errors;


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
            static Dictionary<string, StreamWriter> filesWrite = new Dictionary<string, StreamWriter>(); //objeto encargado de la  escritura oportunas
            static Dictionary<string, StreamReader> filesRead = new Dictionary<string, StreamReader>(); //objeto encargado de la lectura oportunas
            #endregion

            #region Implementación de interface

            public void Init(dynamic obj)
            {
                if (obj.IORW)
                {
                    if (!filesWrite.ContainsKey(obj.IOFilePath))
                        filesWrite.Add(obj.IOFilePath, openFileWriter(obj.IOFilePath, obj.IONoOverrideFile));
                }
                else
                {
                    if (!filesRead.ContainsKey(obj.IOFilePath))
                        filesRead.Add(obj.IOFilePath, openFileReader(obj.IOFilePath, obj.IONoOverrideFile));
                }
            }

            public  void Start()
            {
            }

            public object GetData(dynamic obj)
            {
                List<string> lineas = ReadFile(obj.IOFilePath, obj.IONoOverrideFile);
                obj.IOMessage = lineas;
                return obj;
            }

            public void SetData(dynamic obj)
            {  
                addMessageToFile(obj.IOFilePath, obj.IONoOverrideFile, obj.IOMessage);
            }

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
