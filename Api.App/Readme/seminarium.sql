-- phpMyAdmin SQL Dump
-- version 5.1.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Czas generowania: 31 Paź 2021, 12:00
-- Wersja serwera: 10.4.21-MariaDB
-- Wersja PHP: 8.0.11

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Baza danych: `seminarium`
--

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `klient`
--

CREATE TABLE `klient` (
  `id_klienta` int(10) UNSIGNED NOT NULL,
  `imie` varchar(255) NOT NULL,
  `nazwisko` varchar(255) NOT NULL,
  `numer_telefonu` varchar(9) NOT NULL,
  `adres_email` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Zrzut danych tabeli `klient`
--

INSERT INTO `klient` (`id_klienta`, `imie`, `nazwisko`, `numer_telefonu`, `adres_email`) VALUES
(1, 'Marek', 'Nowak', '963852654', 'marnow@wp.pl'),
(2, 'Aneta', 'Kowalska', '855236789', 'anetakowalska852@interia.pl'),
(5, 'Kacper', 'Kolarz', '857469523', '123@pl.op'),
(6, 'Arek', 'Dobródzki', '857859523', 'wp@wp.pl');

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `pojazd`
--

CREATE TABLE `pojazd` (
  `id_pojazdu` int(10) UNSIGNED NOT NULL,
  `numer_vin` varchar(17) NOT NULL,
  `marka` varchar(255) NOT NULL,
  `model` varchar(255) NOT NULL,
  `kolor` text NOT NULL,
  `rocznik` int(4) UNSIGNED NOT NULL,
  `id_rodzaj_silnika` int(11) UNSIGNED NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Zrzut danych tabeli `pojazd`
--

INSERT INTO `pojazd` (`id_pojazdu`, `numer_vin`, `marka`, `model`, `kolor`, `rocznik`, `id_rodzaj_silnika`) VALUES
(1, 'VF7RWRHF854098257', 'Opel', 'Signum', 'Z20r', 2007, 13),
(2, 'VF1CR1S0H47482095', 'Audi', 'A6', 'R20t', 2003, 8),
(3, '5NPD84LF0HH135026', 'Mercedes', 'GT S', 'B30c', 1990, 10),
(4, 'WDB2030421A675993', 'Bmw', 'Bawara', 'B30c', 1995, 11);

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `poziom_pracownika`
--

CREATE TABLE `poziom_pracownika` (
  `id_poziom_pracownika` int(10) UNSIGNED NOT NULL,
  `rodzaj_pracownika` varchar(255) NOT NULL,
  `status_pracownika` varchar(255) NOT NULL,
  `rodzaj_przepustki` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Zrzut danych tabeli `poziom_pracownika`
--

INSERT INTO `poziom_pracownika` (`id_poziom_pracownika`, `rodzaj_pracownika`, `status_pracownika`, `rodzaj_przepustki`) VALUES
(1, 'Pracownik produkcji', 'Pracownik fizyczny', 'Zwykła'),
(2, 'Brygadzista', 'Zarządzanie działem', 'Kierownicza'),
(3, 'Kierownik', 'Zarządzanie działami', 'Kierownicza'),
(4, 'Dyrektor', 'Zarządzanie', 'Dyrektorska');

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `pracownicy`
--

CREATE TABLE `pracownicy` (
  `id_pracownika` int(10) UNSIGNED NOT NULL,
  `imie` varchar(255) NOT NULL,
  `nazwisko` varchar(255) NOT NULL,
  `pesel` varchar(11) NOT NULL,
  `numer_telefonu` varchar(9) NOT NULL,
  `adres_email` varchar(255) NOT NULL,
  `id_poziom_pracownika` int(11) UNSIGNED NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Zrzut danych tabeli `pracownicy`
--

INSERT INTO `pracownicy` (`id_pracownika`, `imie`, `nazwisko`, `pesel`, `numer_telefonu`, `adres_email`, `id_poziom_pracownika`) VALUES
(1, 'Camile', 'Anetts', '2147483647', '755219255', 'canetts0@ebay.co.uk', 3),
(2, 'Brenda', 'Danton', '2147483647', '556886123', 'bdanton1@homestead.com', 2),
(3, 'Lesli', 'Commings', '2147483647', '935148832', 'lcommings2@rediff.com', 2),
(4, 'Doro', 'Bowmaker', '2147483647', '921155060', 'dbowmaker3@vinaora.com', 3),
(5, 'Anthia', 'Gounard', '2147483647', '412777834', 'agounard4@cornell.edu', 4),
(6, 'Ferdinanda', 'Baudon', '2147483647', '515841741', 'fbaudon5@ucoz.com', 1),
(7, 'Franny', 'Uman', '2147483647', '173399744', 'fuman6@vinaora.com', 2),
(8, 'Chelsey', 'Scottini', '2147483647', '717477878', 'cscottini7@reverbnation.com', 2),
(9, 'Jerrold', 'Stubbs', '2147483647', '683343497', 'jstubbs8@myspace.com', 2),
(10, 'Susannah', 'Huortic', '2147483647', '850643075', 'shuortic9@ameblo.jp', 4);

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `rodzaj_silnika`
--

CREATE TABLE `rodzaj_silnika` (
  `id_rodzaj_silnika` int(10) UNSIGNED NOT NULL,
  `moc` int(20) UNSIGNED NOT NULL,
  `pojemność` float NOT NULL,
  `rodzaj_paliwa` varchar(30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Zrzut danych tabeli `rodzaj_silnika`
--

INSERT INTO `rodzaj_silnika` (`id_rodzaj_silnika`, `moc`, `pojemność`, `rodzaj_paliwa`) VALUES
(8, 130, 2.3, 'Diesel'),
(9, 90, 1.9, 'Diesel'),
(10, 30, 1.3, 'Diesel'),
(11, 125, 1.8, 'Benzyna'),
(12, 180, 2.5, 'Benzyna'),
(13, 140, 1.8, 'Benzyna');

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `zlecenie`
--

CREATE TABLE `zlecenie` (
  `id_zlecenia` int(10) UNSIGNED NOT NULL,
  `nazwa_zlecenia` varchar(255) NOT NULL,
  `opis_zlecenia` varchar(255) NOT NULL,
  `status_zlecenia` varchar(255) NOT NULL,
  `czas_wystawienia` date NOT NULL,
  `id_pracownika` int(11) UNSIGNED NOT NULL,
  `id_klienta` int(11) UNSIGNED NOT NULL,
  `id_pojazdu` int(11) UNSIGNED NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Zrzut danych tabeli `zlecenie`
--

INSERT INTO `zlecenie` (`id_zlecenia`, `nazwa_zlecenia`, `opis_zlecenia`, `status_zlecenia`, `czas_wystawienia`, `id_pracownika`, `id_klienta`, `id_pojazdu`) VALUES
(1, 'Test', 'Test', 'Trwa', '2021-10-31', 3, 2, 1),
(2, 'Test2', 'Test2', 'Oczekuje', '2018-02-20', 4, 5, 4),
(3, 'Updated', 'Updated', 'Trwa', '2021-10-31', 9, 1, 2);

--
-- Indeksy dla zrzutów tabel
--

--
-- Indeksy dla tabeli `klient`
--
ALTER TABLE `klient`
  ADD PRIMARY KEY (`id_klienta`);

--
-- Indeksy dla tabeli `pojazd`
--
ALTER TABLE `pojazd`
  ADD PRIMARY KEY (`id_pojazdu`),
  ADD UNIQUE KEY `numer_vin` (`numer_vin`);

--
-- Indeksy dla tabeli `poziom_pracownika`
--
ALTER TABLE `poziom_pracownika`
  ADD PRIMARY KEY (`id_poziom_pracownika`);

--
-- Indeksy dla tabeli `pracownicy`
--
ALTER TABLE `pracownicy`
  ADD PRIMARY KEY (`id_pracownika`);

--
-- Indeksy dla tabeli `rodzaj_silnika`
--
ALTER TABLE `rodzaj_silnika`
  ADD PRIMARY KEY (`id_rodzaj_silnika`);

--
-- Indeksy dla tabeli `zlecenie`
--
ALTER TABLE `zlecenie`
  ADD PRIMARY KEY (`id_zlecenia`);

--
-- AUTO_INCREMENT dla zrzuconych tabel
--

--
-- AUTO_INCREMENT dla tabeli `klient`
--
ALTER TABLE `klient`
  MODIFY `id_klienta` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT dla tabeli `pojazd`
--
ALTER TABLE `pojazd`
  MODIFY `id_pojazdu` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT dla tabeli `poziom_pracownika`
--
ALTER TABLE `poziom_pracownika`
  MODIFY `id_poziom_pracownika` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT dla tabeli `pracownicy`
--
ALTER TABLE `pracownicy`
  MODIFY `id_pracownika` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT dla tabeli `rodzaj_silnika`
--
ALTER TABLE `rodzaj_silnika`
  MODIFY `id_rodzaj_silnika` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=14;

--
-- AUTO_INCREMENT dla tabeli `zlecenie`
--
ALTER TABLE `zlecenie`
  MODIFY `id_zlecenia` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
