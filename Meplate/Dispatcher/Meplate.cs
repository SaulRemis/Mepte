using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpinPlatform.Dispatcher;
using SpinPlatform.Data;
using System.Threading;

namespace Meplate
{
    class Meplate: SpinDispatcher
    {
            //Clases auxiliares
           CArchivos arch ;

        Meplate()
        {
            arch = new CArchivos("MeplateIni.xml");

            _DispatcherThreads.Add("Adquisicion", new HiloAdquisicion("Adquisicion",arch));
            _DispatcherThreads.Add("Procesamiento", new HiloProcesamiento((Meplate)this, "Procesamiento",arch));


            ConnectMemory("Chapas", new SharedData<List<CMedida>>(20), "Adquisicion", "Procesamiento");
            ConnectMemory("Resultados", new SharedData<Resultados>(1), "Procesamiento");

            CreateEvent("ChapaMedida", new AutoResetEvent(false), "Adquisicion", "Procesamiento");
            CreateEvent("ComenzarMedida", new AutoResetEvent(false), "Adquisicion");
            CreateEvent("FinalizarMedida", new AutoResetEvent(false), "Adquisicion");

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
