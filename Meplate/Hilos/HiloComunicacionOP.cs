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
        dynamic data = new ExpandoObject();

        public ComunicacionOP(string name)
            : base(name)
        {
        }

        public ComunicacionOP(dynamic padre,string name,dynamic parametros)
            : base(name)
        {
            data.COMThread = this;
            data.COMThreadName = "ComunicacionOP";
            data.COMSocketType = parametros.Comunicaciones.socketType;
            data.COMPort = parametros.Comunicaciones.port;
            data.COMIP = parametros.Comunicaciones.ip;
            data.COMBufferSize = parametros.Comunicaciones.buffersize;

            _Padre = padre;
            _server = new SpinCOM();
            _server.Init(data);
        }

        public override void SendMessage(string mensajeAEnviar)
        {
            switch (mensajeAEnviar)
            {
                case "21":
                    _server.SetData("M1");
                    break;
                case "22":
                    _server.SetData("M2");
                    break;
                case "23":
                    _server.SetData("M3");
                    break;
                case "24":
                    _server.SetData("M4");
                    break;
            }
        }

        public void SendMessageM5(string plateid, double[,] data)
        {
            string cadena = "M5" + plateid;
            foreach (Double fila in data)
            {
                string val = Convert.ToString(Math.Truncate(fila));
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
            _server.SetData(cadena);
        }

        public override void FunctionToExecuteByThread()
        {

            while (((SharedData<Byte[]>)SharedMemory["SocketReader"]).Elementos > 0)
            {

                Byte[] mensaje = (Byte[])((SharedData<Byte[]>)SharedMemory["SocketReader"]).Pop();
                String messageid = Encoding.ASCII.GetString(new Byte[] {mensaje[0] , mensaje[1]});
                Trace.WriteLine("New message arrived: MessageID->" + messageid);

                switch (messageid)
                {
                    //TODO
                    case "M9":
                        Events["IDChapa"].Set();

                        string _ID = BitConverter.ToString(mensaje, 2, 16);
                        string _Width = BitConverter.ToString(mensaje, 18, 5);
                        string _Length = BitConverter.ToString(mensaje, 23, 5);


                        PlateID valor = new PlateID(_ID,double.Parse(_Width),double.Parse(_Length));
                        ((SharedData<PlateID>)SharedMemory["IDChapa"]).Set(0, valor);
                        break;
                }
             
            }
        }
    }
}
