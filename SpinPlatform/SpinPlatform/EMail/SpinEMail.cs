using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using SpinPlatform.Dispatcher;
using SpinPlatform.Data;
using System.Dynamic;
using System.Security;

namespace SpinPlatform.EMail
{
    [Serializable()]
    public class SpinEMail : SpinDispatcher
    {
        /// <summary>
        /// Módulo para envío de EMAILS
        /// Éste módulo realiza el envío de emails de forma asíncrona. Esto quiere decir que em hilo que lanza la llamada siempre está disponible no quedando esta manera bloqueado.
        /// Así mismo tiene habilitados los siguientes métodos:
        /// Envío de email sencillo a un único destinatario
        /// Envío de email sencillo a una lista de destinatarios string[]
        /// Envío de email con un archivo adjunto a un único destinatario
        /// Envío de email con un archivo adjunto a una lista de destinatarios string[]
        /// También tiene un método para modificar los parámetros de configuración del módulo "UpdateParams"
        /// NOTA: Para su uso solo es necesario realizar un "Init" y los subsiguientes "SetData".
        /// No es necesario el uso de los métodos GetData, Start o Stop (Sin implementar)
        /// </summary>
        
        #region Variables
        private MailAddress _SMTPFrom;
        private String _SMTPUser;
        private String _SMTPHost;
        private Int32 _SMTPPort;
        private SecureString _SMTPPassword = new SecureString ();
        private Int32 _SMTPTimeOut;
        private Boolean _SMTPEnableSSL;
        #endregion

        #region Implementación de interface

        /// <summary>
        /// Inicializa el Modulo de Emails.
        /// Se le puede enviar en el init como parámetro tanto el objeto completo de configuración con el nombre genérico (SMTP) como el módulo SMTP directamente con cualquier nombre. 
        /// </summary>
        /// <param name="initData">
        /// Campos Obligatorios:  \n
        /// SMTPFrom (string) \n
        /// SMTPHost (string) \n
        /// SMTPPort (int) \n
        /// SMTPPassword (string) \n
        /// SMTPTimeOut (int) \n
        /// SMTPEnableSSL (bool) \n
        /// </param>
        public override void Init(dynamic initData)
        {
            if ((initData as IDictionary<string, object>).ContainsKey("SMTP"))
            {
                _SMTPFrom = new MailAddress(initData.SMTP.SMTPFrom);
                if ((initData.SMTP as IDictionary<string, object>).ContainsKey("SMTPUser"))
                    _SMTPUser = initData.SMTP.SMTPUser;
                else
                    _SMTPUser = initData.SMTP.SMTPFrom;
                _SMTPHost = initData.SMTP.SMTPHost;
                _SMTPPort = initData.SMTP.SMTPPort;
                _SMTPPassword = GetPassword(initData.SMTP.SMTPPassword);
                _SMTPTimeOut = initData.SMTP.SMTPTimeOut;
                _SMTPEnableSSL = initData.SMTP.SMTPEnableSSL;
            }
            else
            {
                _SMTPFrom = new MailAddress(initData.SMTPFrom);
                if ((initData as IDictionary<string, object>).ContainsKey("SMTPUser"))
                    _SMTPUser = initData.SMTPUser;
                else
                    _SMTPUser = initData.SMTPFrom;
                _SMTPHost = initData.SMTPHost;
                _SMTPPort = initData.SMTPPort;
                _SMTPPassword = GetPassword(initData.SMTPPassword);
                _SMTPTimeOut = initData.SMTPTimeOut;
                _SMTPEnableSSL = initData.SMTPEnableSSL;
            }
        }

