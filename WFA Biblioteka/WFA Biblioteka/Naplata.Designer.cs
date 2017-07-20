namespace WFA_Biblioteka
{
    partial class Naplata
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
            this.label1 = new System.Windows.Forms.Label();
            this.listaNenaplaceno = new System.Windows.Forms.ListBox();
            this.btnIzvrsi = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.label1.Font = new System.Drawing.Font("Harlow Solid Italic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(169, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(224, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nenaplaćeni računi";
            // 
            // listaNenaplaceno
            // 
            this.listaNenaplaceno.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.listaNenaplaceno.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listaNenaplaceno.FormattingEnabled = true;
            this.listaNenaplaceno.ItemHeight = 18;
            this.listaNenaplaceno.Location = new System.Drawing.Point(12, 41);
            this.listaNenaplaceno.Name = "listaNenaplaceno";
            this.listaNenaplaceno.Size = new System.Drawing.Size(523, 418);
            this.listaNenaplaceno.TabIndex = 1;
            this.listaNenaplaceno.SelectedIndexChanged += new System.EventHandler(this.listaNenaplaceno_SelectedIndexChanged);
            // 
            // btnIzvrsi
            // 
            this.btnIzvrsi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnIzvrsi.Enabled = false;
            this.btnIzvrsi.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnIzvrsi.Location = new System.Drawing.Point(220, 465);
            this.btnIzvrsi.Name = "btnIzvrsi";
            this.btnIzvrsi.Size = new System.Drawing.Size(101, 23);
            this.btnIzvrsi.TabIndex = 2;
            this.btnIzvrsi.Text = "Izvrši naplatu";
            this.btnIzvrsi.UseVisualStyleBackColor = false;
            this.btnIzvrsi.Click += new System.EventHandler(this.btnIzvrsi_Click);
            // 
            // Naplata
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Teal;
            this.ClientSize = new System.Drawing.Size(547, 503);
            this.Controls.Add(this.btnIzvrsi);
            this.Controls.Add(this.listaNenaplaceno);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Naplata";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Naplata";
            this.TopMost = true;
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Naplata_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Naplata_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Naplata_MouseUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listaNenaplaceno;
        private System.Windows.Forms.Button btnIzvrsi;
    }
}