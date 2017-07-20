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
    public partial class DodavanjeKorisnika : Form
    {
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        static string conString = "Server=localhost;Database=biblioteka;Uid=root;";
        MySqlConnection konekcija = new MySqlConnection(conString);
        MySqlConnection konekcija2 = new MySqlConnection(conString);

        public DodavanjeKorisnika()
        {
            InitializeComponent();

            pocetnoPopunjavanje();
        }

        private void btnOdobri_Click(object sender, EventArgs e)
        {
            string idKorisnika = listaZahtjevi.SelectedItem.ToString();
            idKorisnika = idKorisnika.Substring(0, idKorisnika.IndexOf("\t"));

            konekcija.Open();
            var komanda = konekcija.CreateCommand();
            komanda.CommandText = "SELECT * FROM korisnici WHERE idKorisnici=" + idKorisnika;
            var citac = komanda.ExecuteReader();
            while (citac.Read())
            {
                if (citac["tipKorisnika"].ToString() == "2")
                {
                    konekcija2.Open();
                    var komanda8 = konekcija2.CreateCommand();
                    komanda8.CommandText = "INSERT INTO clanbiblioteke(trajanjeClanarine, PrijavaNeregularnosti, Korisnici_idKorisnici) VALUES ('"+(DateTime.Now.ToString("u").Replace("/","-")).Replace("2014","2010")+"','',"+idKorisnika+")";
                    komanda8.ExecuteNonQuery();
                    konekcija2.Close();
                }
            }
            konekcija.Close();

            konekcija.Open();
            var komanda7 = konekcija.CreateCommand();
            komanda7.CommandText = "UPDATE korisnici SET dodatniDetaljiOKorisniku='' WHERE idKorisnici=" + idKorisnika;
            komanda7.ExecuteNonQuery();
            konekcija.Close();

            btnOdobri.Enabled = false;
            btnOdbij.Enabled = false;

            pocetnoPopunjavanje();

            if (listaZahtjevi.Items.Count < 3)
            {
                this.Close();
            }
        }

        private void DodavanjeKorisnika_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void DodavanjeKorisnika_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void DodavanjeKorisnika_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void pocetnoPopunjavanje()
        {
            listaZahtjevi.Items.Clear();

            listaZahtjevi.Items.Add("ID KORISNIKA\tIME I PREZIME\tBROJ TELEFONA\tUSERNAME\tPASSWORD\tTIP KORISNIKA\tADRESA");
            listaZahtjevi.Items.Add("");

            konekcija.Open();
            var komanda7 = konekcija.CreateCommand();
            komanda7.CommandText = "SELECT * FROM korisnici k,adresa a WHERE k.Adresa_idAdresa=a.idAdresa AND dodatniDetaljiOKorisniku='Zahtjev za novi nalog'";
            var citac7 = komanda7.ExecuteReader();
            while (citac7.Read())
            {
                string tipKorisnika, stanje, dodatak;
                if (citac7["tipKorisnika"].ToString() == "0")
                {
                    tipKorisnika = "Administrator";
                }
                else if (citac7["tipKorisnika"].ToString() == "1")
                {
                    tipKorisnika = "Bibliotekar";
                }
                else
                {
                    tipKorisnika = "Član bibilioteke";
                }
                string unos = citac7["idKorisnici"].ToString() + "\t\t" + citac7["imeKorisnika"].ToString() + " " + citac7["prezimeKorisnika"].ToString() + "\t" + citac7["brojTelefona"].ToString() + "\t" + citac7["username"].ToString() + "\t\t" + citac7["password"].ToString() + "\t\t" + tipKorisnika + "\t" + citac7["Grad"].ToString() + ", " + citac7["Ulica"].ToString() + " " + citac7["Broj"].ToString();
                listaZahtjevi.Items.Add(unos);
            }
            konekcija.Close();
        }

        private void listaZahtjevi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listaZahtjevi.SelectedIndex > 1)
            {
                btnOdbij.Enabled = true;
                btnOdobri.Enabled = true;
            }
            else
            {
                btnOdbij.Enabled = false;
                btnOdobri.Enabled = false;
            }
        }

        private void btnOdbij_Click(object sender, EventArgs e)
        {
            string idKorisnika = listaZahtjevi.SelectedItem.ToString();
            idKorisnika = idKorisnika.Substring(0, idKorisnika.IndexOf("\t"));

            konekcija.Open();
            var komanda = konekcija.CreateCommand();
            komanda.CommandText = "DELETE FROM korisnici WHERE idKorisnici=" + idKorisnika;
            komanda.ExecuteNonQuery();
            konekcija.Close();

            btnOdobri.Enabled = false;
            btnOdbij.Enabled = false;

            pocetnoPopunjavanje();

            if (listaZahtjevi.Items.Count < 3)
            {
                this.Close();
            }
        }

    }
}
