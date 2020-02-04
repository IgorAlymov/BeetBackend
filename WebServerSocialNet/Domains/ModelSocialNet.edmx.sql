
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/04/2019 20:43:46
-- Generated from EDMX file: C:\Users\igor1\Desktop\Сервер\WebServerSocialNet\WebServerSocialNet\Domains\ModelSocialNet.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [SocialDb];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_SocialUserPhoto_SocialUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SocialUserPhoto] DROP CONSTRAINT [FK_SocialUserPhoto_SocialUser];
GO
IF OBJECT_ID(N'[dbo].[FK_SocialUserPhoto_Photo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SocialUserPhoto] DROP CONSTRAINT [FK_SocialUserPhoto_Photo];
GO
IF OBJECT_ID(N'[dbo].[FK_PostPhoto_Post]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PostPhoto] DROP CONSTRAINT [FK_PostPhoto_Post];
GO
IF OBJECT_ID(N'[dbo].[FK_PostPhoto_Photo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PostPhoto] DROP CONSTRAINT [FK_PostPhoto_Photo];
GO
IF OBJECT_ID(N'[dbo].[FK_SocialUserPost]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Posts] DROP CONSTRAINT [FK_SocialUserPost];
GO
IF OBJECT_ID(N'[dbo].[FK_SocialUserLikeComment_SocialUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SocialUserLikeComment] DROP CONSTRAINT [FK_SocialUserLikeComment_SocialUser];
GO
IF OBJECT_ID(N'[dbo].[FK_SocialUserLikeComment_LikeComment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SocialUserLikeComment] DROP CONSTRAINT [FK_SocialUserLikeComment_LikeComment];
GO
IF OBJECT_ID(N'[dbo].[FK_LikeCommentComment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comments] DROP CONSTRAINT [FK_LikeCommentComment];
GO
IF OBJECT_ID(N'[dbo].[FK_SocialUserLikePost_SocialUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SocialUserLikePost] DROP CONSTRAINT [FK_SocialUserLikePost_SocialUser];
GO
IF OBJECT_ID(N'[dbo].[FK_SocialUserLikePost_LikePost]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SocialUserLikePost] DROP CONSTRAINT [FK_SocialUserLikePost_LikePost];
GO
IF OBJECT_ID(N'[dbo].[FK_PostLikePost]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LikePosts] DROP CONSTRAINT [FK_PostLikePost];
GO
IF OBJECT_ID(N'[dbo].[FK_PostComment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comments] DROP CONSTRAINT [FK_PostComment];
GO
IF OBJECT_ID(N'[dbo].[FK_SocialUserFriend]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SocialUsers_Friend] DROP CONSTRAINT [FK_SocialUserFriend];
GO
IF OBJECT_ID(N'[dbo].[FK_UserGroupPhoto]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Photos] DROP CONSTRAINT [FK_UserGroupPhoto];
GO
IF OBJECT_ID(N'[dbo].[FK_UserGroupSocialUser_UserGroup]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserGroupSocialUser] DROP CONSTRAINT [FK_UserGroupSocialUser_UserGroup];
GO
IF OBJECT_ID(N'[dbo].[FK_UserGroupSocialUser_SocialUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserGroupSocialUser] DROP CONSTRAINT [FK_UserGroupSocialUser_SocialUser];
GO
IF OBJECT_ID(N'[dbo].[FK_UserGroupPost]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Posts] DROP CONSTRAINT [FK_UserGroupPost];
GO
IF OBJECT_ID(N'[dbo].[FK_CommentSocialUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comments] DROP CONSTRAINT [FK_CommentSocialUser];
GO
IF OBJECT_ID(N'[dbo].[FK_MessagePhoto]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Photos] DROP CONSTRAINT [FK_MessagePhoto];
GO
IF OBJECT_ID(N'[dbo].[FK_MessageSocialUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Messages] DROP CONSTRAINT [FK_MessageSocialUser];
GO
IF OBJECT_ID(N'[dbo].[FK_MessageReceiver]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Messages] DROP CONSTRAINT [FK_MessageReceiver];
GO
IF OBJECT_ID(N'[dbo].[FK_Friend_inherits_SocialUser]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SocialUsers_Friend] DROP CONSTRAINT [FK_Friend_inherits_SocialUser];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[SocialUsers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SocialUsers];
GO
IF OBJECT_ID(N'[dbo].[Photos]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Photos];
GO
IF OBJECT_ID(N'[dbo].[Comments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Comments];
GO
IF OBJECT_ID(N'[dbo].[Posts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Posts];
GO
IF OBJECT_ID(N'[dbo].[LikePosts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LikePosts];
GO
IF OBJECT_ID(N'[dbo].[LikeComments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LikeComments];
GO
IF OBJECT_ID(N'[dbo].[UserGroups]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserGroups];
GO
IF OBJECT_ID(N'[dbo].[Messages]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Messages];
GO
IF OBJECT_ID(N'[dbo].[SocialUsers_Friend]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SocialUsers_Friend];
GO
IF OBJECT_ID(N'[dbo].[SocialUserPhoto]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SocialUserPhoto];
GO
IF OBJECT_ID(N'[dbo].[PostPhoto]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PostPhoto];
GO
IF OBJECT_ID(N'[dbo].[SocialUserLikeComment]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SocialUserLikeComment];
GO
IF OBJECT_ID(N'[dbo].[SocialUserLikePost]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SocialUserLikePost];
GO
IF OBJECT_ID(N'[dbo].[UserGroupSocialUser]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserGroupSocialUser];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'SocialUsers'
CREATE TABLE [dbo].[SocialUsers] (
    [SocialUserId] int IDENTITY(1,1) NOT NULL,
    [AspUserId] nvarchar(max)  NULL,
    [Firstname] nvarchar(max)  NOT NULL,
    [Lastname] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [AvatarPhotoId] int  NULL,
    [Birthday] datetime  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [Gender] nvarchar(max)  NULL,
    [PhoneNumber] nvarchar(max)  NULL,
    [AboutMe] nvarchar(max)  NULL,
    [City] nvarchar(max)  NULL
);
GO

-- Creating table 'Photos'
CREATE TABLE [dbo].[Photos] (
    [PhotoId] int IDENTITY(1,1) NOT NULL,
    [Filename] nvarchar(max)  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [GroupCover_GroupId] int  NULL,
    [Message_MessageId] int  NULL
);
GO

-- Creating table 'Comments'
CREATE TABLE [dbo].[Comments] (
    [CommentId] int IDENTITY(1,1) NOT NULL,
    [Text] nvarchar(max)  NOT NULL,
    [LikesCounter] int  NOT NULL,
    [Date] datetime  NOT NULL,
    [LikeComment_LikeCommentId] int  NULL,
    [Post_PostId] int  NULL,
    [Author_SocialUserId] int  NULL
);
GO

-- Creating table 'Posts'
CREATE TABLE [dbo].[Posts] (
    [PostId] int IDENTITY(1,1) NOT NULL,
    [Text] nvarchar(max)  NOT NULL,
    [LikesCounter] int  NOT NULL,
    [Date] datetime  NOT NULL,
    [Author_SocialUserId] int  NULL,
    [UserGroupForPost_GroupId] int  NULL
);
GO

-- Creating table 'LikePosts'
CREATE TABLE [dbo].[LikePosts] (
    [LikePostId] int IDENTITY(1,1) NOT NULL,
    [Post_PostId] int  NULL
);
GO

-- Creating table 'LikeComments'
CREATE TABLE [dbo].[LikeComments] (
    [LikeCommentId] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'UserGroups'
CREATE TABLE [dbo].[UserGroups] (
    [GroupId] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Messages'
CREATE TABLE [dbo].[Messages] (
    [MessageId] int IDENTITY(1,1) NOT NULL,
    [Text] nvarchar(max)  NOT NULL,
    [WasReaded] bit  NOT NULL,
    [Author_SocialUserId] int  NULL,
    [Receiver_SocialUserId] int  NULL
);
GO

-- Creating table 'SocialUsers_Friend'
CREATE TABLE [dbo].[SocialUsers_Friend] (
    [FriendId] int IDENTITY(1,1) NOT NULL,
    [SocialUserId] int  NOT NULL,
    [UserFriend_SocialUserId] int  NULL
);
GO

-- Creating table 'SocialUserPhoto'
CREATE TABLE [dbo].[SocialUserPhoto] (
    [PhotoUsers_SocialUserId] int  NOT NULL,
    [UserPhotos_PhotoId] int  NOT NULL
);
GO

-- Creating table 'PostPhoto'
CREATE TABLE [dbo].[PostPhoto] (
    [PostsWithPhoto_PostId] int  NOT NULL,
    [AttachedPhotos_PhotoId] int  NOT NULL
);
GO

-- Creating table 'SocialUserLikeComment'
CREATE TABLE [dbo].[SocialUserLikeComment] (
    [LikesUsers_SocialUserId] int  NOT NULL,
    [LikesComments_LikeCommentId] int  NOT NULL
);
GO

-- Creating table 'SocialUserLikePost'
CREATE TABLE [dbo].[SocialUserLikePost] (
    [LikesUsers_SocialUserId] int  NOT NULL,
    [LikesPosts_LikePostId] int  NOT NULL
);
GO

-- Creating table 'UserGroupSocialUser'
CREATE TABLE [dbo].[UserGroupSocialUser] (
    [Groups_GroupId] int  NOT NULL,
    [UsersForGroup_SocialUserId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [SocialUserId] in table 'SocialUsers'
ALTER TABLE [dbo].[SocialUsers]
ADD CONSTRAINT [PK_SocialUsers]
    PRIMARY KEY CLUSTERED ([SocialUserId] ASC);
GO

-- Creating primary key on [PhotoId] in table 'Photos'
ALTER TABLE [dbo].[Photos]
ADD CONSTRAINT [PK_Photos]
    PRIMARY KEY CLUSTERED ([PhotoId] ASC);
GO

-- Creating primary key on [CommentId] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [PK_Comments]
    PRIMARY KEY CLUSTERED ([CommentId] ASC);
GO

-- Creating primary key on [PostId] in table 'Posts'
ALTER TABLE [dbo].[Posts]
ADD CONSTRAINT [PK_Posts]
    PRIMARY KEY CLUSTERED ([PostId] ASC);
GO

-- Creating primary key on [LikePostId] in table 'LikePosts'
ALTER TABLE [dbo].[LikePosts]
ADD CONSTRAINT [PK_LikePosts]
    PRIMARY KEY CLUSTERED ([LikePostId] ASC);
GO

-- Creating primary key on [LikeCommentId] in table 'LikeComments'
ALTER TABLE [dbo].[LikeComments]
ADD CONSTRAINT [PK_LikeComments]
    PRIMARY KEY CLUSTERED ([LikeCommentId] ASC);
GO

-- Creating primary key on [GroupId] in table 'UserGroups'
ALTER TABLE [dbo].[UserGroups]
ADD CONSTRAINT [PK_UserGroups]
    PRIMARY KEY CLUSTERED ([GroupId] ASC);
GO

-- Creating primary key on [MessageId] in table 'Messages'
ALTER TABLE [dbo].[Messages]
ADD CONSTRAINT [PK_Messages]
    PRIMARY KEY CLUSTERED ([MessageId] ASC);
GO

-- Creating primary key on [SocialUserId] in table 'SocialUsers_Friend'
ALTER TABLE [dbo].[SocialUsers_Friend]
ADD CONSTRAINT [PK_SocialUsers_Friend]
    PRIMARY KEY CLUSTERED ([SocialUserId] ASC);
GO

-- Creating primary key on [PhotoUsers_SocialUserId], [UserPhotos_PhotoId] in table 'SocialUserPhoto'
ALTER TABLE [dbo].[SocialUserPhoto]
ADD CONSTRAINT [PK_SocialUserPhoto]
    PRIMARY KEY CLUSTERED ([PhotoUsers_SocialUserId], [UserPhotos_PhotoId] ASC);
GO

-- Creating primary key on [PostsWithPhoto_PostId], [AttachedPhotos_PhotoId] in table 'PostPhoto'
ALTER TABLE [dbo].[PostPhoto]
ADD CONSTRAINT [PK_PostPhoto]
    PRIMARY KEY CLUSTERED ([PostsWithPhoto_PostId], [AttachedPhotos_PhotoId] ASC);
GO

-- Creating primary key on [LikesUsers_SocialUserId], [LikesComments_LikeCommentId] in table 'SocialUserLikeComment'
ALTER TABLE [dbo].[SocialUserLikeComment]
ADD CONSTRAINT [PK_SocialUserLikeComment]
    PRIMARY KEY CLUSTERED ([LikesUsers_SocialUserId], [LikesComments_LikeCommentId] ASC);
GO

-- Creating primary key on [LikesUsers_SocialUserId], [LikesPosts_LikePostId] in table 'SocialUserLikePost'
ALTER TABLE [dbo].[SocialUserLikePost]
ADD CONSTRAINT [PK_SocialUserLikePost]
    PRIMARY KEY CLUSTERED ([LikesUsers_SocialUserId], [LikesPosts_LikePostId] ASC);
GO

-- Creating primary key on [Groups_GroupId], [UsersForGroup_SocialUserId] in table 'UserGroupSocialUser'
ALTER TABLE [dbo].[UserGroupSocialUser]
ADD CONSTRAINT [PK_UserGroupSocialUser]
    PRIMARY KEY CLUSTERED ([Groups_GroupId], [UsersForGroup_SocialUserId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [PhotoUsers_SocialUserId] in table 'SocialUserPhoto'
ALTER TABLE [dbo].[SocialUserPhoto]
ADD CONSTRAINT [FK_SocialUserPhoto_SocialUser]
    FOREIGN KEY ([PhotoUsers_SocialUserId])
    REFERENCES [dbo].[SocialUsers]
        ([SocialUserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [UserPhotos_PhotoId] in table 'SocialUserPhoto'
ALTER TABLE [dbo].[SocialUserPhoto]
ADD CONSTRAINT [FK_SocialUserPhoto_Photo]
    FOREIGN KEY ([UserPhotos_PhotoId])
    REFERENCES [dbo].[Photos]
        ([PhotoId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SocialUserPhoto_Photo'
CREATE INDEX [IX_FK_SocialUserPhoto_Photo]
ON [dbo].[SocialUserPhoto]
    ([UserPhotos_PhotoId]);
GO

-- Creating foreign key on [PostsWithPhoto_PostId] in table 'PostPhoto'
ALTER TABLE [dbo].[PostPhoto]
ADD CONSTRAINT [FK_PostPhoto_Post]
    FOREIGN KEY ([PostsWithPhoto_PostId])
    REFERENCES [dbo].[Posts]
        ([PostId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [AttachedPhotos_PhotoId] in table 'PostPhoto'
ALTER TABLE [dbo].[PostPhoto]
ADD CONSTRAINT [FK_PostPhoto_Photo]
    FOREIGN KEY ([AttachedPhotos_PhotoId])
    REFERENCES [dbo].[Photos]
        ([PhotoId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PostPhoto_Photo'
CREATE INDEX [IX_FK_PostPhoto_Photo]
ON [dbo].[PostPhoto]
    ([AttachedPhotos_PhotoId]);
GO

-- Creating foreign key on [Author_SocialUserId] in table 'Posts'
ALTER TABLE [dbo].[Posts]
ADD CONSTRAINT [FK_SocialUserPost]
    FOREIGN KEY ([Author_SocialUserId])
    REFERENCES [dbo].[SocialUsers]
        ([SocialUserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SocialUserPost'
CREATE INDEX [IX_FK_SocialUserPost]
ON [dbo].[Posts]
    ([Author_SocialUserId]);
GO

-- Creating foreign key on [LikesUsers_SocialUserId] in table 'SocialUserLikeComment'
ALTER TABLE [dbo].[SocialUserLikeComment]
ADD CONSTRAINT [FK_SocialUserLikeComment_SocialUser]
    FOREIGN KEY ([LikesUsers_SocialUserId])
    REFERENCES [dbo].[SocialUsers]
        ([SocialUserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [LikesComments_LikeCommentId] in table 'SocialUserLikeComment'
ALTER TABLE [dbo].[SocialUserLikeComment]
ADD CONSTRAINT [FK_SocialUserLikeComment_LikeComment]
    FOREIGN KEY ([LikesComments_LikeCommentId])
    REFERENCES [dbo].[LikeComments]
        ([LikeCommentId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SocialUserLikeComment_LikeComment'
CREATE INDEX [IX_FK_SocialUserLikeComment_LikeComment]
ON [dbo].[SocialUserLikeComment]
    ([LikesComments_LikeCommentId]);
GO

-- Creating foreign key on [LikeComment_LikeCommentId] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [FK_LikeCommentComment]
    FOREIGN KEY ([LikeComment_LikeCommentId])
    REFERENCES [dbo].[LikeComments]
        ([LikeCommentId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LikeCommentComment'
CREATE INDEX [IX_FK_LikeCommentComment]
ON [dbo].[Comments]
    ([LikeComment_LikeCommentId]);
GO

-- Creating foreign key on [LikesUsers_SocialUserId] in table 'SocialUserLikePost'
ALTER TABLE [dbo].[SocialUserLikePost]
ADD CONSTRAINT [FK_SocialUserLikePost_SocialUser]
    FOREIGN KEY ([LikesUsers_SocialUserId])
    REFERENCES [dbo].[SocialUsers]
        ([SocialUserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [LikesPosts_LikePostId] in table 'SocialUserLikePost'
ALTER TABLE [dbo].[SocialUserLikePost]
ADD CONSTRAINT [FK_SocialUserLikePost_LikePost]
    FOREIGN KEY ([LikesPosts_LikePostId])
    REFERENCES [dbo].[LikePosts]
        ([LikePostId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SocialUserLikePost_LikePost'
CREATE INDEX [IX_FK_SocialUserLikePost_LikePost]
ON [dbo].[SocialUserLikePost]
    ([LikesPosts_LikePostId]);
GO

-- Creating foreign key on [Post_PostId] in table 'LikePosts'
ALTER TABLE [dbo].[LikePosts]
ADD CONSTRAINT [FK_PostLikePost]
    FOREIGN KEY ([Post_PostId])
    REFERENCES [dbo].[Posts]
        ([PostId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PostLikePost'
CREATE INDEX [IX_FK_PostLikePost]
ON [dbo].[LikePosts]
    ([Post_PostId]);
GO

-- Creating foreign key on [Post_PostId] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [FK_PostComment]
    FOREIGN KEY ([Post_PostId])
    REFERENCES [dbo].[Posts]
        ([PostId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PostComment'
CREATE INDEX [IX_FK_PostComment]
ON [dbo].[Comments]
    ([Post_PostId]);
GO

-- Creating foreign key on [UserFriend_SocialUserId] in table 'SocialUsers_Friend'
ALTER TABLE [dbo].[SocialUsers_Friend]
ADD CONSTRAINT [FK_SocialUserFriend]
    FOREIGN KEY ([UserFriend_SocialUserId])
    REFERENCES [dbo].[SocialUsers]
        ([SocialUserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SocialUserFriend'
CREATE INDEX [IX_FK_SocialUserFriend]
ON [dbo].[SocialUsers_Friend]
    ([UserFriend_SocialUserId]);
GO

-- Creating foreign key on [GroupCover_GroupId] in table 'Photos'
ALTER TABLE [dbo].[Photos]
ADD CONSTRAINT [FK_UserGroupPhoto]
    FOREIGN KEY ([GroupCover_GroupId])
    REFERENCES [dbo].[UserGroups]
        ([GroupId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserGroupPhoto'
CREATE INDEX [IX_FK_UserGroupPhoto]
ON [dbo].[Photos]
    ([GroupCover_GroupId]);
GO

-- Creating foreign key on [Groups_GroupId] in table 'UserGroupSocialUser'
ALTER TABLE [dbo].[UserGroupSocialUser]
ADD CONSTRAINT [FK_UserGroupSocialUser_UserGroup]
    FOREIGN KEY ([Groups_GroupId])
    REFERENCES [dbo].[UserGroups]
        ([GroupId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [UsersForGroup_SocialUserId] in table 'UserGroupSocialUser'
ALTER TABLE [dbo].[UserGroupSocialUser]
ADD CONSTRAINT [FK_UserGroupSocialUser_SocialUser]
    FOREIGN KEY ([UsersForGroup_SocialUserId])
    REFERENCES [dbo].[SocialUsers]
        ([SocialUserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserGroupSocialUser_SocialUser'
CREATE INDEX [IX_FK_UserGroupSocialUser_SocialUser]
ON [dbo].[UserGroupSocialUser]
    ([UsersForGroup_SocialUserId]);
GO

-- Creating foreign key on [UserGroupForPost_GroupId] in table 'Posts'
ALTER TABLE [dbo].[Posts]
ADD CONSTRAINT [FK_UserGroupPost]
    FOREIGN KEY ([UserGroupForPost_GroupId])
    REFERENCES [dbo].[UserGroups]
        ([GroupId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserGroupPost'
CREATE INDEX [IX_FK_UserGroupPost]
ON [dbo].[Posts]
    ([UserGroupForPost_GroupId]);
GO

-- Creating foreign key on [Author_SocialUserId] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [FK_CommentSocialUser]
    FOREIGN KEY ([Author_SocialUserId])
    REFERENCES [dbo].[SocialUsers]
        ([SocialUserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CommentSocialUser'
CREATE INDEX [IX_FK_CommentSocialUser]
ON [dbo].[Comments]
    ([Author_SocialUserId]);
GO

-- Creating foreign key on [Message_MessageId] in table 'Photos'
ALTER TABLE [dbo].[Photos]
ADD CONSTRAINT [FK_MessagePhoto]
    FOREIGN KEY ([Message_MessageId])
    REFERENCES [dbo].[Messages]
        ([MessageId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MessagePhoto'
CREATE INDEX [IX_FK_MessagePhoto]
ON [dbo].[Photos]
    ([Message_MessageId]);
GO

-- Creating foreign key on [Author_SocialUserId] in table 'Messages'
ALTER TABLE [dbo].[Messages]
ADD CONSTRAINT [FK_MessageSocialUser]
    FOREIGN KEY ([Author_SocialUserId])
    REFERENCES [dbo].[SocialUsers]
        ([SocialUserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MessageSocialUser'
CREATE INDEX [IX_FK_MessageSocialUser]
ON [dbo].[Messages]
    ([Author_SocialUserId]);
GO

-- Creating foreign key on [Receiver_SocialUserId] in table 'Messages'
ALTER TABLE [dbo].[Messages]
ADD CONSTRAINT [FK_MessageReceiver]
    FOREIGN KEY ([Receiver_SocialUserId])
    REFERENCES [dbo].[SocialUsers]
        ([SocialUserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MessageReceiver'
CREATE INDEX [IX_FK_MessageReceiver]
ON [dbo].[Messages]
    ([Receiver_SocialUserId]);
GO

-- Creating foreign key on [SocialUserId] in table 'SocialUsers_Friend'
ALTER TABLE [dbo].[SocialUsers_Friend]
ADD CONSTRAINT [FK_Friend_inherits_SocialUser]
    FOREIGN KEY ([SocialUserId])
    REFERENCES [dbo].[SocialUsers]
        ([SocialUserId])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------