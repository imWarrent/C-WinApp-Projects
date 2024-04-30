-- phpMyAdmin SQL Dump
-- version 5.1.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Dec 09, 2021 at 10:26 PM
-- Server version: 10.4.21-MariaDB
-- PHP Version: 8.0.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `bangketa`
--

-- --------------------------------------------------------

--
-- Table structure for table `business`
--

CREATE TABLE `business` (
  `business_id` int(15) NOT NULL,
  `username` text NOT NULL,
  `password` text NOT NULL,
  `business_name` text NOT NULL,
  `owner` text NOT NULL,
  `address` text NOT NULL,
  `contact` bigint(115) NOT NULL,
  `email` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `business`
--

INSERT INTO `business` (`business_id`, `username`, `password`, `business_name`, `owner`, `address`, `contact`, `email`) VALUES
(1, '1', '1', 'Bangketa', '1', '1', 1, '1'),
(2, 'MCDO', 'MCDO', 'MCDONALDS', 'MCDO', 'MCDOO', 1, '1'),
(3, 'fds', 'fsd', 'fd', 'fsd', 'fds', 2147483647, 'fds'),
(4, 'gfhgf', 'fds', 'fds', 'fds', 'fds', 9654667786, 'fds'),
(5, 'jabi', 'jabi', 'Jollibee Incorporation', 'Jollibee', 'Valenzuela', 9475637485, 'jollibee@jabi.com');

-- --------------------------------------------------------

--
-- Table structure for table `product`
--

CREATE TABLE `product` (
  `prod_id` int(15) NOT NULL,
  `business_user` text NOT NULL,
  `product_name` text NOT NULL,
  `category` text NOT NULL,
  `brand` text NOT NULL,
  `quantity` int(115) DEFAULT NULL,
  `price` double(115,2) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `product`
--

INSERT INTO `product` (`prod_id`, `business_user`, `product_name`, `category`, `brand`, `quantity`, `price`) VALUES
(1, '1', '1', '1', '1', 1, 1.00),
(2, 'MCDO', 'FILLETS', 'FOODS', 'MCDOW', 1, 11.00),
(3, 'MCDO', 'BURGER', 'FOODS', 'MCDOWW', 10, 15.00),
(4, 'MCDO', 'APPLE PIE', 'FOODS', 'JABI', 0, 100.00),
(5, 'MCDO', 'APPLE PIES', 'FOODSS', 'JABII', 1, 1000.00),
(6, 'MCDO', 'FILLETSS', 'FOODS', 'MCDOW', 0, 11.00),
(7, 'MCDO', 'FILLETSS', 'FOODS', 'MCDOW', 0, 11.00),
(8, 'jabi', 'Spaghetti', 'Food', 'Jollibee', 0, 50.00),
(9, 'jabi', 'Burger', 'Food', 'Jollibee', 0, 35.00),
(12, 'jabi', 'Coke', 'Drinks', 'Coca Cola', 0, 45.00);

-- --------------------------------------------------------

--
-- Table structure for table `sales`
--

CREATE TABLE `sales` (
  `sales_id` int(15) NOT NULL,
  `product_id` int(15) NOT NULL,
  `business_user` text NOT NULL,
  `date` text NOT NULL,
  `month` text NOT NULL,
  `year` text NOT NULL,
  `quantity` int(115) NOT NULL,
  `total` double(115,2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dumping data for table `sales`
--

INSERT INTO `sales` (`sales_id`, `product_id`, `business_user`, `date`, `month`, `year`, `quantity`, `total`) VALUES
(1, 198, '0', 'December 09, 2021', '', '', 1, 10000.00),
(2, 198, '0', 'December 09, 2021', '', '', 1, 10000.00),
(3, 0, '0', 'December 09, 2021', '', '', 0, 0.00),
(4, 1, '0', 'December 10, 2021', '', '', 1, 1.00),
(5, 1, 'MCDO', 'December 10, 2021', '', '', 1, 1.00),
(6, 2, 'MCDO', 'December 10, 2021', '', '', 5, 55.00),
(7, 2, 'MCDO', 'December 10, 2021', '', '', 10, 110.00),
(8, 3, 'MCDO', 'December 10, 2021', 'December', '2021', 1, 15.00),
(9, 6, 'MCDO', 'December 10, 2021', 'December', '2021', 1, 11.00),
(10, 7, 'MCDO', 'December 10, 2021', 'December', '2021', 1, 11.00),
(11, 2, 'MCDO', 'December 10, 2021', 'December', '2022', 19, 209.00),
(12, 12, 'jabi', 'December 10, 2021', 'December', '2021', 10, 450.00),
(13, 9, 'jabi', 'December 10, 2021', 'December', '2021', 5, 175.00),
(14, 8, 'jabi', 'December 10, 2021', 'December', '2021', 15, 750.00);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `business`
--
ALTER TABLE `business`
  ADD PRIMARY KEY (`business_id`);

--
-- Indexes for table `product`
--
ALTER TABLE `product`
  ADD PRIMARY KEY (`prod_id`);

--
-- Indexes for table `sales`
--
ALTER TABLE `sales`
  ADD PRIMARY KEY (`sales_id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `business`
--
ALTER TABLE `business`
  MODIFY `business_id` int(15) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT for table `product`
--
ALTER TABLE `product`
  MODIFY `prod_id` int(15) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;

--
-- AUTO_INCREMENT for table `sales`
--
ALTER TABLE `sales`
  MODIFY `sales_id` int(15) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=15;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
