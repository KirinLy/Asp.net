using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Threading.Tasks;
using static System.Console;

namespace SampleADO.NET
{
    internal partial class Program
    {
        static async Task Main(string[] args)
        {
            var builder = new SqlConnectionStringBuilder()
            {
                TrustServerCertificate = true,
                Encrypt = true,
                InitialCatalog = "Northwind"
            };

            // show options DB
            WriteLine("Connect to:");
            WriteLine(" 1 - SQL Server on local machine");
            WriteLine(" 2 - Azure SQL Database");
            WriteLine(" 3 – Azure SQL Edge");
            WriteLine();
            Write("Press a key: ");


            // Read key
            ConsoleKeyInfo keyDb = ReadKey();
            WriteLine();

            // Switch key
            switch (keyDb.Key)
            {
                case ConsoleKey.D1:
                    builder.DataSource = "tcp:127.0.0.1,1433"; // SQL Server
                    WriteLine("SQL Server");
                    break;
                case ConsoleKey.D2:
                    builder.DataSource = "tcp:apps-services-net7.database.windows.net,1433"; // Azure
                    WriteLine("Azure");
                    break;
                case ConsoleKey.D3:
                    builder.DataSource = "tcp:127.0.0.1,1433"; // Azure SQL Edge
                    WriteLine("Azure SQL Edge");
                    break;
                default:
                    WriteLine("Invalid option");
                    break;
            }

            WriteLine();
            WriteLine();

            // Authentication
            WriteLine("Authentication:");
            WriteLine(" 1 - Windows Integrated Security");
            WriteLine(" 2 - SQL Login");
            WriteLine();
            Write("Press a key: ");

            // Read key
            ConsoleKeyInfo keyAuth = ReadKey();
            WriteLine();

            // Switch key
            switch (keyAuth.Key)
            {
                case ConsoleKey.D1:
                    builder.IntegratedSecurity = true;
                    WriteLine("Windows Integrated Security");
                    break;
                case ConsoleKey.D2:
                    builder.UserID = "sa";
                    builder.Password = "yourStrong(!)Password";
                    WriteLine("SQL Login");
                    break;
                default:
                    WriteLine("Invalid option");
                    break;
            }


            // Open connection from builder
            try
            {
                var connection = new SqlConnection(builder.ConnectionString);
                connection.InfoMessage += Connection_InfoMessage;
                connection.StateChange += Connection_StateChange;

                await connection.OpenAsync();
                WriteLine($"SQL Server version: {connection.ServerVersion}");

                // input unit price
                Write("Enter a unit price: ");
                string unitPrice = ReadLine();
                if (!int.TryParse(unitPrice, out var price))
                {
                    WriteLine("Invalid price");
                }


                WriteLine();
                WriteLine();

                // Options Text or Stored Procedure
                WriteLine("Execute command using:");
                WriteLine(" 1 - Text");
                WriteLine(" 2 - Stored Procedure");
                WriteLine();
                Write("Press a key: ");
                ConsoleKeyInfo keyCommand = ReadKey();
                WriteLine();

                await GetProducts(connection, keyCommand, price);

                await connection.CloseAsync();
            }
            catch (SqlException ex)
            {
                WriteLine(ex.Message);
            }
        }

        private static async Task GetProducts(SqlConnection connection, ConsoleKeyInfo keyCommand, int price)
        {
            // Create a command
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            // switch key
            SqlParameter priceStoParameter = new();
            SqlParameter count = new();
            SqlParameter rt = new();
            switch (keyCommand.Key)
            {
                case ConsoleKey.D1:
                    command.CommandText =
                        "SELECT [ProductName], [UnitPrice], [UnitsInStock] FROM [Northwind].[dbo].[Products] WHERE [UnitPrice] >= @price";
                    SqlParameter priceParameter = new()
                    {
                        SqlDbType = SqlDbType.Money,
                        ParameterName = "@price",
                        Value = price
                    };
                    command.Parameters.Add(priceParameter);
                    WriteLine("Text");
                    break;
                case ConsoleKey.D2:
                    command.CommandText = "GetExpensiveProducts";
                    command.CommandType = CommandType.StoredProcedure;
                    priceStoParameter = new()
                    {
                        SqlDbType = SqlDbType.Money,
                        ParameterName = "@price",
                        Value = price,
                        Direction = ParameterDirection.Input
                    };
                    count = new()
                    {
                        SqlDbType = SqlDbType.Int,
                        ParameterName = "@count",
                        Direction = ParameterDirection.Output
                    };
                    rt = new()
                    {
                        SqlDbType = SqlDbType.Int,
                        ParameterName = "@rt",
                        Direction = ParameterDirection.ReturnValue
                    };
                    command.Parameters.Add(priceStoParameter);
                    command.Parameters.Add(count);
                    command.Parameters.Add(rt);
                    WriteLine("Stored Procedure");
                    break;
                default:
                    WriteLine("Invalid option");
                    break;
            }

            WriteLine();

            // Execute command
            WriteLine("{0,-40} | {1,-20} | {2,-10}", "ProductName", "UnitPrice", "UnitsInStock");
            await using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                WriteLine("{0,-40} | {1,-20:C} | {2,-10}", reader["ProductName"], reader["UnitPrice"],
                    reader["UnitsInStock"]);
            }

            await reader.CloseAsync();

            // After Close
            WriteLine($"Output count: {count?.Value}");
            WriteLine($"Return value: {rt?.Value}");
        }
    }
}