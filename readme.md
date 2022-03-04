# MovieCharactersAPI

very simple Movie Characters API solely built for educational purposes
- uses .NET Core 5
- uses Entity Framework
- uses local MSSQLEXPRESS instance
- see builtin Swagger for detailed documentation

# Install
 
[Visual Studio Code](https://code.visualstudio.com/download)

or

[Visual Studio](https://visualstudio.microsoft.com/)

- Clone this repo to a local directory:

    `git clone https://github.com/Tranquillo1811/MovieCharacters.git`

 - Open solution in Visual Studio or Visual Studio Code or any other IDE
 - In appsettings.json, change "ConnectionStrings":
    "DatabaseConnection" to refer to your SQL Server Name
    
    (If your SQL Server is hosted on the same computer as you are running this code, you can leave this value set to `localhost`)


# Usage

- run `update-database` in Package Manager Console (and ensure current project is set to MovieCharacters.DAL or 
- run `dotnet ef database update` in any command line (and ensure ./MovieCharacters.DAL is your current folder)

- start Debugging (IISExpress) and use Swagger Browser App to initiate HTTP requests


# Maintainers

<https://github.com/OBeratis>

<https://github.com/Tranquillo1811> 


# Contributing

This project is not supposed to have any contributors.
It is supposed to grade the individuals who composed it.
Right after that graduation the project will be - most likely - instantly discontinued...


# License
[Apache License &copy; 2022](./LICENSE)
