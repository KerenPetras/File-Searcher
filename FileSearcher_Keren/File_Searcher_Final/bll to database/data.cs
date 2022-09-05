using System;
using System.Data.SqlClient;

namespace bll_to_database
{
    public class data //connection to the database and inserting info by the selected tables
    {
        SqlConnection connect = new SqlConnection(@"Data Source=DESKTOP-1P8R2F7\SQLEXPRESS01;Initial Catalog=SearchList;Integrated Security=True");
        SqlCommand insertcomm = new SqlCommand();
        SqlCommand selectComm = new SqlCommand();
        private int id;
        public data()
        {
            insertcomm.Connection = connect;
            selectComm.Connection = connect;
        }

        public void InsertSearches(string name,string location)
        {
            insertcomm.CommandText = $"insert Searches values('{name}','{location}')";
            selectComm.CommandText = $"select searchID from Searches where fileName = '{name}' and fileLocation = '{location}'";
            connect.Open();
            insertcomm.ExecuteNonQuery();
            id =int.Parse(selectComm.ExecuteScalar().ToString());
            connect.Close();
        }

        public void insertResults(string name, string location)
        {
            insertcomm.CommandText = $"insert Results values({id},'{name}','{location}')";
            connect.Open();
            insertcomm.ExecuteNonQuery();
            connect.Close();

        }





    }
}
