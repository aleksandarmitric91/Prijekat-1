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
    public partial class NoviKorisnik : Form
    {
        static string conString = "Server=localhost;Database=biblioteka;Uid=root;";
        MySqlConnection konekcija = new MySqlConnection(conString);
        MySqlConnection konekcija2 = new MySqlConnection(conString);

        public NoviKorisnik()
        {
            InitializeComponent();
            timer1.Start();
        }

        private void btnOtkaziZahtjev_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnProvjeriUnos_Click(object sender, EventArgs e)
        {
            try
            {
                Convert.ToInt16(tbBroj.Text.ToString());
                try
                {
                    konekcija.Open();
                    konekcija2.Open();
                    var komanda = konekcija2.CreateCommand();
                    var komanda1 = konekcija.CreateCommand();
                    var komanda2 = konekcija.CreateCommand();
                    int tipKorisnika;
                    if (rbAdmin.Checked == true)
                    {
                        tipKorisnika = 0;
                    }
                    else if (rbBiblitekar.Checked == true)
                    {
                        tipKorisnika = 1;
                    }
                    else
                    {
                        tipKorisnika = 2;
                    }
                    komanda1.CommandText = "INSERT INTO adresa(Grad,Ulica,Broj) VALUES ('" + tbGrad.Text + "','" + tbUlica.Text + "','" + tbBroj.Text + "')";
                    komanda1.ExecuteNonQuery();
                    komanda2.CommandText = "SELECT idAdresa FROM adresa WHERE Grad='" + tbGrad.Text + "' && Ulica='" + tbUlica.Text + "' && Broj='" + tbBroj.Text + "'";
                    var rezultat = komanda2.ExecuteReader();
                    rezultat.Read();
                    int idAdresa = Convert.ToInt16(rezultat["idAdresa"].ToString());
                    try
                    {
                        komanda.CommandText = "INSERT INTO korisnici(imeKorisnika,prezimeKorisnika,brojTelefona,username,password,salt," +
                           "tipKorisnika,dodatniDetaljiOKorisniku,Adresa_idAdresa,ulogovan) VALUES ('" + tbIme.Text + "','" +
                           tbPrezime.Text + "','" + tbBrojTelefona.Text + "','" + tbUsername.Text + "','" + tbPassword.Text + "',MD5('" +
                           tbPassword.Text + "')," + tipKorisnika + ",'Zahtjev za novi nalog'," + idAdresa + "," + false + ")";
                        komanda.ExecuteNonQuery();
                        MessageBox.Show("Zahtjev za novi nalog je poslat.", "Informacija");
                        this.Close();
                    }
                    catch
                    {
                        komanda.CommandText = "DELETE FROM adresa WHERE idAdresa='"+idAdresa+"'";
                        komanda.ExecuteNonQuery();
                        MessageBox.Show("Korisničko ime već postoji u bazi. Izaberite drugo ime.", "Obaveštenje");
                    }
                }
                catch
                {
                    MessageBox.Show("Podatci koje ste uneli već postoje u bazi podataka.", "Obaveštenje");
                }
            }
            catch
            {
                MessageBox.Show("Unesite broj u polje Broj u sastavu Adrese.", "Greška");
                tbBroj.Text = "";
            }
            finally
            {
                konekcija.Close();
                konekcija2.Close();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (tbIme.TextLength > 0 && tbPrezime.TextLength > 0 && tbBrojTelefona.TextLength > 0 && tbUsername.TextLength > 0 && tbPassword.TextLength > 0 && tbGrad.TextLength > 0 && tbUlica.TextLength > 0 && tbBroj.TextLength > 0)
            {
                btnPosaljiZahtev.Enabled = true;
            }
            else
            {
                btnPosaljiZahtev.Enabled = false;
            }
        }
    }
}
