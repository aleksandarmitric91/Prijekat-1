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
using MySql.Data.Types;

namespace WFA_Biblioteka
{
    public partial class UlogovanoStanje2 : Form
    {
        public int idKorisnici, idClanaBibliteke, prethodniBrojIznajmljenihKnjiga, prethodniBrojRacuna;
        public string ime, prezime;
        static string conString = "Server=localhost;Database=biblioteka;Uid=root;";
        static string conString1 = "Server=localhost;Database=biblioteka;Uid=root;Convert Zero Datetime=True";
        MySqlConnection konekcija = new MySqlConnection(conString);
        MySqlConnection konekcija1 = new MySqlConnection(conString);
        MySqlConnection konekcija2 = new MySqlConnection(conString1);

        public UlogovanoStanje2(string ime,int id,string prezime,int tip)
        {
            this.idKorisnici = id;
            this.ime = ime;
            this.prezime = prezime;
            InitializeComponent();
            PocetnoPopunjavanje();
            if (btnProduzi.Enabled == true)
            {
                tabovi.Enabled = false;
                MessageBox.Show("Molimo Vas da produžite članarinu kako bi mogli nastaviti rad na sistemu.","Poruka");
            }
            timerRefreser.Start();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            konekcija.Open();
            var komanda = konekcija.CreateCommand();
            komanda.CommandText = "UPDATE korisnici SET ulogovan='" + false + "' WHERE idKorisnici=" + idKorisnici;
            komanda.ExecuteNonQuery();
            konekcija.Close();
            this.Close();

        }

        private void prikazi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (prikazi.SelectedIndex > 1)
            {
                btnDodaj.Enabled = true;
            }
            else
            {
                btnDodaj.Enabled = false;
            }
        }

        private void btnDodaj_Click(object sender, EventArgs e)
        {
            string podatci = prikazi.SelectedItem.ToString();
            string idknjige = podatci.Substring(0, podatci.IndexOf("\t"));

            konekcija.Open();
            var komanda = konekcija.CreateCommand();
            komanda.CommandText = "SELECT * FROM knjiga WHERE idKnjige="+idknjige+"";
            var citac = komanda.ExecuteReader();
            citac.Read();
            bool postoji = false;
            string unos = citac["idKnjige"].ToString() + "\t\t" + citac["naslovKnjige"].ToString() + "\t\t\t\t" + citac["cenaIznajmljivanja"].ToString() + "\t\t" + citac["godinaIzdavanja"].ToString();
            string izdato= citac["izdato"].ToString().ToLower();
            konekcija.Close();
            for (int i = 2; i < listaKorpa.Items.Count; i++)
            {
                if (listaKorpa.Items[i].ToString() == unos)
                {
                    postoji = true;
                   
                }
            }
            if (postoji == false)
            {
                if (izdato != "true")
                {
                    listaKorpa.Items.Add(unos);
                    MessageBox.Show("Dodavanje završeno.", "Obaveštenje");
                }
                else
                {
                    MessageBox.Show("Knjiga je već iznajmljena.", "Obaveštenje");
                }
            }
            else 
            {
                MessageBox.Show("Knjiga je već dodata u korpu.", "Obaveštenje");
            }
        }

