Use master
CREATE DATABASE VolunteeringDB
Go

Use VolunteeringDB
Go  


CREATE TABLE Gender(
    GenderID INT IDENTITY(1,1) PRIMARY KEY,
    GenderType NVARCHAR(255) NOT NULL
);



CREATE TABLE Volunteers(
    VolunteerID INT IDENTITY(1,1) PRIMARY KEY,
    fName NVARCHAR(255) NOT NULL,
    lName NVARCHAR(255) NOT NULL,
	Email NVARCHAR(255) NOT NULL UNIQUE,
    UserName NVARCHAR(255) NOT NULL UNIQUE,
    Pass NVARCHAR(255) NOT NULL,
	GenderID INT FOREIGN KEY REFERENCES Gender (GenderID),
    BirthDate DATE NOT NULL,
    AvgRating int NOT NULL default '1',
    ActionDate DATETIME default GETDATE()
);



CREATE TABLE Associations(
    AssociationID INT IDENTITY(1,1) PRIMARY KEY,
    Email NVARCHAR(255) NOT NULL UNIQUE,
    UserName NVARCHAR(255) NOT NULL UNIQUE,
    InformationAbout NVARCHAR(255) NOT NULL,
    PhoneNum NVARCHAR(255) NOT NULL,
    Pass NVARCHAR(255) NOT NULL,
    ActionDate DATETIME default GETDATE(),
);



CREATE TABLE AppAdmin(
    AdminID INT IDENTITY(1,1) PRIMARY KEY,
    UserName NVARCHAR(255) NOT NULL UNIQUE,
    Email NVARCHAR(255) NOT NULL UNIQUE,
    Pass NVARCHAR(255) NOT NULL,
    AdminName NVARCHAR(255) NOT NULL
);



CREATE TABLE OccupationalAreas(
    OccupationalAreaID INT IDENTITY(1,1) PRIMARY KEY,
    OccupationName NVARCHAR(255) NOT NULL
);



CREATE TABLE OccupationalAreasOfAssociation(
    AssociationID INT FOREIGN KEY REFERENCES Associations (AssociationID),
    OccupationalAreaID INT FOREIGN KEY REFERENCES OccupationalAreas (OccupationalAreaID),
	CONSTRAINT PK_OcAreasOfAssociation PRIMARY KEY (AssociationID,OccupationalAreaID)
);



CREATE TABLE Branches(
    BranchID INT IDENTITY(1,1) PRIMARY KEY,
    BranchLocation NVARCHAR(255) NOT NULL
);



CREATE TABLE BranchesOfAssociation(
    AssociationID INT FOREIGN KEY REFERENCES Associations (AssociationID),
    BranchID INT FOREIGN KEY REFERENCES Branches (BranchID),
	CONSTRAINT PK_BranchesOfAss PRIMARY KEY (AssociationID, BranchID)
);


CREATE TABLE Areas(
    AreaID INT IDENTITY(1,1) PRIMARY KEY,
    AreaName NVARCHAR(255) NOT NULL
);

CREATE TABLE DailyEvents(
    EventID INT IDENTITY(1,1) PRIMARY KEY,
    EventLocation NVARCHAR(255) NOT NULL,
    AssociationID INT FOREIGN KEY REFERENCES Associations (AssociationID),
    ActionDate DATETIME default GETDATE(),
    Caption NVARCHAR(255) NOT NULL,
    EventName NVARCHAR(255) NOT NULL,
    StartTime DATETIME NOT NULL,
    EndTime DATETIME NOT NULL,
	AreaID INT FOREIGN KEY REFERENCES Areas (AreaID)
);


CREATE TABLE OccupationalAreasOfEvents(
    EventID INT FOREIGN KEY REFERENCES DailyEvents (EventID),
    OccupationalAreaID INT FOREIGN KEY REFERENCES OccupationalAreas (OccupationalAreaID),
	CONSTRAINT PK_OccuAreasEvents PRIMARY KEY (EventID, OccupationalAreaID)
);


CREATE TABLE VolunteersInEvents(
    EventID INT FOREIGN KEY REFERENCES DailyEvents (EventID),
    VolunteerID INT FOREIGN KEY REFERENCES Volunteers (VolunteerID),
    RatingNum INT NOT NULL,
    WrittenRating NVARCHAR(255),
    ActionDate DATETIME default GETDATE()

	CONSTRAINT PK_VolInEvents PRIMARY KEY (EventID, VolunteerID)

);

CREATE TABLE Comments(
    CommentID INT IDENTITY(1,1) PRIMARY KEY,
    CommentText NVARCHAR(255) NOT NULL,
    EventID INT NOT NULL,
    VolunteerID INT NOT NULL,
	CONSTRAINT FK_EventsComments FOREIGN KEY (EventID,VolunteerID) REFERENCES VolunteersInEvents(EventID,VolunteerID)
);


