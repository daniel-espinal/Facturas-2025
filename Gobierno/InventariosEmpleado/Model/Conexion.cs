using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionGobierno.Model
{
    public class Conexion
    {
        public SqlConnection conexion = new SqlConnection();
        String mostrarError;
        public string MostrarError
        {
            get => mostrarError;
            set => mostrarError = value;
        }

        public bool ConectarServer()
        {
            bool respuesta = false;
            //XpoProvider=MSSqlServer;data source=192.168.103.61;user id=facturacion.gobierno;password=Factura2017*;initial catalog=FACTURACION;Persist Security Info=true;Connection Timeout=1000"
            //  string stringConexion = @"Data Source=localhost;Initial Catalog=FACTURACION;Integrated Security=True";
            //string stringConexion = @"Data Source=192.168.103.61;user id=appfact;password=Appfac72017*;initial catalog=FACTURACION;Persist Security Info=true;Connection Timeout=720000";
            string stringConexion = @"Data Source=192.168.100.161;user id=appfact;password=Appfac72017*;initial catalog=FACTURACION;Persist Security Info=true;Connect Timeout=200; pooling='true'; Max Pool Size=200";
            try
            {
                conexion.ConnectionString = stringConexion;
                conexion.Open();
                respuesta = true;
            }
            catch (Exception ex)
            {
                MostrarError = "Error, al conectarse con el servidor " + ex.Message;
            }

            return respuesta;
        }

        public bool ConectarServer1()
        {
            bool respuesta = false;
            string stringConexion = @"Data Source=192.168.100.28;user id=appsoe;password=:S032017*;initial catalog=eehApps;Persist Security Info=true;Connect Timeout=200; pooling='true'; Max Pool Size=200";
            try
            {
                conexion.ConnectionString = stringConexion;
                conexion.Open();
                respuesta = true;
            }
            catch (Exception ex)
            {
                MostrarError = "Error, al conectarse con el servidor " + ex.Message;
            }

            return respuesta;
        }


        public void notificarFacturacionDialE(ref string vCodigoResult, ref string vMensajeResult, params object[] Datos)
        {
            SqlCommand pvCommand = new SqlCommand("[dbo].[sp_fact_notificar_Estadoresultado_Gobierno_Dial]", conexion);

            pvCommand.CommandType = CommandType.StoredProcedure;
            pvCommand.Parameters.Clear();
            pvCommand.Parameters.Add(new SqlParameter("@cod_agrupacion ", SqlDbType.Int) { SqlValue = Convert.ToInt32(Datos[0]) });
            pvCommand.Parameters.Add(new SqlParameter("@adjuntos", SqlDbType.VarChar, 500) { SqlValue = Convert.ToString(Datos[1]) });
            pvCommand.Parameters.Add(new SqlParameter("@cod_usuario_registro", SqlDbType.VarChar, 50) { SqlValue = Convert.ToString(Datos[2]) });


            SqlParameter CodigoResult = new SqlParameter();
            CodigoResult.ParameterName = "@cod_result";
            CodigoResult.DbType = DbType.Int32;
            CodigoResult.SqlDbType = SqlDbType.Int;
            CodigoResult.Direction = ParameterDirection.Output;
            pvCommand.Parameters.Add(CodigoResult);

            SqlParameter MenssgeResult = new SqlParameter();
            MenssgeResult.ParameterName = "@msj_result";
            MenssgeResult.DbType = DbType.String;
            MenssgeResult.Size = 200;
            MenssgeResult.SqlDbType = SqlDbType.NVarChar;
            MenssgeResult.Direction = ParameterDirection.Output;
            pvCommand.Parameters.Add(MenssgeResult);

            try
            {
                ConectarServer();
                int sqlRows = pvCommand.ExecuteNonQuery();
                vCodigoResult = pvCommand.Parameters["@cod_result"].Value.ToString();
                vMensajeResult = pvCommand.Parameters["@msj_result"].Value.ToString();


            }
            catch (Exception EX)
            {

                Console.WriteLine(EX.Message);

            }
            finally
            {
                Close();
            }


        }
        public void notificarFacturacionGobiernoDial(ref string vCodigoResult, ref string vMensajeResult, params object[] Datos)
        {
            SqlCommand pvCommand = new SqlCommand("[dbo].[sp_fact_notificar_facturacion_Gobierno]", conexion);

            pvCommand.CommandType = CommandType.StoredProcedure;
            pvCommand.Parameters.Clear();
            pvCommand.Parameters.Add(new SqlParameter("@cod_agrupacion ", SqlDbType.Int) { SqlValue = Convert.ToInt32(Datos[0]) });
            pvCommand.Parameters.Add(new SqlParameter("@adjuntos", SqlDbType.VarChar, 500) { SqlValue = Convert.ToString(Datos[1]) });
            pvCommand.Parameters.Add(new SqlParameter("@cod_usuario_registro", SqlDbType.VarChar, 50) { SqlValue = Convert.ToString(Datos[2]) });


            SqlParameter CodigoResult = new SqlParameter();
            CodigoResult.ParameterName = "@cod_result";
            CodigoResult.DbType = DbType.Int32;
            CodigoResult.SqlDbType = SqlDbType.Int;
            CodigoResult.Direction = ParameterDirection.Output;
            pvCommand.Parameters.Add(CodigoResult);

            SqlParameter MenssgeResult = new SqlParameter();
            MenssgeResult.ParameterName = "@msj_result";
            MenssgeResult.DbType = DbType.String;
            MenssgeResult.Size = 200;
            MenssgeResult.SqlDbType = SqlDbType.NVarChar;
            MenssgeResult.Direction = ParameterDirection.Output;
            pvCommand.Parameters.Add(MenssgeResult);

            try
            {
                ConectarServer();
                int sqlRows = pvCommand.ExecuteNonQuery();
                vCodigoResult = pvCommand.Parameters["@cod_result"].Value.ToString();
                vMensajeResult = pvCommand.Parameters["@msj_result"].Value.ToString();


            }
            catch (Exception EX)
            {

                Console.WriteLine(EX.Message);

            }
            finally
            {
                Close();
            }



        }


        public DataTable obtenerDataTable(String vQuery)
        {
            DataTable vDatos = new DataTable();
            try
            {
                string stringConexion = @"Data Source=192.168.100.161;user id=appfact;password=Appfac72017*;initial catalog=FACTURACION;Persist Security Info=true;Connect Timeout=200; pooling='true'; Max Pool Size=200";

                SqlDataAdapter vDataAdapter = new SqlDataAdapter(vQuery, stringConexion);
                vDataAdapter.SelectCommand.CommandTimeout = 25000;
                vDataAdapter.Fill(vDatos);
            }
            catch
            {
              //  throw;
            }
            return vDatos;
        }

        public DataTable obtenerDataTable1(String vQuery)
        {
            DataTable vDatos = new DataTable();
            try
            {
                string stringConexion = @"Data Source=192.168.100.28;user id=appsoe;password=S032017*;initial catalog=eehDirectorio;Persist Security Info=true;Connect Timeout=200; pooling='true'; Max Pool Size=200";

                SqlDataAdapter vDataAdapter = new SqlDataAdapter(vQuery, stringConexion);
                vDataAdapter.SelectCommand.CommandTimeout = 25000;
                vDataAdapter.Fill(vDatos);
            }
            catch
            {
                //  throw;
            }
            return vDatos;
        }



        public void vRegistrarFactura(ref string vCodigoResult, ref string vMensajeResult, params object[] Datos)
        {
            SqlCommand pvCommand = new SqlCommand("[dbo].[sp_ir_registrar_adjunto]", conexion);
            pvCommand.CommandType = CommandType.StoredProcedure;
            pvCommand.Parameters.Clear();
            pvCommand.Parameters.Add(new SqlParameter("@os ", SqlDbType.Int) { SqlValue = Convert.ToInt32(Datos[0]) });
            pvCommand.Parameters.Add(new SqlParameter("@id_liquidacion", SqlDbType.VarChar, 500) { SqlValue = Convert.ToString(Datos[1]) });
            pvCommand.Parameters.Add(new SqlParameter("@nombre", SqlDbType.VarChar, 500) { SqlValue = Convert.ToString(Datos[2]) });
            pvCommand.Parameters.Add(new SqlParameter("@cod_tipo_archivo", SqlDbType.VarChar, 500) { SqlValue = Convert.ToString(Datos[3]) });
            pvCommand.Parameters.Add(new SqlParameter("@formato_archivo", SqlDbType.VarChar, 500) { SqlValue = Convert.ToString(Datos[4]) });
            pvCommand.Parameters.Add(new SqlParameter("@tamanio", SqlDbType.VarChar, 500) { SqlValue = Convert.ToString(Datos[5]) });
            pvCommand.Parameters.Add(new SqlParameter("@sn_privado", SqlDbType.VarChar, 500) { SqlValue = Convert.ToString(Datos[6]) });
            pvCommand.Parameters.Add(new SqlParameter("@txt_desc", SqlDbType.VarChar, 500) { SqlValue = Convert.ToString(Datos[7]) });
            pvCommand.Parameters.Add(new SqlParameter("@byte", SqlDbType.VarChar, 500) { SqlValue = Convert.ToString(Datos[8]) });
            pvCommand.Parameters.Add(new SqlParameter("@cod_usuario_registro ", SqlDbType.VarChar, 500) { SqlValue = Convert.ToString(Datos[9]) });
            pvCommand.Parameters.Add(new SqlParameter("@sn_finalizar", SqlDbType.VarChar, 500) { SqlValue = Convert.ToString(Datos[10]) });
            pvCommand.Parameters.Add(new SqlParameter("@sn_mostrar", SqlDbType.VarChar, 500) { SqlValue = Convert.ToString(Datos[11]) });

            SqlParameter CodigoResult = new SqlParameter();
            CodigoResult.ParameterName = "@COD_ERROR";
            CodigoResult.DbType = DbType.Int32;
            CodigoResult.SqlDbType = SqlDbType.Int;
            CodigoResult.Direction = ParameterDirection.Output;
            pvCommand.Parameters.Add(CodigoResult);


            SqlParameter MenssgeError = new SqlParameter();
            MenssgeError.ParameterName = "@ERROR_MSJ";
            MenssgeError.DbType = DbType.String;
            MenssgeError.Size = 200;
            MenssgeError.SqlDbType = SqlDbType.NVarChar;
            MenssgeError.Direction = ParameterDirection.Output;
            pvCommand.Parameters.Add(MenssgeError);

            SqlParameter MenssgeResult = new SqlParameter();
            MenssgeResult.ParameterName = "@COD_ADJUNTO";
            MenssgeResult.DbType = DbType.String;
            MenssgeResult.Size = 200;
            MenssgeResult.SqlDbType = SqlDbType.NVarChar;
            MenssgeResult.Direction = ParameterDirection.Output;
            pvCommand.Parameters.Add(MenssgeResult);

            try
            {
                ConectarServer1();
                int sqlRows = pvCommand.ExecuteNonQuery();
                vCodigoResult = pvCommand.Parameters["@COD_ERROR"].Value.ToString();
                vMensajeResult = pvCommand.Parameters["@COD_ADJUNTO"].Value.ToString();


            }
            catch (Exception EX)
            {

                Console.WriteLine(EX.Message);

            }
            finally
            {
                Close();
            }




        }
        public void notificarFacturacionAC(ref string vCodigoResult, ref string vMensajeResult, params object[] Datos)
        {
            SqlCommand pvCommand = new SqlCommand("[dbo].[sp_fact_notificar_facturacion_masivos_AC]", conexion);

            pvCommand.CommandType = CommandType.StoredProcedure;
            pvCommand.Parameters.Clear();
            pvCommand.Parameters.Add(new SqlParameter("@cod_agrupacion ", SqlDbType.Int) { SqlValue = Convert.ToInt32(Datos[0]) });
            pvCommand.Parameters.Add(new SqlParameter("@adjuntos", SqlDbType.VarChar, 500) { SqlValue = Convert.ToString(Datos[1]) });
            pvCommand.Parameters.Add(new SqlParameter("@cod_usuario_registro", SqlDbType.VarChar, 50) { SqlValue = Convert.ToString(Datos[2]) });


            SqlParameter CodigoResult = new SqlParameter();
            CodigoResult.ParameterName = "@cod_result";
            CodigoResult.DbType = DbType.Int32;
            CodigoResult.SqlDbType = SqlDbType.Int;
            CodigoResult.Direction = ParameterDirection.Output;
            pvCommand.Parameters.Add(CodigoResult);

            SqlParameter MenssgeResult = new SqlParameter();
            MenssgeResult.ParameterName = "@msj_result";
            MenssgeResult.DbType = DbType.String;
            MenssgeResult.Size = 200;
            MenssgeResult.SqlDbType = SqlDbType.NVarChar;
            MenssgeResult.Direction = ParameterDirection.Output;
            pvCommand.Parameters.Add(MenssgeResult);

            try
            {
                ConectarServer();
                int sqlRows = pvCommand.ExecuteNonQuery();
                vCodigoResult = pvCommand.Parameters["@cod_result"].Value.ToString();
                vMensajeResult = pvCommand.Parameters["@msj_result"].Value.ToString();


            }
            catch (Exception EX)
            {

                Console.WriteLine(EX.Message);

            }
            finally
            {
                Close();
            }



        }
        public void notificarIrregularidad(ref string vCodigoResult, ref string vMensajeResult, params object[] Datos)
        {
            SqlCommand pvCommand = new SqlCommand("[dbo].[sp_fact_notificar_facturacion_irregularidad]", conexion);

            pvCommand.CommandType = CommandType.StoredProcedure;
            pvCommand.Parameters.Clear();
            pvCommand.Parameters.Add(new SqlParameter("@cod_agrupacion ", SqlDbType.Int) { SqlValue = Convert.ToInt32(Datos[0]) });
            pvCommand.Parameters.Add(new SqlParameter("@adjuntos", SqlDbType.VarChar, 500) { SqlValue = Convert.ToString(Datos[1]) });
            pvCommand.Parameters.Add(new SqlParameter("@cod_usuario_registro", SqlDbType.VarChar, 50) { SqlValue = Convert.ToString(Datos[2]) });


            SqlParameter CodigoResult = new SqlParameter();
            CodigoResult.ParameterName = "@cod_result";
            CodigoResult.DbType = DbType.Int32;
            CodigoResult.SqlDbType = SqlDbType.Int;
            CodigoResult.Direction = ParameterDirection.Output;
            pvCommand.Parameters.Add(CodigoResult);

            SqlParameter MenssgeResult = new SqlParameter();
            MenssgeResult.ParameterName = "@msj_result";
            MenssgeResult.DbType = DbType.String;
            MenssgeResult.Size = 200;
            MenssgeResult.SqlDbType = SqlDbType.NVarChar;
            MenssgeResult.Direction = ParameterDirection.Output;
            pvCommand.Parameters.Add(MenssgeResult);

            try
            {
                ConectarServer();
                int sqlRows = pvCommand.ExecuteNonQuery();
                vCodigoResult = pvCommand.Parameters["@cod_result"].Value.ToString();
                vMensajeResult = pvCommand.Parameters["@msj_result"].Value.ToString();


            }
            catch (Exception EX)
            {

                Console.WriteLine(EX.Message);

            }
            finally
            {
                Close();
            }



        }

        public void notificarFacturacionALTOS(ref string vCodigoResult, ref string vMensajeResult, params object[] Datos)
        {
            SqlCommand pvCommand = new SqlCommand("[dbo].[sp_fact_notificar_facturacion_Altos_F_Mercado]", conexion);

            pvCommand.CommandType = CommandType.StoredProcedure;
            pvCommand.Parameters.Clear();
            pvCommand.Parameters.Add(new SqlParameter("@cod_agrupacion ", SqlDbType.Int) { SqlValue = Convert.ToInt32(Datos[0]) });
            pvCommand.Parameters.Add(new SqlParameter("@adjuntos", SqlDbType.VarChar, 500) { SqlValue = Convert.ToString(Datos[1]) });
            pvCommand.Parameters.Add(new SqlParameter("@cod_usuario_registro", SqlDbType.VarChar, 50) { SqlValue = Convert.ToString(Datos[2]) });


            SqlParameter CodigoResult = new SqlParameter();
            CodigoResult.ParameterName = "@cod_result";
            CodigoResult.DbType = DbType.Int32;
            CodigoResult.SqlDbType = SqlDbType.Int;
            CodigoResult.Direction = ParameterDirection.Output;
            pvCommand.Parameters.Add(CodigoResult);

            SqlParameter MenssgeResult = new SqlParameter();
            MenssgeResult.ParameterName = "@msj_result";
            MenssgeResult.DbType = DbType.String;
            MenssgeResult.Size = 200;
            MenssgeResult.SqlDbType = SqlDbType.NVarChar;
            MenssgeResult.Direction = ParameterDirection.Output;
            pvCommand.Parameters.Add(MenssgeResult);

            try
            {
                ConectarServer();
                int sqlRows = pvCommand.ExecuteNonQuery();
                vCodigoResult = pvCommand.Parameters["@cod_result"].Value.ToString();
                vMensajeResult = pvCommand.Parameters["@msj_result"].Value.ToString();


            }
            catch (Exception EX)
            {

                Console.WriteLine(EX.Message);

            }
            finally
            {
                Close();
            }



        }


        public void notificarFacturacionMS(ref string vCodigoResult, ref string vMensajeResult, params object[] Datos)
        {
            SqlCommand pvCommand = new SqlCommand("[dbo].[sp_fact_notificar_facturacion_Masivo]", conexion);

            pvCommand.CommandType = CommandType.StoredProcedure;
            pvCommand.Parameters.Clear();
            pvCommand.Parameters.Add(new SqlParameter("@clave ", SqlDbType.Int) { SqlValue = Convert.ToInt32(Datos[0]) });
            pvCommand.Parameters.Add(new SqlParameter("@adjuntos", SqlDbType.VarChar, 500) { SqlValue = Convert.ToString(Datos[1]) });
            pvCommand.Parameters.Add(new SqlParameter("@cod_usuario_registro", SqlDbType.VarChar, 50) { SqlValue = Convert.ToString(Datos[2]) });


            SqlParameter CodigoResult = new SqlParameter();
            CodigoResult.ParameterName = "@cod_result";
            CodigoResult.DbType = DbType.Int32;
            CodigoResult.SqlDbType = SqlDbType.Int;
            CodigoResult.Direction = ParameterDirection.Output;
            pvCommand.Parameters.Add(CodigoResult);

            SqlParameter MenssgeResult = new SqlParameter();
            MenssgeResult.ParameterName = "@msj_result";
            MenssgeResult.DbType = DbType.String;
            MenssgeResult.Size = 200;
            MenssgeResult.SqlDbType = SqlDbType.NVarChar;
            MenssgeResult.Direction = ParameterDirection.Output;
            pvCommand.Parameters.Add(MenssgeResult);

            try
            {
                ConectarServer();
                int sqlRows = pvCommand.ExecuteNonQuery();
                vCodigoResult = pvCommand.Parameters["@cod_result"].Value.ToString();
                vMensajeResult = pvCommand.Parameters["@msj_result"].Value.ToString();


            }
            catch (Exception EX)
            {

                Console.WriteLine(EX.Message);

            }
            finally
            {
                Close();
            }



        }



        public void notificarFacturacion(ref string vCodigoResult, ref string vMensajeResult, params object[] Datos)
        {
            SqlCommand pvCommand = new SqlCommand("[dbo].[sp_fact_notificar_facturacion]", conexion);

            pvCommand.CommandType = CommandType.StoredProcedure;
            pvCommand.Parameters.Clear();
            pvCommand.Parameters.Add(new SqlParameter("@cod_agrupacion ", SqlDbType.Int) { SqlValue = Convert.ToInt32(Datos[0]) });
            pvCommand.Parameters.Add(new SqlParameter("@adjuntos", SqlDbType.VarChar, 500) { SqlValue = Convert.ToString(Datos[1]) });
            pvCommand.Parameters.Add(new SqlParameter("@cod_usuario_registro", SqlDbType.VarChar, 50) { SqlValue = Convert.ToString(Datos[2]) });


            SqlParameter CodigoResult = new SqlParameter();
            CodigoResult.ParameterName = "@cod_result";
            CodigoResult.DbType = DbType.Int32;
            CodigoResult.SqlDbType = SqlDbType.Int;
            CodigoResult.Direction = ParameterDirection.Output;
            pvCommand.Parameters.Add(CodigoResult);

            SqlParameter MenssgeResult = new SqlParameter();
            MenssgeResult.ParameterName = "@msj_result";
            MenssgeResult.DbType = DbType.String;
            MenssgeResult.Size = 200;
            MenssgeResult.SqlDbType = SqlDbType.NVarChar;
            MenssgeResult.Direction = ParameterDirection.Output;
            pvCommand.Parameters.Add(MenssgeResult);

            try
            {
                ConectarServer();
                int sqlRows = pvCommand.ExecuteNonQuery();
                vCodigoResult = pvCommand.Parameters["@cod_result"].Value.ToString();
                vMensajeResult = pvCommand.Parameters["@msj_result"].Value.ToString();


            }
            catch (Exception EX)
            {

                Console.WriteLine(EX.Message);

            }
            finally
            {
                Close();
            }



        }

        public void notificarFacturacionG(ref string vCodigoResult, ref string vMensajeResult, params object[] Datos)
        {
            SqlCommand pvCommand = new SqlCommand("[dbo].[sp_fact_notificar_facturacion_Gob]", conexion);

            pvCommand.CommandType = CommandType.StoredProcedure;
            pvCommand.Parameters.Clear();
            pvCommand.Parameters.Add(new SqlParameter("@cod_agrupacion ", SqlDbType.Int) { SqlValue = Convert.ToInt32(Datos[0]) });
            pvCommand.Parameters.Add(new SqlParameter("@adjuntos", SqlDbType.VarChar, 500) { SqlValue = Convert.ToString(Datos[1]) });
            pvCommand.Parameters.Add(new SqlParameter("@cod_usuario_registro", SqlDbType.VarChar, 50) { SqlValue = Convert.ToString(Datos[2]) });


            SqlParameter CodigoResult = new SqlParameter();
            CodigoResult.ParameterName = "@cod_result";
            CodigoResult.DbType = DbType.Int32;
            CodigoResult.SqlDbType = SqlDbType.Int;
            CodigoResult.Direction = ParameterDirection.Output;
            pvCommand.Parameters.Add(CodigoResult);

            SqlParameter MenssgeResult = new SqlParameter();
            MenssgeResult.ParameterName = "@msj_result";
            MenssgeResult.DbType = DbType.String;
            MenssgeResult.Size = 200;
            MenssgeResult.SqlDbType = SqlDbType.NVarChar;
            MenssgeResult.Direction = ParameterDirection.Output;
            pvCommand.Parameters.Add(MenssgeResult);

            try
            {
                ConectarServer();
                int sqlRows = pvCommand.ExecuteNonQuery();
                vCodigoResult = pvCommand.Parameters["@cod_result"].Value.ToString();
                vMensajeResult = pvCommand.Parameters["@msj_result"].Value.ToString();


            }
            catch (Exception EX)
            {

                Console.WriteLine(EX.Message);

            }
            finally
            {
                Close();
            }


        }
        public void notificarFacturacionGobiernoDiaria(ref string vCodigoResult, ref string vMensajeResult, params object[] Datos)
        {
            SqlCommand pvCommand = new SqlCommand("[dbo].[sp_fact_notificar_facturacion_Gobierno]", conexion);

            pvCommand.CommandType = CommandType.StoredProcedure;
            pvCommand.Parameters.Clear();
            pvCommand.Parameters.Add(new SqlParameter("@cod_agrupacion ", SqlDbType.Int) { SqlValue = Convert.ToInt32(Datos[0]) });
            pvCommand.Parameters.Add(new SqlParameter("@adjuntos", SqlDbType.VarChar, 500) { SqlValue = Convert.ToString(Datos[1]) });
            pvCommand.Parameters.Add(new SqlParameter("@cod_usuario_registro", SqlDbType.VarChar, 50) { SqlValue = Convert.ToString(Datos[2]) });


            SqlParameter CodigoResult = new SqlParameter();
            CodigoResult.ParameterName = "@cod_result";
            CodigoResult.DbType = DbType.Int32;
            CodigoResult.SqlDbType = SqlDbType.Int;
            CodigoResult.Direction = ParameterDirection.Output;
            pvCommand.Parameters.Add(CodigoResult);

            SqlParameter MenssgeResult = new SqlParameter();
            MenssgeResult.ParameterName = "@msj_result";
            MenssgeResult.DbType = DbType.String;
            MenssgeResult.Size = 200;
            MenssgeResult.SqlDbType = SqlDbType.NVarChar;
            MenssgeResult.Direction = ParameterDirection.Output;
            pvCommand.Parameters.Add(MenssgeResult);

            try
            {
                ConectarServer();
                int sqlRows = pvCommand.ExecuteNonQuery();
                vCodigoResult = pvCommand.Parameters["@cod_result"].Value.ToString();
                vMensajeResult = pvCommand.Parameters["@msj_result"].Value.ToString();


            }
            catch (Exception EX)
            {

                Console.WriteLine(EX.Message);

            }
            finally
            {
                Close();
            }
        }



        public void notificarFacturacionE(ref string vCodigoResult, ref string vMensajeResult, params object[] Datos)
        {
            SqlCommand pvCommand = new SqlCommand("[dbo].[sp_fact_notificar_facturacion_Gob_Est]", conexion);

            pvCommand.CommandType = CommandType.StoredProcedure;
            pvCommand.Parameters.Clear();
            pvCommand.Parameters.Add(new SqlParameter("@cod_agrupacion ", SqlDbType.Int) { SqlValue = Convert.ToInt32(Datos[0]) });
            pvCommand.Parameters.Add(new SqlParameter("@adjuntos", SqlDbType.VarChar, 500) { SqlValue = Convert.ToString(Datos[1]) });
            pvCommand.Parameters.Add(new SqlParameter("@cod_usuario_registro", SqlDbType.VarChar, 50) { SqlValue = Convert.ToString(Datos[2]) });


            SqlParameter CodigoResult = new SqlParameter();
            CodigoResult.ParameterName = "@cod_result";
            CodigoResult.DbType = DbType.Int32;
            CodigoResult.SqlDbType = SqlDbType.Int;
            CodigoResult.Direction = ParameterDirection.Output;
            pvCommand.Parameters.Add(CodigoResult);

            SqlParameter MenssgeResult = new SqlParameter();
            MenssgeResult.ParameterName = "@msj_result";
            MenssgeResult.DbType = DbType.String;
            MenssgeResult.Size = 200;
            MenssgeResult.SqlDbType = SqlDbType.NVarChar;
            MenssgeResult.Direction = ParameterDirection.Output;
            pvCommand.Parameters.Add(MenssgeResult);

            try
            {
                ConectarServer();
                int sqlRows = pvCommand.ExecuteNonQuery();
                vCodigoResult = pvCommand.Parameters["@cod_result"].Value.ToString();
                vMensajeResult = pvCommand.Parameters["@msj_result"].Value.ToString();


            }
            catch (Exception EX)
            {

                Console.WriteLine(EX.Message);

            }
            finally
            {
                Close();
            }


        }



        public void Close()
        {
            conexion.Close();
        }
    }
}
