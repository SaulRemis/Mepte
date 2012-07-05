using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpinPlatform.Dispatcher;
using SpinPlatform.Data;
using System.Diagnostics;
using SpinPlatform.Sensors.Meplaca;

using System.Dynamic;

namespace Meplate
{
    class HiloAdquisicion : SpinThreadEvent
    {
        Meplate _Padre;
        public  CMeplaca _Meplaca;
        double avance, avanceAcumulado, velocidad, velocidadAnterior = 0;
        TimeSpan elapsedTime, totalElapsedTime;
        DateTime t1,t2 ;
        dynamic _AuxMeplaca,_AuxLogCom, _AuxLog, _AuxLogError,_Parameters;
        public  bool _Midiendo = false;
        bool _CalculaOffset = true;
        List<CMedida> medidas;// lista donde se van guardando todos los perfiles de una chapa
        

        public HiloAdquisicion(SpinDispatcher padre, string name, dynamic parametros)
            : base(name)
        {
            _Padre = (Meplate)padre;
            _Parameters = parametros;
            _AuxMeplaca = parametros.Meplaca;
            _AuxLogCom = parametros.LogComunicacion;
            _AuxLog = parametros.LogMeplate;
            _AuxLogError = parametros.LogErrores;
            _Meplaca = new CMeplaca();
            _Meplaca.NewResultEvent += new ResultEventHandler(_Meplaca_NewResultEvent);
        }

        void _Meplaca_NewResultEvent(object sender, DataEventArgs res)
        {
            double vel = LeerVelocidad();
            dynamic temp = (dynamic)res.DataArgs;
            string msg = temp.MEPMessage;
            switch (msg)
            {
                case "InicioChapa":

                    if (vel > 0)
                    {
                        _AuxLog.LOGTXTMessage = "MEPLACA : Recibido Inicio Chapa con velocidad : " + vel.ToString() + " m/s y tension media = " + temp.MEPVoltage;
                        _Padre.Log.SetData(ref _AuxLog, "Informacion");

                        if (_Midiendo == false)
                        {
                            Events["ComenzarMedida"].Set();
                            ((ComunicacionOP)((Meplate)_Padre)._DispatcherThreads["ComunicacionOP"]).SendMessage("21");
                            _Midiendo = true;
                        }


                        break;

                    }

                    break;
                case "FinChapa":

                    if (vel > 0)
                    {
                        if (_Midiendo == true)
                        {
                            _AuxLog.LOGTXTMessage = "MEPLACA : Recibido Fin Chapa con velocidad : " + vel.ToString() + " m/s y tension media = " + temp.MEPVoltage;
                            _Padre.Log.SetData(ref _AuxLog, "Informacion");

                            Events["FinalizarMedida"].Set();
                            ((ComunicacionOP)((Meplate)_Padre)._DispatcherThreads["ComunicacionOP"]).SendMessage("23");
                            _Midiendo = false;
                        }
                        break;

                    }
                    else
                    {
                        if (_Midiendo == true)
                        {

                            Events["AbortarMedida"].Set();
                            //((ComunicacionOP)((Meplate)_Padre)._DispatcherThreads["ComunicacionOP"]).SendMessage("24");
                            _AuxLog.LOGTXTMessage = "MEPLACA : Recibido Abortar Chapa (Sale chapa con velocidad negativa) con velocidad : " + vel.ToString() + " m/s y tension media = " + temp.MEPVoltage;
                            _Padre.Log.SetData(ref _AuxLog, "Informacion");
                            _Midiendo = false;
                        }
                    }

                    break;
                default:
                    break;
            }
           
         
        }

