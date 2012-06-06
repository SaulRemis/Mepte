using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;
using SpinPlatform.IO;

namespace SpinPlatform
{
    namespace Errors
    {
        class SpinExceptionExport
        {
            /// <summary>
            /// Class to export all errors to an external txt file
            /// </summary>


            const string TEXT_FILE = "Errors.txt";

            Mutex mMutex = new Mutex(); //synchronization


            internal void WriteErrorTextFile(Exception exWrite, bool withStackInfo)
            {
                try
                {
                    List<string> lMessages = GetMessageException(exWrite, withStackInfo);

                    WriteMessage(lMessages);
                }
                catch (Exception ex)
                {
                    throw new SpinException( SpinExceptionConstants.SPIN_ERROR_THREAD_WRITTING_ERRORFILE + TEXT_FILE, ex);
                }
            }



            internal void WriteErrorTextFile(string message)
            {
                try
                {
                    List<string> lMessages = new List<string>();
                    lMessages.Add(message);
                    WriteMessage(lMessages);
                }
                catch (Exception ex)
                {
                    throw new SpinException( SpinExceptionConstants.SPIN_ERROR_THREAD_WRITTING_ERRORFILE + TEXT_FILE, ex);
                }
            }


            private List<string> GetMessageException(Exception ex, bool withStackInfo)
            {
                List<string> lMessages = new List<string>();

                lMessages.Add(DateTime.Now.ToString());
                lMessages.Add(((SpinException)ex).ErrorMsg.Substring(0, ((SpinException)ex).ErrorMsg.IndexOf("\n")));
                lMessages.Add("Message: " + ex.Message);
                if (withStackInfo)
                    lMessages.Add("Stack: " + GetStackInfo(ex));
                lMessages.Add("");
                return lMessages;
            }



            private void WriteMessage(List<string> lMessages)
            {
                LockObject();
                SpinIOText.addMessageToFile(TEXT_FILE, true, lMessages);
                UnlockObject();
            }



            #region Información de la pila

            private string GetStackInfo(Exception ex)
            {
                string allstack = GetExceptionOriginal(ex);
                return allstack;
            }

            private string GetExceptionOriginal(Exception ex)
            {
                string s = "";
                if (ex.InnerException == null)
                    s = ex.StackTrace;
                else
                    s += GetExceptionOriginal(ex.InnerException) + "\n" + ex.StackTrace;

                return s;
            }

            #endregion


            #region Lock/Unlock mutex

            public void LockObject()
            {
                mMutex.WaitOne();
            }

            public void UnlockObject()
            {
                mMutex.ReleaseMutex();
            }

            #endregion
        }
    }
}
