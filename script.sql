USE [master]
GO
/****** Object:  Database [FITLIFE]    Script Date: 23/03/2023 14:25:19 ******/
CREATE DATABASE [FITLIFE]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'FITLIFE', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.DESARROLLO\MSSQL\DATA\FITLIFE.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'FITLIFE_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.DESARROLLO\MSSQL\DATA\FITLIFE_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [FITLIFE] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [FITLIFE].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [FITLIFE] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [FITLIFE] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [FITLIFE] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [FITLIFE] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [FITLIFE] SET ARITHABORT OFF 
GO
ALTER DATABASE [FITLIFE] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [FITLIFE] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [FITLIFE] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [FITLIFE] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [FITLIFE] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [FITLIFE] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [FITLIFE] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [FITLIFE] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [FITLIFE] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [FITLIFE] SET  DISABLE_BROKER 
GO
ALTER DATABASE [FITLIFE] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [FITLIFE] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [FITLIFE] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [FITLIFE] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [FITLIFE] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [FITLIFE] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [FITLIFE] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [FITLIFE] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [FITLIFE] SET  MULTI_USER 
GO
ALTER DATABASE [FITLIFE] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [FITLIFE] SET DB_CHAINING OFF 
GO
ALTER DATABASE [FITLIFE] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [FITLIFE] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [FITLIFE] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [FITLIFE] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [FITLIFE] SET QUERY_STORE = ON
GO
ALTER DATABASE [FITLIFE] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [FITLIFE]
GO
/****** Object:  Table [dbo].[ALIMENTO]    Script Date: 23/03/2023 14:25:19 ******/
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
/****** Object:  Table [dbo].[COMIDA]    Script Date: 23/03/2023 14:25:19 ******/
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
/****** Object:  Table [dbo].[COMIDAALIMENTO]    Script Date: 23/03/2023 14:25:19 ******/
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
/****** Object:  Table [dbo].[DIETA]    Script Date: 23/03/2023 14:25:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DIETA](
	[IDDIETA] [int] NOT NULL,
	[IDNUTRICIONISTA] [int] NOT NULL,
	[IDCLIENTE] [int] NOT NULL,
	[FECHA] [datetime] NOT NULL,
	[COMENTARIO] [nvarchar](max) NULL,
 CONSTRAINT [PK_DIETA] PRIMARY KEY CLUSTERED 
(
	[IDDIETA] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EJERCICIO]    Script Date: 23/03/2023 14:25:19 ******/
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
/****** Object:  Table [dbo].[PERFILUSUARIO]    Script Date: 23/03/2023 14:25:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PERFILUSUARIO](
	[IDPERFILUSUARIO] [int] NOT NULL,
	[EDAD] [int] NOT NULL,
	[SEXO] [nvarchar](50) NOT NULL,
	[ALTURA] [int] NOT NULL,
	[PESO] [int] NOT NULL,
	[IDUSUARIO] [int] NOT NULL,
	[IDENTRENADOR] [int] NULL,
	[IDDIETISTA] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RUTINAEJERCICIO]    Script Date: 23/03/2023 14:25:19 ******/
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
/****** Object:  Table [dbo].[RUTINAS]    Script Date: 23/03/2023 14:25:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RUTINAS](
	[IDRUTINA] [int] NOT NULL,
	[IDCLIENTE] [int] NOT NULL,
	[IDENTRENADOR] [int] NOT NULL,
	[FECHA] [datetime] NOT NULL,
	[NOMBRE] [nvarchar](50) NULL,
	[COMENTARIO] [nvarchar](max) NULL,
 CONSTRAINT [PK_RUTINAS] PRIMARY KEY CLUSTERED 
(
	[IDRUTINA] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SOLICITUD]    Script Date: 23/03/2023 14:25:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SOLICITUD](
	[IDSOLICITUD] [int] NOT NULL,
	[SALT] [nvarchar](max) NOT NULL,
	[CODIGO] [int] NOT NULL,
	[IDUSUARIO] [int] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[USUARIOS]    Script Date: 23/03/2023 14:25:19 ******/
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
	[PASSWORDENCRYPT] [varbinary](max) NOT NULL,
	[SALT] [nvarchar](50) NOT NULL,
	[PASSWORD] [nvarchar](50) NOT NULL,
	[ROLE] [nvarchar](50) NOT NULL,
	[ESTADO] [bit] NOT NULL,
 CONSTRAINT [PK_USUARIOS] PRIMARY KEY CLUSTERED 
