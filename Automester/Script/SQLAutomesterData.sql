-- Insert sample data into Customer table
INSERT INTO Customer (Name, Address, Email, Phone)
VALUES 
('John Doe', '123 Main St, City A', 'johndoe@example.com', '555-1234'),
('Jane Smith', '456 Oak St, City B', 'janesmith@example.com', '555-5678'),
('Acme Corp', '789 Elm St, City C', 'contact@acme.com', '555-9999');

-- Insert sample data into Car table
INSERT INTO Car (CustomerID, RegistrationNumber, Brand, Model, Year, Mileage)
VALUES 
(1, 'ABC123', 'Toyota', 'Corolla', 2015, 75000),
(1, 'DEF456', 'Honda', 'Civic', 2018, 50000),
(2, 'XYZ789', 'Ford', 'Focus', 2017, 62000),
(3, 'LMN101', 'Chevrolet', 'Malibu', 2019, 45000);

-- Insert sample data into Repair table
INSERT INTO Repair (CarID, StartDate, EndDate, Description, Price, Status)
VALUES 
(1, '2024-09-01', NULL, 'Brake pad replacement', 200.00, 'in progress'),
(2, '2024-09-10', '2024-09-12', 'Oil change', 50.00, 'completed'),
(3, '2024-09-05', NULL, 'Transmission repair', 1200.00, 'in progress'),
(4, '2024-09-15', NULL, 'Engine diagnostics', 300.00, 'in progress');

-- Insert sample data into Workshop table
-- Only add cars that are currently in the workshop (based on in-progress repairs)
INSERT INTO Workshop (CarID, ArrivalDate)
VALUES 
(1, '2024-09-01'),
(3, '2024-09-05'),
(4, '2024-09-15');

-- Insert sample data into ReadyForPickup table
-- Add cars that have completed repairs
INSERT INTO ReadyForPickup (CarID, ReadyDate)
VALUES 
(2, '2024-09-12');

-- Insert sample data into Employee table
INSERT INTO Employee (Name, HireDate, Position)
VALUES 
('Alice Johnson', '2020-01-15', 'Mechanic'),
('Bob Miller', '2021-06-20', 'Mechanic'),
('Charlie Davis', '2023-03-10', 'Apprentice'),
('Diana Evans', '2018-11-01', 'Receptionist');

-- Insert sample data into EmployeeRepair table
-- Assign employees to repairs and track hours worked
INSERT INTO EmployeeRepair (EmployeeID, RepairID, HoursWorked)
VALUES 
(1, 1, 5.0), -- Alice Johnson worked 5 hours on Repair 1
(2, 3, 8.0), -- Bob Miller worked 8 hours on Repair 3
(1, 4, 2.0), -- Alice Johnson worked 2 hours on Repair 4
(3, 4, 1.5), -- Charlie Davis worked 1.5 hours on Repair 4
(2, 2, 1.0); -- Bob Miller worked 1 hour on completed Repair 2
