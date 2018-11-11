<%@ Page Language="C#" AutoEventWireup="true" CodeFile="tarefa.aspx.cs" Inherits="Views_tarefa" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>GESTA - Gestão de Tarefas Acadêmicas</title>
    <script type="text/javascript" src="../Scripts/jquery-3.3.1.min.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.mask.min.js"></script>
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
                                <a class="nav-link" href="tarefa.aspx">Tarefas <span class="sr-only">(current)</span></a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" href="tarefa.aspx">Pesoas</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" href="materia.aspx">Matérias</a>
                            </li>
                        </ul>
                        <div class="form-inline my-2 my-lg-0">
                            <asp:TextBox runat="server" ID="txtBuscar" placeholder="Cód., Data, Descrição, Participante ou Matéria" CssClass="form-control mr-sm-2"></asp:TextBox>
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
                    <h1>Lista de Tarefas</h1>
                    <asp:Label ID="msgLista" runat="server"></asp:Label>
                    <asp:Table ID="listaTarefa" runat="server" Width="100%" CssClass="table table-hover">
                        <asp:TableHeaderRow>
                            <asp:TableHeaderCell></asp:TableHeaderCell>
                            <asp:TableHeaderCell>Código</asp:TableHeaderCell>
                            <asp:TableHeaderCell>Tipo</asp:TableHeaderCell>
                            <asp:TableHeaderCell>Desc. Resumida</asp:TableHeaderCell>
                            <asp:TableHeaderCell>Participantes</asp:TableHeaderCell>
                            <asp:TableHeaderCell>Materias</asp:TableHeaderCell>
                            <asp:TableHeaderCell>Dt. Limite</asp:TableHeaderCell>
                        </asp:TableHeaderRow>
                    </asp:Table>
                </asp:Panel>
                <asp:Panel ID="panelRegistro" runat="server" Visible="false">
                    <h1>Cadastro de Tarefa</h1>
                    <asp:Label ID="msgRegistro" runat="server"></asp:Label>
                    <fieldset>
                        <div class="form-group">
                            <label for="txtCodigo">Código</label>
                            <asp:HiddenField ID="hdnCodigo" runat="server" />
                            <asp:TextBox ID="txtCodigo" runat="server" CssClass="form-control" aria-describedby="codigoHelp" ReadOnly="true" Enabled="false"></asp:TextBox>
                            <small id="codigoHelp" class="form-text text-muted">Código de registro da tarefa no sistema.</small>
                        </div>

                        <div class="form-group">
                            <label for="txtDataCadastro">Data Cadastro</label>
                            <asp:TextBox ID="txtDataCadastro" runat="server" CssClass="form-control date" aria-describedby="txtDataCadastroHelp" placeholder="__/__/____" ReadOnly="true" Enabled="false"></asp:TextBox>
                        </div>

                        <div class="form-group">
                            <label for="txtDataLimite">Data Limite</label>
                            <asp:TextBox ID="txtDataLimite" runat="server" CssClass="form-control date" aria-describedby="txtDataLimiteHelp" placeholder="__/__/____"></asp:TextBox>
                            <small id="txtDataLimiteHelp" class="form-text text-muted">Data limite para entrega da tarefa.</small>
                        </div>

                        <div class="form-group">
                            <label for="txtDescResumida">Descrição Resumida</label>
                            <asp:TextBox ID="txtDescResumida" runat="server" CssClass="form-control" aria-describedby="txtDescResumidaHelp" placeholder="Descrição Resumida"></asp:TextBox>
                            <small id="txtDescResumidaHelp" class="form-text text-muted">Descreva de forma resumida o contexto do trabalho.</small>
                        </div>

                        <div class="form-group">
                            <label for="txtDescCompleta">Descrição Completa</label>
                            <asp:TextBox ID="txtDescCompleta" runat="server" TextMode="MultiLine" Rows="5" CssClass="form-control"></asp:TextBox>
                        </div>

                        <div class="form-group">
                            <label for="tipoTarefa">Papel da tarefa</label>
                            <asp:RadioButtonList ID="tipoTarefa" runat="server">
                                <asp:ListItem Text="Trabalho" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Prova" Value="2"></asp:ListItem>
                                <asp:ListItem Text="Atividade Complementar" Value="3"></asp:ListItem>
                                <asp:ListItem Text="Dependência" Value="4"></asp:ListItem>
                                <asp:ListItem Text="Outro" Value="99"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>

                        <div class="form-group">
                            <label for="txtParticipantes">Participantes</label>
                            <asp:TextBox ID="txtParticipantes" runat="server" CssClass="form-control" aria-describedby="txtParticipantesHelp" placeholder="Nome dos participantes"></asp:TextBox>
                            <small id="txtParticipantesHelp" class="form-text text-muted">Informe o nome dos participantes, separando cada um por virgula.</small>
                        </div>

                        <div class="form-group">
                            <label for="txtMateria">Matérias</label>
                            <asp:TextBox ID="txtMaterias" runat="server" CssClass="form-control" aria-describedby="txtMateriasHelp" placeholder="Nome da matéria"></asp:TextBox>
                            <small id="txtMateriasHelp" class="form-text text-muted">Caso tenha múltiplas disciplinas envolvidas, informe-as separando cada uma por virgula.</small>
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
    <script>
        $(document).ready(function(){
          $('.date').mask('00/00/0000');
        });
    </script>
</body>
</html>
