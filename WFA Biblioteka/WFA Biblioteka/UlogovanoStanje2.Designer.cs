namespace WFA_Biblioteka
{
    partial class UlogovanoStanje2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label9 = new System.Windows.Forms.Label();
            this.labelKorisnik = new System.Windows.Forms.Label();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnDodaj = new System.Windows.Forms.Button();
            this.prikazi = new System.Windows.Forms.ListBox();
            this.tabovi = new System.Windows.Forms.TabControl();
            this.tabSveKnjige = new System.Windows.Forms.TabPage();
            this.cbNijeIzdato = new System.Windows.Forms.CheckBox();
            this.cbIzdato = new System.Windows.Forms.CheckBox();
            this.comboPrikaz = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnIzbaci = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.listaKorpa = new System.Windows.Forms.ListBox();
            this.btnIznajmi = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.listaIznajmljeneKnjige = new System.Windows.Forms.ListBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.labelaSuma = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.listaRacuna = new System.Windows.Forms.ListBox();
            this.labelaClanarine = new System.Windows.Forms.Label();
            this.btnPodesavanja = new System.Windows.Forms.Button();
            this.btnProduzi = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timerRefreser = new System.Windows.Forms.Timer(this.components);
            this.btnPrimjedba = new System.Windows.Forms.Button();
            this.tabovi.SuspendLayout();
            this.tabSveKnjige.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Harlow Solid Italic", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(12, 9);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(213, 180);
            this.label9.TabIndex = 9;
            this.label9.Text = "Bibliteka Fortuna\r\nBraće Jugovića 55\r\nZvornik\r\n\r\nKontakt telefon: \r\n065/112-112\r\n" +
    "";
            // 
            // labelKorisnik
            // 
            this.labelKorisnik.AutoSize = true;
            this.labelKorisnik.Font = new System.Drawing.Font("Harlow Solid Italic", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelKorisnik.Location = new System.Drawing.Point(10, 228);
            this.labelKorisnik.Name = "labelKorisnik";
            this.labelKorisnik.Size = new System.Drawing.Size(199, 30);
            this.labelKorisnik.TabIndex = 10;
            this.labelKorisnik.Text = "Ulogovani ste kao:\r\n";
            this.labelKorisnik.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // btnLogout
            // 
            this.btnLogout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLogout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnLogout.Location = new System.Drawing.Point(857, 625);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(56, 27);
            this.btnLogout.TabIndex = 11;
            this.btnLogout.Text = "Log Out";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnDodaj
            // 
            this.btnDodaj.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDodaj.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnDodaj.Enabled = false;
            this.btnDodaj.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDodaj.Location = new System.Drawing.Point(759, 625);
            this.btnDodaj.Name = "btnDodaj";
            this.btnDodaj.Size = new System.Drawing.Size(92, 27);
            this.btnDodaj.TabIndex = 12;
            this.btnDodaj.Text = "Dodaj u Korpu";
            this.btnDodaj.UseVisualStyleBackColor = false;
            this.btnDodaj.Click += new System.EventHandler(this.btnDodaj_Click);
            // 
            // prikazi
            // 
            this.prikazi.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.prikazi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.prikazi.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.prikazi.FormattingEnabled = true;
            this.prikazi.ItemHeight = 18;
            this.prikazi.Location = new System.Drawing.Point(6, 35);
            this.prikazi.Name = "prikazi";
            this.prikazi.Size = new System.Drawing.Size(665, 544);
            this.prikazi.TabIndex = 13;
            this.prikazi.SelectedIndexChanged += new System.EventHandler(this.prikazi_SelectedIndexChanged);
            // 
            // tabovi
            // 
            this.tabovi.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabovi.Controls.Add(this.tabSveKnjige);
            this.tabovi.Controls.Add(this.tabPage2);
            this.tabovi.Controls.Add(this.tabPage3);
            this.tabovi.Controls.Add(this.tabPage4);
            this.tabovi.Location = new System.Drawing.Point(231, 12);
            this.tabovi.Name = "tabovi";
            this.tabovi.SelectedIndex = 0;
            this.tabovi.Size = new System.Drawing.Size(682, 607);
            this.tabovi.TabIndex = 16;
            // 
            // tabSveKnjige
            // 
            this.tabSveKnjige.BackColor = System.Drawing.Color.Teal;
            this.tabSveKnjige.Controls.Add(this.cbNijeIzdato);
            this.tabSveKnjige.Controls.Add(this.cbIzdato);
            this.tabSveKnjige.Controls.Add(this.comboPrikaz);
            this.tabSveKnjige.Controls.Add(this.label4);
            this.tabSveKnjige.Controls.Add(this.prikazi);
            this.tabSveKnjige.Location = new System.Drawing.Point(4, 22);
            this.tabSveKnjige.Name = "tabSveKnjige";
            this.tabSveKnjige.Padding = new System.Windows.Forms.Padding(3);
            this.tabSveKnjige.Size = new System.Drawing.Size(674, 581);
            this.tabSveKnjige.TabIndex = 0;
            this.tabSveKnjige.Text = "Pregled svih knjiga";
            this.tabSveKnjige.Enter += new System.EventHandler(this.tabSveKnjige_Enter);
            this.tabSveKnjige.Leave += new System.EventHandler(this.tabSveKnjige_Leave);
            // 
            // cbNijeIzdato
            // 
            this.cbNijeIzdato.AutoSize = true;
            this.cbNijeIzdato.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.cbNijeIzdato.Checked = true;
            this.cbNijeIzdato.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbNijeIzdato.Location = new System.Drawing.Point(305, 12);
            this.cbNijeIzdato.Name = "cbNijeIzdato";
            this.cbNijeIzdato.Size = new System.Drawing.Size(76, 17);
            this.cbNijeIzdato.TabIndex = 20;
            this.cbNijeIzdato.Text = "Nije Izdato";
            this.cbNijeIzdato.UseVisualStyleBackColor = false;
            this.cbNijeIzdato.CheckedChanged += new System.EventHandler(this.cbNijeIzdato_CheckedChanged);
            // 
            // cbIzdato
            // 
            this.cbIzdato.AutoSize = true;
            this.cbIzdato.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.cbIzdato.Checked = true;
            this.cbIzdato.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbIzdato.Location = new System.Drawing.Point(245, 12);
            this.cbIzdato.Name = "cbIzdato";
            this.cbIzdato.Size = new System.Drawing.Size(55, 17);
            this.cbIzdato.TabIndex = 19;
            this.cbIzdato.Text = "Izdato";
            this.cbIzdato.UseVisualStyleBackColor = false;
            this.cbIzdato.CheckedChanged += new System.EventHandler(this.cbIzdato_CheckedChanged);
            // 
            // comboPrikaz
            // 
            this.comboPrikaz.AutoCompleteCustomSource.AddRange(new string[] {
            "Izdavač",
            "Pisac",
            "Kategorija",
            "Izdato",
            "Nije izdato",
            "Godina izdavanja",
            "Naslov",
            "Cijena iznajmljivanja"});
            this.comboPrikaz.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.comboPrikaz.DisplayMember = "(none)";
            this.comboPrikaz.FormattingEnabled = true;
            this.comboPrikaz.Items.AddRange(new object[] {
            "- -",
            "Izdavač",
            "Kategorija",
            "Pisac"});
            this.comboPrikaz.Location = new System.Drawing.Point(102, 6);
            this.comboPrikaz.Name = "comboPrikaz";
            this.comboPrikaz.Size = new System.Drawing.Size(121, 21);
            this.comboPrikaz.Sorted = true;
            this.comboPrikaz.TabIndex = 18;
            this.comboPrikaz.Text = "- -";
            this.comboPrikaz.SelectedIndexChanged += new System.EventHandler(this.comboPrikaz_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.label4.Location = new System.Drawing.Point(8, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Prikaži knjige po:";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.Teal;
            this.tabPage2.Controls.Add(this.btnIzbaci);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.listaKorpa);
            this.tabPage2.Controls.Add(this.btnIznajmi);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(674, 581);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Korpa";
            // 
            // btnIzbaci
            // 
            this.btnIzbaci.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnIzbaci.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnIzbaci.Enabled = false;
            this.btnIzbaci.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnIzbaci.Location = new System.Drawing.Point(482, 549);
            this.btnIzbaci.Name = "btnIzbaci";
            this.btnIzbaci.Size = new System.Drawing.Size(92, 26);
            this.btnIzbaci.TabIndex = 3;
            this.btnIzbaci.Text = "Izbaci";
            this.btnIzbaci.UseVisualStyleBackColor = false;
            this.btnIzbaci.Click += new System.EventHandler(this.btnIzbaci_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Teal;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(150, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Knjige koje su u korpi:";
            // 
            // listaKorpa
            // 
            this.listaKorpa.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listaKorpa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.listaKorpa.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.listaKorpa.FormattingEnabled = true;
            this.listaKorpa.ItemHeight = 18;
            this.listaKorpa.Location = new System.Drawing.Point(3, 19);
            this.listaKorpa.Name = "listaKorpa";
            this.listaKorpa.Size = new System.Drawing.Size(665, 526);
            this.listaKorpa.TabIndex = 1;
            this.listaKorpa.SelectedIndexChanged += new System.EventHandler(this.listaKorpa_SelectedIndexChanged);
            // 
            // btnIznajmi
            // 
            this.btnIznajmi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnIznajmi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnIznajmi.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnIznajmi.Location = new System.Drawing.Point(580, 549);
            this.btnIznajmi.Name = "btnIznajmi";
            this.btnIznajmi.Size = new System.Drawing.Size(88, 26);
            this.btnIznajmi.TabIndex = 0;
            this.btnIznajmi.Text = "Iznajmi";
            this.btnIznajmi.UseVisualStyleBackColor = false;
            this.btnIznajmi.Click += new System.EventHandler(this.btnIznajmi_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.Teal;
            this.tabPage3.Controls.Add(this.listaIznajmljeneKnjige);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(674, 581);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Knjige koje sam iznajmio";
            // 
            // listaIznajmljeneKnjige
            // 
            this.listaIznajmljeneKnjige.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listaIznajmljeneKnjige.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.listaIznajmljeneKnjige.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.listaIznajmljeneKnjige.FormattingEnabled = true;
            this.listaIznajmljeneKnjige.ItemHeight = 18;
            this.listaIznajmljeneKnjige.Location = new System.Drawing.Point(6, 6);
            this.listaIznajmljeneKnjige.Name = "listaIznajmljeneKnjige";
            this.listaIznajmljeneKnjige.Size = new System.Drawing.Size(662, 580);
            this.listaIznajmljeneKnjige.TabIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.Color.Teal;
            this.tabPage4.Controls.Add(this.labelaSuma);
            this.tabPage4.Controls.Add(this.label5);
            this.tabPage4.Controls.Add(this.listaRacuna);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(674, 581);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Moji Računi";
            // 
            // labelaSuma
            // 
            this.labelaSuma.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelaSuma.AutoSize = true;
            this.labelaSuma.BackColor = System.Drawing.Color.Teal;
            this.labelaSuma.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelaSuma.Location = new System.Drawing.Point(3, 561);
            this.labelaSuma.Name = "labelaSuma";
            this.labelaSuma.Size = new System.Drawing.Size(272, 17);
            this.labelaSuma.TabIndex = 2;
            this.labelaSuma.Text = "Suma računa koje sam ostvario:  KM";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Teal;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(195, 17);
            this.label5.TabIndex = 1;
            this.label5.Text = "Računi koje sam ostvario:";
            // 
            // listaRacuna
            // 
            this.listaRacuna.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listaRacuna.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.listaRacuna.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold);
            this.listaRacuna.FormattingEnabled = true;
            this.listaRacuna.ItemHeight = 18;
            this.listaRacuna.Location = new System.Drawing.Point(6, 19);
            this.listaRacuna.Name = "listaRacuna";
            this.listaRacuna.Size = new System.Drawing.Size(662, 544);
            this.listaRacuna.TabIndex = 0;
            // 
            // labelaClanarine
            // 
            this.labelaClanarine.AutoSize = true;
            this.labelaClanarine.Font = new System.Drawing.Font("Harlow Solid Italic", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelaClanarine.Location = new System.Drawing.Point(10, 356);
            this.labelaClanarine.Name = "labelaClanarine";
            this.labelaClanarine.Size = new System.Drawing.Size(205, 60);
            this.labelaClanarine.TabIndex = 17;
            this.labelaClanarine.Text = "Aplikaciju možete\r\nkoristiti do:\r\n";
            this.labelaClanarine.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // btnPodesavanja
            // 
            this.btnPodesavanja.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPodesavanja.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnPodesavanja.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPodesavanja.Location = new System.Drawing.Point(12, 625);
            this.btnPodesavanja.Name = "btnPodesavanja";
            this.btnPodesavanja.Size = new System.Drawing.Size(213, 27);
            this.btnPodesavanja.TabIndex = 18;
            this.btnPodesavanja.Text = "Podešavanja naloga";
            this.btnPodesavanja.UseVisualStyleBackColor = false;
            this.btnPodesavanja.Click += new System.EventHandler(this.btnPodesavanja_Click);
            // 
            // btnProduzi
            // 
            this.btnProduzi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnProduzi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnProduzi.Enabled = false;
            this.btnProduzi.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnProduzi.Location = new System.Drawing.Point(12, 592);
            this.btnProduzi.Margin = new System.Windows.Forms.Padding(0);
            this.btnProduzi.Name = "btnProduzi";
            this.btnProduzi.Size = new System.Drawing.Size(213, 27);
            this.btnProduzi.TabIndex = 19;
            this.btnProduzi.Text = "Produži članarinu";
            this.btnProduzi.UseVisualStyleBackColor = false;
            this.btnProduzi.Click += new System.EventHandler(this.btnProduzi_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox1.BackgroundImage = global::WFA_Biblioteka.Properties.Resources.OpenBook;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(12, 417);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(213, 136);
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // timerRefreser
            // 
            this.timerRefreser.Interval = 3000;
            this.timerRefreser.Tick += new System.EventHandler(this.timerRefreser_Tick);
            // 
            // btnPrimjedba
            // 
            this.btnPrimjedba.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPrimjedba.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnPrimjedba.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPrimjedba.Location = new System.Drawing.Point(12, 556);
            this.btnPrimjedba.Margin = new System.Windows.Forms.Padding(0);
            this.btnPrimjedba.Name = "btnPrimjedba";
            this.btnPrimjedba.Size = new System.Drawing.Size(213, 27);
            this.btnPrimjedba.TabIndex = 20;
            this.btnPrimjedba.Text = "Primjedbe";
            this.btnPrimjedba.UseVisualStyleBackColor = false;
            this.btnPrimjedba.Click += new System.EventHandler(this.btnPrimjedba_Click);
            // 
            // UlogovanoStanje2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Teal;
            this.ClientSize = new System.Drawing.Size(925, 664);
            this.Controls.Add(this.btnPrimjedba);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnProduzi);
            this.Controls.Add(this.btnPodesavanja);
            this.Controls.Add(this.labelaClanarine);
            this.Controls.Add(this.tabovi);
            this.Controls.Add(this.btnDodaj);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.labelKorisnik);
            this.Controls.Add(this.label9);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "UlogovanoStanje2";
            this.Text = "UlogovanoStanje";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UlogovanoStanje2_FormClosing);
            this.tabovi.ResumeLayout(false);
            this.tabSveKnjige.ResumeLayout(false);
            this.tabSveKnjige.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label labelKorisnik;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Button btnDodaj;
        private System.Windows.Forms.ListBox prikazi;
        private System.Windows.Forms.TabControl tabovi;
        private System.Windows.Forms.TabPage tabSveKnjige;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Label labelaClanarine;
        private System.Windows.Forms.ListBox listaKorpa;
        private System.Windows.Forms.Button btnIznajmi;
        private System.Windows.Forms.ListBox listaIznajmljeneKnjige;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox cbNijeIzdato;
        private System.Windows.Forms.CheckBox cbIzdato;
        private System.Windows.Forms.ComboBox comboPrikaz;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnIzbaci;
        private System.Windows.Forms.Label labelaSuma;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListBox listaRacuna;
        private System.Windows.Forms.Button btnPodesavanja;
        private System.Windows.Forms.Button btnProduzi;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Timer timerRefreser;
        private System.Windows.Forms.Button btnPrimjedba;
    }
}