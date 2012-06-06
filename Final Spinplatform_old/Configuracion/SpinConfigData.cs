using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpinPlatform.Data;

namespace SpinPlatform
{
    namespace Config
    {
        public class SpinConfigData : ModuleData
        {
            #region Variables
            //parametro de entrada
            //Para cada nuevo módulo que lea de configuración hay que meter un nuevo bool
            public bool GetLog = false;     //indica qué parámetros leo
            public string Name = "";        // y su nombre

            //parametros
            Dictionary<String,String> _data = new Dictionary<string,string>(); //indicates the name and path where to save the log

            #endregion

            #region Propiedades

            public Dictionary<String, String> Data
            {
                get { return _data; }
                set { _data = value; }
            }

            #endregion
        }
    }
}

