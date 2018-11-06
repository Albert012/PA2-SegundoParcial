using BLL;
using Entities;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CategoriasWeb.UI.Reportes.ReportWebForms
{
    public partial class ReportCuentas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Repositorio<Cuentas> repositorio = new Repositorio<Cuentas>();

            if(!Page.IsPostBack)
            {
                CuentasReportViewer.ProcessingMode = ProcessingMode.Local;
                CuentasReportViewer.Reset();
                CuentasReportViewer.LocalReport.ReportPath = Server.MapPath(@"~\UI\Reportes\ReportCuentas.rdlc");
                CuentasReportViewer.LocalReport.DataSources.Clear();
                CuentasReportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSetCuentas", repositorio.GetList(x => true)));
                CuentasReportViewer.LocalReport.Refresh();
            }
        }
    }
}