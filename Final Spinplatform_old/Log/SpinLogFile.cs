using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;
using SpinPlatform.Errors;
using SpinPlatform.Data;
using System.Reflection;
using System.Runtime.Remoting;
using System.Xml.Linq;
using System.Linq;
using System.Dynamic;
using SpinPlatform.Config;

namespace SpinPlatform.Log
{
    [Serializable()]
    public class SpinLogFile : ISpinPlatformInterface
    {
        /// <summary>
        /// Módulo para LOGS a archivo
        /// Éste módulo realiza la escritura a un LOG o varios de las trazas que vayamos poniendo en nuestro código.
        /// Guarda cada entrada con un TIMESTAMP
        /// Existen tres niveles de trazas Desarrollo, Errores e Información que podrán o no escribirse al log en función de los parámetros
        /// del fichero de configuración.
        /// NOTA: Para su uso solo es necesario realizar un "Init" de configuración y un "Start" para rotar hasta 9 ficheros logs. 
        /// Posteriormente usaremos el log como : nombreModulo.SetData("Frase a escribir","Desarrollo"); y cerraremos con un "Stop" al terminar si nos lo permite la aplicación (No es estrictamente necesario).
        /// No es necesario el uso del método GetData (Sin implementar)
        /// </summary>

        #region Variables
        private string _LOGTXTTraceLevel;
        private int _LOGTXTMaxFileSize;
        private string _LOGTXTFilePath;
        private bool _permiteErrores = false;
        private bool _permiteDesarrollo = false;
        private bool _permiteInformacion = false;
        private readonly object _locker = new object();
        static Dictionary<string, StreamWriter> _filesWrite = new Dictionary<string, StreamWriter>();//objeto encargado de la escritura oportuna
        #endregion

        #region Implementación de interface

        /// <summary>
        /// Inicializa el Modulo LOG. \n
        /// Se le puede enviar en el init como parámetro tanto el objeto completo de configuración con el nombre genérico (LOGTXT) como el módulo LOG directamente con cualquier nombre. 
        /// </summary>
        /// <param name="obj">
        /// Campos Obligatorios:  \n
        /// LOGTXTTraceLevel (string) \n
        /// LOGTXTMaxFileSize (int) \n
        /// LOGTXTFilePath (string) \n
        /// </param>
        public void Init(dynamic obj)
        {
            if ((obj as IDictionary<string, object>).ContainsKey("LOGTXT"))
            {
                _LOGTXTTraceLevel = obj.LOGTXT.LOGTXTTraceLevel ;
                _LOGTXTMaxFileSize = obj.LOGTXT.LOGTXTMaxFileSize;
                _LOGTXTFilePath = obj.LOGTXT.LOGTXTFilePath;
            }
            else
            {
                _LOGTXTTraceLevel = obj.LOGTXTTraceLevel;
                _LOGTXTMaxFileSize = obj.LOGTXTMaxFileSize;
                _LOGTXTFilePath = obj.LOGTXTFilePath;
            }

            if (_LOGTXTTraceLevel.Contains("D"))
                _permiteDesarrollo = true;
            if (_LOGTXTTraceLevel.Contains("I"))
                _permiteInformacion = true;
            if (_LOGTXTTraceLevel.Contains("E"))
                _permiteErrores = true;

            Start();
        }

        /// <summary>
        /// Sin implementar 
        /// </summary>
        /// <param name="data">
        /// Sin implementar
        /// </param>
        /// <param name="parameters">
        /// Sin implementar
        /// </param>
        public void GetData(ref dynamic data, params string[] parameters)
        { 
        }

