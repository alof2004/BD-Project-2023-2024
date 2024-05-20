namespace AgroTrack
{
    partial class AgroTrack
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tabControl1 = new TabControl();
            Quintas = new TabPage();
            buttonPesquisarQuinta = new Button();
            buttonLimparPesquisaQuinta = new Button();
            label4 = new Label();
            label3 = new Label();
            label1 = new Label();
            QuintaMorada = new TextBox();
            QuintaNome = new TextBox();
            PesquisarQuinta = new TextBox();
            Plantas = new ListBox();
            Animais = new ListBox();
            ListaQuintas = new ListBox();
            Empresas = new TabPage();
            button1 = new Button();
            button2 = new Button();
            label5 = new Label();
            textBox2 = new TextBox();
            ListaEmpresas = new ListBox();
            tabPage1 = new TabPage();
            tabPage2 = new TabPage();
            textBox1 = new TextBox();
            textBox3 = new TextBox();
            textBox4 = new TextBox();
            label2 = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            textBox5 = new TextBox();
            Encomenda = new ListBox();
            label9 = new Label();
            dateTimePicker1 = new DateTimePicker();
            label10 = new Label();
            label11 = new Label();
            comboBox1 = new ComboBox();
            comboBox2 = new ComboBox();
            label12 = new Label();
            textBox6 = new TextBox();
            button3 = new Button();
            tabControl1.SuspendLayout();
            Quintas.SuspendLayout();
            Empresas.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(Quintas);
            tabControl1.Controls.Add(Empresas);
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1040, 609);
            tabControl1.TabIndex = 0;
            tabControl1.SelectedIndexChanged += tabControl1_SelectedIndexChanged;
            // 
            // Quintas
            // 
            Quintas.Controls.Add(buttonPesquisarQuinta);
            Quintas.Controls.Add(buttonLimparPesquisaQuinta);
            Quintas.Controls.Add(label4);
            Quintas.Controls.Add(label3);
            Quintas.Controls.Add(label1);
            Quintas.Controls.Add(QuintaMorada);
            Quintas.Controls.Add(QuintaNome);
            Quintas.Controls.Add(PesquisarQuinta);
            Quintas.Controls.Add(Plantas);
            Quintas.Controls.Add(Animais);
            Quintas.Controls.Add(ListaQuintas);
            Quintas.Location = new Point(4, 34);
            Quintas.Name = "Quintas";
            Quintas.Padding = new Padding(3);
            Quintas.Size = new Size(1032, 571);
            Quintas.TabIndex = 0;
            Quintas.Text = "Quintas";
            Quintas.UseVisualStyleBackColor = true;
            Quintas.Click += Quintas_Click;
            // 
            // buttonPesquisarQuinta
            // 
            buttonPesquisarQuinta.Location = new Point(20, 82);
            buttonPesquisarQuinta.Name = "buttonPesquisarQuinta";
            buttonPesquisarQuinta.Size = new Size(177, 34);
            buttonPesquisarQuinta.TabIndex = 12;
            buttonPesquisarQuinta.Text = "Pesquisar";
            buttonPesquisarQuinta.UseVisualStyleBackColor = true;
            buttonPesquisarQuinta.Click += buttonPesquisarQuinta_Click;
            // 
            // buttonLimparPesquisaQuinta
            // 
            buttonLimparPesquisaQuinta.Location = new Point(203, 82);
            buttonLimparPesquisaQuinta.Name = "buttonLimparPesquisaQuinta";
            buttonLimparPesquisaQuinta.Size = new Size(176, 34);
            buttonLimparPesquisaQuinta.TabIndex = 11;
            buttonLimparPesquisaQuinta.Text = "Limpar";
            buttonLimparPesquisaQuinta.UseVisualStyleBackColor = true;
            buttonLimparPesquisaQuinta.Click += buttonLimparPesquisaQuinta_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(20, 17);
            label4.Name = "label4";
            label4.Size = new Size(177, 25);
            label4.TabIndex = 10;
            label4.Text = "Pesquisar por Nome:";
            label4.Click += label4_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(452, 195);
            label3.Name = "label3";
            label3.Size = new Size(74, 25);
            label3.TabIndex = 8;
            label3.Text = "Morada";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(463, 122);
            label1.Name = "label1";
            label1.Size = new Size(61, 25);
            label1.TabIndex = 6;
            label1.Text = "Nome";
            label1.Click += label1_Click;
            // 
            // QuintaMorada
            // 
            QuintaMorada.Location = new Point(551, 192);
            QuintaMorada.Name = "QuintaMorada";
            QuintaMorada.Size = new Size(383, 31);
            QuintaMorada.TabIndex = 5;
            // 
            // QuintaNome
            // 
            QuintaNome.Location = new Point(551, 122);
            QuintaNome.Name = "QuintaNome";
            QuintaNome.Size = new Size(383, 31);
            QuintaNome.TabIndex = 4;
            QuintaNome.TextChanged += textBox1_TextChanged_1;
            // 
            // PesquisarQuinta
            // 
            PesquisarQuinta.Location = new Point(20, 45);
            PesquisarQuinta.Name = "PesquisarQuinta";
            PesquisarQuinta.Size = new Size(359, 31);
            PesquisarQuinta.TabIndex = 3;
            PesquisarQuinta.TextChanged += textBox1_TextChanged;
            // 
            // Plantas
            // 
            Plantas.FormattingEnabled = true;
            Plantas.ItemHeight = 25;
            Plantas.Location = new Point(738, 247);
            Plantas.Name = "Plantas";
            Plantas.Size = new Size(242, 204);
            Plantas.TabIndex = 2;
            Plantas.SelectedIndexChanged += listBox1_SelectedIndexChanged_2;
            // 
            // Animais
            // 
            Animais.FormattingEnabled = true;
            Animais.ItemHeight = 25;
            Animais.Location = new Point(438, 247);
            Animais.Name = "Animais";
            Animais.Size = new Size(242, 204);
            Animais.TabIndex = 1;
            Animais.SelectedIndexChanged += listBox1_SelectedIndexChanged_1;
            // 
            // ListaQuintas
            // 
            ListaQuintas.FormattingEnabled = true;
            ListaQuintas.ItemHeight = 25;
            ListaQuintas.Location = new Point(20, 122);
            ListaQuintas.Name = "ListaQuintas";
            ListaQuintas.Size = new Size(359, 429);
            ListaQuintas.TabIndex = 0;
            ListaQuintas.SelectedIndexChanged += ListaQuintas_SelectedIndexChanged;
            // 
            // Empresas
            // 
            Empresas.Controls.Add(button3);
            Empresas.Controls.Add(textBox6);
            Empresas.Controls.Add(label12);
            Empresas.Controls.Add(comboBox2);
            Empresas.Controls.Add(comboBox1);
            Empresas.Controls.Add(label11);
            Empresas.Controls.Add(label10);
            Empresas.Controls.Add(dateTimePicker1);
            Empresas.Controls.Add(label9);
            Empresas.Controls.Add(Encomenda);
            Empresas.Controls.Add(textBox5);
            Empresas.Controls.Add(label8);
            Empresas.Controls.Add(label7);
            Empresas.Controls.Add(label6);
            Empresas.Controls.Add(label2);
            Empresas.Controls.Add(textBox4);
            Empresas.Controls.Add(textBox3);
            Empresas.Controls.Add(textBox1);
            Empresas.Controls.Add(button1);
            Empresas.Controls.Add(button2);
            Empresas.Controls.Add(label5);
            Empresas.Controls.Add(textBox2);
            Empresas.Controls.Add(ListaEmpresas);
            Empresas.Location = new Point(4, 34);
            Empresas.Name = "Empresas";
            Empresas.Padding = new Padding(3);
            Empresas.Size = new Size(1032, 571);
            Empresas.TabIndex = 1;
            Empresas.Text = "Empresas";
            Empresas.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Location = new Point(17, 90);
            button1.Name = "button1";
            button1.Size = new Size(183, 34);
            button1.TabIndex = 14;
            button1.Text = "Pesquisar";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(206, 89);
            button2.Name = "button2";
            button2.Size = new Size(185, 34);
            button2.TabIndex = 13;
            button2.Text = "Limpar";
            button2.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(108, 24);
            label5.Name = "label5";
            label5.Size = new Size(173, 25);
            label5.TabIndex = 11;
            label5.Text = "Pesquisar por Nome";
            label5.Click += label5_Click_1;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(17, 52);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(374, 31);
            textBox2.TabIndex = 4;
            // 
            // ListaEmpresas
            // 
            ListaEmpresas.FormattingEnabled = true;
            ListaEmpresas.ItemHeight = 25;
            ListaEmpresas.Location = new Point(17, 130);
            ListaEmpresas.Name = "ListaEmpresas";
            ListaEmpresas.Size = new Size(374, 379);
            ListaEmpresas.TabIndex = 1;
            ListaEmpresas.SelectedIndexChanged += listBox1_SelectedIndexChanged_3;
            // 
            // tabPage1
            // 
            tabPage1.Location = new Point(4, 34);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1032, 571);
            tabPage1.TabIndex = 2;
            tabPage1.Text = "Produtos";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            tabPage2.Location = new Point(4, 34);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1032, 571);
            tabPage2.TabIndex = 3;
            tabPage2.Text = "Clientes";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(529, 24);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(467, 31);
            textBox1.TabIndex = 15;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(529, 76);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(467, 31);
            textBox3.TabIndex = 16;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(529, 130);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(467, 31);
            textBox4.TabIndex = 17;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 8.25F);
            label2.Location = new Point(430, 29);
            label2.Name = "label2";
            label2.Size = new Size(61, 23);
            label2.TabIndex = 18;
            label2.Text = "Nome:";
            label2.Click += label2_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 9F);
            label6.Location = new Point(430, 82);
            label6.Name = "label6";
            label6.Size = new Size(78, 25);
            label6.TabIndex = 19;
            label6.Text = "Morada:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 9F);
            label7.Location = new Point(430, 136);
            label7.Name = "label7";
            label7.Size = new Size(88, 25);
            label7.TabIndex = 20;
            label7.Text = "Contacto:";
            label7.Click += label7_Click;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 9F);
            label8.Location = new Point(430, 195);
            label8.Name = "label8";
            label8.Size = new Size(149, 25);
            label8.TabIndex = 21;
            label8.Text = "Tipo de Empresa:";
            label8.Click += label8_Click;
            // 
            // textBox5
            // 
            textBox5.Location = new Point(581, 195);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(415, 31);
            textBox5.TabIndex = 22;
            // 
            // Encomenda
            // 
            Encomenda.FormattingEnabled = true;
            Encomenda.ItemHeight = 25;
            Encomenda.Location = new Point(430, 255);
            Encomenda.Name = "Encomenda";
            Encomenda.Size = new Size(283, 254);
            Encomenda.TabIndex = 23;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 9F);
            label9.Location = new Point(717, 255);
            label9.Name = "label9";
            label9.Size = new Size(129, 25);
            label9.TabIndex = 24;
            label9.Text = "Filtrar por data";
            label9.Click += label9_Click;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(717, 283);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(305, 31);
            dateTimePicker1.TabIndex = 25;
            dateTimePicker1.ValueChanged += dateTimePicker1_ValueChanged;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 9F);
            label10.Location = new Point(717, 322);
            label10.Name = "label10";
            label10.Size = new Size(193, 25);
            label10.TabIndex = 26;
            label10.Text = "Empresa de Transporte";
            label10.Click += label10_Click;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 9F);
            label11.Location = new Point(717, 386);
            label11.Name = "label11";
            label11.Size = new Size(77, 25);
            label11.TabIndex = 27;
            label11.Text = "Origem ";
            label11.Click += label11_Click;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "DHL", "CTT", "GLS", "DPD", "PAACH" });
            comboBox1.Location = new Point(717, 350);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(307, 33);
            comboBox1.TabIndex = 28;
            comboBox1.Text = "Seleciona uma empresa";
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Items.AddRange(new object[] { "Quinta da Vinha Verde", "Quinta da Esperança", "Quinta da Boa Vista", "Quinta da Fonte Fresca", "Quinta das Oliveiras" });
            comboBox2.Location = new Point(717, 414);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(307, 33);
            comboBox2.TabIndex = 29;
            comboBox2.Text = "Seleciona uma quinta";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Segoe UI", 9F);
            label12.Location = new Point(717, 450);
            label12.Name = "label12";
            label12.Size = new Size(70, 25);
            label12.TabIndex = 30;
            label12.Text = "Cliente ";
            // 
            // textBox6
            // 
            textBox6.Location = new Point(717, 478);
            textBox6.Name = "textBox6";
            textBox6.Size = new Size(263, 31);
            textBox6.TabIndex = 31;
            // 
            // button3
            // 
            button3.Location = new Point(979, 478);
            button3.Name = "button3";
            button3.Size = new Size(43, 31);
            button3.TabIndex = 33;
            button3.Text = "Ir";
            button3.UseVisualStyleBackColor = true;
            // 
            // AgroTrack
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(1040, 593);
            Controls.Add(tabControl1);
            Name = "AgroTrack";
            Text = "AgroTrack";
            Load += Form1_Load;
            tabControl1.ResumeLayout(false);
            Quintas.ResumeLayout(false);
            Quintas.PerformLayout();
            Empresas.ResumeLayout(false);
            Empresas.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage Quintas;
        private TabPage Empresas;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private ListBox ListaQuintas;
        private ListBox Animais;
        private ListBox Plantas;
        private TextBox PesquisarQuinta;
        private TextBox QuintaNome;
        private TextBox QuintaMorada;
        private Label label1;
        private Label label4;
        private Label label3;
        private Button buttonLimparPesquisaQuinta;
        private Button buttonPesquisarQuinta;
        private ListBox ListaEmpresas;
        private TextBox textBox2;
        private Button button1;
        private Button button2;
        private Label label5;
        private Label label2;
        private TextBox textBox4;
        private TextBox textBox3;
        private TextBox textBox1;
        private Label label6;
        private Label label7;
        private Label label8;
        private ListBox Encomenda;
        private TextBox textBox5;
        private Label label9;
        private DateTimePicker dateTimePicker1;
        private ComboBox comboBox1;
        private Label label11;
        private Label label10;
        private ComboBox comboBox2;
        private TextBox textBox6;
        private Label label12;
        private Button button3;
    }
}
