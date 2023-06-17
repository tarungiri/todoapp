using Microsoft.EntityFrameworkCore;
using ToDoApp;

var builder = WebApplication.CreateBuilder(args);

#region Prepare the connection string
var databaseConfigSection = builder.Configuration.GetSection("Database");
var serverName = databaseConfigSection.GetValue<string>("ServerName");
var dbName = databaseConfigSection.GetValue<string>("Name");
var userName = databaseConfigSection.GetValue<string>("UserId");
var password = databaseConfigSection.GetValue<string>("Password");

var connectionString = $"Server={serverName};Initial Catalog={dbName};User Id={userName};Password={password};TrustServerCertificate=True";
#endregion

#region DBContext Configuration
builder.Services.AddDbContext<ToDoDbContext>(options =>
    options.UseSqlServer(connectionString, sqlServerOptions => sqlServerOptions.CommandTimeout(120)));
#endregion

var app = builder.Build();

try
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<ToDoDbContext>();
    dbContext.Database.Migrate();
}
catch (Exception ex)
{
    throw new Exception(ex.ToString() + "  :" + connectionString);
}


app.MapGet("/", () => "Welcome to the ToDo App!");

// Get the records from database.
app.MapGet("/todos", async (ToDoDbContext context) => await context.Todo.ToListAsync());

app.Run();
