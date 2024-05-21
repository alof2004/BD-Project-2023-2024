using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Contracts;
using System.Windows.Forms;

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

        private void loadAgricultores(int empresaId)
        {
            string query = "SELECT Id_Trabalhador, Pessoa_N_CartaoCidadao, Quinta_Empresa_Id_Empresa, Codigo_quinta, Empresa_Id_Empresa FROM AgroTrack.AgriculQuinta WHERE Codigo_quinta = @CodigoQuinta";
            SqlCommand cmd = new SqlCommand(query, cn);
            cmd.Parameters.AddWithValue("@EmpresaId", empresaId);

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
    }
}