        private void listaKorpa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listaKorpa.SelectedIndex > 1)
            {
                btnIzbaci.Enabled = true;
            }
            else
            {
                btnIzbaci.Enabled = false;
            }
        }

        private void btnIzbaci_Click(object sender, EventArgs e)
        {
            listaKorpa.Items.RemoveAt(listaKorpa.SelectedIndex);
        }

        private void btnIznajmi_Click(object sender, EventArgs e)
        {
            int idknjige,suma=0;
            for (int i = 2; i < listaKorpa.Items.Count; i++)
            {
                idknjige = Convert.ToInt16(listaKorpa.Items[i].ToString().Substring(0, listaKorpa.Items[i].ToString().IndexOf("\t")));
                konekcija.Open();
                var komanda = konekcija.CreateCommand();
                komanda.CommandText = "SELECT * FROM knjiga WHERE idKnjige=" + idknjige + "";
                var citac = komanda.ExecuteReader();
                citac.Read();
                suma = suma + Convert.ToInt16(citac["cenaIznajmljivanja"].ToString());
                konekcija.Close();

                konekcija.Open();
                var komanda1 = konekcija.CreateCommand();
                komanda1.CommandText = "SELECT idClanaBiblioteke FROM clanbiblioteke WHERE Korisnici_idKorisnici="+idKorisnici+"";
                var citac1 = komanda1.ExecuteReader();
                citac1.Read();
                int idClana=Convert.ToInt16(citac1["idClanaBiblioteke"].ToString());
                this.idClanaBibliteke = idClana;
                konekcija.Close();

                konekcija.Open();
                var komanda2 = konekcija.CreateCommand();
                komanda2.CommandText = "UPDATE knjiga SET izdato=" + true + ",ClanBiblioteke_idClanaBiblioteke=" + idKorisnici + " WHERE idKnjige=" + idknjige + "";
                komanda2.ExecuteNonQuery();
                konekcija.Close();

                konekcija.Open();
                var komanda3 = konekcija.CreateCommand();
                komanda.CommandText = "SELECT * FROM knjiga";
                var citac3 = komanda.ExecuteReader();

                prikazi.Items.Clear();
                prikazi.Items.Add("ID\t\tNAZIV\t\t\t\tCIJENA\t\tGODINA\t\tIZNAJMLJENO");
                prikazi.Items.Add("");
              
                while (citac3.Read())
                {
                    string izn = "Nije iznajmljena";
                    if (citac3["izdato"].ToString().ToLower() == "true")
                    {
                        izn = "Iznajmljena";
                    }
                    string unos = citac3["idKnjige"].ToString() + "\t\t" + citac3["naslovKnjige"].ToString() + "\t\t\t\t" + citac3["cenaIznajmljivanja"].ToString() + "\t\t" + citac3["godinaIzdavanja"].ToString() + "\t\t" + izn;
                    prikazi.Items.Add(unos);
                }
                konekcija.Close();
            }
            for (int j = 2; j < listaKorpa.Items.Count; j++)
            {
                listaKorpa.Items.RemoveAt(j);
                j--;
            }
            MessageBox.Show("Vaš dug po računu je uvećan za vrednost "+suma+" KM.","Inkrement stanja računa");
            
            konekcija.Open();
            var komanda4 = konekcija.CreateCommand();
            string datum = DateTime.Now.ToString("u").Replace("/", "-");

            komanda4.CommandText = "INSERT INTO racun(datumIzdavanja,zaNaplatu,ClanBiblioteke_idClanaBiblioteke, naplaceno) VALUES ('"+datum+"'," + suma + "," + idClanaBibliteke + ","+false+")";
            komanda4.ExecuteNonQuery();
            konekcija.Close();

            listaIznajmljeneKnjige.Items.Clear();
            listaIznajmljeneKnjige.Items.Add("ID\t\tNAZIV\t\t\t\tGODINA");
            listaIznajmljeneKnjige.Items.Add("");

            konekcija.Open();
            var komanda5 = konekcija.CreateCommand();
            komanda5.CommandText = "SELECT * FROM knjiga WHERE ClanBiblioteke_idClanaBiblioteke=" + idClanaBibliteke + "";
            var citac5 = komanda5.ExecuteReader();
            while (citac5.Read())
            {
                string unos = citac5["idKnjige"].ToString() + "\t\t" + citac5["naslovKnjige"].ToString() + "\t\t\t\t" + citac5["godinaIzdavanja"].ToString() + "";
                listaIznajmljeneKnjige.Items.Add(unos);
            }
            konekcija.Close();

            listaRacuna.Items.Clear();
            listaRacuna.Items.Add("ID\t\tDATUM\t\t\t\tNAPLAĆENO");
            listaRacuna.Items.Add("");

            suma = 0;
            konekcija2.Open();
            var komanda6 = konekcija2.CreateCommand();
            komanda6.CommandText = "SELECT * FROM racun WHERE ClanBiblioteke_idClanaBiblioteke=" + idClanaBibliteke + "";
            var citac6 = komanda6.ExecuteReader();
            while (citac6.Read())
            {
                var ddd = citac6["datumIzdavanja"];
                string unos = citac6["idracun"].ToString() + "\t\t" + citac6["datumIzdavanja"].ToString() + "\t\t\t\t" + citac6["zaNaplatu"].ToString() + "";
                suma = suma + Convert.ToInt16(citac6["zaNaplatu"].ToString()); 
                listaRacuna.Items.Add(unos);
            }
            labelaSuma.Text = "Suma računa koje sam ostvario: " + suma + " KM";
            konekcija2.Close();
        }

        private void btnPodesavanja_Click(object sender, EventArgs e)
        {
            Podesavanja set = new Podesavanja(idKorisnici);
            set.ShowDialog();
        }

        private void cbNijeIzdato_CheckedChanged(object sender, EventArgs e)
        {
            prikazi.Items.Clear();

            if (cbIzdato.Checked == true && cbNijeIzdato.Checked == false)
            {
                prikazi.Items.Clear();
                if (comboPrikaz.SelectedItem.ToString() == "Pisac")
                {
                    prikazi.Items.Add("ID KNJIGE\tPISAC\t\tNAZIV\t\t\tCIJENA\tGODINA\tIZNAJMLJENO");
                    prikazi.Items.Add("");

                    konekcija.Open();
                    var komanda = konekcija.CreateCommand();
                    komanda.CommandText = "SELECT k.idKnjige, p.imePisac, k.naslovKnjige, k.cenaIznajmljivanja, k.godinaIzdavanja, k.izdato FROM pisac p, napisao n, knjiga k WHERE k.idKnjige=n.Knjiga_idKnjige AND p.idPisac=n.Pisac_idPisac";
                    var citac = komanda.ExecuteReader();

                    while (citac.Read())
                    {
                        string izn = "Nije iznajmljena";
                        if (citac["izdato"].ToString().ToLower() == "true")
                        {
                            izn = "Iznajmljena";
                        }
                        string unos = citac["idKnjige"].ToString() + "\t\t" + citac["imePisac"].ToString() + "\t\t" + citac["naslovKnjige"].ToString() + "\t\t\t" + citac["cenaIznajmljivanja"].ToString() + "\t" + citac["godinaIzdavanja"].ToString() + "\t" + izn;
                        if (citac["izdato"].ToString().ToLower() == "true")
                        {
                            prikazi.Items.Add(unos);
                        }
                    }
                    konekcija.Close();
                }
                else if (comboPrikaz.SelectedItem.ToString() == "- -")
                {
                    konekcija.Open();
                    var komanda = konekcija.CreateCommand();
                    komanda.CommandText = "SELECT * FROM knjiga";
                    var citac = komanda.ExecuteReader();

                    prikazi.Items.Add("ID\t\tNAZIV\t\t\t\tCIJENA\t\tGODINA\t\tIZNAJMLJENO");
                    prikazi.Items.Add("");

                    while (citac.Read())
                    {
                        string izn = "Nije iznajmljena";
                        if (citac["izdato"].ToString().ToLower() == "true")
                        {
                            izn = "Iznajmljena";
                        }
                        string unos = citac["idKnjige"].ToString() + "\t\t" + citac["naslovKnjige"].ToString() + "\t\t\t\t" + citac["cenaIznajmljivanja"].ToString() + "\t\t" + citac["godinaIzdavanja"].ToString() + "\t\t" + izn;
                        if (citac["izdato"].ToString().ToLower() == "true")
                        {
                            prikazi.Items.Add(unos);
                        }
                    }
                    konekcija.Close();
                }
                else if (comboPrikaz.SelectedItem.ToString() == "Kategorija")
                {
                    prikazi.Items.Add("ID KNJIGE\tKATEGORIJA\t\tNAZIV\t\t\tCIJENA\tGODINA\tIZNAJMLJENO");
                    prikazi.Items.Add("");

                    konekcija.Open();
                    var komanda = konekcija.CreateCommand();
                    komanda.CommandText = "SELECT k.idKnjige, ka.nazivKategorije, k.naslovKnjige, k.cenaIznajmljivanja, k.godinaIzdavanja, k.izdato FROM kategorija ka, pripada p, knjiga k WHERE k.idKnjige=p.Knjiga_idKnjige AND p.Kategorija_idKategorija=ka.idKategorija";
                    var citac = komanda.ExecuteReader();

                    while (citac.Read())
                    {
                        string izn = "Nije iznajmljena";
                        if (citac["izdato"].ToString().ToLower() == "true")
                        {
                            izn = "Iznajmljena";
                        }
                        string unos = citac["idKnjige"].ToString() + "\t\t" + citac["nazivKategorije"].ToString() + "        \t\t" + citac["naslovKnjige"].ToString() + "\t\t\t" + citac["cenaIznajmljivanja"].ToString() + "\t" + citac["godinaIzdavanja"].ToString() + "\t" + izn;
                        if (citac["izdato"].ToString().ToLower() == "true")
                        {
                            prikazi.Items.Add(unos);
                        }
                    }
                    konekcija.Close();
                }
                else if (comboPrikaz.SelectedItem.ToString() == "Izdavač")
                {
                    prikazi.Items.Add("ID KNJIGE\tIZDAVAČ\t\tNAZIV\t\t\tCIJENA\tGODINA\tIZNAJMLJENO");
                    prikazi.Items.Add("");

                    konekcija.Open();
                    var komanda = konekcija.CreateCommand();
                    komanda.CommandText = "SELECT k.idKnjige, iz.nazivIzdavaca, k.naslovKnjige, k.cenaIznajmljivanja, k.godinaIzdavanja, k.izdato FROM izdaje i, izdavac iz, knjiga k WHERE k.idKnjige=i.Knjiga_idKnjige AND iz.idIzdavaca=i.Izdavac_idIzdavaca";
                    var citac = komanda.ExecuteReader();

                    while (citac.Read())
                    {
                        string izn = "Nije iznajmljena";
                        if (citac["izdato"].ToString().ToLower() == "true")
                        {
                            izn = "Iznajmljena";
                        }
                        string unos = citac["idKnjige"].ToString() + "\t\t" + citac["nazivIzdavaca"].ToString() + "\t\t" + citac["naslovKnjige"].ToString() + "\t\t\t" + citac["cenaIznajmljivanja"].ToString() + "\t" + citac["godinaIzdavanja"].ToString() + "\t" + izn;
                        if (citac["izdato"].ToString().ToLower() == "true")
                        {
                            prikazi.Items.Add(unos);
                        }
                    }
                    konekcija.Close();
                }
            }
            else if (cbIzdato.Checked == false && cbNijeIzdato.Checked == true)
            {
                if (comboPrikaz.SelectedItem.ToString() == "Pisac")
                {
                    prikazi.Items.Add("ID KNJIGE\tPISAC\t\tNAZIV\t\t\tCIJENA\tGODINA\tIZNAJMLJENO");
                    prikazi.Items.Add("");

                    konekcija.Open();
                    var komanda = konekcija.CreateCommand();
                    komanda.CommandText = "SELECT k.idKnjige, p.imePisac, k.naslovKnjige, k.cenaIznajmljivanja, k.godinaIzdavanja, k.izdato FROM pisac p, napisao n, knjiga k WHERE k.idKnjige=n.Knjiga_idKnjige AND p.idPisac=n.Pisac_idPisac";
                    var citac = komanda.ExecuteReader();

                    while (citac.Read())
                    {
                        string izn = "Nije iznajmljena";
                        if (citac["izdato"].ToString().ToLower() == "true")
                        {
                            izn = "Iznajmljena";
                        }
                        string unos = citac["idKnjige"].ToString() + "\t\t" + citac["imePisac"].ToString() + "\t\t" + citac["naslovKnjige"].ToString() + "\t\t\t" + citac["cenaIznajmljivanja"].ToString() + "\t" + citac["godinaIzdavanja"].ToString() + "\t" + izn;
                        if (citac["izdato"].ToString().ToLower() != "true")
                        {
                            prikazi.Items.Add(unos);
                        }
                    }
                    konekcija.Close();
                }
                else if (comboPrikaz.SelectedItem.ToString() == "- -")
                {
                    konekcija.Open();
                    var komanda = konekcija.CreateCommand();
                    komanda.CommandText = "SELECT * FROM knjiga";
                    var citac = komanda.ExecuteReader();

                    prikazi.Items.Add("ID\t\tNAZIV\t\t\t\tCIJENA\t\tGODINA\t\tIZNAJMLJENO");
                    prikazi.Items.Add("");

                    while (citac.Read())
                    {
                        string izn = "Nije iznajmljena";
                        if (citac["izdato"].ToString().ToLower() == "true")
                        {
                            izn = "Iznajmljena";
                        }
                        string unos = citac["idKnjige"].ToString() + "\t\t" + citac["naslovKnjige"].ToString() + "\t\t\t\t" + citac["cenaIznajmljivanja"].ToString() + "\t\t" + citac["godinaIzdavanja"].ToString() + "\t\t" + izn;
                        if (citac["izdato"].ToString().ToLower() != "true")
                        {
                            prikazi.Items.Add(unos);
                        }
                    }
                    konekcija.Close();
                }
                else if (comboPrikaz.SelectedItem.ToString() == "Kategorija")
                {
                    prikazi.Items.Add("ID KNJIGE\tKATEGORIJA\t\tNAZIV\t\t\tCIJENA\tGODINA\tIZNAJMLJENO");
                    prikazi.Items.Add("");

                    konekcija.Open();
                    var komanda = konekcija.CreateCommand();
                    komanda.CommandText = "SELECT k.idKnjige, ka.nazivKategorije, k.naslovKnjige, k.cenaIznajmljivanja, k.godinaIzdavanja, k.izdato FROM kategorija ka, pripada p, knjiga k WHERE k.idKnjige=p.Knjiga_idKnjige AND p.Kategorija_idKategorija=ka.idKategorija";
                    var citac = komanda.ExecuteReader();

                    while (citac.Read())
                    {
                        string izn = "Nije iznajmljena";
                        if (citac["izdato"].ToString().ToLower() == "true")
                        {
                            izn = "Iznajmljena";
                        }
                        string unos = citac["idKnjige"].ToString() + "\t\t" + citac["nazivKategorije"].ToString() + "        \t\t" + citac["naslovKnjige"].ToString() + "\t\t\t" + citac["cenaIznajmljivanja"].ToString() + "\t" + citac["godinaIzdavanja"].ToString() + "\t" + izn;
                        if (citac["izdato"].ToString().ToLower() != "true")
                        {
                            prikazi.Items.Add(unos);
                        }
                    }
                    konekcija.Close();
                }
                else if (comboPrikaz.SelectedItem.ToString() == "Izdavač")
                {
                    prikazi.Items.Add("ID KNJIGE\tIZDAVAČ\t\tNAZIV\t\t\tCIJENA\tGODINA\tIZNAJMLJENO");
                    prikazi.Items.Add("");

                    konekcija.Open();
                    var komanda = konekcija.CreateCommand();
                    komanda.CommandText = "SELECT k.idKnjige, iz.nazivIzdavaca, k.naslovKnjige, k.cenaIznajmljivanja, k.godinaIzdavanja, k.izdato FROM izdaje i, izdavac iz, knjiga k WHERE k.idKnjige=i.Knjiga_idKnjige AND iz.idIzdavaca=i.Izdavac_idIzdavaca";
                    var citac = komanda.ExecuteReader();

                    while (citac.Read())
                    {
                        string izn = "Nije iznajmljena";
                        if (citac["izdato"].ToString().ToLower() == "true")
                        {
                            izn = "Iznajmljena";
                        }
                        string unos = citac["idKnjige"].ToString() + "\t\t" + citac["nazivIzdavaca"].ToString() + "\t\t" + citac["naslovKnjige"].ToString() + "\t\t\t" + citac["cenaIznajmljivanja"].ToString() + "\t" + citac["godinaIzdavanja"].ToString() + "\t" + izn;
                        if (citac["izdato"].ToString().ToLower() != "true")
                        {
                            prikazi.Items.Add(unos);
                        }
                    }
                    konekcija.Close();
                }
            }
            else if (cbIzdato.Checked == true && cbNijeIzdato.Checked == true)
            {
                if (comboPrikaz.SelectedItem.ToString() == "Pisac")
                {
                    prikazi.Items.Add("ID KNJIGE\tPISAC\t\tNAZIV\t\t\tCIJENA\tGODINA\tIZNAJMLJENO");
                    prikazi.Items.Add("");

                    konekcija.Open();
                    var komanda = konekcija.CreateCommand();
                    komanda.CommandText = "SELECT k.idKnjige, p.imePisac, k.naslovKnjige, k.cenaIznajmljivanja, k.godinaIzdavanja, k.izdato FROM pisac p, napisao n, knjiga k WHERE k.idKnjige=n.Knjiga_idKnjige AND p.idPisac=n.Pisac_idPisac";
                    var citac = komanda.ExecuteReader();

                    while (citac.Read())
                    {
                        string izn = "Nije iznajmljena";
                        if (citac["izdato"].ToString().ToLower() == "true")
                        {
                            izn = "Iznajmljena";
                        }
                        string unos = citac["idKnjige"].ToString() + "\t\t" + citac["imePisac"].ToString() + "\t\t" + citac["naslovKnjige"].ToString() + "\t\t\t" + citac["cenaIznajmljivanja"].ToString() + "\t" + citac["godinaIzdavanja"].ToString() + "\t" + izn;
                        prikazi.Items.Add(unos);
                    }
                    konekcija.Close();
                }
                else if (comboPrikaz.SelectedItem.ToString() == "- -")
                {
                    konekcija.Open();
                    var komanda = konekcija.CreateCommand();
                    komanda.CommandText = "SELECT * FROM knjiga";
                    var citac = komanda.ExecuteReader();

                    prikazi.Items.Add("ID\t\tNAZIV\t\t\t\tCIJENA\t\tGODINA\t\tIZNAJMLJENO");
                    prikazi.Items.Add("");

                    while (citac.Read())
                    {
                        string izn = "Nije iznajmljena";
                        if (citac["izdato"].ToString().ToLower() == "true")
                        {
                            izn = "Iznajmljena";
                        }
                        string unos = citac["idKnjige"].ToString() + "\t\t" + citac["naslovKnjige"].ToString() + "\t\t\t\t" + citac["cenaIznajmljivanja"].ToString() + "\t\t" + citac["godinaIzdavanja"].ToString() + "\t\t" + izn;
                        prikazi.Items.Add(unos);
                    }
                    konekcija.Close();
                }
                else if (comboPrikaz.SelectedItem.ToString() == "Kategorija")
                {
                    prikazi.Items.Add("ID KNJIGE\tKATEGORIJA\t\tNAZIV\t\t\tCIJENA\tGODINA\tIZNAJMLJENO");
                    prikazi.Items.Add("");

                    konekcija.Open();
                    var komanda = konekcija.CreateCommand();
                    komanda.CommandText = "SELECT k.idKnjige, ka.nazivKategorije, k.naslovKnjige, k.cenaIznajmljivanja, k.godinaIzdavanja, k.izdato FROM kategorija ka, pripada p, knjiga k WHERE k.idKnjige=p.Knjiga_idKnjige AND p.Kategorija_idKategorija=ka.idKategorija";
                    var citac = komanda.ExecuteReader();

                    while (citac.Read())
                    {
                        string izn = "Nije iznajmljena";
                        if (citac["izdato"].ToString().ToLower() == "true")
                        {
                            izn = "Iznajmljena";
                        }
                        string unos = citac["idKnjige"].ToString() + "\t\t" + citac["nazivKategorije"].ToString() + "        \t\t" + citac["naslovKnjige"].ToString() + "\t\t\t" + citac["cenaIznajmljivanja"].ToString() + "\t" + citac["godinaIzdavanja"].ToString() + "\t" + izn;
                        prikazi.Items.Add(unos);
                    }
                    konekcija.Close();
                }
                else if (comboPrikaz.SelectedItem.ToString() == "Izdavač")
                {
                    prikazi.Items.Add("ID KNJIGE\tIZDAVAČ\t\tNAZIV\t\t\tCIJENA\tGODINA\tIZNAJMLJENO");
                    prikazi.Items.Add("");

                    konekcija.Open();
                    var komanda = konekcija.CreateCommand();
                    komanda.CommandText = "SELECT k.idKnjige, iz.nazivIzdavaca, k.naslovKnjige, k.cenaIznajmljivanja, k.godinaIzdavanja, k.izdato FROM izdaje i, izdavac iz, knjiga k WHERE k.idKnjige=i.Knjiga_idKnjige AND iz.idIzdavaca=i.Izdavac_idIzdavaca";
                    var citac = komanda.ExecuteReader();

                    while (citac.Read())
                    {
                        string izn = "Nije iznajmljena";
                        if (citac["izdato"].ToString().ToLower() == "true")
                        {
                            izn = "Iznajmljena";
                        }
                        string unos = citac["idKnjige"].ToString() + "\t\t" + citac["nazivIzdavaca"].ToString() + "\t\t" + citac["naslovKnjige"].ToString() + "\t\t\t" + citac["cenaIznajmljivanja"].ToString() + "\t" + citac["godinaIzdavanja"].ToString() + "\t" + izn;
                        prikazi.Items.Add(unos);
                    }
                    konekcija.Close();
                }
            }
            else 
            {
                if (comboPrikaz.SelectedItem.ToString() == "Pisac")
                {
                    prikazi.Items.Add("ID KNJIGE\tPISAC\t\tNAZIV\t\t\tCIJENA\tGODINA\tIZNAJMLJENO");
                    prikazi.Items.Add("");
                }
                else if (comboPrikaz.SelectedItem.ToString() == "Kategorija")
                {
                    prikazi.Items.Add("ID KNJIGE\tKATEGORIJA\t\tNAZIV\t\t\tCIJENA\tGODINA\tIZNAJMLJENO");
                    prikazi.Items.Add("");
                }
                else if (comboPrikaz.SelectedItem.ToString() == "Izdavač")
                {
                    prikazi.Items.Add("ID KNJIGE\tIZDAVAČ\t\tNAZIV\t\t\tCIJENA\tGODINA\tIZNAJMLJENO");
                    prikazi.Items.Add("");
                }
                else if (comboPrikaz.SelectedItem.ToString() == "- -")
                {
                    prikazi.Items.Add("ID\t\tNAZIV\t\t\t\tCIJENA\t\tGODINA\t\tIZNAJMLJENO");
                    prikazi.Items.Add("");
                }
            }
        }

        private void cbIzdato_CheckedChanged(object sender, EventArgs e)
        {
            prikazi.Items.Clear();

            if (cbIzdato.Checked == true && cbNijeIzdato.Checked == false)
            {
                prikazi.Items.Clear();
                if (comboPrikaz.SelectedItem.ToString() == "Pisac")
                {
                    prikazi.Items.Add("ID KNJIGE\tPISAC\t\tNAZIV\t\t\tCIJENA\tGODINA\tIZNAJMLJENO");
                    prikazi.Items.Add("");

                    konekcija.Open();
                    var komanda = konekcija.CreateCommand();
                    komanda.CommandText = "SELECT k.idKnjige, p.imePisac, k.naslovKnjige, k.cenaIznajmljivanja, k.godinaIzdavanja, k.izdato FROM pisac p, napisao n, knjiga k WHERE k.idKnjige=n.Knjiga_idKnjige AND p.idPisac=n.Pisac_idPisac";
                    var citac = komanda.ExecuteReader();

                    while (citac.Read())
                    {
                        string izn = "Nije iznajmljena";
                        if (citac["izdato"].ToString().ToLower() == "true")
                        {
                            izn = "Iznajmljena";
                        }
                        string unos = citac["idKnjige"].ToString() + "\t\t" + citac["imePisac"].ToString() + "\t\t" + citac["naslovKnjige"].ToString() + "\t\t\t" + citac["cenaIznajmljivanja"].ToString() + "\t" + citac["godinaIzdavanja"].ToString() + "\t" + izn;
                        if (citac["izdato"].ToString().ToLower() == "true")
                        {
                            prikazi.Items.Add(unos);
                        }
                    }
                    konekcija.Close();
                }
                else if (comboPrikaz.SelectedItem.ToString() == "- -")
                {
                    konekcija.Open();
                    var komanda = konekcija.CreateCommand();
                    komanda.CommandText = "SELECT * FROM knjiga";
                    var citac = komanda.ExecuteReader();

                    prikazi.Items.Add("ID\t\tNAZIV\t\t\t\tCIJENA\t\tGODINA\t\tIZNAJMLJENO");
                    prikazi.Items.Add("");

                    while (citac.Read())
                    {
                        string izn = "Nije iznajmljena";
                        if (citac["izdato"].ToString().ToLower() == "true")
                        {
                            izn = "Iznajmljena";
                        }
                        string unos = citac["idKnjige"].ToString() + "\t\t" + citac["naslovKnjige"].ToString() + "\t\t\t\t" + citac["cenaIznajmljivanja"].ToString() + "\t\t" + citac["godinaIzdavanja"].ToString() + "\t\t" + izn;
                        if (citac["izdato"].ToString().ToLower() == "true")
                        {
                            prikazi.Items.Add(unos);
                        }
                    }
                    konekcija.Close();
                }
                else if (comboPrikaz.SelectedItem.ToString() == "Kategorija")
                {
                    prikazi.Items.Add("ID KNJIGE\tKATEGORIJA\t\tNAZIV\t\t\tCIJENA\tGODINA\tIZNAJMLJENO");
                    prikazi.Items.Add("");

                    konekcija.Open();
                    var komanda = konekcija.CreateCommand();
                    komanda.CommandText = "SELECT k.idKnjige, ka.nazivKategorije, k.naslovKnjige, k.cenaIznajmljivanja, k.godinaIzdavanja, k.izdato FROM kategorija ka, pripada p, knjiga k WHERE k.idKnjige=p.Knjiga_idKnjige AND p.Kategorija_idKategorija=ka.idKategorija";
                    var citac = komanda.ExecuteReader();

                    while (citac.Read())
                    {
                        string izn = "Nije iznajmljena";
                        if (citac["izdato"].ToString().ToLower() == "true")
                        {
                            izn = "Iznajmljena";
                        }
                        string unos = citac["idKnjige"].ToString() + "\t\t" + citac["nazivKategorije"].ToString() + "        \t\t" + citac["naslovKnjige"].ToString() + "\t\t\t" + citac["cenaIznajmljivanja"].ToString() + "\t" + citac["godinaIzdavanja"].ToString() + "\t" + izn;
                        if (citac["izdato"].ToString().ToLower() == "true")
                        {
                            prikazi.Items.Add(unos);
                        }
                    }
                    konekcija.Close();
                }
                else if (comboPrikaz.SelectedItem.ToString() == "Izdavač")
                {
                    prikazi.Items.Add("ID KNJIGE\tIZDAVAČ\t\tNAZIV\t\t\tCIJENA\tGODINA\tIZNAJMLJENO");
                    prikazi.Items.Add("");

                    konekcija.Open();
                    var komanda = konekcija.CreateCommand();
                    komanda.CommandText = "SELECT k.idKnjige, iz.nazivIzdavaca, k.naslovKnjige, k.cenaIznajmljivanja, k.godinaIzdavanja, k.izdato FROM izdaje i, izdavac iz, knjiga k WHERE k.idKnjige=i.Knjiga_idKnjige AND iz.idIzdavaca=i.Izdavac_idIzdavaca";
                    var citac = komanda.ExecuteReader();

                    while (citac.Read())
                    {
                        string izn = "Nije iznajmljena";
                        if (citac["izdato"].ToString().ToLower() == "true")
                        {
                            izn = "Iznajmljena";
                        }
                        string unos = citac["idKnjige"].ToString() + "\t\t" + citac["nazivIzdavaca"].ToString() + "\t\t" + citac["naslovKnjige"].ToString() + "\t\t\t" + citac["cenaIznajmljivanja"].ToString() + "\t" + citac["godinaIzdavanja"].ToString() + "\t" + izn;
                        if (citac["izdato"].ToString().ToLower() == "true")
                        {
                            prikazi.Items.Add(unos);
                        }
                    }
                    konekcija.Close();
                }
            }
            else if (cbIzdato.Checked == false && cbNijeIzdato.Checked == true)
            {
                if (comboPrikaz.SelectedItem.ToString() == "Pisac")
                {
                    prikazi.Items.Add("ID KNJIGE\tPISAC\t\tNAZIV\t\t\tCIJENA\tGODINA\tIZNAJMLJENO");
                    prikazi.Items.Add("");

                    konekcija.Open();
                    var komanda = konekcija.CreateCommand();
                    komanda.CommandText = "SELECT k.idKnjige, p.imePisac, k.naslovKnjige, k.cenaIznajmljivanja, k.godinaIzdavanja, k.izdato FROM pisac p, napisao n, knjiga k WHERE k.idKnjige=n.Knjiga_idKnjige AND p.idPisac=n.Pisac_idPisac";
                    var citac = komanda.ExecuteReader();

                    while (citac.Read())
                    {
                        string izn = "Nije iznajmljena";
                        if (citac["izdato"].ToString().ToLower() == "true")
                        {
                            izn = "Iznajmljena";
                        }
                        string unos = citac["idKnjige"].ToString() + "\t\t" + citac["imePisac"].ToString() + "\t\t" + citac["naslovKnjige"].ToString() + "\t\t\t" + citac["cenaIznajmljivanja"].ToString() + "\t" + citac["godinaIzdavanja"].ToString() + "\t" + izn;
                        if (citac["izdato"].ToString().ToLower() != "true")
                        {
                            prikazi.Items.Add(unos);
                        }
                    }
                    konekcija.Close();
                }
                else if (comboPrikaz.SelectedItem.ToString() == "- -")
                {
                    konekcija.Open();
                    var komanda = konekcija.CreateCommand();
                    komanda.CommandText = "SELECT * FROM knjiga";
                    var citac = komanda.ExecuteReader();

                    prikazi.Items.Add("ID\t\tNAZIV\t\t\t\tCIJENA\t\tGODINA\t\tIZNAJMLJENO");
                    prikazi.Items.Add("");

                    while (citac.Read())
                    {
                        string izn = "Nije iznajmljena";
                        if (citac["izdato"].ToString().ToLower() == "true")
                        {
                            izn = "Iznajmljena";
                        }
                        string unos = citac["idKnjige"].ToString() + "\t\t" + citac["naslovKnjige"].ToString() + "\t\t\t\t" + citac["cenaIznajmljivanja"].ToString() + "\t\t" + citac["godinaIzdavanja"].ToString() + "\t\t" + izn;
                        if (citac["izdato"].ToString().ToLower() != "true")
                        {
                            prikazi.Items.Add(unos);
                        }
                    }
                    konekcija.Close();
                }
                else if (comboPrikaz.SelectedItem.ToString() == "Kategorija")
                {
                    prikazi.Items.Add("ID KNJIGE\tKATEGORIJA\t\tNAZIV\t\t\tCIJENA\tGODINA\tIZNAJMLJENO");
                    prikazi.Items.Add("");

                    konekcija.Open();
                    var komanda = konekcija.CreateCommand();
                    komanda.CommandText = "SELECT k.idKnjige, ka.nazivKategorije, k.naslovKnjige, k.cenaIznajmljivanja, k.godinaIzdavanja, k.izdato FROM kategorija ka, pripada p, knjiga k WHERE k.idKnjige=p.Knjiga_idKnjige AND p.Kategorija_idKategorija=ka.idKategorija";
                    var citac = komanda.ExecuteReader();

                    while (citac.Read())
                    {
                        string izn = "Nije iznajmljena";
                        if (citac["izdato"].ToString().ToLower() == "true")
                        {
                            izn = "Iznajmljena";
                        }
                        string unos = citac["idKnjige"].ToString() + "\t\t" + citac["nazivKategorije"].ToString() + "        \t\t" + citac["naslovKnjige"].ToString() + "\t\t\t" + citac["cenaIznajmljivanja"].ToString() + "\t" + citac["godinaIzdavanja"].ToString() + "\t" + izn;
                        if (citac["izdato"].ToString().ToLower() != "true")
                        {
                            prikazi.Items.Add(unos);
                        }
                    }
                    konekcija.Close();
                }
                else if (comboPrikaz.SelectedItem.ToString() == "Izdavač")
                {
                    prikazi.Items.Add("ID KNJIGE\tIZDAVAČ\t\tNAZIV\t\t\tCIJENA\tGODINA\tIZNAJMLJENO");
                    prikazi.Items.Add("");

                    konekcija.Open();
                    var komanda = konekcija.CreateCommand();
                    komanda.CommandText = "SELECT k.idKnjige, iz.nazivIzdavaca, k.naslovKnjige, k.cenaIznajmljivanja, k.godinaIzdavanja, k.izdato FROM izdaje i, izdavac iz, knjiga k WHERE k.idKnjige=i.Knjiga_idKnjige AND iz.idIzdavaca=i.Izdavac_idIzdavaca";
                    var citac = komanda.ExecuteReader();

                    while (citac.Read())
                    {
                        string izn = "Nije iznajmljena";
                        if (citac["izdato"].ToString().ToLower() == "true")
                        {
                            izn = "Iznajmljena";
                        }
                        string unos = citac["idKnjige"].ToString() + "\t\t" + citac["nazivIzdavaca"].ToString() + "\t\t" + citac["naslovKnjige"].ToString() + "\t\t\t" + citac["cenaIznajmljivanja"].ToString() + "\t" + citac["godinaIzdavanja"].ToString() + "\t" + izn;
                        if (citac["izdato"].ToString().ToLower() != "true")
                        {
                            prikazi.Items.Add(unos);
                        }
                    }
                    konekcija.Close();
                }
            }
            else if (cbIzdato.Checked == true && cbNijeIzdato.Checked == true)
            {
                if (comboPrikaz.SelectedItem.ToString() == "Pisac")
                {
                    prikazi.Items.Add("ID KNJIGE\tPISAC\t\tNAZIV\t\t\tCIJENA\tGODINA\tIZNAJMLJENO");
                    prikazi.Items.Add("");

                    konekcija.Open();
                    var komanda = konekcija.CreateCommand();
                    komanda.CommandText = "SELECT k.idKnjige, p.imePisac, k.naslovKnjige, k.cenaIznajmljivanja, k.godinaIzdavanja, k.izdato FROM pisac p, napisao n, knjiga k WHERE k.idKnjige=n.Knjiga_idKnjige AND p.idPisac=n.Pisac_idPisac";
                    var citac = komanda.ExecuteReader();

                    while (citac.Read())
                    {
                        string izn = "Nije iznajmljena";
                        if (citac["izdato"].ToString().ToLower() == "true")
                        {
                            izn = "Iznajmljena";
                        }
                        string unos = citac["idKnjige"].ToString() + "\t\t" + citac["imePisac"].ToString() + "\t\t" + citac["naslovKnjige"].ToString() + "\t\t\t" + citac["cenaIznajmljivanja"].ToString() + "\t" + citac["godinaIzdavanja"].ToString() + "\t" + izn;
                        prikazi.Items.Add(unos);
                    }
                    konekcija.Close();
                }
                else if (comboPrikaz.SelectedItem.ToString() == "- -")
                {
                    konekcija.Open();
                    var komanda = konekcija.CreateCommand();
                    komanda.CommandText = "SELECT * FROM knjiga";
                    var citac = komanda.ExecuteReader();

                    prikazi.Items.Add("ID\t\tNAZIV\t\t\t\tCIJENA\t\tGODINA\t\tIZNAJMLJENO");
                    prikazi.Items.Add("");

                    while (citac.Read())
                    {
                        string izn = "Nije iznajmljena";
                        if (citac["izdato"].ToString().ToLower() == "true")
                        {
                            izn = "Iznajmljena";
                        }
                        string unos = citac["idKnjige"].ToString() + "\t\t" + citac["naslovKnjige"].ToString() + "\t\t\t\t" + citac["cenaIznajmljivanja"].ToString() + "\t\t" + citac["godinaIzdavanja"].ToString() + "\t\t" + izn;
                        prikazi.Items.Add(unos);
                    }
                    konekcija.Close();
                }
                else if (comboPrikaz.SelectedItem.ToString() == "Kategorija")
                {
                    prikazi.Items.Add("ID KNJIGE\tKATEGORIJA\t\tNAZIV\t\t\tCIJENA\tGODINA\tIZNAJMLJENO");
                    prikazi.Items.Add("");

                    konekcija.Open();
                    var komanda = konekcija.CreateCommand();
                    komanda.CommandText = "SELECT k.idKnjige, ka.nazivKategorije, k.naslovKnjige, k.cenaIznajmljivanja, k.godinaIzdavanja, k.izdato FROM kategorija ka, pripada p, knjiga k WHERE k.idKnjige=p.Knjiga_idKnjige AND p.Kategorija_idKategorija=ka.idKategorija";
                    var citac = komanda.ExecuteReader();

                    while (citac.Read())
                    {
                        string izn = "Nije iznajmljena";
                        if (citac["izdato"].ToString().ToLower() == "true")
                        {
                            izn = "Iznajmljena";
                        }
                        string unos = citac["idKnjige"].ToString() + "\t\t" + citac["nazivKategorije"].ToString() + "        \t\t" + citac["naslovKnjige"].ToString() + "\t\t\t" + citac["cenaIznajmljivanja"].ToString() + "\t" + citac["godinaIzdavanja"].ToString() + "\t" + izn;
                        prikazi.Items.Add(unos);
                    }
                    konekcija.Close();
                }
                else if (comboPrikaz.SelectedItem.ToString() == "Izdavač")
                {
                    prikazi.Items.Add("ID KNJIGE\tIZDAVAČ\t\tNAZIV\t\t\tCIJENA\tGODINA\tIZNAJMLJENO");
                    prikazi.Items.Add("");

                    konekcija.Open();
                    var komanda = konekcija.CreateCommand();
                    komanda.CommandText = "SELECT k.idKnjige, iz.nazivIzdavaca, k.naslovKnjige, k.cenaIznajmljivanja, k.godinaIzdavanja, k.izdato FROM izdaje i, izdavac iz, knjiga k WHERE k.idKnjige=i.Knjiga_idKnjige AND iz.idIzdavaca=i.Izdavac_idIzdavaca";
                    var citac = komanda.ExecuteReader();

                    while (citac.Read())
                    {
                        string izn = "Nije iznajmljena";
                        if (citac["izdato"].ToString().ToLower() == "true")
                        {
                            izn = "Iznajmljena";
                        }
                        string unos = citac["idKnjige"].ToString() + "\t\t" + citac["nazivIzdavaca"].ToString() + "\t\t" + citac["naslovKnjige"].ToString() + "\t\t\t" + citac["cenaIznajmljivanja"].ToString() + "\t" + citac["godinaIzdavanja"].ToString() + "\t" + izn;
                        prikazi.Items.Add(unos);
                    }
                    konekcija.Close();
                }
            }
            else
            {
                if (comboPrikaz.SelectedItem.ToString() == "Pisac")
                {
                    prikazi.Items.Add("ID KNJIGE\tPISAC\t\tNAZIV\t\t\tCIJENA\tGODINA\tIZNAJMLJENO");
                    prikazi.Items.Add("");
                }
                else if (comboPrikaz.SelectedItem.ToString() == "Kategorija")
                {
                    prikazi.Items.Add("ID KNJIGE\tKATEGORIJA\t\tNAZIV\t\t\tCIJENA\tGODINA\tIZNAJMLJENO");
                    prikazi.Items.Add("");
                }
                else if (comboPrikaz.SelectedItem.ToString() == "Izdavač")
                {
                    prikazi.Items.Add("ID KNJIGE\tIZDAVAČ\t\tNAZIV\t\t\tCIJENA\tGODINA\tIZNAJMLJENO");
                    prikazi.Items.Add("");
                }
                else if (comboPrikaz.SelectedItem.ToString() == "- -")
                {
                    prikazi.Items.Add("ID\t\tNAZIV\t\t\t\tCIJENA\t\tGODINA\t\tIZNAJMLJENO");
                    prikazi.Items.Add("");
                }
            }
        }

        private void PocetnoPopunjavanje()
        {
            int suma;

            konekcija.Open();
            var komanda = konekcija.CreateCommand();
            komanda.CommandText = "SELECT * FROM knjiga";
            var citac = komanda.ExecuteReader();

            prikazi.Items.Add("ID\t\tNAZIV\t\t\t\tCIJENA\t\tGODINA\t\tIZNAJMLJENO");
            prikazi.Items.Add("");
            listaKorpa.Items.Add("ID\t\tNAZIV\t\t\t\tCIJENA\t\tGODINA");
            listaKorpa.Items.Add("");

            while (citac.Read())
            {
                string izn = "Nije iznajmljena";
                if (citac["izdato"].ToString().ToLower() == "true")
                {
                    izn = "Iznajmljena";
                }
                string unos = citac["idKnjige"].ToString() + "\t\t" + citac["naslovKnjige"].ToString() + "\t\t\t\t" + citac["cenaIznajmljivanja"].ToString() + "\t\t" + citac["godinaIzdavanja"].ToString() + "\t\t" + izn;
                prikazi.Items.Add(unos);
            }
            konekcija.Close();

            labelKorisnik.Text = "Ulogovan korisnik:\n" + ime + " " + prezime + "\nkao član biblioteke.";
            konekcija.Open();
            var komanda1 = konekcija.CreateCommand();
            komanda1.CommandText = "SELECT * FROM clanbiblioteke WHERE Korisnici_idKorisnici=" + idKorisnici + "";
            var citac1 = komanda1.ExecuteReader();
            citac1.Read();
            labelaClanarine.Text = "Aplikaciju možete\nkoristiti do:\n" + citac1["trajanjeClanarine"].ToString();
            if (Convert.ToDateTime(citac1["trajanjeClanarine"]) < DateTime.Now)
            {
                btnProduzi.Enabled = true;
            }

            int idClana = Convert.ToInt16(citac1["idClanaBiblioteke"].ToString());
            this.idClanaBibliteke = idClana;
            konekcija.Close();

            listaIznajmljeneKnjige.Items.Add("ID\t\tNAZIV\t\t\t\tGODINA");
            listaIznajmljeneKnjige.Items.Add("");

            konekcija.Open();
            var komanda2 = konekcija.CreateCommand();
            komanda2.CommandText = "SELECT * FROM knjiga WHERE ClanBiblioteke_idClanaBiblioteke=" + idClanaBibliteke + "";
            var citac2 = komanda2.ExecuteReader();
            while (citac2.Read())
            {
                string unos = citac2["idKnjige"].ToString() + "\t\t" + citac2["naslovKnjige"].ToString() + "\t\t\t\t" + citac2["godinaIzdavanja"].ToString() + "";
                listaIznajmljeneKnjige.Items.Add(unos);
            }
            konekcija.Close();

            suma = 0;
            listaRacuna.Items.Add("ID\t\tDATUM\t\t\t\tNAPLAĆENO");
            listaRacuna.Items.Add("");

            konekcija2.Open();
            var komanda6 = konekcija2.CreateCommand();
            komanda6.CommandText = "SELECT * FROM racun WHERE ClanBiblioteke_idClanaBiblioteke=" + idClanaBibliteke + "";
            var citac6 = komanda6.ExecuteReader();
            while (citac6.Read())
            {
                var ddd = citac6["datumIzdavanja"];
                string unos = citac6["idracun"].ToString() + "\t\t" + citac6["datumIzdavanja"].ToString() + "\t\t\t\t" + citac6["zaNaplatu"].ToString() + "";
                suma = suma + Convert.ToInt16(citac6["zaNaplatu"].ToString());
                listaRacuna.Items.Add(unos);
            }
            labelaSuma.Text = "Suma računa koje sam ostvario: " + suma + " KM";
            konekcija2.Close();
        }

        private void Refresovanje()
        {
            int suma;

            if (prikazi.SelectedItems.Count > 0)
            {
                int indeksKnjige = prikazi.SelectedIndex;
                knjigeRefres();
                prikazi.SetSelected(indeksKnjige, true);
            }
            else 
            {
                knjigeRefres();
            }

            if (tabovi.SelectedTab == tabSveKnjige)
            {
                if (prikazi.SelectedIndex > 1)
                {
                    btnDodaj.Enabled = true;
                }
            }
            else
            {
                btnDodaj.Enabled = false;
            }

            labelKorisnik.Text = "Ulogovan korisnik:\n" + ime + " " + prezime + "\nkao član biblioteke.";
            konekcija.Open();
            var komanda1 = konekcija.CreateCommand();
            komanda1.CommandText = "SELECT * FROM clanbiblioteke WHERE Korisnici_idKorisnici=" + idKorisnici + "";
            var citac1 = komanda1.ExecuteReader();
            citac1.Read();
            labelaClanarine.Text = "Aplikaciju možete\nkoristiti do:\n" + citac1["trajanjeClanarine"].ToString();
            if (Convert.ToDateTime(citac1["trajanjeClanarine"]) < DateTime.Now)
            {
                btnProduzi.Enabled = true;
            }

            int idClana = Convert.ToInt16(citac1["idClanaBiblioteke"].ToString());
            this.idClanaBibliteke = idClana;
            konekcija.Close();

            this.prethodniBrojIznajmljenihKnjiga = listaIznajmljeneKnjige.Items.Count - 2;
            int noviBrojIznajmljenihKnjiga = prethodniBrojIznajmljenihKnjiga;

            konekcija.Open();
            var komanda7 = konekcija.CreateCommand();
            komanda7.CommandText = "SELECT COUNT(*) FROM knjiga WHERE ClanBiblioteke_idClanaBiblioteke=" + idClanaBibliteke + "";
            var citac7 = komanda7.ExecuteReader();
            while (citac7.Read())
            {
                noviBrojIznajmljenihKnjiga = Convert.ToInt16(citac7["COUNT(*)"].ToString());
            }
            konekcija.Close();

            if (noviBrojIznajmljenihKnjiga != prethodniBrojIznajmljenihKnjiga)
            {
                this.prethodniBrojIznajmljenihKnjiga = noviBrojIznajmljenihKnjiga;

                listaIznajmljeneKnjige.Items.Clear();
                listaIznajmljeneKnjige.Items.Add("ID\t\tNAZIV\t\t\t\tGODINA");
                listaIznajmljeneKnjige.Items.Add("");

                konekcija.Open();
                var komanda2 = konekcija.CreateCommand();
                komanda2.CommandText = "SELECT * FROM knjiga WHERE ClanBiblioteke_idClanaBiblioteke=" + idClanaBibliteke + "";
                var citac2 = komanda2.ExecuteReader();
                while (citac2.Read())
                {
                    string unos = citac2["idKnjige"].ToString() + "\t\t" + citac2["naslovKnjige"].ToString() + "\t\t\t\t" + citac2["godinaIzdavanja"].ToString() + "";
                    listaIznajmljeneKnjige.Items.Add(unos);
                }
                konekcija.Close();
            }

            this.prethodniBrojRacuna = listaRacuna.Items.Count - 2;
            int noviBrojRacuna = prethodniBrojRacuna;

            konekcija.Open();
            var komanda8 = konekcija.CreateCommand();
            komanda8.CommandText = "SELECT COUNT(*) FROM racun WHERE ClanBiblioteke_idClanaBiblioteke=" + idClanaBibliteke + "";
            var citac8 = komanda8.ExecuteReader();
            while (citac8.Read())
            {
                noviBrojRacuna = Convert.ToInt16(citac8["COUNT(*)"].ToString());
            }
            konekcija.Close();

            if (noviBrojRacuna != prethodniBrojRacuna)
            {
                this.prethodniBrojRacuna = noviBrojRacuna;
                suma = 0;
                listaRacuna.Items.Clear();
                listaRacuna.Items.Add("ID\t\tDATUM\t\t\t\tNAPLAĆENO");
                listaRacuna.Items.Add("");

                konekcija2.Open();
                var komanda6 = konekcija2.CreateCommand();
                komanda6.CommandText = "SELECT * FROM racun WHERE ClanBiblioteke_idClanaBiblioteke=" + idClanaBibliteke + "";
                var citac6 = komanda6.ExecuteReader();
                while (citac6.Read())
                {
                    var ddd = citac6["datumIzdavanja"];
                    string unos = citac6["idracun"].ToString() + "\t\t" + citac6["datumIzdavanja"].ToString() + "\t\t\t\t" + citac6["zaNaplatu"].ToString() + "";
                    suma = suma + Convert.ToInt16(citac6["zaNaplatu"].ToString());
                    listaRacuna.Items.Add(unos);
                }
                labelaSuma.Text = "Suma računa koje sam ostvario: " + suma + " KM";
                konekcija2.Close();
            }
        }

        private void comboPrikaz_SelectedIndexChanged(object sender, EventArgs e)
        {
            prikazi.Items.Clear();

            if (cbIzdato.Checked == true && cbNijeIzdato.Checked == false)
            {
                prikazi.Items.Clear();
                if (comboPrikaz.SelectedItem.ToString() == "Pisac")
                {
                    prikazi.Items.Add("ID KNJIGE\tPISAC\t\tNAZIV\t\t\tCIJENA\tGODINA\tIZNAJMLJENO");
                    prikazi.Items.Add("");

                    konekcija.Open();
                    var komanda = konekcija.CreateCommand();
                    komanda.CommandText = "SELECT k.idKnjige, p.imePisac, k.naslovKnjige, k.cenaIznajmljivanja, k.godinaIzdavanja, k.izdato FROM pisac p, napisao n, knjiga k WHERE k.idKnjige=n.Knjiga_idKnjige AND p.idPisac=n.Pisac_idPisac";
                    var citac = komanda.ExecuteReader();

                    while (citac.Read())
                    {
                        string izn = "Nije iznajmljena";
                        if (citac["izdato"].ToString().ToLower() == "true")
                        {
                            izn = "Iznajmljena";
                        }
                        string unos = citac["idKnjige"].ToString() + "\t\t" + citac["imePisac"].ToString() + "\t\t" + citac["naslovKnjige"].ToString() + "\t\t\t" + citac["cenaIznajmljivanja"].ToString() + "\t" + citac["godinaIzdavanja"].ToString() + "\t" + izn;
                        if (citac["izdato"].ToString().ToLower() == "true")
                        {
                            prikazi.Items.Add(unos);
                        }
                    }
                    konekcija.Close();
                }
                else if (comboPrikaz.SelectedItem.ToString() == "- -")
                {
                    konekcija.Open();
                    var komanda = konekcija.CreateCommand();
                    komanda.CommandText = "SELECT * FROM knjiga";
                    var citac = komanda.ExecuteReader();

                    prikazi.Items.Add("ID\t\tNAZIV\t\t\t\tCIJENA\t\tGODINA\t\tIZNAJMLJENO");
                    prikazi.Items.Add("");

                    while (citac.Read())
                    {
                        string izn = "Nije iznajmljena";
                        if (citac["izdato"].ToString().ToLower() == "true")
                        {
                            izn = "Iznajmljena";
                        }
                        string unos = citac["idKnjige"].ToString() + "\t\t" + citac["naslovKnjige"].ToString() + "\t\t\t\t" + citac["cenaIznajmljivanja"].ToString() + "\t\t" + citac["godinaIzdavanja"].ToString() + "\t\t" + izn;
                        if (citac["izdato"].ToString().ToLower() == "true")
                        {
                            prikazi.Items.Add(unos);
                        }
                    }
                    konekcija.Close();
                }
                else if (comboPrikaz.SelectedItem.ToString() == "Kategorija")
                {
                    prikazi.Items.Add("ID KNJIGE\tKATEGORIJA\t\tNAZIV\t\t\tCIJENA\tGODINA\tIZNAJMLJENO");
                    prikazi.Items.Add("");

                    konekcija.Open();
                    var komanda = konekcija.CreateCommand();
                    komanda.CommandText = "SELECT k.idKnjige, ka.nazivKategorije, k.naslovKnjige, k.cenaIznajmljivanja, k.godinaIzdavanja, k.izdato FROM kategorija ka, pripada p, knjiga k WHERE k.idKnjige=p.Knjiga_idKnjige AND p.Kategorija_idKategorija=ka.idKategorija";
                    var citac = komanda.ExecuteReader();

                    while (citac.Read())
                    {
                        string izn = "Nije iznajmljena";
                        if (citac["izdato"].ToString().ToLower() == "true")
                        {
                            izn = "Iznajmljena";
                        }
                        string unos = citac["idKnjige"].ToString() + "\t\t" + citac["nazivKategorije"].ToString() + "        \t\t" + citac["naslovKnjige"].ToString() + "\t\t\t" + citac["cenaIznajmljivanja"].ToString() + "\t" + citac["godinaIzdavanja"].ToString() + "\t" + izn;
                        if (citac["izdato"].ToString().ToLower() == "true")
                        {
                            prikazi.Items.Add(unos);
                        }
                    }
                    konekcija.Close();
                }
                else if (comboPrikaz.SelectedItem.ToString() == "Izdavač")
                {
                    prikazi.Items.Add("ID KNJIGE\tIZDAVAČ\t\tNAZIV\t\t\tCIJENA\tGODINA\tIZNAJMLJENO");
                    prikazi.Items.Add("");

                    konekcija.Open();
                    var komanda = konekcija.CreateCommand();
                    komanda.CommandText = "SELECT k.idKnjige, iz.nazivIzdavaca, k.naslovKnjige, k.cenaIznajmljivanja, k.godinaIzdavanja, k.izdato FROM izdaje i, izdavac iz, knjiga k WHERE k.idKnjige=i.Knjiga_idKnjige AND iz.idIzdavaca=i.Izdavac_idIzdavaca";
                    var citac = komanda.ExecuteReader();

                    while (citac.Read())
                    {
                        string izn = "Nije iznajmljena";
                        if (citac["izdato"].ToString().ToLower() == "true")
                        {
                            izn = "Iznajmljena";
                        }
                        string unos = citac["idKnjige"].ToString() + "\t\t" + citac["nazivIzdavaca"].ToString() + "\t\t" + citac["naslovKnjige"].ToString() + "\t\t\t" + citac["cenaIznajmljivanja"].ToString() + "\t" + citac["godinaIzdavanja"].ToString() + "\t" + izn;
                        if (citac["izdato"].ToString().ToLower() == "true")
                        {
                            prikazi.Items.Add(unos);
                        }
                    }
                    konekcija.Close();
                }
            }
            else if (cbIzdato.Checked == false && cbNijeIzdato.Checked == true)
            {
                if (comboPrikaz.SelectedItem.ToString() == "Pisac")
                {
                    prikazi.Items.Add("ID KNJIGE\tPISAC\t\tNAZIV\t\t\tCIJENA\tGODINA\tIZNAJMLJENO");
                    prikazi.Items.Add("");

                    konekcija.Open();
                    var komanda = konekcija.CreateCommand();
                    komanda.CommandText = "SELECT k.idKnjige, p.imePisac, k.naslovKnjige, k.cenaIznajmljivanja, k.godinaIzdavanja, k.izdato FROM pisac p, napisao n, knjiga k WHERE k.idKnjige=n.Knjiga_idKnjige AND p.idPisac=n.Pisac_idPisac";
                    var citac = komanda.ExecuteReader();

                    while (citac.Read())
                    {
                        string izn = "Nije iznajmljena";
                        if (citac["izdato"].ToString().ToLower() == "true")
                        {
                            izn = "Iznajmljena";
                        }
                        string unos = citac["idKnjige"].ToString() + "\t\t" + citac["imePisac"].ToString() + "\t\t" + citac["naslovKnjige"].ToString() + "\t\t\t" + citac["cenaIznajmljivanja"].ToString() + "\t" + citac["godinaIzdavanja"].ToString() + "\t" + izn;
                        if (citac["izdato"].ToString().ToLower() != "true")
                        {
                            prikazi.Items.Add(unos);
                        }
                    }
                    konekcija.Close();
                }
                else if (comboPrikaz.SelectedItem.ToString() == "- -")
                {
                    konekcija.Open();
                    var komanda = konekcija.CreateCommand();
                    komanda.CommandText = "SELECT * FROM knjiga";
                    var citac = komanda.ExecuteReader();

                    prikazi.Items.Add("ID\t\tNAZIV\t\t\t\tCIJENA\t\tGODINA\t\tIZNAJMLJENO");
                    prikazi.Items.Add("");

                    while (citac.Read())
                    {
                        string izn = "Nije iznajmljena";
                        if (citac["izdato"].ToString().ToLower() == "true")
                        {
                            izn = "Iznajmljena";
                        }
                        string unos = citac["idKnjige"].ToString() + "\t\t" + citac["naslovKnjige"].ToString() + "\t\t\t\t" + citac["cenaIznajmljivanja"].ToString() + "\t\t" + citac["godinaIzdavanja"].ToString() + "\t\t" + izn;
                        if (citac["izdato"].ToString().ToLower() != "true")
                        {
                            prikazi.Items.Add(unos);
                        }
                    }
                    konekcija.Close();
                }
                else if (comboPrikaz.SelectedItem.ToString() == "Kategorija")
                {
                    prikazi.Items.Add("ID KNJIGE\tKATEGORIJA\t\tNAZIV\t\t\tCIJENA\tGODINA\tIZNAJMLJENO");
                    prikazi.Items.Add("");

                    konekcija.Open();
                    var komanda = konekcija.CreateCommand();
                    komanda.CommandText = "SELECT k.idKnjige, ka.nazivKategorije, k.naslovKnjige, k.cenaIznajmljivanja, k.godinaIzdavanja, k.izdato FROM kategorija ka, pripada p, knjiga k WHERE k.idKnjige=p.Knjiga_idKnjige AND p.Kategorija_idKategorija=ka.idKategorija";
                    var citac = komanda.ExecuteReader();

                    while (citac.Read())
                    {
                        string izn = "Nije iznajmljena";
                        if (citac["izdato"].ToString().ToLower() == "true")
                        {
                            izn = "Iznajmljena";
                        }
                        string unos = citac["idKnjige"].ToString() + "\t\t" + citac["nazivKategorije"].ToString() + "        \t\t" + citac["naslovKnjige"].ToString() + "\t\t\t" + citac["cenaIznajmljivanja"].ToString() + "\t" + citac["godinaIzdavanja"].ToString() + "\t" + izn;
                        if (citac["izdato"].ToString().ToLower() != "true")
                        {
                            prikazi.Items.Add(unos);
                        }
                    }
                    konekcija.Close();
                }
                else if (comboPrikaz.SelectedItem.ToString() == "Izdavač")
                {
                    prikazi.Items.Add("ID KNJIGE\tIZDAVAČ\t\tNAZIV\t\t\tCIJENA\tGODINA\tIZNAJMLJENO");
                    prikazi.Items.Add("");

                    konekcija.Open();
                    var komanda = konekcija.CreateCommand();
                    komanda.CommandText = "SELECT k.idKnjige, iz.nazivIzdavaca, k.naslovKnjige, k.cenaIznajmljivanja, k.godinaIzdavanja, k.izdato FROM izdaje i, izdavac iz, knjiga k WHERE k.idKnjige=i.Knjiga_idKnjige AND iz.idIzdavaca=i.Izdavac_idIzdavaca";
                    var citac = komanda.ExecuteReader();

                    while (citac.Read())
                    {
                        string izn = "Nije iznajmljena";
                        if (citac["izdato"].ToString().ToLower() == "true")
                        {
                            izn = "Iznajmljena";
                        }
                        string unos = citac["idKnjige"].ToString() + "\t\t" + citac["nazivIzdavaca"].ToString() + "\t\t" + citac["naslovKnjige"].ToString() + "\t\t\t" + citac["cenaIznajmljivanja"].ToString() + "\t" + citac["godinaIzdavanja"].ToString() + "\t" + izn;
                        if (citac["izdato"].ToString().ToLower() != "true")
                        {
                            prikazi.Items.Add(unos);
                        }
                    }
                    konekcija.Close();
                }
            }
            else if (cbIzdato.Checked == true && cbNijeIzdato.Checked == true)
            {
                if (comboPrikaz.SelectedItem.ToString() == "Pisac")
                {
                    prikazi.Items.Add("ID KNJIGE\tPISAC\t\tNAZIV\t\t\tCIJENA\tGODINA\tIZNAJMLJENO");
                    prikazi.Items.Add("");

                    konekcija.Open();
                    var komanda = konekcija.CreateCommand();
                    komanda.CommandText = "SELECT k.idKnjige, p.imePisac, k.naslovKnjige, k.cenaIznajmljivanja, k.godinaIzdavanja, k.izdato FROM pisac p, napisao n, knjiga k WHERE k.idKnjige=n.Knjiga_idKnjige AND p.idPisac=n.Pisac_idPisac";
                    var citac = komanda.ExecuteReader();

                    while (citac.Read())
                    {
                        string izn = "Nije iznajmljena";
                        if (citac["izdato"].ToString().ToLower() == "true")
                        {
                            izn = "Iznajmljena";
                        }
                        string unos = citac["idKnjige"].ToString() + "\t\t" + citac["imePisac"].ToString() + "\t\t" + citac["naslovKnjige"].ToString() + "\t\t\t" + citac["cenaIznajmljivanja"].ToString() + "\t" + citac["godinaIzdavanja"].ToString() + "\t" + izn;
                        prikazi.Items.Add(unos);
                    }
                    konekcija.Close();
                }
                else if (comboPrikaz.SelectedItem.ToString() == "- -")
                {
                    konekcija.Open();
                    var komanda = konekcija.CreateCommand();
                    komanda.CommandText = "SELECT * FROM knjiga";
                    var citac = komanda.ExecuteReader();

                    prikazi.Items.Add("ID\t\tNAZIV\t\t\t\tCIJENA\t\tGODINA\t\tIZNAJMLJENO");
                    prikazi.Items.Add("");

                    while (citac.Read())
                    {
                        string izn = "Nije iznajmljena";
                        if (citac["izdato"].ToString().ToLower() == "true")
                        {
                            izn = "Iznajmljena";
                        }
                        string unos = citac["idKnjige"].ToString() + "\t\t" + citac["naslovKnjige"].ToString() + "\t\t\t\t" + citac["cenaIznajmljivanja"].ToString() + "\t\t" + citac["godinaIzdavanja"].ToString() + "\t\t" + izn;
                        prikazi.Items.Add(unos);
                    }
                    konekcija.Close();
                }
                else if (comboPrikaz.SelectedItem.ToString() == "Kategorija")
                {
                    prikazi.Items.Add("ID KNJIGE\tKATEGORIJA\t\tNAZIV\t\t\tCIJENA\tGODINA\tIZNAJMLJENO");
                    prikazi.Items.Add("");

                    konekcija.Open();
                    var komanda = konekcija.CreateCommand();
                    komanda.CommandText = "SELECT k.idKnjige, ka.nazivKategorije, k.naslovKnjige, k.cenaIznajmljivanja, k.godinaIzdavanja, k.izdato FROM kategorija ka, pripada p, knjiga k WHERE k.idKnjige=p.Knjiga_idKnjige AND p.Kategorija_idKategorija=ka.idKategorija";
                    var citac = komanda.ExecuteReader();

                    while (citac.Read())
                    {
                        string izn = "Nije iznajmljena";
                        if (citac["izdato"].ToString().ToLower() == "true")
                        {
                            izn = "Iznajmljena";
                        }
                        string unos = citac["idKnjige"].ToString() + "\t\t" + citac["nazivKategorije"].ToString() + "        \t\t" + citac["naslovKnjige"].ToString() + "\t\t\t" + citac["cenaIznajmljivanja"].ToString() + "\t" + citac["godinaIzdavanja"].ToString() + "\t" + izn;
                        prikazi.Items.Add(unos);
                    }
                    konekcija.Close();
                }
                else if (comboPrikaz.SelectedItem.ToString() == "Izdavač")
                {
                    prikazi.Items.Add("ID KNJIGE\tIZDAVAČ\t\tNAZIV\t\t\tCIJENA\tGODINA\tIZNAJMLJENO");
                    prikazi.Items.Add("");

                    konekcija.Open();
                    var komanda = konekcija.CreateCommand();
                    komanda.CommandText = "SELECT k.idKnjige, iz.nazivIzdavaca, k.naslovKnjige, k.cenaIznajmljivanja, k.godinaIzdavanja, k.izdato FROM izdaje i, izdavac iz, knjiga k WHERE k.idKnjige=i.Knjiga_idKnjige AND iz.idIzdavaca=i.Izdavac_idIzdavaca";
                    var citac = komanda.ExecuteReader();

                    while (citac.Read())
                    {
                        string izn = "Nije iznajmljena";
                        if (citac["izdato"].ToString().ToLower() == "true")
                        {
                            izn = "Iznajmljena";
                        }
                        string unos = citac["idKnjige"].ToString() + "\t\t" + citac["nazivIzdavaca"].ToString() + "\t\t" + citac["naslovKnjige"].ToString() + "\t\t\t" + citac["cenaIznajmljivanja"].ToString() + "\t" + citac["godinaIzdavanja"].ToString() + "\t" + izn;
                        prikazi.Items.Add(unos);
                    }
                    konekcija.Close();
                }
            }
            else
            {
                if (comboPrikaz.SelectedItem.ToString() == "Pisac")
                {
                    prikazi.Items.Add("ID KNJIGE\tPISAC\t\tNAZIV\t\t\tCIJENA\tGODINA\tIZNAJMLJENO");
                    prikazi.Items.Add("");
                }
                else if (comboPrikaz.SelectedItem.ToString() == "Kategorija")
                {
                    prikazi.Items.Add("ID KNJIGE\tKATEGORIJA\t\tNAZIV\t\t\tCIJENA\tGODINA\tIZNAJMLJENO");
                    prikazi.Items.Add("");
                }
                else if (comboPrikaz.SelectedItem.ToString() == "Izdavač")
                {
                    prikazi.Items.Add("ID KNJIGE\tIZDAVAČ\t\tNAZIV\t\t\tCIJENA\tGODINA\tIZNAJMLJENO");
                    prikazi.Items.Add("");
                }
                else if (comboPrikaz.SelectedItem.ToString() == "- -")
                {
                    prikazi.Items.Add("ID\t\tNAZIV\t\t\t\tCIJENA\t\tGODINA\t\tIZNAJMLJENO");
                    prikazi.Items.Add("");
                }
            }
        }

        private void btnProduzi_Click(object sender, EventArgs e)
        {
            labelaClanarine.Text = "";
            MessageBox.Show("Vaš dug po računu je uvećan za vrednost 10 KM.","Inkrement stanja računa");

            konekcija.Open();
            var komanda4 = konekcija.CreateCommand();
            string datum = DateTime.Now.ToString("u").Replace("/", "-");

            komanda4.CommandText = "INSERT INTO racun(datumIzdavanja,zaNaplatu,ClanBiblioteke_idClanaBiblioteke, naplaceno) VALUES ('" + datum + "',10," + idClanaBibliteke + ","+false+")";
            komanda4.ExecuteNonQuery();
            konekcija.Close();

            listaRacuna.Items.Clear();
            listaRacuna.Items.Add("ID\t\tDATUM\t\t\t\tNAPLAĆENO");
            listaRacuna.Items.Add("");

            int suma = 0;
            konekcija2.Open();
            var komanda6 = konekcija2.CreateCommand();
            komanda6.CommandText = "SELECT * FROM racun WHERE ClanBiblioteke_idClanaBiblioteke=" + idClanaBibliteke + "";
            var citac6 = komanda6.ExecuteReader();
            while (citac6.Read())
            {
                string unos = citac6["idracun"].ToString() + "\t\t" + citac6["datumIzdavanja"].ToString() + "\t\t\t\t" + citac6["zaNaplatu"].ToString() + "";
                suma = suma + Convert.ToInt16(citac6["zaNaplatu"].ToString());
                listaRacuna.Items.Add(unos);
            }
            konekcija2.Close();

            labelaSuma.Text = "Suma računa koje sam ostvario: "+suma+" KM";

            konekcija.Open();
            var komanda2 = konekcija.CreateCommand();
            datum = datum.Replace("10", "11");
            komanda2.CommandText = "UPDATE clanbiblioteke SET trajanjeClanarine='"+datum+"' WHERE idClanaBiblioteke=" + idClanaBibliteke + "";
            komanda2.ExecuteNonQuery();
            konekcija.Close();

            konekcija.Open();
            var komanda1 = konekcija.CreateCommand();
            komanda1.CommandText = "SELECT * FROM clanbiblioteke WHERE Korisnici_idKorisnici=" + idKorisnici + "";
            var citac1 = komanda1.ExecuteReader();
            citac1.Read();
            labelaClanarine.Text = "Aplikaciju možete\nkoristiti do:\n" + citac1["trajanjeClanarine"].ToString();
            konekcija.Close();

            btnProduzi.Enabled = false;
            tabovi.Enabled = true;
        }

        private void timerRefreser_Tick(object sender, EventArgs e)
        {
            try
            {
                Refresovanje();
            }
            catch
            { 
            
            }
        }

        private void UlogovanoStanje2_FormClosing(object sender, FormClosingEventArgs e)
        {
            timerRefreser.Stop();
        }

        private void btnPrimjedba_Click(object sender, EventArgs e)
        {
            Primjedba primjedba = new Primjedba(idClanaBibliteke, idKorisnici, ime, prezime);
            primjedba.Show();
        }

        private void knjigeRefres()
        {
            prikazi.Items.Clear();

            if (cbIzdato.Checked == true && cbNijeIzdato.Checked == false)
            {
                prikazi.Items.Clear();
                if (comboPrikaz.SelectedItem.ToString() == "Pisac")
                {
                    prikazi.Items.Add("ID KNJIGE\tPISAC\t\tNAZIV\t\t\tCIJENA\tGODINA\tIZNAJMLJENO");
                    prikazi.Items.Add("");

                    konekcija.Open();
                    var komanda = konekcija.CreateCommand();
                    komanda.CommandText = "SELECT k.idKnjige, p.imePisac, k.naslovKnjige, k.cenaIznajmljivanja, k.godinaIzdavanja, k.izdato FROM pisac p, napisao n, knjiga k WHERE k.idKnjige=n.Knjiga_idKnjige AND p.idPisac=n.Pisac_idPisac";
                    var citac = komanda.ExecuteReader();

                    while (citac.Read())
                    {
                        string izn = "Nije iznajmljena";
                        if (citac["izdato"].ToString().ToLower() == "true")
                        {
                            izn = "Iznajmljena";
                        }
                        string unos = citac["idKnjige"].ToString() + "\t\t" + citac["imePisac"].ToString() + "\t\t" + citac["naslovKnjige"].ToString() + "\t\t\t" + citac["cenaIznajmljivanja"].ToString() + "\t" + citac["godinaIzdavanja"].ToString() + "\t" + izn;
                        if (citac["izdato"].ToString().ToLower() == "true")
                        {
                            prikazi.Items.Add(unos);
                        }
                    }
                    konekcija.Close();
                }
                else if (comboPrikaz.SelectedItem.ToString() == "- -")
                {
                    konekcija.Open();
                    var komanda = konekcija.CreateCommand();
                    komanda.CommandText = "SELECT * FROM knjiga";
                    var citac = komanda.ExecuteReader();

                    prikazi.Items.Add("ID\t\tNAZIV\t\t\t\tCIJENA\t\tGODINA\t\tIZNAJMLJENO");
                    prikazi.Items.Add("");

                    while (citac.Read())
                    {
                        string izn = "Nije iznajmljena";
                        if (citac["izdato"].ToString().ToLower() == "true")
                        {
                            izn = "Iznajmljena";
                        }
                        string unos = citac["idKnjige"].ToString() + "\t\t" + citac["naslovKnjige"].ToString() + "\t\t\t\t" + citac["cenaIznajmljivanja"].ToString() + "\t\t" + citac["godinaIzdavanja"].ToString() + "\t\t" + izn;
                        if (citac["izdato"].ToString().ToLower() == "true")
                        {
                            prikazi.Items.Add(unos);
                        }
                    }
                    konekcija.Close();
                }
                else if (comboPrikaz.SelectedItem.ToString() == "Kategorija")
                {
                    prikazi.Items.Add("ID KNJIGE\tKATEGORIJA\t\tNAZIV\t\t\tCIJENA\tGODINA\tIZNAJMLJENO");
                    prikazi.Items.Add("");

                    konekcija.Open();
                    var komanda = konekcija.CreateCommand();
                    komanda.CommandText = "SELECT k.idKnjige, ka.nazivKategorije, k.naslovKnjige, k.cenaIznajmljivanja, k.godinaIzdavanja, k.izdato FROM kategorija ka, pripada p, knjiga k WHERE k.idKnjige=p.Knjiga_idKnjige AND p.Kategorija_idKategorija=ka.idKategorija";
                    var citac = komanda.ExecuteReader();

                    while (citac.Read())
                    {
                        string izn = "Nije iznajmljena";
                        if (citac["izdato"].ToString().ToLower() == "true")
                        {
                            izn = "Iznajmljena";
                        }
                        string unos = citac["idKnjige"].ToString() + "\t\t" + citac["nazivKategorije"].ToString() + "        \t\t" + citac["naslovKnjige"].ToString() + "\t\t\t" + citac["cenaIznajmljivanja"].ToString() + "\t" + citac["godinaIzdavanja"].ToString() + "\t" + izn;
                        if (citac["izdato"].ToString().ToLower() == "true")
                        {
                            prikazi.Items.Add(unos);
                        }
                    }
                    konekcija.Close();
                }
                else if (comboPrikaz.SelectedItem.ToString() == "Izdavač")
                {
                    prikazi.Items.Add("ID KNJIGE\tIZDAVAČ\t\tNAZIV\t\t\tCIJENA\tGODINA\tIZNAJMLJENO");
                    prikazi.Items.Add("");

                    konekcija.Open();
                    var komanda = konekcija.CreateCommand();
                    komanda.CommandText = "SELECT k.idKnjige, iz.nazivIzdavaca, k.naslovKnjige, k.cenaIznajmljivanja, k.godinaIzdavanja, k.izdato FROM izdaje i, izdavac iz, knjiga k WHERE k.idKnjige=i.Knjiga_idKnjige AND iz.idIzdavaca=i.Izdavac_idIzdavaca";
                    var citac = komanda.ExecuteReader();

                    while (citac.Read())
                    {
                        string izn = "Nije iznajmljena";
                        if (citac["izdato"].ToString().ToLower() == "true")
                        {
                            izn = "Iznajmljena";
                        }
                        string unos = citac["idKnjige"].ToString() + "\t\t" + citac["nazivIzdavaca"].ToString() + "\t\t" + citac["naslovKnjige"].ToString() + "\t\t\t" + citac["cenaIznajmljivanja"].ToString() + "\t" + citac["godinaIzdavanja"].ToString() + "\t" + izn;
                        if (citac["izdato"].ToString().ToLower() == "true")
                        {
                            prikazi.Items.Add(unos);
                        }
                    }
                    konekcija.Close();
                }
            }
            else if (cbIzdato.Checked == false && cbNijeIzdato.Checked == true)
            {
                if (comboPrikaz.SelectedItem.ToString() == "Pisac")
                {
                    prikazi.Items.Add("ID KNJIGE\tPISAC\t\tNAZIV\t\t\tCIJENA\tGODINA\tIZNAJMLJENO");
                    prikazi.Items.Add("");

                    konekcija.Open();
                    var komanda = konekcija.CreateCommand();
                    komanda.CommandText = "SELECT k.idKnjige, p.imePisac, k.naslovKnjige, k.cenaIznajmljivanja, k.godinaIzdavanja, k.izdato FROM pisac p, napisao n, knjiga k WHERE k.idKnjige=n.Knjiga_idKnjige AND p.idPisac=n.Pisac_idPisac";
                    var citac = komanda.ExecuteReader();

                    while (citac.Read())
                    {
                        string izn = "Nije iznajmljena";
                        if (citac["izdato"].ToString().ToLower() == "true")
                        {
                            izn = "Iznajmljena";
                        }
                        string unos = citac["idKnjige"].ToString() + "\t\t" + citac["imePisac"].ToString() + "\t\t" + citac["naslovKnjige"].ToString() + "\t\t\t" + citac["cenaIznajmljivanja"].ToString() + "\t" + citac["godinaIzdavanja"].ToString() + "\t" + izn;
                        if (citac["izdato"].ToString().ToLower() != "true")
                        {
                            prikazi.Items.Add(unos);
                        }
                    }
                    konekcija.Close();
                }
                else if (comboPrikaz.SelectedItem.ToString() == "- -")
                {
                    konekcija.Open();
                    var komanda = konekcija.CreateCommand();
                    komanda.CommandText = "SELECT * FROM knjiga";
                    var citac = komanda.ExecuteReader();

                    prikazi.Items.Add("ID\t\tNAZIV\t\t\t\tCIJENA\t\tGODINA\t\tIZNAJMLJENO");
                    prikazi.Items.Add("");

                    while (citac.Read())
                    {
                        string izn = "Nije iznajmljena";
                        if (citac["izdato"].ToString().ToLower() == "true")
                        {
                            izn = "Iznajmljena";
                        }
                        string unos = citac["idKnjige"].ToString() + "\t\t" + citac["naslovKnjige"].ToString() + "\t\t\t\t" + citac["cenaIznajmljivanja"].ToString() + "\t\t" + citac["godinaIzdavanja"].ToString() + "\t\t" + izn;
                        if (citac["izdato"].ToString().ToLower() != "true")
                        {
                            prikazi.Items.Add(unos);
                        }
                    }
                    konekcija.Close();
                }
                else if (comboPrikaz.SelectedItem.ToString() == "Kategorija")
                {
                    prikazi.Items.Add("ID KNJIGE\tKATEGORIJA\t\tNAZIV\t\t\tCIJENA\tGODINA\tIZNAJMLJENO");
                    prikazi.Items.Add("");

                    konekcija.Open();
                    var komanda = konekcija.CreateCommand();
                    komanda.CommandText = "SELECT k.idKnjige, ka.nazivKategorije, k.naslovKnjige, k.cenaIznajmljivanja, k.godinaIzdavanja, k.izdato FROM kategorija ka, pripada p, knjiga k WHERE k.idKnjige=p.Knjiga_idKnjige AND p.Kategorija_idKategorija=ka.idKategorija";
                    var citac = komanda.ExecuteReader();

                    while (citac.Read())
                    {
                        string izn = "Nije iznajmljena";
                        if (citac["izdato"].ToString().ToLower() == "true")
                        {
                            izn = "Iznajmljena";
                        }
                        string unos = citac["idKnjige"].ToString() + "\t\t" + citac["nazivKategorije"].ToString() + "        \t\t" + citac["naslovKnjige"].ToString() + "\t\t\t" + citac["cenaIznajmljivanja"].ToString() + "\t" + citac["godinaIzdavanja"].ToString() + "\t" + izn;
                        if (citac["izdato"].ToString().ToLower() != "true")
                        {
                            prikazi.Items.Add(unos);
                        }
                    }
                    konekcija.Close();
                }
                else if (comboPrikaz.SelectedItem.ToString() == "Izdavač")
                {
                    prikazi.Items.Add("ID KNJIGE\tIZDAVAČ\t\tNAZIV\t\t\tCIJENA\tGODINA\tIZNAJMLJENO");
                    prikazi.Items.Add("");

                    konekcija.Open();
                    var komanda = konekcija.CreateCommand();
                    komanda.CommandText = "SELECT k.idKnjige, iz.nazivIzdavaca, k.naslovKnjige, k.cenaIznajmljivanja, k.godinaIzdavanja, k.izdato FROM izdaje i, izdavac iz, knjiga k WHERE k.idKnjige=i.Knjiga_idKnjige AND iz.idIzdavaca=i.Izdavac_idIzdavaca";
                    var citac = komanda.ExecuteReader();

                    while (citac.Read())
                    {
                        string izn = "Nije iznajmljena";
                        if (citac["izdato"].ToString().ToLower() == "true")
                        {
                            izn = "Iznajmljena";
                        }
                        string unos = citac["idKnjige"].ToString() + "\t\t" + citac["nazivIzdavaca"].ToString() + "\t\t" + citac["naslovKnjige"].ToString() + "\t\t\t" + citac["cenaIznajmljivanja"].ToString() + "\t" + citac["godinaIzdavanja"].ToString() + "\t" + izn;
                        if (citac["izdato"].ToString().ToLower() != "true")
                        {
                            prikazi.Items.Add(unos);
                        }
                    }
                    konekcija.Close();
                }
            }
            else if (cbIzdato.Checked == true && cbNijeIzdato.Checked == true)
            {
                if (comboPrikaz.SelectedItem.ToString() == "Pisac")
                {
                    prikazi.Items.Add("ID KNJIGE\tPISAC\t\tNAZIV\t\t\tCIJENA\tGODINA\tIZNAJMLJENO");
                    prikazi.Items.Add("");

                    konekcija.Open();
                    var komanda = konekcija.CreateCommand();
                    komanda.CommandText = "SELECT k.idKnjige, p.imePisac, k.naslovKnjige, k.cenaIznajmljivanja, k.godinaIzdavanja, k.izdato FROM pisac p, napisao n, knjiga k WHERE k.idKnjige=n.Knjiga_idKnjige AND p.idPisac=n.Pisac_idPisac";
                    var citac = komanda.ExecuteReader();

                    while (citac.Read())
                    {
                        string izn = "Nije iznajmljena";
                        if (citac["izdato"].ToString().ToLower() == "true")
                        {
                            izn = "Iznajmljena";
                        }
                        string unos = citac["idKnjige"].ToString() + "\t\t" + citac["imePisac"].ToString() + "\t\t" + citac["naslovKnjige"].ToString() + "\t\t\t" + citac["cenaIznajmljivanja"].ToString() + "\t" + citac["godinaIzdavanja"].ToString() + "\t" + izn;
                        prikazi.Items.Add(unos);
                    }
                    konekcija.Close();
                }
                else if (comboPrikaz.SelectedItem.ToString() == "- -")
                {
                    konekcija.Open();
                    var komanda = konekcija.CreateCommand();
                    komanda.CommandText = "SELECT * FROM knjiga";
                    var citac = komanda.ExecuteReader();

                    prikazi.Items.Add("ID\t\tNAZIV\t\t\t\tCIJENA\t\tGODINA\t\tIZNAJMLJENO");
                    prikazi.Items.Add("");

                    while (citac.Read())
                    {
                        string izn = "Nije iznajmljena";
                        if (citac["izdato"].ToString().ToLower() == "true")
                        {
                            izn = "Iznajmljena";
                        }
                        string unos = citac["idKnjige"].ToString() + "\t\t" + citac["naslovKnjige"].ToString() + "\t\t\t\t" + citac["cenaIznajmljivanja"].ToString() + "\t\t" + citac["godinaIzdavanja"].ToString() + "\t\t" + izn;
                        prikazi.Items.Add(unos);
                    }
                    konekcija.Close();
                }
                else if (comboPrikaz.SelectedItem.ToString() == "Kategorija")
                {
                    prikazi.Items.Add("ID KNJIGE\tKATEGORIJA\t\tNAZIV\t\t\tCIJENA\tGODINA\tIZNAJMLJENO");
                    prikazi.Items.Add("");

                    konekcija.Open();
                    var komanda = konekcija.CreateCommand();
                    komanda.CommandText = "SELECT k.idKnjige, ka.nazivKategorije, k.naslovKnjige, k.cenaIznajmljivanja, k.godinaIzdavanja, k.izdato FROM kategorija ka, pripada p, knjiga k WHERE k.idKnjige=p.Knjiga_idKnjige AND p.Kategorija_idKategorija=ka.idKategorija";
                    var citac = komanda.ExecuteReader();

                    while (citac.Read())
                    {
                        string izn = "Nije iznajmljena";
                        if (citac["izdato"].ToString().ToLower() == "true")
                        {
                            izn = "Iznajmljena";
                        }
                        string unos = citac["idKnjige"].ToString() + "\t\t" + citac["nazivKategorije"].ToString() + "        \t\t" + citac["naslovKnjige"].ToString() + "\t\t\t" + citac["cenaIznajmljivanja"].ToString() + "\t" + citac["godinaIzdavanja"].ToString() + "\t" + izn;
                        prikazi.Items.Add(unos);
                    }
                    konekcija.Close();
                }
                else if (comboPrikaz.SelectedItem.ToString() == "Izdavač")
                {
                    prikazi.Items.Add("ID KNJIGE\tIZDAVAČ\t\tNAZIV\t\t\tCIJENA\tGODINA\tIZNAJMLJENO");
                    prikazi.Items.Add("");

                    konekcija.Open();
                    var komanda = konekcija.CreateCommand();
                    komanda.CommandText = "SELECT k.idKnjige, iz.nazivIzdavaca, k.naslovKnjige, k.cenaIznajmljivanja, k.godinaIzdavanja, k.izdato FROM izdaje i, izdavac iz, knjiga k WHERE k.idKnjige=i.Knjiga_idKnjige AND iz.idIzdavaca=i.Izdavac_idIzdavaca";
                    var citac = komanda.ExecuteReader();

                    while (citac.Read())
                    {
                        string izn = "Nije iznajmljena";
                        if (citac["izdato"].ToString().ToLower() == "true")
                        {
                            izn = "Iznajmljena";
                        }
                        string unos = citac["idKnjige"].ToString() + "\t\t" + citac["nazivIzdavaca"].ToString() + "\t\t" + citac["naslovKnjige"].ToString() + "\t\t\t" + citac["cenaIznajmljivanja"].ToString() + "\t" + citac["godinaIzdavanja"].ToString() + "\t" + izn;
                        prikazi.Items.Add(unos);
                    }
                    konekcija.Close();
                }
            }
            else
            {
                if (comboPrikaz.SelectedItem.ToString() == "Pisac")
                {
                    prikazi.Items.Add("ID KNJIGE\tPISAC\t\tNAZIV\t\t\tCIJENA\tGODINA\tIZNAJMLJENO");
                    prikazi.Items.Add("");
                }
                else if (comboPrikaz.SelectedItem.ToString() == "Kategorija")
                {
                    prikazi.Items.Add("ID KNJIGE\tKATEGORIJA\t\tNAZIV\t\t\tCIJENA\tGODINA\tIZNAJMLJENO");
                    prikazi.Items.Add("");
                }
                else if (comboPrikaz.SelectedItem.ToString() == "Izdavač")
                {
                    prikazi.Items.Add("ID KNJIGE\tIZDAVAČ\t\tNAZIV\t\t\tCIJENA\tGODINA\tIZNAJMLJENO");
                    prikazi.Items.Add("");
                }
                else if (comboPrikaz.SelectedItem.ToString() == "- -")
                {
                    prikazi.Items.Add("ID\t\tNAZIV\t\t\t\tCIJENA\t\tGODINA\t\tIZNAJMLJENO");
                    prikazi.Items.Add("");
                }
            }
        }

        private void tabSveKnjige_Enter(object sender, EventArgs e)
        {
            if (prikazi.SelectedIndex > 1)
            {
                btnDodaj.Enabled = true;
            }
            else
            {
                btnDodaj.Enabled = false;
            }
        }

        private void tabSveKnjige_Leave(object sender, EventArgs e)
        {
            btnDodaj.Enabled = false;
        }

    }
}
