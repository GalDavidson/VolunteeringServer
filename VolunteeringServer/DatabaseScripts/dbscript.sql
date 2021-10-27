Use master
CREATE DATABASE VolunteeringDB
Go

Use VolunteeringDB
Go


CREATE TABLE Gender(
    GenderID INT IDENTITY(1,1) PRIMARY KEY,
    GenderType NVARCHAR NOT NULL
);



CREATE TABLE Volunteers(
    VolunteerID INT IDENTITY(1,1) PRIMARY KEY,
    fName NVARCHAR NOT NULL,
    lName NVARCHAR NOT NULL,
	Email NVARCHAR NOT NULL UNIQUE,
    UserName NVARCHAR NOT NULL UNIQUE,
    Pass NVARCHAR NOT NULL,
    ProfilePic NVARCHAR NOT NULL,
	GenderID INT FOREIGN KEY REFERENCES Gender (GenderID),
    BirthDate DATE NOT NULL,
    ActionDate DATETIME default GETDATE()
);



CREATE TABLE Associations(
    AssociationID INT IDENTITY(1,1) PRIMARY KEY,
    Email NVARCHAR NOT NULL UNIQUE,
    UserName NVARCHAR NOT NULL UNIQUE,
    InformationAbout NVARCHAR NOT NULL,
    PhoneNum NVARCHAR NOT NULL,
    Pass NVARCHAR NOT NULL,
    ActionDate DATETIME default GETDATE(),
	ProfilePic NVARCHAR NOT NULL
);



CREATE TABLE AppAdmin(
    AdminID INT IDENTITY(1,1) PRIMARY KEY,
    UserName NVARCHAR NOT NULL UNIQUE,
    Email NVARCHAR NOT NULL UNIQUE,
    Pass NVARCHAR NOT NULL,
    AdminName NVARCHAR NOT NULL
);



CREATE TABLE OccupationalAreas(
    OccupationalAreaID INT IDENTITY(1,1) PRIMARY KEY,
    OccupationName NVARCHAR NOT NULL
);



CREATE TABLE OccupationalAreasOfAssociation(
    AssociationID INT FOREIGN KEY REFERENCES Associations (AssociationID),
    OccupationalAreaID INT FOREIGN KEY REFERENCES OccupationalAreas (OccupationalAreaID),
	CONSTRAINT PK_OcAreasOfAssociation PRIMARY KEY (AssociationID,OccupationalAreaID)
);



CREATE TABLE Branches(
    BranchID INT IDENTITY(1,1) PRIMARY KEY,
    BranchLocation NVARCHAR NOT NULL
);



CREATE TABLE BranchesOfAssociation(
    AssociationID INT FOREIGN KEY REFERENCES Associations (AssociationID),
    BranchID INT FOREIGN KEY REFERENCES Branches (BranchID),
	CONSTRAINT PK_BranchesOfAss PRIMARY KEY (AssociationID, BranchID)
);


CREATE TABLE DailyEvents(
    EventID INT IDENTITY(1,1) PRIMARY KEY,
    EventLocation NVARCHAR NOT NULL,
    Caption NVARCHAR NOT NULL,
    AssociationID INT FOREIGN KEY REFERENCES Associations (AssociationID),
    ActionDate DATETIME default GETDATE(),
    EventName NVARCHAR NOT NULL,
    EventDate DATETIME NOT NULL
);


CREATE TABLE Posts(
    PostID INT IDENTITY(1,1) PRIMARY KEY,
    ActionDate DATETIME default GETDATE(),
    Caption NVARCHAR NOT NULL,
    AssociationID INT FOREIGN KEY REFERENCES Associations (AssociationID),
	EventID INT FOREIGN KEY REFERENCES DailyEvents (EventID)
);



CREATE TABLE PicturesOfEvents(
    PicID INT IDENTITY(1,1) PRIMARY KEY,
    PicURL NVARCHAR NOT NULL,
    EventID INT FOREIGN KEY REFERENCES DailyEvents (EventID)
);



CREATE TABLE VolunteersInEvents(
    EventID INT FOREIGN KEY REFERENCES DailyEvents (EventID),
    VolunteerID INT FOREIGN KEY REFERENCES Volunteers (VolunteerID),
    RatingNum INT NOT NULL,
    WrittenRating NVARCHAR,
    ActionDate DATETIME default GETDATE()

	CONSTRAINT PK_VolInEvents PRIMARY KEY (EventID, VolunteerID)

);

CREATE TABLE Comments(
    CommentID INT IDENTITY(1,1) PRIMARY KEY,
    CommentText NVARCHAR NOT NULL,
    EventID INT NOT NULL,
    VolunteerID INT NOT NULL,
	CONSTRAINT FK_EventsComments FOREIGN KEY (EventID,VolunteerID) REFERENCES VolunteersInEvents(EventID,VolunteerID)
);

CREATE TABLE PicturesOfPosts(
    PicID INT IDENTITY(1,1) PRIMARY KEY,
    PicURL NVARCHAR NOT NULL,
    PostID INT FOREIGN KEY REFERENCES Posts (PostID)
);



CREATE TABLE OccupationalAreasOfPosts(
    PostID INT FOREIGN KEY REFERENCES Posts (PostID),
    OccupationalAreaID INT FOREIGN KEY REFERENCES DailyEvents (EventID),
	CONSTRAINT PK_OccuAreasPosts PRIMARY KEY (PostID, OccupationalAreaID)
);