        /// <summary>
        /// Guarda datos del módulo Log \n
        /// </summary>
        /// <param name="data">
        /// Variable dinamica de donde obtener los datos a establecer \n
        /// LOGTXTMessage (string) mensaje de log a guardar \n
        /// LOGTXTFilePath (string) fichero destino para guardar \n
        /// </param>
        /// <param name="parameters">
        /// "Error" - Guarda una nueva línea en el log de tipo Error si la configuración del módulo lo permite \n
        /// "Desarrollo" - Guarda una nueva línea en el log de tipo desarrollo si la configuración del módulo lo permite \n
        /// "Informacion" - Guarda una nueva línea en el log de tipo información si la configuración del módulo lo permite \n
        /// </param>
        public void SetData(ref dynamic data, params string[] parameters)
        {
            try
            {
                data.LOGTXTErrors = "";
                foreach (string valor in parameters)
                {
                    switch (valor)
                    {
                        case "Error":
                            if(_permiteErrores)
                                SetData(ref data);
                            break;
                        case "Desarrollo":
                            if(_permiteDesarrollo)
                                SetData(ref data);
                            break;
                        case "Informacion":
                            if(_permiteInformacion)
                                SetData(ref data);
                            break;
                        default:
                            data.LOGTXTErrors = "Wrong Query";
                            break;
                    }
                } 
            }
            catch (Exception ex)
            {
                data.LOGTXTErrors = ex.Message;
                //Ademas se lanzaria la excepcion oportuna
            }
        }

        /// <summary>
        /// Realiza un rotate para el fichero proporcionado 
        /// </summary>
        public void Start()
        {
            rotate(_LOGTXTFilePath);
        }

        /// <summary>
        /// Cierra todos los streamwriters abiertos por el módulo 
        /// </summary>
        public void Stop()
        {
            //Cierro los streamwriters
            Dictionary<string, StreamWriter> escritores = new Dictionary<string,StreamWriter>(_filesWrite);
            foreach (string key in escritores.Keys)
            {
            _filesWrite[key].Close();
            _filesWrite.Remove(key);
            }
        }

        public event Dispatcher.ResultEventHandler NewResultEvent;

        #endregion

        #region métodos

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

        internal void SetData(ref dynamic data)
        {
            dynamic contenido;
            if ((data as IDictionary<string, object>).ContainsKey("LOGTXT"))
                contenido = data.LOGTXT;
            else
                contenido = data;

            if ((contenido as IDictionary<string, object>).ContainsKey("LOGTXTMessage"))
                {
                    //We write to log
                    lock (_locker)
                    {
                        try
                        {
                            contenido.LOGTXTMessage = DateTime.Now + ": " + contenido.LOGTXTMessage;
                            if ((contenido as IDictionary<string, object>).ContainsKey("LOGTXTFilePath"))
                                addMessageToFile(contenido.LOGTXTFilePath, true, contenido.LOGTXTMessage);
                            else
                                addMessageToFile(_LOGTXTFilePath, true, contenido.LOGTXTMessage);
                        }
                        catch (Exception ex)
                        {
                            throw new SpinException(ex.Message, ex);
                        }
                    }
                }
                else
                {
                    
                    if ((data as IDictionary<string, object>).ContainsKey("LOGTXT"))
                        data.LOGTXT.LOGTXTErrors = "Message unavailable";
                    else
                        data.LOGTXTErrors = "Message unavailable";
                }
     
        }

        internal void rotate(string path)
        {
            //Roto los archivos destino
            string name = path.Substring(0,path.IndexOf(".")-1);
            string extension = path.Substring(path.IndexOf("."),path.Length-path.IndexOf("."));
            if (File.Exists(name + "9" + extension))
                File.Delete(name + "9" + extension);
            for (int i = 8; i >= 0; i--)
            {
                if (File.Exists(name + i + extension))
                {
                    File.Move(name + i + extension, name + (i+1) + extension);
                }
            }
        }

        internal void addMessageToFile(string path, bool isOverride, string Message)
        {

            if (!_filesWrite.ContainsKey(path))
            {
             //   rotate(path);
                _filesWrite.Add(path, openFileWriter(path, isOverride));
            }
            _filesWrite[path].WriteLine(Message);
            _filesWrite[path].Close();
            _filesWrite.Remove(path);
        }

        #endregion
    }
}
