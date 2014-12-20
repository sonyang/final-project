
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/20/2014 11:45:20
-- Generated from EDMX file: C:\Users\ddrake\Desktop\final-project\BookReview\Models\Data\BookReviews.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [FinalProject];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Review_BookID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Review] DROP CONSTRAINT [FK_Review_BookID];
GO
IF OBJECT_ID(N'[dbo].[FK_Review_UserID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Review] DROP CONSTRAINT [FK_Review_UserID];
GO
IF OBJECT_ID(N'[dbo].[FK_UserGroup_Group]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserGroup] DROP CONSTRAINT [FK_UserGroup_Group];
GO
IF OBJECT_ID(N'[dbo].[FK_UserGroup_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserGroup] DROP CONSTRAINT [FK_UserGroup_User];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Book]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Book];
GO
IF OBJECT_ID(N'[dbo].[Group]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Group];
GO
IF OBJECT_ID(N'[dbo].[Review]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Review];
GO
IF OBJECT_ID(N'[dbo].[User]', 'U') IS NOT NULL
    DROP TABLE [dbo].[User];
GO
IF OBJECT_ID(N'[dbo].[UserGroup]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserGroup];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Books'
CREATE TABLE [dbo].[Books] (
    [ID] uniqueidentifier  NOT NULL,
    [Title] nvarchar(50)  NOT NULL,
    [Category] nvarchar(50)  NOT NULL,
    [Publisher] nvarchar(50)  NOT NULL,
    [Author] nvarchar(50)  NOT NULL,
    [ISBN10] nchar(10)  NULL,
    [ISBN13] nchar(13)  NULL,
    [CoverArt] nvarchar(256)  NULL,
    [PageCount] int  NOT NULL,
    [Summary] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Groups'
CREATE TABLE [dbo].[Groups] (
    [ID] uniqueidentifier  NOT NULL,
    [Name] nvarchar(30)  NOT NULL
);
GO

-- Creating table 'Reviews'
CREATE TABLE [dbo].[Reviews] (
    [ID] uniqueidentifier  NOT NULL,
    [BookID] uniqueidentifier  NOT NULL,
    [UserID] uniqueidentifier  NOT NULL,
    [Subject] nvarchar(50)  NOT NULL,
    [Body] nvarchar(max)  NOT NULL,
    [Rating] decimal(2,1)  NOT NULL,
    [Date] datetime  NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [ID] uniqueidentifier  NOT NULL,
    [Username] nvarchar(100)  NOT NULL,
    [Password] nchar(40)  NOT NULL,
    [FirstName] nvarchar(25)  NOT NULL,
    [LastName] nvarchar(25)  NOT NULL
);
GO

-- Creating table 'UserGroup'
CREATE TABLE [dbo].[UserGroup] (
    [Groups_ID] uniqueidentifier  NOT NULL,
    [Users_ID] uniqueidentifier  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'Books'
ALTER TABLE [dbo].[Books]
ADD CONSTRAINT [PK_Books]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Groups'
ALTER TABLE [dbo].[Groups]
ADD CONSTRAINT [PK_Groups]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Reviews'
ALTER TABLE [dbo].[Reviews]
ADD CONSTRAINT [PK_Reviews]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [Groups_ID], [Users_ID] in table 'UserGroup'
ALTER TABLE [dbo].[UserGroup]
ADD CONSTRAINT [PK_UserGroup]
    PRIMARY KEY CLUSTERED ([Groups_ID], [Users_ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [BookID] in table 'Reviews'
ALTER TABLE [dbo].[Reviews]
ADD CONSTRAINT [FK_Review_BookID]
    FOREIGN KEY ([BookID])
    REFERENCES [dbo].[Books]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Review_BookID'
CREATE INDEX [IX_FK_Review_BookID]
ON [dbo].[Reviews]
    ([BookID]);
GO

-- Creating foreign key on [UserID] in table 'Reviews'
ALTER TABLE [dbo].[Reviews]
ADD CONSTRAINT [FK_Review_UserID]
    FOREIGN KEY ([UserID])
    REFERENCES [dbo].[Users]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Review_UserID'
CREATE INDEX [IX_FK_Review_UserID]
ON [dbo].[Reviews]
    ([UserID]);
GO

-- Creating foreign key on [Groups_ID] in table 'UserGroup'
ALTER TABLE [dbo].[UserGroup]
ADD CONSTRAINT [FK_UserGroup_Group]
    FOREIGN KEY ([Groups_ID])
    REFERENCES [dbo].[Groups]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Users_ID] in table 'UserGroup'
ALTER TABLE [dbo].[UserGroup]
ADD CONSTRAINT [FK_UserGroup_User]
    FOREIGN KEY ([Users_ID])
    REFERENCES [dbo].[Users]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserGroup_User'
CREATE INDEX [IX_FK_UserGroup_User]
ON [dbo].[UserGroup]
    ([Users_ID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------