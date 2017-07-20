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
    public partial class Form1 : Form
    {
        static string conString = "Server=localhost;Database=biblioteka;Uid=root;";
        MySqlConnection konekcija = new MySqlConnection(conString);
        MySqlConnection konekcija2 = new MySqlConnection(conString);

        public Form1()
        {
            InitializeComponent();
            konekcija.Open();
            var komanda = konekcija.CreateCommand();
            komanda.CommandText = "SELECT * FROM knjiga";
            var citac = komanda.ExecuteReader();
            lista.Items.Add("ID\t\tNAZIV\t\t\t\tCIJENA\t\tGODINA\t\tIZNAJMLJENO");
            lista.Items.Add("");
            while (citac.Read())
            {
                string izn="Nije iznajmljena";
                if(citac["izdato"].ToString().ToLower()=="true")
                {
                    izn = "Iznajmljena";
                }
                string unos = citac["idKnjige"].ToString() + "\t\t" + citac["naslovKnjige"].ToString() + "\t\t\t\t" + citac["cenaIznajmljivanja"].ToString() + "\t\t" + citac["godinaIzdavanja"].ToString() + "\t\t" + izn;
                lista.Items.Add(unos);
            }
            konekcija.Close();
        }

        private void linkNalog_MouseClick(object sender, MouseEventArgs e)
        {
            NoviKorisnik novi = new NoviKorisnik();
            novi.ShowDialog();
        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            konekcija.Open();
            var komanda = konekcija.CreateCommand();
            komanda.CommandText = "SELECT idKorisnici,imeKorisnika,prezimeKorisnika,tipKorisnika,password,dodatniDetaljiOKorisniku FROM korisnici WHERE username LIKE '" + tbUsername.Text + "'";
            var citac = komanda.ExecuteReader();
            
            if (citac.Read() == true)
            {
                if (citac["password"].ToString() == tbSifra.Text && citac["dodatniDetaljiOKorisniku"].ToString() != "Zahtjev za novi nalog")
                {
                    if (Convert.ToInt16(citac["tipKorisnika"].ToString()) == 0)
                    {
                        konekcija2.Open();
                        var komanda2 = konekcija2.CreateCommand();
                        komanda2.CommandText = "UPDATE korisnici SET ulogovan=" + true + " WHERE username='" + tbUsername.Text + "'";
                        komanda2.ExecuteNonQuery();
                        konekcija2.Close();

                        UlogovanoStanje stanje = new UlogovanoStanje(citac["imeKorisnika"].ToString(), Convert.ToInt16(citac["idKorisnici"].ToString()), citac["prezimeKorisnika"].ToString(), Convert.ToInt16(citac["tipKorisnika"].ToString()));
                        stanje.ShowDialog();
                    }
                    else if (Convert.ToInt16(citac["tipKorisnika"].ToString()) == 1)
                    {
                        konekcija2.Open();
                        var komanda2 = konekcija2.CreateCommand();
                        komanda2.CommandText = "UPDATE korisnici SET ulogovan=" + true + " WHERE username='" + tbUsername.Text + "'";
                        komanda2.ExecuteNonQuery();
                        konekcija2.Close();

                        UlogovanoStanje1 stanje = new UlogovanoStanje1(citac["imeKorisnika"].ToString(), Convert.ToInt16(citac["idKorisnici"].ToString()), citac["prezimeKorisnika"].ToString(), Convert.ToInt16(citac["tipKorisnika"].ToString()));
                        stanje.ShowDialog();
                    }
                    else
                    {
                        konekcija2.Open();
                        var komanda2 = konekcija2.CreateCommand();
                        komanda2.CommandText = "UPDATE korisnici SET ulogovan=" + true + " WHERE username='" + tbUsername.Text + "'";
                        komanda2.ExecuteNonQuery();
                        konekcija2.Close();

                        UlogovanoStanje2 stanje = new UlogovanoStanje2(citac["imeKorisnika"].ToString(), Convert.ToInt16(citac["idKorisnici"].ToString()), citac["prezimeKorisnika"].ToString(), Convert.ToInt16(citac["tipKorisnika"].ToString()));
                        stanje.ShowDialog();
                    }
                    konekcija.Close();



                    tbSifra.Text = "";
                    tbUsername.Text = "";
                    upozorenje.Text = "";
                }
                else if (citac["password"].ToString() == tbSifra.Text && citac["dodatniDetaljiOKorisniku"].ToString() == "Zahtjev za novi nalog")
                {
                    MessageBox.Show("Administrator nije još obradio vaše podatke. Pokušajete se ulogovati kasnije.","Obaveštenje");
                    tbSifra.Text = "";
                    upozorenje.Text = "";
                }
                else
                {
                    tbSifra.Text = "";
                    upozorenje.Text = "Pogrešan username ili password!";
                }
            }
            else
            {
                tbSifra.Text = "";
                upozorenje.Text = "Pogrešan username ili password!";
            }

            konekcija.Close();
        }

        private void tbSifra_TextChanged(object sender, EventArgs e)
        {
            upozorenje.Text = "";
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
            {
                if (Application.OpenForms[i].Name != "Form1")
                    Application.OpenForms[i].Close();
            }
        }

    }
}


            