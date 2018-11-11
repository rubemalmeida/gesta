<%@ Page Language="C#" AutoEventWireup="true" CodeFile="pessoa.aspx.cs" Inherits="Views_pessoa" %>

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
                                <a class="nav-link" href="tarefa.aspx">Tarefas</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" href="pessoa.aspx">Pesoas <span class="sr-only">(current)</span></a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" href="materia.aspx">Matérias</a>
                            </li>
                        </ul>
                        <div class="form-inline my-2 my-lg-0">
                            <asp:TextBox runat="server" ID="txtBuscar" placeholder="Código, Nome ou Papel" CssClass="form-control mr-sm-2"></asp:TextBox>
                            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-secondary my-2 my-sm-0" OnClick="btnBuscar_Click" />
                            <asp:Button ID="btnNovoCadastro" runat="server" Text="Novo Cadastro" CssClass="btn btn-primary ml-2" OnClick="btnNovoCadastro_Click" />
                        </div>
                    </div>
                </div>
            </nav>
        </header>
        <section>
            <div class="container">
                <asp:Panel ID="panelLista" runat="server" Visible="false">
                    <h1>Lista de Pessoas</h1>
                    <asp:Label ID="msgLista" runat="server"></asp:Label>
                    <asp:Table ID="listaPessoa" runat="server" Width="100%" CssClass="table table-hover">
                        <asp:TableHeaderRow>
                            <asp:TableHeaderCell></asp:TableHeaderCell>
                            <asp:TableHeaderCell>Código</asp:TableHeaderCell>
                            <asp:TableHeaderCell>Nome</asp:TableHeaderCell>
                            <asp:TableHeaderCell>Papel</asp:TableHeaderCell>
                        </asp:TableHeaderRow>
                    </asp:Table>
                </asp:Panel>
                <asp:Panel ID="panelRegistro" runat="server" Visible="false">
                    <h1>Cadastro de Pessoa</h1>
                    <asp:Label ID="msgRegistro" runat="server"></asp:Label>
                    <fieldset>
                        <div class="form-group">
                            <label for="txtCodigo">Código</label>
                            <asp:HiddenField ID="hdnCodigo" runat="server" />
                            <asp:TextBox ID="txtCodigo" runat="server" CssClass="form-control" aria-describedby="codigoHelp" ReadOnly="true" Enabled="false"></asp:TextBox>
                            <small id="codigoHelp" class="form-text text-muted">Código de registro da pessoa no sistema.</small>
                        </div>

                        <div class="form-group">
                            <label for="txtNome">Nome</label>
                            <asp:TextBox ID="txtNome" runat="server" CssClass="form-control" aria-describedby="nomeHelp" placeholder="Informe o nome"></asp:TextBox>
                            <small id="nomeHelp" class="form-text text-muted">Nome da pessoa envolvida/relacionada em alguma tarefa.</small>
                        </div>
                        <div class="form-group">
                            <label for="tipoPapel">Papel da pessoa</label>
                            <asp:RadioButtonList ID="tipoPapel" runat="server">
                                <asp:ListItem Text="Aluno" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Professor" Value="2"></asp:ListItem>
                                <asp:ListItem Text="Outro" Value="99"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                        <div class="form-inline">
                            <asp:Button ID="Salvar" runat="server" Text="Salvar" CssClass="btn btn-primary btn-lg" OnClick="Salvar_Click" />
                            <asp:Button ID="Deletar" runat="server" Text="Deletar" CssClass="btn btn-danger btn-lg ml-2" Visible="false" OnClick="Deletar_Click" />
                        </div>
                    </fieldset>
                </asp:Panel>

            </div>
        </section>
    </form>
</body>
</html>
