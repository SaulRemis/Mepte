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
      
        public ComunicacionOP(SpinDispatcher padre,string name,dynamic parametros)
            : base(padre, name, (object)parametros.ComunicacionOP)
        {
          
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
        public void SendMessageM5(string plateid, double[,] data)
        {
            dynamic temp = new ExpandoObject();
            string cadena = "M5" + plateid;
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
            temp.COMMessage = cadena;
            _server.SetData(ref temp, "EnviarMensaje");
        }
        public override void FunctionToExecuteByThread()
        {

            while (((SharedData<Byte[]>)SharedMemory["SocketReader"]).Elementos > 0)
            {

                Byte[] val = (Byte[])((SharedData<Byte[]>)SharedMemory["SocketReader"]).Pop();
                string mensaje = Encoding.ASCII.GetString(val);
                String messageid = mensaje.Substring(0, 2);
                Trace.WriteLine("New message arrived: MessageID->" + messageid);

                switch (messageid)
                {
                    //TODO
                    case "M9":
                        Events["IDChapa"].Set();

                        string _ID = mensaje.Substring(2, 16);
                        string _Width = mensaje.Substring(18, 4);
                        string _Length = mensaje.Substring(22, 5);
                        string _Thickness = mensaje.Substring(27, 5);
                        string _Tol1 = mensaje.Substring(32, 2);
                        string _Tol2 = mensaje.Substring(34, 2);


                        PlateID valor = new PlateID (_ID,double.Parse(_Width),double.Parse(_Length),double.Parse(_Thickness)/100,double.Parse(_Tol1),double.Parse(_Tol2));
                        ((SharedData<PlateID>)SharedMemory["IDChapa"]).Add(valor);
                        break;
                }
             
            }
        }
        public override void Closing()
        {
            Trace.WriteLine("ADRI:   saliendo  del HILO COMUNICACION OP");
        }
       
    }
}
