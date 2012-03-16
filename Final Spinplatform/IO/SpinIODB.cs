using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpinPlatform.Errors;
using SpinPlatform.DataBase;
using System.Data;

namespace SpinPlatform
{
    namespace IO
    {
        internal class SpinIODB : ISpinPlatformInterface
        {
            /// <summary>
            /// Class to work with db queries
            /// </summary>

            #region Variables

            private SpinDataBase _exeBaseDeDatos = null;

            #endregion

            #region Implementación de interface

            public void Init(dynamic obj)
            {
                /// <summary>
                /// Se encarga de inicializar el módulo de bases de datos necesario para realizar peticiones
                /// Campos necesarios:
                /// Los solicitados por los módulos subsiguientes.
                /// </summary>

                _exeBaseDeDatos = new SpinDataBase();
                _exeBaseDeDatos.Init(obj);
            }

            public void Start()
            {
                /// <summary>
                /// Se encarga de abrir la conexión de la base de datos
                /// Campos necesarios:
                /// Los solicitados por los módulos subsiguientes.
                /// </summary>
                 _exeBaseDeDatos.Start();
            }

            public object GetData(dynamic obj)
            {
                /// <summary>
                /// Se encarga de preparar las consultas a las bases de datos para recuperar datos
                /// En concreto el objeto de entrada debe disponer de los siguientes campos:
                /// (OBLIGATORIO)DBDefaultTableName: nombre de la tabla sobre la que se realizarán consultas
                /// (OBLIGATORIO)DBQueryType: palabra clave de la consulta. Implementado SELECT.
                /// (OBLIGATORIO)DBQueryColumns: nombre de las columnas de la tabla que se desean consultar 
                ///     (si se desean todas, inicializar simplemente) -> new List<String>();
                ///     (si se desean algunas columnas, realizar un Add por columna) -> .Add("nombreColumna");
                /// (OBLIGATORIO)DBQueryValues: parámetros de selección de la petición
                ///     (cada entrada en lalista será unanueva especificación) -> .Add("usuario='Saul', ");
                /// </summary>

                obj = getMessageToTable(obj);
                try
                {
                    DataTable table = obj.DBResponse[obj.DBDefaultTableName];
                    foreach (DataRow dr in table.Rows)
                    {
                        obj.IOMessage.Add((string)dr[0]);
                    }
                }
                catch (Exception ex)
                {
                    throw SpinException.GetException("SpinIODB:: " + ex.Message, ex);
                }

                return obj;
            }

            public void SetData(dynamic obj)
            {
                /// <summary>
                /// Se encarga de preparar las consultas a las bases de datos para escribir
                /// En concreto el objeto de entrada debe disponer de los siguientes campos:
                /// (OBLIGATORIO)DBDefaultTableName: nombre de la tabla sobre la que se realizarán consultas
                /// (OBLIGATORIO)DBQueryType: palabra clave de la consulta. Implementado INSERT y UPDATE.
                /// (OBLIGATORIO)IOMessage: listado con los datos que se desean introducir.
                /// (OBLIGATORIO)DBQueryColumns: nombre de las columnas de la tabla que se desean tocar 
                ///     (si se desean todas, inicializar simplemente) -> new List<String>();
                ///     (si se desean algunas columnas, realizar un Add por columna) -> .Add("nombreColumna1,nombreColumna2,...");
                ///     (cada entrada en la lista será una nueva petición a la base de datos)
                /// (OPCIONAL)DBQueryUpdateRow: selección de la fila que se desea modificar en el caso de UPDATE solo
                ///     (cada entrada en la lista será una nueva petición a la base de datos) -> .Add("usuario='Saul'");
                /// </summary>
                addMessageToTable(obj);

            }

            public void Stop()
            {
                /// <summary>
                /// Se encarga de cerrar la conexión de la base de datos
                /// Campos necesarios:
                /// Los solicitados por los módulos subsiguientes.
                /// </summary>
                 _exeBaseDeDatos.Stop();
            }

            public event Dispatcher.ResultEventHandler NewResultEvent;

            #endregion

            #region Métodos


           internal void addMessageToTable(dynamic obj)
            {

                for (int i = 0; i < obj.IOMessage.Count; i++)
                {
                    try
                    {
                        String columnas = "";
                        if (((List<String>)obj.DBQueryColumns).Count > 0)
                            columnas ="("+ obj.DBQueryColumns[i]+")";
                        if (obj.DBQueryType == "INSERT")
                            obj.DBQuery = obj.DBQueryType + " INTO " + obj.DBDefaultTableName + " " + columnas + " VALUES ('" + obj.IOMessage[i] + "')";
                        else if (obj.DBQueryType == "UPDATE")
                        {
                            string valores="";
                            string [] separators = new string [1];
                            separators[0]=",";
                            string [] valo = ((String)obj.IOMessage[i]).Split(separators,1000,StringSplitOptions.RemoveEmptyEntries);
                            string[] name = ((String)obj.DBQueryColumns[i]).Split(separators, 1000, StringSplitOptions.RemoveEmptyEntries);
                            for (int tam = 0; tam < valo.Length; tam++)
                            {
                                valores = name[tam] + "='" + valo[tam]+"', ";
                            }
                            obj.DBQuery = obj.DBQueryType + " " + obj.DBDefaultTableName + " SET " + valores.Substring(0,valores.Length-2) + " WHERE " + obj.DBQueryUpdateRow[i];
                        }
                        _exeBaseDeDatos.SetData(obj);
                    }
                    catch(Exception ex)
                    {
                        throw SpinException.GetException("SPINIODB:: "+ex.Message,ex);
                    }
                }
            }

            internal dynamic getMessageToTable(dynamic obj)
            {
                String columns = " *";
                String parameters = "";
                try
                {
                    if (((List<String>)obj.DBQueryColumns).Count > 0)
                    {
                        columns = " (";
                        foreach (String parame in obj.DBQueryColumns)
                        {
                            columns += (String)parame + ", ";
                        }
                        columns = columns.Substring(0, columns.Length - 2) + ") ";
                    }
                    if (((List<String>)obj.DBQueryValues).Count > 0)
                    {
                        parameters = " WHERE ";
                        foreach (String parame in obj.DBQueryValues)
                        {
                            parameters += parame;
                        }
                    }

                    obj.DBQuery = obj.DBQueryType + columns + " FROM " + obj.DBDefaultTableName + parameters;
                    return _exeBaseDeDatos.GetData(obj);
                }
                catch (Exception ex)
                {
                    throw SpinException.GetException("SPINIODB:: " + ex.Message, ex);
                }
            }

            #endregion
        }
    }
}
