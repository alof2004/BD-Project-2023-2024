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
            AgricultoresPag = new TabControl();
            Quintas = new TabPage();
            listBox1 = new ListBox();
            Agricultores = new ListBox();
            label13 = new Label();
            QuintaContacto = new TextBox();
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
            button7 = new Button();
            button6 = new Button();
            button5 = new Button();
            button4 = new Button();
            button3 = new Button();
            textBox6 = new TextBox();
            label12 = new Label();
            comboBox2 = new ComboBox();
            comboBox1 = new ComboBox();
            label11 = new Label();
            label10 = new Label();
            dateTimePicker1 = new DateTimePicker();
            label9 = new Label();
            Encomenda = new ListBox();
            textBox5 = new TextBox();
            label8 = new Label();
            label7 = new Label();
            label6 = new Label();
            label2 = new Label();
            textBox4 = new TextBox();
            textBox3 = new TextBox();
            textBox1 = new TextBox();
            button1 = new Button();
            button2 = new Button();
            label5 = new Label();
            textBox2 = new TextBox();
            ListaEmpresas = new ListBox();
            tabPage1 = new TabPage();
            tabPage2 = new TabPage();
            tabPage3 = new TabPage();
            ListaAgricultores = new ListBox();
            QuintaAgricultor = new Label();
            QuintaAgricultores = new TextBox();
            Contacto = new Label();
            label16 = new Label();
            ContactoAgricultor = new TextBox();
            NomeAgricultor = new TextBox();
            richTextBox1 = new RichTextBox();
            button8 = new Button();
            button9 = new Button();
            label14 = new Label();
            textBox8 = new TextBox();
            ColheitasAgricultor = new ListBox();
            ComprasCliente = new ListBox();
            button10 = new Button();
            button11 = new Button();
            label15 = new Label();
            textBox9 = new TextBox();
            label17 = new Label();
            textBox10 = new TextBox();
            label18 = new Label();
            label19 = new Label();
            textBox11 = new TextBox();
            textBox12 = new TextBox();
            ListaClientes = new ListBox();
            button12 = new Button();
            button13 = new Button();
            button14 = new Button();
            button15 = new Button();
            label20 = new Label();
            NumeroCCAgricultor = new TextBox();
            AgricultoresPag.SuspendLayout();
            Quintas.SuspendLayout();
            Empresas.SuspendLayout();
            tabPage2.SuspendLayout();
            tabPage3.SuspendLayout();
            SuspendLayout();
            // 
            // AgricultoresPag
            // 
            AgricultoresPag.Controls.Add(Quintas);
            AgricultoresPag.Controls.Add(Empresas);
            AgricultoresPag.Controls.Add(tabPage1);
            AgricultoresPag.Controls.Add(tabPage2);
            AgricultoresPag.Controls.Add(tabPage3);
            AgricultoresPag.Location = new Point(0, 0);
            AgricultoresPag.Name = "AgricultoresPag";
            AgricultoresPag.SelectedIndex = 0;
            AgricultoresPag.Size = new Size(1148, 657);
            AgricultoresPag.TabIndex = 0;
            AgricultoresPag.SelectedIndexChanged += tabControl1_SelectedIndexChanged;
            // 
            // Quintas
            // 
            Quintas.Controls.Add(listBox1);
            Quintas.Controls.Add(Agricultores);
            Quintas.Controls.Add(label13);
            Quintas.Controls.Add(QuintaContacto);
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
            Quintas.Size = new Size(1140, 619);
            Quintas.TabIndex = 0;
            Quintas.Text = "Quintas";
            Quintas.UseVisualStyleBackColor = true;
            Quintas.Click += Quintas_Click;
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.ItemHeight = 25;
            listBox1.Location = new Point(744, 212);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(242, 154);
            listBox1.TabIndex = 16;
            // 
            // Agricultores
            // 
            Agricultores.FormattingEnabled = true;
            Agricultores.ItemHeight = 25;
            Agricultores.Location = new Point(439, 210);
            Agricultores.Name = "Agricultores";
            Agricultores.Size = new Size(242, 154);
            Agricultores.TabIndex = 15;
            Agricultores.SelectedIndexChanged += Agricultores_SelectedIndexChanged;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(452, 164);
            label13.Name = "label13";
            label13.Size = new Size(84, 25);
            label13.TabIndex = 14;
            label13.Text = "Contacto";
            label13.Click += label13_Click;
            // 
            // QuintaContacto
            // 
            QuintaContacto.Location = new Point(563, 158);
            QuintaContacto.Name = "QuintaContacto";
            QuintaContacto.Size = new Size(383, 31);
            QuintaContacto.TabIndex = 13;
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
            label3.Location = new Point(464, 97);
            label3.Name = "label3";
            label3.Size = new Size(74, 25);
            label3.TabIndex = 8;
            label3.Text = "Morada";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(475, 24);
            label1.Name = "label1";
            label1.Size = new Size(61, 25);
            label1.TabIndex = 6;
            label1.Text = "Nome";
            label1.Click += label1_Click;
            // 
            // QuintaMorada
            // 
            QuintaMorada.Location = new Point(563, 94);
            QuintaMorada.Name = "QuintaMorada";
            QuintaMorada.Size = new Size(383, 31);
            QuintaMorada.TabIndex = 5;
            // 
            // QuintaNome
            // 
            QuintaNome.Location = new Point(563, 24);
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
            Plantas.Location = new Point(744, 372);
            Plantas.Name = "Plantas";
            Plantas.Size = new Size(242, 204);
            Plantas.TabIndex = 2;
            Plantas.SelectedIndexChanged += listBox1_SelectedIndexChanged_2;
            // 
            // Animais
            // 
            Animais.FormattingEnabled = true;
            Animais.ItemHeight = 25;
            Animais.Location = new Point(439, 372);
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
            ListaQuintas.Size = new Size(359, 454);
            ListaQuintas.TabIndex = 0;
            ListaQuintas.SelectedIndexChanged += ListaQuintas_SelectedIndexChanged;
            // 
            // Empresas
            // 
            Empresas.Controls.Add(button7);
            Empresas.Controls.Add(button6);
            Empresas.Controls.Add(button5);
            Empresas.Controls.Add(button4);
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
            Empresas.Size = new Size(1140, 619);
            Empresas.TabIndex = 1;
            Empresas.Text = "Empresas";
            Empresas.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            button7.Location = new Point(557, 537);
            button7.Name = "button7";
            button7.Size = new Size(264, 34);
            button7.TabIndex = 37;
            button7.Text = "Eliminar Empresa";
            button7.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            button6.Location = new Point(827, 537);
            button6.Name = "button6";
            button6.Size = new Size(264, 34);
            button6.TabIndex = 36;
            button6.Text = "Cancelar Encomenda";
            button6.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            button5.Location = new Point(287, 537);
            button5.Name = "button5";
            button5.Size = new Size(264, 34);
            button5.TabIndex = 35;
            button5.Text = "Adicionar Encomenda";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click_1;
            // 
            // button4
            // 
            button4.Location = new Point(17, 537);
            button4.Name = "button4";
            button4.Size = new Size(264, 34);
            button4.TabIndex = 34;
            button4.Text = "Adicionar Empresa";
            button4.UseVisualStyleBackColor = true;
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
            // textBox6
            // 
            textBox6.Location = new Point(717, 478);
            textBox6.Name = "textBox6";
            textBox6.Size = new Size(263, 31);
            textBox6.TabIndex = 31;
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
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(717, 283);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(305, 31);
            dateTimePicker1.TabIndex = 25;
            dateTimePicker1.ValueChanged += dateTimePicker1_ValueChanged;
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
            // Encomenda
            // 
            Encomenda.FormattingEnabled = true;
            Encomenda.ItemHeight = 25;
            Encomenda.Location = new Point(430, 255);
            Encomenda.Name = "Encomenda";
            Encomenda.Size = new Size(283, 254);
            Encomenda.TabIndex = 23;
            // 
            // textBox5
            // 
            textBox5.Location = new Point(581, 195);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(443, 31);
            textBox5.TabIndex = 22;
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
            // textBox4
            // 
            textBox4.Location = new Point(529, 130);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(495, 31);
            textBox4.TabIndex = 17;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(529, 76);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(495, 31);
            textBox3.TabIndex = 16;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(529, 24);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(493, 31);
            textBox1.TabIndex = 15;
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
            tabPage1.Size = new Size(1140, 619);
            tabPage1.TabIndex = 2;
            tabPage1.Text = "Produtos";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(button15);
            tabPage2.Controls.Add(button14);
            tabPage2.Controls.Add(button12);
            tabPage2.Controls.Add(button13);
            tabPage2.Controls.Add(ComprasCliente);
            tabPage2.Controls.Add(button10);
            tabPage2.Controls.Add(button11);
            tabPage2.Controls.Add(label15);
            tabPage2.Controls.Add(textBox9);
            tabPage2.Controls.Add(label17);
            tabPage2.Controls.Add(textBox10);
            tabPage2.Controls.Add(label18);
            tabPage2.Controls.Add(label19);
            tabPage2.Controls.Add(textBox11);
            tabPage2.Controls.Add(textBox12);
            tabPage2.Controls.Add(ListaClientes);
            tabPage2.Location = new Point(4, 34);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1140, 619);
            tabPage2.TabIndex = 3;
            tabPage2.Text = "Clientes";
            tabPage2.UseVisualStyleBackColor = true;
            tabPage2.Click += tabPage2_Click;
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(label20);
            tabPage3.Controls.Add(NumeroCCAgricultor);
            tabPage3.Controls.Add(ColheitasAgricultor);
            tabPage3.Controls.Add(button8);
            tabPage3.Controls.Add(button9);
            tabPage3.Controls.Add(label14);
            tabPage3.Controls.Add(textBox8);
            tabPage3.Controls.Add(richTextBox1);
            tabPage3.Controls.Add(QuintaAgricultor);
            tabPage3.Controls.Add(QuintaAgricultores);
            tabPage3.Controls.Add(Contacto);
            tabPage3.Controls.Add(label16);
            tabPage3.Controls.Add(ContactoAgricultor);
            tabPage3.Controls.Add(NomeAgricultor);
            tabPage3.Controls.Add(ListaAgricultores);
            tabPage3.Location = new Point(4, 34);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(1140, 619);
            tabPage3.TabIndex = 4;
            tabPage3.Text = "Agricultores";
            tabPage3.UseVisualStyleBackColor = true;
            tabPage3.Click += tabPage3_Click;
            // 
            // ListaAgricultores
            // 
            ListaAgricultores.FormattingEnabled = true;
            ListaAgricultores.ItemHeight = 25;
            ListaAgricultores.Location = new Point(31, 128);
            ListaAgricultores.Name = "ListaAgricultores";
            ListaAgricultores.Size = new Size(359, 279);
            ListaAgricultores.TabIndex = 1;
            ListaAgricultores.SelectedIndexChanged += ListaAgricultores_SelectedIndexChanged;
            // 
            // QuintaAgricultor
            // 
            QuintaAgricultor.AutoSize = true;
            QuintaAgricultor.Location = new Point(437, 169);
            QuintaAgricultor.Name = "QuintaAgricultor";
            QuintaAgricultor.Size = new Size(65, 25);
            QuintaAgricultor.TabIndex = 20;
            QuintaAgricultor.Text = "Quinta";
            // 
            // QuintaAgricultores
            // 
            QuintaAgricultores.Location = new Point(525, 166);
            QuintaAgricultores.Name = "QuintaAgricultores";
            QuintaAgricultores.Size = new Size(383, 31);
            QuintaAgricultores.TabIndex = 19;
            // 
            // Contacto
            // 
            Contacto.AutoSize = true;
            Contacto.Location = new Point(426, 81);
            Contacto.Name = "Contacto";
            Contacto.Size = new Size(84, 25);
            Contacto.TabIndex = 18;
            Contacto.Text = "Contacto";
            Contacto.Click += Contacto_Click;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(437, 32);
            label16.Name = "label16";
            label16.Size = new Size(61, 25);
            label16.TabIndex = 17;
            label16.Text = "Nome";
            // 
            // ContactoAgricultor
            // 
            ContactoAgricultor.Location = new Point(525, 78);
            ContactoAgricultor.Name = "ContactoAgricultor";
            ContactoAgricultor.Size = new Size(383, 31);
            ContactoAgricultor.TabIndex = 16;
            // 
            // NomeAgricultor
            // 
            NomeAgricultor.Location = new Point(525, 32);
            NomeAgricultor.Name = "NomeAgricultor";
            NomeAgricultor.Size = new Size(383, 31);
            NomeAgricultor.TabIndex = 15;
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(929, 10);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(195, 204);
            richTextBox1.TabIndex = 21;
            richTextBox1.Text = "";
            // 
            // button8
            // 
            button8.Location = new Point(31, 75);
            button8.Name = "button8";
            button8.Size = new Size(177, 34);
            button8.TabIndex = 25;
            button8.Text = "Pesquisar";
            button8.UseVisualStyleBackColor = true;
            // 
            // button9
            // 
            button9.Location = new Point(214, 75);
            button9.Name = "button9";
            button9.Size = new Size(176, 34);
            button9.TabIndex = 24;
            button9.Text = "Limpar";
            button9.UseVisualStyleBackColor = true;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(31, 10);
            label14.Name = "label14";
            label14.Size = new Size(177, 25);
            label14.TabIndex = 23;
            label14.Text = "Pesquisar por Nome:";
            // 
            // textBox8
            // 
            textBox8.Location = new Point(31, 38);
            textBox8.Name = "textBox8";
            textBox8.Size = new Size(359, 31);
            textBox8.TabIndex = 22;
            // 
            // ColheitasAgricultor
            // 
            ColheitasAgricultor.FormattingEnabled = true;
            ColheitasAgricultor.ItemHeight = 25;
            ColheitasAgricultor.Location = new Point(437, 231);
            ColheitasAgricultor.Name = "ColheitasAgricultor";
            ColheitasAgricultor.Size = new Size(672, 254);
            ColheitasAgricultor.TabIndex = 26;
            ColheitasAgricultor.SelectedIndexChanged += ColheitasAgricultor_SelectedIndexChanged;
            // 
            // ComprasCliente
            // 
            ComprasCliente.FormattingEnabled = true;
            ComprasCliente.ItemHeight = 25;
            ComprasCliente.Location = new Point(422, 211);
            ComprasCliente.Name = "ComprasCliente";
            ComprasCliente.Size = new Size(712, 304);
            ComprasCliente.TabIndex = 39;
            ComprasCliente.SelectedIndexChanged += listBox2_SelectedIndexChanged;
            // 
            // button10
            // 
            button10.Location = new Point(22, 83);
            button10.Name = "button10";
            button10.Size = new Size(177, 34);
            button10.TabIndex = 38;
            button10.Text = "Pesquisar";
            button10.UseVisualStyleBackColor = true;
            // 
            // button11
            // 
            button11.Location = new Point(205, 83);
            button11.Name = "button11";
            button11.Size = new Size(176, 34);
            button11.TabIndex = 37;
            button11.Text = "Limpar";
            button11.UseVisualStyleBackColor = true;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(22, 18);
            label15.Name = "label15";
            label15.Size = new Size(177, 25);
            label15.TabIndex = 36;
            label15.Text = "Pesquisar por Nome:";
            // 
            // textBox9
            // 
            textBox9.Location = new Point(22, 46);
            textBox9.Name = "textBox9";
            textBox9.Size = new Size(359, 31);
            textBox9.TabIndex = 35;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(422, 139);
            label17.Name = "label17";
            label17.Size = new Size(104, 25);
            label17.TabIndex = 33;
            label17.Text = "CC Número";
            // 
            // textBox10
            // 
            textBox10.Location = new Point(532, 136);
            textBox10.Name = "textBox10";
            textBox10.Size = new Size(570, 31);
            textBox10.TabIndex = 32;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Location = new Point(436, 85);
            label18.Name = "label18";
            label18.Size = new Size(84, 25);
            label18.TabIndex = 31;
            label18.Text = "Contacto";
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Location = new Point(444, 27);
            label19.Name = "label19";
            label19.Size = new Size(61, 25);
            label19.TabIndex = 30;
            label19.Text = "Nome";
            // 
            // textBox11
            // 
            textBox11.Location = new Point(532, 79);
            textBox11.Name = "textBox11";
            textBox11.Size = new Size(570, 31);
            textBox11.TabIndex = 29;
            // 
            // textBox12
            // 
            textBox12.Location = new Point(532, 27);
            textBox12.Name = "textBox12";
            textBox12.Size = new Size(570, 31);
            textBox12.TabIndex = 28;
            // 
            // ListaClientes
            // 
            ListaClientes.FormattingEnabled = true;
            ListaClientes.ItemHeight = 25;
            ListaClientes.Location = new Point(22, 136);
            ListaClientes.Name = "ListaClientes";
            ListaClientes.Size = new Size(359, 279);
            ListaClientes.TabIndex = 27;
            // 
            // button12
            // 
            button12.Location = new Point(422, 537);
            button12.Name = "button12";
            button12.Size = new Size(177, 34);
            button12.TabIndex = 41;
            button12.Text = "Adicionar Cliente";
            button12.UseVisualStyleBackColor = true;
            // 
            // button13
            // 
            button13.Location = new Point(605, 537);
            button13.Name = "button13";
            button13.Size = new Size(176, 34);
            button13.TabIndex = 40;
            button13.Text = "Apagar Cliente";
            button13.UseVisualStyleBackColor = true;
            // 
            // button14
            // 
            button14.Location = new Point(787, 537);
            button14.Name = "button14";
            button14.Size = new Size(176, 34);
            button14.TabIndex = 42;
            button14.Text = "Nova Compra";
            button14.UseVisualStyleBackColor = true;
            // 
            // button15
            // 
            button15.Location = new Point(969, 537);
            button15.Name = "button15";
            button15.Size = new Size(165, 34);
            button15.TabIndex = 43;
            button15.Text = "Apagar Compra";
            button15.UseVisualStyleBackColor = true;
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Location = new Point(415, 128);
            label20.Name = "label20";
            label20.Size = new Size(104, 25);
            label20.TabIndex = 28;
            label20.Text = "Numero CC";
            // 
            // NumeroCCAgricultor
            // 
            NumeroCCAgricultor.Location = new Point(525, 122);
            NumeroCCAgricultor.Name = "NumeroCCAgricultor";
            NumeroCCAgricultor.Size = new Size(383, 31);
            NumeroCCAgricultor.TabIndex = 27;
            // 
            // AgroTrack
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(1153, 617);
            Controls.Add(AgricultoresPag);
            Name = "AgroTrack";
            Text = "AgroTrack";
            Load += Form1_Load;
            AgricultoresPag.ResumeLayout(false);
            Quintas.ResumeLayout(false);
            Quintas.PerformLayout();
            Empresas.ResumeLayout(false);
            Empresas.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            tabPage3.ResumeLayout(false);
            tabPage3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TabControl AgricultoresPag;
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
        private Label label13;
        private TextBox QuintaContacto;
        private ListBox Agricultores;
        private Button button5;
        private Button button4;
        private Button button6;
        private Button button7;
        private ListBox listBox1;
        private TabPage tabPage3;
        private ListBox ListaAgricultores;
        private Label QuintaAgricultor;
        private TextBox QuintaAgricultores;
        private Label Contacto;
        private Label label16;
        private TextBox ContactoAgricultor;
        private TextBox NomeAgricultor;
        private RichTextBox richTextBox1;
        private Button button8;
        private Button button9;
        private Label label14;
        private TextBox textBox8;
        private ListBox ComprasCliente;
        private Button button10;
        private Button button11;
        private Label label15;
        private TextBox textBox9;
        private Label label17;
        private TextBox textBox10;
        private Label label18;
        private Label label19;
        private TextBox textBox11;
        private TextBox textBox12;
        private ListBox ListaClientes;
        private ListBox ColheitasAgricultor;
        private Button button12;
        private Button button13;
        private Button button14;
        private Button button15;
        private Label label20;
        private TextBox NumeroCCAgricultor;
    }
}
