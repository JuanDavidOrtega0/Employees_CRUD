-- Active: 1713397710568@@bsk9tmgwoj6lylnpcxf5-mysql.services.clever-cloud.com@3306@bsk9tmgwoj6lylnpcxf5

CREATE TABLE Employees (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Name VARCHAR(45),
    LastName VARCHAR(45),
    Email VARCHAR(255),
    Phone VARCHAR(15),
    Address VARCHAR(75),
    Role VARCHAR(25),
    Password VARCHAR(125)
);

CREATE TABLE Records (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    RegisterEntry VARCHAR(45),
    RegisterExit VARCHAR(45),
    Employee_Id INT,
    FOREIGN KEY (Employee_Id) REFERENCES Employees(Id)
);

INSERT INTO `Employees`
(`Name`, `LastName`, `Email`, `Phone`, `Address`, `Role`, `Password`)
VALUES
("Juan", "Ortega", "juan@gmail.com", "3053527678", "Medellín", "Administrador", "123"),
("Angelica", "Martinez", "angelica@gmail.com", "3053434374", "Envidago", "Administrador", "123")

SELECT * FROM Employees;

SELECT * FROM Records;