USE Sistema_Tec;

CREATE TABLE Campus ( --Sede
	id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	campusName VARCHAR(255) NOT NULL
);

CREATE TABLE Degree ( --Carrera
	id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	degreeName VARCHAR(255) NOT NULL,
	campusId INT FOREIGN KEY (campusId) REFERENCES Campus(id) NOT NULL
);

CREATE TABLE School ( --Escuela
	id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	schoolName VARCHAR(255) NOT NULL,
	campusId INT FOREIGN KEY (campusId) REFERENCES Campus(id) NOT NULL
);

CREATE TABLE Department ( --Departamenteo
	id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	departmentName VARCHAR(255) NOT NULL,
	campusId INT FOREIGN KEY (campusId) REFERENCES Campus(id) NOT NULL
);


CREATE TABLE Person ( --Persona
	email VARCHAR(255) PRIMARY KEY NOT NULL,
	personPassword VARCHAR(255) NOT NULL,
	id INT NOT NULL UNIQUE, --Cedula
	personName VARCHAR(255) NOT NULL,
	firstLastName VARCHAR(255) NOT NULL,
	secondLastName VARCHAR(255) NOT NULL,
	debt INT NOT NULL
);

CREATE TABLE Student ( --Estudiante, solo tiene las caracter sticas espec ficas de estudiante
	id INT NOT NULL PRIMARY KEY, --Carnet
	email VARCHAR(255) FOREIGN KEY (email) REFERENCES Person(email) NOT NULL UNIQUE,
	degreeId INT FOREIGN KEY (degreeId) REFERENCES Degree(id) NOT NULL,
	isExemptFromPrintingCosts BIT NOT NULL
);

CREATE TABLE Employee ( --Administrativo o profesor, solo tiene las caracter sticas espec ficas de empleado
	id INT NOT NULL PRIMARY KEY, --id de empleado
	email VARCHAR(255) FOREIGN KEY (email) REFERENCES Person(email) NOT NULL UNIQUE,
	isProfessor BIT NOT NULL
);

CREATE TABLE OrganizationType (
	id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	organizationTypeName VARCHAR(255) NOT NULL
);


CREATE TABLE Organization ( --Organizaciones
	email VARCHAR(255) PRIMARY KEY NOT NULL,
	degreeId INT FOREIGN KEY (degreeId) REFERENCES Degree(id),
	organizationPassword VARCHAR(255) NOT NULL,
	organizationName VARCHAR(255) NOT NULL,
	organizationTypeId INT FOREIGN KEY (organizationTypeId) REFERENCES OrganizationType(id) NOT NULL,
	isCouncil BIT NOT NULL
);

CREATE TABLE Application ( --Escuela
	id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	applicationName VARCHAR(255) NOT NULL,
	description VARCHAR(1000) NOT NULL
);

CREATE TABLE ApplicationRole ( --Role es una palabra reservada de sql entonces le puse as 
	id INT IDENTITY(1,1) PRIMARY KEY NOT NULL,
	applicationId INT FOREIGN KEY (applicationId) REFERENCES Application(id) NOT NULL,
	applicationRoleName VARCHAR(255) NOT NULL,
	parentId INT FOREIGN KEY (parentId) REFERENCES ApplicationRole(id) --Por si se necesita una jerarqu a de roles
);

CREATE TABLE PersonXApplicationRole ( --PersonaXRol
	email VARCHAR(255) FOREIGN KEY (email) REFERENCES Person(email) NOT NULL,
    applicationRoleId INT FOREIGN KEY (applicationRoleId) REFERENCES ApplicationRole(id) NOT NULL,
    PRIMARY KEY (email, applicationRoleId)
);

CREATE TABLE PersonXSchool ( --PersonaXRol
	email VARCHAR(255) FOREIGN KEY (email) REFERENCES Person(email) NOT NULL,
	schoolId INT FOREIGN KEY (schoolId) REFERENCES School(id) NOT NULL,
    PRIMARY KEY (email, schoolId)
);

CREATE TABLE PersonXDepartment ( --PersonaXRol
	email VARCHAR(255) FOREIGN KEY (email) REFERENCES Person(email) NOT NULL,
	departmentId INT FOREIGN KEY (departmentId) REFERENCES Department(id) NOT NULL,
    PRIMARY KEY (email, departmentId)
);