INSERT INTO Branches([BranchLocation])
VALUES ('Afula');

INSERT INTO Branches([BranchLocation])
VALUES ('Arad');

INSERT INTO Branches([BranchLocation])
VALUES ('Ashdod');

INSERT INTO Branches([BranchLocation])
VALUES ('Ashkelon');

INSERT INTO Branches([BranchLocation])
VALUES ('Baqa al-Gharbiyye');

INSERT INTO Branches([BranchLocation])
VALUES ('Bat Yam');

INSERT INTO Branches([BranchLocation])
VALUES ('Beersheba');

INSERT INTO Branches([BranchLocation])
VALUES ('Beit Shean');

INSERT INTO Branches([BranchLocation])
VALUES ('Beit Shemesh');

INSERT INTO Branches([BranchLocation])
VALUES ('Bnei Brak');

INSERT INTO Branches([BranchLocation])
VALUES ('Dimona');

INSERT INTO Branches([BranchLocation])
VALUES ('Eilat');

INSERT INTO Branches([BranchLocation])
VALUES ('Elad');

INSERT INTO Branches([BranchLocation])
VALUES ('Givat Shmuel');

INSERT INTO Branches([BranchLocation])
VALUES ('Givatayim');

INSERT INTO Branches([BranchLocation])
VALUES ('Hadera');

INSERT INTO Branches([BranchLocation])
VALUES ('Haifa');

INSERT INTO Branches([BranchLocation])
VALUES ('Herzliya');

INSERT INTO Branches([BranchLocation])
VALUES ('Hod HaSharon');

INSERT INTO Branches([BranchLocation])
VALUES ('Holon');

INSERT INTO Branches([BranchLocation])
VALUES ('Jerusalem');

INSERT INTO Branches([BranchLocation])
VALUES ('Kafr Qasim');

INSERT INTO Branches([BranchLocation])
VALUES ('Karmiel');

INSERT INTO Branches([BranchLocation])
VALUES ('Kfar Saba');

INSERT INTO Branches([BranchLocation])
VALUES ('Kfar Yona');

INSERT INTO Branches([BranchLocation])
VALUES ('Kiryat Ata');

INSERT INTO Branches([BranchLocation])
VALUES ('Kiryat Bialik');

INSERT INTO Branches([BranchLocation])
VALUES ('Kiryat Gat');

INSERT INTO Branches([BranchLocation])
VALUES ('Kiryat Malakhi');

INSERT INTO Branches([BranchLocation])
VALUES ('Kiryat Motzkin');

INSERT INTO Branches([BranchLocation])
VALUES ('Kiryat Ono');

INSERT INTO Branches([BranchLocation])
VALUES ('Kiryat Shmona');

INSERT INTO Branches([BranchLocation])
VALUES ('Kiryat Yam');

INSERT INTO Branches([BranchLocation])
VALUES ('Lod');

INSERT INTO Branches([BranchLocation])
VALUES ('Maalot-Tarshiha');

INSERT INTO Branches([BranchLocation])
VALUES ('Migdal HaEmek');

INSERT INTO Branches([BranchLocation])
VALUES ('Modiin-Maccabim-Reut');

INSERT INTO Branches([BranchLocation])
VALUES ('Nahariya');

INSERT INTO Branches([BranchLocation])
VALUES ('Nazareth');

INSERT INTO Branches([BranchLocation])
VALUES ('Nesher');

INSERT INTO Branches([BranchLocation])
VALUES ('Ness Ziona');

INSERT INTO Branches([BranchLocation])
VALUES ('Netanya');

INSERT INTO Branches([BranchLocation])
VALUES ('Netivot');

INSERT INTO Branches([BranchLocation])
VALUES ('Nof HaGalil');

INSERT INTO Branches([BranchLocation])
VALUES ('Ofakim');

INSERT INTO Branches([BranchLocation])
VALUES ('Or Akiva');

INSERT INTO Branches([BranchLocation])
VALUES ('Or Yehuda');

INSERT INTO Branches([BranchLocation])
VALUES ('Petah Tikva');

INSERT INTO Branches([BranchLocation])
VALUES ('Qalansawe');

INSERT INTO Branches([BranchLocation])
VALUES ('Raanana');

INSERT INTO Branches([BranchLocation])
VALUES ('Rahat');

INSERT INTO Branches([BranchLocation])
VALUES ('Ramat Gan');

INSERT INTO Branches([BranchLocation])
VALUES ('Ramat HaSharon');

INSERT INTO Branches([BranchLocation])
VALUES ('Ramla');

INSERT INTO Branches([BranchLocation])
VALUES ('Rehovot');

INSERT INTO Branches([BranchLocation])
VALUES ('Rishon LeZion');

INSERT INTO Branches([BranchLocation])
VALUES ('Rosh HaAyin');

INSERT INTO Branches([BranchLocation])
VALUES ('Rehovot');

INSERT INTO Branches([BranchLocation])
VALUES ('Safed');

INSERT INTO Branches([BranchLocation])
VALUES ('Sakhnin');

INSERT INTO Branches([BranchLocation])
VALUES ('Sderot');

INSERT INTO Branches([BranchLocation])
VALUES ('Shefa-Amr');

INSERT INTO Branches([BranchLocation])
VALUES ('Tamra');

INSERT INTO Branches([BranchLocation])
VALUES ('Tayibe');

Go
