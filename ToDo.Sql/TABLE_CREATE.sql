CREATE TABLE tblUsers
(
	ID int not null identity(1,1),
	[Username]	nvarchar(32) not null,
	[Password] nvarchar(32) not null,
	[Mail]		nvarchar(32) not null,

	CONSTRAINT pk_tblUsers_ID	PRIMARY KEY (ID)
)


CREATE TABLE tblTasks
(
	ID int not null identity(1,1),
	Title	nvarchar(128)	not null,
	[Description]	nvarchar(1024)	not null,
	[Status]	int	not null,
	DueDate	datetime	not null	default CURRENT_TIMESTAMP,
	UserID	int not null,

	CONSTRAINT	pk_tblTasks_ID	PRIMARY KEY(ID),

	CONSTRAINT fk_tblTasks_UserID_tblUsers_ID
	FOREIGN KEY(UserID)
	REFERENCES tblUsers(ID)
)