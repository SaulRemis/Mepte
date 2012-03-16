using System;
using System.Collections.Generic;
using System.Text;

namespace SpinPlatform
{
    namespace Errors
    {
        public static class SpinExceptionConstants
        {
            /// <summary>
            /// Class of constants to define generic and specific errors. Very useful to find references and change log messages
            /// </summary>
            #region Errores de Hilos

            internal const string SPIN_ERROR_THREAD_CREATING = "Error creating a new thread";
            internal const string SPIN_ERROR_THREAD_STARTING_WITHPARAM = "Error starting a thread with parameters";
            internal const string SPIN_ERROR_THREAD_STARTING = "Error starting a thread";
            internal const string SPIN_ERROR_THREAD_INITIALIZING = "Error initializing a thread";
            internal const string SPIN_ERROR_THREAD_ABORTING = "Error aborting a thread";
            internal const string SPIN_ERROR_THREAD_JOINING = "Error joining a thread";
            internal const string SPIN_ERROR_POOLTHREAD_STARTING = "Errorinicializing Pool of threads";
            internal const string SPIN_ERROR_THREAD_WRITTING_ERRORFILE = "Error writing message error in text file ";

            #endregion

            #region Errores con ficheros txt
            internal const string SPIN_ERROR__FILE_FORMAT_INCORRECT = "Format of file incorrect.";
            internal const string SPIN_ERROR__FILE_NO_POSIBLE_SAVE = "Could not save the file.";
            internal const string SPIN_ERROR__FILE_NO_FOUND = "The file was not found.";
            #endregion

            #region Errores con ficheros XML
            internal const string SPIN_ERROR_XML_UNSERIALIZING = "Error unserializing file: ";

            #endregion
        }
    }
}

