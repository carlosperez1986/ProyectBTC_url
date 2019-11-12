using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Data.Common;

namespace Capas
{
    public static class FuncionesSQL
    {

        /// <summary>
        /// const string strcnn = @"Data Source=localhost;Initial Catalog=gamekeyspotdb;User ID=sa;Password=Carloselias23.;Packet Size=4096";
        /// </summary>
        /// <value>The coneccion string.</value>
        private static string _connectionString;
        public static string ConeccionString
        {
            get
            {
                _connectionString = @"Data Source=82.223.108.84,10606;Initial Catalog=devitoursdb;User ID=devitoursuser;Password=Carloselias23.";
                return _connectionString;
            }
            private set => value = _connectionString;
        }

        /// <summary>
        /// Parametro Tipo para identificar 0 = executenonquery para insert,update,delete; 1= scalar para select(*)
        /// </summary>
        public static async Task<Dictionary<string, object>> PostData(string CommandText, List<SqlParameter> SqlParametros, int tipo)
        {

            SqlConnectionStringBuilder builder =
            new SqlConnectionStringBuilder(ConeccionString)
            {
                MultipleActiveResultSets = true
            };

            Dictionary<string, object> resultado = new Dictionary<string, object>();

            try
            {
                using (SqlConnection cnn = new SqlConnection(builder.ConnectionString))
                {
                    await cnn.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand(CommandText, cnn))
                    {
                        foreach (SqlParameter s in SqlParametros)
                        {
                            SqlParameter sp = new SqlParameter()
                            {
                                Value = s.Value,
                                SqlValue = s.SqlValue,
                                ParameterName = s.ParameterName,
                                DbType = s.DbType,
                                SqlDbType = s.SqlDbType
                            };

                            cmd.Parameters.Add(s);
                        };

                        if (tipo == 0)
                        {
                            resultado.Add("data", await cmd.ExecuteNonQueryAsync());
                            //resultado.Add("valor", cmd.ExecuteNonQueryAsync().Result.ToString());
                        }
                        else
                        {
                            resultado.Add("data", await cmd.ExecuteScalarAsync());
                        };

                    };
                    cnn.Close();
                };
            }
            catch (SqlException ex)
            {
                resultado.Add("error", ex.Message + "|" + ex.InnerException);
                resultado.Add("commandtext", CommandText);
                resultado.Add("tipo", tipo);

            }
            catch (Exception ex)
            {
                resultado.Add("error", ex.Message + "|" + ex.InnerException);
                resultado.Add("commandtext", CommandText);
                resultado.Add("tipo", tipo);

            };

            return resultado;
        }
        private static async Task<Dictionary<string, object>> GetLink(DbDataReader reader)
        {
            Dictionary<string, object> resultado = new Dictionary<string, object>();

            for (int i = 0; i < reader.FieldCount; i++)
            {
                // Process each column as appropriate  
                object type = reader.GetFieldType(i);
                string name = reader.GetName(i);
                object obj = await reader.GetFieldValueAsync<object>(i);
                resultado.Add(name, obj);
                //Console.WriteLine(obj);
            };

            return resultado;
        }

        public static async Task<Dictionary<string, object>> GetData(string CommandText, List<SqlParameter> SqlParametros)
        {
            SqlConnectionStringBuilder builder =
                new SqlConnectionStringBuilder(ConeccionString)
                {
                    // AsynchronousProcessing = true
                    // muls
                    ConnectRetryCount = 2,
                    ConnectRetryInterval = 60,
                    ConnectTimeout = 1000
                };

            Dictionary<string, object> resultado = new Dictionary<string, object>();

            try
            {
                using (SqlConnection cnn = new SqlConnection(builder.ConnectionString))
                {
                    await cnn.OpenAsync();

                    string sql = CommandText;

                    using (SqlCommand cmd = new SqlCommand(CommandText, cnn))
                    {
                        if (SqlParametros != null)
                        {
                            foreach (SqlParameter s in SqlParametros)
                            {
                                SqlParameter sp = new SqlParameter()
                                {
                                    Value = s.Value,
                                    SqlValue = s.SqlValue,
                                    ParameterName = s.ParameterName,
                                    DbType = s.DbType
                                };
                                cmd.Parameters.Add(s);
                            };
                        };

                        IList<object> l = new List<object>();

                        using (DbDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            int x = 0;
                            while (await reader.ReadAsync())
                            {
                                x = x + 1;
                                l.Add(await GetLink(reader));
                                //resultado.Add(x.ToString(), await GetLink(reader));
                            };

                            resultado.Add("data", l);
                            resultado.Add("numberRows", l.Count);

                            //for (int i = 0; i < reader.FieldCount; i++)
                            //{
                            //    // Process each column as appropriate  
                            //    object type = reader.GetFieldType(i);
                            //    string name = reader.GetName(i);
                            //    object obj = await reader.GetFieldValueAsync<object>(i);

                            //    dato.Add(name, obj);
                            //    //Console.WriteLine(obj);
                            //};
                            //resultado.Add("data" + x, dato);
                            //x = x + 1;

                            // await reader.NextResultAsync();
                        };

                        // if (l.Count > 0) { l.Clear(); };

                        // resultado.Add("valor", reader.ReadAsync());
                        //resultado.Add("count", reader.ReadAsync().Result.FieldCount);
                        //resultado.Add("p_valor", dt.Rows[0]);

                        //IDataReader sr = await cmd.ExecuteReaderAsync();

                        //DataTable dt = new DataTable();

                        //dt.Load(sr);

                        return resultado;
                    };

                }
            }
            catch (Exception ex)
            {
                resultado.Add("error", ex.Message);
                resultado.Add("commandtext", CommandText);

                return resultado;
            };
        }



    }
}
