using BLL;
using CategoriasWeb.Utilitary;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CategoriasWeb.UI.Consultas
{
    public partial class cPrestamo : System.Web.UI.Page
    {
        Expression<Func<Prestamos, bool>> filtro = c => true;

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                DesdeTextBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
                HastaTextBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }

        protected void BuscarLinkButton_Click(object sender, EventArgs e)
        {
            Repositorio<Prestamos> repositorio = new Repositorio<Prestamos>();
            int id = 0;

            switch (FiltroDropDownList.SelectedIndex)
            {
                case 0://Todo
                    filtro = c => true && (c.Fecha >= Utils.ToDateTime(DesdeTextBox.Text) && c.Fecha <= Utils.ToDateTime(HastaTextBox.Text)) ;
                    break;

                case 1://PrestamoId
                    id = Utils.ToInt(CriterioTextBox.Text);
                    filtro = c => c.PrestamoId == id && (c.Fecha >= Utils.ToDateTime(DesdeTextBox.Text)) && (c.Fecha <= Utils.ToDateTime(HastaTextBox.Text));
                    break;

                case 2://Fecha
                    filtro = c => c.Fecha.Equals(CriterioTextBox.Text) && (c.Fecha >= Utils.ToDateTime(DesdeTextBox.Text)) && (c.Fecha <= Utils.ToDateTime(HastaTextBox.Text));
                    break;

                case 3://CuentaId
                    id = Utils.ToInt(CriterioTextBox.Text);
                    filtro = c => (c.CuentaId == id) && (c.Fecha >= Utils.ToDateTime(DesdeTextBox.Text)) && (c.Fecha <= Utils.ToDateTime(HastaTextBox.Text));

                    break;
                                  

            }

            PrestamoGridView.DataSource = repositorio.GetList(filtro);
            PrestamoGridView.DataBind();

        }

        protected void PrestamoGridView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}