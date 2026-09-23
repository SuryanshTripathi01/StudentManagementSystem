namespace StudentManagementSystem.Configuration;

public static class DatabaseOptionsFactory
{
    public static DatabaseOptions FromEnvironment()
    {
        return new DatabaseOptions
        {
            Server = Environment.GetEnvironmentVariable("SMS_DB_SERVER") ?? "127.0.0.1",
            Port = uint.TryParse(Environment.GetEnvironmentVariable("SMS_DB_PORT"), out var port) ? port : 3306,
            Database = Environment.GetEnvironmentVariable("SMS_DB_NAME") ?? "student_management",
            User = Environment.GetEnvironmentVariable("SMS_DB_USER") ?? "root",
            Password = Environment.GetEnvironmentVariable("SMS_DB_PASSWORD") ?? string.Empty
        };
    }
}
