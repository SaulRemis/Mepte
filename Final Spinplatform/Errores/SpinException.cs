using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Remoting.Messaging;
using SpinPlatform.Log;
using SpinPlatform.Config;
using SpinPlatform.Data;
using System.Dynamic;

namespace SpinPlatform
{
    namespace Errors
    {
        [Serializable()]
        public class SpinException : Exception
        {

            #region Atributos
            private string errorMsg = "";
            private string targetSite = "";
            #endregion

            #region Propiedades
            internal string ErrorMsg
            {
                get { return errorMsg; }
                set { errorMsg = value; }
            }
            internal string TargetSite
            {
                get { return targetSite; }
                set { targetSite = value; }
            }
            #endregion

            #region Constructores

            /// <summary>
            /// Class to throw and save error data
            /// </summary>
            /// <param name="msg">Text to show</param>
            /// <param name="ex">Exception</param>
            public SpinException(string msg)
                : base(msg, null)
            {
                this.ErrorMsg = msg;
            }

            public SpinException(string msg, Exception ex)
                : base(msg, ex)
            {
                this.ErrorMsg = msg;
            }


            internal SpinException(SerializationInfo info, StreamingContext context)
                : base(info, context) { }

         
            /// <summary>
            /// This class is used when you catch an exception but you don´t know the type and then cast to SpinException.
            /// </summary>
            /// <param name="msg">Text to show</param>
            /// <param name="ex">Exception</param>
            static public Exception GetException(string msg, Exception ex)
            {
                if (typeof(SpinException).Equals(ex.GetType()))
                    return ex;
                else
                    return new SpinException(msg, ex);
            }     

            #endregion

            #region Métodos privados

            /// <summary>
            /// Method who writes the txt errors file
            /// </summary>
            internal void SaveMsgErrors(Exception ex)
            {

                string clientIP;
                TargetSite = ex.TargetSite.ToString();
                try
                {
                    clientIP = CallContext.GetData("ClientIPAddress").ToString();
                }
                catch (Exception)
                {
                    clientIP = "localhost";
                }


                if (typeof(SpinException).Equals(ex.GetType()))
                {

                    ErrorMsg = "Client: " + clientIP + " \n" + "Error Message: " + ((SpinException)ex).ErrorMsg + "\n";

                }
               
                else
                    ErrorMsg = "Client: " + clientIP + "\n" + "Error Message: " + ex.Message +"\n";

                //Write to error file
                try
                {
                   
                    dynamic datos = new ExpandoObject();
                    
                    datos.GetAllMessages = true;
                    datos.GetLastMessage = false;
                    datos.LogElementLevel = 0;
                    datos.Message = new List<string>();



                }
                catch (Exception e)
                {
                    System.Console.WriteLine(e.Message);
                }



            }
 
            #endregion

        }
    }
}

