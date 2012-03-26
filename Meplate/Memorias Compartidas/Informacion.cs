using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Meplate
{

    [Serializable]
    public class Informacion
    {
        //mienbros privados
        double _Avance; // numero de mm añadidos a la chapa hasta el momento
        double _Rate; // las coordenadas (x,y,z) de los max y min para enviar y la distancia entre max y min


        //descriptores de acceso
        public double Avance { get { return _Avance; } }
        public double Rate { get { return _Rate; } }


        //metodos
        public Informacion(double avance, double rate)
        {

            _Avance = avance;
            _Rate = rate;

        }
    }
  
}
