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
           
        }

        protected void BuscarLinkButton_Click(object sender, EventArgs e)
        {
            PrestamoRepositorio repositorio = new PrestamoRepositorio();
            int id = 0;

            switch (FiltroDropDownList.SelectedIndex)
            {
                case 0://Todo
                    filtro = c => true;
                    break;

                case 1://PrestamoId
                    id = Utils.ToInt(CriterioTextBox.Text);
                    filtro = c => c.PrestamoId == id;
                    break;

                case 2://Fecha
                    filtro = c => c.Fecha.Equals(CriterioTextBox.Text);
                    break;

                case 3://CuentaId
                    id = Utils.ToInt(CriterioTextBox.Text);
                    filtro = c => (c.CuentaId == id);

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