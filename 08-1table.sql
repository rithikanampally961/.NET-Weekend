DROP TABLE IF EXISTS Employees;


CREATE TABLE Employees (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100),
    Age INT,
    Department NVARCHAR(100)
);

ALTER TABLE Employees
ADD Salary DECIMAL(18, 2);

SELECT * FROM Employees;

------------------------------------------------------------
CREATE TABLE Employees1 (
    EmployeeID INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(50),
    LastName NVARCHAR(50),
    JoinDate DATE
);
GO

INSERT INTO Employees1 (FirstName, LastName, JoinDate) 
VALUES 
('Aaradhya', 'Iyer', '2025-04-21'),
('Kavya', 'Shetty', '2024-12-13'),
('Manoj', 'Verma', '2023-04-06'),
('Sneha', 'Rao', '2025-05-01'),
('Deepika', 'Kandula', '2024-11-12'),
('Pooja', 'Nair', '2025-03-12');

INSERT INTO Employees1 (FirstName, LastName, JoinDate)  
VALUES  
('Rohan', 'Thakur', '2024-08-15'),  
('Ishita', 'Mehta', '2024-09-10'),  
('Siddharth', 'Chowdhury', '2024-10-25'),  
('Tanvi', 'Bose', '2024-11-20');

CREATE INDEX IX_Employees_JoinDate ON Employees1 (JoinDate);
GO


------------------------------------------------------------
CREATE TABLE Accounts (
    AccountNumber INT PRIMARY KEY,
    AccountType NVARCHAR(20),  
    Balance DECIMAL(18,2)
);


INSERT INTO Accounts VALUES (101, 'Savings', 10000.00);
INSERT INTO Accounts VALUES (102, 'Checking', 5000.00);


----------------------------------------------------------------
CREATE TABLE ChatMessages (
    MessageID INT IDENTITY(1,1) PRIMARY KEY,
    Sender NVARCHAR(100),
    Receiver NVARCHAR(100),
    MessageText NVARCHAR(MAX),
    SentAt DATETIME DEFAULT GETDATE()
);
