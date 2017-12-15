﻿using System;
using System.Data;
using System.Windows.Forms;

namespace CadastroClientes
{
    public partial class FrmCadastroClientes : Form
    {
        public FrmCadastroClientes()
        {
            InitializeComponent();
        }

        private void FrmCadastroClientes_Load(object sender, EventArgs e)
        {

        }

        private void btnGravar_Click(object sender, EventArgs e)
        {
            // cria dataset, que pode ser uma coleção de tabelas
            DataSet dataset = new DataSet("Dados");
            // cria a tabela
            DataTable tabela = CriarEstruturaTabela();
            // adiciona tabela ao dataset
            dataset.Tables.Add(tabela);
            // adiciona os registros na tabela
            DataRow registro = CriaRegistro(tabela);
            tabela.Rows.Add(registro);
            // salvando o cliente em um arquivo XML
            dataset.WriteXml(@".\cliente_" + txtCodigo.Text + ".xml");
        }

        private DataRow CriaRegistro(DataTable tabela)
        {
            // cria os registros
            DataRow registro = tabela.NewRow();
            registro["Codigo"] = txtCodigo.Text;
            registro["Nome"] = txtNome.Text;
            registro["Fone"] = txtTelefone.Text;
            registro["Email"] = txtEmail.Text;
            return registro;
        }

        private DataTable CriarEstruturaTabela()
        {
            DataTable tabela = new DataTable("Clientes");
            // cria colunas na tabela
            tabela.Columns.Add(new DataColumn("Codigo"));
            tabela.Columns.Add(new DataColumn("Nome"));
            tabela.Columns.Add(new DataColumn("Fone"));
            tabela.Columns.Add(new DataColumn("Email"));
            return tabela;
        }

        private void btnAbrir_Click(object sender, EventArgs e)
        {
            // cria o dataset
            DataSet dataset = new DataSet();
            // le o dataset do disco
            dataset.ReadXml(@".\cliente_" + txtCodigo.Text + ".xml");
            // tabela é o primeiro datatable da coleção
            DataTable tabela = dataset.Tables[0];
            // considero o primeiro registro da tabela
            DataRow registro = tabela.Rows[0];
            MostrarDadosTela(registro);
        }

        private void MostrarDadosTela(DataRow registro)
        {
            // mostro dados do registro na tela
            txtNome.Text = registro["Nome"].ToString();
            txtTelefone.Text = registro["Fone"].ToString();
            txtEmail.Text = registro["Email"].ToString();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            // percorrer todos os controles de tela para limpar
            foreach (Control txt in Controls)
                if (txt is TextBox)
                    (txt as TextBox).Clear();
        }
    }
}
