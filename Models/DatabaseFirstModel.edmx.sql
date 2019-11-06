
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/06/2019 12:05:29
-- Generated from EDMX file: E:\MEGASync\Engineering\Application\repos\Bikevision\Models\DatabaseFirstModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [bikewayDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[fk_Customer_AspNetUser1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Customer] DROP CONSTRAINT [fk_Customer_AspNetUser1];
GO
IF OBJECT_ID(N'[dbo].[fk_Customer_Locality1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Customer] DROP CONSTRAINT [fk_Customer_Locality1];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserClaims] DROP CONSTRAINT [FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserLogins] DROP CONSTRAINT [FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserRoles_dbo_AspNetRoles_RoleId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_dbo_AspNetUserRoles_dbo_AspNetRoles_RoleId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_AspNetUserRoles_dbo_AspNetUsers_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AspNetUserRoles] DROP CONSTRAINT [FK_dbo_AspNetUserRoles_dbo_AspNetUsers_UserId];
GO
IF OBJECT_ID(N'[dbo].[fk_EmployeeInThePosition_Employee1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmployeeInThePosition] DROP CONSTRAINT [fk_EmployeeInThePosition_Employee1];
GO
IF OBJECT_ID(N'[dbo].[fk_EmployeeInThePosition_Position1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmployeeInThePosition] DROP CONSTRAINT [fk_EmployeeInThePosition_Position1];
GO
IF OBJECT_ID(N'[dbo].[fk_FeatureOfItem_Feature1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FeatureValueOfItem] DROP CONSTRAINT [fk_FeatureOfItem_Feature1];
GO
IF OBJECT_ID(N'[dbo].[fk_FeatureOfItem_FeatureValue1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FeatureValueOfItem] DROP CONSTRAINT [fk_FeatureOfItem_FeatureValue1];
GO
IF OBJECT_ID(N'[dbo].[fk_FeatureOfItem_Item1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FeatureValueOfItem] DROP CONSTRAINT [fk_FeatureOfItem_Item1];
GO
IF OBJECT_ID(N'[dbo].[fk_Item_Brand1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Item] DROP CONSTRAINT [fk_Item_Brand1];
GO
IF OBJECT_ID(N'[dbo].[fk_Item_Category1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Item] DROP CONSTRAINT [fk_Item_Category1];
GO
IF OBJECT_ID(N'[dbo].[fk_Item_ItemType1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Item] DROP CONSTRAINT [fk_Item_ItemType1];
GO
IF OBJECT_ID(N'[dbo].[fk_Opinion_Customer1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Opinion] DROP CONSTRAINT [fk_Opinion_Customer1];
GO
IF OBJECT_ID(N'[dbo].[fk_Opinion_Item1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Opinion] DROP CONSTRAINT [fk_Opinion_Item1];
GO
IF OBJECT_ID(N'[dbo].[fk_Sale_Customer1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Sale] DROP CONSTRAINT [fk_Sale_Customer1];
GO
IF OBJECT_ID(N'[dbo].[fk_Sale_Employee1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Sale] DROP CONSTRAINT [fk_Sale_Employee1];
GO
IF OBJECT_ID(N'[dbo].[fk_Sale_SaleType1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Sale] DROP CONSTRAINT [fk_Sale_SaleType1];
GO
IF OBJECT_ID(N'[dbo].[fk_SaleDetails_Item1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SaleDetails] DROP CONSTRAINT [fk_SaleDetails_Item1];
GO
IF OBJECT_ID(N'[dbo].[fk_SaleDetails_Sale1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SaleDetails] DROP CONSTRAINT [fk_SaleDetails_Sale1];
GO
IF OBJECT_ID(N'[dbo].[fk_Service_Customer1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Service] DROP CONSTRAINT [fk_Service_Customer1];
GO
IF OBJECT_ID(N'[dbo].[fk_Service_Employee1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Service] DROP CONSTRAINT [fk_Service_Employee1];
GO
IF OBJECT_ID(N'[dbo].[fk_Service_ServiceType1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Service] DROP CONSTRAINT [fk_Service_ServiceType1];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[__MigrationHistory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[__MigrationHistory];
GO
IF OBJECT_ID(N'[dbo].[AspNetRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetRoles];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserClaims]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserClaims];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserLogins]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserLogins];
GO
IF OBJECT_ID(N'[dbo].[AspNetUserRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUserRoles];
GO
IF OBJECT_ID(N'[dbo].[AspNetUsers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AspNetUsers];
GO
IF OBJECT_ID(N'[dbo].[Brand]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Brand];
GO
IF OBJECT_ID(N'[dbo].[Category]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Category];
GO
IF OBJECT_ID(N'[dbo].[Customer]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Customer];
GO
IF OBJECT_ID(N'[dbo].[Employee]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Employee];
GO
IF OBJECT_ID(N'[dbo].[EmployeeInThePosition]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EmployeeInThePosition];
GO
IF OBJECT_ID(N'[dbo].[Feature]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Feature];
GO
IF OBJECT_ID(N'[dbo].[FeatureValue]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FeatureValue];
GO
IF OBJECT_ID(N'[dbo].[FeatureValueOfItem]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FeatureValueOfItem];
GO
IF OBJECT_ID(N'[dbo].[Item]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Item];
GO
IF OBJECT_ID(N'[dbo].[ItemType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ItemType];
GO
IF OBJECT_ID(N'[dbo].[Locality]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Locality];
GO
IF OBJECT_ID(N'[dbo].[Opinion]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Opinion];
GO
IF OBJECT_ID(N'[dbo].[Position]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Position];
GO
IF OBJECT_ID(N'[dbo].[Sale]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sale];
GO
IF OBJECT_ID(N'[dbo].[SaleDetails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SaleDetails];
GO
IF OBJECT_ID(N'[dbo].[SaleType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SaleType];
GO
IF OBJECT_ID(N'[dbo].[Service]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Service];
GO
IF OBJECT_ID(N'[dbo].[ServiceType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ServiceType];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Categories'
CREATE TABLE [dbo].[Categories] (
    [idCategory] int IDENTITY(1,1) NOT NULL,
    [category1] varchar(45)  NOT NULL
);
GO

-- Creating table 'Employees'
CREATE TABLE [dbo].[Employees] (
    [idEmployee] int IDENTITY(1,1) NOT NULL,
    [name] varchar(45)  NOT NULL,
    [surname] varchar(45)  NOT NULL,
    [AspNetUser_idAspNetUser] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'EmployeeInThePositions'
CREATE TABLE [dbo].[EmployeeInThePositions] (
    [dateOfEmployment] datetime  NOT NULL,
    [Employee_idEmployee] int  NOT NULL,
    [Position_idPosition] int  NOT NULL
);
GO

-- Creating table 'ItemTypes'
CREATE TABLE [dbo].[ItemTypes] (
    [idItemType] int IDENTITY(1,1) NOT NULL,
    [type] varchar(45)  NOT NULL
);
GO

-- Creating table 'Localities'
CREATE TABLE [dbo].[Localities] (
    [idLocality] int IDENTITY(1,1) NOT NULL,
    [locality1] varchar(45)  NOT NULL
);
GO

-- Creating table 'Positions'
CREATE TABLE [dbo].[Positions] (
    [idPosition] int IDENTITY(1,1) NOT NULL,
    [name] varchar(45)  NOT NULL
);
GO

-- Creating table 'SaleDetails'
CREATE TABLE [dbo].[SaleDetails] (
    [value] decimal(10,0)  NOT NULL,
    [quantity] int  NOT NULL,
    [Sale_idSale] int  NOT NULL,
    [Item_idItem] int  NOT NULL
);
GO

-- Creating table 'SaleTypes'
CREATE TABLE [dbo].[SaleTypes] (
    [idSaleType] int IDENTITY(1,1) NOT NULL,
    [type] varchar(45)  NOT NULL
);
GO

-- Creating table 'Services'
CREATE TABLE [dbo].[Services] (
    [idService] int IDENTITY(1,1) NOT NULL,
    [title] varchar(45)  NOT NULL,
    [price] decimal(10,0)  NOT NULL,
    [dateOfEmployment] datetime  NOT NULL,
    [description] varchar(max)  NULL,
    [dateOfCompletion] datetime  NULL,
    [Customer_idCustomer] int  NOT NULL,
    [ServiceType_idServiceType] int  NOT NULL,
    [Employee_idEmployee] int  NOT NULL
);
GO

-- Creating table 'ServiceTypes'
CREATE TABLE [dbo].[ServiceTypes] (
    [idServiceType] int IDENTITY(1,1) NOT NULL,
    [type] varchar(45)  NOT NULL
);
GO

-- Creating table 'C__MigrationHistory'
CREATE TABLE [dbo].[C__MigrationHistory] (
    [MigrationId] nvarchar(150)  NOT NULL,
    [ContextKey] nvarchar(300)  NOT NULL,
    [Model] varbinary(max)  NOT NULL,
    [ProductVersion] nvarchar(32)  NOT NULL
);
GO

-- Creating table 'AspNetRoles'
CREATE TABLE [dbo].[AspNetRoles] (
    [Id] nvarchar(128)  NOT NULL,
    [Name] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'AspNetUserClaims'
CREATE TABLE [dbo].[AspNetUserClaims] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] nvarchar(128)  NOT NULL,
    [ClaimType] nvarchar(max)  NULL,
    [ClaimValue] nvarchar(max)  NULL
);
GO

-- Creating table 'AspNetUserLogins'
CREATE TABLE [dbo].[AspNetUserLogins] (
    [LoginProvider] nvarchar(128)  NOT NULL,
    [ProviderKey] nvarchar(128)  NOT NULL,
    [UserId] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'AspNetUsers'
CREATE TABLE [dbo].[AspNetUsers] (
    [Id] nvarchar(128)  NOT NULL,
    [Email] nvarchar(256)  NULL,
    [EmailConfirmed] bit  NOT NULL,
    [PasswordHash] nvarchar(max)  NULL,
    [SecurityStamp] nvarchar(max)  NULL,
    [PhoneNumber] nvarchar(max)  NULL,
    [PhoneNumberConfirmed] bit  NOT NULL,
    [TwoFactorEnabled] bit  NOT NULL,
    [LockoutEndDateUtc] datetime  NULL,
    [LockoutEnabled] bit  NOT NULL,
    [AccessFailedCount] int  NOT NULL,
    [UserName] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'Brands'
CREATE TABLE [dbo].[Brands] (
    [idBrand] int IDENTITY(1,1) NOT NULL,
    [Brand1] varchar(45)  NOT NULL
);
GO

-- Creating table 'Customers'
CREATE TABLE [dbo].[Customers] (
    [idCustomer] int IDENTITY(1,1) NOT NULL,
    [name] varchar(30)  NOT NULL,
    [surname] varchar(50)  NOT NULL,
    [telephoneNumber] bigint  NULL,
    [emailAddress] varchar(50)  NULL,
    [addressOfResidence] varchar(100)  NULL,
    [zipCode] varchar(6)  NULL,
    [Locality_idLocality] int  NOT NULL,
    [AspNetUser_idAspNetUser] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'Features'
CREATE TABLE [dbo].[Features] (
    [idFeature] int IDENTITY(1,1) NOT NULL,
    [feature1] varchar(45)  NOT NULL
);
GO

-- Creating table 'FeatureValues'
CREATE TABLE [dbo].[FeatureValues] (
    [idFeatureValue] int IDENTITY(1,1) NOT NULL,
    [featureValue1] varchar(45)  NOT NULL
);
GO

-- Creating table 'FeatureValueOfItems'
CREATE TABLE [dbo].[FeatureValueOfItems] (
    [Feature_idFeature1] int  NOT NULL,
    [Item_idItem1] int  NOT NULL,
    [FeatureValue_idFeatureValue] int  NOT NULL
);
GO

-- Creating table 'Items'
CREATE TABLE [dbo].[Items] (
    [idItem] int IDENTITY(1,1) NOT NULL,
    [itemName] varchar(45)  NOT NULL,
    [itemDescription] varchar(max)  NOT NULL,
    [avability] int  NOT NULL,
    [price] decimal(10,0)  NOT NULL,
    [discount] int  NULL,
    [outlet] smallint  NULL,
    [weight] float  NULL,
    [dimensions] varchar(20)  NULL,
    [ItemType_idItemType] int  NOT NULL,
    [Category_idCategory] int  NOT NULL,
    [Brand_idBrand] int  NOT NULL
);
GO

-- Creating table 'Opinions'
CREATE TABLE [dbo].[Opinions] (
    [idOpinion] int IDENTITY(1,1) NOT NULL,
    [points] char(1)  NOT NULL,
    [date] datetime  NOT NULL,
    [opinion1] varchar(max)  NULL,
    [Customer_idCustomer] int  NOT NULL,
    [Item_idItem] int  NOT NULL
);
GO

-- Creating table 'Sales'
CREATE TABLE [dbo].[Sales] (
    [idSale] int IDENTITY(1,1) NOT NULL,
    [date] datetime  NOT NULL,
    [Customer_idCustomer] int  NOT NULL,
    [SaleType_idSaleType] int  NOT NULL,
    [Employee_idEmployee] int  NOT NULL
);
GO

-- Creating table 'AspNetUserRoles'
CREATE TABLE [dbo].[AspNetUserRoles] (
    [AspNetRoles_Id] nvarchar(128)  NOT NULL,
    [AspNetUsers_Id] nvarchar(128)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [idCategory] in table 'Categories'
ALTER TABLE [dbo].[Categories]
ADD CONSTRAINT [PK_Categories]
    PRIMARY KEY CLUSTERED ([idCategory] ASC);
GO

-- Creating primary key on [idEmployee] in table 'Employees'
ALTER TABLE [dbo].[Employees]
ADD CONSTRAINT [PK_Employees]
    PRIMARY KEY CLUSTERED ([idEmployee] ASC);
GO

-- Creating primary key on [Employee_idEmployee], [Position_idPosition] in table 'EmployeeInThePositions'
ALTER TABLE [dbo].[EmployeeInThePositions]
ADD CONSTRAINT [PK_EmployeeInThePositions]
    PRIMARY KEY CLUSTERED ([Employee_idEmployee], [Position_idPosition] ASC);
GO

-- Creating primary key on [idItemType] in table 'ItemTypes'
ALTER TABLE [dbo].[ItemTypes]
ADD CONSTRAINT [PK_ItemTypes]
    PRIMARY KEY CLUSTERED ([idItemType] ASC);
GO

-- Creating primary key on [idLocality] in table 'Localities'
ALTER TABLE [dbo].[Localities]
ADD CONSTRAINT [PK_Localities]
    PRIMARY KEY CLUSTERED ([idLocality] ASC);
GO

-- Creating primary key on [idPosition] in table 'Positions'
ALTER TABLE [dbo].[Positions]
ADD CONSTRAINT [PK_Positions]
    PRIMARY KEY CLUSTERED ([idPosition] ASC);
GO

-- Creating primary key on [Sale_idSale], [Item_idItem] in table 'SaleDetails'
ALTER TABLE [dbo].[SaleDetails]
ADD CONSTRAINT [PK_SaleDetails]
    PRIMARY KEY CLUSTERED ([Sale_idSale], [Item_idItem] ASC);
GO

-- Creating primary key on [idSaleType] in table 'SaleTypes'
ALTER TABLE [dbo].[SaleTypes]
ADD CONSTRAINT [PK_SaleTypes]
    PRIMARY KEY CLUSTERED ([idSaleType] ASC);
GO

-- Creating primary key on [idService] in table 'Services'
ALTER TABLE [dbo].[Services]
ADD CONSTRAINT [PK_Services]
    PRIMARY KEY CLUSTERED ([idService] ASC);
GO

-- Creating primary key on [idServiceType] in table 'ServiceTypes'
ALTER TABLE [dbo].[ServiceTypes]
ADD CONSTRAINT [PK_ServiceTypes]
    PRIMARY KEY CLUSTERED ([idServiceType] ASC);
GO

-- Creating primary key on [MigrationId], [ContextKey] in table 'C__MigrationHistory'
ALTER TABLE [dbo].[C__MigrationHistory]
ADD CONSTRAINT [PK_C__MigrationHistory]
    PRIMARY KEY CLUSTERED ([MigrationId], [ContextKey] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetRoles'
ALTER TABLE [dbo].[AspNetRoles]
ADD CONSTRAINT [PK_AspNetRoles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetUserClaims'
ALTER TABLE [dbo].[AspNetUserClaims]
ADD CONSTRAINT [PK_AspNetUserClaims]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [LoginProvider], [ProviderKey], [UserId] in table 'AspNetUserLogins'
ALTER TABLE [dbo].[AspNetUserLogins]
ADD CONSTRAINT [PK_AspNetUserLogins]
    PRIMARY KEY CLUSTERED ([LoginProvider], [ProviderKey], [UserId] ASC);
GO

-- Creating primary key on [Id] in table 'AspNetUsers'
ALTER TABLE [dbo].[AspNetUsers]
ADD CONSTRAINT [PK_AspNetUsers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [idBrand] in table 'Brands'
ALTER TABLE [dbo].[Brands]
ADD CONSTRAINT [PK_Brands]
    PRIMARY KEY CLUSTERED ([idBrand] ASC);
GO

-- Creating primary key on [idCustomer] in table 'Customers'
ALTER TABLE [dbo].[Customers]
ADD CONSTRAINT [PK_Customers]
    PRIMARY KEY CLUSTERED ([idCustomer] ASC);
GO

-- Creating primary key on [idFeature] in table 'Features'
ALTER TABLE [dbo].[Features]
ADD CONSTRAINT [PK_Features]
    PRIMARY KEY CLUSTERED ([idFeature] ASC);
GO

-- Creating primary key on [idFeatureValue] in table 'FeatureValues'
ALTER TABLE [dbo].[FeatureValues]
ADD CONSTRAINT [PK_FeatureValues]
    PRIMARY KEY CLUSTERED ([idFeatureValue] ASC);
GO

-- Creating primary key on [Feature_idFeature1], [Item_idItem1], [FeatureValue_idFeatureValue] in table 'FeatureValueOfItems'
ALTER TABLE [dbo].[FeatureValueOfItems]
ADD CONSTRAINT [PK_FeatureValueOfItems]
    PRIMARY KEY CLUSTERED ([Feature_idFeature1], [Item_idItem1], [FeatureValue_idFeatureValue] ASC);
GO

-- Creating primary key on [idItem] in table 'Items'
ALTER TABLE [dbo].[Items]
ADD CONSTRAINT [PK_Items]
    PRIMARY KEY CLUSTERED ([idItem] ASC);
GO

-- Creating primary key on [idOpinion] in table 'Opinions'
ALTER TABLE [dbo].[Opinions]
ADD CONSTRAINT [PK_Opinions]
    PRIMARY KEY CLUSTERED ([idOpinion] ASC);
GO

-- Creating primary key on [idSale] in table 'Sales'
ALTER TABLE [dbo].[Sales]
ADD CONSTRAINT [PK_Sales]
    PRIMARY KEY CLUSTERED ([idSale] ASC);
GO

-- Creating primary key on [AspNetRoles_Id], [AspNetUsers_Id] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [PK_AspNetUserRoles]
    PRIMARY KEY CLUSTERED ([AspNetRoles_Id], [AspNetUsers_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Employee_idEmployee] in table 'EmployeeInThePositions'
ALTER TABLE [dbo].[EmployeeInThePositions]
ADD CONSTRAINT [fk_EmployeeInThePosition_Employee1]
    FOREIGN KEY ([Employee_idEmployee])
    REFERENCES [dbo].[Employees]
        ([idEmployee])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Employee_idEmployee] in table 'Services'
ALTER TABLE [dbo].[Services]
ADD CONSTRAINT [fk_Service_Employee1]
    FOREIGN KEY ([Employee_idEmployee])
    REFERENCES [dbo].[Employees]
        ([idEmployee])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_Service_Employee1'
CREATE INDEX [IX_fk_Service_Employee1]
ON [dbo].[Services]
    ([Employee_idEmployee]);
GO

-- Creating foreign key on [Position_idPosition] in table 'EmployeeInThePositions'
ALTER TABLE [dbo].[EmployeeInThePositions]
ADD CONSTRAINT [fk_EmployeeInThePosition_Position1]
    FOREIGN KEY ([Position_idPosition])
    REFERENCES [dbo].[Positions]
        ([idPosition])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_EmployeeInThePosition_Position1'
CREATE INDEX [IX_fk_EmployeeInThePosition_Position1]
ON [dbo].[EmployeeInThePositions]
    ([Position_idPosition]);
GO

-- Creating foreign key on [ServiceType_idServiceType] in table 'Services'
ALTER TABLE [dbo].[Services]
ADD CONSTRAINT [fk_Service_ServiceType1]
    FOREIGN KEY ([ServiceType_idServiceType])
    REFERENCES [dbo].[ServiceTypes]
        ([idServiceType])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_Service_ServiceType1'
CREATE INDEX [IX_fk_Service_ServiceType1]
ON [dbo].[Services]
    ([ServiceType_idServiceType]);
GO

-- Creating foreign key on [UserId] in table 'AspNetUserClaims'
ALTER TABLE [dbo].[AspNetUserClaims]
ADD CONSTRAINT [FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId'
CREATE INDEX [IX_FK_dbo_AspNetUserClaims_dbo_AspNetUsers_UserId]
ON [dbo].[AspNetUserClaims]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'AspNetUserLogins'
ALTER TABLE [dbo].[AspNetUserLogins]
ADD CONSTRAINT [FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId'
CREATE INDEX [IX_FK_dbo_AspNetUserLogins_dbo_AspNetUsers_UserId]
ON [dbo].[AspNetUserLogins]
    ([UserId]);
GO

-- Creating foreign key on [AspNetRoles_Id] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [FK_AspNetUserRoles_AspNetRole]
    FOREIGN KEY ([AspNetRoles_Id])
    REFERENCES [dbo].[AspNetRoles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [AspNetUsers_Id] in table 'AspNetUserRoles'
ALTER TABLE [dbo].[AspNetUserRoles]
ADD CONSTRAINT [FK_AspNetUserRoles_AspNetUser]
    FOREIGN KEY ([AspNetUsers_Id])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AspNetUserRoles_AspNetUser'
CREATE INDEX [IX_FK_AspNetUserRoles_AspNetUser]
ON [dbo].[AspNetUserRoles]
    ([AspNetUsers_Id]);
GO

-- Creating foreign key on [AspNetUser_idAspNetUser] in table 'Customers'
ALTER TABLE [dbo].[Customers]
ADD CONSTRAINT [fk_Customer_AspNetUser1]
    FOREIGN KEY ([AspNetUser_idAspNetUser])
    REFERENCES [dbo].[AspNetUsers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_Customer_AspNetUser1'
CREATE INDEX [IX_fk_Customer_AspNetUser1]
ON [dbo].[Customers]
    ([AspNetUser_idAspNetUser]);
GO

-- Creating foreign key on [Brand_idBrand] in table 'Items'
ALTER TABLE [dbo].[Items]
ADD CONSTRAINT [fk_Item_Brand1]
    FOREIGN KEY ([Brand_idBrand])
    REFERENCES [dbo].[Brands]
        ([idBrand])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_Item_Brand1'
CREATE INDEX [IX_fk_Item_Brand1]
ON [dbo].[Items]
    ([Brand_idBrand]);
GO

-- Creating foreign key on [Category_idCategory] in table 'Items'
ALTER TABLE [dbo].[Items]
ADD CONSTRAINT [fk_Item_Category1]
    FOREIGN KEY ([Category_idCategory])
    REFERENCES [dbo].[Categories]
        ([idCategory])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_Item_Category1'
CREATE INDEX [IX_fk_Item_Category1]
ON [dbo].[Items]
    ([Category_idCategory]);
GO

-- Creating foreign key on [Locality_idLocality] in table 'Customers'
ALTER TABLE [dbo].[Customers]
ADD CONSTRAINT [fk_Customer_Locality1]
    FOREIGN KEY ([Locality_idLocality])
    REFERENCES [dbo].[Localities]
        ([idLocality])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_Customer_Locality1'
CREATE INDEX [IX_fk_Customer_Locality1]
ON [dbo].[Customers]
    ([Locality_idLocality]);
GO

-- Creating foreign key on [Customer_idCustomer] in table 'Opinions'
ALTER TABLE [dbo].[Opinions]
ADD CONSTRAINT [fk_Opinion_Customer1]
    FOREIGN KEY ([Customer_idCustomer])
    REFERENCES [dbo].[Customers]
        ([idCustomer])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_Opinion_Customer1'
CREATE INDEX [IX_fk_Opinion_Customer1]
ON [dbo].[Opinions]
    ([Customer_idCustomer]);
GO

-- Creating foreign key on [Customer_idCustomer] in table 'Services'
ALTER TABLE [dbo].[Services]
ADD CONSTRAINT [fk_Service_Customer1]
    FOREIGN KEY ([Customer_idCustomer])
    REFERENCES [dbo].[Customers]
        ([idCustomer])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_Service_Customer1'
CREATE INDEX [IX_fk_Service_Customer1]
ON [dbo].[Services]
    ([Customer_idCustomer]);
GO

-- Creating foreign key on [Feature_idFeature1] in table 'FeatureValueOfItems'
ALTER TABLE [dbo].[FeatureValueOfItems]
ADD CONSTRAINT [fk_FeatureOfItem_Feature1]
    FOREIGN KEY ([Feature_idFeature1])
    REFERENCES [dbo].[Features]
        ([idFeature])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [FeatureValue_idFeatureValue] in table 'FeatureValueOfItems'
ALTER TABLE [dbo].[FeatureValueOfItems]
ADD CONSTRAINT [fk_FeatureOfItem_FeatureValue1]
    FOREIGN KEY ([FeatureValue_idFeatureValue])
    REFERENCES [dbo].[FeatureValues]
        ([idFeatureValue])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_FeatureOfItem_FeatureValue1'
CREATE INDEX [IX_fk_FeatureOfItem_FeatureValue1]
ON [dbo].[FeatureValueOfItems]
    ([FeatureValue_idFeatureValue]);
GO

-- Creating foreign key on [Item_idItem1] in table 'FeatureValueOfItems'
ALTER TABLE [dbo].[FeatureValueOfItems]
ADD CONSTRAINT [fk_FeatureOfItem_Item1]
    FOREIGN KEY ([Item_idItem1])
    REFERENCES [dbo].[Items]
        ([idItem])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_FeatureOfItem_Item1'
CREATE INDEX [IX_fk_FeatureOfItem_Item1]
ON [dbo].[FeatureValueOfItems]
    ([Item_idItem1]);
GO

-- Creating foreign key on [ItemType_idItemType] in table 'Items'
ALTER TABLE [dbo].[Items]
ADD CONSTRAINT [fk_Item_ItemType1]
    FOREIGN KEY ([ItemType_idItemType])
    REFERENCES [dbo].[ItemTypes]
        ([idItemType])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_Item_ItemType1'
CREATE INDEX [IX_fk_Item_ItemType1]
ON [dbo].[Items]
    ([ItemType_idItemType]);
GO

-- Creating foreign key on [Item_idItem] in table 'Opinions'
ALTER TABLE [dbo].[Opinions]
ADD CONSTRAINT [fk_Opinion_Item1]
    FOREIGN KEY ([Item_idItem])
    REFERENCES [dbo].[Items]
        ([idItem])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_Opinion_Item1'
CREATE INDEX [IX_fk_Opinion_Item1]
ON [dbo].[Opinions]
    ([Item_idItem]);
GO

-- Creating foreign key on [Item_idItem] in table 'SaleDetails'
ALTER TABLE [dbo].[SaleDetails]
ADD CONSTRAINT [fk_SaleDetails_Item1]
    FOREIGN KEY ([Item_idItem])
    REFERENCES [dbo].[Items]
        ([idItem])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_SaleDetails_Item1'
CREATE INDEX [IX_fk_SaleDetails_Item1]
ON [dbo].[SaleDetails]
    ([Item_idItem]);
GO

-- Creating foreign key on [Customer_idCustomer] in table 'Sales'
ALTER TABLE [dbo].[Sales]
ADD CONSTRAINT [fk_Sale_Customer1]
    FOREIGN KEY ([Customer_idCustomer])
    REFERENCES [dbo].[Customers]
        ([idCustomer])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_Sale_Customer1'
CREATE INDEX [IX_fk_Sale_Customer1]
ON [dbo].[Sales]
    ([Customer_idCustomer]);
GO

-- Creating foreign key on [Employee_idEmployee] in table 'Sales'
ALTER TABLE [dbo].[Sales]
ADD CONSTRAINT [fk_Sale_Employee1]
    FOREIGN KEY ([Employee_idEmployee])
    REFERENCES [dbo].[Employees]
        ([idEmployee])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_Sale_Employee1'
CREATE INDEX [IX_fk_Sale_Employee1]
ON [dbo].[Sales]
    ([Employee_idEmployee]);
GO

-- Creating foreign key on [SaleType_idSaleType] in table 'Sales'
ALTER TABLE [dbo].[Sales]
ADD CONSTRAINT [fk_Sale_SaleType1]
    FOREIGN KEY ([SaleType_idSaleType])
    REFERENCES [dbo].[SaleTypes]
        ([idSaleType])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_Sale_SaleType1'
CREATE INDEX [IX_fk_Sale_SaleType1]
ON [dbo].[Sales]
    ([SaleType_idSaleType]);
GO

-- Creating foreign key on [Sale_idSale] in table 'SaleDetails'
ALTER TABLE [dbo].[SaleDetails]
ADD CONSTRAINT [fk_SaleDetails_Sale1]
    FOREIGN KEY ([Sale_idSale])
    REFERENCES [dbo].[Sales]
        ([idSale])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------