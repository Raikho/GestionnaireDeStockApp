CREATE PROC UserAdd
    @Name     VARCHAR (50),
    @SurName  VARCHAR (50),
    @UserName VARCHAR (50),
    @Password VARCHAR (50)
AS
	INSERT INTO UserIDTable(UserID, Name, SurName, UserName, Password)
	VALUES(@UserID, @Name, @SurName, @UserName, @Password)