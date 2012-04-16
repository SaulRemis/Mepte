using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Meplate
{
    class PlateID
    {
        string _ID;       
        double _Width;
        double _Length;
        double _Thickness;
        double _Tolerance1;
        double _Tolerance2;


        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public double Width
        {
            get { return _Width; }
            set { _Width = value; }
        }
        public double Length
        {
            get { return _Length; }
            set { _Length = value; }
        }

        public double Thickness
        {
            get { return _Thickness; }
            set { _Thickness = value; }
        }
       
        public double Tolerance1
        {
            get { return _Tolerance1; }
            set { _Tolerance1 = value; }
        }
        public double Tolerance2
        {
            get { return _Tolerance2; }
            set { _Tolerance2 = value; }
        }
        public PlateID(string id,double width, double length, double thickness, double tol1,double tol2)
        {
            _ID =id;
            _Width = width;
            _Length = length;
            _Thickness = thickness;
            _Tolerance1 = tol1;
            _Tolerance2 = tol2;
        }
    }
}
