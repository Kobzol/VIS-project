CREATE TABLE [dbo].[Class]
(
	[id] BIGINT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
    [firstYear] INT NOT NULL,
	[finalYear] INT NOT NULL,
	[room] INT NOT NULL,
	[teacherID] BIGINT NOT NULL,
    [created_at] DATETIME NOT NULL DEFAULT GETDATE()
)

CREATE TABLE [dbo].[Person]
(
	[id] BIGINT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
    [name] NVARCHAR(50) NOT NULL, 
    [surname] NVARCHAR(50) NOT NULL, 
    [birthDate] DATE NOT NULL, 
    [email] NVARCHAR(50) NOT NULL,
	[classId] BIGINT NULL,
    [created_at] DATETIME NOT NULL DEFAULT GETDATE()
)

CREATE TABLE [dbo].[Subject]
(
	[id] BIGINT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[name] NVARCHAR(50) NOT NULL,
	[year] INT NOT NULL,
	[scheduleId] BIGINT NOT NULL,
	[teacherId] BIGINT NOT NULL,
	[created_at] DATETIME NOT NULL DEFAULT GETDATE()
)

CREATE TABLE [dbo].[SubjectStudent]
(
	[id] BIGINT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[studentId] BIGINT NOT NULL,
	[subjectId] BIGINT NOT NULL,
	[created_at] DATETIME NOT NULL DEFAULT GETDATE()
)

CREATE TABLE [dbo].[Schedule]
(
	[id] BIGINT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[created_at] DATETIME NOT NULL DEFAULT GETDATE()
)

CREATE TABLE [dbo].[Supplement]
(
	[id] BIGINT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[canceled] TINYINT NOT NULL,
	[date] DATE NOT NULL,
	[scheduleId] BIGINT NOT NULL,
	[teachingHourId] BIGINT NOT NULL,
	[created_at] DATETIME NOT NULL DEFAULT GETDATE()
)

CREATE TABLE [dbo].[TeachingHour]
(
	[id] BIGINT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[day] INT NOT NULL,
	[order] INT NOT NULL,
	[created_at] DATETIME NOT NULL DEFAULT GETDATE()
)

CREATE TABLE [dbo].[ScheduleTeachingHour]
(
	[id] BIGINT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[scheduleId] BIGINT NOT NULL,
	[teachingHourId] BIGINT NOT NULL,
	[created_at] DATETIME NOT NULL DEFAULT GETDATE()
)

CREATE TABLE [dbo].[Absence]
(
	[id] BIGINT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[date] DATE NOT NULL,
	[type] INT NOT NULL,
	[excused] TINYINT NOT NULL,
	[studentId] BIGINT NOT NULL,
	[teachingHourId] BIGINT NOT NULL,
	[subjectId] BIGINT NOT NULL,
	[created_at] DATETIME NOT NULL DEFAULT GETDATE()
)

CREATE TABLE [dbo].[Test]
(
	[id] BIGINT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[name] NVARCHAR(50) NOT NULL,
	[date] DATE NOT NULL,
	[subjectId] BIGINT NOT NULL,
	[created_at] DATETIME NOT NULL DEFAULT GETDATE()
)

CREATE TABLE [dbo].[Grade]
(
	[id] BIGINT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[weight] FLOAT NOT NULL,
	[value] FLOAT NOT NULL,
	[studentId] BIGINT NOT NULL,
	[testId] BIGINT NOT NULL,
	[created_at] DATETIME NOT NULL DEFAULT GETDATE()
)

ALTER TABLE dbo.Class ADD CONSTRAINT fkClassTeacher FOREIGN KEY (teacherId) REFERENCES Person(id);
ALTER TABLE dbo.Person ADD CONSTRAINT fkPersonClass FOREIGN KEY (classId) REFERENCES Class(id);
ALTER TABLE dbo.Subject ADD CONSTRAINT fkSubjectSchedule FOREIGN KEY (scheduleId) REFERENCES Schedule(id);
ALTER TABLE dbo.Subject ADD CONSTRAINT fkSubjectTeacher FOREIGN KEY (teacherId) REFERENCES Person(id);
ALTER TABLE dbo.SubjectStudent ADD CONSTRAINT fkSubjectStudentStudent FOREIGN KEY (studentId) REFERENCES Person(id);
ALTER TABLE dbo.SubjectStudent ADD CONSTRAINT fkSubjectStudentSubject FOREIGN KEY (subjectId) REFERENCES Subject(id);
ALTER TABLE dbo.Supplement ADD CONSTRAINT fkSupplementSchedule FOREIGN KEY (scheduleId) REFERENCES Schedule(id);
ALTER TABLE dbo.Supplement ADD CONSTRAINT fkSupplementTeachingHour FOREIGN KEY (teachingHourId) REFERENCES TeachingHour(id);
ALTER TABLE dbo.ScheduleTeachingHour ADD CONSTRAINT fkScheduleTeachingHourSchedule FOREIGN KEY (scheduleId) REFERENCES Schedule(id);
ALTER TABLE dbo.ScheduleTeachingHour ADD CONSTRAINT fkScheduleTeachingHourTeachingHour FOREIGN KEY (teachingHourId) REFERENCES TeachingHour(id);
ALTER TABLE dbo.Absence ADD CONSTRAINT fkAbsenceStudent FOREIGN KEY (studentId) REFERENCES Person(id);
ALTER TABLE dbo.Absence ADD CONSTRAINT fkAbsenceTeachingHour FOREIGN KEY (teachingHourId) REFERENCES TeachingHour(id);
ALTER TABLE dbo.Absence ADD CONSTRAINT fkAbsenceSubject FOREIGN KEY (subjectId) REFERENCES Subject(id);
ALTER TABLE dbo.Test ADD CONSTRAINT fkTestSubject FOREIGN KEY (subjectId) REFERENCES Subject(id);
ALTER TABLE dbo.Grade ADD CONSTRAINT fkGradeStudent FOREIGN KEY (studentId) REFERENCES Person(id);
ALTER TABLE dbo.Grade ADD CONSTRAINT fkGradeTest FOREIGN KEY (testId) REFERENCES Test(id);