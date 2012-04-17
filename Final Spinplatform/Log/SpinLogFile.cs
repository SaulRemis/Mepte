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

namespace SpinPlatform.Log
{
    [Serializable()]
    public class SpinLogFile : ISpinPlatformInterface
    {
        #region Variables
        //parametros
        dynamic _data = new ExpandoObject(); //variable dinamica que alamcena datos internos del módulo
        bool permiteErrores = false;
        bool permiteDesarrollo = false;
        bool permiteInformacion = false;
        readonly object _locker = new object();
        static Dictionary<string, StreamWriter> filesWrite = new Dictionary<string, StreamWriter>();//objeto encargado de la escritura oportuna
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
                _data = obj.LOGTXT; //Guardo la sección que me interesa para este módulo
            else
                _data = obj; // me enviaron directamente los datos del módulo de LOG 

            if (_data.LOGTXTTraceLevel.Contains("D"))
                permiteDesarrollo = true;
            if (_data.LOGTXTTraceLevel.Contains("I"))
                permiteInformacion = true;
            if (_data.LOGTXTTraceLevel.Contains("E"))
                permiteErrores = true;

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
        /// LOGTXT.LOGTXTMessage (string) mensaje de log a guardar \n
        /// LOGTXT.LOGTXTFilePath (string) fichero destino para guardar \n
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
                            if(permiteErrores)
                                SetData(ref data);
                            break;
                        case "Desarrollo":
                            if(permiteDesarrollo)
                                SetData(ref data);
                            break;
                        case "Informacion":
                            if(permiteInformacion)
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
        /// Abre un nuevo streamwriter para el fichero proporcionado 
        /// </summary>
        public void Start()
        {
            rotate(_data.LOGTXTFilePath);
            if (!filesWrite.ContainsKey(_data.LOGTXTFilePath))
                filesWrite.Add(_data.LOGTXTFilePath, openFileWriter(_data.LOGTXTFilePath, true));
        }

        /// <summary>
        /// Cierra todos los streamwriters abiertos por el módulo 
        /// </summary>
        public void Stop()
        {
            //Cierro los streamwriters
            Dictionary<string, StreamWriter> escritores = new Dictionary<string,StreamWriter>(filesWrite);
            foreach (string key in escritores.Keys)
            {
            filesWrite[key].Close();
            filesWrite.Remove(key);
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
            if ((data.LOGTXT as IDictionary<string, object>).ContainsKey("LOGTXTMessage"))
            {
                    //We write to log
                    lock (_locker)
                    {
                        try
                        {
                            data.LOGTXT.LOGTXTMessage = DateTime.Now + ": " + data.LOGTXT.LOGTXTMessage;
                            addMessageToFile(data.LOGTXT.LOGTXTFilePath, true, data.LOGTXT.LOGTXTMessage);
                        }
                        catch (Exception ex)
                        {
                            throw new SpinException(ex.Message, ex);
                        }
                    }
            }
            else
            {
                data.LOGTXT.LOGTXTErrors = "Message unavailable";
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
            if (!filesWrite.ContainsKey(path))
            {
                rotate(path);
                filesWrite.Add(path, openFileWriter(path, isOverride));
            }

            filesWrite[path].WriteLine(Message);
        }

        #endregion
    }
}
