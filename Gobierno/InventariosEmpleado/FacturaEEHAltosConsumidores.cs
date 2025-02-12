using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace FacturacionGobierno
{
    public partial class FacturaEEHAltosConsumidores : DevExpress.XtraReports.UI.XtraReport
    {
        public FacturaEEHAltosConsumidores()
        {
            InitializeComponent();
        }

        private void xrPanelDatosCliente_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void xrTableCell10_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {

        }

        private void FacturaEEHAltosConsumidores_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            var banda = GetCurrentRow();

        }
    }
}
