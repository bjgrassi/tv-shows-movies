CREATE TABLE tblUserMovieArchive (
	UserMovieArchiveID int NOT NULL PRIMARY KEY IDENTITY(1,1),
	IsWatchLater bit NOT NULL,
	IsWatched bit NOT NULL,
	MovieFK int NOT NULL,
	UserEmailFK int NOT NULL
)

INSERT INTO tblUserMovieArchive (IsWatchLater, IsWatched, MovieFK, UserEmailFK) VALUES
(1, 0, 1, 'lilyaldrin@email.com'),
(0, 1, 2, 'lilyaldrin@email.com')

CREATE TABLE tblUserSerieArchive (
	UserSerieArchiveID int NOT NULL PRIMARY KEY IDENTITY(1,1),
	IsWatchLater bit NOT NULL,
	IsInProgress bit NOT NULL, 
	IsFinished bit NOT NULL, 
	SerieFK int NOT NULL,
	UserEmailFK int NOT NULL
)

INSERT INTO tblUserSerieArchive (IsWatchLater, IsInProgress, IsFinished, SerieFK, UserEmailFK) VALUES
(1, 0, 0, 1, 'lilyaldrin@email.com'),
(0, 1, 0, 2, 'lilyaldrin@email.com')

CREATE TABLE tblUserSeasonArchive (
	UserSeasonArchiveID int NOT NULL PRIMARY KEY IDENTITY(1,1),
	SeasonNum int NOT NULL, 
	IsWatched bit NOT NULL,
	SerieFK int NOT NULL,
	UserSerieArchiveID int NOT NULL FOREIGN KEY REFERENCES tblUserSerieArchive(UserSerieArchiveID)
)

INSERT INTO tblUserSeasonArchive (SeasonNum, IsWatched, SerieFK, UserSerieArchiveID) VALUES
(1, 0, 0, 1, 1),
(2, 0, 0, 1, 1),
(3, 0, 0, 1, 1),
(4, 0, 0, 1, 1),
(5, 0, 0, 1, 1),

(1, 1, 1, 2, 2),
(2, 1, 1, 2, 2),
(3, 0, 1, 2, 2)