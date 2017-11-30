using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using MVCproject_Zoo.Models;

namespace MVCproject_Zoo.DAO
{
    public class RecordsDAO : DAO
    {
        public List<Animals> GetAllRecords()
        {
            Connect();
            List<Animals> recordList = new List<Animals>();
            try
            {
                SqlCommand command = new SqlCommand("SELECT *FROM Animals", sql);
                Log.For(this).Info("Getting records");
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Animals record = new Animals();
                    record.Id = Convert.ToInt32(reader["Id"]);
                    record.Name =Convert.ToString(reader["Name"]);
                    record.Information =Convert.ToString(reader["Information"]);
                    record.Count = Convert.ToInt32(reader["Count"]);
                    record.AddingDate =Convert.ToDateTime(reader["Date"]);
                    recordList.Add(record);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                recordList.Add(new Animals(ex.Message));
            }
            finally
            {
                Disconnect();
            }
            return recordList;
        }



        public bool AddRecord(Animals records)
        {
            bool result = true;
            Connect();
            try
            {
                Log.For(this).Info("Adding record");
                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO Animals (Name, Information, Count, Date) " +
                    "VALUES (@Name, @Information, @Count, @Date)", sql);
                cmd.Parameters.Add(new SqlParameter("@Name", records.Name));
                cmd.Parameters.Add(new SqlParameter("@Information", records.Information));
                cmd.Parameters.Add(new SqlParameter("@Count", records.Count));
                cmd.Parameters.Add(new SqlParameter("@Date", records.AddingDate));
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                result = false;
            }
            finally
            {
                Disconnect();
            }
            return result;
        }


        public void EditRecord(Animals record)
        {
            try
            {
                Log.For(this).Info("Edit record");
                Connect();
                SqlCommand cmd = new SqlCommand("UPDATE Animals SET Name = @Name, Information = @Information," +
                    "Count = @Count, Date = @Date WHERE Id = @Id", sql);
                cmd.Parameters.Add(new SqlParameter("@Name", record.Name));
                cmd.Parameters.Add(new SqlParameter("@Information", record.Information));
                cmd.Parameters.Add(new SqlParameter("@Count", record.Count));
                cmd.Parameters.Add(new SqlParameter("@Date", record.AddingDate));
                cmd.Parameters.Add(new SqlParameter("@Id", record.Id));
                cmd.ExecuteNonQuery();
            }
            finally
            {
                Disconnect();
            }
        }

        public Animals getById (int id)
        {
            Log.For(this).Info("Getting one record  by id");
            List<Animals> animals = GetAllRecords();
            foreach (var s in animals)
            {
                if (s.Id == id) { return s; }
            }
            return new Animals("Не найден объект с таким Id");
        }

        public void DeleteRecord(int id)
        {
            try
            {
                Log.For(this).Info("Delete record");
                Connect();
                SqlCommand cmd = new SqlCommand("DELETE FROM Animals WHERE Id = @Id", sql);
                cmd.Parameters.Add(new SqlParameter("@Id", id));
                cmd.ExecuteNonQuery();
            }
            finally
            {
                Disconnect();
            }
        }
    }
}