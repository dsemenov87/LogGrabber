select * from Error

insert into Application (Name) values ('By hands');

insert into Error(Message, CallStack) values ('Test error', 'Test error bla-bla-bla...');

insert into LogItem(Message, ApplicationId, ErrorId, Status, Occured) values
	('Error detected.', 1, 1, 1, getdate())

	--delete from LogItem where id = 4;

	update LogItem set UserId = 1

	select * from LogItem
	select * from Error

	select * from "User"