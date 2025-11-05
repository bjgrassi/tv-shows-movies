DROP TABLE IF EXISTS tblPermission, tblAccount, tblAccessPermission, tblRole;

CREATE TABLE tblRole (
	RoleID int not null PRIMARY KEY IDENTITY(1,1),
	TypeName varchar(50) not null,
	Description varchar(150) not null
)

INSERT INTO tblRole (TypeName, Description) VALUES 
('Master', 'Full management rights; Account and content included'),
('Admin', 'Limited account management rights; Content included'),
('Regular', 'No management rights; App Access only')

CREATE TABLE tblPermission (
	PermissionID int not null PRIMARY KEY IDENTITY(1,1),
	TypeName varchar(50) not null
)

INSERT INTO tblPermission (TypeName) VALUES 
('Content.Read'),
('Content.Write'),
('Archive.Read'),
('Archive.Write'),
('Account.Master'),
('Account.Read'),
('Account.Write')

CREATE TABLE tblAccount (
	Email varchar(50) not null PRIMARY KEY,
	FullName varchar(100) not null,
	RoleID int not null FOREIGN KEY REFERENCES tblRole(RoleID)
)

INSERT INTO tblAccount (Email, FullName, RoleID) VALUES
('adminzao@email.com', 'Admin Role', 1),
('masterzaum@email.com', 'Master Role', 2),
('regularzaum@email.com', 'Regular Role', 3),
('lilyaldrin@email.com', 'Lily Aldrin', 3)

CREATE TABLE tblAccessPermission (
	PermissionID int not null FOREIGN KEY REFERENCES tblPermission(PermissionID),
	RoleID int not null FOREIGN KEY REFERENCES tblRole(RoleID)
	CONSTRAINT PK_PermissionID_RoleID PRIMARY KEY CLUSTERED (PermissionID, RoleID)
) 

INSERT INTO tblAccessPermission (RoleID, PermissionID) VALUES
(1, 1),	-- REGULAR		- CONTENT.READ
(1, 3),	-- REGULAR		- ARCHIVE.READ
(1, 4),	-- REGULAR		- ARCHIVE.WRITE
(2, 1),	-- ADMIN		- CONTENT.READ
(2, 2),	-- ADMIN		- CONTENT.WRITE
(2, 6),	-- ADMIN		- ACCOUNT.READ
(2, 7),	-- ADMIN		- ACCOUNT.WRITE
(3, 5)	-- MASTER		- ACCOUNT.MASTER


select * from tblRole
select * from tblPermission
SELECT * FROM tblAccount