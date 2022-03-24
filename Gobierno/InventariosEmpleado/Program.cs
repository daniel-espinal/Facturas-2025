using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.UserSkins;
using DevExpress.Skins;
using DevExpress.LookAndFeel;
using System.Globalization;
using System.Threading;
using DevExpress.XtraSplashScreen;

namespace FrmFacturacion
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(String[] args)
        {

            if (args.Length > 0)
            {
               // MessageBox.Show(args[0] + " Estado: " + args[1]);
                FacturacionGobierno.EjecucionAutomatica Software = new FacturacionGobierno.EjecucionAutomatica(args[0], Convert.ToBoolean(args[1]));
                Application.Run(Software);
            }
            else
            {
            CultureInfo culture = CultureInfo.CreateSpecificCulture("es-HN");
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                SkinManager.EnableFormSkins();
                UserLookAndFeel.Default.SetSkinStyle("DevExpress Style");
                FacturacionGobierno.FrmLogin FormaInicial = new FacturacionGobierno.FrmLogin();
                DialogResult Resultado = DialogResult.Retry;

                while (Resultado == DialogResult.Retry)
                {
                    Resultado = FormaInicial.ShowDialog();
                    if (Resultado == DialogResult.OK)
                    {
                        SplashScreenManager.ShowForm(typeof(SplashScreen1));
                        FacturacionGobierno.FrmFacturacion Software = new FacturacionGobierno.FrmFacturacion() { Usuario = FormaInicial.txtUsuario.Text };
                        Application.Run(Software);
                    }
                }
            }
        }
    }
}
