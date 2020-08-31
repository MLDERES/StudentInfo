Use PROJECTF2030
GO
-- XK75ekq$

IF OBJECT_ID('dbo.Enrollment', 'U') IS NOT NULL
  DROP TABLE dbo.Enrollment
GO

IF OBJECT_ID('dbo.Class', 'U') IS NOT NULL
  DROP TABLE dbo.class
GO

IF OBJECT_ID('dbo.Student', 'U') IS NOT NULL
  DROP TABLE dbo.Student
GO

IF OBJECT_ID('dbo.Instructor', 'U') IS NOT NULL
  DROP TABLE dbo.Instructor
GO


CREATE TABLE [dbo].[Instructor]
(
	[InstructorId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](30) NULL,
	CONSTRAINT [PK_Instructor_table] PRIMARY KEY (InstructorId)
)
GO

CREATE TABLE dbo.Student
(
	StudentId int IDENTITY(1,1) NOT NULL, 
	Name nvarchar(30) NULL, 
	Major char(5) NULL, 
	GradeLevel char(2) NULL,
	Age smallint NULL,
    CONSTRAINT PK_sample_table PRIMARY KEY (StudentId)
)
GO

CREATE TABLE dbo.Class
(
	ClassId int IDENTITY(1,1) NOT NULL, 
	Name nvarchar(30) NULL, 
	ClassTime nvarchar(10) NULL,
	Room nvarchar(10) NULL,
	InstructorId int NULL,
	CONSTRAINT PK_class_table PRIMARY KEY (ClassId),
	FOREIGN KEY (InstructorId) References Instructor(InstructorId)
)
GO


CREATE TABLE [dbo].[Enrollment]
(
	[EnrollmentId] [int] IDENTITY(1,1) NOT NULL,
	[ClassId] [int] NOT NULL,
	[StudentId] [int] NOT NULL,
	 CONSTRAINT [PK_Enrollment_table] PRIMARY KEY (EnrollmentId),
	 FOREIGN KEY (ClassId) References Class(ClassId),
	 FOREIGN KEY (StudentId) References Student(StudentId),
)
GO

-- Now add a bit of data
-- Instructors
INSERT INTO [dbo].[Instructor]
    ([Name])
VALUES
    ('R. Wilson'),
    ('B. Adams'),
    ('E. E. Fred'),
    ('M. Law'),
    ('A. Jones'),
    ('P. Hadley'),
    ('K. Odum'),
    ('T. Smith'),
    ('T. Long'),
    ('A. Smith'),
    ('R. Wilson'),
    ('B. Adams')
GO

-- Students
INSERT INTO [dbo].[Student]
    ([Name]
    ,[Major]
    ,[GradeLevel]
    ,[Age])
VALUES
    ('M. Jones', 'ISYS ', 'GR', 21),
    ('J. Parks', 'ACCT ', 'SO', 19),
    ('R. Baker', 'MGMT ', 'GR', 50),
    ('PJ Glass', 'MKTT ', 'SN', 18),
    ('W. Russell', 'MIS  ', 'JR', 20),
    ('O. Rye', 'ACCT ', 'FR', 18)
GO

-- Classes

INSERT INTO [dbo].[Class]
    ([Name]
    ,[ClassTime]
    ,[Room]
    ,[InstructorId])
VALUES
    ('WCOB1023', 'M-F9', 'SC110', 5),
    ('ISYS4213', 'MWF3', 'SC213', 1),
    ('WCOB1003', 'MWF8', 'SC213', 2),
    ('ISYS2263', 'MWF3', 'EA304', 6),
    ('ISYS450V', 'MWF12', 'EB210', 8)
go

-- Enrollments
INSERT INTO [dbo].[Enrollment]
    ([ClassId]
    ,[StudentId])
VALUES
(5,5),
(4,5),
(3,3),
(5,4),
(4,4),
(2,6),
(2,1),
(2,1),
(1,4),
(4,2)
GO


