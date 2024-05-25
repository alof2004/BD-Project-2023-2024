using Microsoft.IdentityModel.Tokens;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;
using System.Windows.Forms;
using static System.ComponentModel.Design.ObjectSelectorEditor;
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
            LoadAgricultor();
            LoadFiltersQuinta();
            LoadFiltersAgricultores();
            SubmeterNovaQuinta.Hide();
            CodigoAdicionarText.Hide();
            ProdutoIvaBox.Hide();
            ProdutoPrecoBox.Hide();
            ProdutoAdicionarBox.Hide();
            CodigoAdicionarBox.Hide();
            ConfirmarOperacao.Hide();
            ProdutoIvaBox.Hide();
            UnidadeAdicionarBox.Hide();
            ProdutoQuantidadeBox.Hide();
            TipoAdicionarBox.Hide();
            LocalQuintaBox.Hide();
            LocalQuinta.Hide();
            LoadProdutos();
            LoadRetalhistas();
            LoadTransportes();
            LoadFiltersProduto();
            OrdenarProdutos();

            OrdenarPor.Dock = DockStyle.Fill;
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
            string query = "SELECT Empresa_Id_Empresa, Nome, Morada, Contacto FROM AgroTrack.Quinta;";
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
                        Empresa_Id_Empresa = (int)reader["Empresa_Id_Empresa"], // Assuming Rua should be Codigo_quinta
                        Contacto = (int)reader["Contacto"]
                    };

                    ListaQuintas.Items.Add(farm);
                    FiltrarPorQuinta.Items.Add(farm);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to retrieve data from database: " + ex.Message);
            }
        }

        private void LoadAgricultor()
        {
            string query = "SELECT Id_Trabalhador, Pessoa_N_CartaoCidadao, Quinta_Empresa_Id_Empresa, Empresa_Id_Empresa, Nome, NomeQuinta, Contacto FROM AgroTrack.AgriculQuinta;";
            SqlCommand cmd = new SqlCommand(query, cn);

            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Agricultores agricultores = new Agricultores
                    {
                        Id_Trabalhador = (int)reader["Id_Trabalhador"],
                        Pessoa_N_CartaoCidadao = (int)reader["Pessoa_N_CartaoCidadao"],
                        Quinta_Empresa_Id_Empresa = (int)reader["Quinta_Empresa_Id_Empresa"],
                        NomeQuinta = reader["NomeQuinta"].ToString(),
                        Nome = reader["Nome"].ToString(),
                        Contacto = (int)reader["Contacto"]
                    };
                    ListaAgricultores.Items.Add(agricultores);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to retrieve farmers from database: " + ex.Message);
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
                LoadPlantas(selectedFarm.Empresa_Id_Empresa);

            }
        }

        private void LoadPlantas(int empresaId)
        {
            string query = "SELECT Id_Planta, Estacao, Tipo, Lote FROM AgroTrack.PlantasQuinta WHERE Empresa_Id_Empresa = @EmpresaId;";
            SqlCommand cmd = new SqlCommand(query, cn);
            cmd.Parameters.AddWithValue("@EmpresaId", empresaId);

            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                Plantas.Items.Clear(); // Clear previous items
                while (reader.Read())
                {
                    Planta planta = new Planta
                    {
                        Id = (int)reader["Id_Planta"],
                        Tipo = reader["Tipo"].ToString(),
                        Estacao = reader["Estacao"].ToString(),
                        Lote = reader["Lote"].ToString()
                    };

                    Plantas.Items.Add(planta);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to retrieve plants from database: " + ex.Message);
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
            string query = "SELECT Codigo, Nome, Preco, Taxa_de_iva, Unidade_medida, Tipo_de_Produto, Quantidade, NomeProduto FROM AgroTrack.QuintaProduto WHERE Empresa_Id_Empresa = @EmpresaId;";
            SqlCommand cmd = new SqlCommand(query, cn);
            cmd.Parameters.AddWithValue("@EmpresaId", empresaId);

            try
            {
                // Ensure the reader is properly disposed of by using a using statement
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    ProdutosQuinta.Items.Clear(); // Clear previous items
                    while (reader.Read())
                    {
                        ProdutosQuinta produto = new ProdutosQuinta
                        {
                            Id_Quinta = empresaId,
                            Produto = reader["NomeProduto"].ToString(),
                            Id_Produto = (int)reader["Codigo"],
                            Quantidade = (int)reader["Quantidade"]
                        };
                        ProdutosQuinta.Items.Add(produto);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to retrieve animals from database: " + ex.Message);
            }
        }




        private void loadAgricultores(int empresaId)
        {
            string query = "SELECT Id_Trabalhador, Pessoa_N_CartaoCidadao, Quinta_Empresa_Id_Empresa, Empresa_Id_Empresa, Nome, Contacto FROM AgroTrack.AgriculQuinta WHERE Empresa_Id_Empresa = @CodigoQuinta";
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
                MessageBox.Show("Failed to retrieve farmers from database: " + ex.Message);
            }
        }

        private void LoadFiltersQuinta()
        {
            string query = "SELECT Codigo, Nome FROM AgroTrack.Produto;";
            SqlCommand cmd = new SqlCommand(query, cn);
            cmd = new SqlCommand(query, cn);
            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                FiltrarPorProdutoQuinta.Items.Clear(); // Clear previous items
                FiltrarPorProdutoQuinta.Items.Add("Todos os produtos");
                while (reader.Read())
                {
                    ProdutosOnlyName produto = new ProdutosOnlyName
                    {
                        Id_Produto = (int)reader["Codigo"],
                        Produto = reader["Nome"].ToString()
                    };
                    FiltrarPorProdutoQuinta.Items.Add(produto);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to retrieve animals from database: " + ex.Message);
            }

            query = "SELECT Id_Planta, Tipo, Estacao FROM AgroTrack.planta;";
            cmd = new SqlCommand(query, cn);
            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                FilterByPlantQuinta.Items.Clear(); // Clear previous items
                FilterByPlantQuinta.Items.Add("Todas as plantas");
                while (reader.Read())
                {
                    Planta planta = new Planta
                    {
                        Id = (int)reader["Id_Planta"],
                        Tipo = reader["Tipo"].ToString(),
                        Estacao = reader["Estacao"].ToString(),
                    };
                    FilterByPlantQuinta.Items.Add(planta);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to retrieve plants from database: " + ex.Message);
            }

            query = "SELECT Id_Animal, Tipo_de_Animal FROM AgroTrack.Animal;";
            cmd = new SqlCommand(query, cn);
            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                FilterByAnimalQuinta.Items.Clear(); // Clear previous items
                FilterByAnimalQuinta.Items.Add("Todos os animais");
                while (reader.Read())
                {
                    AnimalOnlyName planta = new AnimalOnlyName
                    {
                        Id = (int)reader["Id_Animal"],
                        Tipo = reader["Tipo_de_Animal"].ToString(),
                    };
                    FilterByAnimalQuinta.Items.Add(planta);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to retrieve animals from database: " + ex.Message);
            }

        }

        //Filtros da aba dos Agricultores
        private void LoadFiltersAgricultores()
        {
            string query = "SELECT Codigo, Nome FROM AgroTrack.Produto;";
            SqlCommand cmd = new SqlCommand(query, cn);
            cmd = new SqlCommand(query, cn);
            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                ColheuProduto.Items.Clear(); // Clear previous items
                ColheuProduto.Items.Add("Todos os produtos");
                while (reader.Read())
                {
                    ProdutosOnlyName produto = new ProdutosOnlyName
                    {
                        Id_Produto = (int)reader["Codigo"],
                        Produto = reader["Nome"].ToString()
                    };
                    ColheuProduto.Items.Add(produto);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to retrieve produtos from database: " + ex.Message);
            }

            query = "SELECT Empresa_Id_Empresa, Nome FROM AgroTrack.Quinta;";
            cmd = new SqlCommand(query, cn);
            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                TrabalhaQuinta.Items.Clear(); // Clear previous items
                TrabalhaQuinta.Items.Clear(); // Clear previous items
                TrabalhaQuinta.Items.Add("Todas as quintas");
                while (reader.Read())
                {
                    QuintaOnlyName planta = new QuintaOnlyName
                    {
                        Id_Quinta = (int)reader["Empresa_Id_Empresa"],
                        Nome = reader["Nome"].ToString(),
                    };
                    TrabalhaQuinta.Items.Add(planta);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to retrieve quintas from database: " + ex.Message);
            }

        }

        private void ApplyCombinedFiltersAgricultores()
        {
            var productId = (ColheuProduto.SelectedItem as ProdutosOnlyName)?.Id_Produto;
            var farmId = (TrabalhaQuinta.SelectedItem as QuintaOnlyName)?.Id_Quinta;
            var numberOfColheitas = (int?)QuantidadeColheitas.Value > 0 ? (int?)QuantidadeColheitas.Value : null;

            string query = "SELECT DISTINCT Id_Trabalhador, Pessoa_N_CartaoCidadao, Quinta_Empresa_Id_Empresa, Nome, Contacto, NomeQuinta FROM AgroTrack.AgriculQuinta WHERE 1 = 1";

            if (productId != null)
            {
                query += " AND Pessoa_N_CartaoCidadao IN (SELECT Pessoa_N_CartaoCidadao FROM AgroTrack.FilterFarmerByProduct(@ProductId))";
            }

            if (farmId != null)
            {
                query += " AND Quinta_Empresa_Id_Empresa = @FarmId";
            }

            if (numberOfColheitas.HasValue)
            {
                query += " AND Pessoa_N_CartaoCidadao IN (SELECT Pessoa_N_CartaoCidadao FROM AgroTrack.GetNumberOfColheitasOfFarmer(@MinColheitas))";
            }

            using (SqlCommand cmd = new SqlCommand(query, cn))
            {
                if (productId != null) cmd.Parameters.AddWithValue("@ProductId", productId);
                if (farmId != null) cmd.Parameters.AddWithValue("@FarmId", farmId);
                if (numberOfColheitas.HasValue) cmd.Parameters.AddWithValue("@MinColheitas", numberOfColheitas.Value);

                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    ListaAgricultores.Items.Clear(); // Clear previous items
                    while (reader.Read())
                    {
                        Agricultores agricultores = new Agricultores
                        {
                            Id_Trabalhador = (int)reader["Id_Trabalhador"],
                            Pessoa_N_CartaoCidadao = (int)reader["Pessoa_N_CartaoCidadao"],
                            Quinta_Empresa_Id_Empresa = (int)reader["Quinta_Empresa_Id_Empresa"],
                            NomeQuinta = reader["NomeQuinta"].ToString(),
                            Nome = reader["Nome"].ToString(),
                            Contacto = (int)reader["Contacto"]
                        };
                        ListaAgricultores.Items.Add(agricultores);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to retrieve farmers from database: " + ex.Message);
                }
            }
        }



        private void searchBar(string nome, string table)
        {
            SqlCommand cmd = new SqlCommand("AgroTrack.PesquisarPorNome", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@Nome", nome));
            cmd.Parameters.Add(new SqlParameter("@esquema", "AgroTrack"));
            cmd.Parameters.Add(new SqlParameter("@tabela", table));

            if (table == "Quinta")
            {
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    ListaQuintas.Items.Clear(); // Clear previous items
                    while (reader.Read())
                    {
                        Quinta farm = new Quinta
                        {
                            Id_Quinta = (int)reader["Empresa_Id_Empresa"],
                            Nome = reader["Nome"].ToString(),
                            Morada = reader["Morada"].ToString(),
                            Empresa_Id_Empresa = (int)reader["Empresa_Id_Empresa"], // Assuming Rua should be Codigo_quinta
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
            else if (table == "Produto")
            {
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    ListaProdutos.Items.Clear(); // Clear previous items
                    while (reader.Read())
                    {
                        Produto product = new Produto
                        {
                            Nome = reader["Nome"].ToString(),
                            Id_origem = (int)reader["Id_origem"],
                            Tipo_de_Produto = reader["Tipo_de_Produto"].ToString(),
                            Codigo = (int)reader["Codigo"],
                            Preco = (double)reader["Preco"],
                            Taxa_de_iva = (double)reader["Taxa_de_iva"],
                            Unidade_medida = reader["Unidade_medida"].ToString(),
                        };

                        ListaProdutos.Items.Add(product);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to retrieve data from database: " + ex.Message);
                }
            }
            else if (table == "Retalhista")
            {
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    ListaRetalhistas.Items.Clear(); // Clear previous items
                    while (reader.Read())
                    {
                        Retalhista retalho = new Retalhista
                        {
                            Empresa_Id_Empresa = (int)reader["Empresa_Id_Empresa"],
                            Nome = reader["Nome"].ToString(),
                            Morada = reader["Morada"].ToString(),
                            Contacto = (int)reader["Contacto"]

                        };

                        ListaRetalhistas.Items.Add(retalho);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to retrieve data from database: " + ex.Message);
                }
            }
            else if (table == "Transporte")
            {
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    ListaProdutos.Items.Clear(); // Clear previous items
                    while (reader.Read())
                    {
                        Transportes transporte = new Transportes
                        {
                            Empresa_Id_Empresa = (int)reader["Empresa_Id_Empresa"],
                            Nome = reader["Nome"].ToString(),
                            Morada = reader["Morada"].ToString(),
                            Contacto = (int)reader["Contacto"]

                        };

                        ListaTransportes.Items.Add(transporte);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to retrieve data from database: " + ex.Message);
                }
            }


        }

        //searchbar-quintas
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            string inputQuintaNome = (string)PesquisarQuinta.Text;
            searchBar(inputQuintaNome, "Quinta");

        }

        //ColheitasListBox





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
            PesquisarQuinta.Text = string.Empty;
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

        //Ordenar produtos
        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            OrdenarProdutos();
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
            if (ListaAgricultores.SelectedItem is Agricultores selectedFarmer)
            {
                AgricultorNome.ReadOnly = true;
                AgricultorNumeroCC.ReadOnly = true;
                AgricultorContacto.ReadOnly = true;
                AgricultorQuinta.ReadOnly = true;
                AgricultorNome.Text = selectedFarmer.Nome;
                AgricultorNumeroCC.Text = selectedFarmer.Pessoa_N_CartaoCidadao.ToString();
                AgricultorContacto.Text = selectedFarmer.Contacto.ToString();
                AgricultorQuinta.Text = selectedFarmer.NomeQuinta.ToString();

                LoadContrato(selectedFarmer.Pessoa_N_CartaoCidadao);
                LoadColheitas(selectedFarmer.Pessoa_N_CartaoCidadao);

            }

        }

        private void LoadColheitas(int pessoa_N_CartaoCidadao)
        {
            string query = "select Id_Trabalhador, Pessoa_N_CartaoCidadao,Quinta_Empresa_Id_Empresa, Duracao_colheita, Quantidade, Produto_codigo, DataColheita,NomeProduto, Unidade_medida FROM AgroTrack.AgriculColhe WHERE Pessoa_N_CartaoCidadao = @Pessoa_N_CartaoCidadao";
            SqlCommand cmd = new SqlCommand(query, cn);
            cmd.Parameters.AddWithValue("@Pessoa_N_CartaoCidadao", pessoa_N_CartaoCidadao);
            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                ListaColheitas.Items.Clear(); // Clear previous items
                while (reader.Read())
                {
                    Colheita colheita = new Colheita
                    {
                        Id_Trabalhador = (int)reader["Id_Trabalhador"],
                        Pessoa_N_CartaoCidadao = (int)reader["Pessoa_N_CartaoCidadao"],
                        Quinta_Empresa_Id_Empresa = (int)reader["Quinta_Empresa_Id_Empresa"],
                        Duracao_colheita = (double)reader["Duracao_colheita"],
                        Quantidade = (int)reader["Quantidade"],
                        DataColheita = DateTime.Parse(reader["DataColheita"].ToString()),
                        Produto_codigo = (int)reader["Produto_codigo"],
                        ProdutoNome = reader["NomeProduto"].ToString(),
                        Unidade_de_medida = reader["Unidade_medida"].ToString()
                    };

                    ListaColheitas.Items.Add(colheita);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to retrieve colheitas from database: " + ex.Message);
            }
        }
        private void LoadContrato(int pessoa_N_CartaoCidadao)
        {
            string query = "SELECT ID, Id_Trabalhador, Pessoa_N_CartaoCidadao, Date_str, Date_end, Salario, Descricao FROM AgroTrack.AgriculConquinta WHERE Pessoa_N_CartaoCidadao = @Pessoa_N_CartaoCidadao";
            SqlCommand cmd = new SqlCommand(query, cn);
            cmd.Parameters.AddWithValue("@Pessoa_N_CartaoCidadao", pessoa_N_CartaoCidadao);

            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                AgricultorContrato.Text = "";
                while (reader.Read())
                {
                    Contrato contrato = new Contrato
                    {
                        ID = (int)reader["ID"],
                        Id_Trabalhador = (int)reader["Id_Trabalhador"],
                        Pessoa_N_CartaoCidadao = (int)reader["Pessoa_N_CartaoCidadao"],
                        Date_str = DateTime.Parse(reader["Date_str"].ToString()),
                        Date_end = DateTime.Parse(reader["Date_end"].ToString()),
                        Salario = (double)reader["Salario"],
                        Descricao = reader["Descricao"].ToString()

                    };
                    string contractInfo = $"ID: {contrato.ID}\n" +
                      $"Id Trabalhador: {contrato.Id_Trabalhador}\n" +
                      $"Pessoa N Cartao Cidadao: {contrato.Pessoa_N_CartaoCidadao}\n" +
                      $"Data de In�cio: {contrato.Date_str.ToShortDateString()}\n" +
                      $"Data de T�rmino: {contrato.Date_end.ToShortDateString()}\n" +
                      $"Sal�rio: {contrato.Salario}\n" +
                      $"Descri��o: {contrato.Descricao}\n" +
                      $"-----------------------------\n";

                    AgricultorContrato.AppendText(contractInfo);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to retrieve animals from database: " + ex.Message);
            }
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

        private void label48_Click(object sender, EventArgs e)
        {

        }

        private void ApplyCombinedFiltersQuinta()
        {
            var plantType = (FilterByPlantQuinta.SelectedItem as Planta)?.Tipo;
            var animalType = (FilterByAnimalQuinta.SelectedItem as AnimalOnlyName)?.Tipo;
            var productId = (FiltrarPorProdutoQuinta.SelectedItem as ProdutosOnlyName)?.Id_Produto;
            var numberOfFarmers = (int?)QuantidadeAgricultores.Value > 0 ? (int?)QuantidadeAgricultores.Value : null;

            string query = @"
        SELECT DISTINCT q.Empresa_Id_Empresa, q.Nome, q.Morada, q.Contacto
        FROM AgroTrack.Quinta q
        LEFT JOIN AgroTrack_Quinta_Planta qp ON q.Empresa_Id_Empresa = qp.Empresa_Id_Empresa
        LEFT JOIN AgroTrack_Planta p ON qp.Id_Planta = p.Id_Planta
        WHERE 1 = 1 ";

            if (plantType != null)
            {
                query += " AND p.Tipo = @PlantType";
            }
            if (animalType != null)
            {
                query += " AND q.Empresa_Id_Empresa IN (SELECT Empresa_Id_Empresa FROM AgroTrack.FilterFarmByAnimal(@AnimalType))";
            }
            if (productId != null)
            {
                query += " AND q.Empresa_Id_Empresa IN (SELECT Quinta_Empresa_Id_Empresa FROM AgroTrack.FilterFarmByProduct(@ProductId))";
            }
            if (numberOfFarmers.HasValue)
            {
                query += " AND q.Empresa_Id_Empresa IN (SELECT Empresa_Id_Empresa FROM AgroTrack.FilterFarmByMinimumNumberOfFarmers(@NumberOfFarmers))";
            }

            SqlCommand cmd = new SqlCommand(query, cn);
            if (plantType != null) cmd.Parameters.AddWithValue("@PlantType", plantType);
            if (animalType != null) cmd.Parameters.AddWithValue("@AnimalType", animalType);
            if (productId != null) cmd.Parameters.AddWithValue("@ProductId", productId);
            if (numberOfFarmers.HasValue) cmd.Parameters.AddWithValue("@NumberOfFarmers", numberOfFarmers.Value);

            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                ListaQuintas.Items.Clear(); // Clear previous items

                while (reader.Read())
                {
                    Quinta farm = new Quinta
                    {
                        Id_Quinta = (int)reader["Empresa_Id_Empresa"],
                        Nome = reader["Nome"].ToString(),
                        Morada = reader["Morada"].ToString(),
                        Empresa_Id_Empresa = (int)reader["Empresa_Id_Empresa"],
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

        private void FiltrarPorProdutoQuinta_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyCombinedFiltersQuinta();
        }

        private void FilterByPlantQuinta_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyCombinedFiltersQuinta();
        }

        private void FilterByAnimalQuinta_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyCombinedFiltersQuinta();
        }

        private void QuantidadeAgricultores_ValueChanged(object sender, EventArgs e)
        {
            ApplyCombinedFiltersQuinta();
        }

        private void AdicionarQuinta_Click(object sender, EventArgs e)
        {
            // Hide controls
            QuantidadeAgricultores.Hide();
            FiltrarPorProdutoQuinta.Hide();
            FilterByPlantQuinta.Hide();
            FilterByAnimalQuinta.Hide();
            buttonLimparPesquisaQuinta.Hide();
            label48.Hide();
            label49.Hide();
            label41.Hide();
            label47.Hide();
            label4.Hide();
            PesquisarQuinta.Hide();
            PesquisaPorNomeCliente.Hide();
            Agricultores.Hide();
            ProdutosQuinta.Hide();
            Plantas.Hide();
            button20.Hide();
            RemoverQuinta.Hide();
            Animais.Hide();


            // Enable input fields
            QuintaNome.ReadOnly = false;
            QuintaMorada.ReadOnly = false;
            QuintaContacto.ReadOnly = false;

            // Clear input fields
            QuintaNome.Text = "";
            QuintaMorada.Text = "";
            QuintaContacto.Text = "";
            SubmeterNovaQuinta.Show();
        }

        private void AddQuinta(string nome, string morada, int contacto)
        {
            using (SqlCommand command = new SqlCommand("AddQuinta", cn) { CommandType = CommandType.StoredProcedure })
            {
                command.Parameters.Add(new SqlParameter("@Nome", nome));
                command.Parameters.Add(new SqlParameter("@Morada", morada));
                command.Parameters.Add(new SqlParameter("@Contacto", contacto));
                MessageBox.Show("Quinta adicionada com sucesso!");
                LoadQuinta();
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to add farm to database: " + ex.Message);
                }
            }
        }


        private void SubmeterNovaQuinta_Click(object sender, EventArgs e)
        {
            if (QuintaNome.Text == "" || QuintaMorada.Text == "" || QuintaContacto.Text == "")
            {
                MessageBox.Show("Por favor preencha todos os campos!");
            }
            else
            {
                try
                {
                    AddQuinta(QuintaNome.Text, QuintaMorada.Text, int.Parse(QuintaContacto.Text));

                }

                catch
                (Exception ex)
                {
                    MessageBox.Show("Erro ao adicionar quinta: " + ex.Message);
                }
                finally
                {
                    SubmeterNovaQuinta.Hide();
                    QuantidadeAgricultores.Show();
                    FiltrarPorProdutoQuinta.Show();
                    FilterByPlantQuinta.Show();
                    FilterByAnimalQuinta.Show();
                    buttonLimparPesquisaQuinta.Show();
                    label48.Show();
                    label49.Show();
                    label41.Show();
                    label47.Show();
                    label4.Show();
                    PesquisarQuinta.Show();
                    button20.Show();
                    Agricultores.Show();
                    ProdutosQuinta.Show();
                    Plantas.Show();
                    RemoverQuinta.Show();
                    PesquisaPorNomeCliente.Show();
                    Animais.Show();
                    ListaQuintas.Items.Clear();
                    LoadQuinta();
                }
            }

        }

        //Pesquisar por nome-Empresa-elemento barra de texto

        // Pesquisar por nome-Empresa-elemento barra de texto
        private void textBox10_TextChanged(object sender, EventArgs e)
        {
        }

        // Pesquisar por nome-Empresa-elemento botao pesquisar

        private void RemoverQuinta_Click(object sender, EventArgs e)
        {
            sbyte index = (sbyte)ListaQuintas.SelectedIndex;
            if (index == -1)
            {
                MessageBox.Show("Por favor selecione uma quinta para remover!");
            }
            else
            {
                try
                {
                    RemoveFarm((ListaQuintas.SelectedItem as Quinta).Id_Quinta);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao remover quinta: " + ex.Message);
                }
                finally
                {
                    ListaQuintas.Items.Clear();
                    LoadQuinta();
                }
            }
        }
        private void RemoveFarm(int farmId)
        {
            using (SqlCommand command = new SqlCommand("ApagarQuinta", cn) { CommandType = CommandType.StoredProcedure })
            {
                command.Parameters.Add(new SqlParameter("@Empresa_Id_Empresa", farmId));
                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Quinta removida com sucesso!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to remove farm from database: " + ex.Message);
                }
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            sbyte index = (sbyte)ListaQuintas.SelectedIndex;
            if (index == -1)
            {
                MessageBox.Show("Por favor selecione uma produto para remover!");
            }
            else
            {
                try
                {
                    RemoveProdutosForaDeValidadeFromQuinta((ListaQuintas.SelectedItem as Quinta).Id_Quinta);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao remover empresa: " + ex.Message);
                }
            }
        }
        private void RemoveProdutosForaDeValidadeFromQuinta(int farmId)
        {
            using (SqlCommand command = new SqlCommand("RemoveProdutosForaDeValidadeFromQuinta", cn) { CommandType = CommandType.StoredProcedure })
            {
                command.Parameters.Add(new SqlParameter("@QuintaId", farmId));
                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Produtos fora da validade removidos com sucesso!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to remove produtos out of date from database: " + ex.Message);
                }
            }
        }

        private void AgricultorContrato_TextChanged(object sender, EventArgs e)
        {

        }


        private void LoadProdutos()
        {
            string baseQuery = "SELECT Codigo, Nome, Id_origem, Preco, Taxa_de_iva, Unidade_medida, Tipo_de_Produto FROM AgroTrack.Produto";
            string whereClause = "";

            // Aplicar filtro por tipo de produto
            if (FiltrarPorTipo.SelectedItem != null && FiltrarPorTipo.SelectedItem.ToString() != "Todas os tipos")
            {
                whereClause += $" WHERE Tipo_de_Produto = '{FiltrarPorTipo.SelectedItem.ToString()}'";
            }

            // Aplicar filtro por quinta
            if (FiltrarPorQuinta.SelectedItem != null && FiltrarPorQuinta.SelectedItem.ToString() != "Todas as Quintas")
            {
                if (whereClause != "")
                {
                    whereClause += " AND";
                }
                else
                {
                    whereClause += " WHERE";
                }
                whereClause += $" Id_origem = {(FiltrarPorQuinta.SelectedItem as QuintaOnlyName).Id_Quinta}";
            }

            string query = baseQuery + whereClause;
            SqlCommand cmd = new SqlCommand(query, cn);

            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                ListaProdutos.Items.Clear(); // Clear previous items
                while (reader.Read())
                {
                    Produto product = new Produto
                    {
                        Nome = reader["Nome"].ToString(),
                        Id_origem = (int)reader["Id_origem"],
                        Tipo_de_Produto = reader["Tipo_de_Produto"].ToString(),
                        Codigo = (int)reader["Codigo"],
                        Preco = (double)reader["Preco"],
                        Taxa_de_iva = (double)reader["Taxa_de_iva"],
                        Unidade_medida = reader["Unidade_medida"].ToString(),
                    };

                    ListaProdutos.Items.Add(product);
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
                ProdutoAdicionarInfo.ReadOnly = true;
                ProdutoTipo.ReadOnly = true;
                ProdutoIva.ReadOnly = true;
                ProdutoDisponivel.ReadOnly = true;
                ProdutoPreco.ReadOnly = true;
                QuantidadeVendidaBox.ReadOnly = true;
                ProdutoUnidade.ReadOnly = true;

                ProdutoAdicionarInfo.Text = selectedproduct.Nome;
                ProdutoTipo.Text = selectedproduct.Tipo_de_Produto;
                ProdutoIva.Text = selectedproduct.Taxa_de_iva.ToString();
                ProdutoPreco.Text = selectedproduct.Preco.ToString();
                ProdutoUnidade.Text = selectedproduct.Unidade_medida;
                ProdutoDisponivel.Text = GetQuantidadeDisponivel(selectedproduct.Codigo).ToString();

                LoadQuintas(selectedproduct.Id_origem);
                
            }
           
        }

        private void Retalhistas_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click_1(object sender, EventArgs e)
        {

        }


        //searchbar-Produtos
        private void textBox14_TextChanged(object sender, EventArgs e)
        {
            string inputProdutoNome = (string)PesquisarNomeProdutoBox.Text;
            searchBar(inputProdutoNome, "Produto");
        }


        private void LoadQuintas(int Id_origem)
        {
            string query = @"SELECT DISTINCT Empresa_Id_Empresa, Nome, Morada, Contacto, Quantidade, NomeProduto, Tipo_de_Produto, Codigo, Unidade_medida, Preco, Taxa_de_iva 
            FROM AgroTrack.QuintaProduto  
            WHERE Empresa_Id_Empresa = @Id_origem;";
            SqlCommand cmd = new SqlCommand(query, cn);
            cmd.Parameters.AddWithValue("@Id_origem", Id_origem);

            HashSet<int> quintaIds = new HashSet<int>(); // HashSet para armazenar IDs das quintas já adicionadas

            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                QuintasProdutos.Items.Clear(); // Clear previous items
                while (reader.Read())
                {
                    int quintaId = (int)reader["Empresa_Id_Empresa"];
                    // Verificar se o ID da quinta já foi adicionado
                    if (!quintaIds.Contains(quintaId))
                    {
                        quintaIds.Add(quintaId); // Adicionar o ID da quinta ao HashSet
                        Quinta farm = new Quinta
                        {
                            Id_Quinta = quintaId,
                            Nome = reader["Nome"].ToString(),
                            Morada = reader["Morada"].ToString(),
                            Contacto = (int)reader["Contacto"],
                            // Adicione outras propriedades se necessário
                        };

                        QuintasProdutos.Items.Add(farm);
                    }
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to retrieve data from database: " + ex.Message);
            }
        }





        //cleanProdutosbar
        private void button12_Click(object sender, EventArgs e)
        {
            PesquisarNomeProdutoBox.Text = string.Empty;
        }


        private int GetQuantidadeDisponivel(int produtoCodigo)
        {
            string query = @"
            SELECT COALESCE(SUM(C.Quantidade), 0) as QuantidadeDisponivel
            FROM AgroTrack.Contem C
            WHERE Produto_codigo = @ProdutoCodigo ;";
            SqlCommand cmd = new SqlCommand(query, cn);
            cmd.Parameters.AddWithValue("@ProdutoCodigo", produtoCodigo);

            try
            {
                object result = cmd.ExecuteScalar();
                return Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to retrieve quantity available: " + ex.Message);
                return 0;
            }

        }


        private void LoadFiltersProduto()
        {
            string query = "SELECT Codigo, Tipo_de_Produto FROM AgroTrack.Produto;";
            SqlCommand cmd = new SqlCommand(query, cn);
            cmd = new SqlCommand(query, cn);
            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                FiltrarPorTipo.Items.Clear(); // Clear previous items
                TipoAdicionarBox.Items.Clear();
                FiltrarPorTipo.Items.Add("Todas os tipos");
                HashSet<string> addedTipos = new HashSet<string>();
                while (reader.Read())
                {
                    ProdutosOnlyTipo produto = new ProdutosOnlyTipo
                    {
                        Id_Produto = (int)reader["Codigo"],
                        Tipo_de_Produto = reader["Tipo_de_Produto"].ToString()
                    };
                    string tipoProduto = reader["Tipo_de_Produto"].ToString();
                    if (!addedTipos.Contains(tipoProduto))
                    {
                        addedTipos.Add(tipoProduto);
                        FiltrarPorTipo.Items.Add(tipoProduto);
                        TipoAdicionarBox.Items.Add(tipoProduto);
                    }
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to retrieve animals from database: " + ex.Message);
            }

            query = "SELECT Empresa_Id_Empresa, Nome FROM AgroTrack.Quinta;";
            cmd = new SqlCommand(query, cn);
            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                FiltrarPorQuinta.Items.Clear(); // Clear previous items
                LocalQuintaBox.Items.Clear();
                FiltrarPorQuinta.Items.Add("Todas as Quintas");
                while (reader.Read())
                {
                    QuintaOnlyName Farm = new QuintaOnlyName
                    {
                        Id_Quinta = (int)reader["Empresa_Id_Empresa"],
                        Nome = reader["Nome"].ToString(),
                    };
                    FiltrarPorQuinta.Items.Add(Farm);
                    LocalQuintaBox.Items.Add(Farm);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to retrieve plants from database: " + ex.Message);
            }
        }


        //filtrar por tipo de produto
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            OrdenarProdutos();
            LoadProdutos();
        }

        //filtrar por quinta opcoes ja aparecem
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            OrdenarProdutos();
            LoadProdutos();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
        }

        private void OrdenarProdutos()
        {
            // Verifica quais itens foram selecionados na CheckedListBox
            List<string> opcoesSelecionadas = new List<string>();
            foreach (object itemChecked in Ordenar.CheckedItems)
            {
                opcoesSelecionadas.Add(itemChecked.ToString());
            }

            // Constrói a cláusula ORDER BY com base nas opções selecionadas
            string orderByClause = "";
            if (opcoesSelecionadas.Contains("Nome"))
            {
                orderByClause += "Nome";
            }
            else if (opcoesSelecionadas.Contains("Código (decrescente)"))
            {
                orderByClause += "Codigo DESC";
            }
            else if (opcoesSelecionadas.Contains("Preco (crescente)"))
            {
                orderByClause += "Preco";
            }
            else if (opcoesSelecionadas.Contains("Preco (decrescente)"))
            {
                orderByClause += "Preco DESC";
            }
            else if (opcoesSelecionadas.Contains("Taxa_de_Iva(crescente)"))
            {
                orderByClause += "Taxa_de_iva";
            }
            else if (opcoesSelecionadas.Contains("Taxa_de_Iva(decrescente)"))
            {
                orderByClause += "Taxa_de_iva DESC";
            }

            // Constrói a consulta SQL com base na cláusula ORDER BY
            string query = "SELECT Nome, Codigo, Preco, Taxa_de_iva FROM AgroTrack.Produto";
            if (!string.IsNullOrEmpty(orderByClause))
            {
                query += " ORDER BY " + orderByClause;
            }

            // Executa a consulta SQL e exibe os resultados
            SqlCommand cmd = new SqlCommand(query, cn);
            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                ListaProdutos.Items.Clear(); // Limpa os itens anteriores
                while (reader.Read())
                {
                    Produto product = new Produto
                    {
                        Nome = reader["Nome"].ToString(),
                        Codigo = (int)reader["Codigo"],
                        Preco = (double)reader["Preco"],
                        Taxa_de_iva = (double)reader["Taxa_de_iva"]
                    };
                    ListaProdutos.Items.Add(product);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to retrieve data from database: " + ex.Message);
            }
        }


        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        //ConfirmarOperacao
        private void ConfirmarOperacao_Click(object sender, EventArgs e)
        {
            if (CodigoAdicionarBox.Text == "" || ProdutoAdicionarBox.Text == "" || UnidadeAdicionarBox.Text == "" || ProdutoQuantidadeBox.Text == "" || ProdutoIvaBox.Text == "" || TipoAdicionarBox.Text == "" || ProdutoPrecoBox.Text == "")
            {
                MessageBox.Show("Por favor preencha todos os campos!");
            }
            else
            {
                try
                {
                    int id_origem = GetQuintaIdByName(LocalQuintaBox.Text);
                    AddProduto(id_origem, ProdutoAdicionarBox.Text, UnidadeAdicionarBox.Text, ProdutoIvaBox.Text, TipoAdicionarBox.Text, double.Parse(ProdutoQuantidadeBox.Text), double.Parse(ProdutoPrecoBox.Text));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao adicionar produto: " + ex.Message);
                }
                finally
                {
                    // Hide input fields and show the main controls again
                    CodigoAdicionarText.Hide();
                    ProdutoPrecoBox.Hide();
                    ProdutoAdicionarBox.Hide();
                    CodigoAdicionarBox.Hide();
                    ConfirmarOperacao.Hide();
                    ProdutoIvaBox.Hide();
                    UnidadeAdicionarBox.Hide();
                    ProdutoQuantidadeBox.Hide();
                    LocaldeProducao.Hide();
                    TipoAdicionarBox.Hide();
                    LocalQuintaBox.Hide();

                    Ordenar.Show();
                    OrdenarText.Show();
                    QuintaText.Show();
                    TipoText.Show();
                    QuantidadeBox.Show();
                    QuantidadeText.Show();
                    FiltrarPorTipo.Show();
                    FiltrarPorQuinta.Show();
                    PesquisarNomeProdutoClear.Show();
                    PesquisarNomeProdutoBox.Show();
                    PesquisarNomeProduto.Show();
                    PesquisaPorNomeCliente.Show();
                    AdicionarProdutoProduto.Show();
                    EliminarProduto.Show();
                    EditarInfo.Show();
                    QuantidadeVendidaBox.Show();
                    QuantidadeVendidaText.Show();
                    QuintasProdutos.Show();
                    InformaçoesProduto.Show();
                    ProdutoTipo.Show();
                    ProdutoAdicionarInfo.Show();
                    ProdutoPreco.Show();
                    ProdutoIva.Show();
                    ProdutoIvaBox.Show();
                    ProdutoUnidade.Show();
                    ProdutoDisponivel.Show();
                    ListaProdutos.Items.Clear();
                    LoadProdutos();
                }
            }
        }


        private void AddProduto(int id_origem, string ProdutoNome, string unidademedidaValue, string IvaValue, string tipo, double tipoValue, double preco)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("AddProduto", cn) { CommandType = CommandType.StoredProcedure })
                {
                    command.Parameters.Add(new SqlParameter("@NomeProduto", ProdutoNome));
                    command.Parameters.Add(new SqlParameter("@Id_origem", id_origem));
                    command.Parameters.Add(new SqlParameter("@Tipo_de_Produto", tipoValue));
                    command.Parameters.Add(new SqlParameter("@Preco", preco));
                    command.Parameters.Add(new SqlParameter("@Taxa_de_iva", IvaValue));
                    command.Parameters.Add(new SqlParameter("@Unidade_medida", unidademedidaValue));

                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    command.ExecuteNonQuery();

                    if (cn.State == ConnectionState.Open)
                    {
                        cn.Close();
                    }

                    MessageBox.Show("Produto adicionado com sucesso!");
                }
            }
            catch (Exception ex)
            {
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }

                throw new Exception("Falha ao adicionar o produto: " + ex.Message);
            }
        }




        //botao adicionar produto
        private void AdicionarProdutoProduto_Click_1(object sender, EventArgs e)
        {
            // Hide controls
            Ordenar.Hide();
            OrdenarText.Hide();
            QuintaText.Hide();
            TipoText.Hide();
            QuantidadeBox.Hide();
            QuantidadeText.Hide();
            FiltrarPorTipo.Hide();
            FiltrarPorQuinta.Hide();
            PesquisarNomeProdutoClear.Hide();
            PesquisarNomeProdutoBox.Hide();
            PesquisarNomeProduto.Hide();
            PesquisaPorNomeCliente.Hide();
            AdicionarProdutoProduto.Hide();
            EliminarProduto.Hide();
            EditarInfo.Hide();
            QuantidadeVendidaBox.Hide();
            QuantidadeVendidaText.Hide();
            QuintasProdutos.Hide();
            InformaçoesProduto.Hide();
            ProdutoTipo.Hide();
            ProdutoAdicionarInfo.Hide();
            ProdutoPreco.Hide();
            ProdutoIva.Hide();
            ProdutoUnidade.Hide();
            ProdutoDisponivel.Hide();
            TipoAdicionarBox.Hide();


            // Enable input fields
            CodigoAdicionarBox.ReadOnly = false;
            ProdutoAdicionarBox.ReadOnly = false;
            ProdutoQuantidadeBox.ReadOnly = false;
            ProdutoPrecoBox.ReadOnly = false;


            // Clear input fields
            CodigoAdicionarBox.Text = "";
            ProdutoAdicionarBox.Text = "";
            UnidadeAdicionarBox.Text = "";
            ProdutoQuantidadeBox.Text = "";
            ProdutoIvaBox.Text = "";
            ProdutoPrecoBox.Text = "";
            LocalQuintaBox.Text = "";

            LocalQuinta.Show();
            LocalQuintaBox.Show();
            CodigoAdicionarText.Show();
            ProdutoPrecoBox.Show();
            ProdutoAdicionarBox.Show();
            CodigoAdicionarBox.Show();
            ConfirmarOperacao.Show();
            ProdutoIvaBox.Show();
            UnidadeAdicionarBox.Show();
            ProdutoQuantidadeBox.Show();
            TipoAdicionarBox.Show();
        }


        private int GetQuintaIdByName(string nomeQuinta)
        {
            int quintaId = -1;
            using (SqlCommand command = new SqlCommand("SELECT Empresa_Id_Empresa, Nome, Morada, Contacto FROM AgroTrack.Quinta WHERE Nome = @Nome", cn))
            {
                command.Parameters.Add(new SqlParameter("@Nome", nomeQuinta));
                if (cn.State == ConnectionState.Closed)
                {
                    cn.Open();
                }

                var result = command.ExecuteScalar();

                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }

                if (result != null && int.TryParse(result.ToString(), out quintaId))
                {
                    return quintaId;
                }
                else
                {
                    throw new Exception("Quinta não encontrada.");
                }
            }
        }

        private void RemoveProduto(int produtoid)
        {
            using (SqlCommand command = new SqlCommand("ApagarProduto", cn) { CommandType = CommandType.StoredProcedure })
            {
                command.Parameters.Add(new SqlParameter("@Codigo", produtoid));
                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Produto removida com sucesso!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to remove product from database: " + ex.Message);
                }
            }
        }

        private void EliminarProduto_Click(object sender, EventArgs e)
        {
            sbyte index = (sbyte)ListaProdutos.SelectedIndex;
            if (index == -1)
            {
                MessageBox.Show("Por favor selecione um produto para remover!");
            }
            else
            {
                try
                {
                    RemoveProduto((ListaProdutos.SelectedItem as Produto).Codigo);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao remover quinta: " + ex.Message);
                }
                finally
                {
                    ListaProdutos.Items.Clear();
                    LoadProdutos();
                }
            }
        }


        //PesquisarNomeRetalhistaBox
        private void button35_Click(object sender, EventArgs e)
        {
            PesquisarNomeRetalhistaBox.Text = string.Empty;
        }

        private void PesquisarNomeRetalhistaBox_TextChanged(object sender, EventArgs e)
        {
            string inputRetalhistaNome = (string)PesquisarNomeRetalhistaBox.Text;
            searchBar(inputRetalhistaNome, "Retalhista");
        }

        //PesquisarNomeTransporte
        private void button28_Click(object sender, EventArgs e)
        {
            PesquisarNomeTransporte.Text = string.Empty;
        }

        private void PesquisarNomeTransporte_TextChanged(object sender, EventArgs e)
        {
            string inputTransporteNome = (string)PesquisarNomeTransporte.Text;
            searchBar(inputTransporteNome, "Transporte");
        }

        private void NomeClientes_TextChanged(object sender, EventArgs e)
        {

        }

        private void RetalhistasNome_TextChanged(object sender, EventArgs e)
        {

        }

        private void LoadRetalhistas()
        {
            ListaRetalhistas.Items.Clear();
            string query = "SELECT Empresa_Id_Empresa, Nome, Morada, Contacto FROM AgroTrack.RetalhistasE;";
            SqlCommand cmd = new SqlCommand(query, cn);

            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Retalhista retalho = new Retalhista
                    {
                        Empresa_Id_Empresa = (int)reader["Empresa_Id_Empresa"],
                        Nome = reader["Nome"].ToString(),
                        Morada = reader["Morada"].ToString(),
                        Contacto = (int)reader["Contacto"]

                    };

                    ListaRetalhistas.Items.Add(retalho);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to retrieve data from database: " + ex.Message);
            }
        }



        private void LoadTransportes()
        {
            ListaTransportes.Items.Clear();
            string query = "SELECT Empresa_Id_Empresa, Nome, Morada, Contacto FROM AgroTrack.TransportesE;";
            SqlCommand cmd = new SqlCommand(query, cn);

            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Transportes transporte = new Transportes
                    {
                        Empresa_Id_Empresa = (int)reader["Empresa_Id_Empresa"],
                        Nome = reader["Nome"].ToString(),
                        Morada = reader["Morada"].ToString(),
                        Contacto = (int)reader["Contacto"]

                    };

                    ListaTransportes.Items.Add(transporte);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to retrieve data from database: " + ex.Message);
            }
        }



        //ListaRetalhistas e boxs
        private void ListaRetalhistas_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (ListaRetalhistas.SelectedItem is Retalhista selectedretalho)
            {
                RetalhistasNome.ReadOnly = true;
                RetalhistasMorada.ReadOnly = true;
                RetalhistasContacto.ReadOnly = true;

                RetalhistasNome.Text = selectedretalho.Nome;
                RetalhistasMorada.Text = selectedretalho.Morada;
                RetalhistasContacto.Text = selectedretalho.Contacto.ToString();

            }
        }

        //ListaTransportes e boxs
        private void ListaTransportes_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (ListaTransportes.SelectedItem is Transportes selectedtransporte)
            {
                TransportesNome.ReadOnly = true;
                TransportesMorada.ReadOnly = true;
                TransportesContacto.ReadOnly = true;

                TransportesNome.Text = selectedtransporte.Nome;
                TransportesMorada.Text = selectedtransporte.Morada;
                TransportesContacto.Text = selectedtransporte.Contacto.ToString();
            }
        }


        private void ColheuProduto_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyCombinedFiltersAgricultores();
        }

        private void TrabalhaQuinta_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyCombinedFiltersAgricultores();
        }

        private void QuantidadeColheitas_ValueChanged(object sender, EventArgs e)
        {
            ApplyCombinedFiltersAgricultores();
        }

        private void ListaColheitas_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void AddAgricultor_Click(object sender, EventArgs e)
        {
            // Hide controls
            button13.Hide();
            button14.Hide();
            label29.Hide();
            label36.Hide();
            ColheuProduto.Hide();
            TrabalhaQuinta.Hide();
            QuantidadeColheitas.Hide();
            label35.Hide();
            label33.Hide();
            

            // Enable input fields
            AgricultorNome.ReadOnly = false;
            AgricultorNumeroCC.ReadOnly = false;
            AgricultorContacto.ReadOnly = false;
            AgricultorQuinta.ReadOnly = false;

            // Clear input fields
            AgricultorNome.Text = "";
            AgricultorNumeroCC.Text = "";
            AgricultorContacto.Text = "";
            AgricultorQuinta.Text = "";
            SubmeterAdicionarQuinta.Show();
        }

        private void SubmeterAdicionarQuinta_Click(object sender, EventArgs e)
        {
            if (AgricultorNome.Text == "" || AgricultorNumeroCC.Text == "" || AgricultorContacto.Text == "" || AgricultorQuinta.Text == "")
            {
                MessageBox.Show("Por favor preencha todos os campos!");
            }
            else
            {
                try
                {
                    AddAgricultor(AgricultorNome.Text, int.Parse(AgricultorNumeroCC.Text), AgricultorQuinta.Text, int.Parse(AgricultorContacto.Text));
                }
                catch(Exception ex)
                        {
                            MessageBox.Show("Erro ao adicionar agricultor: " + ex.Message);
                        }
                 finally
                        {
                            SubmeterAdicionarQuinta.Hide();
                            QuantidadeColheitas.Show();
                            ColheuProduto.Show();
                            TrabalhaQuinta.Show();
                            buttonLimparPesquisaQuinta.Show();
                            label48.Show();
                            label49.Show();
                            label41.Show();
                            label47.Show();
                            label4.Show();
                            PesquisarQuinta.Show();
                            button20.Show();
                            Agricultores.Show();
                            ProdutosQuinta.Show();
                            Plantas.Show();
                            RemoverQuinta.Show();
                            PesquisaPorNomeCliente.Show();
                            Animais.Show();
                            ListaAgricultores.Items.Clear();
                            LoadAgricultor();
                        }
                }
        }

        private void CodigoAdicionarBox_TextChanged(object sender, EventArgs e)
        {

        }
        private void AddAgricultor(string nome, int numeroCC, string quinta, int contacto)
        {
            using (SqlCommand command = new SqlCommand("AddAgricultor", cn) { CommandType = CommandType.StoredProcedure })
            {
                command.Parameters.Add(new SqlParameter("@Nome", nome));
                command.Parameters.Add(new SqlParameter("@NumeroCC", numeroCC));
                command.Parameters.Add(new SqlParameter("@Quinta", quinta));
                command.Parameters.Add(new SqlParameter("@Contacto", contacto));
                LoadAgricultor();
                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Agricultor adicionado com sucesso!");

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to add farmer to database: " + ex.Message);
                }
            }
        }
    }

}