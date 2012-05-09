using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using SpinPlatform.Dispatcher;
using SpinPlatform.Data;
using System.Dynamic;
using SpinPlatform.Comunicaciones;
using System.Diagnostics;

namespace Meplate
{
    class ComunicacionOP: SpinThreadSocket
    {
        dynamic _AuxLogCom, _AuxLog, _AuxLogError;
        Meplate _Padre;
        public ComunicacionOP(SpinDispatcher padre,string name,dynamic parametros)
            : base(padre, name, (object)parametros.ComunicacionOP)
        {
            _Padre = (Meplate)padre;
            _AuxLogCom = parametros.LogComunicacion;
            _AuxLog = parametros.LogMeplate;
            _AuxLogError = parametros.LogErrores;
        }

        public override void SendMessage(string mensajeAEnviar)
        {
            dynamic temp = new ExpandoObject();

            switch (mensajeAEnviar)
            {
                case "21":
                    temp.COMMessage = "M1";
                    _server.SetData(ref temp, "EnviarMensaje");
                    break;
                case "22":
                   temp.COMMessage = "M2";
                   _server.SetData(ref temp, "EnviarMensaje");
                    break;
                case "23":
                   temp.COMMessage = "M3";
                   _server.SetData(ref temp, "EnviarMensaje");
                    break;
                case "24":
                   temp.COMMessage = "M4";
                   _server.SetData(ref temp, "EnviarMensaje");
                    break;
            }
        }
        public void SendMessageM5(string plateid, double[,] data, string decision,double puntuacion)
        {
            dynamic temp = new ExpandoObject();
            //añado el id
            string cadena = "M5" + plateid;

            //añado la tabla de puntos
            foreach (double fila in data)
            {
                string val = Convert.ToString(Math.Round(fila));
                int tam = val.Length;
                switch (tam)
                {
                    case 1:
                        cadena += "0000";
                        break;
                    case 2:
                        cadena += "000";
                        break;
                    case 3:
                        cadena += "00";
                        break;
                    case 4:
                        cadena += "0";
                        break;
                    case 5:
                        break;
                }
                cadena += val;
            }
            //añado la decision
            cadena += decision;

            //añado la puntuacion
            string temp1 = Convert.ToString(Math.Round(puntuacion*10));
            int tam1 = temp1.Length;
            switch (tam1)
            {
                case 1:
                    cadena += "00";
                    break;
                case 2:
                    cadena += "0";
                    break;
                case 3:
                    break;
               
            }
            cadena += puntuacion;
            temp.COMMessage = cadena;
            _server.SetData(ref temp, "EnviarMensaje");

            _AuxLogCom.LOGTXTMessage = "OP : New Plate measured : " + plateid + " Decission : " + decision + " Score : " + puntuacion;
            _Padre.Log.SetData(ref _AuxLogCom, "Informacion");

        }
        public override void FunctionToExecuteByThread()
        {

            while (((SharedData<Byte[]>)SharedMemory["SocketReader"]).Elementos > 0)
            {

                Byte[] val = (Byte[])((SharedData<Byte[]>)SharedMemory["SocketReader"]).Pop();
                string mensaje = Encoding.ASCII.GetString(val);
                try
                {
                    String messageid = mensaje.Substring(0, 2);
                    switch (messageid)
                    {
                        //TODO
                        case "M9":
                            Events["IDChapa"].Set();

                            string _ID = mensaje.Substring(2, 16);
                            string _Width = mensaje.Substring(18, 4);
                            string _Length = mensaje.Substring(22, 5);
                            string _Thickness = mensaje.Substring(27, 5);
                            string _Tol1 = mensaje.Substring(32, 3);
                            string _Tol2 = mensaje.Substring(35, 3);


                            PlateID valor = new PlateID(_ID, double.Parse(_Width), double.Parse(_Length), double.Parse(_Thickness) / 100, double.Parse(_Tol1)/10, double.Parse(_Tol2)/10);
                            ((SharedData<PlateID>)SharedMemory["IDChapa"]).Add(valor);
                            _AuxLogCom.LOGTXTMessage = "OP : New ID received : " + _ID + " Width : " + _Width + " Length : " + _Length + " Thickness : " + _Thickness + " Tol1 : " + _Tol1 + " Tol2 : " + _Tol2;
                            _Padre.Log.SetData(ref _AuxLogCom, "Informacion");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    _AuxLogError.LOGTXTMessage ="Error receiving ID from Process computer : "+ ex.Message;
                    _Padre.Log.SetData(ref _AuxLogError, "Informacion");
                   
                }
             
            }
        }
        public override void Closing()
        {
            Trace.WriteLine("ADRI:   saliendo  del HILO COMUNICACION OP");
        }
       
    }
}
