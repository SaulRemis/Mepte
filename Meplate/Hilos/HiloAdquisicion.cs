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
        dynamic  _AuxMeplaca ;
        Tarjeta avancetemp;

        List<CMedida> medidas;// lista donde se van guardando todos los perfiles de una chapa
        

        public HiloAdquisicion(SpinDispatcher padre, string name, dynamic parametros)
            : base(name)
        {
            _Padre = (Meplate)padre;    
            _AuxMeplaca = parametros.Meplaca;
        }

        public override void FunctionToExecuteByThread()
        {
            //Espero a que llegue un mensaje de empezar a medir en el while del Spinthreadevent
           
                //Compruebo que no pulsaron Stop mientras esperaba
            if (_StopEvent.WaitOne(0, true)) return; // si se pulso stop se sale del while del Spinthreadevent
           
                // Inicializo todo
                _Meplaca.GetData(ref _AuxMeplaca, "Medidas"); //Lo usamos para limpiar la lista de medidas                
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
                        if (_AuxMeplaca.COMSocketDatosConnected) avance = avance + LeerAvance(elapsedTime); // en mm
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
                                        ((SharedData<Informacion>)SharedMemory["Informacion"]).Set(0, new Informacion(avanceAcumulado, (double)(10 / totalElapsedTime.TotalSeconds)));

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

                            if (!((SharedData<double[]>)_SharedMemory["Offset"]).Vacio)
                            {
                                _AuxMeplaca.MEPOffsets = (double[])((SharedData<double[]>)SharedMemory["Offset"]).Get(0);
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
            _Meplaca = new CMeplaca();
            _Meplaca.Init(_AuxMeplaca);
            _Meplaca.Start();
            medidas = new List<CMedida>();

            Tarjeta valor = new Tarjeta(0, 0);
            ((SharedData<Tarjeta>)SharedMemory["Velocidad"]).Set(0, valor);

        }
        public override void Closing()
        {
            _Meplaca.Stop();
            Trace.WriteLine("ADRI:   saliendo  del HILO ADQUISICION");
      

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
            if (velocidadAnterior != 0) velocidad = (velocidad + velocidadAnterior) / 2;
            velocidadAnterior = velocidad;
            return  velocidad * elapsedTime.TotalMinutes; //mm
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
