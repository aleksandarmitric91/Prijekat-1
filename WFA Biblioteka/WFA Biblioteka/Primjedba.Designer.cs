namespace WFA_Biblioteka
{
    partial class Primjedba
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
            this.btnIzvrsi = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnPosaljiPrimjedbu = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelKorisnik = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tbNovaPrimjedba = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbPrepiska = new System.Windows.Forms.RichTextBox();
            this.btnObrisi = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnIzvrsi
            // 
            this.btnIzvrsi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnIzvrsi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnIzvrsi.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnIzvrsi.Location = new System.Drawing.Point(231, 592);
            this.btnIzvrsi.Name = "btnIzvrsi";
            this.btnIzvrsi.Size = new System.Drawing.Size(136, 23);
            this.btnIzvrsi.TabIndex = 4;
            this.btnIzvrsi.Text = "Otkaži slanje primjedbe";
            this.btnIzvrsi.UseVisualStyleBackColor = false;
            this.btnIzvrsi.Click += new System.EventHandler(this.btnIzvrsi_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.label1.Font = new System.Drawing.Font("Harlow Solid Italic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(226, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(223, 30);
            this.label1.TabIndex = 3;
            this.label1.Text = "Prethodna prepiska:";
            // 
            // btnPosaljiPrimjedbu
            // 
            this.btnPosaljiPrimjedbu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPosaljiPrimjedbu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnPosaljiPrimjedbu.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPosaljiPrimjedbu.Location = new System.Drawing.Point(844, 592);
            this.btnPosaljiPrimjedbu.Name = "btnPosaljiPrimjedbu";
            this.btnPosaljiPrimjedbu.Size = new System.Drawing.Size(125, 23);
            this.btnPosaljiPrimjedbu.TabIndex = 5;
            this.btnPosaljiPrimjedbu.Text = "Pošalji primjedbu";
            this.btnPosaljiPrimjedbu.UseVisualStyleBackColor = false;
            this.btnPosaljiPrimjedbu.Click += new System.EventHandler(this.btnPosaljiPrimjedbu_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox1.BackgroundImage = global::WFA_Biblioteka.Properties.Resources.OpenBook;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(12, 479);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(213, 136);
            this.pictureBox1.TabIndex = 20;
            this.pictureBox1.TabStop = false;
            // 
            // labelKorisnik
            // 
            this.labelKorisnik.AutoSize = true;
            this.labelKorisnik.Font = new System.Drawing.Font("Harlow Solid Italic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelKorisnik.Location = new System.Drawing.Point(10, 228);
            this.labelKorisnik.Name = "labelKorisnik";
            this.labelKorisnik.Size = new System.Drawing.Size(199, 30);
            this.labelKorisnik.TabIndex = 19;
            this.labelKorisnik.Text = "Ulogovani ste kao:\r\n";
            this.labelKorisnik.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Harlow Solid Italic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(12, 9);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(213, 180);
            this.label9.TabIndex = 18;
            this.label9.Text = "Bibliteka Fortuna\r\nBraće Jugovića 55\r\nZvornik\r\n\r\nKontakt telefon: \r\n065/112-112\r\n" +
    "";
            // 
            // tbNovaPrimjedba
            // 
            this.tbNovaPrimjedba.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbNovaPrimjedba.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.tbNovaPrimjedba.Location = new System.Drawing.Point(828, 41);
            this.tbNovaPrimjedba.MaxLength = 5000;
            this.tbNovaPrimjedba.Name = "tbNovaPrimjedba";
            this.tbNovaPrimjedba.Size = new System.Drawing.Size(141, 545);
            this.tbNovaPrimjedba.TabIndex = 21;
            this.tbNovaPrimjedba.Text = "";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.label2.Font = new System.Drawing.Font("Harlow Solid Italic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(823, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(196, 30);
            this.label2.TabIndex = 22;
            this.label2.Text = "Nova Primjedba:";
            // 
            // tbPrepiska
            // 
            this.tbPrepiska.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tbPrepiska.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.tbPrepiska.Location = new System.Drawing.Point(231, 41);
            this.tbPrepiska.MaxLength = 20000;
            this.tbPrepiska.Name = "tbPrepiska";
            this.tbPrepiska.ReadOnly = true;
            this.tbPrepiska.Size = new System.Drawing.Size(591, 545);
            this.tbPrepiska.TabIndex = 23;
            this.tbPrepiska.Text = "";
            // 
            // btnObrisi
            // 
            this.btnObrisi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnObrisi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnObrisi.Enabled = false;
            this.btnObrisi.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnObrisi.Location = new System.Drawing.Point(373, 592);
            this.btnObrisi.Name = "btnObrisi";
            this.btnObrisi.Size = new System.Drawing.Size(146, 23);
            this.btnObrisi.TabIndex = 24;
            this.btnObrisi.Text = "Obriši prethodnu prepisku";
            this.btnObrisi.UseVisualStyleBackColor = false;
            this.btnObrisi.Click += new System.EventHandler(this.btnObrisi_Click);
            // 
            // Primjedba
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Teal;
            this.ClientSize = new System.Drawing.Size(981, 627);
            this.Controls.Add(this.btnObrisi);
            this.Controls.Add(this.tbPrepiska);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbNovaPrimjedba);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.labelKorisnik);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnPosaljiPrimjedbu);
            this.Controls.Add(this.btnIzvrsi);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Primjedba";
            this.Text = "Primjedba";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnIzvrsi;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnPosaljiPrimjedbu;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label labelKorisnik;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.RichTextBox tbNovaPrimjedba;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox tbPrepiska;
        private System.Windows.Forms.Button btnObrisi;
    }
}