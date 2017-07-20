SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

-- -----------------------------------------------------
-- Schema biblioteka
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `biblioteka` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci ;
USE `biblioteka` ;

-- -----------------------------------------------------
-- Table `biblioteka`.`Adresa`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `biblioteka`.`Adresa` (
  `idAdresa` INT NOT NULL AUTO_INCREMENT,
  `Grad` VARCHAR(50) NULL,
  `Ulica` VARCHAR(50) NULL,
  `Broj` INT NULL,
  PRIMARY KEY (`idAdresa`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `biblioteka`.`Korisnici`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `biblioteka`.`Korisnici` (
  `idKorisnici` INT NOT NULL AUTO_INCREMENT COMMENT 'sa',
  `imeKorisnika` VARCHAR(20) NOT NULL,
  `prezimeKorisnika` VARCHAR(20) NOT NULL,
  `brojTelefona` VARCHAR(20) NOT NULL,
  `username` VARCHAR(20) NOT NULL,
  `password` VARCHAR(20) NOT NULL,
  `salt` VARCHAR(300) NULL DEFAULT '1111',
  `tipKorisnika` INT NOT NULL DEFAULT 2 COMMENT 'tip korisnika: 0 - admin, 1 - bibliotekar, 2 - clan biblioteke',
  `dodatniDetaljiOKorisniku` VARCHAR(250) NULL,
  `Adresa_idAdresa` INT NOT NULL,
  `ulogovan` TINYINT(1) NOT NULL DEFAULT 0,
  PRIMARY KEY (`idKorisnici`),
  UNIQUE INDEX `username_UNIQUE` (`username` ASC),
  INDEX `fk_Korisnici_Adresa1_idx` (`Adresa_idAdresa` ASC),
  CONSTRAINT `fk_Korisnici_Adresa1`
    FOREIGN KEY (`Adresa_idAdresa`)
    REFERENCES `biblioteka`.`Adresa` (`idAdresa`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `biblioteka`.`ClanBiblioteke`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `biblioteka`.`ClanBiblioteke` (
  `idClanaBiblioteke` INT NOT NULL AUTO_INCREMENT,
  `trajanjeClanarine` DATE NULL,
  `PrijavaNeregularnosti` VARCHAR(20000) NULL,
  `Korisnici_idKorisnici` INT NOT NULL,
  PRIMARY KEY (`idClanaBiblioteke`),
  INDEX `fk_ClanBiblioteke_Korisnici1_idx` (`Korisnici_idKorisnici` ASC),
  CONSTRAINT `fk_ClanBiblioteke_Korisnici1`
    FOREIGN KEY (`Korisnici_idKorisnici`)
    REFERENCES `biblioteka`.`Korisnici` (`idKorisnici`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `biblioteka`.`Knjiga`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `biblioteka`.`Knjiga` (
  `idKnjige` INT NOT NULL AUTO_INCREMENT,
  `naslovKnjige` VARCHAR(100) NOT NULL,
  `cenaIznajmljivanja` INT NOT NULL,
  `godinaIzdavanja` YEAR NOT NULL,
  `izdato` TINYINT(1) NOT NULL,
  `ClanBiblioteke_idClanaBiblioteke` INT NULL,
  PRIMARY KEY (`idKnjige`),
  UNIQUE INDEX `idKnjige_UNIQUE` (`idKnjige` ASC),
  INDEX `fk_Knjiga_ClanBiblioteke1_idx` (`ClanBiblioteke_idClanaBiblioteke` ASC),
  CONSTRAINT `fk_Knjiga_ClanBiblioteke1`
    FOREIGN KEY (`ClanBiblioteke_idClanaBiblioteke`)
    REFERENCES `biblioteka`.`ClanBiblioteke` (`idClanaBiblioteke`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `biblioteka`.`Izdavac`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `biblioteka`.`Izdavac` (
  `idIzdavaca` INT NOT NULL AUTO_INCREMENT,
  `nazivIzdavaca` VARCHAR(20) NOT NULL,
  `dodatniDetaljiOIzdavacu` VARCHAR(250) NULL,
  `Adresa_idAdresa` INT NOT NULL,
  PRIMARY KEY (`idIzdavaca`),
  INDEX `fk_Izdavac_Adresa1_idx` (`Adresa_idAdresa` ASC),
  CONSTRAINT `fk_Izdavac_Adresa1`
    FOREIGN KEY (`Adresa_idAdresa`)
    REFERENCES `biblioteka`.`Adresa` (`idAdresa`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `biblioteka`.`Pisac`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `biblioteka`.`Pisac` (
  `idPisac` INT NOT NULL AUTO_INCREMENT,
  `imePisac` VARCHAR(45) NOT NULL,
  `datumRodjenja` DATE NOT NULL,
  `dodatniDetaljiOPiscu` VARCHAR(250) NULL,
  PRIMARY KEY (`idPisac`),
  UNIQUE INDEX `idPisac_UNIQUE` (`idPisac` ASC))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `biblioteka`.`Napisao`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `biblioteka`.`Napisao` (
  `Knjiga_idKnjige` INT NOT NULL,
  `Pisac_idPisac` INT NOT NULL,
  PRIMARY KEY (`Knjiga_idKnjige`, `Pisac_idPisac`),
  INDEX `fk_Knjiga_has_Pisac_Pisac1_idx` (`Pisac_idPisac` ASC),
  INDEX `fk_Knjiga_has_Pisac_Knjiga1_idx` (`Knjiga_idKnjige` ASC),
  CONSTRAINT `fk_Knjiga_has_Pisac_Knjiga1`
    FOREIGN KEY (`Knjiga_idKnjige`)
    REFERENCES `biblioteka`.`Knjiga` (`idKnjige`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Knjiga_has_Pisac_Pisac1`
    FOREIGN KEY (`Pisac_idPisac`)
    REFERENCES `biblioteka`.`Pisac` (`idPisac`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `biblioteka`.`Kategorija`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `biblioteka`.`Kategorija` (
  `idKategorija` INT NOT NULL AUTO_INCREMENT,
  `nazivKategorije` VARCHAR(20) NOT NULL,
  PRIMARY KEY (`idKategorija`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `biblioteka`.`Pripada`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `biblioteka`.`Pripada` (
  `Knjiga_idKnjige` INT NOT NULL,
  `Kategorija_idKategorija` INT NOT NULL,
  PRIMARY KEY (`Knjiga_idKnjige`, `Kategorija_idKategorija`),
  INDEX `fk_Knjiga_has_Kategorija_Kategorija1_idx` (`Kategorija_idKategorija` ASC),
  INDEX `fk_Knjiga_has_Kategorija_Knjiga1_idx` (`Knjiga_idKnjige` ASC),
  CONSTRAINT `fk_Knjiga_has_Kategorija_Knjiga1`
    FOREIGN KEY (`Knjiga_idKnjige`)
    REFERENCES `biblioteka`.`Knjiga` (`idKnjige`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Knjiga_has_Kategorija_Kategorija1`
    FOREIGN KEY (`Kategorija_idKategorija`)
    REFERENCES `biblioteka`.`Kategorija` (`idKategorija`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `biblioteka`.`racun`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `biblioteka`.`racun` (
  `idracun` INT NOT NULL AUTO_INCREMENT,
  `datumIzdavanja` DATETIME NOT NULL,
  `zaNaplatu` INT NOT NULL,
  `naplaceno` TINYINT(1) NOT NULL DEFAULT 1,
  `ClanBiblioteke_idClanaBiblioteke` INT NOT NULL,
  PRIMARY KEY (`idracun`),
  INDEX `fk_racun_ClanBiblioteke1_idx` (`ClanBiblioteke_idClanaBiblioteke` ASC),
  CONSTRAINT `fk_racun_ClanBiblioteke1`
    FOREIGN KEY (`ClanBiblioteke_idClanaBiblioteke`)
    REFERENCES `biblioteka`.`ClanBiblioteke` (`idClanaBiblioteke`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `biblioteka`.`Izdaje`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `biblioteka`.`Izdaje` (
  `Knjiga_idKnjige` INT NOT NULL,
  `Izdavac_idIzdavaca` INT NOT NULL,
  PRIMARY KEY (`Knjiga_idKnjige`, `Izdavac_idIzdavaca`),
  INDEX `fk_Knjiga_has_Izdavac_Izdavac1_idx` (`Izdavac_idIzdavaca` ASC),
  INDEX `fk_Knjiga_has_Izdavac_Knjiga1_idx` (`Knjiga_idKnjige` ASC),
  CONSTRAINT `fk_Knjiga_has_Izdavac_Knjiga1`
    FOREIGN KEY (`Knjiga_idKnjige`)
    REFERENCES `biblioteka`.`Knjiga` (`idKnjige`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Knjiga_has_Izdavac_Izdavac1`
    FOREIGN KEY (`Izdavac_idIzdavaca`)
    REFERENCES `biblioteka`.`Izdavac` (`idIzdavaca`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;

-- -----------------------------------------------------
-- Data for table `biblioteka`.`Adresa`
-- -----------------------------------------------------
START TRANSACTION;
USE `biblioteka`;
INSERT INTO `biblioteka`.`Adresa` (`idAdresa`, `Grad`, `Ulica`, `Broj`) VALUES (1, 'Sarajevo', 'Marsala Tita', 18);
INSERT INTO `biblioteka`.`Adresa` (`idAdresa`, `Grad`, `Ulica`, `Broj`) VALUES (2, 'Sarajevo', 'Zvornicka', 23);
INSERT INTO `biblioteka`.`Adresa` (`idAdresa`, `Grad`, `Ulica`, `Broj`) VALUES (3, 'Bijeljina', '5 Jezera', 45);
INSERT INTO `biblioteka`.`Adresa` (`idAdresa`, `Grad`, `Ulica`, `Broj`) VALUES (4, 'Trebinje', 'Svetog Save', 89);
INSERT INTO `biblioteka`.`Adresa` (`idAdresa`, `Grad`, `Ulica`, `Broj`) VALUES (5, 'Sarajevo', 'Brace Jugovica', 67);
INSERT INTO `biblioteka`.`Adresa` (`idAdresa`, `Grad`, `Ulica`, `Broj`) VALUES (6, 'Zvornik', 'Dvorovi', 44);
INSERT INTO `biblioteka`.`Adresa` (`idAdresa`, `Grad`, `Ulica`, `Broj`) VALUES (7, 'Bileca', 'Nova Varos', 1);
INSERT INTO `biblioteka`.`Adresa` (`idAdresa`, `Grad`, `Ulica`, `Broj`) VALUES (8, 'Lukavica', 'Spasovdanska', 35);
INSERT INTO `biblioteka`.`Adresa` (`idAdresa`, `Grad`, `Ulica`, `Broj`) VALUES (9, 'Zvornik', 'Svetog Save', 61);
INSERT INTO `biblioteka`.`Adresa` (`idAdresa`, `Grad`, `Ulica`, `Broj`) VALUES (10, 'Bijeljina', 'Marsala Tita', 16);
INSERT INTO `biblioteka`.`Adresa` (`idAdresa`, `Grad`, `Ulica`, `Broj`) VALUES (11, 'Sarajevo', 'Svetog Save', 77);

COMMIT;


-- -----------------------------------------------------
-- Data for table `biblioteka`.`Korisnici`
-- -----------------------------------------------------
START TRANSACTION;
USE `biblioteka`;
INSERT INTO `biblioteka`.`Korisnici` (`idKorisnici`, `imeKorisnika`, `prezimeKorisnika`, `brojTelefona`, `username`, `password`, `salt`, `tipKorisnika`, `dodatniDetaljiOKorisniku`, `Adresa_idAdresa`, `ulogovan`) VALUES (1, 'Aleksandar', 'Mitric', '066772713', 'aleksa', '1127', '9fe313b47feb47b0a52f140d2833dc19', 0, 'Direktor', 1, false);
INSERT INTO `biblioteka`.`Korisnici` (`idKorisnici`, `imeKorisnika`, `prezimeKorisnika`, `brojTelefona`, `username`, `password`, `salt`, `tipKorisnika`, `dodatniDetaljiOKorisniku`, `Adresa_idAdresa`, `ulogovan`) VALUES (2, 'Goran', 'Sarenac', '065331961', 'gosa', '1179', 'd95ad7376e0aa24c1f5d3cee8cf1339d', 0, 'Administrator sistema', 2, false);
INSERT INTO `biblioteka`.`Korisnici` (`idKorisnici`, `imeKorisnika`, `prezimeKorisnika`, `brojTelefona`, `username`, `password`, `salt`, `tipKorisnika`, `dodatniDetaljiOKorisniku`, `Adresa_idAdresa`, `ulogovan`) VALUES (3, 'Boris', 'Matic', '066112233', 'boris', '1125', '06ec597471ff1186782a6543dce19641', 1, NULL, 3, false);
INSERT INTO `biblioteka`.`Korisnici` (`idKorisnici`, `imeKorisnika`, `prezimeKorisnika`, `brojTelefona`, `username`, `password`, `salt`, `tipKorisnika`, `dodatniDetaljiOKorisniku`, `Adresa_idAdresa`, `ulogovan`) VALUES (4, 'Dragana', 'Sudzum', '065123456', 'dragana', '1167', '3d70b2ba2a480e7fc350900d7136aa1d', 1, NULL, 4, false);
INSERT INTO `biblioteka`.`Korisnici` (`idKorisnici`, `imeKorisnika`, `prezimeKorisnika`, `brojTelefona`, `username`, `password`, `salt`, `tipKorisnika`, `dodatniDetaljiOKorisniku`, `Adresa_idAdresa`, `ulogovan`) VALUES (5, 'Nikola', 'Nikolic', '066445445', 'nidzo', 'nidzo66', 'ac9ef60d2489e3c16f6ae846598e9165', 2, 'Clan sa privilegijama', 5, false);
INSERT INTO `biblioteka`.`Korisnici` (`idKorisnici`, `imeKorisnika`, `prezimeKorisnika`, `brojTelefona`, `username`, `password`, `salt`, `tipKorisnika`, `dodatniDetaljiOKorisniku`, `Adresa_idAdresa`, `ulogovan`) VALUES (6, 'Dejan', 'Markovic', '+381641111222', 'dejo123', 'miki91', '4819313798058a2f7e46a329cd2867d5', 2, '', 6, false);
INSERT INTO `biblioteka`.`Korisnici` (`idKorisnici`, `imeKorisnika`, `prezimeKorisnika`, `brojTelefona`, `username`, `password`, `salt`, `tipKorisnika`, `dodatniDetaljiOKorisniku`, `Adresa_idAdresa`, `ulogovan`) VALUES (7, 'Dejana', 'Andric', '+4978456456', 'nezavisna', 'sijalica', 'cec03437582ae4d9de382fe65598a263', 2, NULL, 7, false);

COMMIT;


-- -----------------------------------------------------
-- Data for table `biblioteka`.`ClanBiblioteke`
-- -----------------------------------------------------
START TRANSACTION;
USE `biblioteka`;
INSERT INTO `biblioteka`.`ClanBiblioteke` (`idClanaBiblioteke`, `trajanjeClanarine`, `PrijavaNeregularnosti`, `Korisnici_idKorisnici`) VALUES (5, '2014-05-15', NULL, 5);
INSERT INTO `biblioteka`.`ClanBiblioteke` (`idClanaBiblioteke`, `trajanjeClanarine`, `PrijavaNeregularnosti`, `Korisnici_idKorisnici`) VALUES (6, '2013-01-01', NULL, 6);
INSERT INTO `biblioteka`.`ClanBiblioteke` (`idClanaBiblioteke`, `trajanjeClanarine`, `PrijavaNeregularnosti`, `Korisnici_idKorisnici`) VALUES (7, '2012-05-24', '', 7);

COMMIT;


-- -----------------------------------------------------
-- Data for table `biblioteka`.`Knjiga`
-- -----------------------------------------------------
START TRANSACTION;
USE `biblioteka`;
INSERT INTO `biblioteka`.`Knjiga` (`idKnjige`, `naslovKnjige`, `cenaIznajmljivanja`, `godinaIzdavanja`, `izdato`, `ClanBiblioteke_idClanaBiblioteke`) VALUES (1, 'Aaa', 8, 2014, false, NULL);
INSERT INTO `biblioteka`.`Knjiga` (`idKnjige`, `naslovKnjige`, `cenaIznajmljivanja`, `godinaIzdavanja`, `izdato`, `ClanBiblioteke_idClanaBiblioteke`) VALUES (2, 'Bbb', 13, 2012, false, NULL);
INSERT INTO `biblioteka`.`Knjiga` (`idKnjige`, `naslovKnjige`, `cenaIznajmljivanja`, `godinaIzdavanja`, `izdato`, `ClanBiblioteke_idClanaBiblioteke`) VALUES (3, 'Ccc', 11, 2014, false, NULL);
INSERT INTO `biblioteka`.`Knjiga` (`idKnjige`, `naslovKnjige`, `cenaIznajmljivanja`, `godinaIzdavanja`, `izdato`, `ClanBiblioteke_idClanaBiblioteke`) VALUES (4, 'Ddd', 24, 2013, false, NULL);
INSERT INTO `biblioteka`.`Knjiga` (`idKnjige`, `naslovKnjige`, `cenaIznajmljivanja`, `godinaIzdavanja`, `izdato`, `ClanBiblioteke_idClanaBiblioteke`) VALUES (5, 'Eee', 5, 2014, true, 5);
INSERT INTO `biblioteka`.`Knjiga` (`idKnjige`, `naslovKnjige`, `cenaIznajmljivanja`, `godinaIzdavanja`, `izdato`, `ClanBiblioteke_idClanaBiblioteke`) VALUES (6, 'Fff', 9, 2005, false, NULL);
INSERT INTO `biblioteka`.`Knjiga` (`idKnjige`, `naslovKnjige`, `cenaIznajmljivanja`, `godinaIzdavanja`, `izdato`, `ClanBiblioteke_idClanaBiblioteke`) VALUES (7, 'Ggg', 3, 2007, true, 5);
INSERT INTO `biblioteka`.`Knjiga` (`idKnjige`, `naslovKnjige`, `cenaIznajmljivanja`, `godinaIzdavanja`, `izdato`, `ClanBiblioteke_idClanaBiblioteke`) VALUES (8, 'Hhh', 3, 2000, true, 7);
INSERT INTO `biblioteka`.`Knjiga` (`idKnjige`, `naslovKnjige`, `cenaIznajmljivanja`, `godinaIzdavanja`, `izdato`, `ClanBiblioteke_idClanaBiblioteke`) VALUES (9, 'Iii', 6, 1991, false, NULL);
INSERT INTO `biblioteka`.`Knjiga` (`idKnjige`, `naslovKnjige`, `cenaIznajmljivanja`, `godinaIzdavanja`, `izdato`, `ClanBiblioteke_idClanaBiblioteke`) VALUES (10, 'Jjj', 18, 1988, false, NULL);
INSERT INTO `biblioteka`.`Knjiga` (`idKnjige`, `naslovKnjige`, `cenaIznajmljivanja`, `godinaIzdavanja`, `izdato`, `ClanBiblioteke_idClanaBiblioteke`) VALUES (11, 'Kkk', 23, 1980, false, NULL);
INSERT INTO `biblioteka`.`Knjiga` (`idKnjige`, `naslovKnjige`, `cenaIznajmljivanja`, `godinaIzdavanja`, `izdato`, `ClanBiblioteke_idClanaBiblioteke`) VALUES (12, 'Lll', 8, 2011, true, 6);
INSERT INTO `biblioteka`.`Knjiga` (`idKnjige`, `naslovKnjige`, `cenaIznajmljivanja`, `godinaIzdavanja`, `izdato`, `ClanBiblioteke_idClanaBiblioteke`) VALUES (13, 'Mmm', 17, 2012, true, 6);
INSERT INTO `biblioteka`.`Knjiga` (`idKnjige`, `naslovKnjige`, `cenaIznajmljivanja`, `godinaIzdavanja`, `izdato`, `ClanBiblioteke_idClanaBiblioteke`) VALUES (14, 'Nnn', 14, 2013, false, NULL);
INSERT INTO `biblioteka`.`Knjiga` (`idKnjige`, `naslovKnjige`, `cenaIznajmljivanja`, `godinaIzdavanja`, `izdato`, `ClanBiblioteke_idClanaBiblioteke`) VALUES (15, 'Ooo', 4, 2014, false, NULL);

COMMIT;


-- -----------------------------------------------------
-- Data for table `biblioteka`.`Izdavac`
-- -----------------------------------------------------
START TRANSACTION;
USE `biblioteka`;
INSERT INTO `biblioteka`.`Izdavac` (`idIzdavaca`, `nazivIzdavaca`, `dodatniDetaljiOIzdavacu`, `Adresa_idAdresa`) VALUES (1, 'Izd1', 'Nista posebno', 8);
INSERT INTO `biblioteka`.`Izdavac` (`idIzdavaca`, `nazivIzdavaca`, `dodatniDetaljiOIzdavacu`, `Adresa_idAdresa`) VALUES (2, 'Izd2', NULL, 9);
INSERT INTO `biblioteka`.`Izdavac` (`idIzdavaca`, `nazivIzdavaca`, `dodatniDetaljiOIzdavacu`, `Adresa_idAdresa`) VALUES (3, 'Izd3', 'Pravi ljudi', 10);
INSERT INTO `biblioteka`.`Izdavac` (`idIzdavaca`, `nazivIzdavaca`, `dodatniDetaljiOIzdavacu`, `Adresa_idAdresa`) VALUES (4, 'Izd4', NULL, 11);

COMMIT;


-- -----------------------------------------------------
-- Data for table `biblioteka`.`Pisac`
-- -----------------------------------------------------
START TRANSACTION;
USE `biblioteka`;
INSERT INTO `biblioteka`.`Pisac` (`idPisac`, `imePisac`, `datumRodjenja`, `dodatniDetaljiOPiscu`) VALUES (1, 'Pisac1', '1940-08-23', 'Poznat covjek');
INSERT INTO `biblioteka`.`Pisac` (`idPisac`, `imePisac`, `datumRodjenja`, `dodatniDetaljiOPiscu`) VALUES (2, 'Pisac2', '1985-04-19', NULL);
INSERT INTO `biblioteka`.`Pisac` (`idPisac`, `imePisac`, `datumRodjenja`, `dodatniDetaljiOPiscu`) VALUES (3, 'Pisac3', '1655-12-12', NULL);
INSERT INTO `biblioteka`.`Pisac` (`idPisac`, `imePisac`, `datumRodjenja`, `dodatniDetaljiOPiscu`) VALUES (4, 'Pisac4', '1789-05-16', 'Legenda');
INSERT INTO `biblioteka`.`Pisac` (`idPisac`, `imePisac`, `datumRodjenja`, `dodatniDetaljiOPiscu`) VALUES (5, 'Pisac5', '1991-10-04', NULL);

COMMIT;


-- -----------------------------------------------------
-- Data for table `biblioteka`.`Napisao`
-- -----------------------------------------------------
START TRANSACTION;
USE `biblioteka`;
INSERT INTO `biblioteka`.`Napisao` (`Knjiga_idKnjige`, `Pisac_idPisac`) VALUES (1, 1);
INSERT INTO `biblioteka`.`Napisao` (`Knjiga_idKnjige`, `Pisac_idPisac`) VALUES (2, 5);
INSERT INTO `biblioteka`.`Napisao` (`Knjiga_idKnjige`, `Pisac_idPisac`) VALUES (3, 2);
INSERT INTO `biblioteka`.`Napisao` (`Knjiga_idKnjige`, `Pisac_idPisac`) VALUES (3, 3);
INSERT INTO `biblioteka`.`Napisao` (`Knjiga_idKnjige`, `Pisac_idPisac`) VALUES (4, 1);
INSERT INTO `biblioteka`.`Napisao` (`Knjiga_idKnjige`, `Pisac_idPisac`) VALUES (4, 3);
INSERT INTO `biblioteka`.`Napisao` (`Knjiga_idKnjige`, `Pisac_idPisac`) VALUES (4, 5);
INSERT INTO `biblioteka`.`Napisao` (`Knjiga_idKnjige`, `Pisac_idPisac`) VALUES (5, 1);
INSERT INTO `biblioteka`.`Napisao` (`Knjiga_idKnjige`, `Pisac_idPisac`) VALUES (6, 4);
INSERT INTO `biblioteka`.`Napisao` (`Knjiga_idKnjige`, `Pisac_idPisac`) VALUES (7, 5);
INSERT INTO `biblioteka`.`Napisao` (`Knjiga_idKnjige`, `Pisac_idPisac`) VALUES (8, 3);
INSERT INTO `biblioteka`.`Napisao` (`Knjiga_idKnjige`, `Pisac_idPisac`) VALUES (9, 1);
INSERT INTO `biblioteka`.`Napisao` (`Knjiga_idKnjige`, `Pisac_idPisac`) VALUES (10, 2);
INSERT INTO `biblioteka`.`Napisao` (`Knjiga_idKnjige`, `Pisac_idPisac`) VALUES (10, 1);
INSERT INTO `biblioteka`.`Napisao` (`Knjiga_idKnjige`, `Pisac_idPisac`) VALUES (11, 3);
INSERT INTO `biblioteka`.`Napisao` (`Knjiga_idKnjige`, `Pisac_idPisac`) VALUES (12, 5);
INSERT INTO `biblioteka`.`Napisao` (`Knjiga_idKnjige`, `Pisac_idPisac`) VALUES (13, 5);
INSERT INTO `biblioteka`.`Napisao` (`Knjiga_idKnjige`, `Pisac_idPisac`) VALUES (14, 4);
INSERT INTO `biblioteka`.`Napisao` (`Knjiga_idKnjige`, `Pisac_idPisac`) VALUES (14, 5);
INSERT INTO `biblioteka`.`Napisao` (`Knjiga_idKnjige`, `Pisac_idPisac`) VALUES (15, 5);

COMMIT;


-- -----------------------------------------------------
-- Data for table `biblioteka`.`Kategorija`
-- -----------------------------------------------------
START TRANSACTION;
USE `biblioteka`;
INSERT INTO `biblioteka`.`Kategorija` (`idKategorija`, `nazivKategorije`) VALUES (1, 'Roman');
INSERT INTO `biblioteka`.`Kategorija` (`idKategorija`, `nazivKategorije`) VALUES (2, 'Autobiografija');
INSERT INTO `biblioteka`.`Kategorija` (`idKategorija`, `nazivKategorije`) VALUES (3, 'Putopis');
INSERT INTO `biblioteka`.`Kategorija` (`idKategorija`, `nazivKategorije`) VALUES (4, 'Arheoloska');
INSERT INTO `biblioteka`.`Kategorija` (`idKategorija`, `nazivKategorije`) VALUES (5, 'Enciklopedija');

COMMIT;


-- -----------------------------------------------------
-- Data for table `biblioteka`.`Pripada`
-- -----------------------------------------------------
START TRANSACTION;
USE `biblioteka`;
INSERT INTO `biblioteka`.`Pripada` (`Knjiga_idKnjige`, `Kategorija_idKategorija`) VALUES (1, 1);
INSERT INTO `biblioteka`.`Pripada` (`Knjiga_idKnjige`, `Kategorija_idKategorija`) VALUES (2, 2);
INSERT INTO `biblioteka`.`Pripada` (`Knjiga_idKnjige`, `Kategorija_idKategorija`) VALUES (2, 3);
INSERT INTO `biblioteka`.`Pripada` (`Knjiga_idKnjige`, `Kategorija_idKategorija`) VALUES (3, 4);
INSERT INTO `biblioteka`.`Pripada` (`Knjiga_idKnjige`, `Kategorija_idKategorija`) VALUES (4, 5);
INSERT INTO `biblioteka`.`Pripada` (`Knjiga_idKnjige`, `Kategorija_idKategorija`) VALUES (5, 5);
INSERT INTO `biblioteka`.`Pripada` (`Knjiga_idKnjige`, `Kategorija_idKategorija`) VALUES (6, 4);
INSERT INTO `biblioteka`.`Pripada` (`Knjiga_idKnjige`, `Kategorija_idKategorija`) VALUES (6, 3);
INSERT INTO `biblioteka`.`Pripada` (`Knjiga_idKnjige`, `Kategorija_idKategorija`) VALUES (7, 2);
INSERT INTO `biblioteka`.`Pripada` (`Knjiga_idKnjige`, `Kategorija_idKategorija`) VALUES (8, 1);
INSERT INTO `biblioteka`.`Pripada` (`Knjiga_idKnjige`, `Kategorija_idKategorija`) VALUES (9, 1);
INSERT INTO `biblioteka`.`Pripada` (`Knjiga_idKnjige`, `Kategorija_idKategorija`) VALUES (9, 3);
INSERT INTO `biblioteka`.`Pripada` (`Knjiga_idKnjige`, `Kategorija_idKategorija`) VALUES (9, 4);
INSERT INTO `biblioteka`.`Pripada` (`Knjiga_idKnjige`, `Kategorija_idKategorija`) VALUES (10, 5);
INSERT INTO `biblioteka`.`Pripada` (`Knjiga_idKnjige`, `Kategorija_idKategorija`) VALUES (11, 2);
INSERT INTO `biblioteka`.`Pripada` (`Knjiga_idKnjige`, `Kategorija_idKategorija`) VALUES (12, 4);
INSERT INTO `biblioteka`.`Pripada` (`Knjiga_idKnjige`, `Kategorija_idKategorija`) VALUES (12, 5);
INSERT INTO `biblioteka`.`Pripada` (`Knjiga_idKnjige`, `Kategorija_idKategorija`) VALUES (13, 1);
INSERT INTO `biblioteka`.`Pripada` (`Knjiga_idKnjige`, `Kategorija_idKategorija`) VALUES (14, 2);
INSERT INTO `biblioteka`.`Pripada` (`Knjiga_idKnjige`, `Kategorija_idKategorija`) VALUES (15, 2);
INSERT INTO `biblioteka`.`Pripada` (`Knjiga_idKnjige`, `Kategorija_idKategorija`) VALUES (15, 3);

COMMIT;


-- -----------------------------------------------------
-- Data for table `biblioteka`.`racun`
-- -----------------------------------------------------
START TRANSACTION;
USE `biblioteka`;
INSERT INTO `biblioteka`.`racun` (`idracun`, `datumIzdavanja`, `zaNaplatu`, `naplaceno`, `ClanBiblioteke_idClanaBiblioteke`) VALUES (5, '2012-10-04', 10, 1, 5);
INSERT INTO `biblioteka`.`racun` (`idracun`, `datumIzdavanja`, `zaNaplatu`, `naplaceno`, `ClanBiblioteke_idClanaBiblioteke`) VALUES (6, '2013-11-11', 88, 1, 5);
INSERT INTO `biblioteka`.`racun` (`idracun`, `datumIzdavanja`, `zaNaplatu`, `naplaceno`, `ClanBiblioteke_idClanaBiblioteke`) VALUES (7, '2014-05-06', 123, 1, 5);
INSERT INTO `biblioteka`.`racun` (`idracun`, `datumIzdavanja`, `zaNaplatu`, `naplaceno`, `ClanBiblioteke_idClanaBiblioteke`) VALUES (8, '2014-08-09', 46, 0, 5);
INSERT INTO `biblioteka`.`racun` (`idracun`, `datumIzdavanja`, `zaNaplatu`, `naplaceno`, `ClanBiblioteke_idClanaBiblioteke`) VALUES (9, '2014-08-01', 10, 1, 6);
INSERT INTO `biblioteka`.`racun` (`idracun`, `datumIzdavanja`, `zaNaplatu`, `naplaceno`, `ClanBiblioteke_idClanaBiblioteke`) VALUES (10, '2014-04-17', 10, 1, 7);
INSERT INTO `biblioteka`.`racun` (`idracun`, `datumIzdavanja`, `zaNaplatu`, `naplaceno`, `ClanBiblioteke_idClanaBiblioteke`) VALUES (11, '2014-07-13', 19, 0, 7);

COMMIT;


-- -----------------------------------------------------
-- Data for table `biblioteka`.`Izdaje`
-- -----------------------------------------------------
START TRANSACTION;
USE `biblioteka`;
INSERT INTO `biblioteka`.`Izdaje` (`Knjiga_idKnjige`, `Izdavac_idIzdavaca`) VALUES (1, 1);
INSERT INTO `biblioteka`.`Izdaje` (`Knjiga_idKnjige`, `Izdavac_idIzdavaca`) VALUES (2, 1);
INSERT INTO `biblioteka`.`Izdaje` (`Knjiga_idKnjige`, `Izdavac_idIzdavaca`) VALUES (3, 1);
INSERT INTO `biblioteka`.`Izdaje` (`Knjiga_idKnjige`, `Izdavac_idIzdavaca`) VALUES (4, 2);
INSERT INTO `biblioteka`.`Izdaje` (`Knjiga_idKnjige`, `Izdavac_idIzdavaca`) VALUES (5, 2);
INSERT INTO `biblioteka`.`Izdaje` (`Knjiga_idKnjige`, `Izdavac_idIzdavaca`) VALUES (6, 2);
INSERT INTO `biblioteka`.`Izdaje` (`Knjiga_idKnjige`, `Izdavac_idIzdavaca`) VALUES (7, 2);
INSERT INTO `biblioteka`.`Izdaje` (`Knjiga_idKnjige`, `Izdavac_idIzdavaca`) VALUES (7, 3);
INSERT INTO `biblioteka`.`Izdaje` (`Knjiga_idKnjige`, `Izdavac_idIzdavaca`) VALUES (8, 3);
INSERT INTO `biblioteka`.`Izdaje` (`Knjiga_idKnjige`, `Izdavac_idIzdavaca`) VALUES (9, 3);
INSERT INTO `biblioteka`.`Izdaje` (`Knjiga_idKnjige`, `Izdavac_idIzdavaca`) VALUES (9, 4);
INSERT INTO `biblioteka`.`Izdaje` (`Knjiga_idKnjige`, `Izdavac_idIzdavaca`) VALUES (10, 4);
INSERT INTO `biblioteka`.`Izdaje` (`Knjiga_idKnjige`, `Izdavac_idIzdavaca`) VALUES (11, 4);
INSERT INTO `biblioteka`.`Izdaje` (`Knjiga_idKnjige`, `Izdavac_idIzdavaca`) VALUES (12, 1);
INSERT INTO `biblioteka`.`Izdaje` (`Knjiga_idKnjige`, `Izdavac_idIzdavaca`) VALUES (13, 1);
INSERT INTO `biblioteka`.`Izdaje` (`Knjiga_idKnjige`, `Izdavac_idIzdavaca`) VALUES (14, 2);
INSERT INTO `biblioteka`.`Izdaje` (`Knjiga_idKnjige`, `Izdavac_idIzdavaca`) VALUES (15, 3);

COMMIT;

