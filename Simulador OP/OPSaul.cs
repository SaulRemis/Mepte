using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using SpinPlatform.Dispatcher;
using SpinPlatform.Config;
using SpinPlatform.Data;
using SpinPlatform.Errors;
using SpinPlatform.Comunicaciones;
using System.Dynamic;
using SpinPlatform;
using System.Windows.Forms;

namespace OPSaul
{
    class OPSaul: SpinDispatcher
    {
        public dynamic data = new ExpandoObject();
        public Form _formulario;
        #region Funciones Publicas
        public OPSaul(Form1 formulario)
        {
            _formulario = formulario;
        }

        public void Init(ref dynamic parameters)
        {
            SpinConfig con = new SpinConfig();
            parameters = con.GetData(SpinConfigConstants.SPIN_CONFIG_XML_NAME);

            _DispatcherThreads.Add("Consumidor", new HiloConsumidor(this, "Consumidor"));
            _DispatcherThreads.Add("ComunicacionMeplaca", new ComunicacionMeplaca(this, parameters, "ComunicacionMeplaca"));

            ConnectMemory("ResultadosOP", new SharedData<Message>(10), "Consumidor", "ComunicacionMeplaca");
            ConnectMemory("ResultadosUI", new SharedData<Message>(10), "Consumidor");

            CreateEvent("Resultados", new AutoResetEvent(false), "Consumidor", "ComunicacionMeplaca");
            CreateEvent("ResultadosUI", new AutoResetEvent(false), "Consumidor");
        }

        public override void SetData(dynamic obj)
        {
            data.FORMPlate = obj.FORMPlate;
            data.FORMWidth = obj.FORMWidth;
            data.FORMLength = obj.FORMLength;
            ((ComunicacionMeplaca)_DispatcherThreads["ComunicacionMeplaca"]).SendMessage("M9" + data.FORMPlate);
        }

        public override object GetData(dynamic parameters)
        {
            if ((parameters as IDictionary<string, object>).ContainsKey("COMGetSocketLine"))
            {
                if (parameters.COMGetSocketLine)
                {
                    parameters.COMMessage = ((Message)((SharedData<Message>)_DispatcherSharedMemory["ResultadosUI"]).Get(0));
                }
            }
            if ((parameters as IDictionary<string, object>).ContainsKey("FORMGetData"))
            {
                if (parameters.FORMGetData)
                {
                    parameters.Data = ((Form1)_formulario).getDatos();
                }
            }
            return parameters;

        }


        public void PrepareEvent(string thread)
        {
            dynamic data = new ExpandoObject();
            switch (thread)
            {
                case "Consumidor":
                    data.COMGetSocketLine = true;
                    break;
                default:
                    break;
            }

            data = GetData(data);
            DataEventArgs args = new DataEventArgs(data);
            if (Status == SpinDispatcherStatus.Running)  // Por si nadie escucha el evento o esta en proceso de parar
            {
                SetEvent(args);
            }

        }

        #endregion
    }
}