(
	[IDUSUARIO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[EJERCICIO] ([IDEJERCICIO], [NOMBRE]) VALUES (1, N'Press Banca')
INSERT [dbo].[EJERCICIO] ([IDEJERCICIO], [NOMBRE]) VALUES (2, N'Sentadilla')
INSERT [dbo].[EJERCICIO] ([IDEJERCICIO], [NOMBRE]) VALUES (3, N'Peso muerto')
INSERT [dbo].[EJERCICIO] ([IDEJERCICIO], [NOMBRE]) VALUES (4, N'Jalon al pecho')
INSERT [dbo].[EJERCICIO] ([IDEJERCICIO], [NOMBRE]) VALUES (5, N'Remo girondina')
INSERT [dbo].[EJERCICIO] ([IDEJERCICIO], [NOMBRE]) VALUES (6, N'Remo unilateral')
INSERT [dbo].[EJERCICIO] ([IDEJERCICIO], [NOMBRE]) VALUES (7, N'Biceps predicador')
INSERT [dbo].[EJERCICIO] ([IDEJERCICIO], [NOMBRE]) VALUES (8, N'Biceps mancuerna')
INSERT [dbo].[EJERCICIO] ([IDEJERCICIO], [NOMBRE]) VALUES (9, N'Biceps martillo')
INSERT [dbo].[EJERCICIO] ([IDEJERCICIO], [NOMBRE]) VALUES (10, N'Press inclinado')
INSERT [dbo].[EJERCICIO] ([IDEJERCICIO], [NOMBRE]) VALUES (11, N'Press plano')
INSERT [dbo].[EJERCICIO] ([IDEJERCICIO], [NOMBRE]) VALUES (12, N'Triceps polea')
INSERT [dbo].[EJERCICIO] ([IDEJERCICIO], [NOMBRE]) VALUES (13, N'Triceps encima de la cabeza')
INSERT [dbo].[EJERCICIO] ([IDEJERCICIO], [NOMBRE]) VALUES (14, N'Hombro frontal')
INSERT [dbo].[EJERCICIO] ([IDEJERCICIO], [NOMBRE]) VALUES (15, N'Hombro lateral')
INSERT [dbo].[EJERCICIO] ([IDEJERCICIO], [NOMBRE]) VALUES (16, N'Hombro posterior')
INSERT [dbo].[EJERCICIO] ([IDEJERCICIO], [NOMBRE]) VALUES (17, N'Pájaros')
INSERT [dbo].[EJERCICIO] ([IDEJERCICIO], [NOMBRE]) VALUES (18, N'Prensa')
INSERT [dbo].[EJERCICIO] ([IDEJERCICIO], [NOMBRE]) VALUES (19, N'Rumano')
INSERT [dbo].[EJERCICIO] ([IDEJERCICIO], [NOMBRE]) VALUES (20, N'Aductores')
INSERT [dbo].[EJERCICIO] ([IDEJERCICIO], [NOMBRE]) VALUES (21, N'Extension de cuadriceps')
INSERT [dbo].[EJERCICIO] ([IDEJERCICIO], [NOMBRE]) VALUES (22, N'Extension de femoral sentado')
INSERT [dbo].[EJERCICIO] ([IDEJERCICIO], [NOMBRE]) VALUES (23, N'Extension de femoral tumbado')
INSERT [dbo].[EJERCICIO] ([IDEJERCICIO], [NOMBRE]) VALUES (24, N'Gemelo')
GO
INSERT [dbo].[PERFILUSUARIO] ([IDPERFILUSUARIO], [EDAD], [SEXO], [ALTURA], [PESO], [IDUSUARIO], [IDENTRENADOR], [IDDIETISTA]) VALUES (1, 21, N'masculino', 185, 85, 1, 2, 3)
INSERT [dbo].[PERFILUSUARIO] ([IDPERFILUSUARIO], [EDAD], [SEXO], [ALTURA], [PESO], [IDUSUARIO], [IDENTRENADOR], [IDDIETISTA]) VALUES (2, 25, N'masculino', 178, 65, 4, 2, 3)
GO
INSERT [dbo].[RUTINAEJERCICIO] ([IDRUTINAEJERCICIO], [IDRUTINA], [IDEJERCICIO], [SERIES], [REPETICIONES], [PAUSABAJADA], [PAUSASUBIDA], [PAUSAAGUANTE], [ARROBA]) VALUES (1, 1, 1, 4, 8, 0, 0, 0, 0)
INSERT [dbo].[RUTINAEJERCICIO] ([IDRUTINAEJERCICIO], [IDRUTINA], [IDEJERCICIO], [SERIES], [REPETICIONES], [PAUSABAJADA], [PAUSASUBIDA], [PAUSAAGUANTE], [ARROBA]) VALUES (2, 1, 2, 4, 5, 0, 0, 0, 0)
INSERT [dbo].[RUTINAEJERCICIO] ([IDRUTINAEJERCICIO], [IDRUTINA], [IDEJERCICIO], [SERIES], [REPETICIONES], [PAUSABAJADA], [PAUSASUBIDA], [PAUSAAGUANTE], [ARROBA]) VALUES (3, 2, 18, 4, 12, 0, 0, 0, 0)
INSERT [dbo].[RUTINAEJERCICIO] ([IDRUTINAEJERCICIO], [IDRUTINA], [IDEJERCICIO], [SERIES], [REPETICIONES], [PAUSABAJADA], [PAUSASUBIDA], [PAUSAAGUANTE], [ARROBA]) VALUES (4, 2, 20, 4, 15, 0, 0, 0, 10)
INSERT [dbo].[RUTINAEJERCICIO] ([IDRUTINAEJERCICIO], [IDRUTINA], [IDEJERCICIO], [SERIES], [REPETICIONES], [PAUSABAJADA], [PAUSASUBIDA], [PAUSAAGUANTE], [ARROBA]) VALUES (5, 2, 22, 4, 12, 0, 0, 0, 0)
INSERT [dbo].[RUTINAEJERCICIO] ([IDRUTINAEJERCICIO], [IDRUTINA], [IDEJERCICIO], [SERIES], [REPETICIONES], [PAUSABAJADA], [PAUSASUBIDA], [PAUSAAGUANTE], [ARROBA]) VALUES (6, 2, 21, 4, 12, 0, 0, 0, 0)
INSERT [dbo].[RUTINAEJERCICIO] ([IDRUTINAEJERCICIO], [IDRUTINA], [IDEJERCICIO], [SERIES], [REPETICIONES], [PAUSABAJADA], [PAUSASUBIDA], [PAUSAAGUANTE], [ARROBA]) VALUES (7, 3, 4, 4, 12, 0, 0, 0, 0)
INSERT [dbo].[RUTINAEJERCICIO] ([IDRUTINAEJERCICIO], [IDRUTINA], [IDEJERCICIO], [SERIES], [REPETICIONES], [PAUSABAJADA], [PAUSASUBIDA], [PAUSAAGUANTE], [ARROBA]) VALUES (8, 3, 5, 4, 12, 0, 0, 0, 9)
INSERT [dbo].[RUTINAEJERCICIO] ([IDRUTINAEJERCICIO], [IDRUTINA], [IDEJERCICIO], [SERIES], [REPETICIONES], [PAUSABAJADA], [PAUSASUBIDA], [PAUSAAGUANTE], [ARROBA]) VALUES (9, 3, 3, 4, 5, 0, 0, 0, 0)
INSERT [dbo].[RUTINAEJERCICIO] ([IDRUTINAEJERCICIO], [IDRUTINA], [IDEJERCICIO], [SERIES], [REPETICIONES], [PAUSABAJADA], [PAUSASUBIDA], [PAUSAAGUANTE], [ARROBA]) VALUES (10, 4, 1, 4, 5, 0, 0, 0, 9)
INSERT [dbo].[RUTINAEJERCICIO] ([IDRUTINAEJERCICIO], [IDRUTINA], [IDEJERCICIO], [SERIES], [REPETICIONES], [PAUSABAJADA], [PAUSASUBIDA], [PAUSAAGUANTE], [ARROBA]) VALUES (12, 4, 21, 4, 0, 0, 0, 0, 0)
INSERT [dbo].[RUTINAEJERCICIO] ([IDRUTINAEJERCICIO], [IDRUTINA], [IDEJERCICIO], [SERIES], [REPETICIONES], [PAUSABAJADA], [PAUSASUBIDA], [PAUSAAGUANTE], [ARROBA]) VALUES (13, 4, 4, 3, 10, 0, 0, 0, 0)
INSERT [dbo].[RUTINAEJERCICIO] ([IDRUTINAEJERCICIO], [IDRUTINA], [IDEJERCICIO], [SERIES], [REPETICIONES], [PAUSABAJADA], [PAUSASUBIDA], [PAUSAAGUANTE], [ARROBA]) VALUES (14, 5, 12, 3, 15, 0, 0, 0, 0)
INSERT [dbo].[RUTINAEJERCICIO] ([IDRUTINAEJERCICIO], [IDRUTINA], [IDEJERCICIO], [SERIES], [REPETICIONES], [PAUSABAJADA], [PAUSASUBIDA], [PAUSAAGUANTE], [ARROBA]) VALUES (15, 5, 17, 4, 12, 0, 0, 0, 0)
INSERT [dbo].[RUTINAEJERCICIO] ([IDRUTINAEJERCICIO], [IDRUTINA], [IDEJERCICIO], [SERIES], [REPETICIONES], [PAUSABAJADA], [PAUSASUBIDA], [PAUSAAGUANTE], [ARROBA]) VALUES (16, 5, 20, 5, 10, 0, 0, 0, 0)
INSERT [dbo].[RUTINAEJERCICIO] ([IDRUTINAEJERCICIO], [IDRUTINA], [IDEJERCICIO], [SERIES], [REPETICIONES], [PAUSABAJADA], [PAUSASUBIDA], [PAUSAAGUANTE], [ARROBA]) VALUES (17, 6, 2, 5, 10, 0, 0, 0, 0)
INSERT [dbo].[RUTINAEJERCICIO] ([IDRUTINAEJERCICIO], [IDRUTINA], [IDEJERCICIO], [SERIES], [REPETICIONES], [PAUSABAJADA], [PAUSASUBIDA], [PAUSAAGUANTE], [ARROBA]) VALUES (18, 6, 20, 3, 15, 0, 0, 0, 0)
INSERT [dbo].[RUTINAEJERCICIO] ([IDRUTINAEJERCICIO], [IDRUTINA], [IDEJERCICIO], [SERIES], [REPETICIONES], [PAUSABAJADA], [PAUSASUBIDA], [PAUSAAGUANTE], [ARROBA]) VALUES (19, 6, 19, 5, 3, 0, 0, 0, 0)
INSERT [dbo].[RUTINAEJERCICIO] ([IDRUTINAEJERCICIO], [IDRUTINA], [IDEJERCICIO], [SERIES], [REPETICIONES], [PAUSABAJADA], [PAUSASUBIDA], [PAUSAAGUANTE], [ARROBA]) VALUES (20, 7, 4, 4, 12, 0, 0, 0, 0)
INSERT [dbo].[RUTINAEJERCICIO] ([IDRUTINAEJERCICIO], [IDRUTINA], [IDEJERCICIO], [SERIES], [REPETICIONES], [PAUSABAJADA], [PAUSASUBIDA], [PAUSAAGUANTE], [ARROBA]) VALUES (21, 7, 5, 3, 10, 0, 0, 0, 0)
INSERT [dbo].[RUTINAEJERCICIO] ([IDRUTINAEJERCICIO], [IDRUTINA], [IDEJERCICIO], [SERIES], [REPETICIONES], [PAUSABAJADA], [PAUSASUBIDA], [PAUSAAGUANTE], [ARROBA]) VALUES (22, 7, 3, 4, 10, 0, 0, 0, 0)
INSERT [dbo].[RUTINAEJERCICIO] ([IDRUTINAEJERCICIO], [IDRUTINA], [IDEJERCICIO], [SERIES], [REPETICIONES], [PAUSABAJADA], [PAUSASUBIDA], [PAUSAAGUANTE], [ARROBA]) VALUES (23, 8, 1, 4, 10, 0, 0, 0, 0)
INSERT [dbo].[RUTINAEJERCICIO] ([IDRUTINAEJERCICIO], [IDRUTINA], [IDEJERCICIO], [SERIES], [REPETICIONES], [PAUSABAJADA], [PAUSASUBIDA], [PAUSAAGUANTE], [ARROBA]) VALUES (26, 8, 4, 6, 5, 0, 0, 0, 0)
INSERT [dbo].[RUTINAEJERCICIO] ([IDRUTINAEJERCICIO], [IDRUTINA], [IDEJERCICIO], [SERIES], [REPETICIONES], [PAUSABAJADA], [PAUSASUBIDA], [PAUSAAGUANTE], [ARROBA]) VALUES (27, 8, 6, 4, 10, 0, 0, 0, 0)
GO
INSERT [dbo].[RUTINAS] ([IDRUTINA], [IDCLIENTE], [IDENTRENADOR], [FECHA], [NOMBRE], [COMENTARIO]) VALUES (1, 1, 2, CAST(N'2023-03-20T00:00:00.000' AS DateTime), N'Pecho', N'Prueba')
INSERT [dbo].[RUTINAS] ([IDRUTINA], [IDCLIENTE], [IDENTRENADOR], [FECHA], [NOMBRE], [COMENTARIO]) VALUES (2, 1, 2, CAST(N'2023-03-23T00:00:00.000' AS DateTime), N'Pierna', N'He acabado sofocado')
INSERT [dbo].[RUTINAS] ([IDRUTINA], [IDCLIENTE], [IDENTRENADOR], [FECHA], [NOMBRE], [COMENTARIO]) VALUES (3, 1, 2, CAST(N'2023-03-22T00:00:00.000' AS DateTime), N'Espalda', N'')
INSERT [dbo].[RUTINAS] ([IDRUTINA], [IDCLIENTE], [IDENTRENADOR], [FECHA], [NOMBRE], [COMENTARIO]) VALUES (4, 1, 2, CAST(N'2023-04-03T00:00:00.000' AS DateTime), N'Full Body', N'')
INSERT [dbo].[RUTINAS] ([IDRUTINA], [IDCLIENTE], [IDENTRENADOR], [FECHA], [NOMBRE], [COMENTARIO]) VALUES (5, 1, 2, CAST(N'2023-03-28T00:00:00.000' AS DateTime), N'Empujes', N'Duro')
INSERT [dbo].[RUTINAS] ([IDRUTINA], [IDCLIENTE], [IDENTRENADOR], [FECHA], [NOMBRE], [COMENTARIO]) VALUES (6, 1, 2, CAST(N'2023-04-15T00:00:00.000' AS DateTime), N'Pierna lets go', N'')
INSERT [dbo].[RUTINAS] ([IDRUTINA], [IDCLIENTE], [IDENTRENADOR], [FECHA], [NOMBRE], [COMENTARIO]) VALUES (7, 1, 2, CAST(N'2023-05-18T00:00:00.000' AS DateTime), N'Espalda', N'')
INSERT [dbo].[RUTINAS] ([IDRUTINA], [IDCLIENTE], [IDENTRENADOR], [FECHA], [NOMBRE], [COMENTARIO]) VALUES (8, 4, 2, CAST(N'2023-03-22T00:00:00.000' AS DateTime), N'Empujes', N'Parece que funciona')
GO
INSERT [dbo].[USUARIOS] ([IDUSUARIO], [NOMBRE], [APELLIDOS], [DNI], [EMAIL], [PASSWORDENCRYPT], [SALT], [PASSWORD], [ROLE], [ESTADO]) VALUES (1, N'Sergio', N'Alcazar Monedero', N'51742265Q', N'sergioalcazar745@gmail.com', 0x1283974F32B4D280D81F59BB32D3D029D81C4C049E583D193577B115CDDD86ECD53DADC2A75ED98BF7F426AEDFADE01BD350024216812875ACAF6691B0FE92C1, N'ÄÚÛñÂØÝ¦»µ-xß&Óµ~k»?ê¥Øv¦ay²W×ò¡twxbqW', N'Aa123456', N'cliente', 1)
INSERT [dbo].[USUARIOS] ([IDUSUARIO], [NOMBRE], [APELLIDOS], [DNI], [EMAIL], [PASSWORDENCRYPT], [SALT], [PASSWORD], [ROLE], [ESTADO]) VALUES (2, N'Nerea', N'Pérez Gismero', N'51526868Z', N'nerea.perez.gismero@gmail.com', 0xCD49BBC92F94A0937736A12FBC1929080FF731FF6098A5F5CD4E80F590477E074A03730A62AE7064DF20909F498C037B3703A50B760C4B1A5F86BDBD75473434, N'i]:°GBÛbuKâÁb?`Pý ]çòøÖ¦wL´}2ò÷¾rÌ', N'Aa123456', N'entrenador', 1)
INSERT [dbo].[USUARIOS] ([IDUSUARIO], [NOMBRE], [APELLIDOS], [DNI], [EMAIL], [PASSWORDENCRYPT], [SALT], [PASSWORD], [ROLE], [ESTADO]) VALUES (3, N'Ismael', N'Carranza Bouchdak', N'09652572R', N'sergioalcazar2803@gmail.com', 0x58516137D8EEA7B4A494384B23551C8A7C51272AF15F9E7C9F904B0450F59BE6DFB3E323D778CDE27629C6FE4173AC1B01B13A871086BF7A09927F9E049B8EF3, N'»ÀÑî·®]»²òusg)5nó¯2j[-Ú þ]ìçíWãèès*WÒå', N'Aa12345678', N'nutricionista', 1)
INSERT [dbo].[USUARIOS] ([IDUSUARIO], [NOMBRE], [APELLIDOS], [DNI], [EMAIL], [PASSWORDENCRYPT], [SALT], [PASSWORD], [ROLE], [ESTADO]) VALUES (4, N'Antonio', N'García Martínez', N'66843872F', N'fitlifetajamar2023@gmail.com', 0xA31207BE3EAF68C4EC84ACB7759D32CD8253371B8A937F94ADCE7FDFDDAE716174C4A4923A5A96CF25886DF4687D8CCF2185E05C0A4B19292B7FA51709AA3D10, N'dó÷kp$çSv¸)mh-_çQÆ»:mÙ/¬i½»(@±Ô¯.ÛÇB', N'Aa123456', N'cliente', 1)
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
/****** Object:  StoredProcedure [dbo].[SP_DELETE_SOLICITUD_UPDATE_ESTADO_USUARIO]    Script Date: 23/03/2023 14:25:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_DELETE_SOLICITUD_UPDATE_ESTADO_USUARIO]
(@IDUSUARIO INT)
AS
	DELETE FROM SOLICITUD WHERE IDUSUARIO = @IDUSUARIO
	UPDATE USUARIOS SET ESTADO = 1 WHERE IDUSUARIO = @IDUSUARIO
GO
/****** Object:  StoredProcedure [dbo].[SP_ELIMINAR_RUTINA]    Script Date: 23/03/2023 14:25:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_ELIMINAR_RUTINA]
(@IDRUTINA INT)
AS
	DELETE FROM RUTINAEJERCICIO WHERE IDRUTINA = @IDRUTINA
	DELETE FROM RUTINAS WHERE IDRUTINA = @IDRUTINA
GO
/****** Object:  StoredProcedure [dbo].[SP_PRUEBA]    Script Date: 23/03/2023 14:25:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_PRUEBA]
(@IDRUTINA INT OUT)
AS
    SET @IDRUTINA = 5
	PRINT('H: ' + CAST(@IDRUTINA AS NVARCHAR(50)))
GO
/****** Object:  StoredProcedure [dbo].[SP_REGISTER_CLIENTE]    Script Date: 23/03/2023 14:25:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_REGISTER_CLIENTE]
(@NOMBRE NVARCHAR(50), @APELLIDOS NVARCHAR(50), @DNI NVARCHAR(50), @EMAIL NVARCHAR(MAX), @PASSWORDENCRYPT VARBINARY(MAX), @SALT NVARCHAR(50), @PASSWORD NVARCHAR(50), @ROLE NVARCHAR(50), 
@ALTURA INT, @PESO INT, @EDAD INT, @SEXO NVARCHAR(50), @IDUSUARIO INT OUT)
AS
    DECLARE @IDPERFIL INT
	SELECT @IDUSUARIO = MAX(IDUSUARIO) FROM USUARIOS
	SELECT @IDPERFIL = MAX(IDPERFILUSUARIO) FROM PERFILUSUARIO
	IF @IDUSUARIO IS NULL
	BEGIN
	SET @IDUSUARIO = 1
	PRINT 'YES USU'
	END
	ELSE
	BEGIN 
	SET @IDUSUARIO = @IDUSUARIO + 1
	PRINT 'NO USU'
	END

	IF @IDPERFIL IS NULL
	BEGIN
	SET @IDPERFIL = 1
	PRINT 'YES PERFIL'
	END
	ELSE
	BEGIN 
	SET @IDPERFIL = @IDPERFIL + 1
	PRINT 'NO PERFIL'
	END
	INSERT INTO USUARIOS VALUES(@IDUSUARIO, @NOMBRE, @APELLIDOS, @DNI, @EMAIL, @PASSWORDENCRYPT, @SALT, @PASSWORD, @ROLE, 0)
	INSERT INTO PERFILUSUARIO VALUES(@IDPERFIL, @EDAD, @SEXO, @ALTURA, @PESO, @IDUSUARIO, 0, 0)
GO
/****** Object:  StoredProcedure [dbo].[SP_REGISTER_SOLICITUD]    Script Date: 23/03/2023 14:25:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SP_REGISTER_SOLICITUD]
(@SALT NVARCHAR(MAX), @CODIGO INT, @IDUSUARIO INT)
AS
    DECLARE @IDSOLICITUD INT

	SELECT @IDSOLICITUD = MAX(IDSOLICITUD) FROM SOLICITUD

	IF @IDSOLICITUD IS NULL
	BEGIN
		SET @IDSOLICITUD = 1
	END
	ELSE
	BEGIN
		SET @IDSOLICITUD = @IDSOLICITUD + 1
	END
	
	DELETE FROM SOLICITUD WHERE IDUSUARIO = @IDUSUARIO
	INSERT INTO SOLICITUD VALUES(@IDSOLICITUD, @SALT, @CODIGO, @IDUSUARIO)
GO
/****** Object:  StoredProcedure [dbo].[SP_REGISTER_USER]    Script Date: 23/03/2023 14:25:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE[dbo].[SP_REGISTER_USER]
(@NOMBRE NVARCHAR(50), @APELLIDOS NVARCHAR(50), @DNI NVARCHAR(50), @EMAIL NVARCHAR(50), @PASSWORDENCRYPT VARBINARY(MAX), 
@SALT NVARCHAR(50), @PASSWORD NVARCHAR(50), @ROLE NVARCHAR(50), @IDUSUARIO INT OUT)
AS
	SELECT @IDUSUARIO = MAX(IDUSUARIO) FROM USUARIOS
	IF @IDUSUARIO IS NULL
	BEGIN
	SET @IDUSUARIO = 1
	END
	ELSE
	BEGIN
	SET @IDUSUARIO = @IDUSUARIO + 1
	END
	INSERT INTO USUARIOS VALUES(@IDUSUARIO, @NOMBRE, @APELLIDOS, @DNI, @EMAIL, @PASSWORDENCRYPT, @SALT, @PASSWORD, @ROLE, 0)
GO
USE [master]
GO
ALTER DATABASE [FITLIFE] SET  READ_WRITE 
GO