INSERT INTO Branches([BranchLocation])
VALUES ('אום אל-פחם');

INSERT INTO Branches([BranchLocation])
VALUES ('אופקים');

INSERT INTO Branches([BranchLocation])
VALUES ('אור יהודה');

INSERT INTO Branches([BranchLocation])
VALUES ('אור עקיבא');

INSERT INTO Branches([BranchLocation])
VALUES ('אילת');

INSERT INTO Branches([BranchLocation])
VALUES ('אלעד');

INSERT INTO Branches([BranchLocation])
VALUES ('אריאל');

INSERT INTO Branches([BranchLocation])
VALUES ('אשדוד');

INSERT INTO Branches([BranchLocation])
VALUES ('אשקלון');

INSERT INTO Branches([BranchLocation])
VALUES ('באקה אל-גרבייה');

INSERT INTO Branches([BranchLocation])
VALUES ('באר יעקב');

INSERT INTO Branches([BranchLocation])
VALUES ('באר שבע');

INSERT INTO Branches([BranchLocation])
VALUES ('בית שאן');

INSERT INTO Branches([BranchLocation])
VALUES ('בית שמש');

INSERT INTO Branches([BranchLocation])
VALUES ('ביתר עילית');

INSERT INTO Branches([BranchLocation])
VALUES ('בני ברק');

INSERT INTO Branches([BranchLocation])
VALUES ('בת ים');

INSERT INTO Branches([BranchLocation])
VALUES ('גבעת שמואל');

INSERT INTO Branches([BranchLocation])
VALUES ('גבעתיים');

INSERT INTO Branches([BranchLocation])
VALUES ('דימונה');

INSERT INTO Branches([BranchLocation])
VALUES ('הוד השרון');

INSERT INTO Branches([BranchLocation])
VALUES ('הרצליה');

INSERT INTO Branches([BranchLocation])
VALUES ('חדרה');

INSERT INTO Branches([BranchLocation])
VALUES ('חולון');

INSERT INTO Branches([BranchLocation])
VALUES ('חיפה');

INSERT INTO Branches([BranchLocation])
VALUES ('טבריה');

INSERT INTO Branches([BranchLocation])
VALUES ('טייבה');

INSERT INTO Branches([BranchLocation])
VALUES ('טירה');

INSERT INTO Branches([BranchLocation])
VALUES ('טירת כרמל');

INSERT INTO Branches([BranchLocation])
VALUES ('טמרה');

INSERT INTO Branches([BranchLocation])
VALUES ('יבנה');

INSERT INTO Branches([BranchLocation])
VALUES ('יהוד-מונוסון');

INSERT INTO Branches([BranchLocation])
VALUES ('יקנעם עילית');

INSERT INTO Branches([BranchLocation])
VALUES ('ירושלים');

INSERT INTO Branches([BranchLocation])
VALUES ('כפר יונה');

INSERT INTO Branches([BranchLocation])
VALUES ('כפר סבא');

INSERT INTO Branches([BranchLocation])
VALUES ('כפר קאסם');

INSERT INTO Branches([BranchLocation])
VALUES ('כרמיאל');

INSERT INTO Branches([BranchLocation])
VALUES ('לוד');

INSERT INTO Branches([BranchLocation])
VALUES ('מגדל העמק');

INSERT INTO Branches([BranchLocation])
VALUES ('מודיעין עילית');

INSERT INTO Branches([BranchLocation])
VALUES ('מודיעין- מכבים- רעות');

INSERT INTO Branches([BranchLocation])
VALUES ('מעאר');

INSERT INTO Branches([BranchLocation])
VALUES ('מעלה אדומים');

INSERT INTO Branches([BranchLocation])
VALUES ('מעלות-תרשיחא	');

INSERT INTO Branches([BranchLocation])
VALUES ('נהריה');

INSERT INTO Branches([BranchLocation])
VALUES ('נוף הגליל');

INSERT INTO Branches([BranchLocation])
VALUES ('נס ציונה');

INSERT INTO Branches([BranchLocation])
VALUES ('נצרת');

INSERT INTO Branches([BranchLocation])
VALUES ('נשר');

INSERT INTO Branches([BranchLocation])
VALUES ('נתיבות');

INSERT INTO Branches([BranchLocation])
VALUES ('נתניה');

INSERT INTO Branches([BranchLocation])
VALUES ('סחנין');

INSERT INTO Branches([BranchLocation])
VALUES ('עכו');

INSERT INTO Branches([BranchLocation])
VALUES ('עפולה');

INSERT INTO Branches([BranchLocation])
VALUES ('עראבה');

INSERT INTO Branches([BranchLocation])
VALUES ('ערד');

INSERT INTO Branches([BranchLocation])
VALUES ('פתח תקווה');

