using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SpinPlatform.Dispatcher;
using SpinPlatform.Data;
using System.Threading;
using System.Dynamic;
using System.Diagnostics;
using SpinPlatform.Config;


namespace CardSaul
{
    public class CardSaul: SpinDispatcher
    {

        #region Funciones Publicas
        public double _valorSlider = 4;
        public CardSaul()
        {

        }

        public void Init(ref dynamic parameters)
        {
            SpinConfig con = new SpinConfig();
            parameters.CONFFile = SpinConfigConstants.SPIN_CONFIG_XML_NAME;
            con.GetData(parameters, "Parametros");

            _DispatcherThreads.Add("Productor", new HiloProductor(this, "Productor"));
            _DispatcherThreads.Add("Consumidor", new HiloConsumidor(this, "Consumidor"));
            _DispatcherThreads.Add("HiloServidor", new HiloServidor(this, parameters, "HiloServidor"));

            ConnectMemory("ConsProd", new SharedData<String>(10), "Consumidor", "Productor");
            ConnectMemory("Resultados", new SharedData<String>(1), "Consumidor");

            CreateEvent("NuevaMedida", new AutoResetEvent(false), "Consumidor", "Productor");
        }

        public override void SetData(ref dynamic Data, params string[] parameters)
        {
            _valorSlider = Data.VALORSlider;
        }




        public override void GetData(ref dynamic Data, params string[] parameters)
        //public override object GetData(dynamic parameters)
        {
            Data.MEPReturnedData = parameters;
            try
            {
                foreach (string parameter in parameters)
                {
                    switch (parameter)
                    {
                        case "HILOProductor":
                            Data.HILOProductorValue = _valorSlider;
                            break;

                        case "COMGetSocketLine":
                            Data.COMMessage = ((SharedData<String>)_DispatcherSharedMemory["Resultados"]).Get(0);
                            break;


                        default:
                            Data.MEPErrors = "Wrong Query";
                            break;
                    }
                }
                Data.MEPErrors = "";
            }
            catch (Exception ex)
            {

                Data.MEPErrors = ex.Message;
                //Ademas se lanzaria la excepcion oportuna
            }
           
        }
        public void PrepareEvent(string thread )
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
