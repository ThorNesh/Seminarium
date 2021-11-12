-- phpMyAdmin SQL Dump
-- version 5.1.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Czas generowania: 12 Lis 2021, 21:18
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
(3, 'Adam', 'Nowak', '146702208', 'AdamN@wp.pl'),
(4, 'Sergent', 'Figgins', '264494303', 'sfiggins3@hatena.ne.jp'),
(5, 'Rusty', 'Leffek', '769517354', 'rleffek4@geocities.com'),
(6, 'Tybie', 'Willox', '555973457', 'twillox5@weibo.com'),
(7, 'Babbie', 'Narramor', '448114578', 'bnarramor6@hubpages.com'),
(8, 'Justina', 'Ferenczi', '657557398', 'jferenczi7@apple.com'),
(9, 'Wash', 'Rattray', '709608649', 'wrattray8@miitbeian.gov.cn'),
(10, 'Ardelis', 'Orable', '808102483', 'aorable9@boston.com'),
(32, 'string', 'string', 'string', 'string');

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

--
-- Zrzut danych tabeli `clients_vehicles_chains`
--

INSERT INTO `clients_vehicles_chains` (`Id`, `Client_Id`, `Vehicle_Id`, `Message`, `Service`) VALUES
(1, 2, 4, 'Test', 'Test'),
(2, 5, 2, 'Test2', 'Test2'),
(6, 32, 17, 'string', 'string'),
(7, 3, 3, 'Tescik', 'Tescik');

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `commisions`
--

