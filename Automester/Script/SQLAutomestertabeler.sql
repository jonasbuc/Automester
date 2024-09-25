-- Create the database
CREATE DATABASE Automester;
GO

-- Use the newly created database
USE Automester;
GO

-- Create Customer table
CREATE TABLE Customer (
    CustomerID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100),
    Address NVARCHAR(255),
    Email NVARCHAR(100),
    Phone NVARCHAR(20)
);
GO

-- Create Car table
CREATE TABLE Car (
    CarID INT PRIMARY KEY IDENTITY(1,1),
    CustomerID INT,
    RegistrationNumber NVARCHAR(20),
    Brand NVARCHAR(50),
    Model NVARCHAR(50),
    Year INT,
    Mileage INT,
    CONSTRAINT FK_Car_Customer FOREIGN KEY (CustomerID) REFERENCES Customer(CustomerID)
);
GO

-- Create Repair table
CREATE TABLE Repair (
    RepairID INT PRIMARY KEY IDENTITY(1,1),
    CarID INT,
    StartDate DATE,
    EndDate DATE,
    Description NVARCHAR(MAX),
    Price DECIMAL(10, 2),
    Status NVARCHAR(50),
    CONSTRAINT FK_Repair_Car FOREIGN KEY (CarID) REFERENCES Car(CarID)
);
GO

-- Create table for cars in the workshop
CREATE TABLE Workshop (
    WorkshopID INT PRIMARY KEY IDENTITY(1,1),
    CarID INT,
    ArrivalDate DATE,
    CONSTRAINT FK_Workshop_Car FOREIGN KEY (CarID) REFERENCES Car(CarID)
);
GO

-- Create table for cars ready for pickup
CREATE TABLE ReadyForPickup (
    PickupID INT PRIMARY KEY IDENTITY(1,1),
    CarID INT,
    ReadyDate DATE,
    CONSTRAINT FK_ReadyForPickup_Car FOREIGN KEY (CarID) REFERENCES Car(CarID)
);
GO

-- Create Employee table
CREATE TABLE Employee (
    EmployeeID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100),
    HireDate DATE,
    Position NVARCHAR(50)
);
GO

-- Create table to associate repairs with employees (for tracking hours worked)
CREATE TABLE EmployeeRepair (
    EmployeeID INT,
    RepairID INT,
    HoursWorked DECIMAL(5, 2),
    PRIMARY KEY (EmployeeID, RepairID),
    CONSTRAINT FK_EmployeeRepair_Employee FOREIGN KEY (EmployeeID) REFERENCES Employee(EmployeeID),
    CONSTRAINT FK_EmployeeRepair_Repair FOREIGN KEY (RepairID) REFERENCES Repair(RepairID)
);
GO

-- Create Stored Procedure to add a car to the workshop when a new repair is created
CREATE PROCEDURE add_to_workshop
    @CarID INT,
    @StartDate DATE
AS
BEGIN
    -- Insert car into the Workshop table
    INSERT INTO Workshop (CarID, ArrivalDate)
    VALUES (@CarID, @StartDate);
END;
GO

-- Create Stored Procedure to move a car from the workshop to "Ready for Pickup"
CREATE PROCEDURE move_to_ready_for_pickup
    @CarID INT,
    @EndDate DATE
AS
BEGIN
    -- Remove car from the Workshop table
    DELETE FROM Workshop WHERE CarID = @CarID;

    -- Insert car into the ReadyForPickup table
    INSERT INTO ReadyForPickup (CarID, ReadyDate)
    VALUES (@CarID, @EndDate);
END;
GO

-- Create Trigger to call the procedure to add a car to the workshop when a new repair is created
CREATE TRIGGER trigger_add_to_workshop
ON Repair
AFTER INSERT
AS
BEGIN
    DECLARE @CarID INT, @StartDate DATE;
    
    -- Get the CarID and StartDate from the inserted row
    SELECT @CarID = INSERTED.CarID, @StartDate = INSERTED.StartDate
    FROM INSERTED;

    -- Call the stored procedure to add the car to the workshop
    EXEC add_to_workshop @CarID, @StartDate;
END;
GO

-- Create Trigger to move the car from the workshop to "Ready for Pickup" when the repair is marked as completed
CREATE TRIGGER trigger_move_to_ready_for_pickup
ON Repair
AFTER UPDATE
AS
BEGIN
    DECLARE @CarID INT, @EndDate DATE, @NewStatus NVARCHAR(50), @OldStatus NVARCHAR(50);
    
    -- Get the CarID, EndDate, and Status from the updated row
    SELECT @CarID = INSERTED.CarID, @EndDate = INSERTED.EndDate, 
           @NewStatus = INSERTED.Status, @OldStatus = DELETED.Status
    FROM INSERTED
    JOIN DELETED ON INSERTED.RepairID = DELETED.RepairID;
    
    -- If the new status is 'completed' and the old status was not 'completed', move the car
    IF (@NewStatus = 'completed' AND @OldStatus <> 'completed')
    BEGIN
        -- Call the stored procedure to move the car to ReadyForPickup
        EXEC move_to_ready_for_pickup @CarID, @EndDate;
    END;
END;
GO
