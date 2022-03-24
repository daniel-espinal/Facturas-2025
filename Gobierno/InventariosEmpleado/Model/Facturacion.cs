using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionGobierno.Model
{
   public class Facturacion
    {   
        String contador_factura;
        String mesFacturacion;

        List<AvisoGobierno> lstAvisoGobierno;
        List<HistoricoConsumo> lstHistoricoConsumo;
        List<CargosVarios> lstCargosVarios;
        List<Reactiva> lstReactiva;
        List<CodigosAgrupacion> lstCodigoAgrupacion;


        public Facturacion()
        {
            //String[] config = ConfigurationSettings.AppSettings.GetValues("CONTADOR_FACTURA");
            //this.contador_factura = config[0];

            this.mesFacturacion = generarMesActualFacturacion();
        }      

      
        public string Contador_factura { get => contador_factura; set => contador_factura = value; }
        public string MesFacturacion { get => mesFacturacion; set => mesFacturacion = value; }
        public List<AvisoGobierno> LstAvisoGobierno { get => lstAvisoGobierno; set => lstAvisoGobierno = value; }
        public List<HistoricoConsumo> LstHistoricoConsumo { get => lstHistoricoConsumo; set => lstHistoricoConsumo = value; }
        public List<CargosVarios> LstCargosVarios { get => lstCargosVarios; set => lstCargosVarios = value; }
        public List<Reactiva> LstReactiva { get => lstReactiva; set => lstReactiva = value; }
        public List<CodigosAgrupacion> LstCodigoAgrupacion { get => lstCodigoAgrupacion; set => lstCodigoAgrupacion = value; }

        private string generarMesActualFacturacion()
        {
            string ultimoMesFacturacion = DateTime.Today.Year.ToString();
            ultimoMesFacturacion = ultimoMesFacturacion.Remove(0, 2);
            ultimoMesFacturacion += DateTime.Today.Month.ToString();
            return ultimoMesFacturacion;
        }
    }
}
