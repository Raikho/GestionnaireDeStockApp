ALTER PROC UserAdd
    @Name     VARCHAR (50),
    @SurName  VARCHAR (50),
    @UserName VARCHAR (50),
    @Password VARCHAR (50)
AS
	INSERT INTO UserIDTable(Name, SurName, UserName, Password)
	VALUES(@Name, @SurName, @UserName, @Password)