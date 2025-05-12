using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace AsyncEmployeeQuery
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            Console.WriteLine("Fetching recent employee records asynchronously...");

            string connectionString = @"Server=DESKTOP-7GABPVA\SQLEXPRESS;Database=EmployeeDB;Trusted_Connection=True;";
            await FetchEmployeesJoinedLastSixMonthsAsync(connectionString);

            Console.WriteLine("Query completed. Press any key to exit.");
            Console.ReadKey();
        }

        private static async Task FetchEmployeesJoinedLastSixMonthsAsync(string connectionString)
        {
            string query = @"
                SELECT EmployeeID, FirstName, LastName, JoinDate 
                FROM Employees1
                WHERE JoinDate >= DATEADD(MONTH, -6, GETDATE())
                ORDER BY JoinDate DESC";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    await conn.OpenAsync();

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                int id = reader.GetInt32(0);
                                string fname = reader.GetString(1);
                                string lname = reader.GetString(2);
                                DateTime joinDate = reader.GetDateTime(3);

                                Console.WriteLine($"{id} - {fname} {lname}, Joined on {joinDate:d}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error during async query: " + ex.Message);
            }
        }
    }
}
