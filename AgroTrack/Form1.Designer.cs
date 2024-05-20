namespace AgroTrack
{
    partial class Form1
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
            label2 = new Label();
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
            button1 = new Button();
            button2 = new Button();
            label5 = new Label();
            textBox2 = new TextBox();
            ListaEmpresas = new ListBox();
            tabPage1 = new TabPage();
            tabPage2 = new TabPage();
            Agricultores = new ListBox();
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
            tabControl1.Size = new Size(1076, 658);
            tabControl1.TabIndex = 0;
            tabControl1.SelectedIndexChanged += tabControl1_SelectedIndexChanged;
            // 
            // Quintas
            // 
            Quintas.Controls.Add(Agricultores);
            Quintas.Controls.Add(label2);
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
            Quintas.Size = new Size(1068, 620);
            Quintas.TabIndex = 0;
            Quintas.Text = "Quintas";
            Quintas.UseVisualStyleBackColor = true;
            Quintas.Click += Quintas_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(448, 158);
            label2.Name = "label2";
            label2.Size = new Size(84, 25);
            label2.TabIndex = 14;
            label2.Text = "Contacto";
            label2.Click += label2_Click;
            // 
            // QuintaContacto
            // 
            QuintaContacto.Location = new Point(559, 155);
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
            label3.Location = new Point(458, 84);
            label3.Name = "label3";
            label3.Size = new Size(74, 25);
            label3.TabIndex = 8;
            label3.Text = "Morada";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(471, 11);
            label1.Name = "label1";
            label1.Size = new Size(61, 25);
            label1.TabIndex = 6;
            label1.Text = "Nome";
            label1.Click += label1_Click;
            // 
            // QuintaMorada
            // 
            QuintaMorada.Location = new Point(559, 81);
            QuintaMorada.Name = "QuintaMorada";
            QuintaMorada.Size = new Size(383, 31);
            QuintaMorada.TabIndex = 5;
            // 
            // QuintaNome
            // 
            QuintaNome.Location = new Point(559, 11);
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
            Plantas.Location = new Point(743, 343);
            Plantas.Name = "Plantas";
            Plantas.Size = new Size(242, 204);
            Plantas.TabIndex = 2;
            Plantas.SelectedIndexChanged += listBox1_SelectedIndexChanged_2;
            // 
            // Animais
            // 
            Animais.FormattingEnabled = true;
            Animais.ItemHeight = 25;
            Animais.Location = new Point(433, 347);
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
            button1.Location = new Point(632, 79);
            button1.Name = "button1";
            button1.Size = new Size(142, 34);
            button1.TabIndex = 14;
            button1.Text = "Pesquisar";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(790, 79);
            button2.Name = "button2";
            button2.Size = new Size(142, 34);
            button2.TabIndex = 13;
            button2.Text = "Limpar";
            button2.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(449, 45);
            label5.Name = "label5";
            label5.Size = new Size(177, 25);
            label5.TabIndex = 11;
            label5.Text = "Pesquisar por Nome:";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(632, 42);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(300, 31);
            textBox2.TabIndex = 4;
            // 
            // ListaEmpresas
            // 
            ListaEmpresas.FormattingEnabled = true;
            ListaEmpresas.ItemHeight = 25;
            ListaEmpresas.Location = new Point(38, 18);
            ListaEmpresas.Name = "ListaEmpresas";
            ListaEmpresas.Size = new Size(359, 529);
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
            // Agricultores
            // 
            Agricultores.FormattingEnabled = true;
            Agricultores.ItemHeight = 25;
            Agricultores.Location = new Point(433, 201);
            Agricultores.Name = "Agricultores";
            Agricultores.Size = new Size(552, 129);
            Agricultores.TabIndex = 15;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1075, 659);
            Controls.Add(tabControl1);
            Name = "Form1";
            Text = "Form1";
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
        private TextBox QuintaContacto;
        private ListBox Agricultores;
    }
}
