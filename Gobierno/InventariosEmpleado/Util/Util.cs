using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionGobierno.Util
{
    public class Util
    {

        public const string TIPO_FACTURA_MASIVA = @"MASIVA";
        // public const string FACTURACION_DBO_FPACGOB = @"SELECT TOP (2) [hc_ciclo],[hc_clave_abonado],[hc_cod_trans]  FROM [FACTURACION].[dbo].[fpacgob];";
        public const string FACTURACION_DBO_FPACGOB = @"SELECT TOP (2) [hc_ciclo],[hc_clave_abonado],[hc_cod_trans]  FROM [FACTURACION].[dbo].[fpacgob] WHERE hc_clave_abonado = @Clave and hc_cod_trans = @Trans;";

        public const string CODIGO_AGRUPACION_GOB_CAB_PADRE = @"SELECT P.CodAgrupacion, P.NomIns FROM [SAGA].[dbo].[Grupo] AS P order by P.CodAgrupacion asc;";
        public const string EXISTE_PERIODO_FACTURACION = @"SELECT *  FROM [FACTURACION].[dbo].[Fact_Correlativo] WHERE PERIODO IN(@PeriodoAnt, @PeriodoAct) AND ESTADO = 'A' ORDER BY CONTADOR_INICIAL DESC; ";
        
        public const string CREAR_REGISTRO_NUEVO_PERIODO = "INSERT INTO [dbo].[Fact_Correlativo]" +
                        "           ([TIPO_FACTURA] " +
                        "           ,[PERIODO] " +
                        "           ,[CONTADOR_INICIAL]" +
                        "           ,[CONTADOR_FINAL]" +
                        "           ,[ESTADO]" +
                        "           ,[CLAVE]" +
                        "           ,[FECHA]" +
                        "           ,[DESCRIPCION])" +
                        "     VALUES" +
                        "           (@GENERAL" +
                        "           ,@PERIODO" +
                        "           ,@CONTADORI" +
                        "           ,@CONTADORF" +
                        "           ,@ESTADO" +
                        "           ,@CLAVE" +
                        "           ,@FECHA" +
                        "           ,@DESCRIPCION)";

        public const string CREAR_REGISTRO_DETALLE_FACTURACION = "INSERT INTO [dbo].[Fact_Det_Correlativo]" +
                        "           ([PERIODO]" +
                        "           ,[N_FACTURA]" +
                        "           ,[TIPO_FACTURA]" +
                        "           ,[CLAVE]" +
                        "           ,[PREFIJO]" +
                        "           ,[FECHA]" +
                        "           ,[ESTADO]" +
                        "           ,[CANTIDAD])" +
                        "     VALUES" +
                        "           (@PERIODO" +
                        "           ,@N_FACTURA" +
                        "           ,@TIPO_FACTURA" +
                        "           ,@CLAVE" +
                        "           ,@PREFIJO" +
                        "           ,@FECHA" +
                        "           ,@ESTADO" +
                        "           ,@CANTIDAD)";

        public const string ACTUALIZAR_PERIODO_ACTUAL = @"Update Fact_Correlativo set CONTADOR_FINAL = @ContadorFinal,  ESTADO = @Estado where PERIODO = @Periodo AND TIPO_FACTURA = @TipoFactura";

        public const string ACTUALIZAR_PERIODO_ANTERIOR = @"UPDATE Fact_Correlativo set ESTADO = @Estado where PERIODO = @Periodo AND TIPO_FACTURA = @TipoFactura";

        public const string SP_AVISO_GOBIERNO = "[dbo].[EEHAviso_Gobierno]";

        public const string SP_HISTORICO_CONSUMO = "[dbo].[EEHHistorico_Consumo]";

        public const string SP_CARGOS_VARIOS = "[dbo].[EEHCargos_Varios]";

        public const string SP_REACTIVA = "[dbo].[EEHReactiva]";

        public const string SP_CODIGOS_AGRUPACION = "[dbo].[EEHCodigos_Agrupacion]";

    }
}
