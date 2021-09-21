# Wolf-Wolf-Ticket_Sales

This is repository with finished task for Wolf&Wolf company 

It is MVC application which solves problem for ticket sales 

There are 2 types of users: **ADMIN** and **USER**(regular user)

Application use sql database for storing data.

---
  ## User Accounts
### username: admin | password: admin
### username: user1 | password: user
### username: user2 | password: user
### username: user3 | password: user
### username: user4 | password: user
---

**HOW TO USE:**

1. Project contains SQL script file for creating database (DataLayer/script.sql)

2. In appsettings.json file there are 2 settings that need to be updated. 
  One is 'ConnectionString.sqlDb' and there is also flag for database seed which is set to false by default.
  When you run application for first time it need to be set to TRUE so databse can be created and filled with users data needed for using app. After that set 'IsDbSeedRequired' to false or it will break app on next run.
  
3. Next step is to login into application with one of user accounts

---

*as ADMIN user you can add new concerts to database and also see how many tickets are sold for each concert*

*as USER user you can see how many tickets you bought for concerts and also you can buy tickets for other concerts that will be shown under the tabel with tickets info*
