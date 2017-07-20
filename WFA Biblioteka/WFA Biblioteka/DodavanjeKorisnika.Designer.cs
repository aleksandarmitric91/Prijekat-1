namespace WFA_Biblioteka
{
    partial class DodavanjeKorisnika
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
            this.btnOdobri = new System.Windows.Forms.Button();
            this.btnOdbij = new System.Windows.Forms.Button();
            this.listaZahtjevi = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnOdobri
            // 
            this.btnOdobri.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnOdobri.Enabled = false;
            this.btnOdobri.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOdobri.Location = new System.Drawing.Point(286, 303);
            this.btnOdobri.Name = "btnOdobri";
            this.btnOdobri.Size = new System.Drawing.Size(120, 23);
            this.btnOdobri.TabIndex = 0;
            this.btnOdobri.Text = "Odobri pristup";
            this.btnOdobri.UseVisualStyleBackColor = false;
            this.btnOdobri.Click += new System.EventHandler(this.btnOdobri_Click);
            // 
            // btnOdbij
            // 
            this.btnOdbij.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnOdbij.Enabled = false;
            this.btnOdbij.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOdbij.Location = new System.Drawing.Point(422, 303);
            this.btnOdbij.Name = "btnOdbij";
            this.btnOdbij.Size = new System.Drawing.Size(115, 23);
            this.btnOdbij.TabIndex = 1;
            this.btnOdbij.Text = "Odbij zahtjev";
            this.btnOdbij.UseVisualStyleBackColor = false;
            this.btnOdbij.Click += new System.EventHandler(this.btnOdbij_Click);
            // 
            // listaZahtjevi
            // 
            this.listaZahtjevi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.listaZahtjevi.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listaZahtjevi.FormattingEnabled = true;
            this.listaZahtjevi.HorizontalExtent = 1350;
            this.listaZahtjevi.HorizontalScrollbar = true;
            this.listaZahtjevi.ItemHeight = 18;
            this.listaZahtjevi.Location = new System.Drawing.Point(12, 41);
            this.listaZahtjevi.Name = "listaZahtjevi";
            this.listaZahtjevi.Size = new System.Drawing.Size(816, 256);
            this.listaZahtjevi.TabIndex = 2;
            this.listaZahtjevi.SelectedIndexChanged += new System.EventHandler(this.listaZahtjevi_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.label1.Font = new System.Drawing.Font("Harlow Solid Italic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(281, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(256, 30);
            this.label1.TabIndex = 3;
            this.label1.Text = "Zahtjevi za registraciju";
            // 
            // DodavanjeKorisnika
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Teal;
            this.ClientSize = new System.Drawing.Size(840, 336);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listaZahtjevi);
            this.Controls.Add(this.btnOdbij);
            this.Controls.Add(this.btnOdobri);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "DodavanjeKorisnika";
            this.Text = "DodavanjeKorisnika";
            this.TopMost = true;
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DodavanjeKorisnika_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DodavanjeKorisnika_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DodavanjeKorisnika_MouseUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOdobri;
        private System.Windows.Forms.Button btnOdbij;
        private System.Windows.Forms.ListBox listaZahtjevi;
        private System.Windows.Forms.Label label1;
    }
}