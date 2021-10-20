-- phpMyAdmin SQL Dump
-- version 5.1.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Czas generowania: 20 Paź 2021, 20:35
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

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `pracownicy`
--

CREATE TABLE `pracownicy` (
  `id_pracownika` int(10) UNSIGNED NOT NULL,
  `imie` varchar(255) NOT NULL,
  `nazwisko` varchar(255) NOT NULL,
  `pesel` int(11) NOT NULL,
  `numer_telefonu` int(9) NOT NULL,
  `adres_email` varchar(255) NOT NULL,
  `id_poziom_pracownika` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

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
  `numer_zlecenia` int(50) NOT NULL,
  `status_zlecenia` varchar(255) NOT NULL,
  `data_wystawienia` date NOT NULL,
  `czas_wystawienia` datetime NOT NULL,
  `id_pracownika` int(11) NOT NULL,
  `id_klienta` int(11) NOT NULL,
  `id_pojazdu` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

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
  MODIFY `id_poziom_pracownika` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT dla tabeli `pracownicy`
--
ALTER TABLE `pracownicy`
  MODIFY `id_pracownika` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT dla tabeli `rodzaj_silnika`
--
ALTER TABLE `rodzaj_silnika`
  MODIFY `id_rodzaj_silnika` int(10) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=14;

--
-- AUTO_INCREMENT dla tabeli `zlecenie`
--
ALTER TABLE `zlecenie`
  MODIFY `id_zlecenia` int(10) UNSIGNED NOT NULL AUTO_INCREMENT;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
