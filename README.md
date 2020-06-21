# Automation Helper
A web crawler tool which help users to catch the data table from website. Tech highlight: WPF Core, MVVM, Entity Framework, SQLite.

## Design
1. MVVM design pattern by using Caliburn.Micro framework.
2. Xunit apply for unit test.
3. EF for Sqlite.

## Application UI

![image](https://github.com/TheNickDeveloper/AutomationHelper/blob/master/images/AppUi.png)

## How to Use

#### 1. Provide User Name ans Password

Provide user name and password for authentication.

#### 2. Select Ticket Option

Select the type of taget data based on operation requirement, and it will nevigate to the related url for data scripting.

#### 3. Select Time Range

It will catch the data by judging user defined date and time range.
- **Date Range**  
    Set the date range for data catching.


- **Time Range**  
    Set the time range for data catching.

#### 4. Browse Output Path

Define result output path by clicking browse and select the ourput folder.

#### 3. Execute Extract Data

Press extract data button, and the program will start web scrpting. To see if programe finish running by judging execute status: if indicate "Done", then you can find the output result from user defined output path; if failed, it will indicate error message on UI. You can refer further details by checking logs. 