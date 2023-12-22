<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Filtro.aspx.cs" Inherits="INFO_104_Proyecto2_Grupo3.asp.Filtro" %>

<!DOCTYPE html>
<!--INFO-104. Proyecto 2. Grupo 3 -->
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/estiloGrid.css" rel="stylesheet" />
    <link href="css/estiloHeader.css" rel="stylesheet" />
    <link href="css/estiloFooter.css" rel="stylesheet" />
    <link href="css/estiloBoton.css" rel="stylesheet" />
     <link href="css/estiloAccesoFondo.css" rel="stylesheet" />
    <link href="css/estiloNavFiltros.css" rel="stylesheet" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-T3c6CoIi6uLrA9TneNEoa7RxnatzjcDSCmG1MXxSR1GAsXEV/Dwwykc2MPK8M2HN" crossorigin="anonymous"/>
        <style type="text/css">
        .estiloFiltro {
            font-family:"Segoe UI";
            text-align: center;
            
        }
            .auto-style1 {
                text-align: center;
                width: 93px;
            }
    </style>
</head>
<body>
    <form runat="server">   
            <nav>
              <div class="navicon">
                <div></div>
              </div>
              <a style="text-decoration:none" href="Inicio.aspx">Volver a Inicio</a>
            </nav>
        <header>
            <h2 class="estiloFiltro">&nbsp;</h2>
            <h2 class="estiloFiltro">&nbsp;</h2>
            <h2 class="estiloFiltro">Busqueda por filtro</h2>
        </header>


        <div class="text-center">
                     <div class="estiloFiltro">
                        <br />
                        <br />
                        <asp:GridView runat="server" ID="datagrid" PageSize="10" HorizontalAlign="Center"
                            CssClass="mydatagrid" PagerStyle-CssClass="pager" HeaderStyle-CssClass="header"
                            RowStyle-CssClass="rows" AllowPaging="True" />
                        <br />
                        <br />
                    </div>
          <table class="w-50" align="center">
              <tr>
                  <asp:DropDownList ID="dropFiltro" runat="server" DataSourceID="SQLrfiltro" DataTextField="tipo" DataValueField="tipo"></asp:DropDownList>
                  <asp:SqlDataSource ID="SQLrfiltro" runat="server" ConnectionString="<%$ ConnectionStrings:conexion %>" SelectCommand="SELECT [tipo] FROM [filtros]"></asp:SqlDataSource>
              </tr>
             <tr>
                <td class="estiloFiltro">Codigo</td>
                <td class="estiloFiltro">
                    <br />
                    <asp:TextBox ID="tCodigo" runat="server"></asp:TextBox>
                    <br />
                 </td>
            </tr>
          </table>
    </div>
    <div>
        <table class="w-50" align="center">
            <tr align="center">
                <td >
                    <br />
                    <br />
                    <asp:Button ID="BttConsultar" runat="server" Text="Filtrar" CssClass="button button1" OnClick="BttConsultar_Click"/></td>
            </tr>
            <tr align="center">
                <td >&nbsp;</td>
                <td colspan="2" >&nbsp;</td>
                <td >&nbsp;</td>
            </tr>
        </table>
    </div>
    </form>

    <section>

        <table align="center" class="w-50">
            <tr>
                <td class="auto-style1">&nbsp;</td>
                <td><strong>Tipo de Busqueda</strong></td>
            </tr>
            <tr>
                <td class="auto-style1"><strong>Filtro 1</strong></td>
                <td>Usuario y Equipo</td>
            </tr>
            <tr>
                <td class="auto-style1"><strong>Filtro 2</strong></td>
                <td>Usuario, Equipo y Reparo</td>
            </tr>
            <tr>
                <td class="auto-style1"><strong>Filtro 3</strong></td>
                <td>Usuario, Equipo, Reparo y Asignacion</td>
            </tr>
            <tr>
                <td class="auto-style1"><strong>Filtro 4</strong></td>
                <td>Usuario, Equipo, Reparo, Asignacion y Tecnico</td>
            </tr>
            <tr>
                <td class="auto-style1"><strong>Filtro 5</strong></td>
                <td>Usuario, Equipo, Reparo, Asignacion, Tecnico y Detalles</td>
            </tr>
        </table>

    </section>
</body>
</html>
