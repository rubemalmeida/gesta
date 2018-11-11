<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="Views_index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>GESTA - Gestão de Tarefas Acadêmicas</title>
    <script type="text/javascript" src="../Scripts/jquery-3.3.1.min.js"></script>
    <link rel="stylesheet" type="text/css" href="../Scripts/bootstrap.min.css" />
    <link rel="stylesheet" type="text/css" href="../Scripts/default.css" />
</head>
<body>

    <form id="form1" runat="server">
        <header>
            <nav class="navbar navbar-expand-lg navbar-dark bg-primary">
                <div class="container">
                    <a class="navbar-brand" href="#">GESTA</a>
                    <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarColor01" aria-controls="navbarColor01" aria-expanded="false" aria-label="Toggle navigation">
                        <span class="navbar-toggler-icon"></span>
                    </button>

                    <div class="collapse navbar-collapse" id="navbarColor01">
                        <ul class="navbar-nav mr-auto">
                            <li class="nav-item active">
                                <a class="nav-link" href="#">Tarefas <span class="sr-only">(current)</span></a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" href="pessoa.aspx">Pesoas</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" href="materia.aspx">Matérias</a>
                            </li>
                        </ul>
                        <div class="form-inline my-2 my-lg-0">
                            <asp:TextBox runat="server" ID="txtBuscar" placeholder="Tarefa, Pessoa ou Matéria" CssClass="form-control mr-sm-2"></asp:TextBox>
                            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-secondary my-2 my-sm-0" />
                        </div>
                    </div>
                </div>
            </nav>
        </header>
        <section>
            <div class="container">
                <h1>Lista de tarefas</h1>
                <asp:Table ID="listaTarefa" runat="server" Width="100%" CssClass="table table-hover">
                    <asp:TableHeaderRow>
                        <asp:TableHeaderCell></asp:TableHeaderCell>
                        <asp:TableHeaderCell>Código</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Nome</asp:TableHeaderCell>
                        <asp:TableHeaderCell>Papel</asp:TableHeaderCell>
                    </asp:TableHeaderRow>
                </asp:Table>
            </div>
        </section>
    </form>
</body>
</html>
