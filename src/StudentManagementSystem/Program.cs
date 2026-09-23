using StudentManagementSystem.Configuration;
using StudentManagementSystem.Data;
using StudentManagementSystem.Services;
using StudentManagementSystem.UI;

var options = DatabaseOptionsFactory.FromEnvironment();
var initializer = new DatabaseInitializer(options.ConnectionString);

try
{
    await initializer.InitializeAsync();
    var repository = new MySqlStudentRepository(options.ConnectionString);
    var service = new StudentService(repository);
    var ui = new ConsoleUi(service);
    await ui.RunAsync();
}
catch (Exception ex)
{
    Console.Error.WriteLine("Application could not start.");
    Console.Error.WriteLine(ex.Message);
    Console.Error.WriteLine("Verify MySQL is running and SMS_DB_* environment variables are correct.");
    Environment.ExitCode = 1;
}
