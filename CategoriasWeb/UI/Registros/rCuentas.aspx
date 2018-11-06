<%@ Page Title="Cuentas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="rCuentas.aspx.cs" Inherits="CategoriasWeb.UI.Registros.rCuentas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="form-row">
        <%--CuentaId--%>
        <div class="form-group col-md-3">
            <asp:Label Text="Cuenta Id" class="text-primary" runat="server" />
            <asp:TextBox ID="CuentaIdTextBox" class="form-control input-group" TextMode="Number" placeholder="0" runat="server" />

        </div>

        <%--Boton--%>
        <div class="col-lg-1 p-0">
            <asp:LinkButton ID="BuscarLinkButton" CssClass="btn btn-outline-info mt-4" runat="server" OnClick="BuscarLinkButton_Click">
                <span class="fas fa-search"></span>
                     Buscar
            </asp:LinkButton>
        </div>

    </div>

    <div class="form-row">
        <%--Fecha--%>
        <div class="form-group col-md-3">
            <asp:Label Text="Fecha" runat="server" />
            <asp:TextBox ID="FechaTextBox" class="form-control input-group" TextMode="Date" runat="server" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="FechaTextBox" ErrorMessage="Campo Fecha obligatorio" ForeColor="Red" Display="Dynamic" SetFocusOnError="True" ToolTip="Campo Fecha obligatorio">Por favor llenar el campo Fecha </asp:RequiredFieldValidator>
        </div>
    </div>

    <div class="form-row">
        <%--Nombre--%>
        <div class="form-group col-md-3">
            <asp:Label Text="Nombre" runat="server" />
            <asp:TextBox ID="NombreTextBox" class="form-control input-sm" runat="server" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"  ControlToValidate="NombreTextBox" ErrorMessage="Campo Nombre obligatorio" ForeColor="Red" Display="Dynamic" SetFocusOnError="True" ToolTip="Campo Nombre obligatorio">Por favor llenar el campo Nombre </asp:RequiredFieldValidator>
        </div>
    </div>

    <div class="form-row">
        <%--Balance--%>
        <div class="form-group col-md-3">
            <asp:Label Text="Balance" runat="server" />
            <asp:TextBox ID="BalanceTextBox" TextMode="Number" ReadOnly="true" class="form-control input-sm" placeholder="0" runat="server" />
        </div>

    </div>

    <asp:Label ID="MensajeLabel" runat="server" Text=""></asp:Label>

    <%--Botones--%>
    <div class="panel-footer">
        <div class="text-center">
            <div class="form-group" style="display: inline-block">
                <asp:Button Text="New" class="btn btn-outline-info btn-md" runat="server" ID="NuevoButton" OnClick="NuevoButton_Click" />
                <asp:Button Text="Save" class="btn btn-outline-success btn-md" runat="server" ID="GuadarButton" OnClick="GuadarButton_Click" />
                <asp:Button Text="Delete" class="btn btn-outline-danger btn-md" runat="server" ID="EliminarButton" OnClick="EliminarButton_Click" />

            </div>
        </div>
    </div>


</asp:Content>
