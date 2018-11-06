using BLL;
using CategoriasWeb.Utilitary;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CategoriasWeb.UI.Registros
{
    public partial class rPrestamo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                LlenarCombos();
                int id = Utils.ToInt(Request.QueryString["id"]);
                FechaTextBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
                if (id > 0)
                {
                    //Repositorio<Prestamos> repositorio = new Repositorio<Prestamos>();
                    PrestamoRepositorio repositorio = new PrestamoRepositorio();
                    Prestamos prestamo = repositorio.Search(id);

                    if (prestamo == null)
                        Mensaje(TipoMensaje.Error, "Registro No Encontrado");
                    else
                        LlenaCampos(prestamo);

                }

            }

        }

        protected void BuscarLinkButton_Click(object sender, EventArgs e)
        {
            PrestamoRepositorio repositorio = new PrestamoRepositorio();
            Prestamos prestamo = repositorio.Search(Utils.ToInt(PrestamoIdTextBox.Text));

            if (prestamo != null)
            {
                Limpiar();
                LlenaCampos(prestamo);
            }
            else
            {
                Mensaje(TipoMensaje.Error, "No Se Pudo Encontrar");
                Limpiar();
            }
        }

        protected void CalcularButton_Click(object sender, EventArgs e)
        {
            int id = 0;
            PrestamoRepositorio repositorio = new PrestamoRepositorio();

            if (PrestamoIdTextBox.Text == string.Empty)
                ViewState["PrestamosDetalles"] = repositorio.CalcularCuotas(Utils.ToInt(TiempoTextBox.Text), Utils.ToDouble(CapitalTextBox.Text), (Utils.ToDouble(InteresTextBox.Text)) / 100 / 12);
            else
                ViewState["PrestamosDetalles"] = repositorio.CalcularCuotasModificadas((List<PrestamosDetalles>)ViewState["PrestamosDetalles"], id,  Utils.ToInt(TiempoTextBox.Text), Utils.ToDouble(CapitalTextBox.Text), (Utils.ToDouble(InteresTextBox.Text)/100/12));

            DetalleGridView.DataSource = ViewState["PrestamosDetalles"];
            DetalleGridView.DataBind();



        }

        protected void NuevoButton_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        protected void GuadarButton_Click(object sender, EventArgs e)
        {
            PrestamoRepositorio repositorio = new PrestamoRepositorio();
            Prestamos prestamo = repositorio.Search(Utils.ToInt(PrestamoIdTextBox.Text));

            if (prestamo == null)
            {
                if (repositorio.Save(LlenaClase()))
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Popup", "alert('Guardado Correctamente')", true);
                    //Mensaje(TipoMensaje.Sucess, "Guardado Correctamente");
                    Limpiar();
                }
                else
                {
                    Mensaje(TipoMensaje.Error, "No Se Pudo Guardar");
                    Limpiar();
                }
            }
            else
            {
                if (repositorio.Modify(LlenaClase()))
                {
                    ScriptManager.RegisterStartupScript(Page, typeof(Page), "Popup", "alert('Modificado Correctamente')", true);
                   //Mensaje(TipoMensaje.Sucess, "Modificado Correctamente");
                    Limpiar();
                }
                else
                {
                    Mensaje(TipoMensaje.Error, "No Se Pudo Modificar");
                    Limpiar();
                }
            }


        }

        protected void EliminarButton_Click(object sender, EventArgs e)
        {

            PrestamoRepositorio repositorio = new PrestamoRepositorio();
            Prestamos prestamo = repositorio.Search(Utils.ToInt(PrestamoIdTextBox.Text));

            if (prestamo != null)
            {
                repositorio.Delete(prestamo.PrestamoId);
                Mensaje(TipoMensaje.Sucess, "Eliminado Correctamente");
                Limpiar();
            }
            else
            {
                Mensaje(TipoMensaje.Error, "No Se Pudo Eliminar");
                Limpiar();
            }
        }

        private Prestamos LlenaClase()
        {
            Prestamos prestamo = new Prestamos();

            prestamo.PrestamoId = Utils.ToInt(PrestamoIdTextBox.Text);
            prestamo.Fecha = Utils.ToDateTime(FechaTextBox.Text);
            prestamo.CuentaId = Utils.ToInt(CuentaDropDownList.SelectedValue);
            prestamo.TasaInteres = Utils.ToInt(InteresTextBox.Text);
            prestamo.Capital = Utils.ToDecimal(CapitalTextBox.Text);
            prestamo.Tiempo = Utils.ToInt(TiempoTextBox.Text);
            prestamo.Detalle = (List<PrestamosDetalles>)ViewState["PrestamosDetalles"];

            return prestamo;
        }

        private void LlenaCampos(Prestamos prestamo)
        {
            PrestamoIdTextBox.Text = prestamo.PrestamoId.ToString();
            FechaTextBox.Text = prestamo.Fecha.ToString("yyyy-MM-dd");
            CuentaDropDownList.Text = prestamo.CuentaId.ToString();
            CapitalTextBox.Text = prestamo.Capital.ToString();
            InteresTextBox.Text = prestamo.TasaInteres.ToString();
            TiempoTextBox.Text = prestamo.Tiempo.ToString();
            this.BindGrid();
        }

        protected void BindGrid()
        {
            DetalleGridView.DataSource = ((Prestamos)ViewState["PrestamosDetalles"]).Detalle;
            DetalleGridView.DataBind();
        }

        private void Limpiar()
        {
            PrestamoIdTextBox.Text = "";
            FechaTextBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
            CuentaDropDownList.SelectedIndex = 0;
            CapitalTextBox.Text = "";
            InteresTextBox.Text = "";
            TiempoTextBox.Text = "";
            ViewState["PrestamosDetalles"] = null;
        }

        private void LlenarCombos()
        {
            Repositorio<Cuentas> repositorio = new Repositorio<Cuentas>();
            CuentaDropDownList.DataSource = repositorio.GetList(c => true);
            CuentaDropDownList.DataValueField = "CuentaId";
            CuentaDropDownList.DataTextField = "Nombre";
            CuentaDropDownList.DataBind();
            CuentaDropDownList.Items.Insert(0, new ListItem("", ""));
        }

        void Mensaje(TipoMensaje tipo, string mensaje)
        {
            MensajeLabel.Text = mensaje;
            if (tipo == TipoMensaje.Sucess)
                MensajeLabel.CssClass = "alert-success";
            else
                MensajeLabel.CssClass = "alert-danger";
        }

        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (args.Value == string.Empty)
                args.IsValid = false;
            else
                args.IsValid = true;
        }
    }
}