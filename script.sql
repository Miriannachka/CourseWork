USE [master]
GO

/****** Object:  Database [forum_with_cooking_recipes]    Script Date: 07.05.2025 9:34:17 ******/
CREATE DATABASE [forum_with_cooking_recipes]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'forum_with_cooking_recipes', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\forum_with_cooking_recipes.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'forum_with_cooking_recipes_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\forum_with_cooking_recipes_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [forum_with_cooking_recipes].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [forum_with_cooking_recipes] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [forum_with_cooking_recipes] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [forum_with_cooking_recipes] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [forum_with_cooking_recipes] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [forum_with_cooking_recipes] SET ARITHABORT OFF 
GO

ALTER DATABASE [forum_with_cooking_recipes] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [forum_with_cooking_recipes] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [forum_with_cooking_recipes] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [forum_with_cooking_recipes] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [forum_with_cooking_recipes] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [forum_with_cooking_recipes] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [forum_with_cooking_recipes] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [forum_with_cooking_recipes] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [forum_with_cooking_recipes] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [forum_with_cooking_recipes] SET  DISABLE_BROKER 
GO

ALTER DATABASE [forum_with_cooking_recipes] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [forum_with_cooking_recipes] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [forum_with_cooking_recipes] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [forum_with_cooking_recipes] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [forum_with_cooking_recipes] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [forum_with_cooking_recipes] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [forum_with_cooking_recipes] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [forum_with_cooking_recipes] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [forum_with_cooking_recipes] SET  MULTI_USER 
GO

ALTER DATABASE [forum_with_cooking_recipes] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [forum_with_cooking_recipes] SET DB_CHAINING OFF 
GO

ALTER DATABASE [forum_with_cooking_recipes] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [forum_with_cooking_recipes] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [forum_with_cooking_recipes] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [forum_with_cooking_recipes] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO

ALTER DATABASE [forum_with_cooking_recipes] SET QUERY_STORE = ON
GO

ALTER DATABASE [forum_with_cooking_recipes] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO

ALTER DATABASE [forum_with_cooking_recipes] SET  READ_WRITE 
GO

