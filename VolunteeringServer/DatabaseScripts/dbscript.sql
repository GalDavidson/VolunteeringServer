CREATE TABLE Volunteers(
    VolunteerID INT IDENTITY(1,1) PRIMARY KEY,
    fName NVARCHAR NOT NULL,
    lName NVARCHAR NOT NULL,
	Email NVARCHAR NOT NULL UNIQUE,
    UserName NVARCHAR NOT NULL UNIQUE,
    Pass NVARCHAR NOT NULL,
    ProfilePic NVARCHAR NOT NULL,
	GenderID INT NOT NULL,
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


CREATE TABLE Posts(
    PostID INT IDENTITY(1,1) PRIMARY KEY,
    ActionDate DATETIME default GETDATE(),
    Caption NVARCHAR NOT NULL,
    AssociationID INT NOT NULL,
	EventID INT
);



CREATE TABLE Comments(
    CommentID INT IDENTITY(1,1) PRIMARY KEY,
    CommentText NVARCHAR NOT NULL,
    EventID INT NOT NULL,
    VolunteerID INT NOT NULL
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
    AssociationID INT NOT NULL,
    ActionDate DATETIME default GETDATE(),
    EventName NVARCHAR NOT NULL,
    EventDate DATETIME NOT NULL
);


CREATE TABLE PicturesOfEvents(
    PicID INT IDENTITY(1,1) PRIMARY KEY,
    PicURL INT NOT NULL,
    EventID INT FOREIGN KEY REFERENCES DailyEvents (EventID)
);



CREATE TABLE VolunteersInEvents(
    EventID INT FOREIGN KEY REFERENCES DailyEvents (EventID),
    VolunteerID INT FOREIGN KEY REFERENCES Volunteers (VolunteerID),
    RatingNum INT NOT NULL,
    WrittenRating VARCHAR(255) NULL,
    ActionDate DATETIME default GETDATE()

	CONSTRAINT PK_VolInEvents PRIMARY KEY (EventID, VolunteerID)

);

ALTER TABLE
    VolunteersInEvents ADD PRIMARY KEY volunteersinevents_eventid_primary(EventID);
ALTER TABLE
    VolunteersInEvents ADD PRIMARY KEY volunteersinevents_volumteerid_primary(VolumteerID);
ALTER TABLE
    Events ADD CONSTRAINT events_associationid_foreign FOREIGN KEY(AssociationID) REFERENCES Associations(AssociationID);
ALTER TABLE
    Posts ADD CONSTRAINT posts_associationid_foreign FOREIGN KEY(AssociationID) REFERENCES Associations(AssociationID);
ALTER TABLE
    PicturesOfEvents ADD CONSTRAINT picturesofevents_eventid_foreign FOREIGN KEY(EventID) REFERENCES Events(EventID);
ALTER TABLE
    Comments ADD CONSTRAINT comments_eventid_foreign FOREIGN KEY(EventID) REFERENCES Events(EventID);
ALTER TABLE
    Comments ADD CONSTRAINT comments_volunteerid_foreign FOREIGN KEY(VolunteerID) REFERENCES VolunteersInEvents(VolumteerID);