INSERT INTO Branches([BranchLocation])
VALUES ('צפת');

INSERT INTO Branches([BranchLocation])
VALUES ('קלנסווה');

INSERT INTO Branches([BranchLocation])
VALUES ('קריית אונו');

INSERT INTO Branches([BranchLocation])
VALUES ('קריית אתא');

INSERT INTO Branches([BranchLocation])
VALUES ('קריית ביאליק');

INSERT INTO Branches([BranchLocation])
VALUES ('קריית גת');

INSERT INTO Branches([BranchLocation])
VALUES ('קריית ים');

INSERT INTO Branches([BranchLocation])
VALUES ('קריית מוצקין');

INSERT INTO Branches([BranchLocation])
VALUES ('קריית מלאכי');

INSERT INTO Branches([BranchLocation])
VALUES ('קריית שמונה');

INSERT INTO Branches([BranchLocation])
VALUES ('ראש העין');

INSERT INTO Branches([BranchLocation])
VALUES ('ראשון לציון');

INSERT INTO Branches([BranchLocation])
VALUES ('רהט');

INSERT INTO Branches([BranchLocation])
VALUES ('רחובות');

INSERT INTO Branches([BranchLocation])
VALUES ('רמלה');

INSERT INTO Branches([BranchLocation])
VALUES ('רמת גן');

INSERT INTO Branches([BranchLocation])
VALUES ('רמת השרון');

INSERT INTO Branches([BranchLocation])
VALUES ('רעננה');

INSERT INTO Branches([BranchLocation])
VALUES ('שדרות');

INSERT INTO Branches([BranchLocation])
VALUES ('שפרעם');

INSERT INTO Branches([BranchLocation])
VALUES ('תל אביב יפו');

Go


INSERT INTO OccupationalAreas([OccupationName])
VALUES ('התנדבות עם בעלי חיים');

INSERT INTO OccupationalAreas([OccupationName])
VALUES ('עזרה לחקלאים');

INSERT INTO OccupationalAreas([OccupationName])
VALUES ('בריאות ורפואה');

INSERT INTO OccupationalAreas([OccupationName])
VALUES ('חלוקת סלי מזון');

INSERT INTO OccupationalAreas([OccupationName])
VALUES ('התנדבות עם בעלי צרכים מיוחדים');

INSERT INTO OccupationalAreas([OccupationName])
VALUES ('התנדבות במקלטים');

INSERT INTO OccupationalAreas([OccupationName])
VALUES ('עזרה בלימודים');

INSERT INTO OccupationalAreas([OccupationName])
VALUES ('איכות הסביבה');

INSERT INTO OccupationalAreas([OccupationName])
VALUES ('התנדבות בשעת חירום');

INSERT INTO OccupationalAreas([OccupationName])
VALUES ('ביטחון והצלה');

INSERT INTO OccupationalAreas([OccupationName])
VALUES ('חונכות והדרכה');

INSERT INTO OccupationalAreas([OccupationName])
VALUES ('אזרחים ותיקים');

INSERT INTO OccupationalAreas([OccupationName])
VALUES ('טכנולוגיה');

INSERT INTO OccupationalAreas([OccupationName])
VALUES ('תרבות וספורט');


INSERT INTO Gender([GenderType])
VALUES ('נקבה')

INSERT INTO Gender([GenderType])
VALUES ('זכר')

Go

INSERT INTO Areas(AreaName)
VALUES ('גולן')

INSERT INTO Areas(AreaName)
VALUES ('גליל')

INSERT INTO Areas(AreaName)
VALUES ('העמקים')

INSERT INTO Areas(AreaName)
VALUES ('כרמל')

INSERT INTO Areas(AreaName)
VALUES ('יהודה ושומרון')

INSERT INTO Areas(AreaName)
VALUES ('שרון')

INSERT INTO Areas(AreaName)
VALUES ('שפלה')

INSERT INTO Areas(AreaName)
VALUES ('ירושלים')

INSERT INTO Areas(AreaName)
VALUES ('הנגב')

INSERT INTO Areas(AreaName)
VALUES ('הערבה')

INSERT INTO Areas(AreaName)
VALUES ('גוש דן ותל אביב')

Go

INSERT INTO Volunteers([fName],[lName],[Email],[UserName],[Pass],[GenderID],[BirthDate])
VALUES ('Noy','Ganor','noiganor12@gmail.com','Noyga','hoenyhoeny',1,'2004-08-24')

INSERT INTO AppAdmin([UserName],[Email],[Pass],[AdminName])
VALUES ('gal','gal@gmail.com','12345','GalDavidson')

INSERT INTO Associations([Email],[UserName],[InformationAbout],[PhoneNum],[Pass])
VALUES ('a@gmail.com','aa123','sfbwrnb','0384756291','all12345')

