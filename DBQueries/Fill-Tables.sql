USE Sistema_Tec;
INSERT INTO Campus (campusName)
VALUES
	('Cartago'),
	('Limon'),
	('San Jose'),
	('San Carlos'),
	('Alajuela');

INSERT INTO Degree (degreeName, campusId)
VALUES
    ('Licenciatura en Ingeniería en Producción Industrial', 1),
	('Bachillerato en Ingeniería en Computación', 1),
	('Licenciatura en Ingeniería Electrónica', 1),
    ('Licenciatura en Ingeniería en Materiales', 1);

INSERT INTO School (schoolName, campusId)
VALUES
    ('Escuela de Ingeniería en Producción Industrial', 1),
    ('Escuela de Ingeniería en Computación', 1),
	('Escuela de Ingeniería Electrónica', 1),
    ('Escuela de Ciencia e Ingeniería de los Materiales', 1);

INSERT INTO Department (departmentName, campusId)
VALUES
    ('Dirección de Proyectos', 1),
    ('Departamento de Gestión del Talento Humano', 1),
    ('Departamento Financiero Contable', 1);

INSERT INTO Person (email, personPassword, id, personName, firstLastName, secondLastName, debt)
VALUES
    ('machobg08@estudiantec.cr', '1234', 305140419, 'Alex', 'Brenes', 'Garita', 0),
    ('andres2028@estudiantec.cr', '1234', 117770783, 'Andres', 'Sanchez', 'Rojas', 0),
    ('lorcacris13@estudiantec.cr', '1234', 117820120, 'Cristhofer', 'Loria', 'Calderon', 0),
    ('svega106@estudiantec.cr', '1234', 117450724, 'Sebastian', 'Vega', 'Cerdas', 0),
	('dani.alvarado@estudiantec.cr', '1234', 117321389, 'Daniela', 'Alvarado', 'Andrade', 0),
	('ldfozamis@estudiantec.cr', '1234', 110000000, 'Leonardo David', 'Fariña', 'Ozamis', 0),
    ('machacon@itcr.ac.cr', '1234', 107410535, 'Pedro', 'Ramirez', 'Polanci', 0);

INSERT INTO Student (id, email, degreeId, isExemptFromPrintingCosts)
VALUES
    (2018191805, 'machobg08@estudiantec.cr', 2, 0),
    (2018100180, 'andres2028@estudiantec.cr', 2, 0),
    (2018107992, 'lorcacris13@estudiantec.cr', 4, 0),
	(2018001949, 'svega106@estudiantec.cr', 3, 0),
	(2021004342, 'dani.alvarado@estudiantec.cr', 2, 0);
	
INSERT INTO Employee (id, email, isProfessor)
VALUES
    (305140419, 'machacon@itcr.ac.cr', 1),
    (105470415, 'ldfozamis@estudiantec.cr', 0);

INSERT INTO OrganizationType (organizationTypeName)
VALUES
    ('Asociacion'),
    ('Organo');

INSERT INTO Organization (email, degreeId, organizationPassword, organizationName, organizationTypeId, isCouncil)
VALUES
	('ce@estudiantec.cr', NULL, '1234', 'Consejo Ejecutivo', 2, 1),
    ('tee@estudiantec.cr', NULL, '1234', 'Tribunal Electoral Estudiantil', 2, 0),
	('feitec@estudiantec.cr', NULL, '1234', 'FEITEC', 2, 0),
    ('tj@estudiantec.cr', NULL, '1234', 'Tribunal Jurisdiccional', 2, 0),
	('aesetec@estudiantec.cr', 3, '1234', 'AESETEC', 1, 0),
	('aemtec@estudiantec.cr', 4, '1234', 'AEMTEC', 1, 0),
	('valeria_sell@estudiantec.cr', 1, '1234', 'AEPI', 1, 0),
	('juntadirectiva@asodec.com', 2, '1234', 'ASODEC', 1, 0);

INSERT INTO Application (applicationName, description)
VALUES
    ('Sistema-Tec', 'Sistema de servicios para estudiantes y empleados del Instituto Tecnológico de Costa Rica'),
	('Reservación de Canchas', ''),
    ('Emparejatec', ''),
	('Gestor de Asambleas', ''),
	('Gestor de Operadores', ''),
	('Arquitectura', ''),
	('PDF Printer', ''),
	('Gestor de Eventos', ''),
	('Sistema de Asignación Mentor Ahijado', 'El Sistema de Asignación Mentor Ahijado (SAMA) es un sistema diseñado para facilitar la conexión y el emparejamiento entre mentores y ahijados en un entorno educativo. Permite a los estudiantes nuevos (ahijados) encontrar mentores que compartan intereses y experiencias académicas, brindando un apoyo valioso para su transición a la vida universitaria. Los mentores pueden ofrecer orientación personalizada, compartiendo su conocimiento y ayudando a los ahijados a alcanzar sus metas académicas y profesionales. Con SAMA, se fomenta la comunidad y se promueve el éxito estudiantil a través de un proceso de mentoría.');

INSERT INTO ApplicationRole (applicationId, applicationRoleName, parentId)
VALUES
    (1, 'Administrador', NULL),
	(2, 'Administrador', NULL),
	(3, 'Administrador', NULL),
	(4, 'Administrador', NULL),
	(5, 'Administrador', NULL),
	(6, 'Administrador', NULL),
	(7, 'Administrador', NULL),
	(8, 'Administrador', NULL),
	(9, 'Administrador', NULL),
	(9, 'Ahijado', NULL),
	(9, 'Mentor', NULL);


INSERT INTO PersonXApplicationRole (email, applicationRoleId)
VALUES
    ('machobg08@estudiantec.cr', 1),
    ('andres2028@estudiantec.cr', 1),
	('dani.alvarado@estudiantec.cr', 9),
	('ldfozamis@estudiantec.cr', 1),
	('machacon@itcr.ac.cr', 1);


INSERT INTO PersonXSchool (email, schoolId)
VALUES
	('machacon@itcr.ac.cr', 2);



INSERT INTO PersonXDepartment (email, departmentId)
VALUES
	('ldfozamis@estudiantec.cr', 1);





