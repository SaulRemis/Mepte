using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Meplate.Memorias_Compartidas
{
     [Serializable]
    class Tarjeta
    {
         double _Velocidad; ///< Velocdad recibida por la tarjeta
         double _Avance;  ///< Avance recibido por la tarjeta
                          ///
         //descriptores de acceso
        public double Velocidad { get { return _Velocidad; } }
        public double Avance { get { return _Avance; } }


        //metodos
        public Tarjeta(double vel, double avan)
        {

            _Velocidad = vel;
            _Avance = avan;

        }
    }
}