        public override void FunctionToExecuteByThread()
        {
            //Espero a que llegue un mensaje de empezar a medir en el while del Spinthreadevent
           
                //Compruebo que no pulsaron Stop mientras esperaba
            if (_StopEvent.WaitOne(0, true)) return; // si se pulso stop se sale del while del Spinthreadevent
           
                // Inicializo todo
               _Meplaca.SetData(ref _AuxMeplaca, "VaciarBuffer"); //Lo usamos para limpiar la lista de medidas                
                avance = 0;
                avanceAcumulado = 0;
 
                t1 = DateTime.Now;
                totalElapsedTime = TimeSpan.Zero;

                // Mido de continuo hasta que hay señal de fin de chapa
                    do
                    {
                        t2 = DateTime.Now;
                        elapsedTime = t2 - t1;
                        ((ComunicacionTarjeta)_Padre._DispatcherThreads["ComunicacionTarjeta"])._server.GetData(ref _AuxMeplaca,"EstadoSocket");

                        if (_AuxMeplaca.COMSocketDatosConnected)  avance = avance + LeerAvance(elapsedTime); // en mm                       
                        else avance = avance + 40;
                        totalElapsedTime = totalElapsedTime + elapsedTime;
                        t1 = t2;
                        // Si se avanzo lo suficiente para una nueva medida sigo
                        if (avance > _Meplaca.MinimoAvanceParaMedir)
                        {
                            // Leo los perfiles que haya en el meplaca (Pueden ser mas de un perfil) 
                            _Meplaca.GetData(ref _AuxMeplaca, "Medidas");
                             avanceAcumulado = avanceAcumulado + avance;
                            if (_AuxMeplaca.MEPMedidas.Count > 0)
                            {
                                double[] vectorAvance = CalcularAvance(avance, _AuxMeplaca.MEPMedidas.Count); //Descomponemos el avance en avances correspondientes a cada perfil.
                                for (int i = 0; i < _AuxMeplaca.MEPMedidas.Count; i++)
                                {
                                    //Añado los perfiles y la posicion en la lista
                                    medidas.Add(new CMedida(_AuxMeplaca.MEPMedidas[i], avanceAcumulado + vectorAvance[i]));

                                    if (medidas.Count % 10 == 0) 
                                    {
                                        ((SharedData<Informacion>)SharedMemory["Informacion"]).Set(0, new Informacion(avanceAcumulado, (double)(10 / (totalElapsedTime.TotalMilliseconds/1000))));

                                       _Padre.PrepareEvent(_Name);   
                                        totalElapsedTime = TimeSpan.Zero;
                                    }
                                }
                            }
                           
                            avance = 0;
                        }
                        // Si llego la señal de find e chapa guardo la medida de toda la chapa en el memoria
                        // compratida y envio la señal de nueva medida ("ChapaMedida")
                        if (_Events["FinalizarMedida"].WaitOne(0, true))
                        {
                            ((SharedData<List<CMedida>>)_SharedMemory["Chapas"]).Add(new List<CMedida>(medidas)); // envio las medidas de la chapa medida a la memoria compartida
                            medidas.Clear();
                            _Events["ChapaMedida"].Set();

                            //envio los offsets

                            if (!((SharedData<Offset>)_SharedMemory["Offset"]).Vacio)
                            {
                                Offset off_temp = (Offset)((SharedData<Offset>)SharedMemory["Offset"]).Get(0);
                                _AuxMeplaca.MEPValores = (double[])off_temp.Valores;
                                _AuxMeplaca.MEPReferencias = (double[])off_temp.Referencias;
                                _Meplaca.SetData(ref _AuxMeplaca, "EnviarOffsets");
                            }
                            break;
                        }
                        // Si llega la señal de aborat medida borro todas las medidas
                        if (_Events["AbortarMedida"].WaitOne(0, true))
                        {                            
                            medidas.Clear();
                            avanceAcumulado = 0;
                            ((SharedData<Informacion>)SharedMemory["Informacion"]).Set(0, new Informacion(0.0, 0.0));
                            _Padre.PrepareEvent(_Name);  

                            break;
                        }
                    } while (!_StopEvent.WaitOne(0, true));              // Mido de continuo hasta que hay señal de fin de chapa o Stop

                  Trace.WriteLine("ADRI:   Chapa Medida");
            
        }
        public override void Initializate()
        {

            _WakeUpThreadEvent = _Events["ComenzarMedida"];
            Trace.WriteLine("ADRI:   Entrando en el HILO ADQUISICION");
            //INICIALIZAR

            _Meplaca.Init(_AuxMeplaca);
            _Meplaca.Start();
            medidas = new List<CMedida>();

            //Tarjeta valor = new Tarjeta(0, 0);
            //((SharedData<Tarjeta>)SharedMemory["Velocidad"]).Set(0, valor);

        }
        public override void Closing()
        {
            _Meplaca.Stop();

            //Cierro los logs 
            Trace.WriteLine("ADRI:   saliendo  del HILO ADQUISICION");

            _AuxLog.LOGTXTMessage = "Meplate is Closing";
            _Padre.Log.SetData(ref _AuxLog, "Informacion");
          

            _Padre.Log.Stop();
          

        }
        public override bool Stop()
         {

             _Events["ComenzarMedida"].Set();
             _StopEvent.Set();
             _WakeUpThreadEvent.Set();
             return true;
    
         }         

       
        //Funciones calculo avance QUITAR DE AQUI
        private double LeerAvance(TimeSpan elapsedTime)//Calcula el avance medido en mm
        {

            velocidad = LeerVelocidad();

            // si la cahapa no avanzo deshecho las medidas acumuladas
            if (velocidad == 0 & velocidadAnterior == 0)
            {
                if (_CalculaOffset)
                {
                    CalcularOffset();
                    _CalculaOffset = false;
                }

                _Meplaca.SetData(ref _AuxMeplaca, "VaciarBuffer");
            }
            else _CalculaOffset = true;

            if (velocidadAnterior != 0) velocidad = (velocidad + velocidadAnterior) / 2;
            velocidadAnterior = velocidad;
            return  velocidad * elapsedTime.TotalMinutes; //mm
        }

