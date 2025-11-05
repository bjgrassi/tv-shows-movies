DROP TABLE IF EXISTS tblGenreMovie, tblGenreSerie, tblMovie, tblSeason, tblSerie, tblGenre;

CREATE TABLE tblGenre (
    GenreID int PRIMARY KEY IDENTITY(1,1),
    Name varchar(50) NOT NULL UNIQUE -- 'action', 'comedy', etc.
);

INSERT INTO tblGenre (Name) VALUES 
('Action'), ('Comedy'), ('Drama'), ('Sci-Fi'), ('Horror'), ('Documentary'), ('Romance'), ('Thriller'), ('Indie Film'), ('Animation'), 
('Fantasy'), ('Mystery'), ('Adventure'), ('Crime'), ('Musical');

CREATE TABLE tblMovie (
    MovieID int PRIMARY KEY IDENTITY(1,1),
    Title varchar(255) NOT NULL,
    Synopsis text,
    ImageUrl varchar(500),
    ReleaseYear int CHECK (ReleaseYear BETWEEN 1700 AND 9999),
    TypeName varchar(5) DEFAULT 'Movie',
    CreatedAt datetime DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt datetime DEFAULT CURRENT_TIMESTAMP , -- Update on service
    RunningTime int -- in minutes
);

--GO;
--CREATE TRIGGER MovieUpdatedAt ON [tblMovie]
--AFTER UPDATE
--AS BEGIN
--    SET NOCOUNT ON;
--    UPDATE tblMovie
--    SET [UpdatedAt] = GETDATE()
--    WHERE MovieID IN (SELECT MovieID FROM Inserted)
--END;

--GO
--CREATE TRIGGER MovieUpdateAt ON tblMovie
--AFTER UPDATE
--AS BEGIN
--    IF NOT UPDATE(UpdatedAt) -- Evitar recursão na coluna
--    BEGIN
--        UPDATE t
--            SET t.UpdatedAt = CURRENT_TIMESTAMP
--            FROM tblMovie AS t
--            INNER JOIN inserted AS i 
--            ON t.MovieID = i.MovieID;
--    END
--END

INSERT INTO tblMovie (Title, Synopsis, ImageUrl, ReleaseYear, RunningTime) VALUES
('Pulp Fiction', 'The lives of two mob hitmen, a boxer, a gangster and his wife, and a pair of diner bandits intertwine in four tales of violence and redemption.',
    'https://en.wikipedia.org/wiki/Pulp_Fiction#/media/File:Pulp_Fiction_(1994)_poster.jpg', 1994, 149),
('Inside Out', 'Riley (Kaitlyn Dias) is a happy, hockey-loving 11-year-old Midwestern girl, but her world turns upside-down when she and her parents move to San Francisco.',
    'https://en.wikipedia.org/wiki/Inside_Out_%282015_film%29#/media/File:Inside_Out_(2015_film)_poster.jpg', 2015, 95)

CREATE TABLE tblGenreMovie (
	GenreID int not null FOREIGN KEY REFERENCES tblGenre(GenreID),
	MovieID int not null FOREIGN KEY REFERENCES tblMovie(MovieID),
	CONSTRAINT PK_GenreID_MovieID PRIMARY KEY CLUSTERED (GenreID, MovieID)
);

INSERT INTO tblGenreMovie (GenreID, MovieID) VALUES
(1,1), (9,1), (14, 1),
(10,2), (2,2), (11,2), (13,2)

CREATE TABLE tblSerie (
    SerieID int PRIMARY KEY IDENTITY(1,1),
    Title varchar(255) NOT NULL,
    Synopsis text,
    ImageUrl varchar(500),
    TypeName varchar(5) DEFAULT 'Serie',
    CreatedAt datetime DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt datetime DEFAULT CURRENT_TIMESTAMP, -- Update on service
    NumOfSeasons int DEFAULT 1,
    IsFinished bit
);

INSERT INTO tblSerie (Title, Synopsis, ImageUrl, NumOfSeasons, IsFinished) VALUES
('Only Murders in the Building', 'Only Murders In The Building follows three strangers who share an obsession with true crime and suddenly find themselves wrapped up in one as they investigate the mysterious death of a neighbor in their New York City apartment building.',
    'https://en.wikipedia.org/wiki/Only_Murders_in_the_Building#/media/File:Only_Murders_logo.jpeg', 5, 0),
('Dark', 'A family saga with a supernatural twist, set in a German town where the disappearance of two young children exposes the relationships among four families.',
    'https://en.wikipedia.org/wiki/Dark_%28TV_series%29#/media/File:DarkNetflixPosterEnglish.jpg', 3, 1)

CREATE TABLE tblGenreSerie (
	GenreID int not null FOREIGN KEY REFERENCES tblGenre(GenreID),
	SerieID int not null FOREIGN KEY REFERENCES tblSerie(SerieID),
	CONSTRAINT PK_GenreID_SerieID PRIMARY KEY CLUSTERED (GenreID, SerieID)
);

INSERT INTO tblGenreSerie (GenreID, SerieID) VALUES
(3,1), (2,1), (12,1), (14,1),
(3,2), (12,2)

CREATE TABLE tblSeason (
    SeasonID int PRIMARY KEY IDENTITY(1,1),
    SeasonNum int not null,
    Synopsis text,
    ReleaseYear int CHECK (ReleaseYear BETWEEN 1700 AND 9999),
    NumEpisodes int,
    SerieFK int not null FOREIGN KEY (SerieFK) REFERENCES tblSerie(SerieID) ON DELETE CASCADE
);

INSERT INTO tblSeason (SeasonNum, Synopsis, ReleaseYear, NumEpisodes, SerieFK) VALUES
(1, '', 2021, 10, 1), (2, '', 2022, 10, 1), (3, '', 2023, 10, 1), (4, '', 2024, 10, 1), (5, '', 2025, 10, 1),
(1, '', 2017, 10, 2), (2, '', 2019, 8, 2), (3, '', 2020, 8, 2)

SELECT * FROM tblGenre
SELECT * FROM tblMovie
SELECT * FROM tblGenreMovie
SELECT * FROM tblSerie
SELECT * FROM tblSeason;