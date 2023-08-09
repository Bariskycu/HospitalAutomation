Create database HospitalAutomation
go
Use HospitalAutomation
go
Create table Hospital
(HospitalId int identity,
HospitalName nvarchar(50),
Address nvarchar(200),
PhoneNumber nvarchar(20),
WebSite nvarchar(50),
Constraint Pk_HospitalId Primary key(HospitalId)
)
go
Create table Branch
(BranchId int identity,
BranchName nvarchar(50),
HospitalId int,
Constraint Pk_BranchId Primary key(BranchId),
FOREIGN KEY (HospitalId) REFERENCES Hospital(HospitalId)
)
go
Create table Doctor
(DoctorId int identity,
UserName nvarchar(50),
Password nvarchar(50),
Name nvarchar(50),
LastName nvarchar(50),
Branch nvarchar(100),
Phone nvarchar(15),
DateOfBirth nvarchar(50),
Email nvarchar(50),
HospitalId int,
Constraint Pk_DoctorId Primary key(DoctorId),
FOREIGN KEY (HospitalId) REFERENCES Hospital(HospitalId)
)
go
Create table Department
(DepartmentId int identity,
DepartmentName nvarchar(100),
HospitalId int,
Constraint Pk_DepartmentId Primary key(DepartmentId),
FOREIGN KEY (HospitalId) REFERENCES Hospital(HospitalId)
)
go
Create table Ill
(IllId int identity,
UserName nvarchar(50),
Password nvarchar(50),
IllName nvarchar(50),
IllLastName nvarchar(50),
Email nvarchar(100),
HospitalId int,
Constraint Pk_IllId Primary key(IllId),
FOREIGN KEY (HospitalId) REFERENCES Hospital(HospitalId)
)
go
Create table AppointmentList
(AppId int identity,
DoctorId int,
IllName nvarchar(50),
DoctorName nvarchar(50),
Branch nvarchar(50),
Hour nvarchar(20),
Date nvarchar(50),
Constraint Pk_AppId Primary key(AppId),
FOREIGN KEY (DoctorId) REFERENCES Doctor(DoctorId)
)
go
create Procedure SP_IllLogin
	@UName Nvarchar(50),
	@Pass Nvarchar(50)
As
	Declare @returnValue Int = 0
	if Exists(Select * From Ill Where UserName=@UName)
	Begin
		if Exists(Select * From Ill Where UserName=@UName and Password = @Pass)
			Begin
				Select * From Ill Where UserName=@UName and Password = @Pass
				Set @returnValue = 1
			End
		else
			Begin
				Set @returnValue = -1
			End
	End
	else
	Begin
		Set @returnValue = 0
	End
Return @returnValue
go
create Procedure SP_DoctorLogin
	@UName Nvarchar(50),
	@Pass2 Nvarchar(50)
As
	Declare @returnValue Int = 0
	if Exists(Select * From Doctor Where UserName=@UName)
	Begin
		if Exists(Select * From Doctor Where UserName=@UName and Password = @Pass2)
			Begin
				Select * From Doctor Where UserName=@UName and Password = @Pass2
				Set @returnValue = 1
			End
		else
			Begin
				Set @returnValue = -1
			End
	End
	else
	Begin
		Set @returnValue = 0
	End
Return @returnValue
go
Create procedure SP_RandevuListeSelect
as
begin
    select * from AppointmentList
end
go
create procedure SP_RandevuInsert
(

@IllName nvarchar(50),
@DoctorName nvarchar(50),
@Branch nvarchar(50),
@Hour nvarchar(50),
@Date nvarchar(50)
)
as
begin insert into AppointmentList(IllName,DoctorName,Branch,Hour,Date)Values (@IllName,@DoctorName,@Branch,@Hour,@Date)
end
go
create procedure SP_RandevuDelete
@AppId int
as
Delete from AppointmentList where AppId=@AppId 
go
create procedure SP_RandevuUpdate
@AppId int,
@IllName nvarchar(50),
@DoctorName nvarchar(50),
@Branch nvarchar(100),
@Hour nvarchar(50),
@Date nvarchar(50)
as 
Update AppointmentList set @IllName=@IllName,@DoctorName=@DoctorName,Branch=@Branch,Hour=@Hour,Date=@Date where AppId=@AppId
go
Create procedure SP_DoktorInsert
(
@UserName nvarchar(50),
@Password nvarchar(50),
@Name nvarchar(50),
@LastName nvarchar(50),
@Branch nvarchar(50),
@Phone nvarchar(15)
)
as
begin Insert into Doctor(UserName,Password,Name,LastName,Branch,Phone) Values (@UserName,@Password,@Name,@LastName,@Branch,@Phone)
end
go
create procedure SP_DoktorDelete
@DoctorId int
as
Delete from Doctor where DoctorId=@DoctorId
go
create procedure SP_DoktorUpdate
@DoctorId int,
@UserName nvarchar(50),
@Password nvarchar(50),
@Name nvarchar(50),
@LastName nvarchar(50),
@Branch nvarchar(100),
@Phone nvarchar(15)
as
Update Doctor set UserName=@UserName,Password=@Password,Name=@Name,LastName=@LastName,Branch=@Branch,Phone=@Phone where DoctorId=@DoctorId
go
create procedure SP_HastaInsert
(
@UserName nvarchar(50),
@Password nvarchar(50),
@IllName nvarchar(50),
@IllLastName nvarchar(50),
@Email nvarchar(100)
)
as
begin insert into Ill (UserName,Password,IllName,IllLastName,Email) values (@UserName,@Password,@IllName,@IllLastName,@Email)
end
go
create procedure SP_HastaDelete
@IllId int
as
delete from Ill where IllId=@IllId 
go
create procedure SP_HastaUpdate
@IllId int,
@UserName nvarchar(50),
@Password nvarchar(50),
@IllName nvarchar(50),
@IllLastName nvarchar(50),
@Email nvarchar(50)
as
Update Ill set UserName=@UserName,Password=@Password,IllName=@IllName,IllLastName=@IllLastName,Email=@Email where IllId=@IllId