        private void CalcularOffset()
        {
            ((ComunicacionTarjeta)_Padre._DispatcherThreads["ComunicacionTarjeta"])._server.GetData(ref _AuxMeplaca, "EstadoSocket");
            int _NumeroModulos = int.Parse(_AuxMeplaca.MEPNumeroModulos);
            double distancia_a_la_chapa = double.Parse(_AuxMeplaca.MEPDistancia_nominal_trabajo);
            double distancia_entre_sensores = double.Parse(_AuxMeplaca.MEPDistancia_entre_sensores);
            double[] Referencias = new double[_NumeroModulos * 6];
            double[] medidas = new double[_NumeroModulos * 6];

            if (!((SharedData<PlateID>)SharedMemory["IDChapa"]).Vacio)
            {
                PlateID id = ((PlateID)((SharedData<PlateID>)SharedMemory["IDChapa"]).Get(0));
              

               _Meplaca.GetData(ref _AuxMeplaca, "UltimaMedida");
               medidas = (double[])_AuxMeplaca.MEPUltimoPerfil;

               double valor = (distancia_a_la_chapa - id.Thickness  )/10;
               double limder = (int)Math.Round(id.Width / distancia_entre_sensores);
               double limizq =  1;



               for (int i = 0; i < Referencias.Length; i++)
               {
                   if ((i >= limizq) && (i <= limder))
                   {
                       Referencias[i] = valor;
                   }
                   else
                   {
                       medidas[i] = 0;
                       Referencias[i] = 0;
                   }
               }
                ((SharedData<Offset>)SharedMemory["Offset"]).Set(0, new Offset(medidas, Referencias));
           }
            //guardo los offset para enviarlos luego
          
        }
        private double LeerVelocidad()
        {
            Tarjeta tarjetatemp = (Tarjeta)((SharedData<Tarjeta>)SharedMemory["Velocidad"]).Get(0);

            if (tarjetatemp != null) return tarjetatemp.Velocidad*10; // mm/min
            else return 60;


        }//Debe leer la velocidad de la tarjeta de adquisicion o por mensaje
        private double[] CalcularAvance(double avance, int numPerfiles)
        {
            double[] vector = new double[numPerfiles];
            double div = avance / (double)numPerfiles;
            vector[0] = div;
            for (int i = 1; i < numPerfiles; i++)
            {
                vector[i] = vector[i - 1] + div;
            }
            return vector;

        }

    }
}
