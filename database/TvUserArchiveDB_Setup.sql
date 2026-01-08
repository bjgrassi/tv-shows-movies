DROP TABLE IF EXISTS tblUserSeasonArchive, tblUserSerieArchive, tblUserMovieArchive;

CREATE TABLE tblUserMovieArchive (
	UserMovieArchiveID int NOT NULL PRIMARY KEY IDENTITY(1,1),
	IsWatchLater bit NOT NULL,
	IsWatched bit NOT NULL,
	MovieFK int NOT NULL,
	UserAccountFK int NOT NULL
)

INSERT INTO tblUserMovieArchive (IsWatchLater, IsWatched, MovieFK, UserAccountFK) VALUES
(1, 0, 1, 4),
(0, 1, 2, 4)

CREATE TABLE tblUserSerieArchive (
	UserSerieArchiveID int NOT NULL PRIMARY KEY IDENTITY(1,1),
	IsWatchLater bit NOT NULL,
	IsInProgress bit NOT NULL, 
	IsFinished bit NOT NULL, 
	SerieFK int NOT NULL,
	UserAccountFK int NOT NULL
)

INSERT INTO tblUserSerieArchive (IsWatchLater, IsInProgress, IsFinished, SerieFK, UserAccountFK) VALUES
(1, 0, 0, 1, 4),
(0, 1, 0, 2, 4)

CREATE TABLE tblUserSeasonArchive (
	UserSeasonArchiveID int NOT NULL PRIMARY KEY IDENTITY(1,1),
	SeasonNum int NOT NULL, 
	IsWatched bit NOT NULL,
	SerieFK int NOT NULL,
	UserSerieArchiveFK int NOT NULL FOREIGN KEY REFERENCES tblUserSerieArchive(UserSerieArchiveID),
	--UserSerieArchiveFK int not null FOREIGN KEY (UserSerieArchiveFK) REFERENCES tblUserSerieArchive(UserSerieArchiveID) ON DELETE CASCADE
)

INSERT INTO tblUserSeasonArchive (SeasonNum, IsWatched, SerieFK, UserSerieArchiveFK) VALUES
(1, 0, 1, 1),
(2, 0, 1, 1),
(3, 0, 1, 1),
(4, 0, 1, 1),
(5, 0, 1, 1),

(1, 1, 2, 2),
(2, 1, 2, 2),
(3, 0, 2, 2)

SELECT * FROM tblUserMovieArchive
SELECT * FROM tblUserSerieArchive
SELECT * FROM tblUserSeasonArchive