## Getting Started

### PSQL

First, we're going to install Postgres. This will be the database that the app uses to store data. [Here is the link to instructions on installing postgres](https://www.postgresql.org/download/). Choose the operating system of your computer. 

#### Installation

Follow this video to install PostgreSQL: [How to Install PostgreSQL 16 on Windows 11](https://www.youtube.com/watch?v=WxBfnGH3FsU)

Durning the installation, set up password to postgres

Only install PostgreSQL, we do not need create database and table

### Dotnet

You need to install dotnet 7 for MyFirstBlog and dotnet 5 for MyFirstBlogTests. 

You can download the installer [NET 7](https://dotnet.microsoft.com/en-us/download/dotnet/7.0) and [NET 5](https://dotnet.microsoft.com/en-us/download/dotnet/5.0)

### Project Setup
Run backend code to navigate to http://localhost:5000/swagger/index.html

Open pgAdmin4, you will find a database called bvc-blog and in this database there is a table called post

Enter data into this post table. There is a data sample:

Id:  3F2504E0-4F89-11D3-9A0C-0305E82C3301<br>
Title:  Understanding C# class<br>
Slug:  understanding-csharp-class<br>
Body:  In this post, we explore the concept of class in C#.<br>
CreateDate: 2023-05-15 10:00:00

### Send request by Postman
Run Postman to send a request to http://localhost:5000/posts and you could get the response of posts data


