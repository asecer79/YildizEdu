-- Drop database if it exists
IF EXISTS (SELECT * FROM sys.databases WHERE name = 'UniEduDb')
DROP DATABASE UniEduDb;
GO

-- Create database
CREATE DATABASE UniEduDb;
GO

USE UniEduDb;
GO

-- 1. Faculties Table
CREATE TABLE Faculties (
    Id INT PRIMARY KEY IDENTITY(1,1),
    FacultyName VARCHAR(100) NOT NULL,
    DeanName VARCHAR(100),
    EstablishedDate DATE
);

-- 2. Departments Table
CREATE TABLE Departments (
    Id INT PRIMARY KEY IDENTITY(1,1),
    DepartmentName VARCHAR(100) NOT NULL,
    FacultyId INT,
    HeadOfDepartment VARCHAR(100),
    FOREIGN KEY (FacultyId) REFERENCES Faculties(Id)
);

-- 3. Students Table
CREATE TABLE Students (
    Id INT PRIMARY KEY IDENTITY(1,1),
    StudentNumber VARCHAR(10) UNIQUE NOT NULL,
    FirstName VARCHAR(50) NOT NULL,
    LastName VARCHAR(50) NOT NULL,
    BirthDate DATE,
    Gender CHAR(1) CHECK (Gender IN ('M', 'F')),
    Email VARCHAR(100) UNIQUE,
    Phone VARCHAR(15),
    Address TEXT,
    DepartmentId INT,
    RegistrationDate DATE DEFAULT GETDATE(),
    Status VARCHAR(20) CHECK (Status IN ('Active', 'Inactive', 'Graduated', 'Dropped')) DEFAULT 'Active',
    FOREIGN KEY (DepartmentId) REFERENCES Departments(Id)
);

-- 4. Instructors Table
CREATE TABLE Instructors (
    Id INT PRIMARY KEY IDENTITY(1,1),
    FirstName VARCHAR(50) NOT NULL,
    LastName VARCHAR(50) NOT NULL,
    Email VARCHAR(100) UNIQUE,
    Phone VARCHAR(15),
    DepartmentId INT,
    Title VARCHAR(50) CHECK (Title IN ('Prof.', 'Assoc. Prof.', 'Asst. Prof.', 'Instructor')),
    HireDate DATE,
    FOREIGN KEY (DepartmentId) REFERENCES Departments(Id)
);

-- 5. Semesters Table
CREATE TABLE Semesters (
    Id INT PRIMARY KEY IDENTITY(1,1),
    SemesterName VARCHAR(50) NOT NULL,
    StartDate DATE NOT NULL,
    EndDate DATE NOT NULL,
    CHECK (EndDate > StartDate)
);

-- 6. Courses Table
CREATE TABLE Courses (
    Id INT PRIMARY KEY IDENTITY(1,1),
    CourseCode VARCHAR(10) UNIQUE NOT NULL,
    CourseName VARCHAR(100) NOT NULL,
    Credits INT NOT NULL CHECK (Credits > 0 AND Credits <= 10),
    DepartmentId INT,
    InstructorId INT,
    SemesterId INT,
    MaxCapacity INT DEFAULT 50,
    FOREIGN KEY (DepartmentId) REFERENCES Departments(Id),
    FOREIGN KEY (InstructorId) REFERENCES Instructors(Id),
    FOREIGN KEY (SemesterId) REFERENCES Semesters(Id)
);

-- 7. Enrollments Table
CREATE TABLE Enrollments (
    Id INT PRIMARY KEY IDENTITY(1,1),
    StudentId INT NOT NULL,
    CourseId INT NOT NULL,
    SemesterId INT NOT NULL,
    EnrollmentDate DATE DEFAULT GETDATE(),
    Grade DECIMAL(4,2) CHECK (Grade >= 0 AND Grade <= 100),
    AttendancePercentage DECIMAL(5,2) CHECK (AttendancePercentage >= 0 AND AttendancePercentage <= 100),
    FOREIGN KEY (StudentId) REFERENCES Students(Id),
    FOREIGN KEY (CourseId) REFERENCES Courses(Id),
    FOREIGN KEY (SemesterId) REFERENCES Semesters(Id)
);

