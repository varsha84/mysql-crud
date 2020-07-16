using System;
using MySql.Data.MySqlClient;

namespace DbConnect
{
    class Program
    {
        public static void createRecord(MySqlCommand cmd)
        {
            Console.WriteLine("create a record");
            Console.WriteLine("Enter name:");
            string newName = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Enter value:");
            int newPrice = Convert.ToInt32(Console.ReadLine());
            cmd.CommandText  = $"INSERT INTO cars(name, price) VALUES(\"{newName}\",{newPrice})";
            cmd.ExecuteNonQuery();
            Console.WriteLine("row inserted");

        }
        public static void readRecord(MySqlCommand cmd)
        {
            Console.WriteLine("Read last 5 records");
            cmd.CommandText = "SELECT * FROM cars ORDER BY id DESC LIMIT 5";
            using MySqlDataReader rdr= cmd.ExecuteReader(); 
            // while loop to print records
            while (rdr.Read())
            {
                Console.WriteLine("{0} {1} {2}", rdr.GetInt32(0), rdr.GetString(1), 
                        rdr.GetInt32(2));
            }

        }
        public static void delRecord(MySqlCommand cmd)
        {
            Console.WriteLine("Delete a record by name");
            Console.WriteLine("enter name:");
            string newName = Convert.ToString(Console.ReadLine());
            cmd.CommandText = $"delete from cars where name = \"{newName}\"";
            cmd.ExecuteNonQuery(); 
        }
        static void Main(string[] args)
        {
            string cs = @"server=localhost;userid=varsha;password=var1234;database=testdb";

            using var dbConnection = new MySqlConnection(cs);
            dbConnection.Open(); //open connection to  a database

            using var sqlCmd = new MySqlCommand(); 
            sqlCmd.Connection = dbConnection;
            sqlCmd.CommandText = ""; // this must contain a SQL statment,  before call execute method of MySqlCommand class

            while(true)
            {
                Console.WriteLine("1. Create a record");
                Console.WriteLine("2. Read last 10 Records:");
                Console.WriteLine("3. Delete a Record by name");
                Console.WriteLine("4. Exit the program");
                Console.WriteLine("Please Select a option:");

                var  input = Console.ReadLine();
                Console.WriteLine("Entered subject = "+input);
                
                if(input== "1"){
                    createRecord(sqlCmd);
                }        
                else if(input== "2")
                {
                    readRecord(sqlCmd);
                }
                else if(input== "3")
                {
                    delRecord(sqlCmd);
                }
                else if(input== "4")
                {                     
                    Console.WriteLine("Exit the program");
                    break;
                }
                else{
                    Console.WriteLine("Choose correct option");
                }
            }// while loop end 
        }
    }
}
