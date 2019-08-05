
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 02/07/2018 13:22:26
-- Generated from EDMX file: C:\Users\rdberb\Documents\Visual Studio 2017\Projects\Insight_BaseMonitor\Insight_BL\InsightModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Insight_DEV];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_JobJobStepLog]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[JobLogs] DROP CONSTRAINT [FK_JobJobStepLog];
GO
IF OBJECT_ID(N'[dbo].[FK_InstanceInstanceDetail]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[InstanceDetails] DROP CONSTRAINT [FK_InstanceInstanceDetail];
GO
IF OBJECT_ID(N'[dbo].[FK_InstanceDatabaseOption]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DatabaseOptions] DROP CONSTRAINT [FK_InstanceDatabaseOption];
GO
IF OBJECT_ID(N'[dbo].[FK_InstanceBackup]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Backups] DROP CONSTRAINT [FK_InstanceBackup];
GO
IF OBJECT_ID(N'[dbo].[FK_InstanceDrive]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Drives] DROP CONSTRAINT [FK_InstanceDrive];
GO
IF OBJECT_ID(N'[dbo].[FK_InstanceDatabaseFile]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DatabaseFiles] DROP CONSTRAINT [FK_InstanceDatabaseFile];
GO
IF OBJECT_ID(N'[dbo].[FK_InstanceDatabaseSize]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DatabaseSize] DROP CONSTRAINT [FK_InstanceDatabaseSize];
GO
IF OBJECT_ID(N'[dbo].[FK_InstanceUserMembership]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserMembership] DROP CONSTRAINT [FK_InstanceUserMembership];
GO
IF OBJECT_ID(N'[dbo].[FK_InstanceLoginMembership]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LoginMembership] DROP CONSTRAINT [FK_InstanceLoginMembership];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Instances]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Instances];
GO
IF OBJECT_ID(N'[dbo].[InstanceDetails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[InstanceDetails];
GO
IF OBJECT_ID(N'[dbo].[Jobs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Jobs];
GO
IF OBJECT_ID(N'[dbo].[JobLogs]', 'U') IS NOT NULL
    DROP TABLE [dbo].[JobLogs];
GO
IF OBJECT_ID(N'[dbo].[DatabaseOptions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DatabaseOptions];
GO
IF OBJECT_ID(N'[dbo].[Backups]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Backups];
GO
IF OBJECT_ID(N'[dbo].[Drives]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Drives];
GO
IF OBJECT_ID(N'[dbo].[DatabaseFiles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DatabaseFiles];
GO
IF OBJECT_ID(N'[dbo].[DatabaseSize]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DatabaseSize];
GO
IF OBJECT_ID(N'[dbo].[UserMembership]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserMembership];
GO
IF OBJECT_ID(N'[dbo].[LoginMembership]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LoginMembership];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Instances'
CREATE TABLE [dbo].[Instances] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(70)  NOT NULL,
    [Environment] char(3)  NOT NULL,
    [Active] bit  NOT NULL,
    [Habitat] varchar(30)  NOT NULL,
    [Comments] varchar(140)  NULL
);
GO

-- Creating table 'InstanceDetails'
CREATE TABLE [dbo].[InstanceDetails] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Edition] varchar(50)  NOT NULL,
    [Version] char(15)  NOT NULL,
    [ServicePack] char(3)  NOT NULL,
    [MachineType] varchar(10)  NULL,
    [AuthMode] bit  NOT NULL,
    [TcpPort] int  NOT NULL,
    [BackupCompression] bit  NULL,
    [PowerPlan] varchar(16)  NOT NULL,
    [MaxDOP] smallint  NOT NULL,
    [Xp_Cmdshell] bit  NOT NULL,
    [MaxServerMemory_MB] int  NOT NULL,
    [MinServerMemory_MB] int  NOT NULL,
    [ServerMemory_MB] bigint  NOT NULL,
    [CPU_Count] int  NOT NULL,
    [InstallDate] datetime  NOT NULL,
    [LastStartDate] datetime  NULL,
    [RunDate] datetime  NOT NULL,
    [RunCount] int  NOT NULL,
    [InstanceId] int  NOT NULL
);
GO

-- Creating table 'Jobs'
CREATE TABLE [dbo].[Jobs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(100)  NOT NULL,
    [Enabled] bit  NOT NULL,
    [RunDate] datetime  NULL,
    [RunCount] int  NULL
);
GO

-- Creating table 'JobLogs'
CREATE TABLE [dbo].[JobLogs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [RunDate] datetime  NOT NULL,
    [RunCount] int  NOT NULL,
    [JobId] int  NOT NULL,
    [Type] char(3)  NOT NULL,
    [Message] varchar(max)  NOT NULL
);
GO

-- Creating table 'DatabaseOptions'
CREATE TABLE [dbo].[DatabaseOptions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(100)  NOT NULL,
    [Compatibility] smallint  NOT NULL,
    [StateDesc] varchar(60)  NOT NULL,
    [UserAccessDesc] varchar(60)  NOT NULL,
    [RecoveryModel] varchar(60)  NOT NULL,
    [Collation] varchar(60)  NULL,
    [PageVerify] varchar(60)  NOT NULL,
    [ReadOnly] bit  NOT NULL,
    [AutoClose] bit  NOT NULL,
    [AutoShrink] bit  NOT NULL,
    [AutoCreateStats] bit  NOT NULL,
    [AutoUpdateStats] bit  NOT NULL,
    [FullText] bit  NOT NULL,
    [DbChaining] bit  NOT NULL,
    [Trustworthy] bit  NOT NULL,
    [Owner] varchar(80)  NOT NULL,
    [RunDate] datetime  NOT NULL,
    [RunCount] int  NOT NULL,
    [InstanceId] int  NOT NULL
);
GO

-- Creating table 'Backups'
CREATE TABLE [dbo].[Backups] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DBName] varchar(100)  NOT NULL,
    [TapeFull] datetime  NULL,
    [TapeDiff] datetime  NULL,
    [DiskFull] datetime  NULL,
    [DiskDiff] datetime  NULL,
    [DiskTlog] datetime  NULL,
    [RecoveryModel] varchar(60)  NOT NULL,
    [StateDesc] varchar(60)  NOT NULL,
    [Updateability] varchar(128)  NOT NULL,
    [RunDate] datetime  NOT NULL,
    [RunCount] int  NOT NULL,
    [InstanceId] int  NOT NULL
);
GO

-- Creating table 'Drives'
CREATE TABLE [dbo].[Drives] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DriveName] varchar(50)  NOT NULL,
    [Capacity_GB] int  NOT NULL,
    [Used_GB] int  NOT NULL,
    [Free_GB] int  NOT NULL,
    [PercentFree] int  NOT NULL,
    [RunDate] datetime  NOT NULL,
    [RunCount] int  NOT NULL,
    [InstanceId] int  NOT NULL
);
GO

-- Creating table 'DatabaseFiles'
CREATE TABLE [dbo].[DatabaseFiles] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DBName] varchar(100)  NOT NULL,
    [LogicalName] varchar(150)  NOT NULL,
    [Type] char(9)  NOT NULL,
    [Total_Space_MB] int  NOT NULL,
    [Used_Space_MB] decimal(18,2)  NOT NULL,
    [Free_Space_MB] decimal(18,2)  NOT NULL,
    [PercentUsed] decimal(18,2)  NOT NULL,
    [PhysicalName] varchar(255)  NOT NULL,
    [FileGroup] varchar(100)  NULL,
    [FileGrowth] varchar(50)  NOT NULL,
    [AutoGrowth] char(8)  NOT NULL,
    [RunDate] datetime  NOT NULL,
    [RunCount] int  NOT NULL,
    [InstanceId] int  NOT NULL
);
GO

-- Creating table 'DatabaseSize'
CREATE TABLE [dbo].[DatabaseSize] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(100)  NOT NULL,
    [Size] decimal(18,2)  NOT NULL,
    [Used_Space_MB] decimal(18,2)  NOT NULL,
    [Free_Space_MB] decimal(18,2)  NOT NULL,
    [PercentFree] decimal(18,2)  NOT NULL,
    [Updateability] varchar(128)  NOT NULL,
    [StateDesc] varchar(60)  NOT NULL,
    [RunDate] datetime  NOT NULL,
    [RunCount] int  NOT NULL,
    [InstanceId] int  NOT NULL
);
GO

-- Creating table 'UserMembership'
CREATE TABLE [dbo].[UserMembership] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DBName] varchar(100)  NOT NULL,
    [UserName] varchar(100)  NOT NULL,
    [GroupName] varchar(100)  NOT NULL,
    [LoginName] varchar(100)  NOT NULL,
    [Def_DBName] varchar(100)  NULL,
    [Def_SchemaName] varchar(100)  NULL,
    [RunDate] datetime  NOT NULL,
    [RunCount] int  NOT NULL,
    [InstanceId] int  NOT NULL
);
GO

-- Creating table 'LoginMembership'
CREATE TABLE [dbo].[LoginMembership] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [LoginName] varchar(100)  NOT NULL,
    [Type] char(13)  NOT NULL,
    [ServerRole] varchar(25)  NOT NULL,
    [Disabled] bit  NOT NULL,
    [Def_DBName] varchar(100)  NOT NULL,
    [RunDate] datetime  NOT NULL,
    [RunCount] int  NOT NULL,
    [InstanceId] int  NOT NULL
);
GO

-- Creating table 'LogSize'
CREATE TABLE [dbo].[LogSize] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DBName] varchar(100)  NOT NULL,
    [LogSize_MB] decimal(18,2)  NOT NULL,
    [PercentUsed] decimal(18,2)  NOT NULL,
    [RunDate] datetime  NOT NULL,
    [RunCount] int  NOT NULL,
    [InstanceId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Instances'
ALTER TABLE [dbo].[Instances]
ADD CONSTRAINT [PK_Instances]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'InstanceDetails'
ALTER TABLE [dbo].[InstanceDetails]
ADD CONSTRAINT [PK_InstanceDetails]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Jobs'
ALTER TABLE [dbo].[Jobs]
ADD CONSTRAINT [PK_Jobs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'JobLogs'
ALTER TABLE [dbo].[JobLogs]
ADD CONSTRAINT [PK_JobLogs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DatabaseOptions'
ALTER TABLE [dbo].[DatabaseOptions]
ADD CONSTRAINT [PK_DatabaseOptions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Backups'
ALTER TABLE [dbo].[Backups]
ADD CONSTRAINT [PK_Backups]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Drives'
ALTER TABLE [dbo].[Drives]
ADD CONSTRAINT [PK_Drives]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DatabaseFiles'
ALTER TABLE [dbo].[DatabaseFiles]
ADD CONSTRAINT [PK_DatabaseFiles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DatabaseSize'
ALTER TABLE [dbo].[DatabaseSize]
ADD CONSTRAINT [PK_DatabaseSize]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserMembership'
ALTER TABLE [dbo].[UserMembership]
ADD CONSTRAINT [PK_UserMembership]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'LoginMembership'
ALTER TABLE [dbo].[LoginMembership]
ADD CONSTRAINT [PK_LoginMembership]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'LogSize'
ALTER TABLE [dbo].[LogSize]
ADD CONSTRAINT [PK_LogSize]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [JobId] in table 'JobLogs'
ALTER TABLE [dbo].[JobLogs]
ADD CONSTRAINT [FK_JobJobStepLog]
    FOREIGN KEY ([JobId])
    REFERENCES [dbo].[Jobs]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_JobJobStepLog'
CREATE INDEX [IX_FK_JobJobStepLog]
ON [dbo].[JobLogs]
    ([JobId]);
GO

-- Creating foreign key on [InstanceId] in table 'InstanceDetails'
ALTER TABLE [dbo].[InstanceDetails]
ADD CONSTRAINT [FK_InstanceInstanceDetail]
    FOREIGN KEY ([InstanceId])
    REFERENCES [dbo].[Instances]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_InstanceInstanceDetail'
CREATE INDEX [IX_FK_InstanceInstanceDetail]
ON [dbo].[InstanceDetails]
    ([InstanceId]);
GO

-- Creating foreign key on [InstanceId] in table 'DatabaseOptions'
ALTER TABLE [dbo].[DatabaseOptions]
ADD CONSTRAINT [FK_InstanceDatabaseOption]
    FOREIGN KEY ([InstanceId])
    REFERENCES [dbo].[Instances]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_InstanceDatabaseOption'
CREATE INDEX [IX_FK_InstanceDatabaseOption]
ON [dbo].[DatabaseOptions]
    ([InstanceId]);
GO

-- Creating foreign key on [InstanceId] in table 'Backups'
ALTER TABLE [dbo].[Backups]
ADD CONSTRAINT [FK_InstanceBackup]
    FOREIGN KEY ([InstanceId])
    REFERENCES [dbo].[Instances]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_InstanceBackup'
CREATE INDEX [IX_FK_InstanceBackup]
ON [dbo].[Backups]
    ([InstanceId]);
GO

-- Creating foreign key on [InstanceId] in table 'Drives'
ALTER TABLE [dbo].[Drives]
ADD CONSTRAINT [FK_InstanceDrive]
    FOREIGN KEY ([InstanceId])
    REFERENCES [dbo].[Instances]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_InstanceDrive'
CREATE INDEX [IX_FK_InstanceDrive]
ON [dbo].[Drives]
    ([InstanceId]);
GO

-- Creating foreign key on [InstanceId] in table 'DatabaseFiles'
ALTER TABLE [dbo].[DatabaseFiles]
ADD CONSTRAINT [FK_InstanceDatabaseFile]
    FOREIGN KEY ([InstanceId])
    REFERENCES [dbo].[Instances]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_InstanceDatabaseFile'
CREATE INDEX [IX_FK_InstanceDatabaseFile]
ON [dbo].[DatabaseFiles]
    ([InstanceId]);
GO

-- Creating foreign key on [InstanceId] in table 'DatabaseSize'
ALTER TABLE [dbo].[DatabaseSize]
ADD CONSTRAINT [FK_InstanceDatabaseSize]
    FOREIGN KEY ([InstanceId])
    REFERENCES [dbo].[Instances]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_InstanceDatabaseSize'
CREATE INDEX [IX_FK_InstanceDatabaseSize]
ON [dbo].[DatabaseSize]
    ([InstanceId]);
GO

-- Creating foreign key on [InstanceId] in table 'UserMembership'
ALTER TABLE [dbo].[UserMembership]
ADD CONSTRAINT [FK_InstanceUserMembership]
    FOREIGN KEY ([InstanceId])
    REFERENCES [dbo].[Instances]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_InstanceUserMembership'
CREATE INDEX [IX_FK_InstanceUserMembership]
ON [dbo].[UserMembership]
    ([InstanceId]);
GO

-- Creating foreign key on [InstanceId] in table 'LoginMembership'
ALTER TABLE [dbo].[LoginMembership]
ADD CONSTRAINT [FK_InstanceLoginMembership]
    FOREIGN KEY ([InstanceId])
    REFERENCES [dbo].[Instances]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_InstanceLoginMembership'
CREATE INDEX [IX_FK_InstanceLoginMembership]
ON [dbo].[LoginMembership]
    ([InstanceId]);
GO

-- Creating foreign key on [InstanceId] in table 'LogSize'
ALTER TABLE [dbo].[LogSize]
ADD CONSTRAINT [FK_InstanceLogSize]
    FOREIGN KEY ([InstanceId])
    REFERENCES [dbo].[Instances]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_InstanceLogSize'
CREATE INDEX [IX_FK_InstanceLogSize]
ON [dbo].[LogSize]
    ([InstanceId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------