-- 8. Payments Table
CREATE TABLE Payments (
    Id INT PRIMARY KEY IDENTITY(1,1),
    StudentId INT NOT NULL,
    Amount DECIMAL(10,2) NOT NULL CHECK (Amount > 0),
    PaymentDate DATE DEFAULT GETDATE(),
    SemesterId INT,
    PaymentType VARCHAR(20) CHECK (PaymentType IN ('Tuition', 'Registration Fee', 'Additional Fee')),
    Status VARCHAR(20) DEFAULT 'Paid' CHECK (Status IN ('Paid', 'Pending', 'Cancelled')),
    FOREIGN KEY (StudentId) REFERENCES Students(Id),
    FOREIGN KEY (SemesterId) REFERENCES Semesters(Id)
);

-- 9. LibraryBooks Table
CREATE TABLE LibraryBooks (
    Id INT PRIMARY KEY IDENTITY(1,1),
    ISBN VARCHAR(13) UNIQUE NOT NULL,
    Title VARCHAR(200) NOT NULL,
    Author VARCHAR(100),
    PublicationYear INT,
    DepartmentId INT,
    AvailableCopies INT DEFAULT 1 CHECK (AvailableCopies >= 0),
    FOREIGN KEY (DepartmentId) REFERENCES Departments(Id)
);

-- 10. LibraryLoans Table
CREATE TABLE LibraryLoans (
    Id INT PRIMARY KEY IDENTITY(1,1),
    StudentId INT NOT NULL,
    BookId INT NOT NULL,
    LoanDate DATE DEFAULT GETDATE(),
    DueDate DATE NOT NULL,
    ReturnDate DATE,
    FineAmount DECIMAL(6,2) DEFAULT 0.00 CHECK (FineAmount >= 0),
    FOREIGN KEY (StudentId) REFERENCES Students(Id),
    FOREIGN KEY (BookId) REFERENCES LibraryBooks(Id),
    CHECK (DueDate > LoanDate)
);

-- 11. Exams Table
CREATE TABLE Exams (
    Id INT PRIMARY KEY IDENTITY(1,1),
    CourseId INT NOT NULL,
    ExamDate DATE NOT NULL,
    ExamType VARCHAR(20) CHECK (ExamType IN ('Midterm', 'Final', 'Makeup', 'Quiz')),
    MaxScore DECIMAL(5,2) DEFAULT 100.00 CHECK (MaxScore > 0),
    Classroom VARCHAR(20),
    FOREIGN KEY (CourseId) REFERENCES Courses(Id)
);

--12  Events Table
CREATE TABLE Events (
    Id INT PRIMARY KEY IDENTITY(1,1),
    EventName VARCHAR(100) NOT NULL,
    EventDate DATE NOT NULL,
    Location VARCHAR(100),
    OrganizerDepartmentId INT,
    Description TEXT,
    FOREIGN KEY (OrganizerDepartmentId) REFERENCES Departments(Id)
);


--13 DisciplinaryRecords Tablosunu Oluştur
CREATE TABLE DisciplinaryRecords (
    Id INT PRIMARY KEY IDENTITY(1,1),
    StudentId INT NOT NULL,
    IncidentDate DATE NOT NULL,
    Description TEXT NOT NULL,
    Penalty VARCHAR(100),
    DecisionDate DATE,
    FOREIGN KEY (StudentId) REFERENCES Students(Id) ON DELETE CASCADE
);



-- 📌 Örnek Veriler (10 Kayıt) 📌
-- Faculties
INSERT INTO Faculties (FacultyName, DeanName, EstablishedDate) VALUES
('Engineering', 'Dr. John Smith', '1990-01-01'),
('Science', 'Dr. Emily Brown', '1985-03-15'),
('Economics', 'Dr. Michael Lee', '1995-06-10'),
('Education', 'Dr. Sarah Davis', '2000-09-01'),
('Law', 'Dr. David Wilson', '1992-11-20'),
('Medicine', 'Dr. Laura Taylor', '1980-04-15'),
('Architecture', 'Dr. James White', '1998-02-10'),
('Fine Arts', 'Dr. Anna Green', '2005-07-01'),
('Communication', 'Dr. Robert Johnson', '2002-03-25'),
('Sports Sciences', 'Dr. Thomas Clark', '2010-01-15');

