USE VIS;

INSERT INTO Class(firstYear, finalYear, room) VALUES(2015, 2016, 1); -- 1
INSERT INTO Class(firstYear, finalYear, room) VALUES(2015, 2016, 2); -- 2
INSERT INTO Class(firstYear, finalYear, room) VALUES(2015, 2016, 3); -- 3

INSERT INTO Schedule DEFAULT VALUES; -- 1
INSERT INTO Schedule DEFAULT VALUES; -- 2
INSERT INTO Schedule DEFAULT VALUES; -- 3
INSERT INTO Schedule DEFAULT VALUES; -- 4

INSERT INTO Subject(name, [year], scheduleId) VALUES('VIS', 2015, 1); -- 1
INSERT INTO Subject(name, [year], scheduleId) VALUES('ZPG', 2015, 2); -- 2
INSERT INTO Subject(name, [year], scheduleId) VALUES('POS', 2015, 3); -- 3
INSERT INTO Subject(name, [year], scheduleId) VALUES('AT .NET', 2015, 4); -- 4

INSERT INTO Person([role], name, surname, birthDate, email, classId) VALUES(1, 'Jakub', 'Beránek', GETDATE(), 'berykubik@gmail.com', 1); -- 1
INSERT INTO Person([role], name, surname, birthDate, email, classId) VALUES(2, 'Pavel', 'Dohnálek', GETDATE(), 'pavel.dohnalek@vsb.cz', 1); -- 2
INSERT INTO Person([role], name, surname, birthDate, email, classId) VALUES(2, 'John', 'Wayne', GETDATE(), 'john.wayne@atlas.cz', 2); -- 3
INSERT INTO Person([role], name, surname, birthDate, email, classId) VALUES(2, 'Daniel', 'Støíbný', GETDATE(), 'daniel.stribny@vsb.cz', 3); -- 4

INSERT INTO SubjectPerson(personId, subjectId) VALUES(1, 1);
INSERT INTO SubjectPerson(personId, subjectId) VALUES(1, 2);
INSERT INTO SubjectPerson(personId, subjectId) VALUES(1, 3);
INSERT INTO SubjectPerson(personId, subjectId) VALUES(1, 4);
INSERT INTO SubjectPerson(personId, subjectId) VALUES(2, 1);
INSERT INTO SubjectPerson(personId, subjectId) VALUES(3, 2);
INSERT INTO SubjectPerson(personId, subjectId) VALUES(3, 4);
INSERT INTO SubjectPerson(personId, subjectId) VALUES(4, 3);

INSERT INTO Test(name, [date], subjectId) VALUES('Zkouška', GETDATE(), 1); -- 1
INSERT INTO Test(name, [date], subjectId) VALUES('Test', GETDATE(), 1); -- 2

INSERT INTO Grade(value, [weight], studentId, testId) VALUES(1, 5, 1, 1); -- 1
INSERT INTO Grade(value, [weight], studentId, testId) VALUES(1, 10, 1, 2); -- 2

BEGIN
	DECLARE @i int = 1
	DECLARE @j int = 1
	WHILE @i <= 5 BEGIN
		SET @j = 1

		WHILE @j <= 8 BEGIN
			INSERT INTO TeachingHour([day], [order]) VALUES(@i, @j);
			SET @j = @j + 1
		END

		SET @i = @i + 1
	END
END
GO

INSERT INTO ScheduleTeachingHour(scheduleId, teachingHourId) VALUES(1, 2);
INSERT INTO ScheduleTeachingHour(scheduleId, teachingHourId) VALUES(1, 5);
INSERT INTO ScheduleTeachingHour(scheduleId, teachingHourId) VALUES(4, 2);
INSERT INTO ScheduleTeachingHour(scheduleId, teachingHourId) VALUES(1, 10);
INSERT INTO ScheduleTeachingHour(scheduleId, teachingHourId) VALUES(2, 10);
INSERT INTO ScheduleTeachingHour(scheduleId, teachingHourId) VALUES(3, 10);
INSERT INTO ScheduleTeachingHour(scheduleId, teachingHourId) VALUES(4, 10);
INSERT INTO ScheduleTeachingHour(scheduleId, teachingHourId) VALUES(1, 20);
INSERT INTO ScheduleTeachingHour(scheduleId, teachingHourId) VALUES(2, 14);
INSERT INTO ScheduleTeachingHour(scheduleId, teachingHourId) VALUES(2, 18);
INSERT INTO ScheduleTeachingHour(scheduleId, teachingHourId) VALUES(2, 33);
INSERT INTO ScheduleTeachingHour(scheduleId, teachingHourId) VALUES(3, 34);
INSERT INTO ScheduleTeachingHour(scheduleId, teachingHourId) VALUES(3, 8);
INSERT INTO ScheduleTeachingHour(scheduleId, teachingHourId) VALUES(3, 21);
INSERT INTO ScheduleTeachingHour(scheduleId, teachingHourId) VALUES(4, 12);

USE VIS;
INSERT INTO Absence([date], [type], excused, studentId, teachingHourId, subjectId) VALUES(GETDATE(), 1, 1, 1, 5, 1);