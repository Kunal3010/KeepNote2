using System.Data;
using System.Data.SqlClient;
namespace KeepNote2
{
    class Management
    {
        public static void AddNote(string T, string desc, SqlConnection con)
        {
            SqlDataAdapter adp = new SqlDataAdapter("Select * from Notes", con);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            var row = ds.Tables[0].NewRow();
            row["Title"] = T;
            row["Descriptions"] = desc;

            ds.Tables[0].Rows.Add(row);

            SqlCommandBuilder builder = new SqlCommandBuilder(adp);
            adp.Update(ds);
            Console.WriteLine("Database Updated");
        }

        public static void GetNotes(SqlConnection con)
        {
            SqlDataAdapter adp = new SqlDataAdapter("Select * from Notes", con);
            DataSet ds = new DataSet();
            adp.Fill(ds, "Notes");
            for (int i = 0; i < ds.Tables["Notes"].Rows.Count; i++)
            {
                for (int j = 0; j < ds.Tables["Notes"].Columns.Count; j++)
                {
                    Console.Write($"{ds.Tables["Notes"].Rows[i][j]} | ");
                }
                Console.WriteLine();
            }
        }

        public static void UpdateNote(int Id, SqlConnection con)
        {
            SqlDataAdapter adp = new SqlDataAdapter($"Select * from Notes where Id ={Id}", con);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            Console.WriteLine("Enter New Title: ");
            string str1 = Console.ReadLine();
            Console.WriteLine("Enter New Description: ");
            string str2 = Console.ReadLine();
            ds.Tables[0].Rows[0][1] = $"{str1}";
            ds.Tables[0].Rows[0][2] = $"{str2}";

            SqlCommandBuilder builder = new SqlCommandBuilder(adp);
            adp.Update(ds);
            Console.WriteLine("Database Updated");
        }

        public static void GetNote(int Id, SqlConnection con)
        {
            SqlDataAdapter adp = new SqlDataAdapter($"Select * from Notes where Id = {Id}", con);
            DataSet ds = new DataSet();
            adp.Fill(ds, "Notes");
            for (int i = 0; i < ds.Tables["Notes"].Rows.Count; i++)
            {
                for (int j = 0; j < ds.Tables["Notes"].Columns.Count; j++)
                {
                    Console.Write($"{ds.Tables["Notes"].Rows[i][j]} | ");
                }
                Console.WriteLine();
            }
        }

        public static void DeleteNote(int Id, SqlConnection con)
        {
            SqlDataAdapter adp = new SqlDataAdapter($"Select * from Notes where Id ={Id}", con);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            ds.Tables[0].Rows[0].Delete();

            SqlCommandBuilder builder = new SqlCommandBuilder(adp);
            adp.Update(ds);
            Console.WriteLine("Entry Deleted");
        }

    }
    internal class Program
    {
        static void Main(string[] args)
        {
            SqlConnection con = new SqlConnection("Server= US-513K9S3; database=keepNotes; Integrated Security=true");
            string ans = "";
            string title = "";
            string description = "";
            int id;
            do
            {
                Console.WriteLine("Welcome to Keep Note App");
                Console.WriteLine("1. Create Note");
                Console.WriteLine("2. View Notes");
                Console.WriteLine("3. View All Notes");
                Console.WriteLine("4. Update Note");
                Console.WriteLine("5. Delete Note");
                Console.WriteLine();
                int choice = Convert.ToInt16(Console.ReadLine());
                Console.WriteLine();
                switch (choice)
                {
                    case 1:
                        {
                            Console.WriteLine("Enter the Title: ");
                            title = Console.ReadLine();
                            Console.WriteLine("Enter the Description: ");
                            description = Console.ReadLine();
                            Management.AddNote(title,description,con);
                            break;
                        }
                    case 2:
                        {
                            Management.GetNotes(con);
                            break;
                        }
                    case 3:
                        {
                            Console.WriteLine("Enter the note id you want to search");
                            id = Convert.ToInt16(Console.ReadLine());
                            Management.GetNote(id,con);
                            break;
                        }
                    case 4:
                        {
                            Console.WriteLine("Enter the note id you want to Update");
                            id = Convert.ToInt16(Console.ReadLine());
                            Management.UpdateNote(id,con);
                            break;
                        }
                    case 5:
                        {
                            Console.WriteLine("Enter the note id you want to Delete");
                            id = Convert.ToInt16(Console.ReadLine());
                            Management.DeleteNote(id,con);
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Invalid choice!!!!");
                            break;
                        }
                }
                Console.WriteLine();
                Console.WriteLine("Do you wish to continue? [y/n] ");
                ans = Console.ReadLine();
                Console.WriteLine();

            } while (ans.ToLower() == "y");
        }
    }
}
