-- phpMyAdmin SQL Dump
-- version 4.6.5.2
-- https://www.phpmyadmin.net/
--
-- Host: localhost:8889
-- Generation Time: Feb 28, 2018 at 01:41 AM
-- Server version: 5.6.35
-- PHP Version: 7.0.15

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";

--
-- Database: `tyler_wickline`
--

-- --------------------------------------------------------

--
-- Table structure for table `client`
--

CREATE TABLE `client` (
  `id` int(11) NOT NULL,
  `name` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `client`
--

INSERT INTO `client` (`id`, `name`) VALUES
(1, 'Tyler Wickline'),
(2, 'Waylon Dalton\r\n'),
(3, 'Marcus Cruz\r\n'),
(4, 'Eddie Randolph\r\n'),
(5, 'Hadassah Hartman\r\n'),
(6, 'Justine Henderson\r\n'),
(7, 'Thalia Cobb\r\n'),
(8, 'Angela Walker\r\n');

-- --------------------------------------------------------

--
-- Table structure for table `client_stylist`
--

CREATE TABLE `client_stylist` (
  `client_id` int(11) NOT NULL,
  `stylist_id` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `client_stylist`
--

INSERT INTO `client_stylist` (`client_id`, `stylist_id`) VALUES
(1, 1),
(2, 1);

-- --------------------------------------------------------

--
-- Table structure for table `stylist`
--

CREATE TABLE `stylist` (
  `id` int(11) NOT NULL,
  `name` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `stylist`
--

INSERT INTO `stylist` (`id`, `name`) VALUES
(1, 'Max Styles'),
(2, 'Daniel Walls\r\n'),
(3, 'Skyler Macdonald\r\n'),
(4, 'Mike Hamilton\r\n'),
(5, 'Brendan Mullen\r\n'),
(6, 'Emily Brewer\r\n'),
(7, 'Maggie Crane\r\n'),
(8, 'Albert Berger\r\n'),
(9, 'Enrique Richard\r\n');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `client`
--
ALTER TABLE `client`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `client_stylist`
--
ALTER TABLE `client_stylist`
  ADD KEY `client_id` (`client_id`,`stylist_id`),
  ADD KEY `client_id_2` (`client_id`,`stylist_id`),
  ADD KEY `stylist_id` (`stylist_id`);

--
-- Indexes for table `stylist`
--
ALTER TABLE `stylist`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `client`
--
ALTER TABLE `client`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;
--
-- AUTO_INCREMENT for table `stylist`
--
ALTER TABLE `stylist`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;
--
-- Constraints for dumped tables
--

--
-- Constraints for table `client_stylist`
--
ALTER TABLE `client_stylist`
  ADD CONSTRAINT `client_stylist_ibfk_1` FOREIGN KEY (`client_id`) REFERENCES `client` (`id`) ON DELETE CASCADE ON UPDATE NO ACTION,
  ADD CONSTRAINT `client_stylist_ibfk_2` FOREIGN KEY (`stylist_id`) REFERENCES `stylist` (`id`) ON DELETE CASCADE ON UPDATE NO ACTION;
