using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Dynamic;

namespace SpinPlatform
{
    namespace Dispatcher
    {
        public class SpinThreadPeriodic : SpinThreadWhile
        {
             public object _Padre;
             DateTime _Init;
             String _Periocity;
             int _Quantity;
             int _MillisecondsToStart;
             int _PeriodicMilisecond;
             TimeSpan temp;

            dynamic data = new ExpandoObject();


            public SpinThreadPeriodic(SpinDispatcher padre, string name) : base(name)
            {
                _Padre = padre;

            }

            public SpinThreadPeriodic(SpinDispatcher padre, string name, string init,string periocity, string quantity)
                : base(name)
            {
                _Padre = padre;
                _Priority = ThreadPriority.BelowNormal;
                _Init = DateTime.Parse(init);
                _Periocity = periocity;
                _Quantity = int.Parse(quantity);
                switch (_Periocity)
                {
                    case "Minute":
                        _MillisecondsToStart = 1;
                        _PeriodicMilisecond = 60 * 1000 * _Quantity;
                        break;
                    case "Hour":
                        if (_Init.Minute == 0)
                        {
                            _MillisecondsToStart = (60 - DateTime.Now.Minute) * 60 * 1000;
                            _PeriodicMilisecond = 60 * 60 * 1000 * _Quantity;
                            break;
                        }
                        else 
                        {
                            if (DateTime.Now.Minute >_Init.Minute)
                            {
                                _MillisecondsToStart = (60 - (DateTime.Now.Minute - _Init.Minute))*60*1000;
                                _PeriodicMilisecond = 60 * 60 * 1000 * _Quantity;
                                break;
                            }
                            else
                            {
                                _MillisecondsToStart = (_Init.Minute - DateTime.Now.Minute)*60*1000;
                                _PeriodicMilisecond = 60 * 60 * 1000*_Quantity;
                                break;
                            }
                        
                        
                        }
                       
                    case "Day":

                         _MillisecondsToStart = 1;
                        _PeriodicMilisecond = 60 * 1000;
                        break;
                    case "Week":
                         _MillisecondsToStart = 1;
                        _PeriodicMilisecond = 60 * 1000;
                        break;
                    case "Month":
                         _MillisecondsToStart = 1;
                        _PeriodicMilisecond = 60 * 1000;
                        break;
                    default:
                        break;
                }

                _MillisecondsToSleep = _PeriodicMilisecond;
             
            }


            public override void Initializate()
            {

                Thread.Sleep(_MillisecondsToStart);

            }


        }
    }

}
