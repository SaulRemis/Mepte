using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SpinPlatform
{
    namespace Config
    {
        public class SpinConfig_Log
        {

            #region Propiedades
            private string moduleObjetive = "";
            [XmlElement("moduleObjetive")]
            public string ModuleObjetive
            {
                get
                {
                    return moduleObjetive;
                }
                set
                {
                    moduleObjetive = value;
                }
            }

            private string filePath = "";
            [XmlElement("filePath")]
            public string FilePath
            {
                get
                {
                    return filePath;
                }
                set
                {
                    filePath = value;
                }
            }

            private string dataObjetive = "";
            [XmlElement("dataObjetive")]
            public string DataObjetive
            {
                get
                {
                    return dataObjetive;
                }
                set
                {
                    dataObjetive = value;
                }
            }

            private string noOverrideFile = "";
            [XmlElement("noOverrideFile")]
            public string NoOverrideFile
            {
                get { return noOverrideFile; }
                set { noOverrideFile = value; }
            }

             private string traceLevel = "";
            [XmlElement("traceLevel")]
            public string TraceLevel
            {
                get { return traceLevel; }
                set { traceLevel = value; }
            }

            private string defaultTableName = "";
            [XmlElement("DefaultTableName")]
            public string DefaultTableName
            {
                get { return defaultTableName; }
                set { defaultTableName = value; }
            }
            #endregion

        }
    }
}
