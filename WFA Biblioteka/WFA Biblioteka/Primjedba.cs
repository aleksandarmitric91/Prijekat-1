using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WFA_Biblioteka
{
    public partial class Primjedba : Form
    {
        static string conString = "Server=localhost;Database=biblioteka;Uid=root;";
        MySqlConnection konekcija = new MySqlConnection(conString);
        int idClanaBibliteke;
        string prepiska, ime, prezime;

        public Primjedba(int idClana,int idKorisnik, string ime, string prezime)
        {
            InitializeComponent();
            labelKorisnik.Text = "Ulogovan korisnik:\n" + ime + " " + prezime + "\nkao član biblioteke.";
            this.idClanaBibliteke = idClana;
            this.ime = ime;
            this.prezime = prezime;

            konekcija.Open();
            var komanda = konekcija.CreateCommand();
            komanda.CommandText = "SELECT PrijavaNeregularnosti FROM clanbiblioteke WHERE idClanaBiblioteke=" + idClanaBibliteke + "";
            var citac=komanda.ExecuteReader();
            citac.Read();
            tbPrepiska.Text = citac["PrijavaNeregularnosti"].ToString();
            this.prepiska = tbPrepiska.Text;
            konekcija.Close();

            if (tbPrepiska.TextLength > 0)
            {
                btnObrisi.Enabled = true;
            }
            else
            {
                btnObrisi.Enabled = false;
            }
        }

        private void btnIzvrsi_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPosaljiPrimjedbu_Click(object sender, EventArgs e)
        {
            if (tbNovaPrimjedba.TextLength > 1)
            {
                konekcija.Open();
                var komanda = konekcija.CreateCommand();
                prepiska = prepiska + "(" + DateTime.Now + ") " + ime + " " + prezime + " je napisao:\n\t" + tbNovaPrimjedba.Text;
                komanda.CommandText = "UPDATE clanbiblioteke SET PrijavaNeregularnosti='" + prepiska + "\n\n' WHERE idClanaBiblioteke=" + idClanaBibliteke + "";
                komanda.ExecuteNonQuery();
                konekcija.Close();
                MessageBox.Show("Uspješno ste ostavili primjedbu.", "Poruka");
                this.Close();
            }
            else 
            {
                MessageBox.Show("Molimo unesite tekst primjedbe.", "Informacija");
            }
        }

        private void btnObrisi_Click(object sender, EventArgs e)
        {
            konekcija.Open();
            var komanda = konekcija.CreateCommand();
            prepiska = "";
            komanda.CommandText = "UPDATE clanbiblioteke SET PrijavaNeregularnosti='" + prepiska + "' WHERE idClanaBiblioteke=" + idClanaBibliteke + "";
            komanda.ExecuteNonQuery();
            konekcija.Close();
            MessageBox.Show("Uspješno ste obrisali primjedbe.", "Poruka");
            
            tbPrepiska.Clear();
            prepiska = "";
            btnObrisi.Enabled = false;
        }
    }
}