-- Departments
INSERT INTO Departments (DepartmentName, FacultyId, HeadOfDepartment) VALUES
('Computer Engineering', 1, 'Assoc. Prof. Mark Taylor'),
('Mathematics', 2, 'Prof. Jane Miller'),
('Economics', 3, 'Prof. Linda Adams'),
('Education', 4, 'Assoc. Prof. Susan Carter'),
('Law', 5, 'Prof. Peter Evans'),
('Medicine', 6, 'Prof. Emma Wilson'),
('Architecture', 7, 'Instructor Tom Harris'),
('Painting', 8, 'Prof. Lisa Moore'),
('Journalism', 9, 'Assoc. Prof. Chris Young'),
('Physical Education', 10, 'Asst. Prof. Paul King');

-- Students
INSERT INTO Students (StudentNumber, FirstName, LastName, BirthDate, Gender, Email, Phone, DepartmentId, Address) VALUES
('YLDZ001', 'James', 'Wilson', '2002-05-15', 'M', 'james.wilson@example.com', '555-123-4567', 1, '123 Main St'),
('YLDZ002', 'Sophie', 'Brown', '2001-09-20', 'F', 'sophie.brown@example.com', '555-987-6543', 2, '45 Oak St'),
('YLDZ003', 'Ethan', 'Davis', '2003-01-10', 'M', 'ethan.davis@example.com', '555-111-2222', 3, '78 Pine Rd'),
('YLDZ004', 'Olivia', 'Smith', '2002-07-25', 'F', 'olivia.smith@example.com', '555-333-4444', 4, '12 Elm St'),
('YLDZ005', 'Liam', 'Taylor', '2000-12-05', 'M', 'liam.taylor@example.com', '555-555-6666', 5, '90 Cedar Ave'),
('YLDZ006', 'Emma', 'Johnson', '2003-03-18', 'F', 'emma.johnson@example.com', '555-777-8888', 6, '34 Maple St'),
('YLDZ007', 'Noah', 'Clark', '2001-11-30', 'M', 'noah.clark@example.com', '555-999-0000', 7, '56 Birch Rd'),
('YLDZ008', 'Ava', 'Miller', '2002-06-12', 'F', 'ava.miller@example.com', '555-121-3434', 8, '89 Spruce St'),
('YLDZ009', 'William', 'Lee', '2000-08-22', 'M', 'william.lee@example.com', '555-454-6767', 9, '23 Willow Dr'),
('YLDZ010', 'Isabella', 'Adams', '2003-04-09', 'F', 'isabella.adams@example.com', '555-787-9090', 10, '67 Laurel St');

-- Instructors
INSERT INTO Instructors (FirstName, LastName, Email, Phone, DepartmentId, Title, HireDate) VALUES
('Mark', 'Taylor', 'mark.taylor@example.com', '555-111-2222', 1, 'Prof.', '2010-06-01'),
('Jane', 'Miller', 'jane.miller@example.com', '555-333-4444', 2, 'Assoc. Prof.', '2015-09-01'),
('Michael', 'Lee', 'michael.lee@example.com', '555-555-6666', 3, 'Prof.', '2008-03-15'),
('Susan', 'Carter', 'susan.carter@example.com', '555-777-8888', 4, 'Asst. Prof.', '2017-01-20'),
('Peter', 'Evans', 'peter.evans@example.com', '555-999-0000', 5, 'Prof.', '2012-11-10'),
('Emma', 'Wilson', 'emma.wilson@example.com', '555-121-3434', 6, 'Assoc. Prof.', '2016-04-05'),
('Tom', 'Harris', 'tom.harris@example.com', '555-454-6767', 7, 'Instructor', '2019-08-15'),
('Lisa', 'Moore', 'lisa.moore@example.com', '555-787-9090', 8, 'Prof.', '2011-02-25'),
('Chris', 'Young', 'chris.young@example.com', '555-232-5656', 9, 'Asst. Prof.', '2018-07-01'),
('Paul', 'King', 'paul.king@example.com', '555-898-1212', 10, 'Assoc. Prof.', '2014-09-10');

-- Semesters
INSERT INTO Semesters (SemesterName, StartDate, EndDate) VALUES
('Fall 2023', '2023-09-15', '2024-01-15'),
('Spring 2024', '2024-02-15', '2024-06-15'),
('Fall 2024', '2024-09-15', '2025-01-15'),
('Spring 2025', '2025-02-15', '2025-06-15'),
('Summer 2025', '2025-07-01', '2025-08-31'),
('Spring 2023', '2023-02-15', '2023-06-15'),
('Fall 2022', '2022-09-15', '2023-01-15'),
('Spring 2022', '2022-02-15', '2022-06-15'),
('Fall 2021', '2021-09-15', '2022-01-15'),
('Spring 2021', '2021-02-15', '2021-06-15');

