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
            label1 = new Label();
            Contacto = new TextBox();
            Nome = new TextBox();
            PesquisarQuinta = new TextBox();
            Plantas = new ListBox();
            Animais = new ListBox();
            ListaQuintas = new ListBox();
            Empresas = new TabPage();
            tabPage1 = new TabPage();
            tabPage2 = new TabPage();
            label2 = new Label();
            label3 = new Label();
            Morada = new TextBox();
            label4 = new Label();
            buttonLimparPesquisaQuinta = new Button();
            buttonPesquisarQuinta = new Button();
            tabControl1.SuspendLayout();
            Quintas.SuspendLayout();
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
            // 
            // Quintas
            // 
            Quintas.Controls.Add(buttonPesquisarQuinta);
            Quintas.Controls.Add(buttonLimparPesquisaQuinta);
            Quintas.Controls.Add(label4);
            Quintas.Controls.Add(Morada);
            Quintas.Controls.Add(label3);
            Quintas.Controls.Add(label2);
            Quintas.Controls.Add(label1);
            Quintas.Controls.Add(Contacto);
            Quintas.Controls.Add(Nome);
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
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(421, 116);
            label1.Name = "label1";
            label1.Size = new Size(61, 25);
            label1.TabIndex = 6;
            label1.Text = "Nome";
            label1.Click += label1_Click;
            // 
            // Contacto
            // 
            Contacto.Location = new Point(509, 186);
            Contacto.Name = "Contacto";
            Contacto.Size = new Size(383, 31);
            Contacto.TabIndex = 5;
            // 
            // Nome
            // 
            Nome.Location = new Point(509, 116);
            Nome.Name = "Nome";
            Nome.Size = new Size(383, 31);
            Nome.TabIndex = 4;
            Nome.TextChanged += textBox1_TextChanged_1;
            // 
            // PesquisarQuinta
            // 
            PesquisarQuinta.Location = new Point(724, 22);
            PesquisarQuinta.Name = "PesquisarQuinta";
            PesquisarQuinta.Size = new Size(300, 31);
            PesquisarQuinta.TabIndex = 3;
            PesquisarQuinta.TextChanged += textBox1_TextChanged;
            // 
            // Plantas
            // 
            Plantas.FormattingEnabled = true;
            Plantas.ItemHeight = 25;
            Plantas.Location = new Point(739, 422);
            Plantas.Name = "Plantas";
            Plantas.Size = new Size(242, 129);
            Plantas.TabIndex = 2;
            Plantas.SelectedIndexChanged += listBox1_SelectedIndexChanged_2;
            // 
            // Animais
            // 
            Animais.FormattingEnabled = true;
            Animais.ItemHeight = 25;
            Animais.Location = new Point(424, 422);
            Animais.Name = "Animais";
            Animais.Size = new Size(242, 129);
            Animais.TabIndex = 1;
            Animais.SelectedIndexChanged += listBox1_SelectedIndexChanged_1;
            // 
            // ListaQuintas
            // 
            ListaQuintas.FormattingEnabled = true;
            ListaQuintas.ItemHeight = 25;
            ListaQuintas.Location = new Point(20, 22);
            ListaQuintas.Name = "ListaQuintas";
            ListaQuintas.Size = new Size(359, 529);
            ListaQuintas.TabIndex = 0;
            ListaQuintas.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            // 
            // Empresas
            // 
            Empresas.Location = new Point(4, 34);
            Empresas.Name = "Empresas";
            Empresas.Padding = new Padding(3);
            Empresas.Size = new Size(1032, 571);
            Empresas.TabIndex = 1;
            Empresas.Text = "Empresas";
            Empresas.UseVisualStyleBackColor = true;
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
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(408, 189);
            label2.Name = "label2";
            label2.Size = new Size(84, 25);
            label2.TabIndex = 7;
            label2.Text = "Contacto";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(408, 260);
            label3.Name = "label3";
            label3.Size = new Size(74, 25);
            label3.TabIndex = 8;
            label3.Text = "Morada";
            // 
            // Morada
            // 
            Morada.Location = new Point(509, 254);
            Morada.Name = "Morada";
            Morada.Size = new Size(383, 31);
            Morada.TabIndex = 9;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(541, 25);
            label4.Name = "label4";
            label4.Size = new Size(177, 25);
            label4.TabIndex = 10;
            label4.Text = "Pesquisar por Nome:";
            label4.Click += label4_Click;
            // 
            // buttonLimparPesquisaQuinta
            // 
            buttonLimparPesquisaQuinta.Location = new Point(882, 59);
            buttonLimparPesquisaQuinta.Name = "buttonLimparPesquisaQuinta";
            buttonLimparPesquisaQuinta.Size = new Size(142, 34);
            buttonLimparPesquisaQuinta.TabIndex = 11;
            buttonLimparPesquisaQuinta.Text = "Limpar";
            buttonLimparPesquisaQuinta.UseVisualStyleBackColor = true;
            // 
            // buttonPesquisarQuinta
            // 
            buttonPesquisarQuinta.Location = new Point(724, 59);
            buttonPesquisarQuinta.Name = "buttonPesquisarQuinta";
            buttonPesquisarQuinta.Size = new Size(142, 34);
            buttonPesquisarQuinta.TabIndex = 12;
            buttonPesquisarQuinta.Text = "Pesquisar";
            buttonPesquisarQuinta.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1040, 746);
            Controls.Add(tabControl1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            tabControl1.ResumeLayout(false);
            Quintas.ResumeLayout(false);
            Quintas.PerformLayout();
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
        private TextBox Nome;
        private TextBox Contacto;
        private Label label1;
        private Label label4;
        private TextBox Morada;
        private Label label3;
        private Label label2;
        private Button buttonLimparPesquisaQuinta;
        private Button buttonPesquisarQuinta;


    }
}
