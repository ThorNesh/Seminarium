-- phpMyAdmin SQL Dump
-- version 5.1.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Czas generowania: 04 Lis 2021, 18:46
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
-- Baza danych: `warsztat_samochodowy`
--

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `clients`
--

CREATE TABLE `clients` (
  `Id` int(11) UNSIGNED NOT NULL,
  `Name` varchar(50) NOT NULL,
  `LastName` varchar(50) NOT NULL,
  `Phone_Number` varchar(9) NOT NULL,
  `Email` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Zrzut danych tabeli `clients`
--

INSERT INTO `clients` (`Id`, `Name`, `LastName`, `Phone_Number`, `Email`) VALUES
(1, 'Yasmeen', 'De la Feld', '387766289', 'ydelafeld0@jugem.jp'),
(2, 'Antonius', 'Fawdrie', '511677847', 'afawdrie1@usa.gov'),
(3, 'Niles', 'Dubique', '146702208', 'ndubique2@typepad.com'),
(4, 'Sergent', 'Figgins', '264494303', 'sfiggins3@hatena.ne.jp'),
(5, 'Rusty', 'Leffek', '769517354', 'rleffek4@geocities.com'),
(6, 'Tybie', 'Willox', '555973457', 'twillox5@weibo.com'),
(7, 'Babbie', 'Narramor', '448114578', 'bnarramor6@hubpages.com'),
(8, 'Justina', 'Ferenczi', '657557398', 'jferenczi7@apple.com'),
(9, 'Wash', 'Rattray', '709608649', 'wrattray8@miitbeian.gov.cn'),
(10, 'Ardelis', 'Orable', '808102483', 'aorable9@boston.com');

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `clients_vehicles_chains`
--

CREATE TABLE `clients_vehicles_chains` (
  `Id` int(11) UNSIGNED NOT NULL,
  `Client_Id` int(11) UNSIGNED NOT NULL,
  `Vehicle_Id` int(11) UNSIGNED NOT NULL,
  `Message` varchar(255) NOT NULL,
  `Service` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `commisions`
--

CREATE TABLE `commisions` (
  `Id` int(11) NOT NULL,
  `Chain_Id` int(11) UNSIGNED NOT NULL,
  `Code` varchar(10) NOT NULL,
  `Date_Of_Start` date NOT NULL,
  `Hour_Of_Start` time NOT NULL,
  `Status_Id` int(11) UNSIGNED NOT NULL,
  `Worker_Id` int(11) UNSIGNED NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `fuel_types`
--

CREATE TABLE `fuel_types` (
  `Id` int(11) UNSIGNED NOT NULL,
  `Name` varchar(30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Zrzut danych tabeli `fuel_types`
--

INSERT INTO `fuel_types` (`Id`, `Name`) VALUES
(1, 'Benzyna'),
(2, 'Benzyna+LPG'),
(3, 'Diesel'),
(4, 'Hybryda'),
(5, 'Elektryczny');

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `statuses`
--

CREATE TABLE `statuses` (
  `Id` int(11) UNSIGNED NOT NULL,
  `Name` varchar(30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Zrzut danych tabeli `statuses`
--

INSERT INTO `statuses` (`Id`, `Name`) VALUES
(1, 'Oczekuje'),
(2, 'W trakcie'),
(3, 'Gotowy do odbioru'),
(4, 'Odebrany');

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `users`
--

CREATE TABLE `users` (
  `id` int(11) NOT NULL,
  `login` text NOT NULL,
  `password` text NOT NULL,
  `Worker_Id` int(11) UNSIGNED NOT NULL,
  `Is_Super_User` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `vehicles`
--

CREATE TABLE `vehicles` (
  `Id` int(11) UNSIGNED NOT NULL,
  `Brand` varchar(30) NOT NULL,
  `Model` varchar(30) NOT NULL,
  `Production_Year` varchar(4) NOT NULL,
  `Vin` varchar(17) NOT NULL,
  `Registration_Number` varchar(10) NOT NULL,
  `Engine_Power` int(4) UNSIGNED NOT NULL,
  `Engine_Capacity` float UNSIGNED NOT NULL,
  `Fuel_Id` int(10) UNSIGNED NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `workers`
--

CREATE TABLE `workers` (
  `Id` int(11) UNSIGNED NOT NULL,
  `Name` varchar(30) NOT NULL,
  `LastName` varchar(30) NOT NULL,
  `Phone_Number` varchar(9) NOT NULL,
  `Email` varchar(50) NOT NULL,
  `Hired` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Indeksy dla zrzutów tabel
--

--
-- Indeksy dla tabeli `clients`
--
ALTER TABLE `clients`
  ADD PRIMARY KEY (`Id`);

--
-- Indeksy dla tabeli `clients_vehicles_chains`
--
ALTER TABLE `clients_vehicles_chains`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `Client_Id` (`Client_Id`),
  ADD KEY `Vehicle_Id` (`Vehicle_Id`);

--
-- Indeksy dla tabeli `commisions`
--
ALTER TABLE `commisions`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `Code` (`Code`),
  ADD KEY `Chain_Id` (`Chain_Id`),
  ADD KEY `Status_Id` (`Status_Id`),
  ADD KEY `Worker_Id` (`Worker_Id`);

--
-- Indeksy dla tabeli `fuel_types`
--
ALTER TABLE `fuel_types`
  ADD PRIMARY KEY (`Id`);

--
-- Indeksy dla tabeli `statuses`
--
ALTER TABLE `statuses`
  ADD PRIMARY KEY (`Id`);

--
-- Indeksy dla tabeli `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`id`);

--
-- Indeksy dla tabeli `vehicles`
--
ALTER TABLE `vehicles`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `Fuel_Id_2` (`Fuel_Id`);

--
-- Indeksy dla tabeli `workers`
--
ALTER TABLE `workers`
  ADD PRIMARY KEY (`Id`);

--
-- AUTO_INCREMENT dla zrzuconych tabel
--

--
-- AUTO_INCREMENT dla tabeli `clients`
--
ALTER TABLE `clients`
  MODIFY `Id` int(11) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;

--
-- AUTO_INCREMENT dla tabeli `clients_vehicles_chains`
--
ALTER TABLE `clients_vehicles_chains`
  MODIFY `Id` int(11) UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT dla tabeli `commisions`
--
ALTER TABLE `commisions`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT dla tabeli `fuel_types`
--
ALTER TABLE `fuel_types`
  MODIFY `Id` int(11) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT dla tabeli `statuses`
--
ALTER TABLE `statuses`
  MODIFY `Id` int(11) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT dla tabeli `users`
--
ALTER TABLE `users`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT dla tabeli `vehicles`
--
ALTER TABLE `vehicles`
  MODIFY `Id` int(11) UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT dla tabeli `workers`
--
ALTER TABLE `workers`
  MODIFY `Id` int(11) UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- Ograniczenia dla zrzutów tabel
--

--
-- Ograniczenia dla tabeli `clients_vehicles_chains`
--
ALTER TABLE `clients_vehicles_chains`
  ADD CONSTRAINT `clients_vehicles_chains_ibfk_1` FOREIGN KEY (`Client_Id`) REFERENCES `clients` (`Id`),
  ADD CONSTRAINT `clients_vehicles_chains_ibfk_2` FOREIGN KEY (`Vehicle_Id`) REFERENCES `vehicles` (`Id`);

--
-- Ograniczenia dla tabeli `commisions`
--
ALTER TABLE `commisions`
  ADD CONSTRAINT `commisions_ibfk_1` FOREIGN KEY (`Chain_Id`) REFERENCES `clients_vehicles_chains` (`Id`),
  ADD CONSTRAINT `commisions_ibfk_2` FOREIGN KEY (`Status_Id`) REFERENCES `statuses` (`Id`),
  ADD CONSTRAINT `commisions_ibfk_3` FOREIGN KEY (`Worker_Id`) REFERENCES `workers` (`Id`);

--
-- Ograniczenia dla tabeli `vehicles`
--
ALTER TABLE `vehicles`
  ADD CONSTRAINT `vehicles_ibfk_1` FOREIGN KEY (`Fuel_Id`) REFERENCES `fuel_types` (`Id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
