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
    public partial class Podesavanja : Form
    {
        int idKorisnici, idAdresa, idClanBiblioteka;
        static string conString = "Server=localhost;Database=biblioteka;Uid=root;";
        MySqlConnection konekcija = new MySqlConnection(conString);
        MySqlConnection konekcija2 = new MySqlConnection(conString);

        public Podesavanja(int idKorisnika)
        {
            this.idKorisnici=idKorisnika;
            InitializeComponent();

            konekcija.Open();
            var komanda = konekcija.CreateCommand();
            komanda.CommandText = "SELECT brojTelefona,password,Adresa_idAdresa FROM korisnici WHERE idKorisnici="+idKorisnici+"";
            var rezultat = komanda.ExecuteReader();
            rezultat.Read();

            tbBrojTelefona.Text = rezultat["brojTelefona"].ToString();
            tbPassword.Text = rezultat["password"].ToString();
            string idAdresa = rezultat["Adresa_idAdresa"].ToString();
            konekcija.Close();

            konekcija.Open();
            var komanda1 = konekcija.CreateCommand();
            komanda1.CommandText = "SELECT Grad,Ulica,Broj FROM adresa WHERE idAdresa=" + idAdresa + "";
            var rezultat1 = komanda1.ExecuteReader();
            rezultat1.Read();
            tbGrad.Text = rezultat1["Grad"].ToString();
            tbUlica.Text = rezultat1["Ulica"].ToString();
            tbBroj.Text = rezultat1["Broj"].ToString();
            konekcija.Close();
        }

        private void btnOtkaziZahtjev_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnProvjeriUnos_Click(object sender, EventArgs e)//ima greska 
        {
            if (cbDeaktiviraj.Checked == true)
            {
                konekcija.Open();
                var komanda1 = konekcija.CreateCommand();
                komanda1.CommandText = "SELECT * FROM clanbiblioteke WHERE Korisnici_idKorisnici=" + idKorisnici + "";
                var citac1 = komanda1.ExecuteReader();
                citac1.Read();
                this.idClanBiblioteka = Convert.ToInt16(citac1["idClanaBiblioteke"].ToString());
                konekcija.Close();

                konekcija.Open();
                var komanda2 = konekcija.CreateCommand();
                komanda2.CommandText = "UPDATE knjiga SET izdato=" + false + ",ClanBiblioteke_idClanaBiblioteke=NULL WHERE ClanBiblioteke_idClanaBiblioteke=" + idClanBiblioteka + "";
                komanda2.ExecuteNonQuery();
                konekcija.Close();

                konekcija.Open();
                var komanda3 = konekcija.CreateCommand();
                komanda3.CommandText = "DELETE FROM racun WHERE ClanBiblioteke_idClanaBiblioteke=" + idClanBiblioteka + "";
                komanda3.ExecuteNonQuery();
                konekcija.Close();

                konekcija.Open();
                var komanda4 = konekcija.CreateCommand();
                komanda4.CommandText = "DELETE FROM clanbiblioteke WHERE idClanaBiblioteke=" + idClanBiblioteka + "";
                komanda4.ExecuteNonQuery();
                konekcija.Close();

                konekcija.Open();
                var komanda5 = konekcija.CreateCommand();
                komanda5.CommandText = "SELECT * FROM korisnici WHERE idKorisnici=" + idKorisnici + "";
                var citac5 = komanda5.ExecuteReader();
                citac5.Read();
                idAdresa = Convert.ToInt16(citac5["Adresa_idAdresa"].ToString());
                konekcija.Close();

                konekcija.Open();
                var komanda6 = konekcija.CreateCommand();
                komanda6.CommandText = "DELETE FROM korisnici WHERE idKorisnici=" + idKorisnici + "";
                komanda6.ExecuteNonQuery();
                konekcija.Close();

                konekcija.Open();
                var komanda7 = konekcija.CreateCommand();
                komanda7.CommandText = "DELETE FROM adresa WHERE idAdresa=" + idAdresa + "";
                komanda7.ExecuteNonQuery();
                konekcija.Close();

                for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
                {
                    if (Application.OpenForms[i].Name != "Form1")
                        Application.OpenForms[i].Close();
                }
                MessageBox.Show("Vaš nalog je uspješno obrisan!","Informacija");
            }
            else
            {
                try
                {
                    konekcija.Open();
                    var komanda1 = konekcija.CreateCommand();
                    komanda1.CommandText = "SELECT Adresa_idAdresa FROM korisnici WHERE idKorisnici=" + idKorisnici + "";
                    var citac1 = komanda1.ExecuteReader();
                    citac1.Read();
                    this.idAdresa = Convert.ToInt16(citac1["Adresa_idAdresa"].ToString());
                    konekcija.Close();

                    konekcija.Open();
                    var komanda = konekcija.CreateCommand();
                    komanda.CommandText = "UPDATE adresa SET Grad='" + tbGrad.Text + "',Ulica='" + tbUlica.Text + " ',Broj='" + tbBroj.Text + "' WHERE idAdresa=" + this.idAdresa + "";
                    komanda.ExecuteNonQuery();
                    konekcija.Close();

                    konekcija.Open();
                    var komanda2 = konekcija.CreateCommand();
                    if (tbPassword.Enabled == false)
                    {
                        komanda2.CommandText = "UPDATE korisnici SET brojTelefona='" + tbBrojTelefona.Text + "' WHERE idKorisnici=" + this.idKorisnici + "";
                        MessageBox.Show("Sve informacije o korisniku osim passworda će biti izmenjene.", "Informacija");
                    }
                    else
                    {
                        komanda2.CommandText = "UPDATE korisnici SET brojTelefona='" + tbBrojTelefona.Text + "',password='" + tbNoviPassword.Text + "',salt=MD5('" + tbBrojTelefona.Text + "') WHERE idKorisnici=" + this.idKorisnici + "";
                        MessageBox.Show("Sve informacije o korisniku će biti izmenjene.", "Informacija");
                    }
                    komanda2.ExecuteNonQuery();
                    konekcija.Close();
                    this.Close();
                }
                catch
                {
                    MessageBox.Show("Unesite broj u polje Novi broj.", "Greška");
                    tbBroj.Text = "";
                }
            }
        }

        private void tbNoviPasswordPotvrda_TextChanged(object sender, EventArgs e)
        {
            if (tbNoviPassword.TextLength > 0 && tbNoviPasswordPotvrda.TextLength > 0 && tbNoviPasswordPotvrda.Text == tbNoviPassword.Text)
            {
                tbPassword.Enabled = true;
            }
            else
            {
                tbPassword.Enabled = false;
            }
        }

        private void tbNoviPassword_TextChanged(object sender, EventArgs e)
        {
            if (tbNoviPassword.TextLength > 0 && tbNoviPasswordPotvrda.TextLength > 0 && tbNoviPasswordPotvrda.Text == tbNoviPassword.Text)
            {
                tbPassword.Enabled = true;
            }
            else
            {
                tbPassword.Enabled = false;
            }
        }
    }
}
