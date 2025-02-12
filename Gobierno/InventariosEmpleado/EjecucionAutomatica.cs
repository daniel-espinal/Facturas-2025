using FacturacionGobierno.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Xpo.DB.Helpers;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using System.Data.SqlClient;
using System.Data.Sql;
using DevExpress.XtraSplashScreen;
using DevExpress.Data.Filtering;
using FacturacionGobierno.S390DataSetTableAdapters;
using FacturacionGobierno.Properties;
using System.IO;
using System.Threading;




namespace FacturacionGobierno
{
    public partial class EjecucionAutomatica : Form
    {
        private String vPeriodo = "";
        private Boolean vFlag = false;
        public EjecucionAutomatica(String vPeriodo, Boolean vFlag)
        {
            InitializeComponent();

            this.vPeriodo = vPeriodo;
            this.vFlag = vFlag;
        }


        private void EjecucionAutomatica_Load(object sender, EventArgs e)
        {
            string vPeriodo = generarMesFacturacionNew(0);
            Conexion vConexion = new Conexion();
            Boolean vExistePeriodo1 = false;
            String vClaves = "[EEHAviso_Masivo_General] 4";
            DataTable vClavesnuevo = vConexion.obtenerDataTable(vClaves);
            string vSector = string.Empty;
            if (vClavesnuevo.Rows.Count > 0)
            {
                //GENERA GOBIERNO
                {
                    string vPathnG = @"\\192.168.100.8\\Facturas\\Facturas\\GOBIERNO\\" + vPeriodo + "\\{0}";
                    vPathnG = string.Format(vPathnG, DateTime.Now.Year + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00") + "\\");
                    if (!System.IO.Directory.Exists(vPathnG))
                        System.IO.Directory.CreateDirectory(vPathnG);
                    //FacturaCorrelativo facturaCorrelativo = new FacturaCorrelativo();
                    //string periodoFacturacion = facturaCorrelativo.generarMesFacturacion(0);
                    String vClaves1 = "[EEHAviso_Gobierno_Generales] 5";
                    DataTable vClavesnuevo1 = vConexion.obtenerDataTable(vClaves1);
                    if (vClavesnuevo1.Rows.Count > 0)
                    {
                        for (int i = 0; i < vClavesnuevo1.Rows.Count; i++)
                        {
                            string prefijoG = "";
                            string clave = "";
                            string vNombreInstitucionG = "";
                            string VcodigoAgrupacion = "";
                            VcodigoAgrupacion = "[EEHAviso_Gobierno_Generales] 6," + vClavesnuevo1.Rows[i]["codAgrupacion"].ToString();
                            DataTable vAgrupacion = vConexion.obtenerDataTable(VcodigoAgrupacion);
                            prefijoG = vClavesnuevo1.Rows[i]["codAgrupacion"].ToString();
                            clave = vAgrupacion.Rows[0]["Clave"].ToString();

                            string vQuer1 = "[EEH_Gobierno_Generales] 8" + vAgrupacion;
                            DataTable VNUMEROSUMAR = vConexion.obtenerDataTable(vQuer1);
                            int myNum = int.Parse(VNUMEROSUMAR.Rows[0][0].ToString());
                            int vContador = ObtenerCorrelativoN(vConexion, generarMesFacturacionNew(0), generarMesFacturacionNew(1), ref vExistePeriodo1);
                            int vContador1 = vContador + myNum + 1;
                            vNombreInstitucionG = vAgrupacion.Rows[0]["NomIns"].ToString();
                            vNombreInstitucionG = vNombreInstitucionG.Replace('/', ' ');
                            vNombreInstitucionG = vNombreInstitucionG.Replace('|', ' ');
                            vNombreInstitucionG = vNombreInstitucionG.Replace('"', ' ');
                            vNombreInstitucionG = vNombreInstitucionG.Replace('\'', ' ');
                            vNombreInstitucionG = vNombreInstitucionG.Replace(':', ' ');
                            string vPathDocumentoG = vPathnG + prefijoG + "_" + vNombreInstitucionG + ".pdf";
                            //AvisoGobierno agob = new AvisoGobierno();
                            try
                            {
                                GeneracionDiariaGobierno FacturaEspecial = new GeneracionDiariaGobierno();
                                FacturaEspecial.Parameters["Tipo"].Value = 4;
                                FacturaEspecial.Parameters["mes_facturacion"].Value = vPeriodo;
                                FacturaEspecial.Parameters["clave"].Value = prefijoG;
                                FacturaEspecial.Parameters["contador_factura"].Value = vContador; //enviar la siguiente
                                FacturaEspecial.CreateDocument();
                                FacturaEspecial.ExportToPdf(vPathDocumentoG);
                                FacturaEspecial.Dispose();
                                string vQueryFacturas = "[EEHAviso_Gob_Ciclo_Actual_D] " + 0 + "," + vPeriodo + "," + vAgrupacion.Rows[0]["codAgrupacion"].ToString() + "," + 4;
                                vQueryFacturas = String.Format(vQueryFacturas, prefijoG);
                                DataTable vDatosFacturas = vConexion.obtenerDataTable(vQueryFacturas);
                                string vClavesFacturadas = string.Empty;
                                if (vDatosFacturas.Rows.Count > 0)
                                {
                                    for (int x = 0; x < vDatosFacturas.Rows.Count; x++)
                                    {
                                        vClavesFacturadas += vDatosFacturas.Rows[x]["m_clave_primaria"].ToString() + ",";
                                    }
                                }
                                vClavesFacturadas = vClavesFacturadas.Remove(vClavesFacturadas.Length - 1, 1);
                                string vQueryGuardarClaves = " [EEHAviso_Masivo_General]  18, '{0}', '{1}', '{2}'";
                                vQueryGuardarClaves = string.Format(vQueryGuardarClaves, vPeriodo, vDatosFacturas.Rows[0]["PREFIJO"].ToString(), vClavesFacturadas);
                                vConexion.obtenerDataTable(vQueryGuardarClaves);
                                String vQuerDetalleMasivo = "[EEHAviso_Masivo_General] 17," + vAgrupacion.Rows[0]["codAgrupacion"].ToString();
                                vConexion.obtenerDataTable(vQuerDetalleMasivo);
                            }
                            catch (Exception Ex)
                            {
                                Console.WriteLine(Ex.Message);
                                genericos.Log("FACTURACION GOBIERNO ", Ex.Message, "");
                            }
                        }
                    }
                }

                //GENERA TODO LOS GC
                {
                    String vClaves1 = "[EEHAltos_Consumidores_Generales] 9";
                    DataTable vClavesnuevo1 = vConexion.obtenerDataTable(vClaves1);
                    string vAgrupa1 = string.Empty;
                    string vNombreInstitucion1 = string.Empty;

                    string targetPath = @"\\192.168.100.8\\centralizada\\FISICO\\" + vPeriodo + "\\GRANDESCLIENTES" + "\\{0}";
                    targetPath = string.Format(targetPath, DateTime.Now.Year + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00") + "\\");
                    if (!System.IO.Directory.Exists(targetPath))
                        System.IO.Directory.CreateDirectory(targetPath);
                    string targetPathc = @"\\192.168.100.8\\centralizada\\FISICO\\" + vPeriodo + "\\CORPORATIVOS" + "\\{0}";
                    targetPathc = string.Format(targetPathc, DateTime.Now.Year + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00") + "\\");
                    if (!System.IO.Directory.Exists(targetPathc))
                        System.IO.Directory.CreateDirectory(targetPathc);

                    for (int i = 0; i < vClavesnuevo1.Rows.Count; i++)
                    {
                        if (vClavesnuevo1.Rows.Count > 0) //&& i < 5)
                        {
                            Boolean vExistePeriodo = false;

                            string vCodigoAgrupacion1 = vClavesnuevo1.Rows[i]["codAgrupacion"].ToString();
                            string vQueryDetalle1 = "[EEHAltos_Consumidores_Generales] 10,'" + vCodigoAgrupacion1 + "'";
                            DataTable vDatosDetalle1 = vConexion.obtenerDataTable(vQueryDetalle1);
                            try
                            {
                                if (vDatosDetalle1.Rows.Count > 0)
                                {
                                    int vContador = ObtenerCorrelativo(vConexion, generarMesFacturacionNew(0), generarMesFacturacionNew(1), ref vExistePeriodo);
                                    int vContadorPeriodo = vContador;
                                    vAgrupa1 = vClavesnuevo1.Rows[i]["codAgrupacion"].ToString().PadLeft(4, '0');
                                    vNombreInstitucion1 = vClavesnuevo1.Rows[i]["NomIns"].ToString().Trim();
                                    vNombreInstitucion1 = vNombreInstitucion1.Replace('/', ' ');
                                    vNombreInstitucion1 = vNombreInstitucion1.Replace('|', ' ');
                                    vNombreInstitucion1 = vNombreInstitucion1.Replace('"', ' ');
                                    vNombreInstitucion1 = vNombreInstitucion1.Replace('\'', ' ');
                                    vNombreInstitucion1 = vNombreInstitucion1.Replace(':', ' ');
                                    if (vClavesnuevo1.Rows[i]["IdMerCliente"].ToString() == "5" || vClavesnuevo1.Rows[i]["IdMerCliente"].ToString() == "6")
                                    //   if (vClavesnuevo1.Rows[i]["IdMerCliente"].ToString() == "6")
                                    {
                                        string vPathDocumentoGG = targetPath + vAgrupa1 + "_" + vNombreInstitucion1 + ".pdf";
                                        AltosConsumidoresTodos FacturasAltosConsumidores1 = new AltosConsumidoresTodos();
                                        FacturasAltosConsumidores1.Parameters["tipo"].Value = 1;
                                        FacturasAltosConsumidores1.Parameters["codigoAgrupa"].Value = vAgrupa1;
                                        FacturasAltosConsumidores1.Parameters["contador_factura"].Value = vContador; //enviar la siguiente
                                        FacturasAltosConsumidores1.CreateDocument();
                                        FacturasAltosConsumidores1.ExportToPdf(vPathDocumentoGG);
                                        FacturasAltosConsumidores1.Dispose();
                                        String vQuerDetalleMasivo = "[EEHAltos_Consumidores_Generales] 11," + vCodigoAgrupacion1;
                                        vConexion.obtenerDataTable(vQuerDetalleMasivo);
                                        string vQueryFacturas = "[EEHAltos_Consumidores_TD_Mercados] 2,{0}";
                                        vQueryFacturas = String.Format(vQueryFacturas, vAgrupa1);
                                        DataTable vDatosFacturas = vConexion.obtenerDataTable(vQueryFacturas);
                                        string vClavesFacturadas = string.Empty;
                                        if (vDatosFacturas.Rows.Count > 0)
                                        {
                                            for (int x = 0; x < vDatosFacturas.Rows.Count; x++)
                                            {
                                                vClavesFacturadas += vDatosFacturas.Rows[x]["clave"].ToString() + ",";
                                            }
                                            vClavesFacturadas = vClavesFacturadas.Remove(vClavesFacturadas.Length - 1, 1);
                                            string vQueryGuardarClaves = " [EEHAviso_Masivo_General]  6, '{0}', '{1}', '{2}'";
                                            vQueryGuardarClaves = string.Format(vQueryGuardarClaves, vPeriodo, vDatosFacturas.Rows[0]["PREFIJO"].ToString(), vClavesFacturadas);
                                            vConexion.obtenerDataTable(vQueryGuardarClaves);
                                        }
                                        if (vExistePeriodo)
                                        {
                                            string vQueryInsertarContador = "[EEHInsert_Fact_Correlativo_Altos] 2, '','','{0}','{1}','{2}','{3}','{4}'"; // utilizar para unica 
                                            vQueryInsertarContador = string.Format(vQueryInsertarContador, "ALTOS_MENSUAL", generarMesFacturacionNew(0), vContadorPeriodo, vContador, 0);
                                            DataTable vDatosInsert = vConexion.obtenerDataTable(vQueryInsertarContador);
                                        }

                                    }
                                    else
                                    {
                                        string vPathDocumentoGG = targetPathc + vAgrupa1 + "_" + vNombreInstitucion1 + ".pdf";
                                        AltosConsumidoresTodos FacturasAltosConsumidores1 = new AltosConsumidoresTodos();
                                        FacturasAltosConsumidores1.Parameters["tipo"].Value = 3;
                                        FacturasAltosConsumidores1.Parameters["codigoAgrupa"].Value = vAgrupa1;
                                        FacturasAltosConsumidores1.Parameters["contador_factura"].Value = vContador; //enviar la siguiente
                                        FacturasAltosConsumidores1.CreateDocument();
                                        FacturasAltosConsumidores1.ExportToPdf(vPathDocumentoGG);
                                        FacturasAltosConsumidores1.Dispose();
                                        String vQuerDetalleMasivo = "[EEHAltos_Consumidores_Generales] 11," + vCodigoAgrupacion1;
                                        vConexion.obtenerDataTable(vQuerDetalleMasivo);
                                        string vQueryFacturas = "[EEHAltos_Consumidores_TD_Mercados] 2,{0}";
                                        vQueryFacturas = String.Format(vQueryFacturas, vAgrupa1);
                                        DataTable vDatosFacturas = vConexion.obtenerDataTable(vQueryFacturas);
                                        string vClavesFacturadas = string.Empty;
                                        if (vDatosFacturas.Rows.Count > 0)
                                        {
                                            for (int x = 0; x < vDatosFacturas.Rows.Count; x++)
                                            {
                                                vClavesFacturadas += vDatosFacturas.Rows[x]["clave"].ToString() + ",";
                                            }
                                            vClavesFacturadas = vClavesFacturadas.Remove(vClavesFacturadas.Length - 1, 1);
                                            string vQueryGuardarClaves = " [EEHAviso_Masivo_General]  6, '{0}', '{1}', '{2}'";
                                            vQueryGuardarClaves = string.Format(vQueryGuardarClaves, vPeriodo, vDatosFacturas.Rows[0]["PREFIJO"].ToString(), vClavesFacturadas);
                                            vConexion.obtenerDataTable(vQueryGuardarClaves);
                                        }
                                        if (vExistePeriodo)
                                        {
                                            string vQueryInsertarContador = "[EEHInsert_Fact_Correlativo_Altos] 2, '','','{0}','{1}','{2}','{3}','{4}'"; // utilizar para unica 
                                            vQueryInsertarContador = string.Format(vQueryInsertarContador, "ALTOS_MENSUAL", generarMesFacturacionNew(0), vContadorPeriodo, vContador, 0);
                                            DataTable vDatosInsert = vConexion.obtenerDataTable(vQueryInsertarContador);
                                        }

                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                genericos.Log("FACTURACION ALTOS CONSUMIDORES ", ex.Message, "");
                            }


                        }
                    }
                }
                try
                {
                    string[] filePaths1 = Directory.GetFiles(@"\\192.168.100.59\\e\\ArchivosAdjuntos\\Facturas\\GRANDESCLIENTES");
                    foreach (string filePath1 in filePaths1)
                        File.Delete(filePath1);
                    //copia facturas de altos consumidores en 100.8 facturas
                    string fuente = @"\\192.168.100.8\Centralizada\FISICO\" + vPeriodo + "\\GRANDESCLIENTES" + "\\{0}";
                    // string fuente = @"C:\centralizada01\\centralizada\\FISICO\\" + vPeriodo + "\\GRANDESCLIENTES" + "\\{0}";
                    fuente = string.Format(fuente, DateTime.Now.Year + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00"));
                    string destino = (@"\\192.168.100.59\\e\\ArchivosAdjuntos\\Facturas\\GRANDESCLIENTES\\");
                    DirectoryInfo dl = new DirectoryInfo(fuente);
                    if (File.Exists(fuente))
                    {
                        File.Copy(fuente, destino, true);
                    }
                    fuente = @"\\192.168.100.8\Centralizada\FISICO\" + vPeriodo + "\\GRANDESCLIENTES" + "\\{0}";
                    fuente = string.Format(fuente, DateTime.Now.Year + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00"));
                    destino = (@"\\192.168.100.59\\e\\ArchivosAdjuntos\\Facturas\\GRANDESCLIENTES\\");
                    FileInfo[] directorios = null;
                    directorios = dl.GetFiles("*", SearchOption.AllDirectories);
                    string rutaExtendida = "";

                    foreach (FileInfo docs in directorios)
                    {
                        rutaExtendida = docs.DirectoryName.Replace(fuente, "");
                        if (!Directory.Exists(destino + rutaExtendida))
                            Directory.CreateDirectory(destino);
                        File.Copy(docs.FullName, destino + "\\" + docs.Name, true);
                    }


                }
                catch (Exception EX)
                {
                    Console.WriteLine(EX.Message);
                    genericos.Log("COPIA ALTOS CONSUMIDORES GRANDE CLIENTES ", EX.Message, "");
                }
                try
                {

                    vPeriodo = generarMesFacturacionNew(0);
                    string[] filePaths1 = Directory.GetFiles(@"\\192.168.100.59\\e\\ArchivosAdjuntos\\Facturas\\CORPORATIVOS");
                    foreach (string filePath1 in filePaths1)
                        File.Delete(filePath1);
                    //copia facturas de altos consumidores en 100.8 facturas

                    string fuente = @"\\192.168.100.8\\centralizada\\FISICO\\" + vPeriodo + "\\CORPORATIVOS" + "\\{0}";
                    fuente = string.Format(fuente, DateTime.Now.Year + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00"));
                    string destino = (@"\\192.168.100.59\\e\\ArchivosAdjuntos\\Facturas\\CORPORATIVOS\\");
                    DirectoryInfo dl = new DirectoryInfo(fuente);
                    if (File.Exists(fuente))
                    {
                        File.Copy(fuente, destino, true);
                    }


                    fuente = @"\\192.168.100.8\\centralizada\\FISICO\\" + vPeriodo + "\\CORPORATIVOS" + "\\{0}";
                    fuente = string.Format(fuente, DateTime.Now.Year + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00"));
                    destino = (@"\\192.168.100.59\\e\\ArchivosAdjuntos\\Facturas\\CORPORATIVOS\\");
                    FileInfo[] directorios = null;
                    directorios = dl.GetFiles("*", SearchOption.AllDirectories);
                    string rutaExtendida = "";

                    foreach (FileInfo docs in directorios)
                    {
                        rutaExtendida = docs.DirectoryName.Replace(fuente, "");
                        if (!Directory.Exists(destino + rutaExtendida))
                            Directory.CreateDirectory(destino);
                        File.Copy(docs.FullName, destino + "\\" + docs.Name, true);
                    }
                }
                catch (Exception EX)
                {
                    Console.WriteLine(EX.Message);
                    genericos.Log("COPIA ALTOS CONSUMIDORES CORPORATIVOS ", EX.Message, "");

                }
                {
                    String vCODIGOERROR = "";
                    try
                    {
                        String vQuery1 = "[EEHAltos_Consumidores_Generales_Correo] 3";
                        DataTable vDatosCorreo = vConexion.obtenerDataTable(vQuery1);
                        for (int i = 0; i < vDatosCorreo.Rows.Count; i++)
                        {
                            if (vDatosCorreo.Rows[i]["IdMerCliente"].ToString() == "5" || vDatosCorreo.Rows[i]["IdMerCliente"].ToString() == "6")
                            //  if (vDatosCorreo.Rows[i]["IdMerCliente"].ToString() == "6")
                            {
                                string vPDF = "";
                                string vCodigoAgrupacionEEH = vDatosCorreo.Rows[i]["codAgrupacion"].ToString().PadLeft(4, '0');
                                string vNombreInstitucion = vDatosCorreo.Rows[i]["NomIns"].ToString().Trim();
                                vCODIGOERROR = vCodigoAgrupacionEEH;
                                vPDF = vCodigoAgrupacionEEH + "_" + vNombreInstitucion + ".pdf";
                                Object[] vDatos = new object[3];
                                vDatos[0] = vDatosCorreo.Rows[i]["codAgrupacion"].ToString();
                                vDatos[1] = "Facturas\\GRANDESCLIENTES\\" + vPDF; //adjuntos
                                vDatos[2] = "Hector.Valerio";
                                string vCodigoResult = "", vMensajeResult = "";
                                vConexion.notificarFacturacionALTOS(ref vCodigoResult, ref vMensajeResult, vDatos);
                                Console.WriteLine("=======================================   vCodigoAgrupacionEEH    " + vCodigoAgrupacionEEH + "             ====================================");
                                Console.WriteLine(vCodigoResult);
                                Console.WriteLine(vMensajeResult);
                                Console.WriteLine("===============================================================================================================");
                            }
                            else
                            {
                                string vPDF = "";

                                string vCodigoAgrupacionEEH = vDatosCorreo.Rows[i]["codAgrupacion"].ToString().PadLeft(4, '0');
                                string vNombreInstitucion = vDatosCorreo.Rows[i]["NomIns"].ToString().Trim();
                                vCODIGOERROR = vCodigoAgrupacionEEH;
                                vPDF = vCodigoAgrupacionEEH + "_" + vNombreInstitucion + ".pdf";
                                Object[] vDatos = new object[3];
                                vDatos[0] = vDatosCorreo.Rows[i]["codAgrupacion"].ToString();
                                vDatos[1] = "Facturas\\CORPORATIVOS\\" + vPDF; //adjuntos
                                vDatos[2] = "Hector.Valerio";
                                string vCodigoResult = "", vMensajeResult = "";
                                vConexion.notificarFacturacionALTOS(ref vCodigoResult, ref vMensajeResult, vDatos);
                                Console.WriteLine("=======================================   vCodigoAgrupacionEEH    " + vCodigoAgrupacionEEH + "             ====================================");
                                Console.WriteLine(vCodigoResult);
                                Console.WriteLine(vMensajeResult);
                                Console.WriteLine("===============================================================================================================");
                            }

                        }
                    }
                    catch (Exception EX)
                    {
                        Console.WriteLine(EX.Message);
                        genericos.Log("ENVIA POR CORREO ALTOS CONSUMIDORES ", EX.Message, "");
                    }
                }
                //FACTURAS IRREGULARIDAD
                //{
                //    string vQueryDetallei1 = "[EEHAviso_Masivo_General] 25";
                //    DataTable vDatosDetallei1 = vConexion.obtenerDataTable(vQueryDetallei1);
                //    try
                //    {

                //        if (vDatosDetallei1.Rows.Count > 0)
                //            for (int i = 0; i < vDatosDetallei1.Rows.Count; i++)
                //            {
                //                string vClavei = vDatosDetallei1.Rows[i]["Clave"].ToString().PadLeft(4, '0');
                //                string Nombrei = vDatosDetallei1.Rows[i]["m_nombre_abonado"].ToString().Trim();
                //                Nombrei = Nombrei.Replace('/', ' ');
                //                Nombrei = Nombrei.Replace('|', ' ');
                //                Nombrei = Nombrei.Replace('"', ' ');
                //                Nombrei = Nombrei.Replace('\'', ' ');
                //                Nombrei = Nombrei.Replace(':', ' ');

                //                string Liquidacion = vDatosDetallei1.Rows[i]["id_liquidacion"].ToString();
                //                string SectorEEH = vDatosDetallei1.Rows[i]["SECTOR_ID"].ToString();
                //                string VSector = vDatosDetallei1.Rows[i]["SECTOR_EEH"].ToString();
                //                string VOs = vDatosDetallei1.Rows[i]["Os"].ToString();

                //                string vPath = @"\\192.168.100.8\centralizada\Irregularidad\" + vPeriodo + "\\" + VSector + "\\{0}";
                //                // string vPath = @"C:\facturas\\Irregularidad\\" + vPeriodo +"\\" + VSector + "\\{0}";
                //                //\\192.168.100.8\Centralizada\IRREGULARIDAD\2007\COMAYAGUA\20200703
                //                vPath = string.Format(vPath, DateTime.Now.Year + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00") + "\\");
                //                if (!System.IO.Directory.Exists(vPath))
                //                    System.IO.Directory.CreateDirectory(vPath);

                //                string vPathn = @"\\192.168.100.59\\f\\irregularidades\\adjuntos\\";
                //                if (!System.IO.Directory.Exists(vPathn))
                //                    System.IO.Directory.CreateDirectory(vPathn);

                //                string vPathDocumentoGG = vPath + SectorEEH + "_" + Liquidacion + "_" + vClavei + "_" + Nombrei + ".pdf";
                //                string vPathDocumentoIR = vPathn + "FECN" + "_" + vClavei + "_" + Liquidacion + ".pdf";
                //                FacturasEEHIrregularidad FacturasMasivoCorreo = new FacturasEEHIrregularidad();
                //                FacturasMasivoCorreo.Parameters["obtener_todos"].Value = 1;
                //                FacturasMasivoCorreo.Parameters["clave_primaria"].Value = Liquidacion;
                //                FacturasMasivoCorreo.CreateDocument();
                //                FacturasMasivoCorreo.ExportToPdf(vPathDocumentoGG);
                //                FacturasMasivoCorreo.ExportToPdf(vPathDocumentoIR);
                //                FacturasMasivoCorreo.Dispose();
                //                String vQuerDetalleMasivo = "[EEHAviso_Masivo_General] 26," + Liquidacion;
                //                vConexion.obtenerDataTable(vQuerDetalleMasivo);
                //                Object[] vDatos = new object[40];
                //                vDatos[0] = VOs;
                //                vDatos[1] = Liquidacion;
                //                vDatos[2] = "FECN" + "_" + vClavei + "_" + Liquidacion + ".pdf";
                //                vDatos[3] = 54;
                //                vDatos[4] = ".pdf";
                //                vDatos[5] = 0;
                //                vDatos[6] = 0;
                //                vDatos[7] = "FACTURA ECNF";
                //                vDatos[8] = null;
                //                vDatos[9] = "appsoe";
                //                vDatos[10] = 1;
                //                vDatos[11] = 0;

                //                string vCodigoResult = "", vMensajeResult = "";
                //                vConexion.vRegistrarFactura(ref vCodigoResult, ref vMensajeResult, vDatos);
                //                Console.WriteLine("=======================================   vCodigoAgrupacionEEH    " + vClavei + "             ====================================");
                //                Console.WriteLine(vCodigoResult);
                //                Console.WriteLine(vMensajeResult);
                //                Console.WriteLine("===============================================================================================================");

                //            }
                //    }
                //    catch (Exception ex)
                //    {
                //        Console.WriteLine(ex.Message);
                //        genericos.Log("GENERA CLAVES IRREGULARIDAD ", ex.Message, "");
                //    }
                //}
                //COPIA TODAS LAS FACTURAS PARA ENVIAR POR CORREO
                //{
                //    //GENERA TODO LOS GC de Irregularidad 
                //    try
                //    {
                //        String vClavesIrregularidad = "[EEHAltos_Consumidores_Generales] 8";
                //        DataTable vClavesnuevoirregularidad = vConexion.obtenerDataTable(vClavesIrregularidad);
                //        string vAgrupa1 = string.Empty;
                //        string vNombreIrregularidad = string.Empty;
                //        vNombreIrregularidad = vNombreIrregularidad.Replace('/', ' ');
                //        vNombreIrregularidad = vNombreIrregularidad.Replace('|', ' ');
                //        vNombreIrregularidad = vNombreIrregularidad.Replace('"', ' ');
                //        vNombreIrregularidad = vNombreIrregularidad.Replace('\'', ' ');
                //        vNombreIrregularidad = vNombreIrregularidad.Replace(':', ' ');
                //        for (int i = 0; i < vClavesnuevoirregularidad.Rows.Count; i++)
                //        {
                //            if (vClavesnuevoirregularidad.Rows.Count > 0) //&& i < 5)
                //            {
                //                string targetPathirre = @"\\192.168.100.8\facturas\IRREGULARIDAD\" + vPeriodo + "\\{0}";
                //                targetPathirre = string.Format(targetPathirre, DateTime.Now.Year + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00") + "\\");
                //                if (!System.IO.Directory.Exists(targetPathirre))
                //                    System.IO.Directory.CreateDirectory(targetPathirre);
                //                string vCodigoAgrupacionir1 = vClavesnuevoirregularidad.Rows[i]["CodAgrupacion"].ToString();
                //                string vQueryirregularidad = "[EEHAltos_Consumidores_Generales] 12,'" + vCodigoAgrupacionir1 + "'";
                //                DataTable vDatosIrregularidad = vConexion.obtenerDataTable(vQueryirregularidad);
                //                if (vDatosIrregularidad.Rows.Count > 0)
                //                {
                //                    vAgrupa1 = vClavesnuevoirregularidad.Rows[i]["CodAgrupacion"].ToString().PadLeft(4, '0');
                //                    vNombreIrregularidad = vClavesnuevoirregularidad.Rows[i]["NomIns"].ToString().Trim();
                //                    string Liquidacion = vClavesnuevoirregularidad.Rows[i]["id_liquidacion"].ToString();
                //                    vNombreIrregularidad = vNombreIrregularidad.Replace('/', ' ');
                //                    vNombreIrregularidad = vNombreIrregularidad.Replace('|', ' ');
                //                    vNombreIrregularidad = vNombreIrregularidad.Replace('"', ' ');
                //                    vNombreIrregularidad = vNombreIrregularidad.Replace('\'', ' ');
                //                    vNombreIrregularidad = vNombreIrregularidad.Replace(':', ' ');
                //                    string vPathDocumentoGG = targetPathirre + vAgrupa1 + "_" + vNombreIrregularidad + ".pdf";
                //                    EEHFacturaIrregularidadAgrupa Facturasirregularidad = new EEHFacturaIrregularidadAgrupa();
                //                    Facturasirregularidad.Parameters["obtener_todos"].Value = 3;
                //                    Facturasirregularidad.Parameters["ID_Liquidacion"].Value = 1;
                //                    Facturasirregularidad.Parameters["codigoAgrupa"].Value = vAgrupa1;
                //                    Facturasirregularidad.ExportToPdf(vPathDocumentoGG);
                //                    Facturasirregularidad.Dispose();
                //                    String vQuerDetalleIrregularidad = "[EEHAltos_Consumidores_Generales] 13," + vAgrupa1;
                //                    vConexion.obtenerDataTable(vQuerDetalleIrregularidad);
                //                }

                //            }
                //        }
                //        if (vClavesnuevoirregularidad.Rows.Count > 0) //
                //        {
                //            try
                //            {

                //                vPeriodo = generarMesFacturacionNew(0);
                //                string[] filePaths1irregularidad = Directory.GetFiles(@"\\192.168.100.59\\ArchivosAdjuntos\\Envio_Irregularidad\\");
                //                foreach (string filePath1 in filePaths1irregularidad)
                //                    File.Delete(filePath1);
                //                //copia facturas de irregularidades que tienen correo en 100.8 facturas
                //                string fuenteirregularidad = @"\\192.168.100.8\facturas\IRREGULARIDAD\" + vPeriodo + "\\{0}";
                //                fuenteirregularidad = string.Format(fuenteirregularidad, DateTime.Now.Year + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00"));
                //                string destino = (@"\\192.168.100.59\\ArchivosAdjuntos\\Envio_Irregularidad\\");
                //                DirectoryInfo dl = new DirectoryInfo(fuenteirregularidad);
                //                if (File.Exists(fuenteirregularidad))
                //                {
                //                    File.Copy(fuenteirregularidad, destino, true);
                //                }
                //                destino = (@"\\192.168.100.59\ArchivosAdjuntos\Envio_Irregularidad\");
                //                FileInfo[] directorios = null;
                //                directorios = dl.GetFiles("*", SearchOption.AllDirectories);
                //                string rutaExtendida = "";
                //                foreach (FileInfo docs in directorios)
                //                {
                //                    rutaExtendida = docs.DirectoryName.Replace(fuenteirregularidad, "");
                //                    if (!Directory.Exists(destino + rutaExtendida))
                //                        Directory.CreateDirectory(destino);
                //                    File.Copy(docs.FullName, destino + "\\" + docs.Name, true);
                //                }
                //            }
                //            catch (Exception EX)
                //            {
                //                Console.WriteLine(EX.Message );
                //                genericos.Log("COPIA FACTURAS DE IRREGULARIDAD ", EX.Message, "");
                //            }


                //            //ENVIA FACTURA POR CORREOS CLIENTES CON IRREGULARIDAD
                //            {
                //                String vCODIGOERRORIrre = "";
                //                vPeriodo = generarMesFacturacionNew(0);
                //                try
                //                {
                //                    String vQueryIrregularidadCorreo = "[EEH_Masivos_Generales_Correo] 2";
                //                    DataTable vDatosCorreoI = vConexion.obtenerDataTable(vQueryIrregularidadCorreo);
                //                    for (int i = 0; i < vDatosCorreoI.Rows.Count; i++)
                //                    {
                //                        string vPDF = "";
                //                        string vClaveEEHIrregularidad = vDatosCorreoI.Rows[i]["Codagrupacion"].ToString().PadLeft(4, '0');
                //                        string vNombreInstitucionIrregularidad = vDatosCorreoI.Rows[i]["NomIns"].ToString().Trim();
                //                        vCODIGOERRORIrre = vClaveEEHIrregularidad;
                //                        vPDF = vClaveEEHIrregularidad + "_" + vNombreInstitucionIrregularidad + ".pdf";
                //                        Object[] vDatos = new object[3];
                //                        vDatos[0] = vDatosCorreoI.Rows[i]["Codagrupacion"].ToString();
                //                        vDatos[1] = "Envio_Irregularidad\\" + vPDF; //adjuntos
                //                        vDatos[2] = "Celvin.Diaz";
                //                        string vCodigoResult = "", vMensajeResult = "";
                //                        vConexion.notificarIrregularidad(ref vCodigoResult, ref vMensajeResult, vDatos);
                //                        Console.WriteLine("=======================================   vCodigoAgrupacionEEH    " + vClaveEEHIrregularidad + "             ====================================");
                //                        Console.WriteLine(vCodigoResult);
                //                        Console.WriteLine(vMensajeResult);
                //                        Console.WriteLine("===============================================================================================================");
                //                    }
                //                }
                //                catch (Exception EX)
                //                {
                //                    Console.WriteLine(EX.Message);
                //                    genericos.Log("ENVIA FACTURAS IRREGULARIDAD ", EX.Message, "");

                //                }
                //            }
                //        }
                //    }
                //    catch (Exception ex)
                //    {
                //        Console.WriteLine(ex.Message);
                //        genericos.Log("FACTURACION ", ex.Message, "");
                //    }
                //}
                {

                    //GENERA REPARTO ALTOS CONSUMIDOR
                    String vQueryoObtenerSectores = "[EEHAviso_Masivo_General] 36";
                    DataTable vDatosSectores = vConexion.obtenerDataTable(vQueryoObtenerSectores);
                    try
                    {
                        if (vDatosSectores.Rows.Count > 0)
                            for (int i = 0; i < vDatosSectores.Rows.Count; i++)
                            {
                                Boolean vExistePeriodo = false;
                                int vContador = ObtenerCorrelativoN(vConexion, generarMesFacturacionNew(0), generarMesFacturacionNew(1), ref vExistePeriodo);
                                int vContadorPeriodo = vContador;
                                string vClave = vDatosSectores.Rows[i]["Clave"].ToString().PadLeft(4, '0');
                                string Nombre = vDatosSectores.Rows[i]["m_nombre_abonado"].ToString();
                                Nombre = Nombre.Replace('/', ' ');
                                Nombre = Nombre.Replace('|', ' ');
                                Nombre = Nombre.Replace('"', ' ');
                                Nombre = Nombre.Replace('\'', ' ');
                                Nombre = Nombre.Replace(':', ' ');

                                string SectorEEHReparto = vDatosSectores.Rows[i]["SECTOR_ID"].ToString();
                                string VSectorReparto = vDatosSectores.Rows[i]["SECTOR_EEH"].ToString();
                                string vPath = @"\\192.168.100.8\\centralizada\\\REPARTOFACTURAS\\" + vPeriodo + "\\" + VSectorReparto + "\\{0}";

                                vPath = string.Format(vPath, DateTime.Now.Year + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00") + "\\");
                                if (!System.IO.Directory.Exists(vPath))
                                    System.IO.Directory.CreateDirectory(vPath);
                                string vPathRepartoAltos = vPath + vClave + "_" + Nombre + ".pdf";
                                FactRepartoAltosConsumidores FacturasAltosConsumidoresReparto = new FactRepartoAltosConsumidores();
                                FacturasAltosConsumidoresReparto.Parameters["tipo"].Value = 6;
                                FacturasAltosConsumidoresReparto.Parameters["codigoAgrupa"].Value = vClave;
                                FacturasAltosConsumidoresReparto.Parameters["contador_factura"].Value = vContador;
                                FacturasAltosConsumidoresReparto.CreateDocument();
                                FacturasAltosConsumidoresReparto.ExportToPdf(vPathRepartoAltos);
                                FacturasAltosConsumidoresReparto.Dispose();
                                String vQuerDetalleGReparto = "[EEHAviso_Masivo_General] 37," + vClave;
                                vConexion.obtenerDataTable(vQuerDetalleGReparto);
                            }

                    }
                    catch (Exception Ex)
                    {
                        Console.WriteLine(Ex.Message);
                        genericos.Log("FACTURAS ALTOS CONSUMIODRES QUE SE ENTREGAN POR REPARTO ", Ex.Message, "");
                    }

                    // GENERA MASIVO QUE ANTES ERA UN ALTOS CONSUMIDOR
                    String vClavesmasivo = "[EEHAltos_Consumidores_Generales] 14";
                    DataTable vClavesmasivonuevo = vConexion.obtenerDataTable(vClavesmasivo);
                    string vclave = string.Empty;
                    string vnombremasivo = string.Empty;
                    string pathMasivo = @"\\192.168.100.8\\Facturas\\Masivo\\" + vPeriodo + "\\{0}";
                    pathMasivo = string.Format(pathMasivo, DateTime.Now.Year + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00") + "\\");
                    if (!System.IO.Directory.Exists(pathMasivo))
                        System.IO.Directory.CreateDirectory(pathMasivo);
                    for (int i = 0; i < vClavesmasivonuevo.Rows.Count; i++)
                    {
                        if (vClavesmasivonuevo.Rows.Count > 0) //&& i < 5)
                        {
                            try
                            {
                                Boolean vExistePeriodo = false;
                                string clave = vClavesmasivonuevo.Rows[i]["clave"].ToString();
                                string vQueryDetalleMasivo = "[EEHAltos_Consumidores_Generales] 15,'" + clave + "'";
                                DataTable vDatosmDetalle = vConexion.obtenerDataTable(vQueryDetalleMasivo);

                                if (vDatosmDetalle.Rows.Count > 0)
                                {
                                    int vContador = ObtenerCorrelativo(vConexion, generarMesFacturacionNew(0), generarMesFacturacionNew(1), ref vExistePeriodo);
                                    int vContadorPeriodo = vContador;
                                    vclave = vClavesmasivonuevo.Rows[i]["clave"].ToString();
                                    vnombremasivo = vClavesmasivonuevo.Rows[i]["m_nombre_abonado"].ToString().Trim();
                                    vnombremasivo = vnombremasivo.Replace('/', ' ');
                                    vnombremasivo = vnombremasivo.Replace('|', ' ');
                                    vnombremasivo = vnombremasivo.Replace('"', ' ');
                                    vnombremasivo = vnombremasivo.Replace('\'', ' ');
                                    vnombremasivo = vnombremasivo.Replace(':', ' ');
                                    string vPathDocumentoGG = pathMasivo + vclave + "_" + vnombremasivo + ".pdf";
                                    FacturaClienteMasivo facturamasivaAC = new FacturaClienteMasivo();
                                    facturamasivaAC.Parameters["m_clave_primaria"].Value = clave;
                                    facturamasivaAC.CreateDocument();
                                    facturamasivaAC.ExportToPdf(vPathDocumentoGG);
                                    facturamasivaAC.Dispose();
                                    String vQuerDetalleMasivo = "[EEHAltos_Consumidores_Generales] 16," + clave;
                                    vConexion.obtenerDataTable(vQuerDetalleMasivo);

                                }

                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                genericos.Log("GENERA MASIVO QUE ANTES ERA UN ALTOS CONSUMIDOR", ex.Message, "");
                            }



                        }
                    }


                    //  COPIA LAS FACTURAS QUE ANTES PERTENENCIA A ALTOS CONSUMIDORES
                    try
                    {
                        vPeriodo = generarMesFacturacionNew(0);
                        string targetMasivo = (@"\\192.168.100.59\\e\\ArchivosAdjuntos\\Facturas\\MASIVO");

                        if (!System.IO.Directory.Exists(targetMasivo))
                            System.IO.Directory.CreateDirectory(targetMasivo);



                        string[] filePaths1 = Directory.GetFiles(@"\\192.168.100.59\\e\\ArchivosAdjuntos\\Facturas\\MASIVO");
                        foreach (string filePath1 in filePaths1)
                            File.Delete(filePath1);
                        //copia facturas de altos consumidores en 100.8 facturas

                        string fuenteMSA = @"\\192.168.100.8\\facturas\\Masivo\\" + vPeriodo + "\\{0}";
                        fuenteMSA = string.Format(fuenteMSA, DateTime.Now.Year + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00"));
                        string destinoMAC = (@"\\192.168.100.59\\e\\ArchivosAdjuntos\\Facturas\\MASIVO\\");
                        DirectoryInfo dl = new DirectoryInfo(fuenteMSA);
                        if (File.Exists(fuenteMSA))
                        {
                            File.Copy(fuenteMSA, destinoMAC, true);
                        }

                        fuenteMSA = @"\\192.168.100.8\\facturas\\Masivo\\" + vPeriodo + "\\MASIVO" + "\\{0}";
                        fuenteMSA = string.Format(fuenteMSA, DateTime.Now.Year + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00"));
                        destinoMAC = (@"\\192.168.100.59\\e\\ArchivosAdjuntos\\Facturas\\MASIVO\\");
                        FileInfo[] directorios = null;
                        directorios = dl.GetFiles("*", SearchOption.AllDirectories);
                        string rutaExtendida = "";

                        foreach (FileInfo docs in directorios)
                        {
                            rutaExtendida = docs.DirectoryName.Replace(fuenteMSA, "");
                            if (!Directory.Exists(destinoMAC + rutaExtendida))
                                Directory.CreateDirectory(destinoMAC);
                            File.Copy(docs.FullName, destinoMAC + "\\" + docs.Name, true);
                        }
                    }
                    catch (Exception EX)
                    {
                        Console.WriteLine(EX.Message);
                        genericos.Log("COPIA MASIVOS QUE ANTES ERAN AC ", EX.Message, "");
                    }


                    // ENVIA POR CORREO LAS CUENTAS DE ALTOS CONNSUMIDORES QUE ANTES PERTENCIAN  A MASIVO
                    try
                    {

                        String vQuery = "[EEHAltos_Consumidores_Generales_Mercado] 9";
                        DataTable vDatosCorreo = vConexion.obtenerDataTable(vQuery);
                        for (int i = 0; i < vDatosCorreo.Rows.Count; i++)
                        {
                            string vPDF = "";
                            string vCodigoAgrupacionEEH = vDatosCorreo.Rows[i]["m_clave_primaria"].ToString();
                            string vNombreInstitucion = vDatosCorreo.Rows[i]["NomIns"].ToString().Trim();
                            vPDF = vCodigoAgrupacionEEH + "_" + vNombreInstitucion + ".pdf";
                            Object[] vDatos = new object[3];
                            vDatos[0] = vDatosCorreo.Rows[i]["m_clave_primaria"].ToString();
                            vDatos[1] = "facturas\\MASIVO" + "\\" + vPDF; //adjuntos
                            vDatos[2] = "EEH";
                            string vCodigoResult = "", vMensajeResult = "";
                            vConexion.notificarFacturacionAC(ref vCodigoResult, ref vMensajeResult, vDatos);
                            Console.WriteLine("=======================================   vCodigoAgrupacionEEH    " + vCodigoAgrupacionEEH + "             ====================================");
                            Console.WriteLine(vCodigoResult);
                            Console.WriteLine(vMensajeResult);
                            Console.WriteLine("===============================================================================================================");
                        }

                    }
                    catch (Exception EX)
                    {

                        Console.WriteLine(EX.Message);
                        genericos.Log("FACTURACION ", EX.Message, "");
                    }

                }
                // GENERA CLAVES DE SIGCON QUE SE ENVIAN POR CORREO
                {

                    string targetMSPath = @"\\192.168.100.8\\centralizada\\MASIVO\\" + vPeriodo + "\\{0}";
                    targetMSPath = string.Format(targetMSPath, DateTime.Now.Year + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00") + "\\");
                    if (!System.IO.Directory.Exists(targetMSPath))
                        System.IO.Directory.CreateDirectory(targetMSPath);
                    try
                    {

                        string vQueryMSdetalle = "[EEHAviso_Masivo_General] 22";
                        DataTable vDatosDetalleMS = vConexion.obtenerDataTable(vQueryMSdetalle);
                        if (vDatosDetalleMS.Rows.Count > 0)

                            for (int i = 0; i < vDatosDetalleMS.Rows.Count; i++)
                            {
                                string vQueryDetallemsC = "[EEHAviso_Masivo_General] 24";
                                DataTable vDatosDetalleMSC = vConexion.obtenerDataTable(vQueryDetallemsC);
                                if (vDatosDetalleMSC.Rows.Count > 0)
                                {
                                    string vClavemsc = vDatosDetalleMS.Rows[i]["Clave"].ToString().PadLeft(4, '0');
                                    string Nombremsc = vDatosDetalleMS.Rows[i]["M_NOMBRE_ABONADO"].ToString();
                                    string vPathDocumentoMSC = targetMSPath + vClavemsc + "_" + Nombremsc + ".pdf";
                                    FacturaEEHMasivoCorreo FacturasMasivoCorreo = new FacturaEEHMasivoCorreo();
                                    FacturasMasivoCorreo.Parameters["obtener_todos"].Value = 0;
                                    FacturasMasivoCorreo.Parameters["pagina"].Value = 1;
                                    FacturasMasivoCorreo.Parameters["maximo"].Value = 100;
                                    FacturasMasivoCorreo.Parameters["clave_primaria"].Value = vClavemsc;
                                    FacturasMasivoCorreo.CreateDocument();
                                    FacturasMasivoCorreo.ExportToPdf(vPathDocumentoMSC);
                                    FacturasMasivoCorreo.Dispose();
                                    String vQuerDetalleMasivoM = "[EEHAviso_Masivo_General] 38," + vClavemsc;
                                    vConexion.obtenerDataTable(vQuerDetalleMasivoM);
                                }

                            }

                    }
                    catch (Exception EX)
                    {
                        Console.WriteLine(EX.Message);
                        genericos.Log("FACTURACION ", EX.Message, "");
                    }
                    try
                    {
                        string[] filePaths1RP = Directory.GetFiles(@"\\192.168.100.59\\e\\ArchivosAdjuntos\\Reparto_Correo\\");
                        foreach (string fileParp1 in filePaths1RP)
                            File.Delete(fileParp1);
                        //copia facturas de altos consumidores en 100.8 facturas
                        string fuenteMs = @"\\192.168.100.8\Centralizada\MASIVO\" + vPeriodo + "\\{0}";
                        fuenteMs = string.Format(fuenteMs, DateTime.Now.Year + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00"));
                        string destinoMS = (@"\\192.168.100.59\\e\\ArchivosAdjuntos\\Reparto_Correo\\");
                        DirectoryInfo dl = new DirectoryInfo(fuenteMs);
                        if (File.Exists(fuenteMs))
                        {
                            File.Copy(fuenteMs, destinoMS, true);
                        }
                        fuenteMs = @"\\192.168.100.8\Centralizada\MASIVO\" + vPeriodo + "\\{0}";
                        fuenteMs = string.Format(fuenteMs, DateTime.Now.Year + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00"));
                        destinoMS = (@"\\192.168.100.59\\e\\ArchivosAdjuntos\\Reparto_Correo\\");
                        FileInfo[] directorios = null;
                        directorios = dl.GetFiles("*", SearchOption.AllDirectories);
                        string rutaExtendida = "";
                        foreach (FileInfo docs in directorios)
                        {
                            // FileAttributes at = File.GetAttributes(docs.DirectoryName);
                            rutaExtendida = docs.DirectoryName.Replace(fuenteMs, "");
                            //if (at.HasFlag(FileAttributes.Directory)){
                            if (!Directory.Exists(destinoMS + rutaExtendida))
                                Directory.CreateDirectory(destinoMS);
                            //}
                            File.Copy(docs.FullName, destinoMS + "\\" + docs.Name, true);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        genericos.Log("FACTURACION ", ex.Message, "");
                    }
                }
                try
                {

                    String vQueryMS1 = "[EEH_Masivos_Generales_Correo] 1";
                    DataTable vDatosCorreoMS = vConexion.obtenerDataTable(vQueryMS1);
                    for (int i = 0; i < vDatosCorreoMS.Rows.Count; i++)
                    {
                        string vPDF = "";
                        string vClaveEEHMS = vDatosCorreoMS.Rows[i]["Clave"].ToString().PadLeft(4, '0');
                        string vNombreInstitucion = vDatosCorreoMS.Rows[i]["m_nombre_abonado"].ToString().Trim();

                        vPDF = vClaveEEHMS + "_" + vNombreInstitucion + ".pdf";
                        Object[] vDatos = new object[3];
                        vDatos[0] = vDatosCorreoMS.Rows[i]["Clave"].ToString();
                        vDatos[1] = "Reparto_Correo\\" + vPDF; //adjuntos
                        vDatos[2] = "wilvert.dubon";
                        string vCodigoResult = "", vMensajeResult = "";
                        vConexion.notificarFacturacionMS(ref vCodigoResult, ref vMensajeResult, vDatos);
                        Console.WriteLine("=======================================   vCodigoAgrupacionEEH    " + vClaveEEHMS + "             ====================================");
                        Console.WriteLine(vCodigoResult);
                        Console.WriteLine(vMensajeResult);
                        Console.WriteLine("===============================================================================================================");
                    }
                }
                catch (Exception EX)
                {
                    Console.WriteLine(EX.Message);
                    genericos.Log("FACTURACION ", EX.Message, "");
                }
                //GENERA TODO GOBIERNO  -- LISTO
                {

                    string vPathnGA = @"\\192.168.100.8\centralizada\GOBIERNO\" + vPeriodo + "\\{0}";
                    vPathnGA = string.Format(vPathnGA, DateTime.Now.Year + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00") + "\\");
                    if (!System.IO.Directory.Exists(vPathnGA))
                        System.IO.Directory.CreateDirectory(vPathnGA);

                    string periodoFacturacionGA = generarMesFacturacionNew(0);
                    String vClavesGA = "[EEH_Gobierno_Generales] 15";
                    DataTable vClavesnuevoGA = vConexion.obtenerDataTable(vClavesGA);
                    if (vClavesnuevoGA.Rows.Count > 0)
                    {
                        for (int i = 0; i < vClavesnuevoGA.Rows.Count; i++)
                        {
                            string prefijoGA = "";
                            string clave = "";
                            string vNombreInstitucionGA = "";
                            string VcodigoAgrupacion = "";
                            VcodigoAgrupacion = "[EEH_Gobierno_Generales] 16," + vClavesnuevoGA.Rows[i]["codAgrupacion"].ToString();
                            DataTable vAgrupacion = vConexion.obtenerDataTable(VcodigoAgrupacion);
                            if (vAgrupacion.Rows.Count > 0)
                            {

                                prefijoGA = vClavesnuevoGA.Rows[i]["codAgrupacion"].ToString();
                                //clave = vAgrupacion.Rows[0]["Clave"].ToString();
                                string vQuerGA = "[EEH_Gobierno_Generales] 17" + vAgrupacion;
                                DataTable VNUMEROSUMAR = vConexion.obtenerDataTable(vQuerGA);
                                int myNum = int.Parse(VNUMEROSUMAR.Rows[0][0].ToString());
                                int vContador = ObtenerCorrelativoN(vConexion, generarMesFacturacionNew(0), generarMesFacturacionNew(1), ref vExistePeriodo1);
                                int vContador1 = vContador + myNum + 17;

                                vNombreInstitucionGA = vAgrupacion.Rows[0]["NomIns"].ToString().Trim();
                                vNombreInstitucionGA = vNombreInstitucionGA.Replace('/', ' ');
                                vNombreInstitucionGA = vNombreInstitucionGA.Replace('|', ' ');
                                vNombreInstitucionGA = vNombreInstitucionGA.Replace('"', ' ');
                                vNombreInstitucionGA = vNombreInstitucionGA.Replace('\'', ' ');
                                vNombreInstitucionGA = vNombreInstitucionGA.Replace(':', ' ');
                                string vPathDocumentoG = vPathnGA + prefijoGA + "_" + vNombreInstitucionGA + ".pdf";
                                try
                                {
                                    GeneracionDiariaGobierno FacturaEspecialGA = new GeneracionDiariaGobierno();
                                    FacturaEspecialGA.Parameters["Tipo"].Value = 7;
                                    FacturaEspecialGA.Parameters["mes_facturacion"].Value = periodoFacturacionGA;
                                    FacturaEspecialGA.Parameters["clave"].Value = prefijoGA;
                                    FacturaEspecialGA.Parameters["contador_factura"].Value = vContador; //enviar la siguiente
                                    FacturaEspecialGA.CreateDocument();
                                    FacturaEspecialGA.ExportToPdf(vPathDocumentoG);
                                    FacturaEspecialGA.Dispose();

                                    String vQuerDetalleMasivoGA = "[EEHAviso_Masivo_General] 45," + vAgrupacion.Rows[0]["codAgrupacion"].ToString();
                                    vConexion.obtenerDataTable(vQuerDetalleMasivoGA);
                                    {
                                        string vQueryInsertarContador = "[EEHInsert_Fact_Correlativo_Altos] 7, '','','{0}','{1}','{2}','{3}','{4}'"; // utilizar para unica 
                                        vQueryInsertarContador = string.Format(vQueryInsertarContador, "Gobierno_diario", generarMesFacturacionNew(0), vContador, vContador1, prefijoGA);
                                        DataTable vDatosInsert = vConexion.obtenerDataTable(vQueryInsertarContador);
                                    }




                                }
                                catch (Exception Ex)
                                {
                                    Console.WriteLine(Ex.Message);
                                }

                            }

                        }
                    }
                    try
                    {
                        string[] filePaths1GA = Directory.GetFiles(@"\\192.168.100.59\e\ArchivosAdjuntos\Facturas\Gobierno");
                        foreach (string filePath1GA in filePaths1GA)
                            File.Delete(filePath1GA);

                        string fuenteGA = @"\\192.168.100.8\centralizada\GOBIERNO\" + vPeriodo + "\\{0}";
                        fuenteGA = string.Format(fuenteGA, DateTime.Now.Year + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00"));
                        string destinoGA = (@"\\192.168.100.59\e\ArchivosAdjuntos\Facturas\Gobierno\");
                        DirectoryInfo dl = new DirectoryInfo(fuenteGA);
                        if (File.Exists(fuenteGA))
                        {
                            File.Copy(fuenteGA, destinoGA, true);
                        }
                        fuenteGA = @"\\192.168.100.8\centralizada\GOBIERNO\" + vPeriodo + "\\{0}";
                        fuenteGA = string.Format(fuenteGA, DateTime.Now.Year + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00"));
                        destinoGA = (@"\\192.168.100.59\e\ArchivosAdjuntos\Facturas\Gobierno\");
                        FileInfo[] directorios = null;
                        directorios = dl.GetFiles("*", SearchOption.AllDirectories);
                        string rutaExtendida = "";

                        foreach (FileInfo docs in directorios)
                        {
                            rutaExtendida = docs.DirectoryName.Replace(fuenteGA, "");
                            if (!Directory.Exists(destinoGA + rutaExtendida))
                                Directory.CreateDirectory(destinoGA);
                            File.Copy(docs.FullName, destinoGA + "\\" + docs.Name, true);
                        }

                    }
                    catch (Exception EX)
                    {
                        Console.WriteLine(EX.Message);
                        genericos.Log("FACTURACION ", EX.Message, "");
                    }

                }

                String vCODIGOERRORG = "";
                try
                {

                    String vQuery1GA = "[EEH_Gobierno_Generales] 18";
                    DataTable vDatosCorreoGA = vConexion.obtenerDataTable(vQuery1GA);
                    for (int i = 0; i < vDatosCorreoGA.Rows.Count; i++)

                    {

                        string vPDF = "";
                        string vCodigoAgrupacionEEH = vDatosCorreoGA.Rows[i]["codAgrupacion"].ToString().PadLeft(4, '0');
                        string vNombreInstitucion = vDatosCorreoGA.Rows[i]["NomIns"].ToString().Trim();
                        vCODIGOERRORG = vCodigoAgrupacionEEH;
                        vPDF = vCodigoAgrupacionEEH + "_" + vNombreInstitucion + ".pdf";
                        Object[] vDatos = new object[3];
                        vDatos[0] = vDatosCorreoGA.Rows[i]["codAgrupacion"].ToString();
                        vDatos[1] = "Facturas\\GOBIERNO\\" + vPDF; //adjuntos
                        vDatos[2] = "noe.alvarez";
                        string vCodigoResult = "", vMensajeResult = "";
                        vConexion.notificarFacturacionGobiernoDiaria(ref vCodigoResult, ref vMensajeResult, vDatos);
                        Console.WriteLine("=======================================   vCodigoAgrupacionEEH    " + vCodigoAgrupacionEEH + "  ====================================");
                        Console.WriteLine(vCodigoResult);
                        Console.WriteLine(vMensajeResult);
                        Console.WriteLine("===============================================================================================================");

                    }

                }
                catch (Exception)
                {

                    throw;
                }


                // GENERA LAS REMERSAS UNICAS DE IRREGULARIDAD
                string vQueryRemesaIrregularidad = "[EEHAviso_Masivo_General] 43";
                DataTable vDetalleFactIrregularidad = vConexion.obtenerDataTable(vQueryRemesaIrregularidad);
                try
                {
                    if (vDetalleFactIrregularidad.Rows.Count > 0)
                        for (int i = 0; i < vDetalleFactIrregularidad.Rows.Count; i++)
                        {
                            string vClave = vDetalleFactIrregularidad.Rows[i]["Clave"].ToString().PadLeft(4, '0');
                            string Nombre = vDetalleFactIrregularidad.Rows[i]["m_nombre_abonado"].ToString().Trim();
                            Nombre = Nombre.Replace('/', ' ');
                            Nombre = Nombre.Replace('|', ' ');
                            Nombre = Nombre.Replace('"', ' ');
                            Nombre = Nombre.Replace('\'', ' ');
                            Nombre = Nombre.Replace(':', ' ');
                            string Liquidacion = vDetalleFactIrregularidad.Rows[i]["id_liquidacion"].ToString();
                            string SectorEEH = vDetalleFactIrregularidad.Rows[i]["SECTOR_ID"].ToString();
                            string VSector = vDetalleFactIrregularidad.Rows[i]["SECTOR_EEH"].ToString();
                            string VOs = vDetalleFactIrregularidad.Rows[i]["Os"].ToString();
                            string vPathremesa = @"\\192.168.100.8\centralizada\Irregularidad\" + vPeriodo + "\\" + VSector + "\\{0}";
                            vPathremesa = string.Format(vPathremesa, DateTime.Now.Year + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00") + "\\");
                            if (!System.IO.Directory.Exists(vPathremesa))
                                System.IO.Directory.CreateDirectory(vPathremesa);
                            string vPathn = @"\\192.168.100.59\\f\\irregularidades\\adjuntos\\";
                            if (!System.IO.Directory.Exists(vPathn))
                                System.IO.Directory.CreateDirectory(vPathn);
                            string vPathDocumentoGGI = vPathremesa + SectorEEH + "_" + Liquidacion + "_" + vClave + "_" + Nombre + ".pdf";
                            string vPathDocumentoIRRemesas = vPathn + "FECN" + "_" + vClave + "_" + Liquidacion + ".pdf";
                            FacturaIrregularidadRemesas FacturasRemesasirregularidad = new FacturaIrregularidadRemesas();
                            FacturasRemesasirregularidad.Parameters["obtener_todos"].Value = 5;
                            FacturasRemesasirregularidad.Parameters["clave_primaria"].Value = Liquidacion;
                            FacturasRemesasirregularidad.CreateDocument();
                            FacturasRemesasirregularidad.ExportToPdf(vPathDocumentoGGI);
                            FacturasRemesasirregularidad.ExportToPdf(vPathDocumentoIRRemesas);
                            FacturasRemesasirregularidad.Dispose();
                            String vQuerDetalleMasivoRemesa = "[EEHAviso_Masivo_General] 44," + Liquidacion;
                            vConexion.obtenerDataTable(vQuerDetalleMasivoRemesa);
                            Object[] vDatos = new object[40];
                            vDatos[0] = VOs;
                            vDatos[1] = Liquidacion;
                            vDatos[2] = "FECN" + "_" + vClave + "_" + Liquidacion + ".pdf";
                            vDatos[3] = 54;
                            vDatos[4] = ".pdf";
                            vDatos[5] = 0;
                            vDatos[6] = 0;
                            vDatos[7] = "FACTURA ECNF";
                            vDatos[8] = null;
                            vDatos[9] = "appsoe";
                            vDatos[10] = 1;
                            vDatos[11] = 0;
                            string vCodigoResult = "", vMensajeResult = "";
                            vConexion.vRegistrarFactura(ref vCodigoResult, ref vMensajeResult, vDatos);
                            Console.WriteLine("=======================================   vCodigoAgrupacionEEH    " + vClave + "             ====================================");
                            Console.WriteLine(vCodigoResult);
                            Console.WriteLine(vMensajeResult);
                            Console.WriteLine("===============================================================================================================");

                        }

                }
                catch (Exception Ex)
                {

                    Console.WriteLine(Ex.Message);
                    genericos.Log("FACTURAS ALTOS CONSUMIODRES QUE SE ENTREGAN POR REPARTO ", Ex.Message, "");
                }

                /// GENERA LOS LOTES DE REMESAS
                {

                    String vRemesas = "[EEHAviso_Masivo_General]40";
                    DataTable vRemesasIrregularidad = vConexion.obtenerDataTable(vRemesas);
                    string targetPath = (@"\\192.168.100.8\centralizada\REMESAS_IRREGULARIDAD\" + vPeriodo + "\\{0}\\");
                    targetPath = string.Format(targetPath, DateTime.Now.Year + DateTime.Now.Month.ToString("00") + DateTime.Now.Day.ToString("00"));
                    if (!System.IO.Directory.Exists(targetPath))
                        System.IO.Directory.CreateDirectory(targetPath);
                    for (int i = 0; i < vRemesasIrregularidad.Rows.Count; i++)
                    {
                        if (vRemesasIrregularidad.Rows.Count > 0) //&& i < 5)
                        {
                            string vRemesasAgrupa = vRemesasIrregularidad.Rows[i]["num_remersas"].ToString();
                            string vQueryRemesasDetalle = "[EEHAviso_Masivo_General]39,'" + vRemesasAgrupa + "'";
                            DataTable vQueryDetalleRemesas = vConexion.obtenerDataTable(vQueryRemesasDetalle);
                            if (vQueryDetalleRemesas.Rows.Count > 0)
                            {
                                string vPathDocumentoRemesa = targetPath + vRemesasAgrupa + ".pdf";
                                FacturasEEHIrregularidadReserva facturasIrregularidadMasivo = new FacturasEEHIrregularidadReserva();
                                facturasIrregularidadMasivo.Parameters["obtener_todos"].Value = 4;
                                facturasIrregularidadMasivo.Parameters["ID_Liquidacion"].Value = 1;
                                facturasIrregularidadMasivo.Parameters["clave_primaria"].Value = vRemesasAgrupa;
                                facturasIrregularidadMasivo.CreateDocument();
                                facturasIrregularidadMasivo.ExportToPdf(vPathDocumentoRemesa);
                                facturasIrregularidadMasivo.Dispose();
                                String vQuerDetalleIrregularidad = "[EEHAviso_Masivo_General]41," + vRemesasAgrupa;
                                vConexion.obtenerDataTable(vQuerDetalleIrregularidad);
                                String vQuerDetallePath = "[EEHAviso_Masivo_General]42," + "'" + vPathDocumentoRemesa + "'" + "," + vRemesasAgrupa;
                                vConexion.obtenerDataTable(vQuerDetallePath);

                            }
                        }
                    }


                }



            }
            this.Close();
        }
        public string generarMesFacturacionNew(int value)
        {

            string ultimoMesFacturacion = DateTime.Today.Year.ToString();
            ultimoMesFacturacion = ultimoMesFacturacion.Remove(0, 2);
            ultimoMesFacturacion += (DateTime.Today.Month - (value)).ToString().PadLeft(2, '0');
            return ultimoMesFacturacion;
        }

        private int ObtenerCorrelativo(Conexion vConexion, string vPeriodoActual, string vPeriodoAnterior, ref Boolean ExistePeriodo)
        {
            int vCorrelativo = 0;
            try
            {
                string vQueryObtenerCorrelativo = "[EEHInsert_Fact_Correlativo_Altos] 5";//"[EEHInsert_Fact_Correlativo_Altos] 1,'" + vPeriodoActual + "','" + vPeriodoAnterior + "'";
                DataTable vDatosCorrelativo = vConexion.obtenerDataTable(vQueryObtenerCorrelativo);

                if (vDatosCorrelativo.Rows.Count > 0)
                {
                    vCorrelativo = Convert.ToInt32(vDatosCorrelativo.Rows[0][0].ToString());
                    ExistePeriodo = true;
                }
            }
            catch (Exception EX)
            {


            }

            return vCorrelativo + 1;
        }

        private int ObtenerCorrelativoN(Conexion vConexion, string vPeriodoActual, string vPeriodoAnterior, ref Boolean ExistePeriodo)
        {
            int vCorrelativo = 0;
            try
            {
                string vQueryObtenerCorrelativo = "[EEHInsert_Fact_Correlativo_Altos] 6";//"[EEHInsert_Fact_Correlativo_Altos] 1,'" + vPeriodoActual + "','" + vPeriodoAnterior + "'";
                DataTable vDatosCorrelativo = vConexion.obtenerDataTable(vQueryObtenerCorrelativo);

                if (vDatosCorrelativo.Rows.Count > 0)
                {
                    vCorrelativo = Convert.ToInt32(vDatosCorrelativo.Rows[0][0].ToString());
                    ExistePeriodo = true;
                }


            }
            catch (Exception EX)
            {


                Console.Write(EX.Message);

            }




            return vCorrelativo + 1;
        }

    }
 
}
