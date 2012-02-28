using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpinPlatform.Data;

namespace Meplate
{
    class CFilesData: ModuleData
    {
        /// <summary>
        /// Entry parameters
        /// </summary>
        public bool GetVariable = false;
        public bool SetXMLVariables = false;        
        public bool LogWriteLine = false;
        public bool ErrorWriteLine = false;
        public bool WriteLine = false;
        /// <summary>
        /// Entry variables
        /// </summary>
        string _ConfigFilePath;
        string _LogFilePath;
        string _ErrorFilePath;
        string _FilePath;
        string _Line;
        bool _SaveXML = false;
        bool _RotateFiles = false;
        string _ReadVariable;
        string[] _WrittenVariables;
        string[] _WrittenValues;
        /// <summary>
        /// Exit variables
        /// </summary>
        string _ReadValue;
        /// <summary>
        /// Properties
        /// </summary>
        public string ReadVariable
        {
            get { return _ReadVariable; }
            set { _ReadVariable = value; }
        }
        public string[] WrittenVariables
        {
            get { return _WrittenVariables; }
            set { _WrittenVariables = value; }
        }
        public string ReadValue
        {
            get { return _ReadValue; }
            set { _ReadValue = value; }
        }
        public string[] WrittenValues
        {
            get { return _WrittenValues; }
            set { _WrittenValues = value; }
        }
        public string ConfigFilePath
        {
            get { return _ConfigFilePath; }
            set { _ConfigFilePath = value; }
        }     
        public string LogFilePath
        {
            get { return _LogFilePath; }
            set { _LogFilePath = value; }
        }      
        public string ErrorFilePath
        {
            get { return _ErrorFilePath; }
            set { _ErrorFilePath = value; }
        }
        public bool RotateFiles
        {
            get { return _RotateFiles; }
            set { _RotateFiles = value; }
        }
        public string FilePath
        {
            get { return _FilePath; }
            set { _FilePath = value; }
        }
        public string Line
        {
            get { return _Line; }
            set { _Line = value; }
        }
        public bool SaveXML
        {
            get { return _SaveXML; }
            set { _SaveXML = value; }
        }
        /// <summary>
        /// Reset the entry and exit variables
        /// </summary>
        public void ResetData()
        {
            _ConfigFilePath = null;
            _LogFilePath = null;
            _ErrorFilePath = null;
            _FilePath = null;
            _Line = null;
            _SaveXML = false;
            _RotateFiles = false;
            _ReadVariable = null;
            _WrittenVariables = null;
            _WrittenValues = null;

            _ReadValue = null;
        }
    }
}
