
CREATE DATABASE `unity_mysql`;

CREATE TABLE `unity_mysql`.`scoreboard` 
( `ID` INT(11) NOT NULL AUTO_INCREMENT,
  `USER` VARCHAR(20) NOT NULL , 
  `SCORE`  DECIMAL(20,2) NOT NULL,
  `MOMENT` TIMESTAMP NOT NULL , 
  PRIMARY KEY (`ID`)) 

  ENGINE = InnoDB;
  
-- SOME DATA
INSERT INTO `scoreboard`(`USER`, `SCORE`, `MOMENT`) 
VALUES 
('Sonic', 10000.00, '2019-07-30 14:30:58'),
('Tails', 20000.00, '2019-07-30 14:31:58'),
('Shadow', 30000.00, '2019-07-30 14:32:58'),
('Knuckles', 40000.00, '2019-07-30 14:33:58'),
('Tails', 50000.00, '2019-07-30 14:34:58'),
('Big', 60000.00, '2019-07-30 14:35:58'),
('Metal Sonic', 70000.00, '2019-07-30 14:36:58'),
('Silver', 80000.00, '2019-07-30 14:37:58'),
('Amy', 90000.00, '2019-07-30 14:38:58'),
('Mighty', 100000.00, '2019-07-30 14:39:58');