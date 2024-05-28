using Microsoft.IdentityModel.Tokens;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;
using System.Globalization;
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
            LoadClientes();

            //botoes da aba clientes que Têm de ser escondidos no início
            SubmeterCliente.Hide();
            AddCompraCliente.Hide();
            AddCompraClienteLabel.Hide();
            AddCompraQuantidade.Hide();
            AddCompraQuantidadeLabel.Hide();
            AddCompraProduto.Hide();
            AddCompraProdutoLabel.Hide();
            AddCompraData.Hide();
            AddCompraDataLabel.Hide();
            AddCompraMetodo.Hide();
            AddCompraMetodoLabel.Hide();
            AddCompraQuinta.Hide();
            AddCompraQuintaLabel.Hide();
            SubmeterCompra.Hide();
            ConfirmarEncomenda.Hide();
            ConfirmarEmpresa.Hide();
            PrazoEncomenda.Hide();
            PrazoBox.Hide();
            MoradaEncomenda.Hide();
            MoradaBox.Hide();
            EntregaEncomenda.Hide();
            EntregaBox.Hide();
            RetalhistaEncomenda.Hide();
            RetalhistaBox.Hide();
            TransportesEncomenda.Hide();
            TransportesBox.Hide();
            QuintaEncomenda.Hide();
            QuintaBox.Hide();
            ConfirmarRetalhista.Hide();
            PrazoEncomendaRetalhista.Hide();
            PrazoBoxRetalhista.Hide();
            MoradaRetalhista.Hide();
            MoradaRetalhistaBox.Hide();
            DataRetalhistaEncoemndabOX.Hide();
            CompradorEncoemndaRetalhista.Hide();
            EmpresaDeTransporteEncoemndaRetalhista.Hide();
            EmpresaDeTransporteEncoemndaRetalhistaBox.Hide();
            QuintaEncoemndaRetalhista.Hide();
            QuintaEncoemndaRetalhistaBox.Hide();
            ConfirmarRetalhistaEncoemnda.Hide();
            SubmeterNovaQuinta.Hide();
            AddProdutoToQuintaSubmit.Hide();
            EncomendaListaProdutos.Hide();
            AlterarQuantidadeQuinta.Hide();
            AlterarNumero.Hide();
            AlterarDataEncomenda.Hide();
            Confirmar.Hide();
            ConfirmarData.Hide();
            DataDeEntregaAtual.Hide();
            DataDeEntregaAtualBOX.Hide();
            NovaDataDeEntrega.Hide();
            NovaDataDeEntregaBOX.Hide();
            DataEncomendaTransportes.Value = DateTime.Now;


            // botoes de adicionar agricultor que são escondidos no início 
            label10.Hide();
            DescricaoContrato.Hide();
            label8.Hide();
            SalarioAdicionarAgricultor.Hide();
            label7.Hide();
            label11.Hide();
            InicioContrato.Hide();
            FimContrato.Hide();
            SubmeterAgricultor.Hide();
            SelectQuintaAddAgricultor.Hide();


            //botões de adicionar colheita que são escondidos no início
            AddColheitaAgricultor.Hide();
            AddColheitaListaAgricultores.Hide();
            AddColheitaProdutosLista.Hide();
            AddColheitaProduto.Hide();
            AddColheitaQuantidade.Hide();
            AddColheitaQuantidadeLabel.Hide();
            AddColheitaDuracao.Hide();
            AddColheitaDuracaoTexto.Hide();
            AddColheitaData.Hide();
            AddColheitaDataLabel.Hide();
            AddColheitaValidade.Hide();
            AddColheitaValidadeLabel.Hide();
            SubmeterColheita.Hide();


            SubmeterNovaQuinta.Hide();
            CodigoAdicionarText.Hide();
            ProdutoIvaBox.Hide();
            ProdutoPrecoBox.Hide();
            ProdutoAdicionarBox.Hide();
            CodigoAdicionarBox.Hide();
            ConfirmarOperacao.Hide();
            ProdutoIvaBox.Hide();
            UnidadeAdicionarBox.Hide();
            TipoAdicionarBox.Hide();
            AddProdutoToQuintaData.Hide();
            AddProdutoToQuintaQuantidade.Hide();
            AddProdutoToQuintaProdutoID.Hide();
            AddProdutoToQuintaDataLabel.Hide();
            AddProdutoToQuintaQuantidadeLabel.Hide();
            AddProdutoToQuintaProdutoIDLabel.Hide();
            AddProdutoToQuintaID.Hide();
            AddProdutoToQuintaIDLabel.Hide();
            AddAnimalID.Hide();
            AddAnimalAnimalLabel.Hide();
            AddAnimalBrinco.Hide();
            AddAnimalBrincoLabel.Hide();
            AddAnimalIdade.Hide();
            AddAnimalIdadeLabel.Hide();
            AddAnimalQuinta.Hide();
            AddAnimalQuintaLabel.Hide();
            AddAnimalSubmeter.Hide();
            AddPlantaEstacao.Hide();
            AddPlantaEstacaoLabel.Hide();
            AddPlantaLote.Hide();
            AddPlantaLoteLabel.Hide();
            AddAnimalID.Hide();
            AddPlantaSubmeter.Hide();
            AddPlantaIDLabel.Hide();
            AddPlantaIDPlanta.Hide();


            LoadProdutos();
            LoadRetalhistas();
            LoadTransportes();
            LoadFiltersProduto();
            OrdenarProdutos();
            LoadFilterRetalhistas();
            LoadFiltersTransportes();

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
                AddProdutoToQuintaID.Items.Clear();
                AddAnimalQuinta.Items.Clear();
                ListaQuintas.Items.Clear();
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
                    AddAnimalQuinta.Items.Add(farm);
                    AddProdutoToQuintaID.Items.Add(farm);
                    FiltrarPorQuinta.Items.Add(farm);
                }
                reader.Close();
                LoadFiltersQuinta();
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
                AddColheitaListaAgricultores.Items.Clear();
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
                    AddColheitaListaAgricultores.Items.Add(agricultores);
                }
                reader.Close();
                LoadFiltersAgricultores();
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
                AlterarQuantidadeQuinta.Visible = false;
                AlterarNumero.Visible = false;
                Confirmar.Visible = false;

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
            string query = "SELECT DISTINCT Codigo, Nome, Preco, Taxa_de_iva, Unidade_medida, Tipo_de_Produto, Quantidade, NomeProduto FROM AgroTrack.QuintaProduto WHERE Empresa_Id_Empresa = @EmpresaId;";
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
                AddProdutoToQuintaProdutoID.Items.Clear();
                AddColheitaProdutosLista.Items.Clear();

                while (reader.Read())
                {
                    ProdutosOnlyName produto = new ProdutosOnlyName
                    {
                        Id_Produto = (int)reader["Codigo"],
                        Produto = reader["Nome"].ToString()
                    };
                    FiltrarPorProdutoQuinta.Items.Add(produto);
                    AddProdutoToQuintaProdutoID.Items.Add(produto);
                    AddColheitaProdutosLista.Items.Add(produto);
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
                AddPlantaIDPlanta.Items.Clear();

                while (reader.Read())
                {
                    Planta planta = new Planta
                    {
                        Id = (int)reader["Id_Planta"],
                        Tipo = reader["Tipo"].ToString(),
                        Estacao = reader["Estacao"].ToString(),
                    };
                    FilterByPlantQuinta.Items.Add(planta);
                    AddPlantaIDPlanta.Items.Add(planta);
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
                AddAnimalID.Items.Clear();
                while (reader.Read())
                {
                    AnimalOnlyName planta = new AnimalOnlyName
                    {
                        Id = (int)reader["Id_Animal"],
                        Tipo = reader["Tipo_de_Animal"].ToString(),
                    };
                    AddAnimalID.Items.Add(planta);
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
                SelectQuintaAddAgricultor.Items.Clear();
                TrabalhaQuinta.Items.Add("Todas as quintas");
                while (reader.Read())
                {
                    QuintaOnlyName planta = new QuintaOnlyName
                    {
                        Id_Quinta = (int)reader["Empresa_Id_Empresa"],
                        Nome = reader["Nome"].ToString(),
                    };
                    SelectQuintaAddAgricultor.Items.Add(planta);
                    TrabalhaQuinta.Items.Add(planta);
                    QuintaBox.Items.Add(planta);
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
            else if (table == "RetalhistasE")
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
            else if (table == "TransportesE")
            {
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    ListaTransportes.Items.Clear(); // Clear previous items
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
            else if (table == "AgriculQuinta")
            {
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
            else if (table == "Cliente")
            {
                try
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    ListaClientes.Items.Clear(); // Clear previous items
                    while (reader.Read())
                    {
                        Cliente cliente = new Cliente
                        {
                            Pessoa_N_CartaoCidadao = (int)reader["Pessoa_N_CartaoCidadao"],
                            Nome = reader["Nome"].ToString(),
                            Contacto = (int)reader["Contacto"]
                        };
                        ListaClientes.Items.Add(cliente);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to retrieve farmers from database: " + ex.Message);
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
            PesquisarQuinta.Text = "";
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
                      $"Data de Inicio: {contrato.Date_str.ToShortDateString()}\n" +
                      $"Data de Termino: {contrato.Date_end.ToShortDateString()}\n" +
                      $"Salario: {contrato.Salario}\n" +
                      $"Descricao: {contrato.Descricao}\n" +
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
            if (ProdutosQuinta.SelectedItem != null)
            {
                // Mostra o botão AlterarQuantidadeQuinta
                AlterarQuantidadeQuinta.Visible = true;
            }
            else
            {
                // Esconde o botão AlterarQuantidadeQuinta se nenhum item estiver selecionado
                AlterarQuantidadeQuinta.Visible = false;
            }
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
            string baseQuery = "SELECT Codigo, Nome, Preco, Taxa_de_iva, Unidade_medida, Tipo_de_Produto, Quantidade, Quinta_Empresa_Id_Empresa FROM AgroTrack.ProdutoContem";
            string whereClause = "";

            // Apply filter by product type
            if (FiltrarPorTipo.SelectedItem != null && FiltrarPorTipo.SelectedItem.ToString() != "Todas os tipos")
            {
                whereClause += $" WHERE Tipo_de_Produto = '{FiltrarPorTipo.SelectedItem.ToString()}'";
            }

            // Apply filter by farm
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
                whereClause += $" Quinta_Empresa_Id_Empresa = {(FiltrarPorQuinta.SelectedItem as QuintaOnlyName).Id_Quinta}";
            }

            if (QuantidadeBox.Value > 0)
            {
                if (whereClause != "")
                {
                    whereClause += " AND";
                }
                else
                {
                    whereClause += " WHERE";
                }
                whereClause += $" Quantidade >= {QuantidadeBox.Value}";
            }

            string query = baseQuery + whereClause;
            SqlCommand cmd = new SqlCommand(query, cn);

            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                ListaProdutos.Items.Clear(); // Clear previous items

                // Use a HashSet to track added products to avoid duplicates
                HashSet<int> addedProductIds = new HashSet<int>();

                int count = 0; // Debugging: Count the number of products read
                while (reader.Read())
                {
                    int codigo = (int)reader["Codigo"];
                    if (!addedProductIds.Contains(codigo))
                    {
                        Produto product = new Produto
                        {
                            Nome = reader["Nome"].ToString(),
                            Tipo_de_Produto = reader["Tipo_de_Produto"].ToString(),
                            Codigo = codigo,
                            Preco = (double)reader["Preco"],
                            Taxa_de_iva = (double)reader["Taxa_de_iva"],
                            Unidade_medida = reader["Unidade_medida"].ToString(),
                        };

                        ListaProdutos.Items.Add(product);
                        addedProductIds.Add(codigo);
                        count++; // Increment count for each product added
                    }
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
                QuantidadeVendidaBox.Text = GetQuantidadeVendida(selectedproduct.Codigo).ToString();

                LoadQuintas(selectedproduct.Codigo);


            }

        }
        private int GetQuantidadeVendida(int produtoCodigo)
        {
            string query = @"SELECT AgroTrack.CalcularQuantidadeProdutoVendido(@ProductId) AS TotalProductCount";
            SqlCommand sqlCommand = new SqlCommand(query, cn);
            sqlCommand.Parameters.AddWithValue("@ProductId", produtoCodigo);
            return (int)sqlCommand.ExecuteScalar();
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


        private void LoadQuintas(int Codigo)
        {
            string query = @"SELECT DISTINCT Codigo,Nome,Preco, Taxa_de_iva, Unidade_medida, Tipo_de_Produto, Quantidade, Quinta_Empresa_Id_Empresa, Morada,Contacto
            FROM AgroTrack.ProdutoEmpresaQuinta  
            WHERE Codigo = @Codigo;";
            SqlCommand cmd = new SqlCommand(query, cn);
            cmd.Parameters.AddWithValue("@Codigo", Codigo);

            HashSet<int> quintaIds = new HashSet<int>(); // HashSet para armazenar IDs das quintas já adicionadas

            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                QuintasProdutos.Items.Clear(); // Clear previous items
                AddProdutoToQuintaID.Items.Clear();
                while (reader.Read())
                {
                    int quintaId = (int)reader["Quinta_Empresa_Id_Empresa"];

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
                        AddProdutoToQuintaID.Items.Add(farm);
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
            string query = @"SELECT AgroTrack.GetTotalNumberOfProductInAllFarms(@ProductId) AS TotalProductCount";
            SqlCommand sqlCommand = new SqlCommand(query, cn);
            sqlCommand.Parameters.AddWithValue("@ProductId", produtoCodigo);
            return (int)sqlCommand.ExecuteScalar();
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
                FiltrarPorQuinta.Items.Add("Todas as Quintas");
                while (reader.Read())
                {
                    QuintaOnlyName Farm = new QuintaOnlyName
                    {
                        Id_Quinta = (int)reader["Empresa_Id_Empresa"],
                        Nome = reader["Nome"].ToString(),
                    };
                    FiltrarPorQuinta.Items.Add(Farm);
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
            LoadProdutos();
        }

        private void OrdenarProdutos_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            for (int i = 0; i < Ordenar.Items.Count; i++)
            {
                if (i != e.Index)
                {
                    Ordenar.SetItemChecked(i, false);
                }
            }

            if (e.NewValue == CheckState.Checked)
            {
                switch (e.Index)
                {
                    case 0:
                        OrdenarProdutosFunc("NomeCrescente");
                        break;
                    case 1:
                        OrdenarProdutosFunc("NomeDecrescente");
                        break;
                    case 2:
                        OrdenarProdutosFunc("IDCrescente");
                        break;
                    case 3:
                        OrdenarProdutosFunc("IDDecrescente");
                        break;
                    case 4:
                        OrdenarProdutosFunc("SalarioCrescente");
                        break;
                    case 5:
                        OrdenarProdutosFunc("SalarioDecrescente");
                        break;
                    case 6:
                        OrdenarProdutosFunc("IvaCrescente");
                        break;
                    case 7:
                        OrdenarProdutosFunc("IvaDecrescente");
                        break;
                    default:
                        break;
                }
            }
        }

        private void OrdenarProdutosFunc(string order)
        {
            SqlCommand cmd = new SqlCommand("AgroTrack.OrdenarProdutos", cn) { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@Order", order);
            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                ListaProdutos.Items.Clear(); // Clear previous items
                while (reader.Read())
                {
                    Produto product = new Produto
                    {
                        Nome = reader["Nome"].ToString(),
                        Tipo_de_Produto = reader["Tipo_de_Produto"].ToString(),
                        Codigo = (int)reader["Codigo"],
                        Preco = (double)reader["Preco"],
                        Taxa_de_iva = (double)reader["Taxa_de_iva"],
                        Unidade_medida = reader["Unidade_medida"].ToString(),
                    };
                    ListaProdutos.Items.Add(product);
                }

                reader.Close(); // Close the DataReader after reading its results
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to retrieve data from database: " + ex.Message);
            }

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
            string query = "SELECT Nome, Codigo, Preco, Taxa_de_iva, Tipo_de_Produto, Unidade_medida FROM AgroTrack.Produto";
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


        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        //ConfirmarOperacao
        private void ConfirmarOperacao_Click(object sender, EventArgs e)
        {
            if (UnidadeAdicionarBox.Text == "" || ProdutoIvaBox.Text == "" || TipoAdicionarBox.Text == "" || ProdutoPrecoBox.Text == "")
            {
                MessageBox.Show("Por favor preencha todos os campos!");
            }
            else
            {
                try
                {
                    double preco = double.Parse(ProdutoPrecoBox.Text);
                    double iva = double.Parse(ProdutoIvaBox.SelectedItem.ToString());
                    AddProduto(ProdutoAdicionarBox.Text, UnidadeAdicionarBox.Text, iva, TipoAdicionarBox.Text, preco);
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
                    LocaldeProducao.Hide();
                    TipoAdicionarBox.Hide();

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
                    ProdutoUnidade.Show();
                    ProdutoDisponivel.Show();
                    ListaProdutos.Items.Clear();
                    LoadProdutos();
                }
            }
        }

        private void AddProduto(string ProdutoNome, string unidademedidaValue, double IvaValue, string tipo, double preco)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("AddProduto", cn) { CommandType = CommandType.StoredProcedure })
                {
                    // Adiciona os parâmetros ao comando
                    command.Parameters.Add(new SqlParameter("@NomeProduto", ProdutoNome));
                    command.Parameters.Add(new SqlParameter("@Tipo_de_Produto", tipo));
                    command.Parameters.Add(new SqlParameter("@Preco", preco));
                    command.Parameters.Add(new SqlParameter("@Taxa_de_iva", IvaValue));
                    command.Parameters.Add(new SqlParameter("@Unidade_medida", unidademedidaValue));

                    // Verifica o estado da conexão e abre se necessário
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    // Executa o comando
                    command.ExecuteNonQuery();

                    // Exibe mensagem de sucesso
                    MessageBox.Show("Produto adicionado com sucesso!");
                }
            }
            catch (Exception ex)
            {
                // Fecha a conexão se estiver aberta
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }

                // Lança a exceção
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
            CodigoAdicionarBox.Hide();
            CodigoAdicionarText.Hide();
            ProdutoQuantidade.Hide();



            // Enable input fields
            CodigoAdicionarBox.ReadOnly = false;
            ProdutoAdicionarBox.ReadOnly = false;
            ProdutoPrecoBox.ReadOnly = false;


            // Clear input fields
            CodigoAdicionarBox.Text = "";
            ProdutoAdicionarBox.Text = "";
            UnidadeAdicionarBox.Text = "";
            ProdutoIvaBox.Text = "";
            ProdutoPrecoBox.Text = "";
            TipoAdicionarBox.Text = "";


            ProdutoPrecoBox.Show();
            ProdutoAdicionarBox.Show();
            ConfirmarOperacao.Show();
            ProdutoIvaBox.Show();
            UnidadeAdicionarBox.Show();
            TipoAdicionarBox.Show();
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
            searchBar(inputRetalhistaNome, "RetalhistasE");
        }

        //PesquisarNomeTransporte
        private void button28_Click(object sender, EventArgs e)
        {
            PesquisarNomeTransporte.Text = string.Empty;
        }

        private void PesquisarNomeTransporte_TextChanged(object sender, EventArgs e)
        {
            string inputTransporteNome = (string)PesquisarNomeTransporte.Text;
            searchBar(inputTransporteNome, "TransportesE");
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
                        Contacto = (int)reader["Contacto"],

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

                DateTime dataSelecionada = DataEncomendaTransportes.Value.Date;
                DateTime dataAtual = DateTime.Now.Date;

                // Verifica se a data selecionada é diferente e maior que a data atual
                if (dataSelecionada > dataAtual)
                {
                    LoadEncomendasRealizadas(selectedretalho.Empresa_Id_Empresa, DataRetalhistasEncomenda.Value);
                }
                else
                {
                    LoadEncomendasRealizadas(selectedretalho.Empresa_Id_Empresa);
                }

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
                DataEntregaInicio.ReadOnly = true;
                TransportesTipo.ReadOnly = true;

                TransportesNome.Text = selectedtransporte.Nome;
                TransportesMorada.Text = selectedtransporte.Morada;
                TransportesContacto.Text = selectedtransporte.Contacto.ToString();

                DataEntregaInicio.Text = "";

                DateTime dataSelecionada = DataEncomendaTransportes.Value.Date;
                DateTime dataAtual = DateTime.Now.Date;

                // Verifica se a data selecionada é diferente e maior que a data atual
                if (dataSelecionada > dataAtual)
                {
                    LoadEncomendasEntrega(selectedtransporte.Empresa_Id_Empresa, dataSelecionada);
                }
                else
                {
                    LoadEncomendasEntrega(selectedtransporte.Empresa_Id_Empresa);
                }


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
            ListaAgricultores.Hide();
            textBox18.Hide();
            OrdenarAgricultores.Hide();
            button14.Hide();
            label29.Hide();
            button15.Hide();
            label36.Hide();
            ColheuProduto.Hide();
            TrabalhaQuinta.Hide();
            QuantidadeColheitas.Hide();
            label35.Hide();
            label33.Hide();
            ApagarAgricultor.Hide();
            AddColheita.Hide();
            ApagarColheita.Hide();
            ListaColheitas.Hide();
            ContratoLabel.Hide();
            label34.Hide();
            label10.Show();
            DescricaoContrato.Show();
            label8.Show();
            SalarioAdicionarAgricultor.Show();
            label7.Show();
            label11.Show();
            InicioContrato.Show();
            FimContrato.Show();
            SubmeterAgricultor.Show();

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

        }

        private void CodigoAdicionarBox_TextChanged(object sender, EventArgs e)
        {

        }
        private void AddAgricultor(string nome, int numeroCC, QuintaOnlyName quinta, int contacto, string descricao, double salario, DateTime inicio, DateTime fim)
        {
            using (SqlCommand command = new SqlCommand("AddAgricultor", cn) { CommandType = CommandType.StoredProcedure })
            {
                command.Parameters.Add(new SqlParameter("@Nome", nome));
                command.Parameters.Add(new SqlParameter("@N_CartaoCidadao", numeroCC));
                command.Parameters.Add(new SqlParameter("@QuintaId", quinta.Id_Quinta));
                command.Parameters.Add(new SqlParameter("@Contacto", contacto));
                command.Parameters.Add(new SqlParameter("@DescricaoContrato", descricao));
                command.Parameters.Add(new SqlParameter("@Salario", salario));
                command.Parameters.Add(new SqlParameter("@ContractStartDate", inicio));
                command.Parameters.Add(new SqlParameter("@ContractEndDate", fim));
                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Agricultor adicionado com sucesso!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to add agricultor to database: " + ex.Message);
                }
            }
        }

        private void SubmeterAgricultor_Click(object sender, EventArgs e)
        {
            if (AgricultorNome.Text == "" || AgricultorNumeroCC.Text == "" || AgricultorContacto.Text == "" || SelectQuintaAddAgricultor.SelectedIndex == -1 || DescricaoContrato.Text == "" || SalarioAdicionarAgricultor.Text == "")
            {
                MessageBox.Show("Por favor preencha todos os campos!");
            }
            else
            {
                try
                {
                    QuintaOnlyName quinta = SelectQuintaAddAgricultor.SelectedItem as QuintaOnlyName;
                    if (quinta == null)
                    {
                        MessageBox.Show("Erro ao recuperar a quinta selecionada.");
                        return;
                    }
                    AddAgricultor(AgricultorNome.Text, int.Parse(AgricultorNumeroCC.Text), quinta, int.Parse(AgricultorContacto.Text), DescricaoContrato.Text, double.Parse(SalarioAdicionarAgricultor.Text), InicioContrato.Value, FimContrato.Value);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao adicionar agricultor: " + ex.Message);
                }
                finally
                {
                    SubmeterAgricultor.Hide();
                    label10.Hide();
                    DescricaoContrato.Hide();
                    label8.Hide();
                    SalarioAdicionarAgricultor.Hide();
                    label7.Hide();
                    label11.Hide();
                    InicioContrato.Hide();
                    FimContrato.Hide();
                    AgricultorNome.ReadOnly = true;
                    AgricultorNumeroCC.ReadOnly = true;
                    AgricultorContacto.ReadOnly = true;
                    AgricultorQuinta.ReadOnly = true;
                    AgricultorNome.Text = "";
                    AgricultorNumeroCC.Text = "";
                    AgricultorContacto.Text = "";
                    AgricultorQuinta.Text = "";
                    button15.Show();
                    button14.Show();
                    label29.Show();
                    label36.Show();
                    ColheuProduto.Show();
                    TrabalhaQuinta.Show();
                    QuantidadeColheitas.Show();
                    label35.Show();
                    label33.Show();
                    ApagarAgricultor.Show();
                    label34.Show();
                    ListaAgricultores.Show();
                    textBox18.Show();
                    OrdenarAgricultores.Show();
                    AddColheita.Show();
                    ApagarColheita.Show();
                    ListaColheitas.Show();
                    AgricultorContacto.Show();
                    ContratoLabel.Show();
                    ListaAgricultores.Items.Clear();
                    LoadAgricultor();
                }
            }
        }

        private void ApagarAgricultor_Click(object sender, EventArgs e)
        {
            sbyte index = (sbyte)ListaAgricultores.SelectedIndex;
            if (index == -1)
            {
                MessageBox.Show("Por favor selecione um agricultor para remover!");
            }
            else
            {
                try
                {
                    RemoveAgricultor((ListaAgricultores.SelectedItem as Agricultores).Pessoa_N_CartaoCidadao);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao remover quinta: " + ex.Message);
                }
                finally
                {
                    ListaAgricultores.Items.Clear();
                    LoadAgricultor();
                }
            }
        }
        private void RemoveAgricultor(int numeroCC)
        {
            using (SqlCommand command = new SqlCommand("ApagarAgricultor", cn) { CommandType = CommandType.StoredProcedure })
            {
                command.Parameters.Add(new SqlParameter("@Pessoa_N_CartaoCidadao", numeroCC));
                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Agricultor removido com sucesso!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to remove agricultor from database: " + ex.Message);
                }
            }
        }

        private void AddColheitaProdutosLista_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void AddColheita_Click(object sender, EventArgs e)
        {
            SubmeterAgricultor.Hide();
            label10.Hide();
            DescricaoContrato.Hide();
            label8.Hide();
            SalarioAdicionarAgricultor.Hide();
            label7.Hide();
            label11.Hide();
            InicioContrato.Hide();
            FimContrato.Hide();
            AgricultorNome.ReadOnly = true;
            AgricultorNumeroCC.ReadOnly = true;
            AgricultorContacto.ReadOnly = true;
            AgricultorQuinta.ReadOnly = true;
            AgricultorNome.Text = "";
            AgricultorNumeroCC.Text = "";
            AgricultorContacto.Text = "";
            AgricultorQuinta.Text = "";
            button15.Hide();
            button14.Hide();
            label29.Hide();
            label36.Hide();
            ColheuProduto.Hide();
            TrabalhaQuinta.Hide();
            QuantidadeColheitas.Hide();
            label35.Hide();
            label33.Hide();
            ApagarAgricultor.Hide();
            label34.Hide();
            ListaAgricultores.Hide();
            textBox18.Hide();
            OrdenarAgricultores.Hide();
            AddColheita.Hide();
            ApagarColheita.Hide();
            ListaColheitas.Hide();
            AgricultorContacto.Hide();
            ContratoLabel.Hide();
            SelectQuintaAddAgricultor.Hide();

            label31.Hide();
            AgricultorNome.Hide();
            AgricultorContacto.Hide();
            AgricultorNumeroCC.Hide();
            AgricultorQuinta.Hide();
            AgricultorContrato.Hide();
            label28.Hide();
            label32.Hide();
            QuintaDoAgricultor.Hide();

            AddColheitaAgricultor.Show();
            AddColheitaListaAgricultores.Show();
            AddColheitaProdutosLista.Show();
            AddColheitaProduto.Show();
            AddColheitaQuantidade.Show();
            AddColheitaQuantidadeLabel.Show();
            AddColheitaDuracao.Show();
            AddColheitaDuracaoTexto.Show();
            AddColheitaData.Show();
            AddColheitaDataLabel.Show();
            AddColheitaValidade.Show();
            AddColheitaValidadeLabel.Show();
            SubmeterColheita.Show();


        }

        private void SubmeterColheita_Click(object sender, EventArgs e)
        {
            if (AddColheitaListaAgricultores.SelectedIndex == -1 || AddColheitaProdutosLista.SelectedIndex == -1 || AddColheitaQuantidadeLabel.Text == "" || AddColheitaDuracao.Text == "")
            {
                MessageBox.Show("Por favor preencha todos os campos!");
            }
            else
            {
                try
                {
                    Agricultores agricultor = AddColheitaListaAgricultores.SelectedItem as Agricultores;
                    ProdutosOnlyName produto = AddColheitaProdutosLista.SelectedItem as ProdutosOnlyName;
                    AdicionarColheita(agricultor.Pessoa_N_CartaoCidadao, produto.Id_Produto, int.Parse(AddColheitaQuantidadeLabel.Text), int.Parse(AddColheitaDuracaoTexto.Text), AddColheitaData.Value, AddColheitaValidade.Value);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao adicionar colheita: " + ex.Message);
                }
                finally
                {
                    AddColheitaAgricultor.Hide();
                    AddColheitaListaAgricultores.Hide();
                    AddColheitaProdutosLista.Hide();
                    AddColheitaProduto.Hide();
                    AddColheitaQuantidade.Hide();
                    AddColheitaQuantidadeLabel.Hide();
                    AddColheitaDuracao.Hide();
                    AddColheitaDuracaoTexto.Hide();
                    AddColheitaData.Hide();
                    AddColheitaDataLabel.Hide();
                    AddColheitaValidade.Hide();
                    AddColheitaValidadeLabel.Hide();
                    SubmeterColheita.Hide();
                    button15.Show();
                    button14.Show();
                    label29.Show();
                    label36.Show();
                    ColheuProduto.Show();
                    TrabalhaQuinta.Show();
                    QuantidadeColheitas.Show();
                    label35.Show();
                    label33.Show();
                    ApagarAgricultor.Show();
                    label34.Show();
                    ListaAgricultores.Show();
                    textBox18.Show();
                    OrdenarAgricultores.Show();
                    AddColheita.Show();
                    ApagarColheita.Show();
                    ListaColheitas.Show();
                    AgricultorContacto.Show();
                    ContratoLabel.Show();
                    label31.Show();
                    AgricultorNome.Show();
                    AgricultorContacto.Show();
                    AgricultorNumeroCC.Show();
                    AgricultorQuinta.Show();
                    AgricultorContrato.Show();
                    label28.Show();
                    label32.Show();
                    QuintaDoAgricultor.Show();



                    ListaColheitas.Items.Clear();
                }
            }
        }
        private void AdicionarColheita(int agricultor, int produto, int quantidade, int duracao, DateTime data, DateTime validade)
        {
            using (SqlCommand command = new SqlCommand("AddColheita", cn) { CommandType = CommandType.StoredProcedure })
            {
                command.Parameters.Add(new SqlParameter("@Agricultor_Pessoa_N_CartaoCidadao", agricultor));
                command.Parameters.Add(new SqlParameter("@Produto_codigo", produto));
                command.Parameters.Add(new SqlParameter("@Quantidade", quantidade));
                command.Parameters.Add(new SqlParameter("@Duracao_colheita", duracao));
                command.Parameters.Add(new SqlParameter("@DataColheita", data));
                command.Parameters.Add(new SqlParameter("@Data_de_validade", validade));
                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Colheita adicionada com sucesso!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to add colheita to database: " + ex.Message);
                }
            }
        }

        private void ApagarColheita_Click(object sender, EventArgs e)
        {
            sbyte index = (sbyte)ListaColheitas.SelectedIndex;
            if (index == -1)
            {
                MessageBox.Show("Por favor selecione uma colheita para remover!");
            }
            else
            {
                try
                {
                    RemoveColheita((ListaColheitas.SelectedItem as Colheita).Produto_codigo, (ListaColheitas.SelectedItem as Colheita).Pessoa_N_CartaoCidadao, (ListaColheitas.SelectedItem as Colheita).DataColheita);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao remover quinta: " + ex.Message);
                }
                finally
                {
                    ListaColheitas.Items.Clear();
                    LoadAgricultor();
                }
            }
        }
        private void RemoveColheita(int codigo, int cartaoCC, DateTime date)
        {
            using (SqlCommand command = new SqlCommand("ApagarColheita", cn) { CommandType = CommandType.StoredProcedure })
            {
                command.Parameters.Add(new SqlParameter("@Produto_codigo", codigo));
                command.Parameters.Add(new SqlParameter("@Agricultor_Pessoa_N_CartaoCidadao", cartaoCC));
                command.Parameters.Add(new SqlParameter("@DataColheita", date));
                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Colheita removida com sucesso!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to remove colheita from database: " + ex.Message);
                }
            }

        }

        private void textBox18_TextChanged(object sender, EventArgs e)
        {
            searchBar(textBox18.Text, "AgriculQuinta");
        }
        // Clientes

        private void LoadClientes()
        {
            ListaClientes.Items.Clear();
            string query = "SELECT Pessoa_N_CartaoCidadao, Nome, Contacto FROM AgroTrack.Cliente;";
            SqlCommand cmd = new SqlCommand(query, cn);

            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Cliente cliente = new Cliente
                    {
                        Pessoa_N_CartaoCidadao = (int)reader["Pessoa_N_CartaoCidadao"],
                        Nome = reader["Nome"].ToString(),
                        Contacto = (int)reader["Contacto"]
                    };

                    ListaClientes.Items.Add(cliente);
                }
                reader.Close();
                LoadFiltersClientes();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to retrieve data from database: " + ex.Message);
            }
        }

        private void PesquisaPorNomeCliente_TextChanged(object sender, EventArgs e)
        {
            searchBar(PesquisaPorNomeCliente.Text, "Cliente");
        }

        private void ListaClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ListaClientes.SelectedItem is Cliente selectedcliente)
            {
                NomeClientes.ReadOnly = true;
                ContactoCliente.ReadOnly = true;
                CCclientes.ReadOnly = true;

                NomeClientes.Text = selectedcliente.Nome;
                ContactoCliente.Text = selectedcliente.Contacto.ToString();
                CCclientes.Text = selectedcliente.Pessoa_N_CartaoCidadao.ToString();

                LoadCompras(selectedcliente.Pessoa_N_CartaoCidadao);
            }
        }
        private void LoadCompras(int cartaoCC)
        {
            string query = "SELECT Pessoa_N_CartaoCidadao, DataCompra, ID_Quinta, Produto_codigo, Quantidade, Preco, Metodo_de_pagamento, Nome, Tipo_de_Produto FROM AgroTrack.ClienteCompra WHERE Pessoa_N_CartaoCidadao = @CartaoCC;";
            SqlCommand cmd = new SqlCommand(query, cn);
            cmd.Parameters.AddWithValue("@CartaoCC", cartaoCC);

            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                ListaCompras.Items.Clear(); // Clear previous items
                while (reader.Read())
                {
                    Compra compra = new Compra
                    {
                        Pessoa_N_CartaoCidadao = (int)reader["Pessoa_N_CartaoCidadao"],
                        ID_Quinta = (int)reader["ID_Quinta"],
                        DataCompra = (DateTime)reader["DataCompra"],
                        Produto_codigo = (int)reader["Produto_codigo"],
                        Quantidade = (int)reader["Quantidade"],
                        Preco = (double)reader["Preco"],
                        Metodo_de_pagamento = reader["Metodo_de_pagamento"].ToString(),
                        Nome = reader["Nome"].ToString(),
                        Tipo_de_Produto = reader["Tipo_de_Produto"].ToString()
                    };
                    ListaCompras.Items.Add(compra);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to retrieve data from database: " + ex.Message);
            }
        }
        private void LoadFiltersClientes()
        {
            string query = "SELECT Codigo, Nome FROM AgroTrack.Produto;";
            SqlCommand cmd = new SqlCommand(query, cn);
            cmd = new SqlCommand(query, cn);
            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                AddCompraProduto.Items.Clear(); // Clear previous items
                ComprouProduto.Items.Clear(); // Clear previous items

                while (reader.Read())
                {
                    ProdutosOnlyName produto = new ProdutosOnlyName
                    {
                        Id_Produto = (int)reader["Codigo"],
                        Produto = reader["Nome"].ToString()
                    };
                    AddCompraProduto.Items.Add(produto);
                    ComprouProduto.Items.Add(produto);
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
                AddCompraQuinta.Items.Clear(); // Clear previous items
                ComprouQuinta.Items.Clear(); // Clear previous items
                while (reader.Read())
                {
                    QuintaOnlyName Farm = new QuintaOnlyName
                    {
                        Id_Quinta = (int)reader["Empresa_Id_Empresa"],
                        Nome = reader["Nome"].ToString(),
                    };
                    AddCompraQuinta.Items.Add(Farm);
                    ComprouQuinta.Items.Add(Farm);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to retrieve plants from database: " + ex.Message);
            }

            query = "SELECT Pessoa_N_CartaoCidadao, Nome, Contacto FROM AgroTrack.Cliente;";
            cmd = new SqlCommand(query, cn);
            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                AddCompraCliente.Items.Clear(); // Clear previous items
                while (reader.Read())
                {
                    Cliente cliente = new Cliente
                    {
                        Pessoa_N_CartaoCidadao = (int)reader["Pessoa_N_CartaoCidadao"],
                        Nome = reader["Nome"].ToString(),
                        Contacto = (int)reader["Contacto"]
                    };
                    AddCompraCliente.Items.Add(cliente);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to retrieve data from database: " + ex.Message);
            }
        }

        private void ComprouProduto_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyCombinedFiltersClientes();
        }

        private void ComprouQuinta_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyCombinedFiltersClientes();
        }

        private void NumeroComprasCliente_ValueChanged(object sender, EventArgs e)
        {
            ApplyCombinedFiltersClientes();
        }

        private void ApplyCombinedFiltersClientes()
        {
            var productId = (ComprouProduto.SelectedItem as ProdutosOnlyName)?.Id_Produto ?? null;
            var quintaId = (ComprouQuinta.SelectedItem as QuintaOnlyName)?.Id_Quinta ?? null;
            var quantidade = (int)NumeroComprasCliente.Value;

            string query = "SELECT N_CartaoCidadao, Nome, Contacto FROM AgroTrack.FiltrarClientes(@ProdutoCodigo, @QuintaId, @NumeroCompras);";
            SqlCommand cmd = new SqlCommand(query, cn);
            cmd.Parameters.AddWithValue("@ProdutoCodigo", productId ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@QuintaId", quintaId ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@NumeroCompras", quantidade == 0 ? (object)DBNull.Value : quantidade);
            cmd.ExecuteNonQuery();

            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                ListaClientes.Items.Clear(); // Clear previous items
                while (reader.Read())
                {
                    Cliente cliente = new Cliente
                    {
                        Pessoa_N_CartaoCidadao = (int)reader["N_CartaoCidadao"],
                        Nome = reader["Nome"].ToString(),
                        Contacto = (int)reader["Contacto"]
                    };
                    ListaClientes.Items.Add(cliente);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to retrieve data from database: " + ex.Message);
            }

        }

        private void AdicionarCliente_Click(object sender, EventArgs e)
        {
            // Hide controls
            label44.Hide();
            PesquisaPorNomeCliente.Hide();
            button23.Hide();
            ListaClientes.Hide();
            OrdenarClientes.Hide();
            label40.Hide();
            label37.Hide();
            label39.Hide();
            label38.Hide();
            ComprouProduto.Hide();
            ComprouQuinta.Hide();
            NumeroComprasCliente.Hide();
            ListaCompras.Hide();
            ApagarCliente.Hide();
            AdicionarCompra.Hide();
            SubmeterCliente.Show();


            NomeClientes.ReadOnly = false;
            ContactoCliente.ReadOnly = false;
            CCclientes.ReadOnly = false;

            // Clear input fields
            NomeClientes.Text = "";
            ContactoCliente.Text = "";
            CCclientes.Text = "";
        }

        private void SubmeterCliente_Click(object sender, EventArgs e)
        {
            if (NomeClientes.Text == "" || ContactoCliente.Text == "" || CCclientes.Text == "")
            {
                MessageBox.Show("Por favor preencha todos os campos!");
            }
            else
            {
                try
                {
                    AddCliente(NomeClientes.Text, int.Parse(CCclientes.Text), int.Parse(ContactoCliente.Text));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao adicionar cliente: " + ex.Message);
                }
                finally
                {
                    SubmeterCliente.Hide();
                    NomeClientes.ReadOnly = true;
                    ContactoCliente.ReadOnly = true;
                    CCclientes.ReadOnly = true;
                    NomeClientes.Text = "";
                    ContactoCliente.Text = "";
                    CCclientes.Text = "";
                    label44.Show();
                    PesquisaPorNomeCliente.Show();
                    button23.Show();
                    ListaClientes.Show();
                    OrdenarClientes.Show();
                    label40.Show();
                    label37.Show();
                    label39.Show();
                    label38.Show();
                    ComprouProduto.Show();
                    ComprouQuinta.Show();
                    NumeroComprasCliente.Show();
                    ListaCompras.Show();
                    ApagarCliente.Show();
                    AdicionarCompra.Show();
                    ListaClientes.Items.Clear();
                    LoadClientes();
                }
            }
        }
        private void AddCliente(string nome, int numeroCC, int contacto)
        {
            using (SqlCommand command = new SqlCommand("AgroTrack.AdicionarCliente", cn) { CommandType = CommandType.StoredProcedure })
            {
                command.Parameters.Add(new SqlParameter("@Nome", nome));
                command.Parameters.Add(new SqlParameter("@N_CartaoCidadao", numeroCC));
                command.Parameters.Add(new SqlParameter("@Contacto", contacto));
                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Cliente adicionado com sucesso!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to add cliente to database: " + ex.Message);
                }
            }
        }

        private void ApagarCliente_Click(object sender, EventArgs e)
        {
            if (ListaClientes.SelectedItem is Cliente selectedcliente)
            {
                try
                {
                    RemoveCliente(selectedcliente.Pessoa_N_CartaoCidadao);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao remover cliente: " + ex.Message);
                }
                finally
                {
                    ListaClientes.Items.Clear();
                    LoadClientes();
                }
            }
            else
            {
                MessageBox.Show("Por favor selecione um cliente para remover!");
            }
        }

        private void RemoveCliente(int numeroCC)
        {
            using (SqlCommand command = new SqlCommand("AgroTrack.ApagarCliente", cn) { CommandType = CommandType.StoredProcedure })
            {
                command.Parameters.Add(new SqlParameter("@Pessoa_N_CartaoCidadao", numeroCC));
                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Cliente removido com sucesso!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to remove cliente from database: " + ex.Message);
                }
            }
        }

        private void AdicionarCompra_Click(object sender, EventArgs e)
        {
            label44.Hide();
            PesquisaPorNomeCliente.Hide();
            button23.Hide();
            ListaClientes.Hide();
            OrdenarClientes.Hide();
            label40.Hide();
            label37.Hide();
            label39.Hide();
            label38.Hide();
            ComprouProduto.Hide();
            ComprouQuinta.Hide();
            NumeroComprasCliente.Hide();
            ListaCompras.Hide();
            ApagarCliente.Hide();
            AdicionarCompra.Hide();
            NomeClientes.Hide();
            ContactoCliente.Hide();
            CCclientes.Hide();
            LabelNomeCliente.Hide();
            LabelContactoCliente.Hide();
            LabelClienteCC.Hide();
            AddCompraCliente.Show();
            AddCompraProduto.Show();
            AddCompraQuinta.Show();
            AddCompraQuantidade.Show();
            AddCompraMetodo.Show();
            AddCompraData.Show();
            AddCompraClienteLabel.Show();
            AddCompraProdutoLabel.Show();
            AddCompraQuintaLabel.Show();
            AddCompraQuantidadeLabel.Show();
            AddCompraMetodoLabel.Show();
            AddCompraDataLabel.Show();
            SubmeterCompra.Show();


        }

        private void SubmeterCompra_Click(object sender, EventArgs e)
        {
            if (AddCompraCliente.SelectedIndex == -1 || AddCompraProduto.SelectedIndex == -1 || AddCompraQuinta.SelectedIndex == -1 || AddCompraQuantidade.Text == "" || AddCompraMetodo.Text == "" || AddCompraData.Text == "")
            {
                MessageBox.Show("Por favor preencha todos os campos!");
            }
            else
            {
                try
                {
                    AddCompra((AddCompraCliente.SelectedItem as Cliente).Pessoa_N_CartaoCidadao, (AddCompraProduto.SelectedItem as ProdutosOnlyName).Id_Produto, (AddCompraQuinta.SelectedItem as QuintaOnlyName).Id_Quinta, int.Parse(AddCompraQuantidade.Text), AddCompraMetodo.Text, AddCompraData.Value);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao adicionar compra: " + ex.Message);
                }
                finally
                {
                    AddCompraCliente.Hide();
                    AddCompraProduto.Hide();
                    AddCompraQuinta.Hide();
                    AddCompraQuantidade.Hide();
                    AddCompraMetodo.Hide();
                    AddCompraData.Hide();
                    AddCompraClienteLabel.Hide();
                    AddCompraProdutoLabel.Hide();
                    AddCompraQuintaLabel.Hide();
                    AddCompraQuantidadeLabel.Hide();
                    AddCompraMetodoLabel.Hide();
                    AddCompraDataLabel.Hide();
                    SubmeterCompra.Hide();
                    label44.Show();
                    PesquisaPorNomeCliente.Show();
                    button23.Show();
                    ListaClientes.Show();
                    OrdenarClientes.Show();
                    label40.Show();
                    label37.Show();
                    label39.Show();
                    label38.Show();
                    ComprouProduto.Show();
                    ComprouQuinta.Show();
                    NumeroComprasCliente.Show();
                    ListaCompras.Show();
                    ApagarCliente.Show();
                    AdicionarCompra.Show();
                    NomeClientes.Show();
                    ContactoCliente.Show();
                    CCclientes.Show();
                    LabelNomeCliente.Show();
                    LabelContactoCliente.Show();
                    LabelClienteCC.Show();
                    ListaCompras.Items.Clear();
                    LoadClientes();
                }
            }
        }

        private void AddCompra(int cliente, int produto, int quinta, int quantidade, string metodo, DateTime data)
        {
            using (SqlCommand command = new SqlCommand("AgroTrack.AdicionarCompra", cn) { CommandType = CommandType.StoredProcedure })
            {
                command.Parameters.Add(new SqlParameter("@Cliente_Pessoa_N_CartaoCidadao", cliente));
                command.Parameters.Add(new SqlParameter("@Produto_codigo", produto));
                command.Parameters.Add(new SqlParameter("@ID_Quinta", quinta));
                command.Parameters.Add(new SqlParameter("@Quantidade", quantidade));
                command.Parameters.Add(new SqlParameter("@Metodo_de_pagamento", metodo));
                command.Parameters.Add(new SqlParameter("@Data", data));
                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Compra adicionada com sucesso!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to add compra to database: " + ex.Message);
                }
            }
        }

        private void label40_Click(object sender, EventArgs e)
        {

        }

        private void OrdenarClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void OrdenarClientes_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            for (int i = 0; i < OrdenarClientes.Items.Count; i++)
            {
                if (i != e.Index)
                {
                    OrdenarClientes.SetItemChecked(i, false);
                }
            }

            if (e.NewValue == CheckState.Checked)
            {
                switch (e.Index)
                {
                    case 0:
                        OrdenarClientesFunc("NomeCrescente");
                        break;
                    case 1:
                        OrdenarClientesFunc("NomeDecrescente");
                        break;
                    case 2:
                        OrdenarClientesFunc("TelefoneCrescente");
                        break;
                    case 3:
                        OrdenarClientesFunc("TelefoneDecrescente");
                        break;
                    default:
                        break;
                }
            }
        }


        private void OrdenarClientesFunc(string order)
        {
            SqlCommand cmd = new SqlCommand("AgroTrack.OrdenarClientes", cn) { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@Order", order);

            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                ListaClientes.Items.Clear(); // Clear previous items
                while (reader.Read())
                {
                    Cliente cliente = new Cliente
                    {
                        Pessoa_N_CartaoCidadao = (int)reader["Pessoa_N_CartaoCidadao"],
                        Nome = reader["Nome"].ToString(),
                        Contacto = (int)reader["Contacto"]
                    };
                    ListaClientes.Items.Add(cliente);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to retrieve data from database: " + ex.Message);
            }
        }

        private void OrdenarAgricultores_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void OrdenarAgricultores_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            for (int i = 0; i < OrdenarAgricultores.Items.Count; i++)
            {
                if (i != e.Index)
                {
                    OrdenarAgricultores.SetItemChecked(i, false);
                }
            }

            if (e.NewValue == CheckState.Checked)
            {
                switch (e.Index)
                {
                    case 0:
                        OrdenarAgricultoresFunc("NomeCrescente");
                        break;
                    case 1:
                        OrdenarAgricultoresFunc("NomeDecrescente");
                        break;
                    case 2:
                        OrdenarAgricultoresFunc("IDCrescente");
                        break;
                    case 3:
                        OrdenarAgricultoresFunc("IDDecrescente");
                        break;
                    case 4:
                        OrdenarAgricultoresFunc("SalarioCrescente");
                        break;
                    case 5:
                        OrdenarAgricultoresFunc("SalarioDecrescente");
                        break;
                    default:
                        break;
                }
            }
        }
        private void OrdenarAgricultoresFunc(string order)
        {
            SqlCommand cmd = new SqlCommand("AgroTrack.OrdenarAgricultores", cn) { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@Order", order);
            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                ListaAgricultores.Items.Clear(); // Clear previous items
                while (reader.Read())
                {
                    Agricultores agricultor = new Agricultores
                    {
                        Id_Trabalhador = (int)reader["Id_Trabalhador"],
                        Pessoa_N_CartaoCidadao = (int)reader["Pessoa_N_CartaoCidadao"],
                        Quinta_Empresa_Id_Empresa = (int)reader["Empresa_Id_Empresa"],
                        NomeQuinta = reader["NomeQuinta"].ToString(),
                        Nome = reader["Nome"].ToString(),
                        Contacto = (int)reader["Contacto"]
                    };
                    ListaAgricultores.Items.Add(agricultor);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to retrieve data from database: " + ex.Message);
            }
        }

        private int EMPRESAID;
        private DateTime DataLimiteEncomendas = DateTime.Now;

        private void LoadEncomendasRealizadas(int empresaId, DateTime dataLimite)
        {
            int? empresaDeTransportesId = null;
            int? quintaId = null;
            EMPRESAID = empresaId;

            // Obtenha os valores selecionados nos filtros
            if (FiltrarTransporteRetalhistas.SelectedItem is TransportesOnlyName selectedTransport)
            {
                empresaDeTransportesId = selectedTransport.Empresa_Id_Empresa;
            }

            if (QuintasRetalhistas.SelectedItem is QuintaOnlyName selectedQuinta)
            {
                quintaId = selectedQuinta.Id_Quinta;
            }

            string query = @"SELECT DISTINCT Nome, Morada, Contacto, Codigo, prazo_entrega, Morada_entrega, Entrega, Retalhista_Empresa_Id_Empresa, Empresa_De_Transportes_Id_Empresa, Quinta_Empresa_Id, PrecoTotal
                     FROM AgroTrack.EmpresaEncomenda 
                     WHERE Retalhista_Empresa_Id_Empresa = @Empresa_Id_Empresa 
                     AND Entrega <= @DataLimite";

            if (empresaDeTransportesId.HasValue)
            {
                query += " AND Empresa_De_Transportes_Id_Empresa = @EmpresaDeTransportesId";
            }

            if (quintaId.HasValue)
            {
                query += " AND Quinta_Empresa_Id = @QuintaId";
            }

            SqlCommand cmd = new SqlCommand(query, cn);
            cmd.Parameters.AddWithValue("@Empresa_Id_Empresa", empresaId);
            cmd.Parameters.AddWithValue("@DataLimite", dataLimite);

            if (empresaDeTransportesId.HasValue)
            {
                cmd.Parameters.AddWithValue("@EmpresaDeTransportesId", empresaDeTransportesId.Value);
            }

            if (quintaId.HasValue)
            {
                cmd.Parameters.AddWithValue("@QuintaId", quintaId.Value);
            }

            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                EncomendasRealizadas.Items.Clear(); // Clear previous items

                while (reader.Read())
                {
                    Encomenda Order = new Encomenda
                    {
                        Codigo = (int)reader["Codigo"],
                        PrazoEntrega = (int)reader["prazo_entrega"],
                        MoradaEntrega = reader["Morada_entrega"].ToString(),
                        Entrega = DateTime.Parse(reader["Entrega"].ToString()),
                        RetalhistaEmpresaId = (int)reader["Retalhista_Empresa_Id_Empresa"],
                        EmpresaDeTransportesId = (int)reader["Empresa_De_Transportes_Id_Empresa"],
                        QuintaEmpresaId = (int)reader["Quinta_Empresa_Id"],
                        Preco = reader["PrecoTotal"] != DBNull.Value ? (double)reader["PrecoTotal"] : 0.0
                    };

                    EncomendasRealizadas.Items.Add(Order);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to retrieve data from database: " + ex.Message);
            }
        }




        private int EMPRESAENTREGAID;

        private void LoadEncomendasEntrega(int empresaId, DateTime dataLimite)
        {
            int? retalhistaId = null;
            int? quintaId = null;
            EMPRESAENTREGAID = empresaId;

            // Obtenha o valor selecionado no filtro de retalhistas
            if (FiltrarRetalhistaTransportes.SelectedItem is RetalhistasOnlyName selectedRetalhista)
            {
                retalhistaId = selectedRetalhista.Empresa_Id_Empresa;
            }

            // Obtenha o valor selecionado no filtro de quintas
            if (QuintasTransportes.SelectedItem is QuintaOnlyName selectedQuinta)
            {
                quintaId = selectedQuinta.Id_Quinta;
            }

            string query = @"SELECT DISTINCT Nome, Morada, Contacto, Codigo, prazo_entrega, Morada_entrega, Entrega, Retalhista_Empresa_Id_Empresa, Empresa_De_Transportes_Id_Empresa, Quinta_Empresa_Id 
                     FROM AgroTrack.EmpresaEncomenda 
                     WHERE Empresa_De_Transportes_Id_Empresa = @Empresa_Id_Empresa 
                     AND Entrega <= @DataLimite";

            if (retalhistaId.HasValue)
            {
                query += " AND Retalhista_Empresa_Id_Empresa = @RetalhistaId";
            }

            if (quintaId.HasValue)
            {
                query += " AND Quinta_Empresa_Id = @QuintaId";
            }

            SqlCommand cmd = new SqlCommand(query, cn);
            cmd.Parameters.AddWithValue("@Empresa_Id_Empresa", empresaId);
            cmd.Parameters.AddWithValue("@DataLimite", dataLimite);

            if (retalhistaId.HasValue)
            {
                cmd.Parameters.AddWithValue("@RetalhistaId", retalhistaId.Value);
            }

            if (quintaId.HasValue)
            {
                cmd.Parameters.AddWithValue("@QuintaId", quintaId.Value);
            }

            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                EncomendasEntrega.Items.Clear(); // Clear previous items
                while (reader.Read())
                {
                    Encomenda Order = new Encomenda
                    {
                        Codigo = (int)reader["Codigo"],
                        PrazoEntrega = (int)reader["prazo_entrega"],
                        MoradaEntrega = reader["Morada_entrega"].ToString(),
                        Entrega = DateTime.Parse(reader["Entrega"].ToString()),
                        RetalhistaEmpresaId = (int)reader["Retalhista_Empresa_Id_Empresa"],
                        EmpresaDeTransportesId = (int)reader["Empresa_De_Transportes_Id_Empresa"],
                        QuintaEmpresaId = (int)reader["Quinta_Empresa_Id"]
                    };

                    EncomendasEntrega.Items.Add(Order);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to retrieve data from database: " + ex.Message);
            }
        }




        private void label59_Click(object sender, EventArgs e)
        {

        }

        private void DataEncomendaTransportes_ValueChanged(object sender, EventArgs e)
        {
            DateTime dataSelecionada = DataEncomendaTransportes.Value.Date;
            DateTime dataAtual = DateTime.Now.Date;

            // Verifica se a data selecionada é diferente e maior que a data atual
            if (dataSelecionada > dataAtual)
            {
                if (ListaTransportes.SelectedItem is Transportes selectedTransporte)
                {

                    LoadEncomendasEntrega(selectedTransporte.Empresa_Id_Empresa, dataSelecionada);
                    ListaTransportes_SelectedIndexChanged_1(null, null);
                }
            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            PesquisaPorNomeCliente.Text = "";
        }


        //filtrar por retalhistas transportes
        private void FiltrarRetalhistaTransportes_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime dataLimite = DateTime.Now; // ou qualquer outra lógica para determinar a data limite
            LoadEncomendasEntrega(EMPRESAENTREGAID, dataLimite);
        }

        //filtrar por transpotes retalhistas
        private void FiltrarTransporteRetalhistas_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime dataLimite = DateTime.Now; // ou qualquer outra lógica para determinar a data limite
            LoadEncomendasRealizadas(EMPRESAID, dataLimite);
        }

        private void LoadFiltersTransportes()
        {
            string query = "SELECT Empresa_Id_Empresa, Nome, Morada,Contacto FROM AgroTrack.RetalhistasE;";
            SqlCommand cmd = new SqlCommand(query, cn);
            cmd = new SqlCommand(query, cn);
            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                FiltrarRetalhistaTransportes.Items.Clear(); // Clear previous items              TipoAdicionarBox.Items.Clear();
                FiltrarRetalhistaTransportes.Items.Add("Todas os retalhistas");
                HashSet<string> addedTipos = new HashSet<string>();
                while (reader.Read())
                {
                    RetalhistasOnlyName produto = new RetalhistasOnlyName
                    {
                        Nome = reader["Nome"].ToString(),
                        Morada = reader["Morada"].ToString(),
                        Contacto = (int)reader["Contacto"],
                        Empresa_Id_Empresa = (int)reader["Empresa_Id_Empresa"]
                    };
                    FiltrarRetalhistaTransportes.Items.Add(produto);
                    DataRetalhistaEncoemndabOX.Items.Add(produto);
                    RetalhistaBox.Items.Add(produto);
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
                QuintasTransportes.Items.Clear(); // Clear previous items
                QuintasTransportes.Items.Add("Todas as Quintas");
                while (reader.Read())
                {
                    QuintaOnlyName Farm = new QuintaOnlyName
                    {
                        Id_Quinta = (int)reader["Empresa_Id_Empresa"],
                        Nome = reader["Nome"].ToString(),
                    };
                    QuintasTransportes.Items.Add(Farm);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to retrieve plants from database: " + ex.Message);
            }
        }


        private void LoadFilterRetalhistas()
        {
            string query = "SELECT Empresa_Id_Empresa, Nome, Morada,Contacto FROM AgroTrack.TransportesE;";
            SqlCommand cmd = new SqlCommand(query, cn);
            cmd = new SqlCommand(query, cn);
            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                FiltrarTransporteRetalhistas.Items.Clear(); // Clear previous items              TipoAdicionarBox.Items.Clear();
                FiltrarTransporteRetalhistas.Items.Add("Todas os Transportes");
                HashSet<string> addedTipos = new HashSet<string>();
                while (reader.Read())
                {
                    TransportesOnlyName produto = new TransportesOnlyName
                    {
                        Nome = reader["Nome"].ToString(),
                        Morada = reader["Morada"].ToString(),
                        Contacto = (int)reader["Contacto"],
                        Empresa_Id_Empresa = (int)reader["Empresa_Id_Empresa"]
                    };
                    FiltrarTransporteRetalhistas.Items.Add(produto);
                    EmpresaDeTransporteEncoemndaRetalhistaBox.Items.Add(produto);
                    TransportesBox.Items.Add(produto);
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
                QuintasRetalhistas.Items.Clear(); // Clear previous items
                QuintasRetalhistas.Items.Add("Todas as Quintas");
                while (reader.Read())
                {
                    QuintaOnlyName Farm = new QuintaOnlyName
                    {
                        Id_Quinta = (int)reader["Empresa_Id_Empresa"],
                        Nome = reader["Nome"].ToString(),
                    };
                    QuintasRetalhistas.Items.Add(Farm);
                    QuintaEncoemndaRetalhistaBox.Items.Add(Farm);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to retrieve plants from database: " + ex.Message);
            }
        }


        //filtrar por quinta retalhistas

        private void QuintasRetalhistas_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime dataLimite = DateTime.Now; // ou qualquer outra lógica para determinar a data limite
            LoadEncomendasRealizadas(EMPRESAID, dataLimite);
        }

        //filtrar por quinta transportes
        private void QuintasTransportes_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime dataLimite = DateTime.Now; // ou qualquer outra lógica para determinar a data limite
            LoadEncomendasEntrega(EMPRESAENTREGAID, dataLimite);
        }

        private void AdicionarEmpresa_Click(object sender, EventArgs e)
        {
            TransportesIdEmpresa.Hide();
            TransportesTipo.Hide();
            EncomendasEntrega.Hide();
            FiltrarPorDataTransportes.Hide();
            EmpresaRetalhista.Hide();
            OrigemTransportes.Hide();
            DataEncomendaTransportes.Hide();
            FiltrarRetalhistaTransportes.Hide();
            QuintasTransportes.Hide();
            AdicionarEmpresa.Hide();
            EliminarEmpresaTransportes.Hide();
            DataEntregaInicial.Hide();
            DataEntregaInicio.Hide();



            // Enable input fields
            TransportesNome.ReadOnly = false;
            TransportesMorada.ReadOnly = false;
            TransportesContacto.ReadOnly = false;

            TransportesContacto.Text = "";
            TransportesMorada.Text = "";
            TransportesNome.Text = "";


            ConfirmarEncomenda.Show();
        }

        private void ConfirmarEncomenda_Click(object sender, EventArgs e)
        {
            if (TransportesNome.Text == "" || TransportesMorada.Text == "" || TransportesContacto.Text == "")
            {
                MessageBox.Show("Por favor preencha todos TransportesContacto campos!");
            }
            else
            {
                try
                {
                    string nome = TransportesNome.Text;
                    string morada = TransportesMorada.Text;
                    int contacto = int.Parse(TransportesContacto.Text);
                    AddEmpresaTransportes(nome, morada, contacto);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao adicionar produto: " + ex.Message);
                }
                finally
                {

                    ConfirmarEncomenda.Hide();
                    TransportesNome.ReadOnly = true;
                    TransportesMorada.ReadOnly = true;
                    TransportesContacto.ReadOnly = true;
                    TransportesContacto.Text = "";
                    TransportesMorada.Text = "";
                    TransportesNome.Text = "";
                    TransportesIdEmpresa.Show();
                    TransportesTipo.Show();
                    EncomendasEntrega.Show();
                    FiltrarPorDataTransportes.Show();
                    EmpresaRetalhista.Show();
                    OrigemTransportes.Show();
                    DataEncomendaTransportes.Show();
                    FiltrarRetalhistaTransportes.Show();
                    QuintasTransportes.Show();
                    AdicionarEmpresa.Show();
                    EliminarEmpresaTransportes.Show();
                    ListaTransportes.Items.Clear();
                    DataEntregaInicial.Show();
                    DataEntregaInicio.Show();
                    LoadTransportes();
                }
            }
        }


        private void AddEmpresaTransportes(string nome, string morada, int contacto)
        {
            using (SqlCommand command = new SqlCommand("AgroTrack.AddEmpresaTransportes", cn) { CommandType = CommandType.StoredProcedure })
            {
                command.Parameters.Add(new SqlParameter("@Nome", nome));
                command.Parameters.Add(new SqlParameter("@Morada", morada));
                command.Parameters.Add(new SqlParameter("@Contacto", contacto));
                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Empresa de transportes adicionada com sucesso!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to add empresa de transportes to database: " + ex.Message);
                }
            }
        }

        private void AdicionarEncomendaTransportes_Click(object sender, EventArgs e)
        {
            NomeTransportes.Hide();
            TransportesIdEmpresa.Hide();
            TransportesTipo.Hide();
            EncomendasEntrega.Hide();
            FiltrarPorDataTransportes.Hide();
            EmpresaRetalhista.Hide();
            OrigemTransportes.Hide();
            DataEncomendaTransportes.Hide();
            FiltrarRetalhistaTransportes.Hide();
            QuintasTransportes.Hide();
            AdicionarEmpresa.Hide();
            EliminarEmpresaTransportes.Hide();
            MoradaTransportes.Hide();
            ContactoTransportes.Hide();
            TransportesNome.Hide();
            TransportesMorada.Hide();
            TransportesContacto.Hide();
            TipoDeEmpresaRetalhista.Hide();
            RetalhistasTipo.Hide();
            RetalhistasNome.Hide();
            RetalhistasMorada.Hide();
            RetalhistasContacto.Hide();

            LoadTransportes();




            // Enable input fields
            PrazoBox.ReadOnly = false;
            MoradaBox.ReadOnly = false;


            PrazoBox.Text = "";
            MoradaBox.Text = "";
            EntregaBox.Text = "";
            RetalhistaBox.Text = "";
            TransportesBox.Text = "";
            QuintaBox.Text = "";


            ConfirmarEmpresa.Show();
            PrazoEncomenda.Show();
            PrazoBox.Show();
            MoradaEncomenda.Show();
            MoradaBox.Show();
            EntregaEncomenda.Show();
            EntregaBox.Show();
            RetalhistaEncomenda.Show();
            RetalhistaBox.Show();
            TransportesEncomenda.Show();
            TransportesBox.Show();
            QuintaEncomenda.Show();
            QuintaBox.Show();


        }

        private void ConfirmarEmpresa_Click(object sender, EventArgs e)
        {
            if (PrazoBox.Text == "" || MoradaBox.Text == "" || EntregaBox.Text == "" || RetalhistaBox.Text == "" || TransportesBox.Text == "" || QuintaBox.Text == "")
            {
                MessageBox.Show("Por favor preencha todos os campos!");
            }
            else
            {
                try
                {
                    int prazo = int.Parse(PrazoBox.Text);
                    string morada = MoradaBox.Text;
                    DateTime entrega = DateTime.Parse(EntregaBox.Text);
                    RetalhistasOnlyName comprador = (RetalhistasOnlyName)RetalhistaBox.SelectedItem;
                    TransportesOnlyName empresaDeTransporte = (TransportesOnlyName)TransportesBox.SelectedItem;
                    QuintaOnlyName quinta = (QuintaOnlyName)QuintaBox.SelectedItem;
                    if (comprador == null || empresaDeTransporte == null || quinta == null)
                    {
                        MessageBox.Show("Por favor selecione um comprador, uma empresa de transporte e uma quinta!");
                        return;
                    }

                    int compradorId = comprador.Empresa_Id_Empresa;
                    int empresaDeTransporteId = empresaDeTransporte.Empresa_Id_Empresa;
                    int quintaId = quinta.Id_Quinta;

                    AddEncomendaTransporte(prazo, morada, entrega, compradorId, empresaDeTransporteId, quintaId);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao adicionar encomenda: " + ex.Message);
                }
                finally
                {

                    ConfirmarEmpresa.Hide();
                    PrazoBox.Hide();
                    MoradaBox.Hide();
                    EntregaBox.Hide();
                    RetalhistaBox.Hide();
                    TransportesBox.Hide();
                    QuintaBox.Hide();
                    PrazoEncomenda.Hide();
                    MoradaEncomenda.Hide();
                    EntregaEncomenda.Hide();
                    RetalhistaEncomenda.Hide();
                    TransportesEncomenda.Hide();
                    QuintaEncomenda.Hide();
                    NomeTransportes.Show();
                    TransportesIdEmpresa.Show();
                    TransportesTipo.Show();
                    EncomendasEntrega.Show();
                    FiltrarPorDataTransportes.Show();
                    EmpresaRetalhista.Show();
                    OrigemTransportes.Show();
                    DataEncomendaTransportes.Show();
                    FiltrarRetalhistaTransportes.Show();
                    QuintasTransportes.Show();
                    AdicionarEmpresa.Show();
                    EliminarEmpresaTransportes.Show();
                    MoradaTransportes.Show();
                    ContactoTransportes.Show();
                    TransportesNome.Show();
                    TransportesMorada.Show();
                    TransportesContacto.Show();
                    ListaTransportes.Items.Clear();
                    LoadTransportes();
                }
            }
        }




        private void button14_Click(object sender, EventArgs e)
        {
            textBox18.Text = "";
        }

        private void AdicionarProdutoQuinta_Click(object sender, EventArgs e)
        {
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
            QuintaNome.Hide();
            QuintaMorada.Hide();
            QuintaContacto.Hide();
            SubmeterNovaQuinta.Hide();
            AdicionarProdutoQuinta.Hide();
            AdicionarAnimalPlanta.Hide();
            RemoverProdutoQuinta.Hide();
            RemoverAnimalPlanta.Hide();
            ListaQuintas.Hide();
            label1.Hide();
            label3.Hide();
            label13.Hide();

            // Mostrar campos de input
            AddProdutoToQuintaData.Show();
            AddProdutoToQuintaQuantidade.Show();
            AddProdutoToQuintaProdutoID.Show();
            AddProdutoToQuintaDataLabel.Show();
            AddProdutoToQuintaQuantidadeLabel.Show();
            AddProdutoToQuintaProdutoIDLabel.Show();
            AddProdutoToQuintaID.Show();
            AddProdutoToQuintaIDLabel.Show();
            AddProdutoToQuintaSubmit.Show();

        }

        private void dateTimePicker1_ValueChanged_1(object sender, EventArgs e)
        {

        }

        private void AddProdutoToQuintaSubmit_Click(object sender, EventArgs e)
        {
            if (AddProdutoToQuintaProdutoID.SelectedIndex == -1 || AddProdutoToQuintaQuantidade.Text == "" || AddProdutoToQuintaData.Text == "" || AddProdutoToQuintaID.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor preencha todos os campos!");
            }
            else
            {
                try
                {
                    int produtoId = (AddProdutoToQuintaProdutoID.SelectedItem as ProdutosOnlyNameMedida).Id_Produto;
                    int quantidade = int.Parse(AddProdutoToQuintaQuantidade.Text);
                    DateTime data = AddProdutoToQuintaData.Value;
                    int quintaId = (AddProdutoToQuintaID.SelectedItem as Quinta).Id_Quinta;
                    AddProdutoToQuinta(quintaId, produtoId, quantidade, data);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao adicionar produto à quinta: " + ex.Message);
                }
                finally
                {
                    AddProdutoToQuintaData.Hide();
                    AddProdutoToQuintaQuantidade.Hide();
                    AddProdutoToQuintaProdutoID.Hide();
                    AddProdutoToQuintaDataLabel.Hide();
                    AddProdutoToQuintaQuantidadeLabel.Hide();
                    AddProdutoToQuintaProdutoIDLabel.Hide();
                    AddProdutoToQuintaID.Hide();
                    AddProdutoToQuintaIDLabel.Hide();
                    AddProdutoToQuintaSubmit.Hide();
                    label48.Show();
                    label49.Show();
                    label41.Show();
                    label47.Show();
                    label4.Show();
                    PesquisarQuinta.Show();
                    PesquisaPorNomeCliente.Show();
                    Agricultores.Show();
                    ProdutosQuinta.Show();
                    Plantas.Show();
                    button20.Show();
                    RemoverQuinta.Show();
                    Animais.Show();
                    QuintaNome.Show();
                    QuintaMorada.Show();
                    QuintaContacto.Show();
                    AdicionarProdutoQuinta.Show();
                    AdicionarAnimalPlanta.Show();
                    RemoverProdutoQuinta.Show();
                    RemoverAnimalPlanta.Show();
                    ListaQuintas.Show();
                    label1.Show();
                    label3.Show();
                    label13.Show();
                    FilterByAnimalQuinta.Show();
                    FilterByPlantQuinta.Show();
                    FiltrarPorProdutoQuinta.Show();
                    QuantidadeAgricultores.Show();
                    ListaQuintas.Items.Clear();
                    LoadQuinta();
                    LoadFiltersQuinta();
                }
            }
        }
        private void AddProdutoToQuinta(int quintaId, int produtoId, int quantidade, DateTime data)
        {
            using (SqlCommand command = new SqlCommand("AddProdutoToQuinta", cn) { CommandType = CommandType.StoredProcedure })
            {
                command.Parameters.Add(new SqlParameter("@QuintaId", quintaId));
                command.Parameters.Add(new SqlParameter("@ProdutoId", produtoId));
                command.Parameters.Add(new SqlParameter("@Quantidade", quantidade));
                command.Parameters.Add(new SqlParameter("@DataDeValidade", data));
                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Produto adicionado à quinta com sucesso!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to add produto to quinta: " + ex.Message);
                }
            }
        }

        private void RemoverProdutoQuinta_Click(object sender, EventArgs e)
        {
            int quintaId = (ListaQuintas.SelectedItem as Quinta).Id_Quinta;
            if (ProdutosQuinta.SelectedItem is Produto selectedProduto)
            {
                try
                {
                    RemoveProdutoFromQuinta(quintaId, selectedProduto.Codigo);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao remover produto da quinta: " + ex.Message);
                }
                finally
                {
                    ProdutosQuinta.Items.Clear();
                    LoadQuinta();
                }
            }
            else
            {
                MessageBox.Show("Por favor selecione um produto para remover!");
            }
        }

        private void RemoveProdutoFromQuinta(int quintaId, int produtoId)
        {
            using (SqlCommand command = new SqlCommand("RemoveProdutoFromQuinta", cn) { CommandType = CommandType.StoredProcedure })
            {
                command.Parameters.Add(new SqlParameter("@QuintaId", quintaId));
                command.Parameters.Add(new SqlParameter("@ProdutoId", produtoId));
                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Produto removido da quinta com sucesso!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to remove produto from quinta: " + ex.Message);
                }
            }
        }

        private void AdicionarAnimalPlanta_Click(object sender, EventArgs e)
        {
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
            QuintaNome.Hide();
            QuintaMorada.Hide();
            QuintaContacto.Hide();
            SubmeterNovaQuinta.Hide();
            AdicionarProdutoQuinta.Hide();
            AdicionarAnimalPlanta.Hide();
            RemoverProdutoQuinta.Hide();
            RemoverAnimalPlanta.Hide();
            ListaQuintas.Hide();
            label1.Hide();
            label3.Hide();
            label13.Hide();

            AddAnimalID.Show();
            AddAnimalAnimalLabel.Show();
            AddAnimalBrinco.Show();
            AddAnimalBrincoLabel.Show();
            AddAnimalIdade.Show();
            AddAnimalIdadeLabel.Show();
            AddAnimalQuinta.Show();
            AddAnimalQuintaLabel.Show();
            AddAnimalSubmeter.Show();
        }

        private void AddAnimalSubmeter_Click(object sender, EventArgs e)
        {
            if (AddAnimalID.SelectedIndex == -1 || AddAnimalBrinco.Text == "" || AddAnimalIdade.Text == "" || AddAnimalQuinta.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor preencha todos os campos!");
            }
            else
            {
                try
                {
                    int animalId = (AddAnimalID.SelectedItem as AnimalOnlyName).Id;
                    string brinco = (AddAnimalBrinco.Text);
                    int idade = int.Parse(AddAnimalIdade.Text);
                    int quintaId = (AddAnimalQuinta.SelectedItem as Quinta).Id_Quinta;
                    AddAnimalToQuinta(quintaId, animalId, brinco, idade);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao adicionar animal à quinta: " + ex.Message);
                }
                finally
                {
                    AddAnimalID.Hide();
                    AddAnimalAnimalLabel.Hide();
                    AddAnimalBrinco.Hide();
                    AddAnimalBrincoLabel.Hide();
                    AddAnimalIdade.Hide();
                    AddAnimalIdadeLabel.Hide();
                    AddAnimalQuinta.Hide();
                    AddAnimalQuintaLabel.Hide();
                    AddAnimalSubmeter.Hide();
                    label48.Show();
                    label49.Show();
                    label41.Show();
                    label47.Show();
                    label4.Show();
                    PesquisarQuinta.Show();
                    PesquisaPorNomeCliente.Show();
                    Agricultores.Show();
                    ProdutosQuinta.Show();
                    Plantas.Show();
                    button20.Show();
                    RemoverQuinta.Show();
                    Animais.Show();
                    QuintaNome.Show();
                    QuintaMorada.Show();
                    QuintaContacto.Show();
                    AdicionarProdutoQuinta.Show();
                    AdicionarAnimalPlanta.Show();
                    RemoverProdutoQuinta.Show();
                    RemoverAnimalPlanta.Show();
                    ListaQuintas.Show();
                    label1.Show();
                    label3.Show();
                    label13.Show();
                    FilterByAnimalQuinta.Show();
                    FilterByPlantQuinta.Show();
                    FiltrarPorProdutoQuinta.Show();
                    QuantidadeAgricultores.Show();
                    ListaQuintas.Items.Clear();
                    LoadQuinta();
                    LoadFiltersQuinta();
                }
            }
        }
        private void AddAnimalToQuinta(int quintaId, int animalId, string brinco, int idade)
        {
            using (SqlCommand command = new SqlCommand("AddAnimalToQuinta", cn) { CommandType = CommandType.StoredProcedure })
            {
                command.Parameters.Add(new SqlParameter("@Quinta_Id", quintaId));
                command.Parameters.Add(new SqlParameter("@Id_Animal", animalId));
                command.Parameters.Add(new SqlParameter("@Brinco", brinco));
                command.Parameters.Add(new SqlParameter("@Idade", idade));
                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Animal adicionado à quinta com sucesso!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to add animal to quinta: " + ex.Message);
                }
            }
        }

        private void AdicionarRetalhistas_Click(object sender, EventArgs e)
        {
            RetalhistasNome.Text = "";
            RetalhistasMorada.Text = "";
            RetalhistasContacto.Text = "";

            RetalhistasNome.ReadOnly = false;
            RetalhistasMorada.ReadOnly = false;
            RetalhistasContacto.ReadOnly = false;



            ConfirmarRetalhista.Show();

            dataRetalhista.Hide();
            empresaretalhistas.Hide();
            QuintaRetalhistaSelecao.Hide();
            DataRetalhistasEncomenda.Hide();
            FiltrarTransporteRetalhistas.Hide();
            QuintasRetalhistas.Hide();
            AdicionarRetalhistas.Hide();
            AdicionarEncomendasRetalhista.Hide();
            EliminarRetalhista.Hide();
            CancelarEncoemendRetalhistas.Hide();
            EncomendasRealizadas.Hide();
            TipoDeEmpresaRetalhista.Hide();
            RetalhistasTipo.Hide();



        }

        private void ConfirmarRetalhista_Click(object sender, EventArgs e)
        {
            if (RetalhistasNome.Text == "" || RetalhistasMorada.Text == "" || RetalhistasContacto.Text == "")
            {
                MessageBox.Show("Por favor preencha todos os campos!");
            }
            else
            {
                try
                {
                    string nome = RetalhistasNome.Text;
                    string morada = RetalhistasMorada.Text;
                    int contacto = int.Parse(RetalhistasContacto.Text);
                    AddRetalhista(nome, morada, contacto);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao adicionar retalhista: " + ex.Message);
                }
                finally
                {

                    dataRetalhista.Show();
                    empresaretalhistas.Show();
                    QuintaRetalhistaSelecao.Show();
                    DataRetalhistasEncomenda.Show();
                    FiltrarTransporteRetalhistas.Show();
                    QuintasRetalhistas.Show();
                    AdicionarEncomendasRetalhista.Show();
                    EliminarRetalhista.Show();
                    CancelarEncoemendRetalhistas.Show();
                    EncomendasRealizadas.Show();
                    ConfirmarRetalhista.Hide();
                    TipoDeEmpresaRetalhista.Show();
                    AdicionarRetalhistas.Show();


                    RetalhistasNome.ReadOnly = true;
                    RetalhistasMorada.ReadOnly = true;
                    RetalhistasContacto.ReadOnly = true;
                    RetalhistasTipo.ReadOnly = true;

                    RetalhistasNome.Text = "";
                    RetalhistasMorada.Text = "";
                    RetalhistasContacto.Text = "";



                    ListaRetalhistas.Items.Clear();
                    LoadRetalhistas();
                }
            }
        }

        private void AdicionarEncomendasRetalhista_Click(object sender, EventArgs e)
        {
            EncomendaListaProdutos.Show();
            PrazoEncomendaRetalhista.Show();
            PrazoBoxRetalhista.Show();
            MoradaRetalhista.Show();
            MoradaRetalhistaBox.Show();
            DataRetalhistaEncoemndabOX.Show();
            CompradorEncoemndaRetalhista.Show();
            EmpresaDeTransporteEncoemndaRetalhista.Show();
            EmpresaDeTransporteEncoemndaRetalhistaBox.Show();
            QuintaEncoemndaRetalhista.Show();
            QuintaEncoemndaRetalhistaBox.Show();
            ConfirmarRetalhistaEncoemnda.Show();
            RetalhistasTipo.Hide();
            RetalhistasNome.Hide();
            RetalhistasMorada.Hide();
            RetalhistasContacto.Hide();
            TipoDeEmpresaRetalhista.Hide();
            ItemsEncomenda.Hide();


            AdicionarRetalhistas.Hide();
            AdicionarEncomendasRetalhista.Hide();
            EliminarRetalhista.Hide();
            CancelarEncoemendRetalhistas.Hide();

            dataRetalhista.Hide();
            empresaretalhistas.Hide();
            QuintaRetalhistaSelecao.Hide();
            DataRetalhistasEncomenda.Hide();
            FiltrarTransporteRetalhistas.Hide();
            QuintasRetalhistas.Hide();
            AdicionarRetalhistas.Hide();
            AdicionarEncomendasRetalhista.Hide();
            EliminarRetalhista.Hide();
            CancelarEncoemendRetalhistas.Hide();
            EncomendasRealizadas.Hide();

            PrazoBoxRetalhista.Text = "";
            MoradaRetalhistaBox.Text = "";
            DataRetalhistaEncoemndabOX.Text = "";
            EmpresaDeTransporteEncoemndaRetalhistaBox.Text = "";
            QuintaEncoemndaRetalhistaBox.Text = "";



        }

        private void ConfirmarRetalhistaEncoemnda_Click(object sender, EventArgs e)
        {
            if (PrazoBoxRetalhista.Text == "" || MoradaRetalhistaBox.Text == "" || DataRetalhistaEncoemndabOX.Text == "" || EmpresaDeTransporteEncoemndaRetalhistaBox.Text == "" || QuintaEncoemndaRetalhistaBox.Text == "" || EncomendaListaProdutos.Rows.Count == 0)
            {
                MessageBox.Show("Por favor preencha todos os campos!");
            }
            else
            {
                try
                {
                    int prazo = int.Parse(PrazoBoxRetalhista.Text);
                    string morada = MoradaRetalhistaBox.Text;
                    RetalhistasOnlyName comprador = (RetalhistasOnlyName)DataRetalhistaEncoemndabOX.SelectedItem;
                    TransportesOnlyName empresaDeTransporte = (TransportesOnlyName)EmpresaDeTransporteEncoemndaRetalhistaBox.SelectedItem;
                    QuintaOnlyName quinta = (QuintaOnlyName)QuintaEncoemndaRetalhistaBox.SelectedItem;
                    if (comprador == null || empresaDeTransporte == null || quinta == null)
                    {
                        MessageBox.Show("Por favor selecione um comprador, uma empresa de transporte e uma quinta!");
                        return;
                    }

                    int compradorId = comprador.Empresa_Id_Empresa;
                    int empresaDeTransporteId = empresaDeTransporte.Empresa_Id_Empresa;
                    int quintaId = quinta.Id_Quinta;

                    AddEncomendaRetalhista(prazo, morada, compradorId, empresaDeTransporteId, quintaId);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao adicionar encomenda: " + ex.Message);
                }
                finally
                {
                    RetalhistasTipo.Show();
                    RetalhistasNome.Show();
                    RetalhistasMorada.Show();
                    RetalhistasContacto.Show();
                    TipoDeEmpresaRetalhista.Show();


                    AdicionarRetalhistas.Show();
                    AdicionarEncomendasRetalhista.Show();
                    EliminarRetalhista.Show();
                    CancelarEncoemendRetalhistas.Show();

                    dataRetalhista.Show();
                    empresaretalhistas.Show();
                    QuintaRetalhistaSelecao.Show();
                    DataRetalhistasEncomenda.Show();
                    FiltrarTransporteRetalhistas.Show();
                    QuintasRetalhistas.Show();
                    AdicionarRetalhistas.Show();
                    AdicionarEncomendasRetalhista.Show();
                    EliminarRetalhista.Show();
                    CancelarEncoemendRetalhistas.Show();
                    EncomendasRealizadas.Show();
                    ItemsEncomenda.Show();

                    EncomendaListaProdutos.Hide();
                    PrazoEncomendaRetalhista.Hide();
                    PrazoBoxRetalhista.Hide();
                    MoradaRetalhista.Hide();
                    MoradaRetalhistaBox.Hide();
                    DataRetalhistaEncoemndabOX.Hide();
                    CompradorEncoemndaRetalhista.Hide();
                    EmpresaDeTransporteEncoemndaRetalhista.Hide();
                    EmpresaDeTransporteEncoemndaRetalhistaBox.Hide();
                    QuintaEncoemndaRetalhista.Hide();
                    QuintaEncoemndaRetalhistaBox.Hide();
                    ConfirmarRetalhistaEncoemnda.Hide();

                    PrazoBoxRetalhista.Text = "";
                    MoradaRetalhistaBox.Text = "";
                    DataRetalhistaEncoemndabOX.Text = "";
                    EmpresaDeTransporteEncoemndaRetalhistaBox.Text = "";
                    QuintaEncoemndaRetalhistaBox.Text = "";

                    ListaRetalhistas.Items.Clear();
                    LoadRetalhistas();


                }
            }
        }

        //botão eliminar Empresa de transportes
        private void EliminarEmpresaTransportes_Click(object sender, EventArgs e)
        {
            sbyte index = (sbyte)ListaTransportes.SelectedIndex;
            if (index == -1)
            {
                MessageBox.Show("Por favor selecione uma empresa de transportes para remover!");
            }
            else
            {
                try
                {
                    RemoverTransporte((ListaTransportes.SelectedItem as Transportes).Empresa_Id_Empresa);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao remover transporte: " + ex.Message);
                }
                finally
                {
                    ListaTransportes.Items.Clear();
                    LoadTransportes();
                }
            }
        }

        //botão eliminar retalhista

        private void EliminarRetalhista_Click(object sender, EventArgs e)
        {
            sbyte index = (sbyte)ListaRetalhistas.SelectedIndex;
            if (index == -1)
            {
                MessageBox.Show("Por favor selecione um retalhista para remover!");
            }
            else
            {
                try
                {
                    RemoverRetalhista((ListaRetalhistas.SelectedItem as Retalhista).Empresa_Id_Empresa);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao remover retalhista: " + ex.Message);
                }
                finally
                {
                    ListaRetalhistas.Items.Clear();
                    LoadRetalhistas();
                }
            }
        }


        private void RemoverTransporte(int TransporteID)
        {
            using (SqlCommand command = new SqlCommand("ApagarTransporte", cn) { CommandType = CommandType.StoredProcedure })
            {
                command.Parameters.Add(new SqlParameter("@Empresa_Id_Empresa", TransporteID));
                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Empresa removida com sucesso!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to remove transport from database: " + ex.Message);
                }
            }
        }

        private void RemoverRetalhista(int RetalhistaID)
        {
            using (SqlCommand command = new SqlCommand("ApagarRetalhista", cn) { CommandType = CommandType.StoredProcedure })
            {
                command.Parameters.Add(new SqlParameter("@Empresa_Id_Empresa", RetalhistaID));
                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Retalhista removida com sucesso!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to remove product from database: " + ex.Message);
                }
            }
        }


        private void AddRetalhista(string nome, string morada, int contacto)
        {
            using (SqlCommand command = new SqlCommand("AgroTrack.AddRetalhista", cn) { CommandType = CommandType.StoredProcedure })
            {
                command.Parameters.Add(new SqlParameter("@Nome", nome));
                command.Parameters.Add(new SqlParameter("@Morada", morada));
                command.Parameters.Add(new SqlParameter("@Contacto", contacto));
                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Retalhista adicionado com sucesso!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to add Retalhista to database: " + ex.Message);
                }
            }
        }



        private void AdicionarPlanta_Click(object sender, EventArgs e)
        {
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
            QuintaNome.Hide();
            QuintaMorada.Hide();
            QuintaContacto.Hide();
            SubmeterNovaQuinta.Hide();
            AdicionarProdutoQuinta.Hide();
            AdicionarAnimalPlanta.Hide();
            RemoverProdutoQuinta.Hide();
            RemoverAnimalPlanta.Hide();
            ListaQuintas.Hide();
            label1.Hide();
            label3.Hide();
            label13.Hide();
            AdicionarQuinta.Hide();

            AddPlantaLote.Show();
            AddPlantaIDPlanta.Show();
            AddPlantaLoteLabel.Show();
            AddPlantaEstacao.Show();
            AddPlantaEstacaoLabel.Show();
            AddPlantaSubmeter.Show();
            AddAnimalQuinta.Show();
            AddAnimalQuintaLabel.Show();
            AddPlantaIDLabel.Show();
            AddPlantaIDPlanta.Show();

        }

        private void AddPlantaSubmeter_Click(object sender, EventArgs e)
        {
            if (AddPlantaIDPlanta.SelectedIndex == -1 || AddPlantaLote.Text == "" || AddPlantaEstacao.Text == "" || AddAnimalQuinta.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor preencha todos os campos!");
            }
            else
            {
                try
                {
                    int plantaId = (AddPlantaIDPlanta.SelectedItem as Planta).Id;
                    string lote = AddPlantaLote.Text;
                    string estacao = AddPlantaEstacao.Text;
                    int quintaId = (AddAnimalQuinta.SelectedItem as Quinta).Id_Quinta;
                    AddPlantaToQuinta(quintaId, plantaId, lote, estacao);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao adicionar planta à quinta: " + ex.Message);
                }
                finally
                {
                    AddAnimalID.Hide();
                    AddPlantaLote.Hide();
                    AddPlantaLoteLabel.Hide();
                    AddPlantaEstacao.Hide();
                    AddPlantaEstacaoLabel.Hide();
                    AddPlantaSubmeter.Hide();
                    AddAnimalQuinta.Hide();
                    AddAnimalQuintaLabel.Hide();
                    AddPlantaIDLabel.Hide();
                    AddPlantaIDPlanta.Hide();
                    label48.Show();
                    label49.Show();
                    label41.Show();
                    label47.Show();
                    label4.Show();
                    PesquisarQuinta.Show();
                    PesquisaPorNomeCliente.Show();
                    Agricultores.Show();
                    ProdutosQuinta.Show();
                    Plantas.Show();
                    button20.Show();
                    RemoverQuinta.Show();
                    Animais.Show();
                    QuintaNome.Show();
                    QuintaMorada.Show();
                    QuintaContacto.Show();
                    AdicionarProdutoQuinta.Show();
                    AdicionarAnimalPlanta.Show();
                    RemoverProdutoQuinta.Show();
                    RemoverAnimalPlanta.Show();
                    ListaQuintas.Show();
                    label1.Show();
                    label3.Show();
                    label13.Show();
                    FilterByAnimalQuinta.Show();
                    FilterByPlantQuinta.Show();
                    FiltrarPorProdutoQuinta.Show();
                    QuantidadeAgricultores.Show();
                    AdicionarQuinta.Show();
                    ListaQuintas.Items.Clear();
                    LoadQuinta();
                    LoadFiltersQuinta();
                }
            }
        }

        private void AddPlantaToQuinta(int quintaId, int plantaId, string lote, string estacao)
        {
            using (SqlCommand command = new SqlCommand("AddPlantaToQuinta", cn) { CommandType = CommandType.StoredProcedure })
            {
                command.Parameters.Add(new SqlParameter("@QuintaId", quintaId));
                command.Parameters.Add(new SqlParameter("@IdPlanta", plantaId));
                command.Parameters.Add(new SqlParameter("@Lote", lote));
                command.Parameters.Add(new SqlParameter("@Estacao", estacao));
                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Planta adicionada à quinta com sucesso!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to add planta to quinta: " + ex.Message);
                }
            }
        }

        private void RemoverAnimalPlanta_Click(object sender, EventArgs e)
        {
            if (Animais.SelectedItem is Animal selectedAnimal)
            {
                try
                {
                    RemoveAnimalFromQuinta((ListaQuintas.SelectedItem as Quinta).Id_Quinta, int.Parse(selectedAnimal.Id), selectedAnimal.Brinco);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao remover animal da quinta: " + ex.Message);
                }
                finally
                {
                    Animais.Items.Clear();
                    LoadAnimals((ListaQuintas.SelectedItem as Quinta).Id_Quinta);
                }
            }
            else if (Plantas.SelectedItem is Planta selectedPlanta)
            {
                try
                {
                    RemovePlantaFromQuinta((ListaQuintas.SelectedItem as Quinta).Id_Quinta, selectedPlanta.Id, selectedPlanta.Lote);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao remover planta da quinta: " + ex.Message);
                }
                finally
                {
                    Plantas.Items.Clear();
                    LoadPlantas((ListaQuintas.SelectedItem as Quinta).Id_Quinta);
                }
            }
            else
            {
                MessageBox.Show("Por favor selecione um animal ou planta para remover!");
            }
        }

        private void RemoveAnimalFromQuinta(int quintaId, int animalId, string brinco)
        {
            using (SqlCommand command = new SqlCommand("RemoveAnimalFromQuinta", cn) { CommandType = CommandType.StoredProcedure })
            {
                command.Parameters.Add(new SqlParameter("@QuintaId", quintaId));
                command.Parameters.Add(new SqlParameter("@AnimalId", animalId));
                command.Parameters.Add(new SqlParameter("@Brinco", brinco));
                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Animal removido da quinta com sucesso!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to remove animal from quinta: " + ex.Message);
                }
            }
        }

        private void RemovePlantaFromQuinta(int quintaId, int plantaId, string lote)
        {
            using (SqlCommand command = new SqlCommand("RemovePlantFromQuinta", cn) { CommandType = CommandType.StoredProcedure })
            {
                command.Parameters.Add(new SqlParameter("@QuintaId", quintaId));
                command.Parameters.Add(new SqlParameter("@PlantaId", plantaId));
                command.Parameters.Add(new SqlParameter("@Lote", lote));
                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Planta removida da quinta com sucesso!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to remove planta from quinta: " + ex.Message);
                }
            }
        }

        private void QuantidadeVendidaBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void EntregaBox_TextChanged(object sender, EventArgs e)
        {

        }

        //botão adicionar encomenda

        private void AddEncomendaTransporte(int prazo, string morada, DateTime entrega, int retalhista, int transportes, int quinta)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("AgroTrack.AddEncomendaTransportes", cn) { CommandType = CommandType.StoredProcedure })
                {
                    // Adiciona os parâmetros ao comandoº

                    command.Parameters.Add(new SqlParameter("@Prazo_entrega", prazo));
                    command.Parameters.Add(new SqlParameter("@Morada_entrega", morada));
                    command.Parameters.Add(new SqlParameter("@Entrega", entrega));
                    command.Parameters.Add(new SqlParameter("@Retalhista_Empresa_Id_Empresa", retalhista));
                    command.Parameters.Add(new SqlParameter("@Empresa_De_Transportes_Id_Empresa", transportes));
                    command.Parameters.Add(new SqlParameter("@Quinta_Empresa_Id", quinta));

                    // Executa o comando
                    command.ExecuteNonQuery();

                    // Exibe mensagem de sucesso
                    MessageBox.Show("Encomenda adicionada com sucesso!");
                }
            }
            catch (Exception ex)
            {
                // Fecha a conexão se estiver aberta
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }

                // Lança a exceção
                throw new Exception("Falha ao adicionar a encomenda: " + ex.Message);
            }
        }

        private void LoadTabelaProdutos(int empresaID)
        {
            string query = "SELECT Codigo, NomeProduto, Empresa_Id_Empresa, Unidade_medida, Quantidade FROM AgroTrack.QuintaProduto WHERE Empresa_Id_Empresa = @Empresa_Id_Empresa";
            List<ProdutosOnlyNameMedida> produtos = new List<ProdutosOnlyNameMedida>();

            using (SqlCommand command = new SqlCommand(query, cn))
            {
                command.Parameters.Add(new SqlParameter("@Empresa_Id_Empresa", empresaID));

                if (cn.State != ConnectionState.Open)
                {
                    cn.Open();
                }

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ProdutosOnlyNameMedida produto = new ProdutosOnlyNameMedida(reader.GetInt32(0), reader.GetString(1), reader.GetString(3), reader.GetInt32(4));
                        produtos.Add(produto);
                    }
                }
            }

            if (produtos.Count > 0)
            {
                DataGridViewComboBoxColumn productColumn = (DataGridViewComboBoxColumn)EncomendaListaProdutos.Columns["productColumn"];
                productColumn.Items.Clear();

                // Add items to the combo box column
                foreach (var produto in produtos)
                {
                    productColumn.Items.Add(produto.ToString()); // Convert ToString() result to string explicitly
                }

                productColumn.ValueMember = "Id_Produto";
                productColumn.DisplayMember = ""; // Leave empty to use ToString() method
            }
            else
            {
                MessageBox.Show("No products found for the given Empresa ID.");
            }
        }


        private void EncomendaListaProdutos_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            // Log the error or display a message
            MessageBox.Show("Error occurred in DataGridView: " + e.Exception.Message, "Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            e.ThrowException = false; // Prevent the default error dialog
        }

        // Subscribe to the DataError event
        private void SaveProductsToDatabase(int encomendaId)
        {
            foreach (DataGridViewRow row in EncomendaListaProdutos.Rows)
            {
                if (row.Cells["productColumn"].Value != null && row.Cells["quantityColumn"].Value != null)
                {
                    string selectedProductString = row.Cells["productColumn"].Value.ToString(); // Get the selected string
                    int productId = GetProductIdFromComboBoxString(selectedProductString); // Extract the Id_Produto
                    int quantidade = int.Parse(row.Cells["quantityColumn"].Value.ToString());
                    AddProductToEncomenda(encomendaId, productId, quantidade);
                }
            }
        }

        private int GetProductIdFromComboBoxString(string selectedProductString)
        {
            // Assuming the selectedProductString format is "Id_Produto - Nome (Unidade_medida)"
            string[] parts = selectedProductString.Split('-');
            if (parts.Length > 0)
            {
                return int.Parse(parts[0].Trim()); // Extract and parse the Id_Produto
            }
            else
            {
                throw new ArgumentException("Invalid format for selected product string.");
            }
        }


        private void AddProductToEncomenda(int encomendaId, int productId, int quantidade)
        {
            using (SqlCommand command = new SqlCommand("AgroTrack.AddProductToEncomenda", cn) { CommandType = CommandType.StoredProcedure })
            {
                command.Parameters.Add(new SqlParameter("@Produto_codigo", productId));
                command.Parameters.Add(new SqlParameter("@Quantidade", quantidade));
                command.Parameters.Add(new SqlParameter("@Codigo", encomendaId));
                try
                {
                    if (cn.State != ConnectionState.Open)
                    {
                        cn.Open();
                    }
                    command.ExecuteNonQuery();
                    MessageBox.Show("Produto adicionado à encomenda com sucesso!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to add product to encomenda: " + ex.Message);
                }
            }
        }

        private void AddEncomendaRetalhista(int prazo, string morada, int retalhista, int transportes, int quinta)
        {
            try
            {
                using (SqlCommand command = new SqlCommand("AgroTrack.AddEncomendaRetalhistas", cn) { CommandType = CommandType.StoredProcedure })
                {
                    // Adiciona os parâmetros ao comando
                    command.Parameters.Add(new SqlParameter("@Prazo_entrega", prazo));
                    command.Parameters.Add(new SqlParameter("@Morada_entrega", morada));
                    command.Parameters.Add(new SqlParameter("@Retalhista_Empresa_Id_Empresa", retalhista));
                    command.Parameters.Add(new SqlParameter("@Empresa_De_Transportes_Id_Empresa", transportes));
                    command.Parameters.Add(new SqlParameter("@Quinta_Empresa_Id", quinta));

                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    command.ExecuteNonQuery();

                    int idEncomenda = getEncomendaID();
                    SaveProductsToDatabase(idEncomenda);
                    MessageBox.Show("Encomenda adicionada com sucesso!");
                }
            }
            catch (Exception ex)
            {
                // Fecha a conexão se estiver aberta
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }

                // Lança a exceção
                throw new Exception("Falha ao adicionar a encomenda: " + ex.Message);
            }
        }

        private int getEncomendaID()
        {
            using (SqlCommand command = new SqlCommand("SELECT MAX(Codigo) FROM AgroTrack.Encomenda", cn))
            {
                if (cn.State == ConnectionState.Closed)
                {
                    cn.Open();
                }

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return reader.GetInt32(0);
                    }
                    else
                    {
                        throw new Exception("Failed to get encomenda ID.");
                    }
                }
            }
        }

        //eliminar encomenda trnsportes
        private void CancelarEncoemendRetalhistas_Click(object sender, EventArgs e)
        {
            sbyte index = (sbyte)EncomendasRealizadas.SelectedIndex;
            if (index == -1)
            {

                MessageBox.Show("Por favor selecione um Encomenda para remover!");
            }
            else
            {
                try
                {
                    RemoverEncomendaRetalhista((EncomendasRealizadas.SelectedItem as Encomenda).Codigo);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao remover Encomenda: " + ex.Message);
                }
                finally
                {
                    LoadEncomendasRealizadas(EMPRESAID, DataLimiteEncomendas);
                }
            }
        }


        private void RemoverEncomendaRetalhista(int EncomendaID)
        {
            using (SqlCommand command = new SqlCommand("ApagarEncomendaRetalhista", cn) { CommandType = CommandType.StoredProcedure })
            {
                command.Parameters.Add(new SqlParameter("@Codigo", EncomendaID));
                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Encomenda removida com sucesso!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to remove Encomenda from database: " + ex.Message);
                }
            }
        }

        private void RemoverEncomendaTransportes(int EncomendaID)
        {
            using (SqlCommand command = new SqlCommand("ApagarEncomendaTransportes", cn) { CommandType = CommandType.StoredProcedure })
            {
                command.Parameters.Add(new SqlParameter("@Codigo", EncomendaID));
                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Encomenda removida com sucesso!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to remove Encomenda from database: " + ex.Message);
                }
            }
        }

        private void CancelarEncoemndaTransportes_Click_1(object sender, EventArgs e)
        {
            sbyte index = (sbyte)EncomendasEntrega.SelectedIndex;
            if (index == -1)
            {
                MessageBox.Show("Por favor selecione uma Encomenda para remover!");
            }
            else
            {
                try
                {
                    RemoverEncomendaTransportes((EncomendasEntrega.SelectedItem as Encomenda).Codigo);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao remover Encomenda: " + ex.Message);
                }
                finally
                {
                    LoadEncomendasEntrega(EMPRESAID, DataLimiteEncomendas);
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void QuintaEncoemndaRetalhistaBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            EncomendaListaProdutos.Rows.Clear();
            LoadTabelaProdutos((QuintaEncoemndaRetalhistaBox.SelectedItem as QuintaOnlyName).Id_Quinta);
        }

        private void EncomendasRealizadas_SelectedIndexChanged(object sender, EventArgs e)
        {
            Encomenda encomenda = EncomendasRealizadas.SelectedItem as Encomenda;
            LoadEncomendaItemsRetalhistas(encomenda.Codigo);

        }
        private void LoadEncomendaItemsTransportes(int encomendaId)
        {
            string query = "SELECT Encomenda_Codigo, ProdutoCodigo, Quantidade, NomeProduto FROM AgroTrack.EncomendaItems WHERE Encomenda_Codigo = @Codigo";
            SqlCommand command = new SqlCommand(query, cn);
            command.Parameters.Add(new SqlParameter("@Codigo", encomendaId));
            ItemsEncomenda.Items.Clear();
            ItemsEncomendaTransportes.Items.Clear();

            try
            {
                if (cn.State != ConnectionState.Open)
                {
                    cn.Open();
                }

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Item item = new Item
                        {
                            EncomendaCodigo = reader.GetInt32(0),
                            ProdutoId = reader.GetInt32(1),
                            Quantidade = reader.GetInt32(2),
                            ProdutoNome = reader.GetString(3)
                        };
                        ItemsEncomendaTransportes.Items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load encomenda items: " + ex.Message);
            }
        }

        private void LoadEncomendaItemsRetalhistas(int encomendaId)
        {
            string query = "SELECT Encomenda_Codigo, ProdutoCodigo, Quantidade, NomeProduto FROM AgroTrack.EncomendaItems WHERE Encomenda_Codigo = @Codigo";
            SqlCommand command = new SqlCommand(query, cn);
            command.Parameters.Add(new SqlParameter("@Codigo", encomendaId));
            ItemsEncomenda.Items.Clear();
            ItemsEncomendaTransportes.Items.Clear();

            try
            {
                if (cn.State != ConnectionState.Open)
                {
                    cn.Open();
                }

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Item item = new Item
                        {
                            EncomendaCodigo = reader.GetInt32(0),
                            ProdutoId = reader.GetInt32(1),
                            Quantidade = reader.GetInt32(2),
                            ProdutoNome = reader.GetString(3)
                        };
                        ItemsEncomenda.Items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load encomenda items: " + ex.Message);
            }
        }


        private void EncomendasEntrega_SelectedIndexChanged(object sender, EventArgs e)
        {
            Encomenda encomenda = EncomendasEntrega.SelectedItem as Encomenda;
            AlterarDataEncomenda.Show();
            DataEntregaInicio.Text = "";
            if (EncomendasEntrega.SelectedItem != null)
            {
                DataDeEntregaAtualBOX.Text = (EncomendasEntrega.SelectedItem as Encomenda).Entrega.ToString();
                DataEntregaInicio.Text = (EncomendasEntrega.SelectedItem as Encomenda).Entrega.ToString();

            }
            LoadEncomendaItemsTransportes(encomenda.Codigo);
        }

        private void AddCompraProduto_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadFiltersClientes();
        }

        private void UnidadeAdicionarBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void TipoAdicionarBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void QuintasProdutos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ProdutoTipo_TextChanged(object sender, EventArgs e)
        {

        }

        private void AlterarQuantidade()
        {

        }

        private void AlterarQuantidadeQuinta_Click(object sender, EventArgs e)
        {
            if (ProdutosQuinta.SelectedItem != null)
            {
                AlterarNumero.Visible = true;
                Confirmar.Visible = true;

                AlterarNumero.Value = ((ProdutosQuinta)ProdutosQuinta.SelectedItem).Quantidade;
            }
        }

        // O método Confirmar_Click é chamado quando o botão Confirmar é clicado
        private void Confirmar_Click(object sender, EventArgs e)
        {
            if (ProdutosQuinta.SelectedItem != null)
            {
                ProdutosQuinta produtoSelecionado = (ProdutosQuinta)ProdutosQuinta.SelectedItem;

                int novaQuantidade = (int)AlterarNumero.Value;

                AtualizarQuantidadeNoBancoDeDados(produtoSelecionado.Id_Produto, novaQuantidade);


                produtoSelecionado.Quantidade = novaQuantidade;

                AlterarNumero.Visible = false;
                Confirmar.Visible = false;
            }
            LoadQuinta();
        }

        private void AtualizarQuantidadeNoBancoDeDados(int idProduto, int novaQuantidade)
        {
            string query = "UPDATE AgroTrack.QuintaProduto SET Quantidade = @NovaQuantidade WHERE Codigo = @IdProduto";
            // Abre uma nova conexão usando o bloco using para garantir que será fechada automaticamente
            using (SqlCommand command = new SqlCommand(query, cn))
            {
                // Adiciona os parâmetros ao comando
                try
                {
                    command.Parameters.Add(new SqlParameter("@NovaQuantidade", novaQuantidade));
                    command.Parameters.Add(new SqlParameter("@IdProduto", idProduto));

                    // Abre a conexão se estiver fechada
                    if (cn.State == ConnectionState.Closed)
                    {
                        cn.Open();
                    }

                    // Executa o comando
                    command.ExecuteNonQuery();
                    if (ListaQuintas_SelectedIndexChanged == null)
                    {
                        MessageBox.Show("Quantidade do produto atualizada com sucesso! Selecione a Quinta para ver a mudança");
                    }
                    ListaQuintas_SelectedIndexChanged(null, null);
                }
                catch (Exception ex)
                {
                    // Fecha a conexão se estiver aberta
                    if (cn.State == ConnectionState.Open)
                    {
                        cn.Close();
                    }
                }
            }
        }

        private void AlterarDataEncomenda_Click(object sender, EventArgs e)
        {
            if (EncomendasEntrega.SelectedItem != null)
            {
                TransportesIdEmpresa.Hide();
                TransportesTipo.Hide();
                FiltrarPorDataTransportes.Hide();
                EmpresaRetalhista.Hide();
                OrigemTransportes.Hide();
                DataEncomendaTransportes.Hide();
                DataEntregaInicial.Hide();
                DataEntregaInicio.Hide();
                FiltrarRetalhistaTransportes.Hide();
                QuintasTransportes.Hide();
                AdicionarEmpresa.Hide();
                EliminarEmpresaTransportes.Hide();
                AlterarDataEncomenda.Show();
                ConfirmarData.Show();
                DataDeEntregaAtual.Show();
                DataDeEntregaAtualBOX.Show();
                NovaDataDeEntrega.Show();
                NovaDataDeEntregaBOX.Show();


                DataDeEntregaAtualBOX.ReadOnly = true;

            }
        }

        private void ItemsEncomendaTransportes_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void EncomendaListaProdutos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ConfirmarData_Click(object sender, EventArgs e)
        {
            if (EncomendasEntrega.SelectedItem != null)
            {
                Encomenda encomendaSelecionada = (Encomenda)EncomendasEntrega.SelectedItem;

                DateTime novaDataEntrega = NovaDataDeEntregaBOX.Value;

                AtualizarDataEntregaNoBancoDeDados(encomendaSelecionada.Codigo, novaDataEntrega);

                LoadEncomendasEntrega(EMPRESAENTREGAID, DateTime.Now);
            }
        }

        private void NovaDataDeEntregaBOX_ValueChanged(object sender, EventArgs e)
        {

        }


        private void AtualizarDataEntregaNoBancoDeDados(int codigoEncomenda, DateTime novaDataEntrega)
        {
            {
                string query = "UPDATE AgroTrack.EmpresaEncomenda SET Entrega = @NovaDataEntrega WHERE Codigo = @CodigoEncomenda";
                // Abre uma nova conexão usando o bloco using para garantir que será fechada automaticamente
                using (SqlCommand command = new SqlCommand(query, cn))
                {
                    // Adiciona os parâmetros ao comando
                    try
                    {
                        command.Parameters.AddWithValue("@NovaDataEntrega", novaDataEntrega);
                        command.Parameters.AddWithValue("@CodigoEncomenda", codigoEncomenda);

                        // Abre a conexão se estiver fechada
                        if (cn.State == ConnectionState.Closed)
                        {
                            cn.Open();
                        }

                        // Executa o comando
                        command.ExecuteNonQuery();
                        if (ListaQuintas_SelectedIndexChanged == null)
                        {
                            MessageBox.Show("Data atualizada com sucesso! Selecione a Quinta para ver a mudança");
                        }
                        command.ExecuteNonQuery();
                        MessageBox.Show("Data atualizada com sucesso!");
                        NomeTransportes.Show();
                        TransportesIdEmpresa.Show();
                        TransportesTipo.Show();
                        EncomendasEntrega.Show();
                        FiltrarPorDataTransportes.Show();
                        EmpresaRetalhista.Show();
                        OrigemTransportes.Show();
                        DataEncomendaTransportes.Show();
                        FiltrarRetalhistaTransportes.Show();
                        QuintasTransportes.Show();
                        AdicionarEmpresa.Show();
                        EliminarEmpresaTransportes.Show();
                        MoradaTransportes.Show();
                        ContactoTransportes.Show();
                        TransportesNome.Show();
                        TransportesMorada.Show();
                        TransportesContacto.Show();
                        ConfirmarData.Hide();
                        DataDeEntregaAtual.Hide();
                        DataDeEntregaAtualBOX.Hide();
                        NovaDataDeEntrega.Hide();
                        NovaDataDeEntregaBOX.Hide();
                        DataEntregaInicial.Show();
                        DataEntregaInicio.Show();


                        ListaTransportes.Items.Clear();
                        LoadTransportes();
                        ItemsEncomendaTransportes_SelectedIndexChanged(null, null);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Failed to update data: " + ex.Message);
                        if (cn.State == ConnectionState.Open)
                        {
                            cn.Close();
                        }

                    }
                }


            }
        }

        private void LoadEncomendasRealizadas(int empresaId)
        {
            int? empresaDeTransportesId = null;
            int? quintaId = null;
            EMPRESAID = empresaId;

            // Obtenha os valores selecionados nos filtros
            if (FiltrarTransporteRetalhistas.SelectedItem is TransportesOnlyName selectedTransport)
            {
                empresaDeTransportesId = selectedTransport.Empresa_Id_Empresa;
            }

            if (QuintasRetalhistas.SelectedItem is QuintaOnlyName selectedQuinta)
            {
                quintaId = selectedQuinta.Id_Quinta;
            }

            string query = @"SELECT DISTINCT Nome, Morada, Contacto, Codigo, prazo_entrega, Morada_entrega, Entrega, Retalhista_Empresa_Id_Empresa, Empresa_De_Transportes_Id_Empresa, Quinta_Empresa_Id, PrecoTotal
                     FROM AgroTrack.EmpresaEncomenda 
                     WHERE Retalhista_Empresa_Id_Empresa = @Empresa_Id_Empresa";

            if (empresaDeTransportesId.HasValue)
            {
                query += " AND Empresa_De_Transportes_Id_Empresa = @EmpresaDeTransportesId";
            }

            if (quintaId.HasValue)
            {
                query += " AND Quinta_Empresa_Id = @QuintaId";
            }

            SqlCommand cmd = new SqlCommand(query, cn);
            cmd.Parameters.AddWithValue("@Empresa_Id_Empresa", empresaId);

            if (empresaDeTransportesId.HasValue)
            {
                cmd.Parameters.AddWithValue("@EmpresaDeTransportesId", empresaDeTransportesId.Value);
            }

            if (quintaId.HasValue)
            {
                cmd.Parameters.AddWithValue("@QuintaId", quintaId.Value);
            }

            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                EncomendasRealizadas.Items.Clear(); // Clear previous items

                while (reader.Read())
                {
                    Encomenda order = new Encomenda
                    {
                        Codigo = (int)reader["Codigo"],
                        PrazoEntrega = (int)reader["prazo_entrega"],
                        MoradaEntrega = reader["Morada_entrega"].ToString(),
                        Entrega = DateTime.Parse(reader["Entrega"].ToString()),
                        RetalhistaEmpresaId = (int)reader["Retalhista_Empresa_Id_Empresa"],
                        EmpresaDeTransportesId = (int)reader["Empresa_De_Transportes_Id_Empresa"],
                        QuintaEmpresaId = (int)reader["Quinta_Empresa_Id"],
                        Preco = reader["PrecoTotal"] != DBNull.Value ? (double)reader["PrecoTotal"] : 0.0
                    };

                    EncomendasRealizadas.Items.Add(order);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to retrieve data from database: " + ex.Message);
            }
        }

        private int EMPRESAENTREGAID2;

        private void LoadEncomendasEntrega(int empresaId)
        {
            int? retalhistaId = null;
            int? quintaId = null;
            EMPRESAENTREGAID2 = empresaId;

            // Obtenha o valor selecionado no filtro de retalhistas
            if (FiltrarRetalhistaTransportes.SelectedItem is RetalhistasOnlyName selectedRetalhista)
            {
                retalhistaId = selectedRetalhista.Empresa_Id_Empresa;
            }

            // Obtenha o valor selecionado no filtro de quintas
            if (QuintasTransportes.SelectedItem is QuintaOnlyName selectedQuinta)
            {
                quintaId = selectedQuinta.Id_Quinta;
            }

            string query = @"SELECT DISTINCT Nome, Morada, Contacto, Codigo, prazo_entrega, Morada_entrega, Entrega, Retalhista_Empresa_Id_Empresa, Empresa_De_Transportes_Id_Empresa, Quinta_Empresa_Id 
                     FROM AgroTrack.EmpresaEncomenda 
                     WHERE Empresa_De_Transportes_Id_Empresa = @Empresa_Id_Empresa";

            if (retalhistaId.HasValue)
            {
                query += " AND Retalhista_Empresa_Id_Empresa = @RetalhistaId";
            }

            if (quintaId.HasValue)
            {
                query += " AND Quinta_Empresa_Id = @QuintaId";
            }

            SqlCommand cmd = new SqlCommand(query, cn);
            cmd.Parameters.AddWithValue("@Empresa_Id_Empresa", empresaId);

            if (retalhistaId.HasValue)
            {
                cmd.Parameters.AddWithValue("@RetalhistaId", retalhistaId.Value);
            }

            if (quintaId.HasValue)
            {
                cmd.Parameters.AddWithValue("@QuintaId", quintaId.Value);
            }

            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                EncomendasEntrega.Items.Clear(); // Clear previous items
                while (reader.Read())
                {
                    Encomenda order = new Encomenda
                    {
                        Codigo = (int)reader["Codigo"],
                        PrazoEntrega = (int)reader["prazo_entrega"],
                        MoradaEntrega = reader["Morada_entrega"].ToString(),
                        Entrega = DateTime.Parse(reader["Entrega"].ToString()),
                        RetalhistaEmpresaId = (int)reader["Retalhista_Empresa_Id_Empresa"],
                        EmpresaDeTransportesId = (int)reader["Empresa_De_Transportes_Id_Empresa"],
                        QuintaEmpresaId = (int)reader["Quinta_Empresa_Id"]
                    };

                    EncomendasEntrega.Items.Add(order);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to retrieve data from database: " + ex.Message);
            }
        }


        private void DataRetalhistasEncomenda_ValueChanged(object sender, EventArgs e)
        {
            DateTime dataSelecionada = DataRetalhistasEncomenda.Value.Date;
            DateTime dataAtual = DateTime.Now.Date;

            if (dataSelecionada > dataAtual)
            {
                if (ListaRetalhistas.SelectedItem is Retalhista selectedretalho)
                {
                    LoadEncomendasRealizadas(selectedretalho.Empresa_Id_Empresa, dataSelecionada);
                }
            }

        }

        private void PrazoEncomendaRetalhista_Click(object sender, EventArgs e)
        {

        }
    }
}