        /// <summary>
        /// Envía datos por el socket \n
        /// </summary>
        /// <param name="obj">
        /// Variable dinamica de donde obtener los datos a establecer \n
        /// SMTPMessage: Objeto de tipo String (All) \n
        /// SMTPTo: Objeto de tipo String o string[] (All) \n
        /// SMTPSubject: Objeto de tipo String (All) \n
        /// SMTPAttachment:  Objeto de tipo String (SendEmailWithAttachment, SendEmailToListWithAttachment) \n
        /// SMTPFrom: Objeto de tipo String (UpdateParams) \n
        /// SMTPHost: Objeto de tipo String (UpdateParams) \n
        /// SMTPPort: Objeto de tipo int (UpdateParams) \n
        /// SMTPPassword: Objeto de tipo String (UpdateParams) \n
        /// SMTPTimeOut: Objeto de tipo int (UpdateParams) \n
        /// SMTPEnableSSL: Objeto de tipo bool (UpdateParams) \n
        /// </param>
        /// <param name="parameters">
        /// "SendEmail" - Envía email standard \n
        /// "SendEmailWithAttachment" - Envía email standard con un archivo adjunto \n
        /// "UpdateParams" - Cambia información deenvío de servidor \n
        /// </param>
        public override void SetData(ref dynamic obj, params string[] parameters)
        {

            try
            {
                foreach (string parameter in parameters)
                {
                    switch (parameter)
                    {
                        case "SendEmail":
                            Send(obj.SMTPTo,obj.SMTPMessage,obj.SMTPSubject);
                            break;
                        case "SendEmailToList":
                            SendToList(obj.SMTPTo, obj.SMTPMessage, obj.SMTPSubject);
                            break;
                        case "SendEmailWithAttachment":
                            SendWithAttachment(obj.SMTPTo, obj.SMTPMessage, obj.SMTPSubject,obj.SMTPAttachment);
                            break;
                        case "SendEmailToListWithAttachment":
                            SendToListWithAttachment(obj.SMTPTo, obj.SMTPMessage, obj.SMTPSubject, obj.SMTPAttachment);
                            break;
                        case "UpdateParams":
                            UpdateParams(obj.SMTPFrom, obj.SMTPHost, obj.SMTPPort, obj.SMTPPassword, obj.SMTPTimeOut, obj.SMTPEnableSSL);
                            break;
                        default:
                            obj.SMTPErrors = "Wrong Query";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                obj.SMTPErrors = ex.Message;
            }
        }


        /// <summary>
        /// Obtiene datos del Email
        /// </summary>
        /// <param name="Data">
        /// Variable dinamica donde guardar los resultados: \n
        /// </param>
        /// <param name="parameters">
        /// </param>
        public override void GetData(ref dynamic Data, params string[] parameters)
        {
            Data.SMTPReturnedData = parameters;
            try
            {
                foreach (string parameter in parameters)
                {
                    switch (parameter)
                    {
                        default:
                            Data.SMTPErrors = "Wrong Query";
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Data.SMTPErrors = ex.Message;
            }

        }

        /// <summary>
        /// Para el Módulo de  EMAIL
        /// No es necesario
        /// </summary>
        public void Start()
        {
        }

        /// <summary>
        /// Para el Módulo de  EMAIL
        /// No es necesario
        /// </summary>
        public override void Stop()
        {
        }

        #endregion


        #region métodos

        private void UpdateParams(string from, string host, int port,string password,int timeOut, bool enableSSL)
        {
            _SMTPFrom = new MailAddress(from);
            _SMTPUser = from;
            _SMTPHost = host;
            _SMTPPort = port;
            _SMTPPassword.Clear();
            _SMTPPassword = GetPassword(password);
            _SMTPTimeOut = timeOut;
            _SMTPEnableSSL = enableSSL;
        }

        private SecureString GetPassword(string password)
        {
            SecureString pass = new SecureString();
            for (int i = 0; i < password.Length; i++)
            {
                pass.AppendChar(password[i]);
            }
            return pass;
        }

        private void Send(string to,string mensaje,string titulo)
        {
                MailMessage email = new MailMessage();
                email.From = _SMTPFrom;

                email.Subject = titulo;
                email.SubjectEncoding = System.Text.Encoding.UTF8;
                email.Body = mensaje;
                email.BodyEncoding = System.Text.Encoding.UTF8;
                email.IsBodyHtml = false; 
                email.To.Add(new MailAddress(to));
                SmtpClient Smtp = new SmtpClient();
                Smtp.Host = _SMTPHost;
                Smtp.Timeout = _SMTPTimeOut;
                Smtp.EnableSsl = _SMTPEnableSSL;
                Smtp.UseDefaultCredentials = false;
                Smtp.Port = _SMTPPort;
                if (_SMTPPassword != null)
                    Smtp.Credentials = new System.Net.NetworkCredential(_SMTPUser, _SMTPPassword);
                Smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                
                try
                {
                    Smtp.SendAsync(email,email);
                    //Smtp.Send(email);
                }
                catch (System.Net.Mail.SmtpException ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.ReadLine();
                }
        }

        private void SendToList(string[] to, string mensaje, string titulo)
        {
            MailMessage email = new MailMessage();
            email.From = _SMTPFrom;

            email.Subject = titulo;
            email.SubjectEncoding = System.Text.Encoding.UTF8;
            email.Body = mensaje;
            email.BodyEncoding = System.Text.Encoding.UTF8;
            email.IsBodyHtml = false;
            foreach (string direccion in to)
            {
                email.To.Add(new MailAddress(direccion));
            }
            SmtpClient Smtp = new SmtpClient();
            Smtp.Host = _SMTPHost;
            Smtp.Timeout = _SMTPTimeOut;
            Smtp.EnableSsl = _SMTPEnableSSL;
            Smtp.UseDefaultCredentials = false;
            Smtp.Port = _SMTPPort;
            if (_SMTPPassword != null)
                Smtp.Credentials = new System.Net.NetworkCredential(_SMTPUser, _SMTPPassword);
            Smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

            try
            {
                Smtp.SendAsync(email, email);
                //Smtp.Send(email);
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }

        private void SendWithAttachment(string to, string mensaje, string titulo, string attachment)
        {
            MailMessage email = new MailMessage();
            email.From = _SMTPFrom;

            // Create  the file attachment for this e-mail message.
            Attachment data = new Attachment(attachment, System.Net.Mime.MediaTypeNames.Application.Octet);
            // Add the file attachment to this e-mail message.
            email.Attachments.Add(data);

            email.Subject = titulo;
            email.SubjectEncoding = System.Text.Encoding.UTF8;
            email.Body = mensaje;
            email.BodyEncoding = System.Text.Encoding.UTF8;
            email.IsBodyHtml = false;
            email.To.Add(new MailAddress(to));
            SmtpClient Smtp = new SmtpClient();
            Smtp.Host = _SMTPHost;
            Smtp.Timeout = _SMTPTimeOut;
            Smtp.EnableSsl = _SMTPEnableSSL;
            Smtp.UseDefaultCredentials = false;
            Smtp.Port = _SMTPPort;
            if (_SMTPPassword != null)
                Smtp.Credentials = new System.Net.NetworkCredential(_SMTPUser, _SMTPPassword);
            Smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

            try
            {
                Smtp.SendAsync(email, email);
                //Smtp.Send(email);
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }

        private void SendToListWithAttachment(string[] to, string mensaje, string titulo, string attachment)
        {
            MailMessage email = new MailMessage();
            email.From = _SMTPFrom;

            // Create  the file attachment for this e-mail message.
            Attachment data = new Attachment(attachment);
            // Add the file attachment to this e-mail message.
            email.Attachments.Add(data);

            email.Subject = titulo;
            email.SubjectEncoding = System.Text.Encoding.UTF8;
            email.Body = mensaje;
            email.BodyEncoding = System.Text.Encoding.UTF8;
            email.IsBodyHtml = false;
            foreach (string direccion in to)
            {
                email.To.Add(new MailAddress(direccion));
            }
            SmtpClient Smtp = new SmtpClient();
            Smtp.Host = _SMTPHost;
            Smtp.Timeout = _SMTPTimeOut;
            Smtp.EnableSsl = _SMTPEnableSSL;
            Smtp.UseDefaultCredentials = false;
            Smtp.Port = _SMTPPort;
            if (_SMTPPassword != null)
                Smtp.Credentials = new System.Net.NetworkCredential(_SMTPUser, _SMTPPassword);
            Smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

            try
            {
                Smtp.SendAsync(email, email);
                //Smtp.Send(email);
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }

        #endregion

    }
}
