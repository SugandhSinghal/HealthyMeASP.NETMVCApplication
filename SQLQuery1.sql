--Creating table profiles
CREATE TABLE [dbo].[Profiles] (
[Id] int IDENTITY(1,1) NOT NULL,
[Name] nvarchar (Max) NOT NULL,
[Age] nvarchar (Max) NOT NULL,
[Gender] nvarchar (Max) NOT NULL,
[Address] nvarchar (Max) NOT NULL,
[UserId] nvarchar (Max) NOT NULL,
PRIMARY KEY (Id),
FOREIGN KEY (UserId) REFERENCES [dbo].[AspNetUsers](Id)
);

--Creating table Appointments
CREATE TABLE [dbo].[Appointments] (
[AppointmentId] int IDENTITY(1,1) NOT NULL,
[ClientName] NVARCHAR (256) NOT NULL,
[TrainerName] NVARCHAR (256) NOT NULL,
[Date] DATE NOT NULL,
[AppointmentAddress] NVARCHAR (Max) NOT NULL,
PRIMARY KEY (AppointmentId),
FOREIGN KEY (ClientName) REFERENCES [dbo].[AspNetUsers](UserName),
FOREIGN KEY (TrainerName) REFERENCES [dbo].[AspNetUsers](UserName)
);

-- Creating table 'Exercises'
CREATE TABLE [dbo].[Exercises] (
[ExerciseId] int IDENTITY(1,1) NOT NULL,
[ExerciseName] nvarchar(max) NOT NULL,
[ExerciseDescription] nvarchar(max) NOT NULL,
[Tips] nvarchar(max) NOT NULL,
     PRIMARY KEY (ExerciseId)  
); 

-- Creating table 'DietPlan'
CREATE TABLE [dbo].[DietPlan] (
[DietId] int IDENTITY(1,1) NOT NULL,
[DietName] nvarchar(max) NOT NULL,
[Accuracy] nvarchar(max) NOT NULL,
[Level] nvarchar(max) NOT NULL,
[DietDescription] nvarchar(max) NOT NULL,
[UserName] nvarchar(256) NOT NULL,
     PRIMARY KEY (DietId),
     FOREIGN KEY (UserName) REFERENCES [dbo].[AspNetUsers](UserName)  
);

--Creating table Ratings
CREATE TABLE [dbo].[Ratings] (
[RatingId] int IDENTITY(1,1) NOT NULL,
[Rating] int NOT NULL,
[AppointmentId] int NOT NULL,
[Comments] NVARCHAR (Max) NOT NULL,
PRIMARY KEY (RatingId),
FOREIGN KEY (AppointmentId) REFERENCES [dbo].[Appointments](AppointmentId)
);

GO