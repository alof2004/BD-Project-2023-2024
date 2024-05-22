using Microsoft.IdentityModel.Tokens;
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
            LoadFiltersQuinta();
            SubmeterNovaQuinta.Hide();
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

            string query = "SELECT Codigo, Nome, Preco, Taxa_de_iva, Unidade_medida, Tipo_de_Produto, Quantidade FROM AgroTrack.QuintaProduto WHERE Empresa_Id_Empresa = @EmpresaId;";
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
            else if (table == "Empresa")
            {
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    ListaEmpresas.Items.Clear(); // Clear previous items
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

        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            string inputQuintaNome = (string)PesquisarQuinta.Text;
            searchBar(inputQuintaNome, "Quinta");

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

        private void label48_Click(object sender, EventArgs e)
        {

        }

        private void ApplyCombinedFilters()
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
            ApplyCombinedFilters();
        }

        private void FilterByPlantQuinta_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyCombinedFilters();
        }

        private void FilterByAnimalQuinta_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyCombinedFilters();
        }

        private void QuantidadeAgricultores_ValueChanged(object sender, EventArgs e)
        {
            ApplyCombinedFilters();
        }

        private void AdicionarQuinta_Click(object sender, EventArgs e)
        {
            // Hide controls
            QuantidadeAgricultores.Hide();
            FiltrarPorProdutoQuinta.Hide();
            FilterByPlantQuinta.Hide();
            FilterByAnimalQuinta.Hide();
            buttonPesquisarQuinta.Hide();
            buttonLimparPesquisaQuinta.Hide();
            label48.Hide();
            label49.Hide();
            label47.Hide();
            Agricultores.Hide();
            ProdutosQuinta.Hide();
            Plantas.Hide();
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
            string query = "INSERT INTO Quintas (Nome, Morada, Contacto) VALUES (@Nome, @Morada, @Contacto)";

                using (SqlCommand command = new SqlCommand(query, cn))
                {
                    command.Parameters.AddWithValue("@Nome", nome);
                    command.Parameters.AddWithValue("@Morada", morada);
                    command.Parameters.AddWithValue("@Contacto", contacto);

                    try
                    {
                        cn.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        // Log or handle the exception as needed
                        throw new Exception("Erro ao adicionar a Quinta", ex);
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
                    buttonPesquisarQuinta.Show();
                    buttonLimparPesquisaQuinta.Show();
                    label48.Show();
                    label49.Show();
                    label47.Show();
                    Agricultores.Show();
                    ProdutosQuinta.Show();
                    Plantas.Show();
                    Animais.Show();
                    ListaQuintas.Items.Clear();
                    LoadQuinta();
                }
            }

        }
    }
}