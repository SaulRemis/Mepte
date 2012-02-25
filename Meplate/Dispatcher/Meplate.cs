﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpinPlatform.Dispatcher;
using SpinPlatform.Data;

namespace Meplate
{
    class Meplate: SpinDispatcher
    {
        Meplate()
        {
            _DispatcherThreads.Add("Adquisicion", new HiloAdquisicion("Adquisicion"));
            _DispatcherThreads.Add("Procesamiento", new HiloProcesamiento(this, "Procesamiento"));


            ConnectMemory("Perfiles", new SharedData<CMedida>(20), "Adquisicion", "Procesamiento");
            ConnectMemory("Resultados", new SharedData<Resultados>(1), "Procesamiento");

          //  CreateEvent("NuevaMedida", new AutoResetEvent(false), "Consumidor", "Productor");

         }

        public override object GetData(object parameters)
        {
           /* if (parameters.GetType()==typeof(DispatcherData))
            {
                DispatcherData data = (DispatcherData)parameters;

                data.ResetData();
                if (data.GetFecha)
                {
                    ResultsDataDate temp = (ResultsDataDate)((SharedData<ResultsDataDate>)_DispatcherSharedMemory["Resultados"]).Get(0);
                    data.Hora = temp.Hora;
                    data.Minutos = temp.Minutos;
                    data.Segundos = temp.Segundos;
                    data.Milisegundos = temp.Milisegundos;

                }
                return data;
            }
            else */       return null;
        }
        public void PrepareEvent(string thread )
        {
          
          /*  DispatcherData temp = new DispatcherData();

            switch (thread)
            {
                case "Consumidor":
                    temp.GetFecha = true;
                    break;
                default:
                    break;
            }

            object data = GetData(temp);
            DataEventArgs args = new DataEventArgs(data);
            if (Status == SpinDispatcherStatus.Running)  // Por si nadie escucha el evento o esta en proceso de parar
            {
                SetEvent(args);
            }*/
             
        }

    }
}
