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


        public PlateID(string id,double width, double length)
        {
            _ID =id;
            _Width = width;
            _Length = length;
        }
    }
}
