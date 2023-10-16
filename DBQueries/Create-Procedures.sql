USE Sistema_TEC;
GO

CREATE PROCEDURE Change_Password
	@email VARCHAR(255),
	@oldPassword VARCHAR(255),
	@newPassword VARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION;
    
    BEGIN TRY
        IF EXISTS (SELECT 1 FROM Person WHERE email = @email)
			BEGIN
				UPDATE Person
				SET personPassword = @newPassword
				WHERE email = @email AND personPassword = @oldPassword;
			END
        ELSE
			BEGIN
				;THROW 60000, 'Un usuario con este correo ya existe.', 1;
			END
        
        COMMIT;
    END TRY
    BEGIN CATCH
        ROLLBACK; 
        THROW 60001, 'Hubo un error al procesar la solicitud.', 1;
    END CATCH
END;
GO



CREATE PROCEDURE Create_Person
    @email VARCHAR(255),
    @personPassword VARCHAR(255),
    @id INT,
    @personName VARCHAR(255),
    @firstLastName VARCHAR(255),
    @secondLastName VARCHAR(255),
    @debt INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION;
    
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Person WHERE email = @email)
			BEGIN
				INSERT INTO Person (email, personPassword, id, personName, firstLastName, secondLastName, debt)
				VALUES (@email, @personPassword, @id, @personName, @firstLastName, @secondLastName, @debt);
			END
        ELSE
			BEGIN
				;THROW 60000, 'Un usuario con este correo ya existe.', 1;
			END
        
        COMMIT;
    END TRY
    BEGIN CATCH
        ROLLBACK; 
        THROW 60001, 'Hubo un error al procesar la solicitud.', 1;
    END CATCH
END;
GO

CREATE PROCEDURE Create_Student
    @email VARCHAR(255),
	@carnet INT,
	@degreeId INT ,
	@isExemptFromPrintingCosts BIT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION;
    
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Student WHERE email = @email)
			BEGIN
				INSERT INTO Student(id, email, degreeId, isExemptFromPrintingCosts)
				VALUES (@carnet, @email, @degreeId, @isExemptFromPrintingCosts);
			END
        ELSE
			BEGIN
				;THROW 60000, 'Un estudiante con este correo ya existe.', 1;
			END
        
        COMMIT;
    END TRY
    BEGIN CATCH
        ROLLBACK; 
        THROW 60001, 'Hubo un error al procesar la solicitud.', 1;
    END CATCH
END;
GO

CREATE PROCEDURE Create_Employee
    @id INT,
    @email VARCHAR(255),
    @isProfessor INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION;
    
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Employee WHERE email = @email)
			BEGIN
				INSERT INTO Employee (id, email, isProfessor)
				VALUES (@id, @email, @isProfessor);
			END
        ELSE
			BEGIN
				;THROW 60000, 'Un empleado con este correo ya existe.', 1;
			END
        
        COMMIT;
    END TRY
    BEGIN CATCH
        ROLLBACK; 
        THROW 60001, 'Hubo un error al procesar la solicitud.', 1;
    END CATCH
END;
GO


CREATE PROCEDURE Create_Application_Role
    @applicationId INT,
    @applicationRoleName VARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION;
    
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM ApplicationRole WHERE id = @applicationId AND applicationRoleName = @applicationRoleName) AND EXISTS (SELECT 1 FROM Application WHERE id = @applicationId)
			BEGIN
				INSERT INTO ApplicationRole (applicationId, applicationRoleName, parentId)
				VALUES (@applicationId, @applicationRoleName, null);
			END
        ELSE
			BEGIN
				;THROW 60000, 'Un rol con este nombre para esta aplicación ya existe o la aplicación no existe.', 1;
			END
        
        COMMIT;
    END TRY
    BEGIN CATCH
        ROLLBACK; 
        THROW 60001, 'Hubo un error al procesar la solicitud.', 1;
    END CATCH
END;
GO


CREATE PROCEDURE Assign_Role
    @email VARCHAR(255),
    @applicationRoleId INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION;
    
    BEGIN TRY
        IF EXISTS (SELECT 1 FROM ApplicationRole WHERE id = @applicationRoleId) AND EXISTS (SELECT 1 FROM Person WHERE email = @email)
			BEGIN
				INSERT INTO PersonXApplicationRole (email, applicationRoleId)
				VALUES (@email, @applicationRoleId);
			END
        ELSE
			BEGIN
				;THROW 60000, 'Un rol con este id no existe o la persona no existe.', 1;
			END
        
        COMMIT;
    END TRY
    BEGIN CATCH
        ROLLBACK; 
        THROW 60001, 'Hubo un error al procesar la solicitud.', 1;
    END CATCH
END;
GO