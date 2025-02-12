using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace FacturacionGobierno
{
    public partial class EEHFacturaGobierno : DevExpress.XtraReports.UI.XtraReport
    {
        public EEHFacturaGobierno()
        {
            InitializeComponent();
        }
        private void xrPanelDatosCliente_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

    }
}
