using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using gesta.Controllers;
using gesta.Models;

public partial class Views_index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            var lista = PessoaController.Lista();

            foreach (var reg in lista)
            {
                TableRow row = new TableRow();
                row.CssClass = "table-light";

                TableCell acao = new TableCell();
                HyperLink linkEditar = new HyperLink();
                linkEditar.Text = "Editar";
                linkEditar.NavigateUrl = $"~/index.aspx?Id={reg.IdPessoa}&Acao=Editar";
                acao.Controls.Add(linkEditar);

                HyperLink linkExcluir = new HyperLink();
                linkExcluir.Text = "Excluir";
                linkExcluir.CssClass = "ml-2";
                linkExcluir.NavigateUrl = $"~/index.aspx?Id={reg.IdPessoa}&Acao=Excluir";
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

                listaTarefa.Rows.Add(row);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}