-- Courses
INSERT INTO Courses (CourseCode, CourseName, Credits, DepartmentId, InstructorId, SemesterId) VALUES
('CS101', 'Intro to Programming', 4, 1, 1, 1),
('MATH201', 'Linear Algebra', 3, 2, 2, 2),
('ECON101', 'Microeconomics', 3, 3, 3, 3),
('EDU201', 'Educational Psychology', 4, 4, 4, 4),
('LAW101', 'Introduction to Law', 5, 5, 5, 5),
('MED101', 'Anatomy', 6, 6, 6, 6),
('ARCH101', 'Architectural Design', 4, 7, 7, 7),
('ART201', 'Painting Techniques', 3, 8, 8, 8),
('JRN101', 'News Writing', 4, 9, 9, 9),
('PE101', 'Sports Physiology', 3, 10, 10, 10);


-- Enrollments (Öğrencilerin ders kayıtları)
INSERT INTO Enrollments (StudentId, CourseId, SemesterId, Grade, AttendancePercentage) VALUES
(1, 1, 1, 85.50, 90.00),
(2, 2, 2, 92.00, 95.00),
(3, 3, 3, 78.00, 85.00),
(4, 4, 4, 88.50, 92.00),
(5, 5, 5, 91.00, 93.00),
(6, 6, 6, 84.00, 88.00),
(7, 7, 7, 79.50, 87.00),
(8, 8, 8, 90.00, 94.00),
(9, 9, 9, 86.50, 91.00),
(10, 10, 10, 83.00, 89.00);

-- Payments (Öğrenci ödemeleri)
INSERT INTO Payments (StudentId, Amount, SemesterId, PaymentType, Status) VALUES
(1, 5000.00, 1, 'Tuition', 'Paid'),
(2, 4500.00, 2, 'Tuition', 'Paid'),
(3, 4800.00, 3, 'Tuition', 'Pending'),
(4, 5200.00, 4, 'Tuition', 'Paid'),
(5, 6000.00, 5, 'Tuition', 'Paid'),
(6, 7500.00, 6, 'Tuition', 'Paid'),
(7, 5100.00, 7, 'Tuition', 'Pending'),
(8, 4700.00, 8, 'Tuition', 'Paid'),
(9, 4900.00, 9, 'Tuition', 'Paid'),
(10, 5300.00, 10, 'Tuition', 'Paid');

-- LibraryBooks (Kütüphane kitapları)
INSERT INTO LibraryBooks (ISBN, Title, Author, PublicationYear, DepartmentId, AvailableCopies) VALUES
('9781234567890', 'Fundamentals of Algorithms', 'John Doe', 2019, 1, 5),
('9780987654321', 'Mathematical Analysis', 'Jane Smith', 2020, 2, 3),
('9781112223334', 'Economics 101', 'David Brown', 2018, 3, 4),
('9784445556667', 'Innovations in Education', 'Emma Wilson', 2021, 4, 2),
('9787778889990', 'History of Law', 'Michael Lee', 2017, 5, 6),
('9780001112223', 'Medical Biology', 'Sarah Davis', 2019, 6, 3),
('9783334445556', 'History of Architecture', 'Robert Johnson', 2020, 7, 4),
('9786667778889', 'Art and Aesthetics', 'Laura Taylor', 2016, 8, 5),
('9789990001112', 'Media and Communication', 'James White', 2021, 9, 2),
('9782223334445', 'Sports and Health', 'Emily Green', 2018, 10, 3);

