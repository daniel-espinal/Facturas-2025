using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionGobierno.Model
{
    public class AvisoGobierno 
    {
        long row;
        string prefijo;
        string sufijo;
        decimal m_clave_primaria;
        decimal m_codigo_city;
        decimal m_codigo_ruta;
        decimal m_codigo_acomet;
        string m_nombre_abonado;
        string m_direcc_abonado;
        string m_contador_activo;
        decimal m_multip_activo;
        decimal m_numero_agujas;
        decimal m_numero_transforma;
        decimal m_fases;
        decimal m_numero_deposito;
        decimal m_valor_deposito;
        decimal m_fecha_conexion;
        decimal m_tarifa_servicio;
        decimal m_sector_consumo;
        string m_ident;
        string m_rtn;
        decimal m_fecha_actual;
        decimal m_fecha_anterior;
        decimal m_lectura_actual;
        decimal m_lectura_anterior;
        decimal m_consumo_del_mes;
        decimal m_consumo_estimado;
        decimal m_consumo_adicional;
        decimal m_dias_facturados;
        decimal m_consumos1;
        decimal m_consumos2;
        decimal m_consumos3;
        decimal m_consumos4;
        decimal m_consumos5;
        decimal m_consumos6;
        decimal m_consumos7;
        decimal m_consumos8;
        decimal m_consumos9;
        decimal m_consumos10;
        decimal m_consumos11;
        decimal m_consumos12;
        decimal m_dias1;
        decimal m_dias2;
        decimal m_dias3;
        decimal m_dias4;
        decimal m_dias5;
        decimal m_dias6;
        decimal m_dias7;
        decimal m_dias8;
        decimal m_dias9;
        decimal m_dias10;
        decimal m_dias11;
        decimal m_dias12;
        decimal m_cobro_energia;
        decimal m_cobro_diesel;
        decimal m_cobro_interes;
        decimal m_cobro_publico;
        decimal m_otros_dr_cr;
        decimal m_subsidio;
        decimal m_saldo_del_mes;
        decimal m_saldo_30_dias;
        decimal m_saldo_60_dias;
        decimal m_saldo_90_dias;
        decimal m_saldo_120_dias;
        decimal m_saldo_anterior;
        decimal m_valor_rectifica;
        decimal m_pagos_del_ciclo;
        decimal m_pagos_del_mes;
        decimal m_pagos_no_act;
        decimal m_fecha_pago;
        decimal m_ctl_pago1;
        decimal m_ctl_pago2;
        decimal m_ctl_pago3;
        decimal m_ctl_pago4;
        decimal m_ctl_pago5;
        decimal m_ctl_pago6;
        decimal m_colector;
        decimal m_fecha_emision;
        decimal m_ultimo_mes_fact;
        decimal m_estado_registro;
        decimal m_fecha_inactivo;
        string m_codigo_archivo;
        decimal m_codigo_promedio;
        decimal m_codigo_contrato;
        decimal m_codigo_lectura;
        decimal m_codigo_u_medido;
        decimal m_codigo_m_avisos;
        decimal m_codigo_comenta;
        string m_codigo_corte;
        decimal m_fecha_corte;
        decimal m_fecha_3edad;
        decimal m_codigo_3edad;
        decimal m_codigo_fraude;
        decimal m_fecha_fraude;
        decimal w_ultimo_mes_grab;
        decimal m_fecha_garantia;
        decimal m_catastro;
        string m_no_garantia;
        decimal m_region;
        decimal m_total_cargo_mes;
        decimal m_telefono;
        decimal m_fax;
        decimal m_isv;
        decimal m_isv_ant;
        decimal m_numero_cortes;
        string m_nivel_residencial;
        string m_bono_general;
        string m_ubicacion;
        //decimal m_codigo_agrupa;
        //decimal a_descripcion;
        string t_descripcion_tarifa;
        string s_descripcion_sector;
        DateTime fecha_emision_real;
        DateTime fecha_vence_real;
        string m_codigo_agrupa;
        string a_descripcion;
        string IMPRESION;


        public long Row { get => row; set => row = value; }
        public string Prefijo { get => prefijo; set => prefijo = value; }
        public string Sufijo { get => sufijo; set => sufijo = value; }
        public decimal Clave_primaria { get => m_clave_primaria; set => m_clave_primaria = value; }
        public decimal Codigo_city { get => m_codigo_city; set => m_codigo_city = value; }
        public decimal Codigo_ruta { get => m_codigo_ruta; set => m_codigo_ruta = value; }
        public decimal Codigo_acomet { get => m_codigo_acomet; set => m_codigo_acomet = value; }
        public string Nombre_abonado { get => m_nombre_abonado; set => m_nombre_abonado = value; }
        public string Direcc_abonado { get => m_direcc_abonado; set => m_direcc_abonado = value; }
        public string Contador_activo { get => m_contador_activo; set => m_contador_activo = value; }
        public decimal Multip_activo { get => m_multip_activo; set => m_multip_activo = value; }
        public decimal Numero_agujas { get => m_numero_agujas; set => m_numero_agujas = value; }
        public decimal Numero_transforma { get => m_numero_transforma; set => m_numero_transforma = value; }
        public decimal Fases { get => m_fases; set => m_fases = value; }
        public decimal Numero_deposito { get => m_numero_deposito; set => m_numero_deposito = value; }
        public decimal Valor_deposito { get => m_valor_deposito; set => m_valor_deposito = value; }
        public decimal Fecha_conexion { get => m_fecha_conexion; set => m_fecha_conexion = value; }
        public decimal Tarifa_servicio { get => m_tarifa_servicio; set => m_tarifa_servicio = value; }
        public decimal Sector_consumo { get => m_sector_consumo; set => m_sector_consumo = value; }
        public string Ident { get => m_ident; set => m_ident = value; }
        public string Rtn { get => m_rtn; set => m_rtn = value; }
        public decimal Fecha_actual { get => m_fecha_actual; set => m_fecha_actual = value; }
        public decimal Fecha_anterior { get => m_fecha_anterior; set => m_fecha_anterior = value; }
        public decimal Lectura_actual { get => m_lectura_actual; set => m_lectura_actual = value; }
        public decimal Lectura_anterior { get => m_lectura_anterior; set => m_lectura_anterior = value; }
        public decimal Consumo_del_mes { get => m_consumo_del_mes; set => m_consumo_del_mes = value; }
        public decimal Consumo_estimado { get => m_consumo_estimado; set => m_consumo_estimado = value; }
        public decimal Consumo_adicional { get => m_consumo_adicional; set => m_consumo_adicional = value; }
        public decimal Dias_facturados { get => m_dias_facturados; set => m_dias_facturados = value; }
        public decimal Consumos1 { get => m_consumos1; set => m_consumos1 = value; }
        public decimal Consumos2 { get => m_consumos2; set => m_consumos2 = value; }
        public decimal Consumos3 { get => m_consumos3; set => m_consumos3 = value; }
        public decimal Consumos4 { get => m_consumos4; set => m_consumos4 = value; }
        public decimal Consumos5 { get => m_consumos5; set => m_consumos5 = value; }
        public decimal Consumos6 { get => m_consumos6; set => m_consumos6 = value; }
        public decimal Consumos7 { get => m_consumos7; set => m_consumos7 = value; }
        public decimal Consumos8 { get => m_consumos8; set => m_consumos8 = value; }
        public decimal Consumos9 { get => m_consumos9; set => m_consumos9 = value; }
        public decimal Consumos10 { get => m_consumos10; set => m_consumos10 = value; }
        public decimal Consumos11 { get => m_consumos11; set => m_consumos11 = value; }
        public decimal Consumos12 { get => m_consumos12; set => m_consumos12 = value; }
        public decimal Dias1 { get => m_dias1; set => m_dias1 = value; }
        public decimal Dias2 { get => m_dias2; set => m_dias2 = value; }
        public decimal Dias3 { get => m_dias3; set => m_dias3 = value; }
        public decimal Dias4 { get => m_dias4; set => m_dias4 = value; }
        public decimal Dias5 { get => m_dias5; set => m_dias5 = value; }
        public decimal Dias6 { get => m_dias6; set => m_dias6 = value; }
        public decimal Dias7 { get => m_dias7; set => m_dias7 = value; }
        public decimal Dias8 { get => m_dias8; set => m_dias8 = value; }
        public decimal Dias9 { get => m_dias9; set => m_dias9 = value; }
        public decimal Dias10 { get => m_dias10; set => m_dias10 = value; }
        public decimal Dias11 { get => m_dias11; set => m_dias11 = value; }
        public decimal Dias12 { get => m_dias12; set => m_dias12 = value; }
        public decimal Cobro_energia { get => m_cobro_energia; set => m_cobro_energia = value; }
        public decimal Cobro_diesel { get => m_cobro_diesel; set => m_cobro_diesel = value; }
        public decimal Cobro_interes { get => m_cobro_interes; set => m_cobro_interes = value; }
        public decimal Cobro_publico { get => m_cobro_publico; set => m_cobro_publico = value; }
        public decimal Otros_dr_cr { get => m_otros_dr_cr; set => m_otros_dr_cr = value; }
        public decimal Subsidio { get => m_subsidio; set => m_subsidio = value; }
        public decimal Saldo_del_mes { get => m_saldo_del_mes; set => m_saldo_del_mes = value; }
        public decimal Saldo_30_dias { get => m_saldo_30_dias; set => m_saldo_30_dias = value; }
        public decimal Saldo_60_dias { get => m_saldo_60_dias; set => m_saldo_60_dias = value; }
        public decimal Saldo_90_dias { get => m_saldo_90_dias; set => m_saldo_90_dias = value; }
        public decimal Saldo_120_dias { get => m_saldo_120_dias; set => m_saldo_120_dias = value; }
        public decimal Saldo_anterior { get => m_saldo_anterior; set => m_saldo_anterior = value; }
        public decimal Valor_rectifica { get => m_valor_rectifica; set => m_valor_rectifica = value; }
        public decimal Pagos_del_ciclo { get => m_pagos_del_ciclo; set => m_pagos_del_ciclo = value; }
        public decimal Pagos_del_mes { get => m_pagos_del_mes; set => m_pagos_del_mes = value; }
        public decimal Pagos_no_act { get => m_pagos_no_act; set => m_pagos_no_act = value; }
        public decimal Fecha_pago { get => m_fecha_pago; set => m_fecha_pago = value; }
        public decimal Ctl_pago1 { get => m_ctl_pago1; set => m_ctl_pago1 = value; }
        public decimal Ctl_pago2 { get => m_ctl_pago2; set => m_ctl_pago2 = value; }
        public decimal Ctl_pago3 { get => m_ctl_pago3; set => m_ctl_pago3 = value; }
        public decimal Ctl_pago4 { get => m_ctl_pago4; set => m_ctl_pago4 = value; }
        public decimal Ctl_pago5 { get => m_ctl_pago5; set => m_ctl_pago5 = value; }
        public decimal Ctl_pago6 { get => m_ctl_pago6; set => m_ctl_pago6 = value; }
        public decimal Colector { get => m_colector; set => m_colector = value; }
        public decimal Fecha_emision { get => m_fecha_emision; set => m_fecha_emision = value; }
        public decimal Ultimo_mes_fact { get => m_ultimo_mes_fact; set => m_ultimo_mes_fact = value; }
        public decimal Estado_registro { get => m_estado_registro; set => m_estado_registro = value; }
        public decimal Fecha_inactivo { get => m_fecha_inactivo; set => m_fecha_inactivo = value; }
        public string Codigo_archivo { get => m_codigo_archivo; set => m_codigo_archivo = value; }
        public decimal Codigo_promedio { get => m_codigo_promedio; set => m_codigo_promedio = value; }
        public decimal Codigo_contrato { get => m_codigo_contrato; set => m_codigo_contrato = value; }
        public decimal Codigo_lectura { get => m_codigo_lectura; set => m_codigo_lectura = value; }
        public decimal Codigo_u_medido { get => m_codigo_u_medido; set => m_codigo_u_medido = value; }
        public decimal Codigo_m_avisos { get => m_codigo_m_avisos; set => m_codigo_m_avisos = value; }
        public decimal Codigo_comenta { get => m_codigo_comenta; set => m_codigo_comenta = value; }
        public string Codigo_corte { get => m_codigo_corte; set => m_codigo_corte = value; }
        public decimal Fecha_corte { get => m_fecha_corte; set => m_fecha_corte = value; }
        public decimal Fecha_3edad { get => m_fecha_3edad; set => m_fecha_3edad = value; }
        public decimal Codigo_3edad { get => m_codigo_3edad; set => m_codigo_3edad = value; }
        public decimal Codigo_fraude { get => m_codigo_fraude; set => m_codigo_fraude = value; }
        public decimal Fecha_fraude { get => m_fecha_fraude; set => m_fecha_fraude = value; }
        public decimal W_ultimo_mes_grab { get => w_ultimo_mes_grab; set => w_ultimo_mes_grab = value; }
        public decimal Fecha_garantia { get => m_fecha_garantia; set => m_fecha_garantia = value; }
        public decimal Catastro { get => m_catastro; set => m_catastro = value; }
        public string No_garantia { get => m_no_garantia; set => m_no_garantia = value; }
        public decimal Region { get => m_region; set => m_region = value; }
        public decimal Total_cargo_mes { get => m_total_cargo_mes; set => m_total_cargo_mes = value; }
        public decimal Telefono { get => m_telefono; set => m_telefono = value; }
        public decimal Fax { get => m_fax; set => m_fax = value; }
        public decimal Isv { get => m_isv; set => m_isv = value; }
        public decimal Isv_ant { get => m_isv_ant; set => m_isv_ant = value; }
        public decimal Numero_cortes { get => m_numero_cortes; set => m_numero_cortes = value; }
        public string Nivel_residencial { get => m_nivel_residencial; set => m_nivel_residencial = value; }
        public string Bono_general { get => m_bono_general; set => m_bono_general = value; }
        public string Ubicacion { get => m_ubicacion; set => m_ubicacion = value; }
        public string T_descripcion_tarifa { get => t_descripcion_tarifa; set => t_descripcion_tarifa = value; }
        public string S_descripcion_sector { get => s_descripcion_sector; set => s_descripcion_sector = value; }
        public DateTime Fecha_emision_real { get => fecha_emision_real; set => fecha_emision_real = value; }
        public DateTime Fecha_vence_real { get => fecha_vence_real; set => fecha_vence_real = value; }
        public string Codigo_agrupa { get => m_codigo_agrupa; set => m_codigo_agrupa = value; }
        public string A_descripcion { get => a_descripcion; set => a_descripcion = value; }
        public string IMPRESION1 { get => IMPRESION; set => IMPRESION = value; }
        public List<AvisoGobierno> ObtenerListaAvisoGobierno(decimal contador)
        {
            List<AvisoGobierno> lstAvisoGobierno = new List<AvisoGobierno>();
            Conexion conexion = new Conexion();

            try
            {
                DataTable dataTable = new DataTable();
                SqlDataAdapter dataAdapter = new SqlDataAdapter(Util.Util.SP_AVISO_GOBIERNO, conexion.conexion);
                // String[] config = ConfigurationSettings.AppSettings.GetValues("CONTADOR_FACTURA");
                //tring contador_factura = config[0];
                //string contador_factura = "98335";
                string contador_factura = contador.ToString();
              

                if (conexion.ConectarServer())
                {
                   
                    FacturaCorrelativo facturaCorrelativo = new FacturaCorrelativo();
                    dataAdapter.SelectCommand.CommandTimeout = 120;
                    dataAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    dataAdapter.SelectCommand.Parameters.Add("@CONTADOR_FACTURA", SqlDbType.VarChar).Value = contador_factura;
                    dataAdapter.SelectCommand.Parameters.Add("@ULTIMO_MES_FACTURA", SqlDbType.VarChar).Value = facturaCorrelativo.generarMesFacturacion(1);//generarMesActualFacturacion();
                    dataAdapter.SelectCommand.Parameters.Add("@CODIGO_AGRUPA", SqlDbType.VarChar).Value = "0";//registros sin codigo de agrupacion
                    dataAdapter.SelectCommand.Parameters.Add("@OBTENER_TODOS", SqlDbType.VarChar).Value = "1";//obtenerTodos los registros
                    dataAdapter.SelectCommand.Parameters.Add("@PAGINA", SqlDbType.VarChar).Value = "1";//registros sin codigo de agrupacion
                    dataAdapter.SelectCommand.Parameters.Add("@MAXIMO", SqlDbType.VarChar).Value = "1";//obtenerTodos los registros                   
                    dataAdapter.Fill(dataTable);

                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {

                        AvisoGobierno avisoGobierno = new AvisoGobierno();

                        avisoGobierno.Row = (long)dataTable.Rows[i]["Row#"];
                        avisoGobierno.Prefijo = (string)dataTable.Rows[i]["PREFIJO"];
                        avisoGobierno.Sufijo = (string)dataTable.Rows[i]["SUFIJO"];
                        avisoGobierno.Clave_primaria = (decimal)dataTable.Rows[i]["m_clave_primaria"];
                        avisoGobierno.Codigo_city = (decimal)dataTable.Rows[i]["m_codigo_city"];
                        avisoGobierno.Codigo_ruta = (decimal)dataTable.Rows[i]["m_codigo_ruta"];
                        avisoGobierno.Codigo_acomet = (decimal)dataTable.Rows[i]["m_codigo_acomet"];
                        avisoGobierno.Nombre_abonado = (string)dataTable.Rows[i]["m_nombre_abonado"];
                        avisoGobierno.Direcc_abonado = (string)dataTable.Rows[i]["m_direcc_abonado"];
                        avisoGobierno.Contador_activo = dataTable.Rows[i]["m_contador_activo"].ToString();
                        avisoGobierno.Multip_activo = (decimal)dataTable.Rows[i]["m_multip_activo"];
                        avisoGobierno.Numero_agujas = (decimal)dataTable.Rows[i]["m_numero_agujas"];
                        avisoGobierno.Numero_transforma = (decimal)dataTable.Rows[i]["m_numero_transforma"];
                        avisoGobierno.Fases = (decimal)dataTable.Rows[i]["m_fases"];
                        avisoGobierno.Numero_deposito = (decimal)dataTable.Rows[i]["m_numero_deposito"];
                        avisoGobierno.Valor_deposito = (decimal)dataTable.Rows[i]["m_valor_deposito"];
                        avisoGobierno.Fecha_conexion = (decimal)dataTable.Rows[i]["m_fecha_conexion"];
                        avisoGobierno.Tarifa_servicio = (decimal)dataTable.Rows[i]["m_tarifa_servicio"];
                        avisoGobierno.Sector_consumo = (decimal)dataTable.Rows[i]["m_sector_consumo"];
                        avisoGobierno.Ident = dataTable.Rows[i]["m_ident"].ToString();
                        avisoGobierno.Rtn = dataTable.Rows[i]["m_rtn"].ToString();
                        avisoGobierno.Fecha_actual = (decimal)dataTable.Rows[i]["m_fecha_actual"];
                        avisoGobierno.Fecha_anterior = (decimal)dataTable.Rows[i]["m_fecha_anterior"];
                        avisoGobierno.Lectura_actual = (decimal)dataTable.Rows[i]["m_lectura_actual"];
                        avisoGobierno.Lectura_anterior = (decimal)dataTable.Rows[i]["m_lectura_anterior"];
                        avisoGobierno.Consumo_del_mes = (decimal)dataTable.Rows[i]["m_consumo_del_mes"];
                        avisoGobierno.Consumo_estimado = (decimal)dataTable.Rows[i]["m_consumo_estimado"];
                        avisoGobierno.Consumo_adicional = (decimal)dataTable.Rows[i]["m_consumo_adicional"];
                        avisoGobierno.Dias_facturados = (decimal)dataTable.Rows[i]["m_dias_facturados"];
                        avisoGobierno.Consumos1 = (decimal)dataTable.Rows[i]["m_consumos1"];
                        avisoGobierno.Consumos2 = (decimal)dataTable.Rows[i]["m_consumos2"];
                        avisoGobierno.Consumos3 = (decimal)dataTable.Rows[i]["m_consumos3"];
                        avisoGobierno.Consumos4 = (decimal)dataTable.Rows[i]["m_consumos4"];
                        avisoGobierno.Consumos5 = (decimal)dataTable.Rows[i]["m_consumos5"];
                        avisoGobierno.Consumos6 = (decimal)dataTable.Rows[i]["m_consumos6"];
                        avisoGobierno.Consumos7 = (decimal)dataTable.Rows[i]["m_consumos7"];
                        avisoGobierno.Consumos8 = (decimal)dataTable.Rows[i]["m_consumos8"];
                        avisoGobierno.Consumos9 = (decimal)dataTable.Rows[i]["m_consumos9"];
                        avisoGobierno.Consumos10 = (decimal)dataTable.Rows[i]["m_consumos10"];
                        avisoGobierno.Consumos11 = (decimal)dataTable.Rows[i]["m_consumos11"];
                        avisoGobierno.Consumos12 = (decimal)dataTable.Rows[i]["m_consumos12"];
                        avisoGobierno.Dias1 = (decimal)dataTable.Rows[i]["m_dias1"];
                        avisoGobierno.Dias2 = (decimal)dataTable.Rows[i]["m_dias2"];
                        avisoGobierno.Dias3 = (decimal)dataTable.Rows[i]["m_dias3"];
                        avisoGobierno.Dias4 = (decimal)dataTable.Rows[i]["m_dias4"];
                        avisoGobierno.Dias5 = (decimal)dataTable.Rows[i]["m_dias5"];
                        avisoGobierno.Dias6 = (decimal)dataTable.Rows[i]["m_dias6"];
                        avisoGobierno.Dias7 = (decimal)dataTable.Rows[i]["m_dias7"];
                        avisoGobierno.Dias8 = (decimal)dataTable.Rows[i]["m_dias8"];
                        avisoGobierno.Dias9 = (decimal)dataTable.Rows[i]["m_dias9"];
                        avisoGobierno.Dias10 = (decimal)dataTable.Rows[i]["m_dias10"];
                        avisoGobierno.Dias11 = (decimal)dataTable.Rows[i]["m_dias11"];
                        avisoGobierno.Dias12 = (decimal)dataTable.Rows[i]["m_dias12"];
                        avisoGobierno.Cobro_energia = (decimal)dataTable.Rows[i]["m_cobro_energia"];
                        avisoGobierno.Cobro_diesel = (decimal)dataTable.Rows[i]["m_cobro_diesel"];
                        avisoGobierno.Cobro_interes = (decimal)dataTable.Rows[i]["m_cobro_interes"];
                        avisoGobierno.Cobro_publico = (decimal)dataTable.Rows[i]["m_cobro_publico"];
                        avisoGobierno.Otros_dr_cr = Convert.ToDecimal(dataTable.Rows[i]["m_otros_dr_cr"]);
                        //avisoGobierno.Otros_dr_cr = (decimal)dataTable.Rows[i]["m_otros_dr_cr"];
                        avisoGobierno.Subsidio = (decimal)dataTable.Rows[i]["m_subsidio"];
                        avisoGobierno.Saldo_del_mes = (decimal)dataTable.Rows[i]["m_saldo_del_mes"];
                        avisoGobierno.Saldo_30_dias = (decimal)dataTable.Rows[i]["m_saldo_30_dias"];
                        avisoGobierno.Saldo_60_dias = (decimal)dataTable.Rows[i]["m_saldo_60_dias"];
                        avisoGobierno.Saldo_90_dias = (decimal)dataTable.Rows[i]["m_saldo_90_dias"];
                        avisoGobierno.Saldo_120_dias = (decimal)dataTable.Rows[i]["m_saldo_120_dias"];
                        avisoGobierno.Saldo_anterior = Convert.ToDecimal(dataTable.Rows[i]["m_saldo_anterior"]);
                        // avisoGobierno.Saldo_anterior = (decimal)dataTable.Rows[i]["m_saldo_anterior"];
                        avisoGobierno.Valor_rectifica = (decimal)dataTable.Rows[i]["m_valor_rectifica"];
                        avisoGobierno.Pagos_del_ciclo = (decimal)dataTable.Rows[i]["m_pagos_del_ciclo"];
                        avisoGobierno.Pagos_del_mes = (decimal)dataTable.Rows[i]["m_pagos_del_mes"];
                        avisoGobierno.Pagos_no_act = (decimal)dataTable.Rows[i]["m_pagos_no_act"];
                        avisoGobierno.Fecha_pago = (decimal)dataTable.Rows[i]["m_fecha_pago"];
                        avisoGobierno.Ctl_pago1 = (decimal)dataTable.Rows[i]["m_ctl_pago1"];
                        avisoGobierno.Ctl_pago2 = (decimal)dataTable.Rows[i]["m_ctl_pago2"];
                        avisoGobierno.Ctl_pago3 = (decimal)dataTable.Rows[i]["m_ctl_pago3"];
                        avisoGobierno.Ctl_pago4 = (decimal)dataTable.Rows[i]["m_ctl_pago4"];
                        avisoGobierno.Ctl_pago5 = (decimal)dataTable.Rows[i]["m_ctl_pago5"];
                        avisoGobierno.Ctl_pago6 = (decimal)dataTable.Rows[i]["m_ctl_pago6"];
                        avisoGobierno.Colector = (decimal)dataTable.Rows[i]["m_colector"];
                        avisoGobierno.Fecha_emision = (decimal)dataTable.Rows[i]["m_fecha_emision"];
                        avisoGobierno.Ultimo_mes_fact = (decimal)dataTable.Rows[i]["m_ultimo_mes_fact"];
                        avisoGobierno.Estado_registro = (decimal)dataTable.Rows[i]["m_estado_registro"];
                        avisoGobierno.Fecha_inactivo = (decimal)dataTable.Rows[i]["m_fecha_inactivo"];
                        avisoGobierno.Codigo_archivo = (string)dataTable.Rows[i]["m_codigo_archivo"];
                        avisoGobierno.Codigo_promedio = (decimal)dataTable.Rows[i]["m_codigo_promedio"];
                        avisoGobierno.Codigo_contrato = (decimal)dataTable.Rows[i]["m_codigo_contrato"];
                        avisoGobierno.Codigo_lectura = (decimal)dataTable.Rows[i]["m_codigo_lectura"];
                        avisoGobierno.Codigo_u_medido = (decimal)dataTable.Rows[i]["m_codigo_u_medido"];
                        avisoGobierno.Codigo_m_avisos = (decimal)dataTable.Rows[i]["m_codigo_m_avisos"];
                        avisoGobierno.Codigo_comenta = (decimal)dataTable.Rows[i]["m_codigo_comenta"];
                        avisoGobierno.Codigo_corte = (string)dataTable.Rows[i]["m_codigo_corte"];
                        avisoGobierno.Fecha_corte = (decimal)dataTable.Rows[i]["m_fecha_corte"];
                        avisoGobierno.Fecha_3edad = (decimal)dataTable.Rows[i]["m_fecha_3edad"];
                        avisoGobierno.Codigo_3edad = (decimal)dataTable.Rows[i]["m_codigo_3edad"];
                        avisoGobierno.Codigo_fraude = (decimal)dataTable.Rows[i]["m_codigo_fraude"];
                        avisoGobierno.Fecha_fraude = (decimal)dataTable.Rows[i]["m_fecha_fraude"];
                        avisoGobierno.W_ultimo_mes_grab = (decimal)dataTable.Rows[i]["w_ultimo_mes_grab"];
                        avisoGobierno.Fecha_garantia = (decimal)dataTable.Rows[i]["m_fecha_garantia"];
                        avisoGobierno.Catastro = (decimal)dataTable.Rows[i]["m_catastro"];
                        avisoGobierno.No_garantia = (string)dataTable.Rows[i]["m_no_garantia"];
                        avisoGobierno.Region = (decimal)dataTable.Rows[i]["m_region"];
                        avisoGobierno.Total_cargo_mes = Convert.ToDecimal(dataTable.Rows[i]["m_total_cargo_mes"]);
                        //avisoGobierno.Total_cargo_mes = (decimal)dataTable.Rows[i]["m_total_cargo_mes"];
                        avisoGobierno.Telefono = (decimal)dataTable.Rows[i]["m_telefono"];
                        avisoGobierno.Fax = (decimal)dataTable.Rows[i]["m_fax"];
                        avisoGobierno.Isv = (decimal)dataTable.Rows[i]["m_isv"];
                        avisoGobierno.Isv_ant = (decimal)dataTable.Rows[i]["m_isv_ant"];
                        avisoGobierno.Numero_cortes = (decimal)dataTable.Rows[i]["m_numero_cortes"];
                        avisoGobierno.Nivel_residencial = (string)dataTable.Rows[i]["m_nivel_residencial"];
                        avisoGobierno.Bono_general = (string)dataTable.Rows[i]["m_bono_general"];
                        avisoGobierno.Ubicacion = (string)dataTable.Rows[i]["m_ubicacion"];
                        avisoGobierno.T_descripcion_tarifa = (string)dataTable.Rows[i]["t_descripcion_tarifa"];
                        avisoGobierno.S_descripcion_sector = (string)dataTable.Rows[i]["s_descripcion_sector"];
                        avisoGobierno.Fecha_emision_real = (DateTime)dataTable.Rows[i]["fecha_emision_real"];
                        avisoGobierno.Fecha_vence_real = (DateTime)dataTable.Rows[i]["fecha_vence_real"];
                        avisoGobierno.Codigo_agrupa = dataTable.Rows[i]["m_codigo_agrupa"].ToString();
                        avisoGobierno.A_descripcion = (string)dataTable.Rows[i]["a_descripcion"];
                        avisoGobierno.IMPRESION1 = dataTable.Rows[i]["IMPRESION"].ToString();
                               lstAvisoGobierno.Add(avisoGobierno);
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

            return lstAvisoGobierno;
        }

        private string generarMesActualFacturacion()
        {
            string ultimoMesFacturacion = DateTime.Today.Year.ToString();
            ultimoMesFacturacion = ultimoMesFacturacion.Remove(0, 2);
            ultimoMesFacturacion += DateTime.Today.Month.ToString();
            return ultimoMesFacturacion;
        }

        

    }
    

}
