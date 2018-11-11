using gesta.Controllers;
using gesta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Views_pessoa : System.Web.UI.Page
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
                    var pessoa = PessoaController.Lista().FirstOrDefault(p => p.IdPessoa == id);
                    if(pessoa != null)
                    {
                        hdnCodigo.Value = dsId;
                        txtCodigo.Text = dsId;
                        txtNome.Text = pessoa.Nome;
                        tipoPapel.SelectedIndex = tipoPapel.Items.IndexOf(tipoPapel.Items.FindByText(Formatacao.GetDescriptionFromEnum(pessoa.Papel)));

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

    private void BuscarPessoas(string pesquisa = null)
    {
        var lista = PessoaController.Lista();
        if (!string.IsNullOrWhiteSpace(pesquisa))
        {
            pesquisa = pesquisa.ToLower();
            lista = lista.Where(p => 
                p.Nome.ToLower().Contains(pesquisa) ||
                p.IdPessoa.ToString().ToLower().Equals(pesquisa) ||
                Formatacao.GetDescriptionFromEnum(p.Papel).ToLower().Contains(pesquisa)
            ).ToList();
        }

        foreach (var reg in lista)
        {
            TableRow row = new TableRow();
            row.CssClass = "table-light";

            TableCell acao = new TableCell();
            HyperLink linkEditar = new HyperLink();
            linkEditar.Text = "Editar";
            linkEditar.NavigateUrl = $"~/Views/pessoa.aspx?Id={reg.IdPessoa}&Acao=Editar";
            acao.Controls.Add(linkEditar);

            HyperLink linkExcluir = new HyperLink();
            linkExcluir.Text = "Excluir";
            linkExcluir.CssClass = "ml-2";
            linkExcluir.NavigateUrl = $"~/Views/pessoa.aspx?Id={reg.IdPessoa}&Acao=Excluir";
            acao.Controls.Add(linkExcluir);
            row.Cells.AddAt(0, acao);

            TableCell cod = new TableCell();
            cod.Text = reg.IdPessoa.ToString("0");
            row.Cells.AddAt(1, cod);

            TableCell nome = new TableCell();
            nome.Text = reg.Nome;
            row.Cells.AddAt(2, nome);

            TableCell papel = new TableCell();
            papel.Text = Formatacao.GetDescriptionFromEnum(reg.Papel);
            row.Cells.AddAt(3, papel);

            listaPessoa.Rows.Add(row);
        }
    }

    private void LimparCampos()
    {
        msgRegistro.Text = "";
        hdnCodigo.Value = "";
        txtCodigo.Text = "";
        txtNome.Text = "";
        tipoPapel.ClearSelection();
    }

    private bool DeletarRegistro(string idPessoa)
    {
        int codigo = Convert.ToInt32(hdnCodigo.Value);
        var pessoa = PessoaController.Lista().FirstOrDefault(p => p.IdPessoa == codigo);
        if (pessoa == null) return false;
        PessoaController.Remover(pessoa);
        return true;
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        panelRegistro.Visible = false;
        panelLista.Visible = true;

        BuscarPessoas(txtBuscar.Text);
    }

    protected void btnNovoCadastro_Click(object sender, EventArgs e)
    {
        panelRegistro.Visible = true;
        panelLista.Visible = false;

        LimparCampos();
    }

    protected void Salvar_Click(object sender, EventArgs e)
    {
        string nome = txtNome.Text;
        int dsTipoPessoa = Convert.ToInt32(tipoPapel.SelectedValue);
        Papel tipoPessoa = (Papel)dsTipoPessoa;

        if (string.IsNullOrWhiteSpace(txtCodigo.Text))
        {
            PessoaController.Inseir(new Pessoa()
            {
                Nome = nome,
                Papel = tipoPessoa
            });
            msgRegistro.CssClass = "text-success";
            msgRegistro.Text = "Registo salvo com sucesso.";
            msgRegistro.Visible = true;
        }
        else
        {
            int codigo = Convert.ToInt32(hdnCodigo.Value);
            var pessoa = PessoaController.Lista().FirstOrDefault(p => p.IdPessoa == codigo);
            if (pessoa == null)
            {
                msgRegistro.CssClass = "text-danger";
                msgRegistro.Text = "Não foi possível identificar o registro.";
                msgRegistro.Visible = true;
                return;
            }
            pessoa.Nome = nome;
            pessoa.Papel = tipoPessoa;
            PessoaController.Editar(pessoa);
            msgRegistro.CssClass = "text-success";
            msgRegistro.Text = "Registo atualizado com sucesso.";
            msgRegistro.Visible = true;
        }
    }

    protected void Deletar_Click(object sender, EventArgs e)
    {
        if(!DeletarRegistro(hdnCodigo.Value))
        {
            msgRegistro.CssClass = "text-danger";
            msgRegistro.Text = "Não foi possível remover o registro.";
            msgRegistro.Visible = true;
            return;
        }

        btnBuscar_Click(null,null);

        msgLista.CssClass = "text-success";
        msgLista.Text = "Registro remivo com sucesso.";
        msgLista.Visible = true;
        
        //Response.Redirect("index.aspx", false);
    }
}