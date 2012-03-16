using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpinPlatform
{
    namespace IO
    {
        class SpinIOXMLData : SpinIOTxtData
        {
         #region Variables

            //parametros
            Type _structureType; //indicates the kind of structure of the xml file we are reading
            #endregion

            #region Propiedades

            public Type StructureType
            {
                get { return _structureType; }
                set { _structureType = value; }
            }
        
            #endregion
        }

    }
}