INSERT INTO Associations([Email],[UserName],[InformationAbout],[PhoneNum],[Pass])
VALUES ('b@gmail.com','bbb123','afrbrwtb','0769758761','band12345')

INSERT INTO Associations([Email],[UserName],[InformationAbout],[PhoneNum],[Pass])
VALUES ('c@gmail.com','cc123','wrnwtrynb','0584936722','coke1234')

INSERT INTO Volunteers([fName],[lName],[Email],[UserName],[Pass],[GenderID],[BirthDate])
VALUES ('Shiri','Yaffe','6363shiri@gmail.com','Shirile','joya2328',1,'2004-09-03')

INSERT INTO Volunteers([fName],[lName],[Email],[UserName],[Pass],[GenderID],[BirthDate])
VALUES ('Shira','Yosov','shirayosov6@gmail.com','ShiraShira','pizzaNitza',1,'2004-04-04')

INSERT INTO DailyEvents(EventLocation, AssociationID, Caption, EventName, StartTime, EndTime, AreaID)
VALUES ('חיפה', 1 ,'בואו יהיה כיף', 'טיפול בחזירי בר', '2022-5-20 17:00', '2022-5-20 19:00', 4)

INSERT INTO DailyEvents(EventLocation, AssociationID, Caption, EventName, StartTime, EndTime, AreaID)
VALUES ('כפר סבא', 2 ,'!!! יהיו אוכל ושתייה בחינם', 'התנדבות בבית אבות', '2022-7-4 11:00', '2022-7-4 13:30', 6)

INSERT INTO DailyEvents(EventLocation, AssociationID, Caption, EventName, StartTime, EndTime, AreaID)
VALUES ('אור עקיבא', 1 ,'מצווה חשובה במיוחד', 'חלוקת סלי מזון וביקור קשישים', '2022-8-15 9:00','2022-8-15 12:00', 4)

INSERT INTO OccupationalAreasOfAssociation(AssociationID, OccupationalAreaID)
VALUES (1,1)

INSERT INTO OccupationalAreasOfAssociation(AssociationID, OccupationalAreaID)
VALUES (1,4)

INSERT INTO OccupationalAreasOfAssociation(AssociationID, OccupationalAreaID)
VALUES (1,12)

INSERT INTO OccupationalAreasOfAssociation(AssociationID, OccupationalAreaID)
VALUES (2,12)

INSERT INTO OccupationalAreasOfAssociation(AssociationID, OccupationalAreaID)
VALUES (3,10)

INSERT INTO OccupationalAreasOfAssociation(AssociationID, OccupationalAreaID)
VALUES (3,14)

INSERT INTO OccupationalAreasOfEvents(EventID, OccupationalAreaID)
VALUES (1,1)

INSERT INTO OccupationalAreasOfEvents(EventID, OccupationalAreaID)
VALUES (2,12)

INSERT INTO OccupationalAreasOfEvents(EventID, OccupationalAreaID)
VALUES (3,4)

INSERT INTO OccupationalAreasOfEvents(EventID, OccupationalAreaID)
VALUES (3,12)

INSERT INTO VolunteersInEvents(EventID, VolunteerID, RatingNum, WrittenRating)
VALUES (1,1,0,'')

INSERT INTO VolunteersInEvents(EventID, VolunteerID, RatingNum, WrittenRating)
VALUES (1,2,0,'')

INSERT INTO VolunteersInEvents(EventID, VolunteerID, RatingNum, WrittenRating)
VALUES (1,3,0,'')

INSERT INTO VolunteersInEvents(EventID, VolunteerID, RatingNum, WrittenRating)
VALUES (2,1,0,'')

INSERT INTO VolunteersInEvents(EventID, VolunteerID, RatingNum, WrittenRating)
VALUES (2,2,0,'')

INSERT INTO VolunteersInEvents(EventID, VolunteerID, RatingNum, WrittenRating)
VALUES (3,1,0,'')

INSERT INTO VolunteersInEvents(EventID, VolunteerID, RatingNum, WrittenRating)
VALUES (3,3,0,'')

CREATE TABLE Ranks(
    RankID INT IDENTITY(1,1) PRIMARY KEY,
    RankName NVARCHAR(255) NOT NULL
);

INSERT INTO Ranks(RankName)
VALUES ('מתחיל.ה')

INSERT INTO Ranks(RankName)
VALUES ('מתחבר.ת לקונספט')

INSERT INTO Ranks(RankName)
VALUES ('מתקדמ.ת')

INSERT INTO Ranks(RankName)
VALUES ('מתמיד.ה')

INSERT INTO Ranks(RankName)
VALUES ('מאסטר')

--scaffold-dbcontext "Server=localhost\sqlexpress;Database=VolunteeringDB;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models –force