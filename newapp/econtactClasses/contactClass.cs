using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace newapp.econtactClasses
{
    class contactClass
    {
        //Getter Setter Properties
        //Acts as Data Carries in our Application
        public int ContactID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }

        static string myconnstrng = ConfigurationManager.ConnectionStrings["connstrng"].ConnectionString;

        //Selecting data from database
        public DataTable Select()
        {
            //Step 1 Database Connection 
            SqlConnection conn = new SqlConnection(myconnstrng);
            DataTable dt = new DataTable();
            try
            {
                //Step 2 writing SQL query
                String sql = "SELECT * FROM tbl_contact";
                //Creating cmd using sql and conn 
                SqlCommand cmd = new SqlCommand(sql, conn);
                //Creating SQL DataAdapter using cmd
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                cmd.Connection = conn;

                conn.Open();
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }
        //Inserting data into database
        public bool Insert(contactClass c)
        {
            //Creating default return type and setting its value to false
            bool isSuccess = false;

            //Step 1 Connect database 
            SqlConnection conn = new SqlConnection(myconnstrng);
            try {
                //Step 2 Creating SQL query to insert data
                string sql = "INSERT INTO tbl_contact(FirstName, LastName, ContactNo, Address, Gender) VALUES (@FirstName, @LastName, @ContactNo,@Address, @Gender)";

                //Creating sql command using sql and conn
                SqlCommand cmd = new SqlCommand(sql, conn);
                //Create parameter to add data
                cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
                cmd.Parameters.AddWithValue("@LastName", c.LastName);
                cmd.Parameters.AddWithValue("@ContactNo", c.ContactNo);
                cmd.Parameters.AddWithValue("@Address", c.Address);
                cmd.Parameters.AddWithValue("@Gender", c.Gender);
                cmd.Connection = conn;

                //Connection open
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                //If the query runs then the value of rows will be > 0 else it will be 0
                if(rows>0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }

            }
            catch (Exception ex){
                Console.WriteLine(ex);
            }
            finally
            {

                conn.Close();
            }
            return isSuccess;
        }
        //Method to update data in database from our application
        public bool Update(contactClass c)
        {
            //Create a default return type and set it to false
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                //Sql to update data in database
                string sql = "UPDATE tbl_contact SET FirstName=@FirstName, LastName=@LastName, ContactNo= @ContactNo, Address=@Address, Gender=@Gender WHERE ContactID=@ContactID";

                //Create sql command to insert data
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("FirstName",c.FirstName);
                cmd.Parameters.AddWithValue("@LastName", c.LastName);
                cmd.Parameters.AddWithValue("@ContactNo", c.ContactNo);
                cmd.Parameters.AddWithValue("@Address", c.Address);
                cmd.Parameters.AddWithValue("@Gender", c.Gender);
                cmd.Parameters.AddWithValue("ContactID", c.ContactID);
                cmd.Connection = conn;

                //open database connection
                conn.Open();

                int rows = cmd.ExecuteNonQuery();
                //IF the query runs then the value of rows will be > 0 else it will be 0
                if (rows>0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }
        //Method to delete data
        public bool Delete(contactClass c)
        {
            //default return value
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(myconnstrng);
            try
            {
                //Sql to delete data in database
                string sql = "DELETE FROM tbl_contact WHERE ContactID=@ContactID";

                //Create sql command
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ContactID", c.ContactID);
                cmd.Connection = conn;

                //open database connection
                conn.Open();

                int rows = cmd.ExecuteNonQuery();
                //IF the query runs then the value of rows will be > 0 else it will be 0
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }
    }
}
