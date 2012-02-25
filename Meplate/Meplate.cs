using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpinPlatform.Dispatcher;

namespace Meplate
{
    class Meplate: SpinDispatcher
    {
        Meplate()
        {
            _DispatcherThreads.Add("Productor", new HiloProductor("Productor"));
            _DispatcherThreads.Add("Consumidor", new HiloConsumidor(this, "Consumidor"));

            ConnectMemory("Fecha", new SharedData<DateTime>(10), "Consumidor", "Productor");
            ConnectMemory("Resultados", new SharedData<ResultsDataDate>(1), "Consumidor");

            CreateEvent("NuevaMedida", new AutoResetEvent(false), "Consumidor", "Productor");

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
