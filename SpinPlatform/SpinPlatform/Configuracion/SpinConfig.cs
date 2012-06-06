using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using SpinPlatform.IO;
using SpinPlatform.Data;
using System.Dynamic;
using System.Xml.Linq;


namespace SpinPlatform
{
    namespace Config
    {
        [XmlRoot("configParameters")]
        public class SpinConfig : ISpinPlatformInterface
        {
            /// <summary>
            /// Módulo de Configuración
            /// Éste módulo realiza la lectura del archivo XML de configuración de la aplicación en curso
            /// Tiene habilitado un único método GetData (con dos sobrecargas) para ello.
            /// Los tipos de variables que soporta son "String","Int","Bool" y "List".
            /// NOTA: Para su uso solo es necesario realizar un "GetData".
            /// No es necesario el uso de los métodos Init, SetData, Start o Stop (Sin implementar)
            /// </summary>
            
            #region Implementación de interface

            public void GetData(ref dynamic data, params string[] parameters)
            {
                //Gestionar mensaje
                data = _getExpandoFromXml(data);
            }

            public dynamic GetData(string data)
            {
                //Gestionar mensaje
                dynamic datos = new ExpandoObject();
                datos = _getExpandoFromXml(data);
                return datos;
            }

            public void Init(dynamic obj)
            {
                //parametros
            }

            public void SetData(ref dynamic data, params string[] parameters)
            {
                   
            }

            public void Start()
            {
            }

            public void Stop()
            {
            }

            public event Dispatcher.ResultEventHandler NewResultEvent;

            #endregion


            #region Métodos

            private static dynamic _getExpandoFromXml(String file, XElement node = null)
            {
                if (String.IsNullOrWhiteSpace(file) && node == null) return null;
                // If a file is not empty then load the xml and overwrite node with the
                // root element of the loaded document
                if (!String.IsNullOrWhiteSpace(file))
                {
                    var doc = XDocument.Load(file);  
                    node = doc.Root;
                }
                dynamic result = new ExpandoObject();


                foreach (var gn in node.Elements())
                {
                    // The code determines if it is a container node based on the child
                    // elements with the same name. If there is only one child element,
                    // but it should still be treated as an container obejct then ensure
                    // the attribute "type" with value "list" is added to the node.
                    var skip = false;
                    skip = (from a in gn.Attributes()
                            where a.Name.LocalName.ToLower() == "type"
                            select a.Value.ToLower()).FirstOrDefault() == "list" ? true :
                               gn.Elements().GroupBy(n => n.Name.LocalName).Count() == 1;

                    var p = result as IDictionary<String, dynamic>;
                    var values = new List<dynamic>();
                    // If the current node is a container node then we want to skip adding
                    // the container node itself, but instead we load the children elements
                    // of the current node. If the current node has child elements then load
                    // those child elements recursively
                    /*           values.Add("mio");
                               values.Add(23);
                               values.Add(Boolean.Parse("true"));
                      */
                    if (skip)
                        foreach (var item in gn.Elements())
                        {
                            values.Add(
                                (item.HasElements) ?
                                //YES
                                    _getExpandoFromXml(null, item) :
                                //NO
                            (
                            (from a in item.Attributes()
                             where a.Name.LocalName.ToLower() == "type"
                             select a.Value.ToLower()).FirstOrDefault() == "int" ?
                            gn.Value.Trim() :
                            ((from a in item.Attributes()
                              where a.Name.LocalName.ToLower() == "type"
                              select a.Value.ToLower()).FirstOrDefault() == "bool" ? gn.Value.Trim() :
                               gn.Value.Trim()

                            )
                            )
                            )
                            ;
                        }

                    else

                        if (gn.HasElements) values.Add(_getExpandoFromXml(null, gn));
                        else if ((from a in gn.Attributes()
                                  where a.Name.LocalName.ToLower() == "type"
                                  select a.Value.ToLower()).FirstOrDefault() == "int") values.Add(Int32.Parse(gn.Value.Trim()));
                        else if ((from a in gn.Attributes()
                                  where a.Name.LocalName.ToLower() == "type"
                                  select a.Value.ToLower()).FirstOrDefault() == "bool") values.Add(Boolean.Parse(gn.Value.Trim()));
                        else values.Add(gn.Value.Trim());

                    ;

                    // Add the object name + value or value collection to the dictionary
                    p[gn.Name.LocalName] = skip ? values : values.FirstOrDefault();
                }
                return result;
            }                    

            #endregion

        }
    }
}
