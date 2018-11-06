<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportCuentas.aspx.cs" Inherits="CategoriasWeb.UI.Reportes.ReportWebForms.ReportCuentas" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <rsweb:ReportViewer ID="CuentasReportViewer" runat="server" ProcessingMode="Remote" Height="100%" Width="100%" >
               
            </rsweb:ReportViewer>

        </div>
    </form>
</body>
</html>
