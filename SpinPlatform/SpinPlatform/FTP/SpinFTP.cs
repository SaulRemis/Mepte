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
using System.Net;
using System.Net.Sockets;

namespace SpinPlatform.FTP
{
    [Serializable()]
    public class  SpinFTP : ISpinPlatformInterface
    {
        /// <summary>
        /// Módulo para trabajo con archivos por FTP
        /// Éste módulo tiene implementadas las siguientes funcionalidades: Comprobar existencia de directorio, crear directorio y subir archivo.
        /// NOTA: Para su uso se emplea un "Init" de configuración, "Start" para conectarse con el servidor, subsiguientes "SetData" y "GetData"
        /// para terminar con un "Stop" para cerrar la conexión
        /// </summary>
        #region Variables
        private string _FTPDireccionRemota;
        private string _FTPUsuario;
        private string _FTPPassword;
        private int _FTPIntentos;
        private FtpLib.FtpConnection clienteFTP;//cliente FTP  
        int intentos = 0;

        #endregion

        #region Implementación de interface

        /// <summary>
        /// Inicializa el Modulo FTP.
        /// </summary>
        /// <param name="obj">
        /// Campos Obligatorios:  \n
        /// FTPDireccionRemota (string) \n
        /// FTPUsuario (string) \n
        /// FTPPassword (string) \n
        /// FTPIntentos (int) \n
        /// </param>
        public void Init(dynamic obj)
        {
            if ((obj as IDictionary<string, object>).ContainsKey("FTP"))
            {
                _FTPDireccionRemota = obj.FTP.FTPDireccionRemota;
                _FTPUsuario = obj.FTP.FTPUsuario;
                _FTPPassword = obj.FTP.FTPPassword;
                _FTPIntentos = obj.FTP.FTPIntentos;
            }
            else
            {
                _FTPDireccionRemota = obj.FTPDireccionRemota;
                _FTPUsuario = obj.FTPUsuario;
                _FTPPassword = obj.FTPPassword;
                _FTPIntentos = obj.FTPIntentos;
            }
            clienteFTP = new FtpLib.FtpConnection(_FTPDireccionRemota, _FTPUsuario, _FTPPassword);//Cliente FTP
        }

        /// <summary>
        /// Abre la conexión FTP
        /// </summary>
        public void Start()
        {
            try
            {
                clienteFTP.Open();
                clienteFTP.Login();
            }
            catch(Exception ex)
            {
                throw SpinException.GetException("SpinFTP:: "+ex.Message,ex);
            }
        }

        /// <summary>
        /// Hace comprobaciones en el servidor FTP y obtiene datos
        /// </summary>
        /// <param name="data">
        /// Variable dinamica donde guardar los resultados: \n
        /// FTPExisteDirectorio (bool) resultados de "ExisteDirectorio" \n
        /// </param>
        /// <param name="parameters">
        /// "ExisteDirectorio" - Comprueba la existencia del Directorio, necesita parámetro FTPPathDirectorio (string) \n
        /// </param>
        public void GetData(ref dynamic data, params string[] parameters)
        {
            data.FTPReturnedData = parameters;
            try
            {
                data.FTPErrors = "";
                foreach (string parameter in parameters)
                {
                    switch (parameter)
                    {
                        case "ExisteDirectorio":
                            ExisteDirectorio(ref data);
                            break;
                        default:
                            data.FTPErrors = "Wrong Query";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                data.FTPErrors = ex.Message;
            }
        }

        /// <summary>
        /// Guarda datos al servidor FTP \n
        /// </summary>
        /// <param name="data">
        /// Variable dinamica de donde obtener los datos a establecer \n
        /// FTPNombreArchivo (string) nombre de archivo a subir atraves de"SubirArchivo" \n
        /// FTPPathDirectorio (string) path de directorio a crear atraves de"CrearDirectorio" \n
        /// </param>
        /// <param name="parameters">
        /// "SubirArchivo" - Sube un archivo al servidor FTP \n
        /// "CrearDirectorio" - Crear una nueva carpeta en el servidor FTP \n
        /// </param>
        public void SetData(ref dynamic data, params string[] parameters)
        {
            try
            {
                data.FTPErrors = "";
                foreach (string valor in parameters)
                {
                    switch (valor)
                    {
                        case "SubirArchivo":
                            EnviarArchivoFTP(data);
                            break;
                        case "CrearDirectorio":
                            CrearDirectorioFTP(data);
                            break;
                        default:
                            data.FTPErrors = "Wrong Query";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                data.FTPErrors = ex.Message;
            }
        }

        /// <summary>
        /// Cierra conexión FTP
        /// </summary>
        public void Stop()
        {
            try
            {
                clienteFTP.Close();
            }
            catch (Exception ex)
            {
                throw SpinException.GetException("SpinFTP:: " + ex.Message, ex);
            }

        }

        public event Dispatcher.ResultEventHandler NewResultEvent;

        #endregion

        #region métodos

        private bool ExisteDirectorio(ref dynamic datos)
        {
            intentos = _FTPIntentos;
            datos.FTPExisteDirectorio = false;
            while (intentos > 0)
            {
                try
                {
                    datos.FTPExisteDirectorio = clienteFTP.DirectoryExists(datos.FTPPathDirectorio);
                    return true;
                }
                catch
                {
                    intentos = intentos - 1;
                }
            }
            if (intentos == 0)
                throw new SpinException("SpinFTP::No se ha conseguido subir el archivo");
            return false;
        }

        private bool EnviarArchivoFTP(dynamic datos)
        {
            intentos = _FTPIntentos;
            while (intentos > 0)
            {
                try
                {
                    clienteFTP.PutFile(datos.FTPNombreArchivo);
                    return true;
                }
                catch
                {
                    intentos = intentos - 1;
                }
            }
            if (intentos == 0)
                throw new SpinException("SpinFTP::No se ha conseguido subir el archivo");
            return false;
        }

        private bool CrearDirectorioFTP(dynamic datos)
        {
            intentos = _FTPIntentos;
            while (intentos > 0)
            {
                try
                {
                    clienteFTP.CreateDirectory(datos.FTPPathDirectorio);
                    return true;
                }
                catch
                {
                    intentos = intentos - 1;
                }
            }
            if (intentos == 0)
                throw new SpinException("SpinFTP::No se ha conseguido subir el archivo");
            return false;
        }

        #endregion
    }
}
