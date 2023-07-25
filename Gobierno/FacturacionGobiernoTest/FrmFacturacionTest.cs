using FacturacionGobierno;
using FacturacionGobierno.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionGobiernoTest
{
    [TestClass]
    public  class FrmFacturacionTest
    {

        [TestMethod]
        public void GenerarFacturacionTest()
        {
            FacturaDEV rep = MGeneraReporte();
           
        }

        private FacturaDEV MGeneraReporte()
        {
            Facturacion facturacion = new Facturacion();

            AvisoGobierno avisoGobierno = new AvisoGobierno();
            HistoricoConsumo historicoConsumo = new HistoricoConsumo();
            CargosVarios cargosVarios = new CargosVarios();
            Reactiva reactiva = new Reactiva();

            FacturaDEV reporte = new FacturaDEV();

            try
            {                
                DataSet dsReporte = MToDataSet(historicoConsumo.obtenerHistoricoConsumo(), "Historico_Consumo");
                dsReporte.Reset();
                reporte.DataSource = dsReporte;
                dsReporte.WriteXmlSchema(@"D:\Reporte_Historico_Consumo.xsd");
                /*
                 dsReporte = MToDataSet(historicoConsumo.obtenerHistoricoConsumo(), "Historico_Consumo");
                reporte.DataSource = dsReporte;
                dsReporte.WriteXmlSchema(@"D:\Reporte_Historico_Consumo.xsd");

                dsReporte = MToDataSet(cargosVarios.ObtenerCargosVarios("1710"), "Cargos_Varios");
                reporte.DataSource = dsReporte;
                dsReporte.WriteXmlSchema(@"D:\Reporte_Cargos_Variosa.xsd");

                dsReporte = MToDataSet(reactiva.ObtenerReactiva("1709"), "Reactiva");
                reporte.DataSource = dsReporte;
                dsReporte.WriteXmlSchema(@"D:\Reporte_Reactiva.xsd");
                */


                return reporte;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }

        }

        public DataSet MToDataSet<T>(IList<T> list, string tableName)
        {
            DataSet ds = new DataSet();
            for (int i = 0; i< list.Count; i++)
            {

                T tt = list[i];
                Type elementType = typeof(T);
            
                DataTable t = new DataTable(tableName);
                ds.Tables.Add(t);

                //add a column to table for each public property on T
                foreach (var propInfo in elementType.GetProperties())
                {
                    Type ColType = Nullable.GetUnderlyingType(propInfo.PropertyType) ?? propInfo.PropertyType;

                    t.Columns.Add(propInfo.Name, ColType);
                }

                //go through each property on T and add each value to the table
                foreach (T item in list)
                {
                    DataRow row = t.NewRow();

                    foreach (var propInfo in elementType.GetProperties())
                    {
                        row[propInfo.Name] = propInfo.GetValue(item, null) ?? DBNull.Value;
                    }

                    t.Rows.Add(row);
                }
            }
            return ds;
        }
    }
}
