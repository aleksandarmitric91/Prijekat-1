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
    public partial class UlogovanoStanje : Form
    {
        int idKorisnici, idClana, prethodniBrojClanova, prethodniBrojKnjiga, prethodniBrojRacuna, prethodniBrojDetalja;
        string ime, prezime, odgovor;
        int prethodniBrojPisci, prethodniBrojKategorija, prethodniBrojIzdavac, prethodniBrojKorisnika;
  
        static string conString = "Server=localhost;Database=biblioteka;Uid=root;";
        MySqlConnection konekcija = new MySqlConnection(conString);

        public UlogovanoStanje(string ime,int id,string prezime,int tip)
        {
            this.idKorisnici = id;
            this.ime = ime;
            this.prezime = prezime;

            InitializeComponent();
            pocetnoPopunjavanje();

            timerProvjeraZahtjeva.Start();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            konekcija.Open();
            var komanda = konekcija.CreateCommand();
            komanda.CommandText="UPDATE korisnici SET ulogovan='"+false+"' WHERE idKorisnici="+ idKorisnici;
            komanda.ExecuteNonQuery();
            konekcija.Close();
            this.Close();
        }

        private void btnPodesavanja_Click(object sender, EventArgs e)
        {
            Podesavanja1 set = new Podesavanja1(idKorisnici);
            set.ShowDialog();
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

        private void pocetnoPopunjavanje()
        {
            labelKorisnik.Text = "Ulogovan korisnik:\n" + ime + " " + prezime + "\nkao administrator.";

            konekcija.Open();
            var komanda = konekcija.CreateCommand();
            komanda.CommandText = "SELECT * FROM knjiga";
            var citac = komanda.ExecuteReader();

            listaPrimjedbe.Items.Clear();
            listaKorisnici.Items.Clear();
            listaDetalji.Items.Clear();
            prikazi.Items.Clear();
            listaClanovi1.Items.Clear();
            listaClanovi2.Items.Clear();
            listaKnjige.Items.Clear();
            listaRacuni.Items.Clear();
            listaIzdavaci.Items.Clear();
            listaPisci.Items.Clear();
            listaKategorije.Items.Clear();

            prikazi.Items.Add("ID\t\tNAZIV\t\t\t\tCIJENA\t\tGODINA\t\tIZNAJMLJENO");
            prikazi.Items.Add("");
            listaClanovi1.Items.Add("ID CLANA\t\tIME\t\tPREZIME\t\tTRAJANJE CLANARINE\tBROJ TELEFONA");
            listaClanovi1.Items.Add("");
            listaClanovi2.Items.Add("ID CLANA\t\tIME\t\tPREZIME\t\tTRAJANJE CLANARINE\tBROJ TELEFONA");
            listaClanovi2.Items.Add("");
            listaKnjige.Items.Add("ID KNJIGE\tNAZIV\t\tGODINA");
            listaKnjige.Items.Add("");
            listaRacuni.Items.Add("ID RACUN\tDATUM\t\t\tNAPLAĆENO");
            listaRacuni.Items.Add("");

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

            konekcija.Open();
            var komanda1 = konekcija.CreateCommand();
            komanda1.CommandText = "SELECT * FROM clanbiblioteke c, korisnici k WHERE c.Korisnici_idKorisnici=k.idkorisnici";
            var citac1 = komanda1.ExecuteReader();
            while (citac1.Read())
            {
                string unos = citac1["idClanaBiblioteke"].ToString() + "\t\t" + citac1["imeKorisnika"].ToString() + "\t\t" + citac1["prezimeKorisnika"].ToString() + "\t\t" + citac1["trajanjeClanarine"].ToString() + "\t\t" + citac1["brojTelefona"].ToString();
                listaClanovi1.Items.Add(unos);
                listaClanovi2.Items.Add(unos);
            }
            konekcija.Close();

            listaDetalji.Items.Add("ID CLANA\t\tTRAJANJE ČLANARINE\tIME I PREZIME\tBROJ TELEFONA\t\tADRESA STANOVANJA");
            listaDetalji.Items.Add("");

            konekcija.Open();
            var komanda3 = konekcija.CreateCommand();
            komanda3.CommandText = "SELECT * FROM clanbiblioteke c, korisnici k, adresa a WHERE c.Korisnici_idKorisnici=k.idKorisnici AND k.Adresa_idAdresa=a.idAdresa";
            var citac3 = komanda3.ExecuteReader();
            while (citac3.Read())
            {
                string unos = citac3["idClanaBiblioteke"].ToString() + "\t\t" + citac3["trajanjeClanarine"].ToString() +
                    "\t\t" + citac3["imeKorisnika"].ToString() + " " + citac3["prezimeKorisnika"].ToString() + "\t" +
                    citac3["brojTelefona"].ToString() + "\t\t" + citac3["Grad"].ToString() + ", " + citac3["Ulica"].ToString() + " " + citac3["Broj"].ToString();
                listaDetalji.Items.Add(unos);
            }
            konekcija.Close();

            konekcija.Open();
            var komanda4 = konekcija.CreateCommand();
            komanda4.CommandText = "SELECT * FROM kategorija";
            var citac4 = komanda4.ExecuteReader();
            while (citac4.Read())
            {
                string unos = citac4["idKategorija"].ToString() + "\t" + citac4["nazivKategorije"].ToString();
                listaKategorije.Items.Add(unos);
            }
            konekcija.Close();

            konekcija.Open();
            var komanda5 = konekcija.CreateCommand();
            komanda5.CommandText = "SELECT * FROM pisac";
            var citac5 = komanda5.ExecuteReader();
            while (citac5.Read())
            {
                string unos = citac5["idPisac"].ToString() + "\t" + citac5["imePisac"].ToString();
                listaPisci.Items.Add(unos);
            }
            konekcija.Close();

            konekcija.Open();
            var komanda6 = konekcija.CreateCommand();
            komanda6.CommandText = "SELECT * FROM izdavac";
            var citac6 = komanda6.ExecuteReader();
            while (citac6.Read())
            {
                string unos = citac6["idIzdavaca"].ToString() + "\t" + citac6["nazivIzdavaca"].ToString();
                listaIzdavaci.Items.Add(unos);
            }
            konekcija.Close();

            listaKorisnici.Items.Add("ID KORISNIKA\tIME I PREZIME\tBROJ TELEFONA\tUSERNAME\tPASSWORD\tTIP KORISNIKA\tADRESA\t\t\tSTANJE");
            listaKorisnici.Items.Add("");

            konekcija.Open();
            var komanda7 = konekcija.CreateCommand();
            komanda7.CommandText = "SELECT * FROM korisnici k,adresa a WHERE k.Adresa_idAdresa=a.idAdresa";
            var citac7 = komanda7.ExecuteReader();
            while (citac7.Read())
            {
                string tipKorisnika, stanje, dodatak;
                if(citac7["tipKorisnika"].ToString()=="0")
                {
                    tipKorisnika="Administrator";
                }
                else if(citac7["tipKorisnika"].ToString()=="1")
                {
                    tipKorisnika="Bibliotekar";
                }
                else
                {
                    tipKorisnika="Član bibilioteke";
                }
                if (citac7["ulogovan"].ToString().ToLower() == "true")
                {
                    stanje = "Ulogovan";
                }
                else
                {
                    stanje = "Nije ulogovan";
                }
                if (citac7["Ulica"].ToString().Length < 3)
                {
                    dodatak = "\t\t";
                }
                else
                {
                    dodatak = "";
                }
                string unos = citac7["idKorisnici"].ToString() + "\t\t" + citac7["imeKorisnika"].ToString() + " " + citac7["prezimeKorisnika"].ToString() + "\t" + citac7["brojTelefona"].ToString() + "\t" + citac7["username"].ToString() + "\t\t" + citac7["password"].ToString() + "\t\t" + tipKorisnika + "\t" + citac7["Grad"].ToString() + ", " + citac7["Ulica"].ToString() + " " + citac7["Broj"].ToString() + dodatak + "   \t" + stanje;
                listaKorisnici.Items.Add(unos);
            }
            konekcija.Close();

            listaPrimjedbe.Items.Add("ID CLANA\t\tIME\t\tPREZIME\t\tTRAJANJE CLANARINE\tBROJ TELEFONA");
            listaPrimjedbe.Items.Add("");

            konekcija.Open();
            var komanda8 = konekcija.CreateCommand();
            komanda8.CommandText = "SELECT * FROM clanbiblioteke c, korisnici k WHERE c.Korisnici_idKorisnici=k.idkorisnici";
            var citac8 = komanda8.ExecuteReader();
            while (citac8.Read())
            {
                if (citac8["PrijavaNeregularnosti"].ToString().Length > 0)
                {
                    string unos = citac8["idClanaBiblioteke"].ToString() + "\t\t" + citac8["imeKorisnika"].ToString() + "\t\t" + citac8["prezimeKorisnika"].ToString() + "\t\t" + citac8["trajanjeClanarine"].ToString() + "\t\t" + citac8["brojTelefona"].ToString();
                    listaPrimjedbe.Items.Add(unos);
                }
            }
            konekcija.Close();

            konekcija.Open();
            var komanda14 = konekcija.CreateCommand();
            komanda14.CommandText = "SELECT COUNT(*) FROM korisnici k,adresa a WHERE k.Adresa_idAdresa=a.idAdresa";
            var citac14 = komanda14.ExecuteReader();
            citac14.Read();
            this.prethodniBrojKorisnika = Convert.ToInt16(citac14["COUNT(*)"].ToString());
            konekcija.Close();
        }

        private void refres()
        {

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

            int noviBrojClanova = prethodniBrojClanova;
            konekcija.Open();
            var komanda7 = konekcija.CreateCommand();
            komanda7.CommandText = "SELECT COUNT(*) FROM clanbiblioteke c, korisnici k WHERE c.Korisnici_idKorisnici=k.idkorisnici";
            var citac7 = komanda7.ExecuteReader();
            while (citac7.Read())
            {
                noviBrojClanova = Convert.ToInt16(citac7["COUNT(*)"].ToString());
            }
            konekcija.Close();

            if (noviBrojClanova != prethodniBrojClanova)
            {
                this.prethodniBrojClanova = noviBrojClanova;
                listaClanovi1.Items.Clear();
                listaClanovi2.Items.Clear();
                listaClanovi1.Items.Add("ID CLANA\t\tIME\t\tPREZIME\t\tTRAJANJE CLANARINE\tBROJ TELEFONA");
                listaClanovi1.Items.Add("");
                listaClanovi2.Items.Add("ID CLANA\t\tIME\t\tPREZIME\t\tTRAJANJE CLANARINE\tBROJ TELEFONA");
                listaClanovi2.Items.Add("");

                konekcija.Open();
                var komanda1 = konekcija.CreateCommand();
                komanda1.CommandText = "SELECT * FROM clanbiblioteke c, korisnici k WHERE c.Korisnici_idKorisnici=k.idkorisnici";
                var citac1 = komanda1.ExecuteReader();
                while (citac1.Read())
                {
                    string unos = citac1["idClanaBiblioteke"].ToString() + "\t\t" + citac1["imeKorisnika"].ToString() + "\t\t" + citac1["prezimeKorisnika"].ToString() + "\t\t" + citac1["trajanjeClanarine"].ToString() + "\t\t" + citac1["brojTelefona"].ToString();
                    listaClanovi1.Items.Add(unos);
                    listaClanovi2.Items.Add(unos);
                }
                konekcija.Close();
            }

            int noviBrojDetalja = prethodniBrojDetalja;
            konekcija.Open();
            var komanda8 = konekcija.CreateCommand();
            komanda8.CommandText = "SELECT COUNT(*) FROM clanbiblioteke c, korisnici k, adresa a WHERE c.Korisnici_idKorisnici=k.idKorisnici AND k.Adresa_idAdresa=a.idAdresa";
            var citac8 = komanda8.ExecuteReader();
            while (citac8.Read())
            {
                noviBrojDetalja = Convert.ToInt16(citac8["COUNT(*)"].ToString());
            }
            konekcija.Close();

            if (noviBrojDetalja != prethodniBrojDetalja)
            {
                this.prethodniBrojDetalja = noviBrojDetalja;
                listaDetalji.Items.Clear();
                listaDetalji.Items.Add("ID CLANA\t\tTRAJANJE ČLANARINE\tIME I PREZIME\tBROJ TELEFONA\t\tADRESA STANOVANJA");
                listaDetalji.Items.Add("");

                konekcija.Open();
                var komanda3 = konekcija.CreateCommand();
                komanda3.CommandText = "SELECT * FROM clanbiblioteke c, korisnici k, adresa a WHERE c.Korisnici_idKorisnici=k.idKorisnici AND k.Adresa_idAdresa=a.idAdresa";
                var citac3 = komanda3.ExecuteReader();
                while (citac3.Read())
                {
                    string unos = citac3["idClanaBiblioteke"].ToString() + "\t\t" + citac3["trajanjeClanarine"].ToString() +
                        "\t\t" + citac3["imeKorisnika"].ToString() + " " + citac3["prezimeKorisnika"].ToString() + "\t" +
                        citac3["brojTelefona"].ToString() + "\t\t" + citac3["Grad"].ToString() + ", " + citac3["Ulica"].ToString() + " " + citac3["Broj"].ToString();
                    listaDetalji.Items.Add(unos);
                }
                konekcija.Close();
            }

            this.prethodniBrojPisci = listaPisci.Items.Count;
            this.prethodniBrojIzdavac = listaIzdavaci.Items.Count;
            this.prethodniBrojKategorija = listaKategorije.Items.Count;

            int noviBrojPisci = prethodniBrojPisci, noviBrojIzdavac = prethodniBrojIzdavac, noviBrojKategorija = prethodniBrojKategorija;

            konekcija.Open();
            var komanda9 = konekcija.CreateCommand();
            komanda9.CommandText = "SELECT COUNT(*) FROM kategorija";
            var citac9 = komanda9.ExecuteReader();
            while (citac9.Read())
            {
                noviBrojKategorija = Convert.ToInt16(citac9["COUNT(*)"].ToString());
            }
            konekcija.Close();

            if (noviBrojKategorija != prethodniBrojKategorija)
            {
                this.prethodniBrojKategorija = noviBrojKategorija;
                listaKategorije.Items.Clear();

                konekcija.Open();
                var komanda4 = konekcija.CreateCommand();
                komanda4.CommandText = "SELECT * FROM kategorija";
                var citac4 = komanda4.ExecuteReader();
                while (citac4.Read())
                {
                    string unos = citac4["idKategorija"].ToString() + "\t" + citac4["nazivKategorije"].ToString();
                    listaKategorije.Items.Add(unos);
                }
                konekcija.Close();
            }

            konekcija.Open();
            var komanda10 = konekcija.CreateCommand();
            komanda10.CommandText = "SELECT COUNT(*) FROM pisac";
            var citac10 = komanda10.ExecuteReader();
            while (citac10.Read())
            {
                noviBrojPisci = Convert.ToInt16(citac10["COUNT(*)"].ToString());
            }
            konekcija.Close();

            if (noviBrojPisci != prethodniBrojPisci)
            {
                this.prethodniBrojPisci = noviBrojPisci;
                listaPisci.Items.Clear();

                konekcija.Open();
                var komanda5 = konekcija.CreateCommand();
                komanda5.CommandText = "SELECT * FROM pisac";
                var citac5 = komanda5.ExecuteReader();
                while (citac5.Read())
                {
                    string unos = citac5["idPisac"].ToString() + "\t" + citac5["imePisac"].ToString();
                    listaPisci.Items.Add(unos);
                }
                konekcija.Close();
            }

            konekcija.Open();
            var komanda11 = konekcija.CreateCommand();
            komanda11.CommandText = "SELECT COUNT(*) FROM izdavac";
            var citac11 = komanda11.ExecuteReader();
            while (citac11.Read())
            {
                noviBrojIzdavac = Convert.ToInt16(citac11["COUNT(*)"].ToString());
            }
            konekcija.Close();

            if (noviBrojIzdavac != prethodniBrojIzdavac)
            {
                this.prethodniBrojIzdavac = noviBrojIzdavac;
                listaIzdavaci.Items.Clear();

                konekcija.Open();
                var komanda6 = konekcija.CreateCommand();
                komanda6.CommandText = "SELECT * FROM izdavac";
                var citac6 = komanda6.ExecuteReader();
                while (citac6.Read())
                {
                    string unos = citac6["idIzdavaca"].ToString() + "\t" + citac6["nazivIzdavaca"].ToString();
                    listaIzdavaci.Items.Add(unos);
                }
                konekcija.Close();
            }

            listaPrimjedbe.Items.Clear();
            listaPrimjedbe.Items.Add("ID CLANA\t\tIME\t\tPREZIME\t\tTRAJANJE CLANARINE\tBROJ TELEFONA");
            listaPrimjedbe.Items.Add("");

            string unosZaRefres = "";

            konekcija.Open();
            var komanda13 = konekcija.CreateCommand();
            komanda13.CommandText = "SELECT * FROM clanbiblioteke c, korisnici k WHERE c.Korisnici_idKorisnici=k.idkorisnici";
            var citac13 = komanda13.ExecuteReader();
            while (citac13.Read())
            {
                if (citac13["PrijavaNeregularnosti"].ToString().Length > 0)
                {
                    string unos = citac13["idClanaBiblioteke"].ToString() + "\t\t" + citac13["imeKorisnika"].ToString() + "\t\t" + citac13["prezimeKorisnika"].ToString() + "\t\t" + citac13["trajanjeClanarine"].ToString() + "\t\t" + citac13["brojTelefona"].ToString();
                    listaPrimjedbe.Items.Add(unos);
                    if (idClana.ToString() == citac13["idClanaBiblioteke"].ToString())
                    {
                        unosZaRefres = unos;
                    }
                }
            }
            konekcija.Close();

            if (tbOdgovor.TextLength > 0)
            {
                if (tbPrepiska.TextLength > 0)
                {
                    listaPrimjedbe.SetSelected(listaPrimjedbe.FindStringExact(unosZaRefres), true);
                    tbOdgovor.Text = odgovor;
                    tbOdgovor.Select(tbOdgovor.Text.Length, 0);
                }
            }

            if (tbOdgovor.TextLength == 0)
            {
                if (tbPrepiska.TextLength > 0)
                {
                    listaPrimjedbe.SetSelected(listaPrimjedbe.FindStringExact(unosZaRefres), true);
                    tbOdgovor.Text = odgovor;
                    tbOdgovor.Select(tbOdgovor.Text.Length, 0);
                }
            }

            konekcija.Open();
            var komanda15 = konekcija.CreateCommand();
            komanda15.CommandText = "SELECT COUNT(*) FROM knjiga WHERE ClanBiblioteke_idClanaBiblioteke=" + idClana + "";
            var citac15 = komanda15.ExecuteReader();
            citac15.Read();
            int noviBrojKnjiga = Convert.ToInt16(citac15["COUNT(*)"].ToString());
            konekcija.Close();

            if (noviBrojKnjiga != prethodniBrojKnjiga)
            {
                this.prethodniBrojKnjiga = noviBrojKnjiga;

                listaKorisnici.Items.Clear();
                listaKorisnici.Items.Add("ID KORISNIKA\tIME I PREZIME\tBROJ TELEFONA\tUSERNAME\tPASSWORD\tTIP KORISNIKA\tADRESA\t\t\tSTANJE");
                listaKorisnici.Items.Add("");

                konekcija.Open();
                var komanda12 = konekcija.CreateCommand();
                komanda12.CommandText = "SELECT * FROM korisnici k,adresa a WHERE k.Adresa_idAdresa=a.idAdresa";
                var citac12 = komanda12.ExecuteReader();
                while (citac12.Read())
                {
                    string tipKorisnika, stanje, dodatak;
                    if (citac12["tipKorisnika"].ToString() == "0")
                    {
                        tipKorisnika = "Administrator";
                    }
                    else if (citac12["tipKorisnika"].ToString() == "1")
                    {
                        tipKorisnika = "Bibliotekar";
                    }
                    else
                    {
                        tipKorisnika = "Član bibilioteke";
                    }
                    if (citac12["ulogovan"].ToString().ToLower() == "true")
                    {
                        stanje = "Ulogovan";
                    }
                    else
                    {
                        stanje = "Nije ulogovan";
                    }
                    if (citac12["Ulica"].ToString().Length < 3)
                    {
                        dodatak = "\t\t";
                    }
                    else
                    {
                        dodatak = "";
                    }
                    string unos = citac12["idKorisnici"].ToString() + "\t\t" + citac12["imeKorisnika"].ToString() + " " + citac12["prezimeKorisnika"].ToString() + "\t" + citac12["brojTelefona"].ToString() + "\t" + citac12["username"].ToString() + "\t\t" + citac12["password"].ToString() + "\t\t" + tipKorisnika + "\t" + citac12["Grad"].ToString() + ", " + citac12["Ulica"].ToString() + " " + citac12["Broj"].ToString() + dodatak + "   \t" + stanje;
                    listaKorisnici.Items.Add(unos);
                }
                konekcija.Close();
            }

            
        }

        private void listaClanovi1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listaKnjige.Items.Clear();
            listaKnjige.Items.Add("ID KNJIGE\tNAZIV\t\tGODINA");
            listaKnjige.Items.Add("");

            if (listaClanovi1.SelectedIndex > 1)
            {
                listaKnjige.Enabled = true;

                string idClana = listaClanovi1.SelectedItem.ToString();
                idClana = idClana.Substring(0, idClana.IndexOf("\t"));

                konekcija.Open();
                var komanda = konekcija.CreateCommand();
                komanda.CommandText = "SELECT * FROM knjiga WHERE ClanBiblioteke_idClanaBiblioteke=" + idClana + "";
                var citac = komanda.ExecuteReader();
                while (citac.Read())
                {
                    string unos = citac["idKnjige"].ToString() + "\t\t" + citac["naslovKnjige"].ToString() + "\t\t" + citac["godinaIzdavanja"].ToString() + "";
                    listaKnjige.Items.Add(unos);
                }
                konekcija.Close();
            }
            else
            {
                listaKnjige.Enabled = false;
            }
        }

        private void listaClanovi2_SelectedIndexChanged(object sender, EventArgs e)
        {
            listaRacuni.Items.Clear();
            listaRacuni.Items.Add("ID RACUN\tDATUM\t\t\tNAPLAĆENO");
            listaRacuni.Items.Add("");

            if (listaClanovi2.SelectedIndex > 1)
            {
                listaRacuni.Enabled = true;

                string idClana = listaClanovi2.SelectedItem.ToString();
                idClana = idClana.Substring(0, idClana.IndexOf("\t"));

                konekcija.Open();
                var komanda = konekcija.CreateCommand();
                komanda.CommandText = "SELECT * FROM racun WHERE ClanBiblioteke_idClanaBiblioteke=" + idClana + "";
                var citac = komanda.ExecuteReader();
                while (citac.Read())
                {
                    string unos = citac["idracun"].ToString() + "\t\t" + citac["datumIzdavanja"].ToString() + "\t\t" + citac["zaNaplatu"].ToString() + "";
                    listaRacuni.Items.Add(unos);
                }
                konekcija.Close();
            }
            else
            {
                listaRacuni.Enabled = false;
            }
        }

        private void rbBezNoveKategorije_CheckedChanged(object sender, EventArgs e)
        {
            if (rbBezNoveKategorije.Checked == true)
            {
                tbKategorijaNaziv.Enabled = false;
            }
            else
            {
                tbKategorijaNaziv.Enabled = true;
            }
        }

        private void rbBezNovogPisca_CheckedChanged(object sender, EventArgs e)
        {
            if (rbBezNovogPisca.Checked == true)
            {
                tbPisacIme.Enabled = false;
                dtpPisacRodjenje.Enabled = false;
                tbPisacDodatniDetalji.Enabled = false;
            }
            else
            {
                tbPisacIme.Enabled = true;
                dtpPisacRodjenje.Enabled = true;
                tbPisacDodatniDetalji.Enabled = true;
            }
        }

        private void rbBezNovogIzdavaca_CheckedChanged(object sender, EventArgs e)
        {
            if (rbBezNovogIzdavaca.Checked == true)
            {
                tbIzdavacNaziv.Enabled = false;
                tbIzdavacDodatniDetalji.Enabled = false;
                tbAdresaGrad.Enabled = false;
                tbAdresaUlica.Enabled = false;
                numericBroj.Enabled = false;
            }
            else
            {
                tbIzdavacNaziv.Enabled = true;
                tbIzdavacDodatniDetalji.Enabled = true;
                tbAdresaGrad.Enabled = true;
                tbAdresaUlica.Enabled = true;
                numericBroj.Enabled = true;
            }
        }

        private void btnDodajKnjigu_Click(object sender, EventArgs e)
        {
            int idNoveAdrese, idKnjige, i=0;
            int[] nizKategorije = new int[100];
            int[] nizPisci = new int[100];
            int[] nizIzdavaci = new int[100];

            konekcija.Open();
            var komanda = konekcija.CreateCommand();
            komanda.CommandText = "INSERT INTO knjiga(naslovKnjige, cenaIznajmljivanja, godinaIzdavanja, izdato, ClanBiblioteke_idClanaBiblioteke)"+
                " VALUES ('"+tbKnjigaNaslov.Text+"',"+numericCena.Value+","+numericGodina.Value+","+false+",NULL)";
            komanda.ExecuteNonQuery();
            konekcija.Close();

            konekcija.Open();
            var komanda6 = konekcija.CreateCommand();
            komanda6.CommandText = "SELECT idKnjige FROM knjiga WHERE  naslovKnjige='"+tbKnjigaNaslov.Text+"' AND cenaIznajmljivanja="+numericCena.Value+" AND godinaIzdavanja="+numericGodina.Value;
            var citac6 = komanda6.ExecuteReader();
            citac6.Read();
            idKnjige = Convert.ToInt16(citac6["idKnjige"].ToString());
            konekcija.Close();

            if (rbSaNovomKategorijom.Checked == true)
            {
                konekcija.Open();
                var komanda1 = konekcija.CreateCommand();
                komanda1.CommandText = "INSERT INTO kategorija(nazivKategorije) VALUES ('"+tbKategorijaNaziv.Text+"')";
                komanda1.ExecuteNonQuery();
                konekcija.Close();

                konekcija.Open();
                var komanda7 = konekcija.CreateCommand();
                komanda7.CommandText = "SELECT idKategorija FROM kategorija WHERE nazivKategorije='"+tbKategorijaNaziv.Text+"'";
                var citac7 = komanda7.ExecuteReader();
                citac7.Read();
                nizKategorije[i] = Convert.ToInt16(citac7["idKategorija"].ToString());
                i++;
                konekcija.Close();
            }

            foreach (var item in listaKategorije.SelectedItems)
            {
                string prevod=item.ToString();
                nizKategorije[i] = Convert.ToInt16(prevod.Substring(0, prevod.IndexOf("\t")));
                i++;
            }

            for (int j = 0;nizKategorije[j]!=0; j++)
            {
                konekcija.Open();
                var komanda8 = konekcija.CreateCommand();
                komanda8.CommandText = "INSERT INTO pripada(Knjiga_idKnjige, Kategorija_idKategorija) VALUES ("+idKnjige+","+nizKategorije[j]+")";
                komanda8.ExecuteNonQuery();
                konekcija.Close();
            }

            i = 0;
            if (rbSaNovimPiscem.Checked == true)
            {
                konekcija.Open();
                var komanda2 = konekcija.CreateCommand();
                string datum = dtpPisacRodjenje.Value.ToString("u").Replace("-", "/");
                komanda2.CommandText = "INSERT INTO pisac(imePisac, datumRodjenja, dodatniDetaljiOPiscu) VALUES ('"+tbPisacIme.Text+"','"+datum+"','"+tbPisacDodatniDetalji.Text+"')";
                komanda2.ExecuteNonQuery();
                konekcija.Close();

                konekcija.Open();
                var komanda9 = konekcija.CreateCommand();
                komanda9.CommandText = "SELECT idPisac FROM pisac WHERE imePisac='" + tbPisacIme.Text + "'";
                var citac9 = komanda9.ExecuteReader();
                citac9.Read();
                nizPisci[i] = Convert.ToInt16(citac9["idPisac"].ToString());
                i++;
                konekcija.Close();
            }

            foreach (var item in listaPisci.SelectedItems)
            {
                string prevod = item.ToString();
                nizPisci[i] = Convert.ToInt16(prevod.Substring(0, prevod.IndexOf("\t")));
                i++;
            }

            for (int j = 0; nizPisci[j] != 0; j++)
            {
                konekcija.Open();
                var komanda10 = konekcija.CreateCommand();
                komanda10.CommandText = "INSERT INTO napisao(Knjiga_idKnjige, Pisac_idPisac) VALUES (" + idKnjige + "," + nizPisci[j] + ")";
                komanda10.ExecuteNonQuery();
                konekcija.Close();
            }

            i = 0;
            if (rbSaNovimIzdavacem.Checked == true)
            {
                konekcija.Open();
                var komanda3 = konekcija.CreateCommand();
                komanda3.CommandText = "INSERT INTO adresa(Grad,Ulica,Broj) VALUES ('"+tbAdresaGrad.Text+"','"+tbAdresaUlica.Text+"',"+numericBroj.Value+")";
                komanda3.ExecuteNonQuery();
                konekcija.Close();

                konekcija.Open();
                var komanda5 = konekcija.CreateCommand();
                komanda5.CommandText = "SELECT idAdresa FROM adresa WHERE  Grad='" + tbAdresaGrad.Text + "' AND Ulica='" + tbAdresaUlica.Text + "' AND Broj=" + numericBroj.Value + "";
                var citac5=komanda5.ExecuteReader();
                citac5.Read();
                idNoveAdrese = Convert.ToInt16(citac5["idAdresa"].ToString());
                konekcija.Close();

                konekcija.Open();
                var komanda4 = konekcija.CreateCommand();
                komanda4.CommandText = "INSERT INTO izdavac(nazivIzdavaca, dodatniDetaljiOIzdavacu, Adresa_idAdresa) VALUES ('"+tbIzdavacNaziv.Text+"','"+tbIzdavacDodatniDetalji.Text+"',"+idNoveAdrese+")";
                komanda4.ExecuteNonQuery();
                konekcija.Close();

                konekcija.Open();
                var komanda11 = konekcija.CreateCommand();
                komanda11.CommandText = "SELECT idIzdavaca FROM izdavac WHERE nazivIzdavaca='" + tbIzdavacNaziv.Text + "'";
                var citac11 = komanda11.ExecuteReader();
                citac11.Read();
                nizIzdavaci[i] = Convert.ToInt16(citac11["idIzdavaca"].ToString());
                i++;
                konekcija.Close();
            }

            foreach (var item in listaIzdavaci.SelectedItems)
            {
                string prevod = item.ToString();
                nizIzdavaci[i] = Convert.ToInt16(prevod.Substring(0, prevod.IndexOf("\t")));
                i++;
            }

            for (int j = 0; nizIzdavaci[j] != 0; j++)
            {
                konekcija.Open();
                var komanda12 = konekcija.CreateCommand();
                komanda12.CommandText = "INSERT INTO izdaje(Knjiga_idKnjige, Izdavac_idIzdavaca) VALUES (" + idKnjige + "," + nizIzdavaci[j] + ")";
                komanda12.ExecuteNonQuery();
                konekcija.Close();
            }

            MessageBox.Show("Dodavanje knjige u biblioteku je izvršeno.","Informacija");
            pocetnoPopunjavanje();
        }

        private void tbKnjigaNaslov_TextChanged(object sender, EventArgs e)
        {
            if (tbKnjigaNaslov.TextLength > 0)
            {
                btnDodajKnjigu.Enabled = true;
            }
            else
            {
                btnDodajKnjigu.Enabled = false;
            }
        }

        private void btnIzdavacDodaj_Click(object sender, EventArgs e)
        {
            if (rbBezNovogIzdavaca.Checked == true)
            {
                MessageBox.Show("Potrebno je da uključite opciju Sa novim izdavačem\nda bi mogli uneti potrebne podatke za upis novog izdavača.", "Upustvo");
            }
            else
            {
                listaIzdavaci.ClearSelected();

                konekcija.Open();
                var komanda3 = konekcija.CreateCommand();
                komanda3.CommandText = "INSERT INTO adresa(Grad,Ulica,Broj) VALUES ('" + tbAdresaGrad.Text + "','" + tbAdresaUlica.Text + "'," + numericBroj.Value + ")";
                komanda3.ExecuteNonQuery();
                konekcija.Close();

                konekcija.Open();
                var komanda5 = konekcija.CreateCommand();
                komanda5.CommandText = "SELECT idAdresa FROM adresa WHERE  Grad='" + tbAdresaGrad.Text + "' AND Ulica='" + tbAdresaUlica.Text + "' AND Broj=" + numericBroj.Value + "";
                var citac5 = komanda5.ExecuteReader();
                citac5.Read();
                int idNoveAdrese = Convert.ToInt16(citac5["idAdresa"].ToString());
                konekcija.Close();

                konekcija.Open();
                var komanda4 = konekcija.CreateCommand();
                komanda4.CommandText = "INSERT INTO izdavac(nazivIzdavaca, dodatniDetaljiOIzdavacu, Adresa_idAdresa) VALUES ('" + tbIzdavacNaziv.Text + "','" + tbIzdavacDodatniDetalji.Text + "'," + idNoveAdrese + ")";
                komanda4.ExecuteNonQuery();
                konekcija.Close();

                MessageBox.Show("Dodavanje novog izdavača uspješno obavljeno.","Informacija");
                pocetnoPopunjavanje();
            }
        }

        private void btnKategorijaDodaj_Click(object sender, EventArgs e)
        {
            if (rbBezNoveKategorije.Checked == true)
            {
                MessageBox.Show("Potrebno je da uključite opciju Sa novom kategorijom\nda bi mogli uneti potrebne podatke za upis nove kategorije.", "Upustvo");
            }
            else
            {
                listaKategorije.ClearSelected();

                konekcija.Open();
                var komanda1 = konekcija.CreateCommand();
                komanda1.CommandText = "INSERT INTO kategorija(nazivKategorije) VALUES ('" + tbKategorijaNaziv.Text + "')";
                komanda1.ExecuteNonQuery();
                konekcija.Close();

                MessageBox.Show("Dodavanje nove kategorije uspješno obavljeno.", "Informacija");
                pocetnoPopunjavanje();
            }
        }

        private void btnPisacDodaj_Click(object sender, EventArgs e)
        {
            if (rbBezNovogPisca.Checked == true)
            {
                MessageBox.Show("Potrebno je da uključite opciju Sa novim piscem\nda bi mogli uneti potrebne podatke za upis novog pisca.", "Upustvo");
            }
            else
            {
                listaPisci.ClearSelected();

                konekcija.Open();
                var komanda2 = konekcija.CreateCommand();
                string datum = dtpPisacRodjenje.Value.ToString("u").Replace("-", "/");
                komanda2.CommandText = "INSERT INTO pisac(imePisac, datumRodjenja, dodatniDetaljiOPiscu) VALUES ('" + tbPisacIme.Text + "','" + datum + "','" + tbPisacDodatniDetalji.Text + "')";
                komanda2.ExecuteNonQuery();
                konekcija.Close();

                MessageBox.Show("Dodavanje novog pisca uspješno obavljeno.", "Informacija");
                pocetnoPopunjavanje();
            }
        }

        private void listaPrimjedbe_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listaPrimjedbe.SelectedIndex > 1)
            {
                this.odgovor = tbOdgovor.Text;
                tbOdgovor.ReadOnly = false;
                tbOdgovor.Text = "";
                btnIzbpisiPrepisku.Enabled = true;

                konekcija.Open();
                var komanda8 = konekcija.CreateCommand();
                string pomClan = listaPrimjedbe.SelectedItem.ToString();
                this.idClana = Convert.ToInt16(pomClan.Substring(0, pomClan.IndexOf("\t")));
                komanda8.CommandText = "SELECT PrijavaNeregularnosti FROM clanbiblioteke WHERE idClanaBiblioteke=" + idClana;
                var citac8 = komanda8.ExecuteReader();
                while (citac8.Read())
                {
                    tbPrepiska.Text = citac8["PrijavaNeregularnosti"].ToString();
                }
                konekcija.Close();
            }
            else 
            {
                tbOdgovor.ReadOnly = true;
                tbOdgovor.Text = "";
                tbPrepiska.Text = "";
                btnIzbpisiPrepisku.Enabled = false;
            }

        }

        private void tbOdgovor_TextChanged(object sender, EventArgs e)
        {
            if (tbOdgovor.TextLength > 0)
            {
                btnOdgovori.Enabled = true;
            }
            else
            {
                btnOdgovori.Enabled = false;
            }
        }

        private void btnOdgovori_Click(object sender, EventArgs e)
        {
            string prepiska = tbPrepiska.Text;
            konekcija.Open();
            var komanda = konekcija.CreateCommand();
            prepiska = prepiska + "(" + DateTime.Now + ") " + ime + " " + prezime + " je napisao:\n\t" + tbOdgovor.Text;
            string idClana = listaPrimjedbe.SelectedItem.ToString();
            idClana = idClana.Substring(0, idClana.IndexOf("\t"));
            komanda.CommandText = "UPDATE clanbiblioteke SET PrijavaNeregularnosti='" + prepiska + "\n\n' WHERE idClanaBiblioteke=" + idClana + "";
            komanda.ExecuteNonQuery();

            konekcija.Close();
            MessageBox.Show("Uspješno ste odgovorili.", "Poruka");
            pocetnoPopunjavanje();
            tbOdgovor.Text = "";
            tbPrepiska.Text = "";
            btnIzbpisiPrepisku.Enabled = false;
            tbOdgovor.ReadOnly = true;
        }

        private void btnIzbpisiPrepisku_Click(object sender, EventArgs e)
        {
            tbPrepiska.Clear();
            konekcija.Open();
            var komanda = konekcija.CreateCommand();
            string idClana = listaPrimjedbe.SelectedItem.ToString();
            idClana = idClana.Substring(0, idClana.IndexOf("\t"));
            komanda.CommandText = "UPDATE clanbiblioteke SET PrijavaNeregularnosti='' WHERE idClanaBiblioteke=" + idClana + "";
            komanda.ExecuteNonQuery();
            konekcija.Close();

            MessageBox.Show("Prepiska je uspješno obrisana.", "Poruka");
            pocetnoPopunjavanje();
            tbOdgovor.Text = "";
            tbPrepiska.Text = "";
            btnIzbpisiPrepisku.Enabled = false;
        }

        private void timerProvjeraZahtjeva_Tick(object sender, EventArgs e)
        {
            bool formaOtvorena = false;

            konekcija.Open();
            var komanda = konekcija.CreateCommand();
            komanda.CommandText = "SELECT * FROM korisnici WHERE dodatniDetaljiOKorisniku='Zahtjev za novi nalog'";
            var citac = komanda.ExecuteReader();

            FormCollection fc = Application.OpenForms;

            foreach (Form frm in fc)
            {
                if (frm.Text == "DodavanjeKorisnika")
                {
                    formaOtvorena = true;
                    break;
                }
            }

            if (citac.Read() == true && formaOtvorena == false)
            {
                DodavanjeKorisnika dodajKor = new DodavanjeKorisnika();
                dodajKor.Show();
            }
            konekcija.Close();
            try
            {
                refres();
            }
            catch
            { }
        }

        private void UlogovanoStanje_FormClosing(object sender, FormClosingEventArgs e)
        {
            timerProvjeraZahtjeva.Stop();
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

    }
}
