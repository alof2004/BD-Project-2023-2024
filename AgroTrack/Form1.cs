using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AgroTrack
{
    public partial class AgroTrack : Form
    {
        private SqlConnection cn;

        public AgroTrack()
        {
            InitializeComponent();
            verifySGBDConnection();
            LoadQuinta();
            LoadEmpresa();
            LoadProdutos();
            AgricultoresTab.Dock = DockStyle.Fill;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cn = getSGBDConnection();
            verifySGBDConnection();
        }

        private SqlConnection getSGBDConnection()
        {
            return new SqlConnection("data source = tcp:mednat.ieeta.pt\\SQLSERVER, 8101; Initial Catalog = p8g3; uid = p8g3; password = UmMilhaoDeEuros1#");
        }

        private bool verifySGBDConnection()
        {
            if (cn == null)
                cn = getSGBDConnection();

            if (cn.State != ConnectionState.Open)
                cn.Open();

            return cn.State == ConnectionState.Open;
        }

        private void LoadQuinta()
        {
            string query = "SELECT Codigo_quinta, Empresa_Id_Empresa, Nome, Morada, Contacto FROM AgroTrack.Quinta;";
            SqlCommand cmd = new SqlCommand(query, cn);

            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Quinta farm = new Quinta
                    {
                        Id_Quinta = (int)reader["Empresa_Id_Empresa"],
                        Nome = reader["Nome"].ToString(),
                        Morada = reader["Morada"].ToString(),
                        Empresa_Id_Empresa = (int)reader["Codigo_quinta"], // Assuming Rua should be Codigo_quinta
                        Contacto = (int)reader["Contacto"]
                    };

                    ListaQuintas.Items.Add(farm);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to retrieve data from database: " + ex.Message);
            }
        }

        private void ListaQuintas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ListaQuintas.SelectedItem is Quinta selectedFarm)
            {
                QuintaNome.ReadOnly = true;
                QuintaMorada.ReadOnly = true;
                QuintaContacto.ReadOnly = true;

                QuintaNome.Text = selectedFarm.Nome;
                QuintaMorada.Text = selectedFarm.Morada;
                QuintaContacto.Text = selectedFarm.Contacto.ToString();

                LoadAnimals(selectedFarm.Empresa_Id_Empresa);
                loadAgricultores(selectedFarm.Empresa_Id_Empresa);
                loadProdutosQuinta(selectedFarm.Empresa_Id_Empresa);

            }
        }

        private void LoadAnimals(int empresaId)
        {
            string query = "SELECT Id_Animal, Tipo_de_Animal, Idade, Brinco FROM AgroTrack.AnimaisQuinta WHERE Empresa_Id_Empresa = @EmpresaId;";
            SqlCommand cmd = new SqlCommand(query, cn);
            cmd.Parameters.AddWithValue("@EmpresaId", empresaId);

            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                Animais.Items.Clear(); // Clear previous items
                while (reader.Read())
                {
                    Animal animal = new Animal
                    {
                        Id = reader["Id_Animal"].ToString(),
                        Tipo = reader["Tipo_de_Animal"].ToString(),
                        Idade = reader["Idade"].ToString(),
                        Brinco = reader["Brinco"].ToString()
                    };

                    Animais.Items.Add(animal);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to retrieve animals from database: " + ex.Message);
            }
        }

        private void loadProdutosQuinta(int empresaId)
        {

            string query = "SELECT Codigo, Nome, Preco, Taxa_de_iva, Unidade_medida, Tipo_de_Produto, Quantidade FROM AgroTrack.QuintaProduto WHERE Codigo_quinta = @EmpresaId;";
            SqlCommand cmd = new SqlCommand(query, cn);
            cmd.Parameters.AddWithValue("@EmpresaId", empresaId);
            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                ProdutosQuinta.Items.Clear(); // Clear previous items
                while (reader.Read())
                {
                    ProdutosQuinta produto = new ProdutosQuinta
                    {
                        Id_Quinta = empresaId,
                        Produto = reader["Nome"].ToString(),
                        Id_Produto = (int)reader["Codigo"],
                        Quantidade = (int)reader["Quantidade"]

                    };
                    ProdutosQuinta.Items.Add(produto);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to retrieve animals from database: " + ex.Message);
            }

        }



        private void loadAgricultores(int empresaId)
        {
            string query = "SELECT Id_Trabalhador, Pessoa_N_CartaoCidadao, Quinta_Empresa_Id_Empresa, Codigo_quinta, Empresa_Id_Empresa, Nome, Contacto FROM AgroTrack.AgriculQuinta WHERE Codigo_quinta = @CodigoQuinta";
            SqlCommand cmd = new SqlCommand(query, cn);
            cmd.Parameters.AddWithValue("@CodigoQuinta", empresaId);

            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                Agricultores.Items.Clear(); // Clear previous items
                while (reader.Read())
                {
                    Agricultores agricultores = new Agricultores
                    {
                        Id_Trabalhador = (int)reader["Id_Trabalhador"],
                        Pessoa_N_CartaoCidadao = (int)reader["Pessoa_N_CartaoCidadao"],
                        Quinta_Empresa_Id_Empresa = (int)reader["Quinta_Empresa_Id_Empresa"],
                        Nome = reader["Nome"].ToString(),
                        Contacto = (int)reader["Contacto"]
                    };
                    Agricultores.Items.Add(agricultores);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to retrieve animals from database: " + ex.Message);
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged_2(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void LoadEmpresa()
        {
            string query = "SELECT Id_Empresa,Nome,Morada, Contacto, Tipo_De_Empresa FROM AgroTrack.Empresa;";
            SqlCommand cmd = new SqlCommand(query, cn);

            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Empresa Company = new Empresa
                    {
                        Id_Empresa = (int)reader["Id_Empresa"],
                        Nome = reader["Nome"].ToString(),
                        Morada = reader["Morada"].ToString(),
                        Contacto = (int)reader["Contacto"],
                        TipoEmpresa = reader["Tipo_De_Empresa"].ToString(),
                    };

                    ListaEmpresas.Items.Add(Company);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to retrieve data from database: " + ex.Message);
            }
        }

        private void ListaEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ListaEmpresas.SelectedItem is Empresa selectedCompany)
            {
                Nome.ReadOnly = true;
                Morada.ReadOnly = true;
                Contacto.ReadOnly = true;
                TipoEmpresa.ReadOnly = true;

                Nome.Text = selectedCompany.Nome;
                Morada.Text = selectedCompany.Morada;
                Contacto.Text = selectedCompany.Contacto.ToString();
                TipoEmpresa.Text = selectedCompany.TipoEmpresa;

                LoadEncomendas(selectedCompany.Id_Empresa);
            }
        }

        private void LoadEncomendas(int empresaId)
        {
            string query = "SELECT Codigo, prazo_entrega, Morada_entrega, Entrega,Retalhista_Empresa_Id_Empresa,Empresa_De_Transportes_Id_Empresa, Quinta_Empresa_Id FROM AgroTrack.AnimaisQuinta WHERE Empresa_Id_Empresa = @EmpresaId;";
            SqlCommand cmd = new SqlCommand(query, cn);
            cmd.Parameters.AddWithValue("@EmpresaId", empresaId);

            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                Encomenda.Items.Clear(); // Clear previous items
                while (reader.Read())
                {
                    Encomenda Enc = new Encomenda
                    {
                        Codigo = (int)reader["Codigo"],
                        PrazoEntrega = (int)reader["PrazoEntrega"],
                        MoradaEntrega = reader["MoradaEntrega"].ToString(),
                        Entrega = DateTime.Parse(reader["DataEntrega"].ToString()),
                        RetalhistaEmpresaId = (int)reader["RetalhistaEmpresaId"],
                        EmpresaDeTransportesId = (int)reader["EmpresaDeTransportesId"],
                        QuintaEmpresaId = (int)reader["QuintaEmpresaId"]
                    };

                    Encomenda.Items.Add(Enc);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to retrieve animals from database: " + ex.Message);
            }
        }

        private void listBox1_SelectedIndexChanged_3(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void Quintas_Click(object sender, EventArgs e)
        {

        }

        private void buttonPesquisarQuinta_Click(object sender, EventArgs e)
        {

        }

        private void buttonLimparPesquisaQuinta_Click(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click_1(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click_1(object sender, EventArgs e)
        {

        }

        private void Agricultores_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label32_Click(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void ListaAgricultores_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label38_Click(object sender, EventArgs e)
        {

        }

        private void label39_Click(object sender, EventArgs e)
        {

        }

        private void AgricultorQuinta_TextChanged(object sender, EventArgs e)
        {

        }

        private void Empresas_Click(object sender, EventArgs e)
        {

        }

        private void colheitasQuintas_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //Pesquisar por nome-Empresa-elemento barra de texto

        // Pesquisar por nome-Empresa-elemento barra de texto
        private void textBox10_TextChanged(object sender, EventArgs e)
        {
        }

        // Pesquisar por nome-Empresa-elemento botao pesquisar
        private void button1_Click(object sender, EventArgs e)
        {
            string searchText = ProdutoTipoOrigem.Text.ToLower();
            ListaEmpresas.Items.Clear();

            string query = "SELECT Id_Empresa, Nome, Morada, Contacto, Tipo_De_Empresa FROM AgroTrack.Empresa WHERE LOWER(Nome) LIKE @searchText;";
            SqlCommand cmd = new SqlCommand(query, cn);
            cmd.Parameters.AddWithValue("@searchText", "%" + searchText + "%");

            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Empresa company = new Empresa
                    {
                        Id_Empresa = (int)reader["Id_Empresa"],
                        Nome = reader["Nome"].ToString(),
                        Morada = reader["Morada"].ToString(),
                        Contacto = (int)reader["Contacto"],
                        TipoEmpresa = reader["Tipo_De_Empresa"].ToString(),
                    };

                    ListaEmpresas.Items.Add(company);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to retrieve data from database: " + ex.Message);
            }
        }

        // Pesquisar por nome-Empresa-elemento botao limpar
        private void button2_Click(object sender, EventArgs e)
        {
            ProdutoTipoOrigem.Text = string.Empty;
            ListaEmpresas.Items.Clear();
            LoadEmpresa();
        }


        private void LoadProdutos()
        {
            string query = "SELECT Pno.Codigo,Pro.Nome, Pro.Id_origem, Pro.Preco,Pro.Taxa_de_iva,Pro.Unidade_medida,Pro.Tipo_de_Produto FROM AgroTrack_Produto ;";
            SqlCommand cmd = new SqlCommand(query, cn);

            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Produto Product = new Produto
                    {
                        Nome = reader["Nome"].ToString(),
                        Id_origem= (int)reader["Id_origem"],
                        Tipo_de_Produto= reader["Tipo_de_Produto"].ToString(),
                        Codigo = (int)reader["Codigo"],
                        Preco= (int)reader["Preco"],
                        Taxa_de_iva= (int)reader["Taxa_de_iva"],
                        Unidade_medida= reader["Unidade_medida"].ToString()
                    };

                    ListaProdutos.Items.Add(Product);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to retrieve data from database: " + ex.Message);
            }
        }


        //produtos 
        private void ListaProdutos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ListaProdutos.SelectedItem is Produto selectedproduct)
            {
                ProdutoTipoOrigem.ReadOnly = true;
                ProdutoNome.ReadOnly = true;
                ProdutoTipo.ReadOnly = true;
                ProdutoDisponivel.ReadOnly = true;
                ProdutoVendida.ReadOnly = true;
                ProdutoOrigem.ReadOnly = true;
                ProdutoProducao.ReadOnly = true;

                ProdutoNome.Text = selectedproduct.Nome;
                ProdutoTipo.Text = selectedproduct.Tipo_de_Produto;
                ProdutoTipoOrigem.Text = selectedproduct.Id_origem.ToString();

                LoadQuinta();


            }
        }
    }
}