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
    public partial class UlogovanoStanje1 : Form
    {
        int idKorisnici, suma, prethodniBrojClanova, prethodniBrojKnjiga, prethodniBrojRacuna, prethodniBrojDetalja;
        int prethodniBrojPisci, prethodniBrojKategorija, prethodniBrojIzdavac;
        string ime, prezime;
        static string conString = "Server=localhost;Database=biblioteka;Uid=root;";
        MySqlConnection konekcija = new MySqlConnection(conString);

        public UlogovanoStanje1(string ime,int id,string prezime,int tip)
        {
            this.idKorisnici = id;
            this.ime = ime;
            this.prezime = prezime;

            InitializeComponent();
            pocetnoPopunjavanje();
            naplata.Start();
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
            labelKorisnik.Text = "Ulogovan korisnik:\n" + ime + " " + prezime + "\nkao bibliotekar.";

            konekcija.Open();
            var komanda = konekcija.CreateCommand();
            komanda.CommandText = "SELECT * FROM knjiga";
            var citac = komanda.ExecuteReader();

            prikazi.Items.Clear();
            listaClanovi1.Items.Clear();
            listaClanovi2.Items.Clear();
            listaPoslovanje.Items.Clear();
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
            listaPoslovanje.Items.Add("ID RACUNA\tDATUM IZDAVANJA\t\tNAPLAĆENO");
            listaPoslovanje.Items.Add("");
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

            suma = 0;
            konekcija.Open();
            var komanda2 = konekcija.CreateCommand();
            DateTime datumOd = dtpOd.Value;
            DateTime datumDo = dtpDo.Value;
            string datumOD = datumOd.ToString("u");
            string datumDO = datumDo.ToString("u");

            komanda2.CommandText = "SELECT * FROM racun WHERE datumIzdavanja<='" + datumDO + "' && datumIzdavanja>'" + datumOD + "'";
            var citac2 = komanda2.ExecuteReader();
            while (citac2.Read())
            {
                string unos = citac2["idracun"].ToString() + "\t\t" + citac2["datumIzdavanja"].ToString() + "\t\t" + citac2["zaNaplatu"].ToString() + "";
                suma = suma + Convert.ToInt16(citac2["zaNaplatu"].ToString());
                listaPoslovanje.Items.Add(unos);
            }
            tbSumaPoslovanja.Text = suma.ToString();
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

            konekcija.Open();
            var komanda7 = konekcija.CreateCommand();
            komanda7.CommandText = "SELECT COUNT(*) FROM clanbiblioteke c, korisnici k WHERE c.Korisnici_idKorisnici=k.idkorisnici";
            var citac7 = komanda7.ExecuteReader();
            while (citac7.Read())
            {
                this.prethodniBrojClanova = Convert.ToInt16(citac7["COUNT(*)"].ToString());
            }
            konekcija.Close();

            konekcija.Open();
            var komanda8 = konekcija.CreateCommand();
            komanda8.CommandText = "SELECT COUNT(*) FROM clanbiblioteke c, korisnici k, adresa a WHERE c.Korisnici_idKorisnici=k.idKorisnici AND k.Adresa_idAdresa=a.idAdresa";
            var citac8 = komanda8.ExecuteReader();
            while (citac8.Read())
            {
                this.prethodniBrojDetalja = Convert.ToInt16(citac8["COUNT(*)"].ToString());
            }
            konekcija.Close();
        }

        private void listaClanovi1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listaKnjige.Items.Clear();
            listaKnjige.Items.Add("ID KNJIGE\tNAZIV\t\tGODINA");
            listaKnjige.Items.Add("");
            this.prethodniBrojKnjiga = listaKnjige.Items.Count - 2;
            int noviBrojKnjiga = prethodniBrojKnjiga;

            if (listaClanovi1.SelectedIndex > 1)
            {
                listaKnjige.Enabled = true;

                string idClana = listaClanovi1.SelectedItem.ToString();
                idClana = idClana.Substring(0, idClana.IndexOf("\t"));

                konekcija.Open();
                var komanda1 = konekcija.CreateCommand();
                komanda1.CommandText = "SELECT COUNT(*) FROM knjiga WHERE ClanBiblioteke_idClanaBiblioteke=" + idClana + "";
                var citac1 = komanda1.ExecuteReader();
                while (citac1.Read())
                {
                    noviBrojKnjiga = Convert.ToInt16(citac1["COUNT(*)"].ToString());
                }
                konekcija.Close();

                if (noviBrojKnjiga != prethodniBrojKnjiga)
                {
                    this.prethodniBrojKnjiga = noviBrojKnjiga;
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

            }
            else
            {
                listaKnjige.Enabled = false;
            }
        }

        private void dtpOd_ValueChanged(object sender, EventArgs e)
        {
            listaPoslovanje.Items.Clear();
            listaPoslovanje.Items.Add("ID RACUNA\tDATUM IZDAVANJA\t\tNAPLAĆENO");
            listaPoslovanje.Items.Add("");

            suma = 0;
            konekcija.Open();
            var komanda2 = konekcija.CreateCommand();
            DateTime datumOd = dtpOd.Value;
            DateTime datumDo = dtpDo.Value;
            string datumOD = datumOd.ToString("u");
            string datumDO = datumDo.ToString("u");

            komanda2.CommandText = "SELECT * FROM racun WHERE datumIzdavanja<='" + datumDO+ "' && datumIzdavanja>'" + datumOD + "'";
            var citac2 = komanda2.ExecuteReader();
            while (citac2.Read())
            {
                string unos = citac2["idracun"].ToString() + "\t\t" + citac2["datumIzdavanja"].ToString() + "\t\t" + citac2["zaNaplatu"].ToString() + "";
                suma = suma + Convert.ToInt16(citac2["zaNaplatu"].ToString());
                listaPoslovanje.Items.Add(unos);
            }
            tbSumaPoslovanja.Text = suma.ToString();
            konekcija.Close();
        }

        private void dtpDo_ValueChanged(object sender, EventArgs e)
        {
            listaPoslovanje.Items.Clear();
            listaPoslovanje.Items.Add("ID RACUNA\tDATUM IZDAVANJA\t\tNAPLAĆENO");
            listaPoslovanje.Items.Add("");

            suma = 0;
            konekcija.Open();
            var komanda2 = konekcija.CreateCommand();
            DateTime datumOd = dtpOd.Value;
            DateTime datumDo = dtpDo.Value;
            string datumOD = datumOd.ToString("u");
            string datumDO = datumDo.ToString("u");

            komanda2.CommandText = "SELECT * FROM racun WHERE datumIzdavanja<='" + datumDO + "' && datumIzdavanja>'" + datumOD + "'";
            var citac2 = komanda2.ExecuteReader();
            while (citac2.Read())
            {
                string unos = citac2["idracun"].ToString() + "\t\t" + citac2["datumIzdavanja"].ToString() + "\t\t" + citac2["zaNaplatu"].ToString() + "";
                suma = suma + Convert.ToInt16(citac2["zaNaplatu"].ToString());
                listaPoslovanje.Items.Add(unos);
            }
            tbSumaPoslovanja.Text = suma.ToString();
            konekcija.Close();
        }

        private void listaClanovi2_SelectedIndexChanged(object sender, EventArgs e)
        {
            listaRacuni.Items.Clear();
            listaRacuni.Items.Add("ID RACUN\tDATUM\t\t\tNAPLAĆENO");
            listaRacuni.Items.Add("");
            this.prethodniBrojRacuna = listaRacuni.Items.Count - 2;
            int noviBrojRacuna = prethodniBrojRacuna;

            if (listaClanovi2.SelectedIndex > 1)
            {
                listaRacuni.Enabled = true;

                string idClana = listaClanovi2.SelectedItem.ToString();
                idClana = idClana.Substring(0, idClana.IndexOf("\t"));

                konekcija.Open();
                var komanda1 = konekcija.CreateCommand();
                komanda1.CommandText = "SELECT COUNT(*) FROM racun WHERE ClanBiblioteke_idClanaBiblioteke=" + idClana + "";
                var citac1 = komanda1.ExecuteReader();
                while (citac1.Read())
                {
                    noviBrojRacuna = Convert.ToInt16(citac1["COUNT(*)"].ToString());
                }
                konekcija.Close();

                if (noviBrojRacuna != prethodniBrojRacuna)
                {
                    this.prethodniBrojRacuna = noviBrojRacuna;
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
            }
            else
            {
                listaRacuni.Enabled = false;
            }
        }

        private void btnVrati_Click(object sender, EventArgs e)
        {
            string idKnjige = listaKnjige.SelectedItem.ToString();
            idKnjige = idKnjige.Substring(0, idKnjige.IndexOf("\t"));

            konekcija.Open();
            var komanda = konekcija.CreateCommand();
            komanda.CommandText = "UPDATE knjiga SET izdato=" + false + ",ClanBiblioteke_idClanaBiblioteke=NULL WHERE idKnjige=" + idKnjige + "";
            komanda.ExecuteNonQuery();
            konekcija.Close();

            listaKnjige.Items.Clear();
            listaKnjige.Items.Add("ID KNJIGE\tNAZIV\t\tGODINA");
            listaKnjige.Items.Add("");

            string idClana = listaClanovi1.SelectedItem.ToString();
            idClana = idClana.Substring(0, idClana.IndexOf("\t"));

            konekcija.Open();
            var komanda1 = konekcija.CreateCommand();
            komanda1.CommandText = "SELECT * FROM knjiga WHERE ClanBiblioteke_idClanaBiblioteke=" + idClana + "";
            var citac1 = komanda1.ExecuteReader();
            while (citac1.Read())
            {
                string unos = citac1["idKnjige"].ToString() + "\t\t" + citac1["naslovKnjige"].ToString() + "\t\t" + citac1["godinaIzdavanja"].ToString() + "";
                listaKnjige.Items.Add(unos);
            }
            konekcija.Close();
            MessageBox.Show("Vraćanje knjige je uspješno izvršeno.","Informacija");
        }

        private void listaKnjige_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listaKnjige.SelectedIndex > 1)
            {
                btnVrati.Enabled = true;
            }
            else
            {
                btnVrati.Enabled = false;
            }
        }

        private void naplata_Tick(object sender, EventArgs e)
        {
            bool formaOtvorena=false;

            konekcija.Open();
            var komanda = konekcija.CreateCommand();
            komanda.CommandText = "SELECT * FROM racun WHERE naplaceno=" + false + "";
            var citac = komanda.ExecuteReader();

            FormCollection fc = Application.OpenForms;

            foreach (Form frm in fc)
            {
                if (frm.Text == "Naplata")
                {
                    formaOtvorena=true;
                    break;
                }
            }

            if (citac.Read()==true && formaOtvorena==false)
            {
                Naplata nap=new Naplata();
                nap.Show();
            }
            konekcija.Close();

            try
            {
                refres();
            }
            catch
            {
            
            }
        }

        private void UlogovanoStanje1_FormClosing(object sender, FormClosingEventArgs e)
        {
            naplata.Stop();
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

        private void refres()
        {
            labelKorisnik.Text = "Ulogovan korisnik:\n" + ime + " " + prezime + "\nkao bibliotekar.";

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
            
            int noviBrojClanova=prethodniBrojClanova;
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

            int indeksPoslovanja = listaPoslovanje.SelectedIndex;

            listaPoslovanje.Items.Clear();
            listaPoslovanje.Items.Add("ID RACUNA\tDATUM IZDAVANJA\t\tNAPLAĆENO");
            listaPoslovanje.Items.Add("");

            suma = 0;
            konekcija.Open();
            var komanda2 = konekcija.CreateCommand();
            DateTime datumOd = dtpOd.Value;
            DateTime datumDo = dtpDo.Value;
            string datumOD = datumOd.ToString("u");
            string datumDO = datumDo.ToString("u");

            komanda2.CommandText = "SELECT * FROM racun WHERE datumIzdavanja<='" + datumDO + "' && datumIzdavanja>'" + datumOD + "'";
            var citac2 = komanda2.ExecuteReader();
            while (citac2.Read())
            {
                string unos = citac2["idracun"].ToString() + "\t\t" + citac2["datumIzdavanja"].ToString() + "\t\t" + citac2["zaNaplatu"].ToString() + "";
                suma = suma + Convert.ToInt16(citac2["zaNaplatu"].ToString());
                listaPoslovanje.Items.Add(unos);
            }
            tbSumaPoslovanja.Text = suma.ToString();
            konekcija.Close();
            listaPoslovanje.SetSelected(indeksPoslovanja, true);

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

            if (noviBrojKategorija!=prethodniBrojKategorija)
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
