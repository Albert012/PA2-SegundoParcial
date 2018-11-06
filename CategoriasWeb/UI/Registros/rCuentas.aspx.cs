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
    public partial class rCuentas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                int id = Utils.ToInt(Request.QueryString["id"]);
                FechaTextBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
                if(id > 0)
                {
                    Repositorio<Cuentas> repositorio = new Repositorio<Cuentas>();
                    var cuenta = repositorio.Search(id);

                    if (cuenta == null)
                        Mensaje(TipoMensaje.Error, "No Found");
                    else
                        LlenaCampos(cuenta);
                }
            }
        }

        protected void BuscarLinkButton_Click(object sender, EventArgs e)
        {
            Repositorio<Cuentas> repositorio = new Repositorio<Cuentas>();
            Cuentas cuenta = repositorio.Search(Utils.ToInt(CuentaIdTextBox.Text));

            if (cuenta != null)
            {
                Limpiar();
                LlenaCampos(cuenta);
            }
            else
            {
                Mensaje(TipoMensaje.Error, "No Found");
                Limpiar();
            }
        }

        protected void NuevoButton_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        protected void GuadarButton_Click(object sender, EventArgs e)
        {
            Repositorio<Cuentas> repositorio = new Repositorio<Cuentas>();
            Cuentas cuenta = repositorio.Search(Utils.ToInt(CuentaIdTextBox.Text));

            if (cuenta == null)
            {
                if (repositorio.Save(LlenaClase()))
                {
                    //Mensaje(TipoMensaje.Sucess, "Guardado Correctamente");
                    Utils.MostraMensaje(this, "Guardado Correctamente", "Exito", "info");
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
                    Mensaje(TipoMensaje.Sucess, "Modificado Correctamente");
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
            Repositorio<Cuentas> repositorio = new Repositorio<Cuentas>();
            Cuentas cuenta = repositorio.Search(Utils.ToInt(CuentaIdTextBox.Text));

            if (cuenta != null)
            {
                repositorio.Delete(cuenta.CuentaId);
                Mensaje(TipoMensaje.Sucess, "Eliminado Correctamente");
                Limpiar();
            }
            else
            {
                Mensaje(TipoMensaje.Error, "No Se Pudo Eliminar");
                Limpiar();
            }
        }

        private Cuentas LlenaClase()
        {
            Cuentas cuenta = new Cuentas();
            cuenta.CuentaId = Utils.ToInt(CuentaIdTextBox.Text);
            cuenta.Fecha = Utils.ToDateTime(FechaTextBox.Text);
            cuenta.Nombre = NombreTextBox.Text;
            cuenta.Balance = Utils.ToDecimal(BalanceTextBox.Text);
            return cuenta;
        }

        private void LlenaCampos(Cuentas cuenta)
        {
            CuentaIdTextBox.Text = cuenta.CuentaId.ToString();
            FechaTextBox.Text = cuenta.Fecha.ToString("yyyy-MM-dd");
            NombreTextBox.Text = cuenta.Nombre;
            BalanceTextBox.Text = cuenta.Balance.ToString();
        }

        private void Limpiar()
        {
            CuentaIdTextBox.Text = "";
            FechaTextBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
            NombreTextBox.Text = "";
            BalanceTextBox.Text = "";
        }

        void MostrarMensaje()
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "toastr_message", script: "toastr['success']('Guardado');", addScriptTags: true);

        }
        void Mensaje(TipoMensaje tipo, string mensaje)
        {
            MensajeLabel.Text = mensaje;
            if (tipo == TipoMensaje.Sucess)
                MensajeLabel.CssClass = "alert-success";
            else
                MensajeLabel.CssClass = "alert-danger";
        }

    }
}