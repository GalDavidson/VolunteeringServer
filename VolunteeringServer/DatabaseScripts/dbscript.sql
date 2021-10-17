

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
    AssociationsID INT IDENTITY(1,1),
    OccupationalAreaID INT IDENTITY(1,1),
	CONSTRAINT PK_OccupationalAreasOfAssociation PRIMARY KEY (AssociationsID,OccupationalAreaID)
);
ALTER TABLE
    OccupationalAreasOfAssociation ADD PRIMARY KEY occupationalareasofassociation_associationsid_primary(AssociationsID);
ALTER TABLE
    OccupationalAreasOfAssociation ADD PRIMARY KEY occupationalareasofassociation_occupationalareasid_primary(OccupationalAreasID);



CREATE TABLE Branchs(
    BranchID INT  NOT NULL ,
    BranchLocation NVARCHAR NOT NULL
);
ALTER TABLE
    Branchs ADD PRIMARY KEY branchs_branchid_primary(BranchID);
CREATE TABLE BranchesOfAssociation(
    AssociationID INT  NOT NULL ,
    BranchID INT NOT NULL
);
ALTER TABLE
    BranchesOfAssociation ADD PRIMARY KEY branchesofassociation_associationid_primary(AssociationID);
ALTER TABLE
    BranchesOfAssociation ADD PRIMARY KEY branchesofassociation_branchid_primary(BranchID);
CREATE TABLE Events(
    EventID INT  NOT NULL,
    Location NVARCHAR NOT NULL,
    Caption NVARCHAR NOT NULL,
    AssociationID INT NOT NULL
);
ALTER TABLE
    Events ADD PRIMARY KEY events_eventid_primary(EventID);
CREATE TABLE PicturesOfEvents(
    PicID INT  NOT NULL ,
    URL INT NOT NULL,
    EventID INT NOT NULL
);
ALTER TABLE
    PicturesOfEvents ADD PRIMARY KEY picturesofevents_picid_primary(PicID);
CREATE TABLE VolunteersInEvents(
    EventID INT  NOT NULL,
    VolumteerID INT NOT NULL,
    RatingNum INT NOT NULL,
    WrittenRating VARCHAR(255) NULL,
    ActionDate DATETIME default GETDATE()
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
