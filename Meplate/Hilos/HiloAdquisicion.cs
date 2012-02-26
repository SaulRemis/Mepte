using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpinPlatform.Dispatcher;
using SpinPlatform.Data;
using System.Diagnostics;

namespace Meplate
{
    class HiloAdquisicion : SpinThreadEvent
    {
        CMeplaca _Meplaca;
        double avance, avanceAcumulado, velocidad, velocidadAnterior = 0;
        TimeSpan elapsedTime;
        DateTime t1 ;
        DateTime t2;

        List<CMedida> medidas;// lista donde se van guardando todos los perfiles de una chapa


        public HiloAdquisicion(string name, CArchivos arch)
            : base(name)
        {
            _Meplaca = new CMeplaca(arch);
            _MillisecondsToSleep = 0;
        }

        public override void FunctionToExecuteByThread()
        {
            //Espero a que llegue un mensaje de empezar a medir
           
                //Compruebo que no pulsaron Stop mientras esperaba
                if (_StopEvent.WaitOne(0, true))
                {
                    _Meplaca.Cerrar();
                    return; // si se pulso stop se sale del while del Spinthreadevent
                }

                // Inicializo todo
                _Meplaca.LeerMedidas(); //Lo usamos para limpiar la lista de medidas                
                avance = 0;
                avanceAcumulado = 0;
                t1 = DateTime.Now;
                /*DEBUG
                totalElapsedTime = 0;
                 */

                // Mido de continuo hasta que hay señal de fin de chapa
                while (true)
                {

                    t2 = DateTime.Now;
                    elapsedTime = t2 - t1;
                    avance = avance + LeerAvance(elapsedTime);
                    /*DEBUG
                    totalElapsedTime = totalElapsedTime + elapsedTime.TotalSeconds;
                    */
                    t1 = t2;
                    // Si se avanzo lo suficiente para una nueva medida sigo
                    if (avance > _Meplaca.MinimoAvanceParaMedir)
                    {
                        // Leo los perfiles que haya en el meplaca (Pueden ser mas de un perfil) 
                        List<double[]> medidasAux = new List<double[]>(_Meplaca.LeerMedidas());
                        if (medidasAux.Count > 0)
                        {
                            double[] vectorAvance = CalcularAvance(avance, medidasAux.Count); //Descomponemos el avance en avances correspondientes a cada perfil.
                            for (int i = 0; i < medidasAux.Count; i++)
                            {

                                //Añado los perfiles y la posicion en la lista
                                medidas.Add(new CMedida(medidasAux[i], avanceAcumulado + vectorAvance[i]));
                                /*DEBUG
                                if ((medidas.Count % 10) == 0)
                                {
                                    numPerfiles = medidas.Count;
                                    rate = 10 / totalElapsedTime;
                                    totalElapsedTime = 0;
                                    mainForm.Invoke(delegateDisplay);
                                }*/
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

                        break;
                    }
                    if (_StopEvent.WaitOne(0, true))
                    {
                        _Meplaca.Cerrar();
                        break; // si se pulso stop se sale  

                    }

                }
            
            Trace.WriteLine("ADRI:   saliendo  del HILO ADQUISICION");
            
        }
        public override void Initializate()
        {
            Trace.WriteLine("ADRI:   Entrando en el HILO ADQUISICION");
            //INICIALIZAR
            _Meplaca.Inicializar();
            medidas = new List<CMedida>();
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
            return 20; // m/min


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
