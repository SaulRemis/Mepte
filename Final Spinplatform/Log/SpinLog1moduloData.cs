using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpinPlatform.Data;

namespace SpinPlatform
{
    namespace Log
    {
        public class SpinLog1moduloData : ModuleData
        {

            #region Variables
            //parametro de entrada
            public bool GetLastMessage = false; //Get just the last log message
            public bool GetAllMessages = false; //Get the entire log message (if both at true,this is prioritarie)


            //parametros
            String _filePath = null; //indicates the name and path where to save the log
            List<String> _message = new List<string>(); //indicates the message to save
            //parametros
            Dictionary<String, String> _data = new Dictionary<string, string>(); //indicates the name and path where to save the log
            int _logAplicationLevel = 0; //indicates the current log level of the application.
            int _logElementLevel = 0; //indicates the level of log required for the current log element
            String _moduletoWorkWith = null; //indicates which module we will use to write-read (to make it generic)
            String _moduledatatoWorkWith = null; //indicates which module we will use to write-read (to make it generic)
            bool _nooverrideFiles; //if false, it overrides the log file! (auto-configurable in configuration file)
            #endregion

            #region Propiedades

            public Dictionary<String, String> Data
            {
                get { return _data; }
                set { _data = value; }
            }

            public String FilePath
            {
                get { return _filePath; }
                set { _filePath = value; }
            }

            public List<String> Message
            {
                get { return _message; }
                set { _message = value; }
            }

            public bool NoOverrideFiles
            {
                get { return _nooverrideFiles; }
                set { _nooverrideFiles = value; }
            }

            public int LogApplicationLevel
            {
                get { return _logAplicationLevel; }
                set { _logAplicationLevel = value; }
            }

            public int LogElementLevel
            {
                get { return _logElementLevel; }
                set { _logElementLevel = value; }
            }
            String _milisegundos = null;

            public String ModuleToWorkWith
            {
                get { return _moduletoWorkWith; }
                set { _moduletoWorkWith = value; }
            }

            public String ModuleDataToWorkWith
            {
                get { return _moduledatatoWorkWith; }
                set { _moduledatatoWorkWith = value; }
            }

            #endregion

        }
    }
}
