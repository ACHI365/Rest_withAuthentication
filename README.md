# Rest_withAuthentication
RestAPI with authentication using .NET 6

RestAPI using .NET 6, using swagger for endpoints visualization and PosgreSQL for database.

In order to run this, change appsettings.json "DefaultConnection" settings to your own and write in command line -> dotnet ef add migrations "name" -> dotnet ef update database, this should set up database and after that just run the Program.cs

After running the app, you should sign up, then sign in and use the following token to authorize, after that you get access to Blogs. If you sign in with different account you don't have to authorize again.
