using System;
using System.IO;
using System.Text;

using System.Threading;
using System.Xml;
using System.Runtime.InteropServices;

using SpinPlatform;


namespace SpinPlatform.IO
{
    public class CFiles: ISpinPlatformInterface
    {
        XmlDocument _XMLConfigFile;
        private string _ConfigFilePath;
        private string _LogFilePath;
        private string _ErrorFilePath;
        /// <summary>
        /// Constructor of the CFiles class
        /// </summary>
        public CFiles()
        {
        }
        /// <summary>
        /// Function to rename the log file and error file
        /// </summary>
        private void RotateFiles()
        {
            //Create log file
            if (System.IO.File.Exists(_LogFilePath))
            {
                for (int i = 1; i < 100; i++)
                {
                    if (!System.IO.File.Exists(_LogFilePath.Substring(_LogFilePath.Length - 4) + "(" + i.ToString() + ").txt"))
                    {
                        System.IO.File.Copy(_LogFilePath, _LogFilePath.Substring(_LogFilePath.Length - 4) + "(" + i.ToString() + ").txt");
                        System.IO.File.Delete(_LogFilePath);
                        System.IO.File.Create(_LogFilePath);
                        break;
                    }
                }
            }
            else System.IO.File.Create(_LogFilePath);
            //Create error file
            if (System.IO.File.Exists(_ErrorFilePath))
            {
                for (int i = 1; i < 100; i++)
                {
                    if (!System.IO.File.Exists(_ErrorFilePath.Substring(_ErrorFilePath.Length - 4) + "(" + i.ToString() + ").txt"))
                    {
                        System.IO.File.Copy(_ErrorFilePath, _ErrorFilePath.Substring(_ErrorFilePath.Length - 4) + "(" + i.ToString() + ").txt");
                        System.IO.File.Delete(_ErrorFilePath);
                        System.IO.File.Create(_ErrorFilePath);
                        break;
                    }
                }
            }
            else System.IO.File.Create(_ErrorFilePath);
        }
        /// <summary>
        /// Add a line in a text file
        /// </summary>
        /// <param name="path"></param>
        /// <param name="line"></param>
        private void WriteLine(string path, string line)
        {
            if (System.IO.File.Exists(path))
            {
                try
                {
                    StreamWriter escritor = new StreamWriter(path, true);
                    escritor.WriteLine(line);
                    escritor.Flush();
                    escritor.Close();
                }
                catch (Exception e1)
                {
                    //System.Windows.Forms.MessageBox.Show("Error escritura archivo configuracion(variable '" + Linea + "' inexistente).");
                }
            }
        }
        /// <summary>
        /// Function to read a variable from the configuration file
        /// </summary>
        /// <param name="variableName"></param>
        /// <returns>Value of the configuration variable</returns>
        private string ReadXML(string variableName)
        {
            try
            {
                XmlNodeList Nodo = _XMLConfigFile.GetElementsByTagName(variableName);
                return Nodo[0].InnerText;
            }
            catch (Exception e1)
            {
                //EscribirLinea("C://Sable//SableError.txt", fechaActual() + "Error lectura archivo configuracion(variable '"+nombreVariable+ "' inexistente).");
                
                return "-1";
            }
        }
        /// <summary>
        /// Function to modify the value(s) of a variable(s) from the configuration file
        /// </summary>
        /// <param name="variableNames"></param>
        /// <param name="values"></param>
        /// <param name="save"> Permanently save the changes in the configuration file </param>
        private void WriteXML(string[] variableNames, string[] values, bool save)
        {
            for (int i = 0; i < variableNames.Length; i++)
            {
                XmlNodeList ListaNodos = _XMLConfigFile.GetElementsByTagName(variableNames[i]);
                ListaNodos[0].InnerText = values[i];
            }
            if (save)
            {
                _XMLConfigFile.Save(_ConfigFilePath);
            }

        }

        public void Start()
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }

        public object GetData(object parameters)
        {
            if (parameters.GetType() == typeof(CFilesData))
            {
                CFilesData data = (CFilesData)parameters;

                if (data.GetVariable)
                {
                    data.ReadValue = ReadXML(data.ReadVariable);
                }

                return data;
            }
            return null;
        }

        public void Init(object parameters)
        {
            if (parameters.GetType() == typeof(CFilesData))
            {
                CFilesData data = (CFilesData)parameters;

                if (data.ConfigFilePath != null)
                {
                    _ConfigFilePath = data.ConfigFilePath;
                }
                else
                {
                    throw new Exception("Configuration file path is not defined.");
                }
                if (data.LogFilePath != null)
                {
                    _LogFilePath = data.LogFilePath;
                }
                else
                {
                    _LogFilePath = "./AppLog.txt";
                }
                if (data.ErrorFilePath != null)
                {
                    _ErrorFilePath = data.ErrorFilePath;
                }
                else
                {
                    _ErrorFilePath = "./AppError.txt";
                }
                _XMLConfigFile = new XmlDocument();
                _XMLConfigFile.Load(_ConfigFilePath);

                if (data.RotateFiles)
                {
                    RotateFiles();
                }
                else 
                {
                    if (!System.IO.File.Exists(_LogFilePath))
                        System.IO.File.Create(_LogFilePath);
                    if (!System.IO.File.Exists(_ErrorFilePath))
                        System.IO.File.Create(_ErrorFilePath);
                }
            }
        }

        public void SetData(object parameters)
        {
            if (parameters.GetType() == typeof(CFilesData))
            {
                CFilesData data = (CFilesData)parameters;

                if (data.SetXMLVariables)
                {
                    WriteXML(data.WrittenVariables, data.WrittenValues,data.SaveXML);
                }
                if (data.ErrorWriteLine)
                {
                    WriteLine(_ErrorFilePath, "<" + DateTime.Now.ToString("dd/MM/YYYY HH:mm:ss") + ">" + data.Line);
                }
                if (data.LogWriteLine)
                {
                    WriteLine(_LogFilePath, "<" + DateTime.Now.ToString("dd/MM/YYYY HH:mm:ss") + ">" + data.Line);
                }
                if (data.WriteLine)
                {
                    WriteLine(data.FilePath, data.Line);
                }
            }
        }

        public event SpinPlatform.Dispatcher.ResultEventHandler NewResultEvent;
    }

}