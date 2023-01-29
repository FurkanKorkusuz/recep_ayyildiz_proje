using Microsoft.Data.SqlClient;
using recep_ayyildiz.Entities;
using recep_ayyildiz_proje.Models;
using System;
using System.Collections.Generic;

namespace recep_ayyildiz.DataAccess
{
    public class PersonelDB
    {

        private string _connectionString = "Data Source=89.252.180.81\\MSSQLSERVER2016;Initial Catalog=MyTestDB;User Id=furkank7_test;Password=MyTestDB_123;TrustServerCertificate=True";

        public void AddLog(PersonalLog personalLog)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("INSERT INTO PersonalLogs (PersonalID, State, Date) VALUES (@PersonalID, @State, @Date)", connection))
                {
                    command.Parameters.AddWithValue("@PersonalID", personalLog.PersonalID);
                    command.Parameters.AddWithValue("@State", personalLog.State);
                    command.Parameters.AddWithValue("@Date", DateTime.Now);
                    command.ExecuteNonQuery();
                }
            }

        }

        public Personal AddPersonel(Personal personal)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("INSERT INTO Personals (FirstName, LastName) OUTPUT INSERTED.ID VALUES (@FirstName, @LastName)", connection))
                {
                    command.Parameters.AddWithValue("@FirstName", "FirstName");
                    command.Parameters.AddWithValue("@LastName", "LastName");
                    personal.ID = (int)command.ExecuteScalar();
                }
            }
            return personal;

        }

        public List<PersonalLogViewDto> GetPersonalLog(PersonalLog filter)
        {
             List<PersonalLogViewDto> personalLogs= new List<PersonalLogViewDto>();
            PersonalLogViewDto personalLog;

            string flt = "";
            if (filter.PersonalID>0)
            {
                flt += $" AND PersonalID=@PersonalID";
            }

            string strSql = @$"SELECT pl.*, FirstName, LastName
                            FROM PersonalLogs  pl
                            left join Personals p on p.ID = pl.PersonalID 
                            WHERE 1=1 {flt} 
                            ORDER BY ID DESC";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(strSql, connection))
                {
                    command.Parameters.AddWithValue("@PersonalID", "PersonalID");
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        personalLog = new PersonalLogViewDto();
                        personalLog.ID = (int)reader["ID"];
                        personalLog.PersonalID = (int)reader["PersonalID"];
                        personalLog.State = (byte)reader["State"] == 1 ? "Giriş" : "Çıkış";
                        personalLog.Date = (DateTime)reader["Date"];
                        personalLog.FirstName = reader["FirstName"] != DBNull.Value ?  reader["FirstName"].ToString() : "";
                        personalLog.LastName = reader["LastName"] != DBNull.Value ? reader["LastName"].ToString() : "";
                        personalLogs.Add(personalLog);
                    }
                }
            }
            return personalLogs;


          
        }
    }
}
