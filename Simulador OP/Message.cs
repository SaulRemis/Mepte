using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OPSaul
{
    class Message
    {
        string _messageid;
        string _plateid;
        double[,] _tabla1;
        string _decision;
        double _puntuacion;

        public string Messageid
        {
            get { return _messageid; }
            set { _messageid = value; }
        }

        public string Plateid
        {
            get { return _plateid; }
            set { _plateid = value; }
        }
        public string Decision
        {
            get { return _decision; }
            set { _decision = value; }
        }

        public double[,] Tabla1
        {
            get { return _tabla1; }
            set { _tabla1 = value; }
        }
        public double Puntuacion
        {
            get { return _puntuacion; }
            set { _puntuacion = value; }
        }

        public Message(string messageid,string plateid,string values,string decision,string puntuacion)
        {
            _messageid = messageid;
            _plateid = plateid;
            _decision = decision;
            _puntuacion = double.Parse(puntuacion)/10;
            int cont=0;
            string temp;
            double[,] tabl = new double [10,7];
            if (_messageid.Equals("M5"))
            {
                try
                {
                    for (int i = 0; i < 10; i++)
                    {
                        for (int j = 0; j < 7; j++)
                        {
                            temp = values.Substring(cont, 5);
                            tabl[i, j] = double.Parse(temp);
                            cont = cont + 5;
                           // tabl[i, j] = double.Parse(values.Substring((5 * j*(i+1)), 5));
                        }
                    }
                }
                catch
                {
                }
            }
            _tabla1 = tabl;

        }
    }
}
