using gesta.Controllers;
using gesta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Views_tarefa : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
            return;

        string dsId = Request.QueryString["Id"];
        string dsAcao = Request.QueryString["Acao"];
        if (!string.IsNullOrWhiteSpace(dsId) && !string.IsNullOrWhiteSpace(dsAcao))
        {
            int id = 0;
            int.TryParse(dsId, out id);
            if (id >= 1)
            {
                if (dsAcao.Equals("Editar"))
                {
                    var tarefa = TarefaController.Lista().FirstOrDefault(p => p.IdTarefa == id);
                    if (tarefa != null)
                    {
                        hdnCodigo.Value = dsId;
                        txtCodigo.Text = dsId;
                        txtDescResumida.Text = tarefa.DescricaoResumida;
                        txtDescCompleta.Text = tarefa.DescricaoCompleta;
                        tipoTarefa.SelectedIndex = tipoTarefa.Items.IndexOf(tipoTarefa.Items.FindByText(Formatacao.GetDescriptionFromEnum(tarefa.TipoTarefa)));
                        txtDataCadastro.Text = tarefa.DataCadastro.ToString("dd/MM/yyyy HH:mm");
                        txtDataLimite.Text = tarefa.DataLimite.ToString("dd/MM/yyyy HH:mm");
                        txtParticipantes.Text = string.Join(",", tarefa.ParticipantesRemoto.Select(p => p.Nome));
                        txtMaterias.Text = string.Join(",", tarefa.MateriasRemoto.Select(p => p.Nome));

                        panelRegistro.Visible = true;
                        panelLista.Visible = false;
                        Deletar.Visible = true;
                        return;
                    }
                }

                if (dsAcao.Equals("Excluir"))
                {
                    btnBuscar_Click(null, null);

                    if (!DeletarRegistro(dsId))
                    {
                        msgRegistro.CssClass = "text-danger";
                        msgRegistro.Text = "Não foi possível remover o registro.";
                        msgRegistro.Visible = true;
                        return;
                    }
                    //Response.Redirect("index.aspx", false);
                }
            }
        }
        btnBuscar_Click(null, null);
    }

    private void BuscarTarefas(string pesquisa = null)
    {
        var lista = TarefaController.Lista();
        if (!string.IsNullOrWhiteSpace(pesquisa))
        {
            pesquisa = pesquisa.ToLower();
            lista = lista.Where(p =>
                p.IdTarefa.ToString().Equals(pesquisa) ||
                p.DescricaoResumida.ToLower().Contains(pesquisa) ||
                p.DescricaoCompleta.ToLower().Contains(pesquisa) ||
                Formatacao.GetDescriptionFromEnum(p.TipoTarefa).ToLower().Contains(pesquisa)
            ).ToList();
        }

        foreach (var reg in lista)
        {
            TableRow row = new TableRow();
            row.CssClass = "table-light";

            TableCell acao = new TableCell();
            HyperLink linkEditar = new HyperLink();
            linkEditar.Text = "Editar";
            linkEditar.NavigateUrl = $"~/Views/tarefa.aspx?Id={reg.IdTarefa}&Acao=Editar";
            acao.Controls.Add(linkEditar);

            HyperLink linkExcluir = new HyperLink();
            linkExcluir.Text = "Excluir";
            linkExcluir.CssClass = "ml-2";
            linkExcluir.NavigateUrl = $"~/Views/tarefa.aspx?Id={reg.IdTarefa}&Acao=Excluir";
            acao.Controls.Add(linkExcluir);
            row.Cells.AddAt(0, acao);

            TableCell cod = new TableCell();
            cod.Text = reg.IdTarefa.ToString("0");
            row.Cells.AddAt(1, cod);

            TableCell papel = new TableCell();
            papel.Text = Formatacao.GetDescriptionFromEnum(reg.TipoTarefa);
            row.Cells.AddAt(2, papel);

            TableCell descResumida = new TableCell();
            if (reg.DescricaoResumida.Length < 25)
                descResumida.Text = reg.DescricaoResumida;
            else
                descResumida.Text = $"{reg.DescricaoResumida.Substring(0, 23)}...";
            row.Cells.AddAt(3, descResumida);

            TableCell participantes = new TableCell();
            participantes.Text = string.Join(",", reg.ParticipantesRemoto);
            row.Cells.AddAt(4, participantes);
            
            TableCell materias = new TableCell();
            materias.Text = string.Join(",", reg.MateriasRemoto);
            row.Cells.AddAt(5, materias);

            TableCell dataLimit = new TableCell();
            dataLimit.Text = reg.DataLimite.ToString("dd/MM/yyyy");
            row.Cells.AddAt(6, dataLimit);

            listaTarefa.Rows.Add(row);
        }
    }

    private void LimparCampos()
    {
        msgRegistro.Text = "";
        hdnCodigo.Value = "";
        txtCodigo.Text = "";
        txtDataCadastro.Text = "";
        txtDataLimite.Text = "";
        txtDescResumida.Text = "";
        txtDescCompleta.Text = "";
        tipoTarefa.ClearSelection();
        txtParticipantes.Text = "";
        txtMaterias.Text = "";
    }

    private bool DeletarRegistro(string idTarefa)
    {
        int codigo = Convert.ToInt32(hdnCodigo.Value);
        var tarefa = TarefaController.Lista().FirstOrDefault(p => p.IdTarefa == codigo);
        if (tarefa == null) return false;
        TarefaController.Remover(tarefa);
        return true;
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        panelRegistro.Visible = false;
        panelLista.Visible = true;

        BuscarTarefas(txtBuscar.Text);
    }

    protected void btnNovoCadastro_Click(object sender, EventArgs e)
    {
        panelRegistro.Visible = true;
        panelLista.Visible = false;

        LimparCampos();
    }

    protected void Salvar_Click(object sender, EventArgs e)
    {
        int dsTipoTarefa = Convert.ToInt32(tipoTarefa.SelectedValue);
        TipoTarefa auxTipoTarefa = (TipoTarefa)dsTipoTarefa;

        if (string.IsNullOrWhiteSpace(txtCodigo.Text))
        {
            var listaParticipantes = new List<Pessoa>();
            foreach (var nomePessoa in txtParticipantes.Text.Split(',').ToList<string>())
            {
                Pessoa pessoa = PessoaController.Lista().FirstOrDefault(p => p.Nome.Equals(nomePessoa));
                if (pessoa == null || !(pessoa.IdPessoa >= 1))
                {
                    pessoa = PessoaController.Inseir(new Pessoa()
                    {
                        Nome = nomePessoa,
                        Papel = Papel.Outros
                    });
                }
                listaParticipantes.Add(pessoa);
            }

            var listaMaterias = new List<Materia>();
            foreach (var nomeMateria in txtParticipantes.Text.Split(',').ToList<string>())
            {
                Materia materia = MateriaController.Lista().FirstOrDefault(p => p.Nome.Equals(nomeMateria));
                if (materia == null || !(materia.IdMateria >= 1))
                {
                    materia = MateriaController.Inseir(new Materia() { Nome = nomeMateria });
                }
                listaMaterias.Add(materia);
            }

            TarefaController.Inseir(new Tarefa()
            {
                DataCadastro = DateTime.Now,
                DataLimite = Convert.ToDateTime(txtDataLimite.Text),
                DescricaoResumida = txtDescResumida.Text,
                DescricaoCompleta = txtDescCompleta.Text,
                TipoTarefa = auxTipoTarefa,
                Participantes = listaParticipantes,
                Materias = listaMaterias
            });

            msgRegistro.CssClass = "text-success";
            msgRegistro.Text = "Registo salvo com sucesso.";
            msgRegistro.Visible = true;
        }
        else
        {
            int codigo = Convert.ToInt32(hdnCodigo.Value);
            var tarefa = TarefaController.Lista().FirstOrDefault(p => p.IdTarefa == codigo);
            if (tarefa == null)
            {
                msgRegistro.CssClass = "text-danger";
                msgRegistro.Text = "Não foi possível identificar o registro.";
                msgRegistro.Visible = true;
                return;
            }

            tarefa.DataLimite = Convert.ToDateTime(txtDataLimite.Text);
            tarefa.DescricaoResumida = txtDescResumida.Text;
            tarefa.DescricaoCompleta = txtDescCompleta.Text;
            tarefa.TipoTarefa = auxTipoTarefa;

            TarefaController.Editar(tarefa);

            msgRegistro.CssClass = "text-success";
            msgRegistro.Text = "Registo atualizado com sucesso.";
            msgRegistro.Visible = true;
        }
    }

    protected void Deletar_Click(object sender, EventArgs e)
    {
        if (!DeletarRegistro(hdnCodigo.Value))
        {
            msgRegistro.CssClass = "text-danger";
            msgRegistro.Text = "Não foi possível remover o registro.";
            msgRegistro.Visible = true;
            return;
        }

        btnBuscar_Click(null, null);

        msgLista.CssClass = "text-success";
        msgLista.Text = "Registro remivo com sucesso.";
        msgLista.Visible = true;

        //Response.Redirect("index.aspx", false);
    }
}