INSERT INTO tblUsers([Username], [Password], [Mail]) values
('max', 'max', 'max@gmail.com');

INSERT INTO tblTasks([Title], [Description], [Status], UserID) values
('Create project', 'Run VS - click create - select WebApi', 1, 1),
('add Di to proj', 'use nuget to add di ', 1, 1),
('Implement native dnd ', 'Use HTML for dnd', 1, 1),
('publish project', 'publish proj on gearhost', 1, 1);


select * from tblUsers