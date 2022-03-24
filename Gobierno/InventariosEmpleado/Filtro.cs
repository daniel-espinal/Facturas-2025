using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Filtering;
using DevExpress.XtraEditors.Repository;
using DevExpress.Data.Filtering.Helpers;

namespace FacturacionGobierno
{
    public partial class Filtro : DevExpress.XtraEditors.XtraForm
    {
        public Filtro()
        {
            InitializeComponent();
        }

        private void Filtro_Load(object sender, EventArgs e)
        {
            FacturacionDataSet DSInventarios = new FacturacionDataSet();



        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            

        }


        private string cadenaFiltro;

        public string Criterios
        {
            get { return cadenaFiltro; }
            set { cadenaFiltro = value; }
        }


        /*public FilterCriteria Filtrado
        {
            get { return FiltroControl.FilterCriteria; }
            set { Filtrado = FiltroControl.FilterCriteria; }
        }*/
        //hola

        private XtraForm Padre;

        public XtraForm FormaPadre
        {
            get { return Padre; }
            set { Padre = value; }
        }





    }
}