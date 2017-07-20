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
    public partial class Naplata : Form
    {
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

        static string conString = "Server=localhost;Database=biblioteka;Uid=root;";
        MySqlConnection konekcija = new MySqlConnection(conString);

        public Naplata()
        {
            InitializeComponent();

            listaNenaplaceno.Items.Clear();
            listaNenaplaceno.Items.Add("ID RAČUN\tIME\tPREZIEM\t\tZA NAPLATIT");
            listaNenaplaceno.Items.Add("");

            konekcija.Open();
            var komanda = konekcija.CreateCommand();
            komanda.CommandText = "SELECT * FROM racun r, clanbiblioteke c, korisnici k WHERE r.naplaceno=false and r.ClanBiblioteke_idClanaBiblioteke=c.idClanaBiblioteke and k.idKorisnici=c.Korisnici_idKorisnici";
            var citac = komanda.ExecuteReader();
            while (citac.Read())
            {
                string unos = citac["idracun"].ToString() + "\t\t" + citac["imeKorisnika"].ToString() + "\t" + citac["prezimeKorisnika"].ToString() + "\t\t" + citac["zaNaplatu"].ToString();
                listaNenaplaceno.Items.Add(unos);
            }
            konekcija.Close();
        }

        private void listaNenaplaceno_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listaNenaplaceno.SelectedIndex > 1)
            {
                btnIzvrsi.Enabled = true;
            }
            else
            {
                btnIzvrsi.Enabled = false;
            }
        }

        private void Naplata_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void Naplata_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void Naplata_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void btnIzvrsi_Click(object sender, EventArgs e)
        {
            string idRacuna = listaNenaplaceno.SelectedItem.ToString();
            idRacuna = idRacuna.Substring(0, idRacuna.IndexOf("\t"));

            konekcija.Open();
            var komanda = konekcija.CreateCommand();
            komanda.CommandText = "UPDATE racun SET naplaceno=" + true + " WHERE idracun=" + idRacuna + "";
            komanda.ExecuteNonQuery();
            konekcija.Close();

            listaNenaplaceno.Items.RemoveAt(listaNenaplaceno.SelectedIndex);

            if (listaNenaplaceno.Items.Count < 3)
            {
                this.Close();
            }
        }
    }
}
