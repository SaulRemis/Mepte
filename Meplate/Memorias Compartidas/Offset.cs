using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Meplate
{
    class Offset
    {
             
        double [] _Valores;
        double [] _Referencias;



        public double[] Valores
        {
            get { return _Valores; }
            set { _Valores = value; }
        }

        public double[] Referencias
        {
            get { return _Referencias; }
            set { _Referencias = value; }
        }


        public Offset(double[] valores, double[] referencias)
        {
            _Valores = valores;
            _Referencias = referencias;
        }

    }
}
