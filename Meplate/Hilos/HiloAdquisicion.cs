using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpinPlatform.Dispatcher;
using SpinPlatform.Data;
using System.Diagnostics;
using SpinPlatform.Sensors.Meplaca;
using SpinPlatform.IO;
using System.Dynamic;

namespace Meplate
{
    class HiloAdquisicion : SpinThreadEvent
    {
        Meplate _Padre;
        CMeplaca _Meplaca;
        double avance, avanceAcumulado, velocidad, velocidadAnterior = 0;
        TimeSpan elapsedTime, totalElapsedTime;
        DateTime t1 ;
        DateTime t2;
        dynamic Parametros;


        List<CMedida> medidas;// lista donde se van guardando todos los perfiles de una chapa



        public HiloAdquisicion(Meplate padre, string name, dynamic parametros)
            : base(name)
        {
            Parametros = parametros;
            _Padre = padre;
        }

        public override void FunctionToExecuteByThread()
        {
            //Espero a que llegue un mensaje de empezar a medir en el while del Spinthreadevent
           
                //Compruebo que no pulsaron Stop mientras esperaba
                if (_StopEvent.WaitOne(0, true))
                {

                    return; // si se pulso stop se sale del while del Spinthreadevent
                }

                // Inicializo todo
                _Meplaca.GetData("Medidas"); //Lo usamos para limpiar la lista de medidas                
                avance = 0;
                avanceAcumulado = 0;
                t1 = DateTime.Now;
                totalElapsedTime = TimeSpan.Zero;
                dynamic aux = new ExpandoObject();

                // Mido de continuo hasta que hay señal de fin de chapa

                    do
                    {
                       
                        t2 = DateTime.Now;
                        elapsedTime = t2 - t1;
                        avance = avance + LeerAvance(elapsedTime);

                        totalElapsedTime = totalElapsedTime + elapsedTime;
                        t1 = t2;
                        // Si se avanzo lo suficiente para una nueva medida sigo
                        if (avance > _Meplaca.MinimoAvanceParaMedir)
                        {
                            // Leo los perfiles que haya en el meplaca (Pueden ser mas de un perfil) 
                            aux = _Meplaca.GetData("Medidas");
                            if (aux.Medidas.Count > 0)
                            {
                                double[] vectorAvance = CalcularAvance(avance, aux.Medidas.Count); //Descomponemos el avance en avances correspondientes a cada perfil.
                                for (int i = 0; i < aux.Medidas.Count; i++)
                                {

                                    //Añado los perfiles y la posicion en la lista
                                    medidas.Add(new CMedida(aux.Medidas[i], avanceAcumulado + vectorAvance[i]));

                                    if (medidas.Count % 10 == 0) 
                                    {
                                        ((SharedData<Informacion>)SharedMemory["Informacion"]).Set(0, new Informacion(medidas.Count, (double)10 / totalElapsedTime.TotalSeconds));

                                       _Padre.PrepareEvent(_Name);   /// ERROR al darle stop con el 
                                        totalElapsedTime = TimeSpan.Zero;
                                    }
                                }
                            }

                            avanceAcumulado = avanceAcumulado + avance;
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
                                aux.Offsets = (double[])((SharedData<double[]>)SharedMemory["Offset"]).Get(0);
                                _Meplaca.SetData(aux, "EnviarOffsets");
                            }

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
            _Meplaca.Init(Parametros);
            _Meplaca.Start();
            medidas = new List<CMedida>();

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
            return 1000 * velocidad * elapsedTime.TotalMinutes;
        }
        private double LeerVelocidad()
        {
            //***Leer velocidad de la tarjeta analogica.
            return 60; // m/min


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
