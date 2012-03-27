using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Xml.Serialization;
using SpinPlatform.Errors;

namespace SpinPlatform
{
    namespace IO
    {
        class SpinIOXML : ISpinPlatformInterface
        {
            /// <summary>
            /// Class to work with XML files
            /// </summary>

            #region Implementación de interface

            public void Init(object obj)
            {
                
            }

            public void Start()
            {
            }

            public void GetData(ref dynamic data, params string[] parameters)
            {
                data = unserialize(data.GetData("FilePath"), data.GetData("StructureType"));
            }

            public void SetData(ref dynamic data, params string[] parameters)
            {
                             
            }

            public void Stop()
            {
                
            }

            public event Dispatcher.ResultEventHandler NewResultEvent;

            #endregion


            #region Métodos

            internal XmlTextReader openFileXml(string path)
            {
                XmlTextReader reader = new XmlTextReader((string)path);
                return reader;
            }

            internal void saveFileXml(object obj)
            {

            }

            internal void serialize(string filename, object obj)
            {
                StreamWriter stream = null;
                try
                {
                    SpinIOTxt modulo = new SpinIOTxt();
                    stream = modulo.openFileWriter(filename, false);
                    XmlSerializer serializer = new XmlSerializer(obj.GetType());
                    serializer.Serialize(stream, obj);
                }
                finally
                {
                    if (stream != null)
                        stream.Close();
                }
            }

            internal object unserialize(string filename, Type t)
            {
                StreamReader stream = null;

                try
                {
                    SpinIOTxt modulo = new SpinIOTxt();
                    stream = modulo.openFileReader(filename, false);
                    XmlSerializer serializer = new XmlSerializer(t);
                    return serializer.Deserialize(stream);
                }
                catch (Exception ex)
                {
                    throw SpinException.GetException(SpinExceptionConstants.SPIN_ERROR_XML_UNSERIALIZING + filename, ex);
                }
                finally
                {
                    if (stream != null)
                        stream.Close();
                }
            }


            #endregion
        }
    }
}
