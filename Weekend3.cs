using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ChatSystem
{
    class Program
    {
        private static string connectionString = @"Server=DESKTOP-7GABPVA\SQLEXPRESS;Database=EmployeeDB;Trusted_Connection=True;";

        static async Task Main(string[] args)
        {
            Console.WriteLine("=== Simple Chat System ===");

            while (true)
            {
                Console.Write("Sender: ");
                string sender = Console.ReadLine();

                Console.Write("Receiver: ");
                string receiver = Console.ReadLine();

                Console.Write("Message: ");
                string message = Console.ReadLine();

                await SaveChatMessageAsync(sender, receiver, message);
                Console.WriteLine("Message sent!\n");

                Console.Write("View recent messages for (receiver): ");
                string user = Console.ReadLine();

                var messages = await GetRecentMessagesAsync(user);
                Console.WriteLine($"\n--- Recent Messages for {user} ---");
                foreach (var msg in messages)
                {
                    Console.WriteLine(msg);
                }

                Console.WriteLine("\nPress Enter to continue or type 'exit' to quit.");
                if (Console.ReadLine().Trim().ToLower() == "exit")
                    break;
            }
        }

        public static async Task SaveChatMessageAsync(string sender, string receiver, string message)
        {
            string query = @"INSERT INTO ChatMessages (Sender, Receiver, MessageText)
                             VALUES (@Sender, @Receiver, @MessageText);";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                await conn.OpenAsync();

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Sender", sender);
                    cmd.Parameters.AddWithValue("@Receiver", receiver);
                    cmd.Parameters.AddWithValue("@MessageText", message);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public static async Task<List<string>> GetRecentMessagesAsync(string user)
        {
            List<string> messages = new List<string>();
            string query = @"SELECT Sender, MessageText, SentAt 
                             FROM ChatMessages 
                             WHERE Receiver = @Receiver
                             ORDER BY SentAt DESC";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                await conn.OpenAsync();

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Receiver", user);

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            string sender = reader.GetString(0);
                            string text = reader.GetString(1);
                            DateTime sentAt = reader.GetDateTime(2);

                            messages.Add($"From {sender} at {sentAt:HH:mm:ss}: {text}");
                        }
                    }
                }
            }

            return messages;
        }
    }
}
