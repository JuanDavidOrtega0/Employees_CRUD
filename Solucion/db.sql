-- Active: 1713213038695@@bsk9tmgwoj6lylnpcxf5-mysql.services.clever-cloud.com@3306@bsk9tmgwoj6lylnpcxf5

CREATE TABLE Employees (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Name VARCHAR(45),
    LastName VARCHAR(45),
    Email VARCHAR(255),
    Phone VARCHAR(15),
    Address VARCHAR(75),
    Password VARCHAR(125)
);