# TechMart

Steps for doing the project

1. Create a solution and a main web project
2. Create 4 more projects which are class libraries -> presentation, business, dataAccess, domain
3. Web project depends on Presentation, business, DataAccess.
4. Presentation depends on business and Domain
5. Business depends on dataAccess and Domain
6. DataAccess depends on Domain
7. Create Entities based on the requirements of the client in the Domain project
8. Add validations and data annotation as per requirement
9. Create a data folder inside DataAccess project and add a dbContext class.
10. Install EntityFrameworkCore, Tool and based on what database you are using install the provider for that database.
11. Implement the dbContext class and add DbSet for each entity you have created in the Domain project. Also override the OnModelCreating method to configure the relationships between entities if needed.
12.   
	1. Add-Migration Message -Project TechMart.DataAccess -StartupProject TechMart
	1. Remove-Migration -Project TechMart.DataAccess -StartupProject TechMart
	1. Update-Database -Project TechMart.DataAccess -StartupProject TechMart