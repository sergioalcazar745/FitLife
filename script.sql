USE [master]
GO
/****** Object:  Database [FITLIFE2]    Script Date: 03/03/2023 13:55:55 ******/
CREATE DATABASE [FITLIFE2]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'FITLIFE2', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.DESARROLLO\MSSQL\DATA\FITLIFE2.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'FITLIFE2_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.DESARROLLO\MSSQL\DATA\FITLIFE2_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [FITLIFE2] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [FITLIFE2].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [FITLIFE2] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [FITLIFE2] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [FITLIFE2] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [FITLIFE2] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [FITLIFE2] SET ARITHABORT OFF 
GO
ALTER DATABASE [FITLIFE2] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [FITLIFE2] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [FITLIFE2] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [FITLIFE2] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [FITLIFE2] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [FITLIFE2] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [FITLIFE2] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [FITLIFE2] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [FITLIFE2] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [FITLIFE2] SET  DISABLE_BROKER 
GO
ALTER DATABASE [FITLIFE2] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [FITLIFE2] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [FITLIFE2] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [FITLIFE2] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [FITLIFE2] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [FITLIFE2] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [FITLIFE2] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [FITLIFE2] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [FITLIFE2] SET  MULTI_USER 
GO
ALTER DATABASE [FITLIFE2] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [FITLIFE2] SET DB_CHAINING OFF 
GO
ALTER DATABASE [FITLIFE2] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [FITLIFE2] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [FITLIFE2] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [FITLIFE2] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [FITLIFE2] SET QUERY_STORE = ON
GO
ALTER DATABASE [FITLIFE2] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [FITLIFE2]
GO
/****** Object:  Table [dbo].[ALIMENTO]    Script Date: 03/03/2023 13:55:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ALIMENTO](
	[IDALIMENTO] [int] NOT NULL,
	[NOMBRE] [nvarchar](50) NOT NULL,
	[PESO] [decimal](2, 2) NOT NULL,
	[KCAL] [decimal](2, 2) NOT NULL,
	[CARBOHIDRATOS] [decimal](2, 2) NOT NULL,
	[PROTEINAS] [decimal](2, 2) NOT NULL,
	[GRASAS] [decimal](2, 2) NOT NULL,
	[FIBRA] [decimal](2, 2) NOT NULL,
 CONSTRAINT [PK_ALIMENTO] PRIMARY KEY CLUSTERED 
(
	[IDALIMENTO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[COMIDA]    Script Date: 03/03/2023 13:55:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[COMIDA](
	[IDCOMIDA] [int] NOT NULL,
	[IDDIETA] [int] NOT NULL,
	[NOMBRE] [nvarchar](50) NOT NULL,
	[TOTALKCAL] [decimal](2, 2) NOT NULL,
	[COMENTARIO] [nvarchar](max) NULL,
 CONSTRAINT [PK_COMIDA] PRIMARY KEY CLUSTERED 
(
	[IDCOMIDA] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[COMIDAALIMENTO]    Script Date: 03/03/2023 13:55:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[COMIDAALIMENTO](
	[IDCOMIDAALIMENTO] [int] NOT NULL,
	[IDCOMIDA] [int] NOT NULL,
	[IDALIMENTO] [int] NOT NULL,
	[PESO] [decimal](2, 2) NOT NULL,
	[KCAL] [decimal](2, 2) NOT NULL,
	[CARBOHIDRATOS] [decimal](2, 2) NOT NULL,
	[PROTEINAS] [decimal](2, 2) NOT NULL,
	[FIBRA] [decimal](2, 2) NOT NULL,
	[GRASAS] [decimal](2, 2) NOT NULL,
 CONSTRAINT [PK_COMIDAALIMENTO] PRIMARY KEY CLUSTERED 
(
	[IDCOMIDAALIMENTO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DIETA]    Script Date: 03/03/2023 13:55:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DIETA](
	[IDDIETA] [int] NOT NULL,
	[IDNUTRICIONISTA] [int] NOT NULL,
	[IDCLIENTE] [int] NOT NULL,
	[FECHA] [datetime] NOT NULL,
 CONSTRAINT [PK_DIETA] PRIMARY KEY CLUSTERED 
(
	[IDDIETA] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EJERCICIO]    Script Date: 03/03/2023 13:55:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EJERCICIO](
	[IDEJERCICIO] [int] NOT NULL,
	[NOMBRE] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_EJERCICIO] PRIMARY KEY CLUSTERED 
(
	[IDEJERCICIO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PERFILUSUARIO]    Script Date: 03/03/2023 13:55:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PERFILUSUARIO](
	[EDAD] [int] NOT NULL,
	[SEXO] [nvarchar](50) NOT NULL,
	[ALTURA] [int] NOT NULL,
	[PESO] [int] NOT NULL,
	[IDUSUARIO] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RUTINAEJERCICIO]    Script Date: 03/03/2023 13:55:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RUTINAEJERCICIO](
	[IDRUTINAEJERCICIO] [int] NOT NULL,
	[IDRUTINA] [int] NOT NULL,
	[IDEJERCICIO] [int] NOT NULL,
	[SERIES] [int] NOT NULL,
	[REPETICIONES] [int] NOT NULL,
	[PAUSABAJADA] [int] NOT NULL,
	[PAUSASUBIDA] [int] NOT NULL,
	[PAUSAAGUANTE] [int] NOT NULL,
	[ARROBA] [int] NOT NULL,
 CONSTRAINT [PK_RUTINAEJERCICIO] PRIMARY KEY CLUSTERED 
(
	[IDRUTINAEJERCICIO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RUTINAS]    Script Date: 03/03/2023 13:55:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RUTINAS](
	[IDRUTINA] [int] NOT NULL,
	[IDCLIENTE] [int] NOT NULL,
	[IDENTRENADOR] [int] NOT NULL,
	[FECHA] [datetime] NOT NULL,
	[COMENTARIO] [nvarchar](max) NULL,
 CONSTRAINT [PK_RUTINAS] PRIMARY KEY CLUSTERED 
(
	[IDRUTINA] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[USUARIOS]    Script Date: 03/03/2023 13:55:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[USUARIOS](
	[IDUSUARIO] [int] NOT NULL,
	[NOMBRE] [nvarchar](50) NOT NULL,
	[APELLIDOS] [nvarchar](50) NOT NULL,
	[DNI] [nvarchar](50) NOT NULL,
	[EMAIL] [nvarchar](50) NOT NULL,
	[PASSWORD] [nvarchar](50) NOT NULL,
	[ROLE] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_USUARIOS] PRIMARY KEY CLUSTERED 
(
	[IDUSUARIO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[COMIDA]  WITH CHECK ADD  CONSTRAINT [FK_COMIDA_DIETA] FOREIGN KEY([IDDIETA])
REFERENCES [dbo].[DIETA] ([IDDIETA])
GO
ALTER TABLE [dbo].[COMIDA] CHECK CONSTRAINT [FK_COMIDA_DIETA]
GO
ALTER TABLE [dbo].[COMIDAALIMENTO]  WITH CHECK ADD  CONSTRAINT [FK_COMIDAALIMENTO_ALIMENTO] FOREIGN KEY([IDALIMENTO])
REFERENCES [dbo].[ALIMENTO] ([IDALIMENTO])
GO
ALTER TABLE [dbo].[COMIDAALIMENTO] CHECK CONSTRAINT [FK_COMIDAALIMENTO_ALIMENTO]
GO
ALTER TABLE [dbo].[COMIDAALIMENTO]  WITH CHECK ADD  CONSTRAINT [FK_COMIDAALIMENTO_COMIDA] FOREIGN KEY([IDCOMIDA])
REFERENCES [dbo].[COMIDA] ([IDCOMIDA])
GO
ALTER TABLE [dbo].[COMIDAALIMENTO] CHECK CONSTRAINT [FK_COMIDAALIMENTO_COMIDA]
GO
ALTER TABLE [dbo].[DIETA]  WITH CHECK ADD  CONSTRAINT [FK_DIETA_USUARIOS] FOREIGN KEY([IDNUTRICIONISTA])
REFERENCES [dbo].[USUARIOS] ([IDUSUARIO])
GO
ALTER TABLE [dbo].[DIETA] CHECK CONSTRAINT [FK_DIETA_USUARIOS]
GO
ALTER TABLE [dbo].[DIETA]  WITH CHECK ADD  CONSTRAINT [FK_DIETA_USUARIOS1] FOREIGN KEY([IDCLIENTE])
REFERENCES [dbo].[USUARIOS] ([IDUSUARIO])
GO
ALTER TABLE [dbo].[DIETA] CHECK CONSTRAINT [FK_DIETA_USUARIOS1]
GO
ALTER TABLE [dbo].[PERFILUSUARIO]  WITH CHECK ADD  CONSTRAINT [FK_PERFILUSUARIO_USUARIOS] FOREIGN KEY([IDUSUARIO])
REFERENCES [dbo].[USUARIOS] ([IDUSUARIO])
GO
ALTER TABLE [dbo].[PERFILUSUARIO] CHECK CONSTRAINT [FK_PERFILUSUARIO_USUARIOS]
GO
ALTER TABLE [dbo].[RUTINAEJERCICIO]  WITH CHECK ADD  CONSTRAINT [FK_RUTINAEJERCICIO_EJERCICIO] FOREIGN KEY([IDEJERCICIO])
REFERENCES [dbo].[EJERCICIO] ([IDEJERCICIO])
GO
ALTER TABLE [dbo].[RUTINAEJERCICIO] CHECK CONSTRAINT [FK_RUTINAEJERCICIO_EJERCICIO]
GO
ALTER TABLE [dbo].[RUTINAEJERCICIO]  WITH CHECK ADD  CONSTRAINT [FK_RUTINAEJERCICIO_RUTINAS] FOREIGN KEY([IDRUTINA])
REFERENCES [dbo].[RUTINAS] ([IDRUTINA])
GO
ALTER TABLE [dbo].[RUTINAEJERCICIO] CHECK CONSTRAINT [FK_RUTINAEJERCICIO_RUTINAS]
GO
ALTER TABLE [dbo].[RUTINAS]  WITH CHECK ADD  CONSTRAINT [FK_RUTINAS_USUARIOS] FOREIGN KEY([IDCLIENTE])
REFERENCES [dbo].[USUARIOS] ([IDUSUARIO])
GO
ALTER TABLE [dbo].[RUTINAS] CHECK CONSTRAINT [FK_RUTINAS_USUARIOS]
GO
ALTER TABLE [dbo].[RUTINAS]  WITH CHECK ADD  CONSTRAINT [FK_RUTINAS_USUARIOS1] FOREIGN KEY([IDENTRENADOR])
REFERENCES [dbo].[USUARIOS] ([IDUSUARIO])
GO
ALTER TABLE [dbo].[RUTINAS] CHECK CONSTRAINT [FK_RUTINAS_USUARIOS1]
GO
USE [master]
GO
ALTER DATABASE [FITLIFE2] SET  READ_WRITE 
GO
