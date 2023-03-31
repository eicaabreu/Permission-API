Create database Permission

CREATE TABLE permission_types (
    id INT IDENTITY PRIMARY KEY,
    description varchar(100) NOT NULL
);


CREATE TABLE permissions (
    Id INT IDENTITY PRIMARY KEY,
    Name varchar(100) NOT NULL,
    LastName varchar(100) NOT NULL,
    PermissionTypeId INT NOT NULL,
    CreatedAt dateTime NOT NULL,
    UpdatedAt dateTime NOT NULL,
	DeletedAt dateTime NOT NULL,
    CONSTRAINT fk_permission_type_id FOREIGN KEY(PermissionTypeId) REFERENCES permission_types(id)
    
);

INSERT INTO permission_types ( description) VALUES
    ('Enfermedad')
   ,('Diligencias')
   ,('Otros')
    

