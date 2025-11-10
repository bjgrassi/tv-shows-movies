DROP TABLE IF EXISTS tblAccessPermission, tblPermission, tblAccount, tblRole;

CREATE TABLE tblRole (
	RoleID int not null PRIMARY KEY IDENTITY(1,1),
	TypeName varchar(50) not null,
	Description nvarchar(max) not null
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
	AccountID int not null PRIMARY KEY IDENTITY(1,1),
	Email varchar(50) not null UNIQUE,
	FullName varchar(100) not null,
	Password varchar(12) not null,
	RoleID int not null FOREIGN KEY REFERENCES tblRole(RoleID)
)

INSERT INTO tblAccount (Email, FullName, Password, RoleID) VALUES
('adminzao@email.com', 'Admin Role', '102938', 1),
('masterzaum@email.com', 'Master Role', '102938', 2),
('regularzaum@email.com', 'Regular Role', '102938', 3),
('lilyaldrin@email.com', 'Lily Aldrin', '123456', 3)

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