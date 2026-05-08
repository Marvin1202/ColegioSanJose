CREATE DATABASE ColegioDB;
GO

USE ColegioDB;
GO

CREATE TABLE Alumnos (
    AlumnoId INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Apellido NVARCHAR(100) NOT NULL,
    FechaNacimiento DATE NOT NULL,
    Grado NVARCHAR(50) NOT NULL
);

CREATE TABLE Materias (
    MateriaId INT PRIMARY KEY IDENTITY(1,1),
    NombreMateria NVARCHAR(100) NOT NULL,
    Docente NVARCHAR(100) NOT NULL
);

CREATE TABLE Expedientes (
    ExpedienteId INT PRIMARY KEY IDENTITY(1,1),
    AlumnoId INT NOT NULL,
    MateriaId INT NOT NULL,
    NotaFinal DECIMAL(5,2) NOT NULL,
    Observaciones NVARCHAR(255),

    CONSTRAINT FK_Expedientes_Alumnos
        FOREIGN KEY (AlumnoId)
        REFERENCES Alumnos(AlumnoId),

    CONSTRAINT FK_Expedientes_Materias
        FOREIGN KEY (MateriaId)
        REFERENCES Materias(MateriaId)
);