using Microsoft.Data.Sqlite;

namespace HabitTracker.Data;

public class AdoNetDbContext : IDisposable
{
    private readonly string _connectionString;
    private SqliteConnection _connection;
    private bool _disposed = false;

    public AdoNetDbContext(string databasePath)
    {
        _connectionString = $"Data Source={databasePath};";

        if (!File.Exists(databasePath))
        {
            CreateDatabase();
        }

        _connection = new SqliteConnection(_connectionString);
        _connection.Open();
    }

    private void CreateDatabase()
    {
        using (var connection = new SqliteConnection(_connectionString))
        {
            connection.Open();

            string createHabitTableQuery = @"
                CREATE TABLE IF NOT EXISTS Habits (
                    Id TEXT PRIMARY KEY NOT NULL,
                    Name TEXT NOT NULL
                );";

            using (var command = new SqliteCommand(createHabitTableQuery, connection))
            {
                command.ExecuteNonQuery();
            }

            string createOccurrenceTableQuery = @"
                CREATE TABLE IF NOT EXISTS Occurrences (
                    Id TEXT PRIMARY KEY NOT NULL,
                    Date TEXT NOT NULL,
                    HabitId TEXT NOT NULL,
                    FOREIGN KEY (HabitId) REFERENCES Habits(Id)
                );";

            using (var command = new SqliteCommand(createOccurrenceTableQuery, connection))
            {
                command.ExecuteNonQuery();
            }

            connection.Close();
        }

        Console.WriteLine("Database and initial tables created successfully.");
    }

    public int ExecuteNonQuery(string query, Dictionary<string, object> parameters = null!)
    {
        using SqliteCommand command = new(query, _connection);
        AddParameters(command, parameters);
        return command.ExecuteNonQuery();
    }

    public object ExecuteScalar(string query, Dictionary<string, object> parameters = null!)
    {
        using SqliteCommand command = new(query, _connection);
        AddParameters(command, parameters);
        return command.ExecuteScalar()!;
    }

    public List<Dictionary<string, object>> ExecuteQuery(string query, Dictionary<string, object> parameters = null!)
    {
        using SqliteCommand command = new(query, _connection);
        AddParameters(command, parameters);

        using SqliteDataReader reader = command.ExecuteReader();
        List<Dictionary<string, object>> results = [];

        while (reader.Read())
        {
            Dictionary<string, object> row = [];

            for (int i = 0; i < reader.FieldCount; i++)
            {
                row[reader.GetName(i)] = reader.GetValue(i);
            }

            results.Add(row);
        }

        return results;
    }

    private static void AddParameters(SqliteCommand command, Dictionary<string, object> parameters)
    {
        if (parameters is not null)
        {
            foreach (var param in parameters)
            {
                command.Parameters.AddWithValue(param.Key, param.Value);
            }
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing && _connection is not null)
            {
                _connection.Close();
                _connection.Dispose();
                _connection = null!;
            }
            _disposed = true;
        }
    }

    // Dispose method to close the connection
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
