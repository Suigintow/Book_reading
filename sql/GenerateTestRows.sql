CREATE TABLE [dbo].[Book] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [Title]       VARCHAR (MAX) NOT NULL,
    [Description] VARCHAR (MAX) NULL,
    [Author]      VARCHAR (MAX) NULL,
    [Pages]       INT           NOT NULL,
    [Genre]       VARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Review] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [Author]       VARCHAR (MAX) NULL,
    [BookId]       INT           NULL,
    [Text]         VARCHAR (MAX) NULL,
    [LikeCount]    INT           DEFAULT ((0)) NULL,
    [ReportReason] VARCHAR (MAX) DEFAULT ('') NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

create table t1000 (num int not null)
go
declare @i int
set @i = 1
while @i < 1001
begin
	insert t1000 values(@i)
	set @i = @i + 1
end
go

insert Book (Title, Description, Pages, Author, Genre)
select 'Title' + cast(id as varchar(10)), 'Description' + cast(id as varchar(10)), num,
	case when num = 1 then 'Test Author' else 'Author' + cast(id as varchar(10)) end,
	'Genre' + cast(id as varchar(10))
from (
	select t1.num * t2.num as id, t1.num from 
		t1000 t1 cross join t1000 t2
) t
go