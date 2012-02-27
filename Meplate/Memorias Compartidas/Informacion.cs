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
        int _Perfiles; // numero de perfiles añadidos a la chapa hasta el momento
        double _Rate; // las coordenadas (x,y,z) de los max y min para enviar y la distancia entre max y min


        //descriptores de acceso
        public int Perfiles { get { return _Perfiles; } }
        public double Rate { get { return _Rate; } }


        //metodos
        public Informacion(int medidas, double rate)
        {

            _Perfiles = medidas;
            _Rate = rate;

        }
    }
  
}