-- LibraryLoans (Öğrencilerin ödünç aldığı kitaplar)
INSERT INTO LibraryLoans (StudentId, BookId, LoanDate, DueDate, ReturnDate, FineAmount) VALUES
(1, 1, '2024-10-01', '2024-11-01', NULL, 0.00),
(2, 2, '2024-10-02', '2024-11-02', '2024-10-25', 0.00),
(3, 3, '2024-10-03', '2024-11-03', NULL, 0.00),
(4, 4, '2024-10-04', '2024-11-04', NULL, 0.00),
(5, 5, '2024-10-05', '2024-11-05', '2024-10-20', 0.00),
(6, 6, '2024-10-06', '2024-11-06', NULL, 0.00),
(7, 7, '2024-10-07', '2024-11-07', NULL, 0.00),
(8, 8, '2024-10-08', '2024-11-08', '2024-10-30', 0.00),
(9, 9, '2024-10-09', '2024-11-09', NULL, 0.00),
(10, 10, '2024-10-10', '2024-11-10', NULL, 0.00);

-- Exams (Sınavlar)
INSERT INTO Exams (CourseId, ExamDate, ExamType, MaxScore, Classroom) VALUES
(1, '2024-11-15', 'Midterm', 100.00, 'B-101'),
(2, '2024-11-16', 'Midterm', 100.00, 'C-202'),
(3, '2024-11-17', 'Midterm', 100.00, 'D-303'),
(4, '2024-11-18', 'Midterm', 100.00, 'E-404'),
(5, '2024-11-19', 'Midterm', 100.00, 'F-505'),
(6, '2024-11-20', 'Midterm', 100.00, 'G-606'),
(7, '2024-11-21', 'Midterm', 100.00, 'H-707'),
(8, '2024-11-22', 'Midterm', 100.00, 'I-808'),
(9, '2024-11-23', 'Midterm', 100.00, 'J-909'),
(10, '2024-11-24', 'Midterm', 100.00, 'K-1010');

-- Events (Etkinlikler)
INSERT INTO Events (EventName, EventDate, Location, OrganizerDepartmentId, Description) VALUES
('Career Day', '2024-12-01', 'Conference Hall', 1, 'Networking with industry professionals'),
('Math Symposium', '2024-12-02', 'Lecture Room A', 2, 'Presentations on advanced mathematics'),
('Economics Workshop', '2024-12-03', 'Seminar Room B', 3, 'Economic trends discussion'),
('Teaching Seminar', '2024-12-04', 'Auditorium C', 4, 'Innovative teaching methods'),
('Law Conference', '2024-12-05', 'Hall D', 5, 'Legal system updates'),
('Medical Research Day', '2024-12-06', 'Lab E', 6, 'Latest medical research presentations'),
('Architecture Exhibition', '2024-12-07', 'Gallery F', 7, 'Student project showcase'),
('Art Festival', '2024-12-08', 'Open Area G', 8, 'Art and performance event'),
('Media Forum', '2024-12-09', 'Room H', 9, 'Media industry insights'),
('Sports Day', '2024-12-10', 'Sports Complex', 10, 'Competitions and fitness activities');

-- DisciplinaryRecords (Disiplin cezaları)
INSERT INTO DisciplinaryRecords (StudentId, IncidentDate, Description, Penalty, DecisionDate) VALUES
(1, '2024-10-10', 'Cheating during exam', '1-week suspension', '2024-10-15'),
(2, '2024-10-11', 'Late library book return', 'Fine', '2024-10-16'),
(3, '2024-10-12', 'Disrupting class', 'Warning', '2024-10-17'),
(4, '2024-10-13', 'Unauthorized event attendance', 'Community service', '2024-10-18'),
(5, '2024-10-14', 'Vandalism', '2-week suspension', '2024-10-19');



-- LibraryLoans İçin Örnek Veriler
INSERT INTO LibraryLoans (StudentId, BookId, LoanDate, DueDate, ReturnDate, FineAmount) VALUES
(1, 1, '2024-10-01', '2024-11-01', NULL, 0.00),
(2, 2, '2024-10-02', '2024-11-02', '2024-10-25', 0.00),
(3, 3, '2024-10-03', '2024-11-03', NULL, 0.00),
(4, 4, '2024-10-04', '2024-11-04', NULL, 0.00),
(5, 5, '2024-10-05', '2024-11-05', '2024-10-20', 0.00),
(6, 6, '2024-10-06', '2024-11-06', NULL, 0.00),
(7, 7, '2024-10-07', '2024-11-07', NULL, 0.00),
(8, 8, '2024-10-08', '2024-11-08', '2024-10-30', 0.00),
(9, 9, '2024-10-09', '2024-11-09', NULL, 0.00),
(10, 10, '2024-10-10', '2024-11-10', NULL, 0.00);