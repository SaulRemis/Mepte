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
    class ComunicacionTarjeta: SpinThreadSocket
    {
        dynamic _AuxLogCom, _AuxLog, _AuxLogError;
        Meplate _Padre;

        public bool _Midiendo = false;
        public ComunicacionTarjeta(SpinDispatcher padre, string name,dynamic parametros)
            : base(padre, name, (object)parametros.ComunicacionTarjeta)
        {
            _Padre = (Meplate)padre;
            _AuxLogCom = parametros.LogComunicacion;
            _AuxLog = parametros.LogMeplate;
            _AuxLogError = parametros.LogErrores;
        }

        public override void FunctionToExecuteByThread()
        {

            while (((SharedData<Byte[]>)SharedMemory["SocketReader"]).Elementos > 0)
            {
               
                Byte[] val = (Byte[])((SharedData<Byte[]>)SharedMemory["SocketReader"]).Pop(); 
                short messageid = BitConverter.ToInt16(val, 16);
               // Trace.WriteLine("New message arrived: MessageID->" + messageid);
                switch (messageid)
                {
                    case 21:
                        _AuxLogCom.LOGTXTMessage = "ADDA : Recibido Inicio Chapa con velocidad : " + (((Tarjeta)(((SharedData<Tarjeta>)_SharedMemory["Velocidad"]).Get(0))).Velocidad / 100.0).ToString() + " m/s";
                        _Padre.LogCom.SetData(ref _AuxLogCom, "Informacion"); 
                        if (_Midiendo == false)
                        {
                            Events["ComenzarMedida"].Set();
                            ((ComunicacionOP)((Meplate)_Padre)._DispatcherThreads["ComunicacionOP"]).SendMessage("21");
                            _AuxLogCom.LOGTXTMessage = "ADDA : Empieza a medir ";
                            _Padre.LogCom.SetData(ref _AuxLogCom, "Informacion"); 
                        }
                        _Midiendo = true;
                        break;
                    case 22:
                        ((ComunicacionOP)((Meplate)_Padre)._DispatcherThreads["ComunicacionOP"]).SendMessage("22");
                        break;
                    case 23:
                        _AuxLogCom.LOGTXTMessage = "ADDA : Recibido Fin Chapa con velocidad : " + (((Tarjeta)(((SharedData<Tarjeta>)_SharedMemory["Velocidad"]).Get(0))).Velocidad / 100.0).ToString() + " m/s";
                        _Padre.LogCom.SetData(ref _AuxLogCom, "Informacion"); 
                        if (_Midiendo == true)
                        {
                            Events["FinalizarMedida"].Set();
                            ((ComunicacionOP)((Meplate)_Padre)._DispatcherThreads["ComunicacionOP"]).SendMessage("23");
                            _AuxLogCom.LOGTXTMessage = "ADDA : Acaba de medir ";
                            _Padre.LogCom.SetData(ref _AuxLogCom, "Informacion"); 
                        }
                        _Midiendo = false;
                        break;
                    case 24:
                        if (_Midiendo == true)
                        {
                            Events["AbortarMedida"].Set();
                            ((ComunicacionOP)((Meplate)_Padre)._DispatcherThreads["ComunicacionOP"]).SendMessage("24");
                            _AuxLogCom.LOGTXTMessage = "ADDA : Recibido Abortar Chapa (Sale chapa con velocidad negativa) con velocidad : " + (((Tarjeta)(((SharedData<Tarjeta>)_SharedMemory["Velocidad"]).Get(0))).Velocidad / 100.0).ToString() + " m/s";
                            _Padre.LogCom.SetData(ref _AuxLogCom, "Informacion"); 
                        }
                        _Midiendo = false;
                        break;
                    case 26:

                        Tarjeta valor = new Tarjeta((double)BitConverter.ToInt16(val, 18), (double)BitConverter.ToInt16(val, 20));
                        ((SharedData<Tarjeta>)SharedMemory["Velocidad"]).Set(0, valor);
                        break;
                }
            }
        }
       
    }
}
