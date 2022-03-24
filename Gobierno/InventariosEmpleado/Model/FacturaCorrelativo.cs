using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionGobierno.Model
{
    public class FacturaCorrelativo
    {
        string tipoFactura;
        decimal periodo;
        decimal contadorInicial;
        decimal contadorFinal;
        string estado;
        string clave;
        string descripcion;
        DateTime fecha;
        Boolean generardetalle;




        public string TipoFactura { get => tipoFactura; set => tipoFactura = value; }
        public decimal Periodo { get => periodo; set => periodo = value; }
        public decimal ContadorInicial { get => contadorInicial; set => contadorInicial = value; }
        public decimal ContadorFinal { get => contadorFinal; set => contadorFinal = value; }
        public string Estado { get => estado; set => estado = value; }
        public string Clave { get => clave; set => clave = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        public DateTime Fecha { get => fecha; set => fecha = value; }
        public bool Generardetalle { get => generardetalle; set => generardetalle = value; }

        public FacturaCorrelativo obtenerFacturaCorrelativo(string tipoFactura, int clave, int paramPeriodoFact)
        {
            FacturaCorrelativo fc = obtenerTipoFactura(tipoFactura);          
            
            if (fc != null)
            {
                fc.ContadorInicial -= 1;
                fc.Generardetalle = false;
            }
            else
            {
                fc = crearRegistroPeriodoActualFacturacion(tipoFactura, clave, paramPeriodoFact);
                fc.Generardetalle = true;
            }

            return fc;
        }

        private FacturaCorrelativo obtenerTipoFactura(string tipoFactura)
        {
            FacturaCorrelativo fc = null;
            // if (tipoFactura == "MASIVA")
            if (tipoFactura == Util.Util.TIPO_FACTURA_MASIVA)               
            {
                fc = exitePeriodoActualFacturacion(generarMesFacturacion(2), generarMesFacturacion(1), tipoFactura);
            }
            else // cuando es FAC_ESPECIAL_CLAVE
            {
                fc = exitePeriodoActualFacturacion(generarMesFacturacion(1), generarMesFacturacion(0), tipoFactura);
            }

            return fc;
        }

        public int insertarDetalleCorrelativo(List<AvisoGobierno> listAvisoGob, int cantidadFacturas, decimal periodo, string tipoFactura)
        {


            int recordInsert = 0;
            try
            {
                int nroFactura = cantidadFacturas + 1;
                for (int i = 0; i < listAvisoGob.Count; i++)
                {
                    Conexion conexion = new Conexion();
                    SqlCommand command = new SqlCommand();
                    command.Connection = conexion.conexion;
                    command.CommandType = CommandType.Text;
                    command.CommandText = Util.Util.CREAR_REGISTRO_DETALLE_FACTURACION;
                    command.Parameters.AddWithValue("@PERIODO", periodo);
                    command.Parameters.AddWithValue("@N_FACTURA", nroFactura);
                    // command.Parameters.AddWithValue("@TIPO_FACTURA", "MASIVA");
                    command.Parameters.AddWithValue("@TIPO_FACTURA", tipoFactura);
                    command.Parameters.AddWithValue("@CLAVE", listAvisoGob[i].Clave_primaria);
                    command.Parameters.AddWithValue("@PREFIJO", listAvisoGob[i].Prefijo);
                    command.Parameters.AddWithValue("@FECHA", DateTime.Now);
                    command.Parameters.AddWithValue("@ESTADO", "I");
                    command.Parameters.AddWithValue("@CANTIDAD", 1);

                    if (conexion.ConectarServer())
                    {
                        try
                        {
                            int recordsAffected = command.ExecuteNonQuery();
                            if (recordsAffected == 1)
                            {
                                recordInsert++;
                                nroFactura++;
                            }
                        }
                        catch (Exception e)
                        {

                            Console.WriteLine(e.Message);
                        }


                    }

                    conexion.Close();
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            return recordInsert;
        }

        public Boolean actualiarPeriodoAnterior(decimal periodo, string tipoFactura)
        {
            Boolean estadoActualizado = false;
            Conexion conexion = new Conexion();
            try
            {
                SqlCommand command = new SqlCommand();
                if (conexion.ConectarServer())
                {
                    string querystr = Util.Util.ACTUALIZAR_PERIODO_ANTERIOR;
                    SqlCommand sqlCommand = new SqlCommand(querystr, conexion.conexion);
                    sqlCommand.Parameters.AddWithValue("@Estado", "I");
                    sqlCommand.Parameters.AddWithValue("@Periodo", periodo);
                    sqlCommand.Parameters.AddWithValue("@TipoFactura", tipoFactura);
                    int recordsAffected = sqlCommand.ExecuteNonQuery();
                    if (recordsAffected == 1)
                    {
                        estadoActualizado = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conexion.Close();
            }

            return estadoActualizado;
        }
        public Boolean actualizarPeriodo(decimal contadorFinal, decimal periodo, string tipoFactura)
        {
            Boolean periodoActualizado = false;
            Conexion conexion = new Conexion();
            try
            {
                SqlCommand command = new SqlCommand();
                if (conexion.ConectarServer())
                {
                    string querystr = Util.Util.ACTUALIZAR_PERIODO_ACTUAL;
                    SqlCommand sqlCommand = new SqlCommand(querystr, conexion.conexion);
                    sqlCommand.Parameters.AddWithValue("@ContadorFinal", contadorFinal);
                    sqlCommand.Parameters.AddWithValue("@Estado", "A");
                    sqlCommand.Parameters.AddWithValue("@Periodo", periodo);
                    sqlCommand.Parameters.AddWithValue("@TipoFactura", tipoFactura);
                   
                    int recordsAffected = sqlCommand.ExecuteNonQuery();
                    if (recordsAffected == 1)
                    {
                        periodoActualizado = true;
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conexion.Close();
            }
            return periodoActualizado;
        }

        public string generarMesFact3anterior(int value)
        {
            DateTime d = DateTime.Now.AddMonths(-2);
            d = d.AddMonths(-value);
            string vMes = d.Month.ToString().PadLeft(2, '0');

            string vaño = d.Year.ToString().Remove(0, 2);

            string vPeriodoFacturacion = vaño + vMes;


            string ultimoMesFacturacion = DateTime.Today.Year.ToString();
            ultimoMesFacturacion = ultimoMesFacturacion.Remove(0, 2);
            if ((DateTime.Today.Month) == 1)
            {
                int vCambioAnio = Convert.ToInt32(ultimoMesFacturacion) - 1;
                ultimoMesFacturacion = Convert.ToString(vCambioAnio);
                ultimoMesFacturacion += "12".PadLeft(2, '0');
                if (value == 2)
                {
                    ultimoMesFacturacion = "1711";
                }
            }
            else
            {
                ultimoMesFacturacion += (DateTime.Today.Month - (value)).ToString().PadLeft(2, '0');
            }
            // ultimoMesFacturacion += (DateTime.Today.Month - (value)).ToString().PadLeft(2, '0');
            if (ultimoMesFacturacion == "1800")
            {
                ultimoMesFacturacion = "1712";
            }
            return vPeriodoFacturacion;
        }


        public string generarMesFactanterior(int value)
        {
            DateTime d = DateTime.Now.AddMonths(-1);
            d = d.AddMonths(-value);
            string vMes = d.Month.ToString().PadLeft(2, '0');

            string vaño = d.Year.ToString().Remove(0, 2);

            string vPeriodoFacturacion = vaño + vMes;


            string ultimoMesFacturacion = DateTime.Today.Year.ToString();
            ultimoMesFacturacion = ultimoMesFacturacion.Remove(0, 2);
            if ((DateTime.Today.Month) == 1)
            {
                int vCambioAnio = Convert.ToInt32(ultimoMesFacturacion) - 1;
                ultimoMesFacturacion = Convert.ToString(vCambioAnio);
                ultimoMesFacturacion += "12".PadLeft(2, '0');
                if (value == 2)
                {
                    ultimoMesFacturacion = "1711";
                }
            }
            else
            {
                ultimoMesFacturacion += (DateTime.Today.Month - (value)).ToString().PadLeft(2, '0');
            }
            // ultimoMesFacturacion += (DateTime.Today.Month - (value)).ToString().PadLeft(2, '0');
            if (ultimoMesFacturacion == "1800")
            {
                ultimoMesFacturacion = "1712";
            }
            return vPeriodoFacturacion;
        }


        public string generarMesFacturacion(int value)
        {
            DateTime d = DateTime.Now;
            d = d.AddMonths(-value);
            string vMes = d.Month.ToString().PadLeft(2, '0');

            string vaño = d.Year.ToString().Remove(0, 2);

            string vPeriodoFacturacion= vaño+vMes;


            string ultimoMesFacturacion = DateTime.Today.Year.ToString();
            ultimoMesFacturacion = ultimoMesFacturacion.Remove(0, 2);
            if ((DateTime.Today.Month) == 1)
            {
                int vCambioAnio = Convert.ToInt32(ultimoMesFacturacion) - 1;
                ultimoMesFacturacion = Convert.ToString(vCambioAnio);
                ultimoMesFacturacion += "12".PadLeft(2, '0');
                if (value == 2)
                {
                    ultimoMesFacturacion = "1711";
                }
            }
            else
            {
                ultimoMesFacturacion += (DateTime.Today.Month - (value)).ToString().PadLeft(2, '0');
            }
            // ultimoMesFacturacion += (DateTime.Today.Month - (value)).ToString().PadLeft(2, '0');
            if(ultimoMesFacturacion == "1800")
            {
                ultimoMesFacturacion = "1712";
            }
            return vPeriodoFacturacion;
        }

        public string generarMesFacturacionNew(int value)
        {

            string ultimoMesFacturacion = DateTime.Today.Year.ToString();
            ultimoMesFacturacion = ultimoMesFacturacion.Remove(0, 2);          
             ultimoMesFacturacion += (DateTime.Today.Month - (value)).ToString().PadLeft(2, '0');
            return ultimoMesFacturacion;
        }

      
        private List<FacturaCorrelativo> obtenerListaFactCorrelativo(string periodAnterior, string periodoActual)
        {
            List<FacturaCorrelativo> listfacCorr = new List<FacturaCorrelativo>();
            Conexion conexion = new Conexion();
            try
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conexion.conexion;
                command.CommandText = Util.Util.EXISTE_PERIODO_FACTURACION;
                command.Parameters.Add("@PeriodoAnt", SqlDbType.Decimal);
                command.Parameters["@PeriodoAnt"].Value = periodAnterior;
                command.Parameters.Add("@PeriodoAct", SqlDbType.Decimal);
                command.Parameters["@PeriodoAct"].Value = periodoActual;

                if (conexion.ConectarServer())
                {
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {

                        while (reader.Read())
                        {
                            FacturaCorrelativo fac = new FacturaCorrelativo();
                            fac.TipoFactura = reader["TIPO_FACTURA"].ToString();
                            fac.Periodo = (decimal)reader["PERIODO"];
                            fac.ContadorInicial = (decimal)reader["CONTADOR_INICIAL"];
                            fac.ContadorFinal = (decimal)reader["CONTADOR_FINAL"];
                            fac.Estado = reader["ESTADO"].ToString();
                            fac.Clave = reader["CLAVE"].ToString();
                            fac.Descripcion = reader["PERIODO"].ToString();
                            listfacCorr.Add(fac);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            finally
            {
                conexion.Close();
            }
            return listfacCorr;
        }

        private FacturaCorrelativo exitePeriodoActualFacturacion(string periodoAnterior, string periodoActual, string tipoFactura)
        {
            FacturaCorrelativo facCorrExiste = null;

            List<FacturaCorrelativo> listfacCorr = obtenerListaFactCorrelativo(periodoAnterior, periodoActual);

            for (int i = 0; i < listfacCorr.Count; i++)
            {
                if (ContadorFinal == 0)
                {
                    if (listfacCorr[i].Periodo == Convert.ToDecimal(periodoAnterior))
                    {
                        ContadorFinal = listfacCorr[i].ContadorFinal;
                    }

                    if (listfacCorr[i].Periodo == Convert.ToDecimal(periodoActual))
                    {
                        // if (listfacCorr[i].TipoFactura == "MASIVA")
                        if (listfacCorr[i].TipoFactura == tipoFactura)
                        {
                            ContadorFinal = 0;
                            facCorrExiste = listfacCorr[i];
                            break;
                        }
                        else
                        {
                            ContadorFinal = listfacCorr[i].ContadorFinal;
                        }

                    }
                }
            }
            return facCorrExiste;
        }

        private FacturaCorrelativo crearRegistroPeriodoActualFacturacion(string tipoFactura, int clave, int paramPeriodoFact)
        {

            FacturaCorrelativo fctCorr = new FacturaCorrelativo();
            string periodo = "";
            //   if(tipoFactura == "MASIVA")
            if (tipoFactura == Util.Util.TIPO_FACTURA_MASIVA)
            {
                periodo = generarMesFacturacion(1);
            }
            else
            {
                periodo = generarMesFacturacion(paramPeriodoFact);
            }

            Conexion conexion = new Conexion();
            try
            {
                SqlCommand command = new SqlCommand();
                command.Connection = conexion.conexion;
                command.CommandType = CommandType.Text;
                command.CommandText = Util.Util.CREAR_REGISTRO_NUEVO_PERIODO;
                //command.Parameters.AddWithValue("@GENERAL", "MASIVA");
                command.Parameters.AddWithValue("@GENERAL", tipoFactura);
                command.Parameters.AddWithValue("@CONTADORI", ContadorFinal + 1);
                command.Parameters.AddWithValue("@CONTADORF", 0);
                command.Parameters.AddWithValue("@PERIODO", periodo);
                command.Parameters.AddWithValue("@ESTADO", "N");
                command.Parameters.AddWithValue("@CLAVE", clave);
                command.Parameters.AddWithValue("@FECHA", DateTime.Now);
                command.Parameters.AddWithValue("@DESCRIPCION", "");

                if (conexion.ConectarServer())
                {
                    int recordsAffected = command.ExecuteNonQuery();
                    if (recordsAffected == 1)
                    {
                        fctCorr.TipoFactura = tipoFactura;
                        // No se le agrega+1 ya que le procedimiento almacenado realiza un ROW_NUMBER() desde le contador Final
                        fctCorr.contadorInicial = ContadorFinal;
                        fctCorr.ContadorFinal = 0;
                        fctCorr.Estado = "N";
                        fctCorr.Clave = clave.ToString();
                        fctCorr.Periodo = Convert.ToDecimal(periodo);
                        fctCorr.Descripcion = "Registro Nuevo";
                    }
                }
                ContadorFinal = 0;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                conexion.Close();
            }
            return fctCorr;
        }

    }
}