CREATE TABLE `commisions` (
  `Id` int(11) UNSIGNED NOT NULL,
  `Chain_Id` int(11) UNSIGNED NOT NULL,
  `Code` varchar(10) NOT NULL,
  `Date_Of_Start` varchar(10) NOT NULL,
  `Hour_Of_Start` varchar(8) NOT NULL,
  `Status_Id` int(11) UNSIGNED NOT NULL,
  `Worker_Id` int(11) UNSIGNED NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Zrzut danych tabeli `commisions`
--

INSERT INTO `commisions` (`Id`, `Chain_Id`, `Code`, `Date_Of_Start`, `Hour_Of_Start`, `Status_Id`, `Worker_Id`) VALUES
(1, 1, 'AwS09FxR', '2021-11-10', '22:07:12', 1, 1),
(5, 6, 'bXjmgKC8', 'string', 'string', 1, 0),
(6, 7, 'FgqbrxKH', '21.07.25', '12:00:00', 1, 0);

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
-- Struktura tabeli dla tabeli `test`
--

CREATE TABLE `test` (
  `Id` int(11) UNSIGNED NOT NULL,
  `Var` varchar(30) NOT NULL,
  `Var2` varchar(30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Zrzut danych tabeli `test`
--

INSERT INTO `test` (`Id`, `Var`, `Var2`) VALUES
(1, 'test', 'test'),
(2, 'test', 'test'),
(3, 'test', 'test'),
(4, 'test', 'test'),
(5, 'test', 'test'),
(6, 'test', 'test'),
(7, 'test', 'test'),
(8, 'test', 'test'),
(9, 'test', 'test'),
(10, '10', '10'),
(11, '2', '5'),
(12, '5', '76');

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `users`
--

CREATE TABLE `users` (
  `id` int(11) UNSIGNED NOT NULL,
  `login` varchar(30) NOT NULL,
  `password` varchar(255) NOT NULL,
  `Worker_Id` int(11) UNSIGNED NOT NULL,
  `Is_Super_User` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Zrzut danych tabeli `users`
--

INSERT INTO `users` (`id`, `login`, `password`, `Worker_Id`, `Is_Super_User`) VALUES
(1, 'Urson', '$2a$11$ntw512lsBs14eA0j8dJjhOVtYwzX4NkdhdVX.y8s6WStxgIj0CjNu', 1, 1),
(2, 'Steffen', '$2a$11$tg7FrhAX4ZzilXNcdRYaBuv8uxNDSkPVflFxQNBpxHrLZYPux.Dt2', 2, 0),
(3, 'Aprilette', '$2a$11$pvATEAjL.xVj9oGYAuutEOnHToSPauxazW0bhq0fk1OKbIP2oCcte', 3, 0),
(4, 'Loutitia', '$2a$11$cQQZQ5SPV49VXpQs/V0sJeesjOiTq3PXuKGg2yGLTgs51lpJlzwja', 4, 1),
(5, 'Hallie', '$2a$11$bfGPskXCFp4d1MFgOpvQZ.dGfTVM3e65Wf3slmjzyqx0k9NTfi/Ha', 5, 1),
(6, 'Rudiger', '$2a$11$cNmAdayBGCs8AWEd1saBLeFRPkBwwnk3QtdhClIQvZRniwrS0vpja', 6, 0),
(7, 'Tomasine', '$2a$11$7JUtK3FuJWT3UAsKBj/3..GSK2GBgbb55yr1nHK/vOXkF.lElm5j.', 7, 0),
(8, 'Kev', '$2a$11$YIuLETp8kQ89dVqpIjlfiuGq684Venr8UT/Miy6mOUQaVy/V5g4fK', 8, 0),
(9, 'Barby', '$2a$11$Ebjn6Z8/5eyarS1H1sb07OnmelXXGL4eVeink3UiZ/JkUi3cCjVY.', 9, 1),
(10, 'Gram', '$2a$11$6KmyJVfU5DAKwgYh.EI8A.NIFmUMCkw7B8d2rapps4eDBxGSnFH72', 10, 1);

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

--
-- Zrzut danych tabeli `vehicles`
--

INSERT INTO `vehicles` (`Id`, `Brand`, `Model`, `Production_Year`, `Vin`, `Registration_Number`, `Engine_Power`, `Engine_Capacity`, `Fuel_Id`) VALUES
(1, 'Pontiac', 'Aztek', '2002', '1FMCU0D75AK929270', 'PA 25639', 61, 2.9, 4),
(2, 'Cadillac', 'CTS', '2010', 'SALAC2D40BA567061', 'SI 25639', 206, 0.2, 3),
(3, 'Lincoln', 'Continental', '1996', 'WA1AM74L29D164307', 'PKN 85B21', 500, 6, 5),
(4, 'Acura', 'Legend', '1988', 'WBAKN9C53FD240759', 'LV 25639', 70, 2.4, 4),
(5, 'Land Rover', 'Freelander', '2001', '3VW467AT6DM028984', 'BR 25639', 115, 2.7, 2),
(6, 'Ford', 'Falcon', '1967', '1GYFK56269R790935', 'CN 25639', 262, 2.8, 1),
(7, 'Honda', 'Odyssey', '1998', '5UXFB33533L656823', 'MW 25639', 158, 2.4, 1),
(8, 'Dodge', 'D250 Club', '1992', '3FAHP0CG4AR055761', 'PH 25639', 80, 0.4, 3),
(9, 'BMW', '6 Series', '2009', 'JH4NA126X3T382131', 'RU 25639', 34, 1.7, 1),
(10, 'Audi', 'S8', '2009', 'WAUXF78K99A474013', 'GR 25639', 123, 0.8, 2),
(17, 'string', 'string', 'stri', 'string', 'string', 0, 0, 1);

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
-- Zrzut danych tabeli `workers`
--

INSERT INTO `workers` (`Id`, `Name`, `LastName`, `Phone_Number`, `Email`, `Hired`) VALUES
(0, 'brak', 'brak', 'brak', 'brak', 1),
(1, 'Morty', 'Murfin', '961390756', 'mmurfin0@house.gov', 1),
(2, 'Serena', 'Gribbell', '539610498', 'sgribbell1@sfgate.com', 0),
(3, 'Inessa', 'McCaig', '972242759', 'imccaig2@umn.edu', 1),
(4, 'Arv', 'Pablos', '552604227', 'apablos3@artisteer.com', 0),
(5, 'Garrot', 'Exroll', '526664923', 'gexroll4@netscape.com', 1),
(6, 'Rosamund', 'Crosseland', '301317164', 'rcrosseland5@ucoz.ru', 1),
(7, 'Karmen', 'Turfes', '310271811', 'kturfes6@prnewswire.com', 0),
(8, 'Elisabetta', 'Renad', '677630591', 'erenad7@google.es', 0),
(9, 'Tandi', 'Paolicchi', '937437605', 'tpaolicchi8@simplemachines.org', 0),
(10, 'Ogden', 'Southorn', '855195309', 'osouthorn9@washington.edu', 1);

--
-- Indeksy dla zrzutów tabel
--

--
-- Indeksy dla tabeli `clients`
--
ALTER TABLE `clients`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `Phone_Number` (`Phone_Number`);

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
-- Indeksy dla tabeli `test`
--
ALTER TABLE `test`
  ADD PRIMARY KEY (`Id`);

--
-- Indeksy dla tabeli `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`id`),
  ADD UNIQUE KEY `login` (`login`),
  ADD KEY `Worker_Id` (`Worker_Id`);

--
-- Indeksy dla tabeli `vehicles`
--
ALTER TABLE `vehicles`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `Vin` (`Vin`),
  ADD KEY `Fuel_Id_2` (`Fuel_Id`);

--
-- Indeksy dla tabeli `workers`
--
ALTER TABLE `workers`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `UQ_Email` (`Email`);

--
-- AUTO_INCREMENT dla zrzuconych tabel
--

--
-- AUTO_INCREMENT dla tabeli `clients`
--
ALTER TABLE `clients`
  MODIFY `Id` int(11) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=42;

--
-- AUTO_INCREMENT dla tabeli `clients_vehicles_chains`
--
ALTER TABLE `clients_vehicles_chains`
  MODIFY `Id` int(11) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT dla tabeli `commisions`
--
ALTER TABLE `commisions`
  MODIFY `Id` int(11) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

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
-- AUTO_INCREMENT dla tabeli `test`
--
ALTER TABLE `test`
  MODIFY `Id` int(11) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=15;

--
-- AUTO_INCREMENT dla tabeli `users`
--
ALTER TABLE `users`
  MODIFY `id` int(11) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT dla tabeli `vehicles`
--
ALTER TABLE `vehicles`
  MODIFY `Id` int(11) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=18;

--
-- AUTO_INCREMENT dla tabeli `workers`
--
ALTER TABLE `workers`
  MODIFY `Id` int(11) UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;

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
-- Ograniczenia dla tabeli `users`
--
ALTER TABLE `users`
  ADD CONSTRAINT `users_ibfk_1` FOREIGN KEY (`Worker_Id`) REFERENCES `workers` (`Id`);

--
-- Ograniczenia dla tabeli `vehicles`
--
ALTER TABLE `vehicles`
  ADD CONSTRAINT `vehicles_ibfk_1` FOREIGN KEY (`Fuel_Id`) REFERENCES `fuel_types` (`Id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
