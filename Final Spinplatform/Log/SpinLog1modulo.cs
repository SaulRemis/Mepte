using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;
using SpinPlatform.Errors;
using SpinPlatform.IO;
using System.Reflection;
using System.Runtime.Remoting;
using System.Xml.Linq;
using System.Linq;
using System.Dynamic;
using SpinPlatform.Config;
using System.Text;
using System.IO;
using SpinPlatform.Errors;


namespace SpinPlatform
{
    namespace Log
    {
        [Serializable()]
        public class SpinLog1modulo : ISpinPlatformInterface
        {
            #region Variables
            //parametros
            String _filePath = null; //indicates the name and path where to save the log
            int _logAplicationLevel = 0; //indicates the current log level of the application.
            int _logElementLevel = 0; //indicates the level of log required for the current log element
            string _moduletoWorkWithType;
            string _moduledatatoWorkWithType;
            string _defaultTableName;
            dynamic _moduletoWorkWith; //indicates which module we will use to write-read (to make it generic)
            dynamic _moduleDataToWorkWith;
            bool _noOverrideFile;

            Mutex mMutex = new Mutex(); //synchronization
            #endregion

            #region Implementación de interface

            public void Init(object obj)
            {
                //leo del fichero de configuración
                dynamic conf = obj;
              
                //guardo parametros
                _filePath = conf.Data["filePath"]; //indicates the name and path where to save the log
                _logAplicationLevel = Int32.Parse(conf.Data["traceLevel"]); //indicates the current log level of the application.
                _logElementLevel = conf.LogElementLevel; //indicates the level of log required for the current log element
                _moduletoWorkWithType = conf.Data["moduleObjetive"]; //indicates which module we will use to write-read (to make it generic)
                _moduledatatoWorkWithType = conf.Data["dataObjetive"]; //indicates data object to send to next module
                _noOverrideFile = bool.Parse(conf.Data["noOverrideFile"]);
                _defaultTableName = conf.Data["DefaultTableName"];
            }

            public object GetData(object obj)
            {
                dynamic ob = obj;
                if ((ob.GetLastMessage || ob.GetAllMessages) && _moduletoWorkWithType == "SpinPlatform.IO.SpinIOTxt")
                {
                    //We read log
                    LockObject();

                    List<string> lineas = ReadFile(_filePath, ob.NoOverrideFiles);
                    ob.Message = lineas;
                   

                    UnlockObject();
                    if (ob.GetAllMessages)
                        ((SpinLog1moduloData)obj).Message = ob.Message;
                    else if (ob.GetLastMessage)
                    {
                        return ob.Message[ob.Message.Count - 1];
                    }
                }
                else
                {
                    ((SpinLog1moduloData)obj).Message.Clear();
                    ((SpinLog1moduloData)obj).Message.Add("We couldn´t find any needed data (check booleans) in object: SpinLogData");
                }
                return obj;
            }

            public void SetData(object obj)
            {
                dynamic ob = obj;
                if (ob.LogElementLevel >= this._logAplicationLevel && ob.Message.Count > 0 && _moduletoWorkWithType == "SpinPlatform.IO.SpinIOTxt")
                {
                    //We write to log
                    LockObject();

                    addMessageToFile(_filePath, ob.NoOverrideFiles, ob.Message);

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
            public SpinLog1modulo() : base() { }
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

            #region métodos de Rubén para base de datos por reordenar

            private static dynamic _getExpandoFromXml(String file, XElement node = null)
            {
                if (String.IsNullOrWhiteSpace(file) && node == null) return null;
                // If a file is not empty then load the xml and overwrite node with the
                // root element of the loaded document
                if (!String.IsNullOrWhiteSpace(file))
                {
                    var doc = XDocument.Load(file);
                    node = doc.Root;
                }
                dynamic result = new ExpandoObject();


                foreach (var gn in node.Elements())
                {
                    // The code determines if it is a container node based on the child
                    // elements with the same name. If there is only one child element,
                    // but it should still be treated as an container obejct then ensure
                    // the attribute "type" with value "list" is added to the node.
                    var skip = false;
                    skip = (from a in gn.Attributes()
                            where a.Name.LocalName.ToLower() == "type"
                            select a.Value.ToLower()).FirstOrDefault() == "list" ? true :
                               gn.Elements().GroupBy(n => n.Name.LocalName).Count() == 1;

                    var p = result as IDictionary<String, dynamic>;
                    var values = new List<dynamic>();
                    // If the current node is a container node then we want to skip adding
                    // the container node itself, but instead we load the children elements
                    // of the current node. If the current node has child elements then load
                    // those child elements recursively
                    /*           values.Add("mio");
                               values.Add(23);
                               values.Add(Boolean.Parse("true"));
                      */
                    if (skip)
                        foreach (var item in gn.Elements())
                        {
                            values.Add(
                                (item.HasElements) ?
                                //YES
                                    _getExpandoFromXml(null, item) :
                                //NO
                            (
                            (from a in item.Attributes()
                             where a.Name.LocalName.ToLower() == "type"
                             select a.Value.ToLower()).FirstOrDefault() == "int" ?
                            gn.Value.Trim() :
                            ((from a in item.Attributes()
                              where a.Name.LocalName.ToLower() == "type"
                              select a.Value.ToLower()).FirstOrDefault() == "bool" ? gn.Value.Trim() :
                               gn.Value.Trim()

                            )
                            )
                            )
                            ;
                        }

                    else

                        if (gn.HasElements) values.Add(_getExpandoFromXml(null, gn));
                        else if ((from a in gn.Attributes()
                                  where a.Name.LocalName.ToLower() == "type"
                                  select a.Value.ToLower()).FirstOrDefault() == "int") values.Add(Int32.Parse(gn.Value.Trim()));
                        else if ((from a in gn.Attributes()
                                  where a.Name.LocalName.ToLower() == "type"
                                  select a.Value.ToLower()).FirstOrDefault() == "bool") values.Add(Boolean.Parse(gn.Value.Trim()));
                        else values.Add(gn.Value.Trim());

                    ;


                    //                            gn.Value.Trim());

                    // Add the object name + value or value collection to the dictionary
                    p[gn.Name.LocalName] = skip ? values : values.FirstOrDefault();
                }
                return result;
            }

            #endregion


            internal void addMessageToFile(string path, bool isOverride, List<string> listMessages)
            {
                StreamWriter escritor = openFileWriter(path, isOverride);
                for (int i = 0; i < listMessages.Count; i++)
                    escritor.WriteLine(listMessages[i]);

                escritor.Close();
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
                    throw new SpinException(SpinExceptionConstants.SPIN_ERROR__FILE_NO_FOUND + ": " + path, ex);
                }
            }

            internal List<string> ReadFile(string path, bool isOverride)
            {
                StreamReader lector = openFileReader(path, isOverride);
                List<string> listMessages = new List<string>();
                string linea = "Start";
                while (linea != null)
                {
                    linea = lector.ReadLine();
                    if (linea != null)
                        listMessages.Add(linea);
                }

                lector.Close();
                return listMessages;
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
        }
    }
}