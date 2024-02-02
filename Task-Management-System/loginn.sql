CREATE DATABASE Task_Management

USE Task_Management

CREATE TABLE  loginn (
    User_Name VARCHAR(255),
    Email VARCHAR(255),
    Password VARCHAR(255)
);

ALTER TABLE loginn
ADD User_Type VARCHAR(50);

ALTER TABLE loginn
ADD User_ID  VARCHAR(50);
ALTER TABLE loginn
ADD CONSTRAINT PK_User_ID PRIMARY KEY (User_ID );



DELETE FROM loginn
WHERE User_Name = 'example_username';

select * from loginn
 INSERT INTO loginn (User_Name, Email, Password) VALUES ('muhanoorr', 'muhamme7d7@gmail.com', '12324389');
 INSERT INTO loginn (User_Name,User_ID, Email, Password) VALUES ('miki',10, 'mikiman@gmail.com', '87654381');
 UPDATE loginn
SET User_Type = 'Staff'
WHERE User_Name = 'miki';
UPDATE loginn
SET User_ID = '00'
WHERE User_Name = 'muhanoorr';

 UPDATE loginn
SET User_ID = '01'
WHERE User_Name = 'tole';


USE Task_Management;

-- Creating 'Tasks' table
CREATE TABLE Tasks (
    TaskID INT PRIMARY KEY IDENTITY(1,1),
    TaskTitle VARCHAR(255),
    Description VARCHAR(MAX),
    DueDate DATETIME,
    StaffEmail VARCHAR(255),
    StaffName VARCHAR(255),
    StaffID VARCHAR(50),  -- Match the data type with 'User_ID' in 'loginn'
    CONSTRAINT FK_StaffTasks FOREIGN KEY (StaffID) REFERENCES loginn(User_ID)
);
select * from Tasks
ALTER TABLE Tasks
DROP CONSTRAINT FK_StaffTasks;

DELETE FROM Tasks
WHERE StaffID = 2;

CREATE TABLE SubmissionTasks (
    SubmissionTaskID INT PRIMARY KEY IDENTITY(1,1),
    StaffID INT,
    Task NVARCHAR(MAX),
    Files VARBINARY(MAX)
);
select * from SubmissionTasks
ALTER TABLE SubmissionTasks
DROP COLUMN Task;
ALTER TABLE SubmissionTasks
DROP CONSTRAINT PK_SubmissionTaskID;

ALTER TABLE SubmissionTasks
DROP CONSTRAINT SubmissionTasks;

SET IDENTITY_INSERT SubmissionTasks ON;