using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpinPlatform.Data;

namespace SpinPlatform
{
    namespace IO
    {
        public class SpinIODBData : ModuleData, ISpinDataInterface
        {

            #region Variables
            //parametro de entrada
            public bool GetPath = false;
            public bool _getMessages = false;

            //parametros
            String _filePath = null; //indicates the name and path where to save the log
            String _defaultTableName = null;
            List<String> _message = new List<string>(); //indicates the message to write
            bool _nooverrideFiles = true; //indica si tengo que sobreescribir el fichero o continuar la escritura
            bool _rw = false; //false reading, true writting

            #endregion

            #region Propiedades

            public String FilePath
            {
                get { return _filePath; }
                set { _filePath = value; }
            }

            public String DefaultTableName
            {
                get { return _defaultTableName; }
                set { _defaultTableName = value; }
            }

            public List<String> Message
            {
                get { return _message; }
                set { _message = value; }
            }

            public bool GetMessages
            {
                get { return _getMessages; }
                set { _getMessages = value; }
            }

            public bool RW
            {
                get { return _rw; }
                set { _rw = value; }
            }

            public bool NoOverrideFiles
            {
                get { return _nooverrideFiles; }
                set { _nooverrideFiles = value; }
            }

            #endregion

        }
    }
}
