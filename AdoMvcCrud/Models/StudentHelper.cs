using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace AdoMvcCrud.Models
{
    public class StudentHelper
    {
        private string Connection;
        private SqlConnection con;

        public StudentHelper()
        {
            Connection = ConfigurationManager.ConnectionStrings["AdoMvc"].ConnectionString;
            con = new SqlConnection(Connection);

        }

        public List<Students> LoadAll(out bool status)
        {
            List<Students> list = new List<Students>();

            try
            {
                SqlCommand cmd = new SqlCommand("select * from Students", con);

                con.Open();
                SqlDataReader Readr = cmd.ExecuteReader();

                while (Readr.Read())
                {
                    Students s = new Students();

                    s.Id = (int)Readr["id"];
                    s.Name = Readr["Name"].ToString();
                    s.Gender = Readr["Gender"].ToString();
                    s.Age = (int)Readr["Age"];
                    s.DateOfBirth = (DateTime)Readr["DateOfBirth"];

                    list.Add(s);
                }

                status = true;

            }
            catch (Exception ex)
            {
                status = false;
            }
            finally
            {
                con.Close();
            }

            return list;


        }
        public List<Students> LoadAllWithAdaper(out bool status)
        {
            List<Students> list = new List<Students>();

            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter("select * from Students",con);
                DataSet ds = new DataSet();
                adapter.Fill(ds,"Std");

                if (ds != null && ds.Tables["Std"] != null && ds.Tables["Std"].Rows != null && ds.Tables["Std"].Rows.Count > 0)
                {
                    

                    for (int i = 0; i <ds.Tables["Std"].Rows.Count; i++)
                    {
                        Students st = new Students();
                        DataRow row = ds.Tables["Std"].Rows[i];

                        st.Id =(int)row["id"];
                        st.Name = row["Name"].ToString();
                        st.Gender = row["Gender"].ToString();
                        st.Age = (int)row["Age"];
                        st.DateOfBirth =(DateTime)row["DateOfBirth"];

                        list.Add(st);
                    }

                }

                status = true;

            }
            catch (Exception ex)
            {
                status = false;
            }
            finally
            {
                con.Close();
            }

            return list;


        }
        public bool Create(Students s,out string status)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("usp_Insert", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Name", s.Name);
                cmd.Parameters.AddWithValue("@Gender", s.Gender);
                cmd.Parameters.AddWithValue("@Age", s.Age);
                cmd.Parameters.AddWithValue("@DateOfBirth", s.DateOfBirth);

                SqlParameter IdOut = new SqlParameter();
                IdOut.ParameterName = "@Out";
                IdOut.SqlDbType = System.Data.SqlDbType.Int;
                IdOut.Direction = System.Data.ParameterDirection.Output;
                cmd.Parameters.Add(IdOut);
                
                
                con.Open();
                int number = cmd.ExecuteNonQuery();
                status = IdOut.Value.ToString();
                return true;

            }
            catch (Exception ex)
            {

                status = 0.ToString();
                return false;
            }
            finally
            {
                con.Close();
            }


        }
        public bool CreateAdapter(Students s,out string status)
        {
            try
            {
                SqlDataAdapter cmd = new SqlDataAdapter();
                cmd.InsertCommand = new SqlCommand("usp_Insert",con);
                cmd.InsertCommand.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.InsertCommand.Parameters.AddWithValue("@Name", s.Name);
                cmd.InsertCommand.Parameters.AddWithValue("@Gender", s.Gender);
                cmd.InsertCommand.Parameters.AddWithValue("@Age", s.Age);
                cmd.InsertCommand.Parameters.AddWithValue("@DateOfBirth", s.DateOfBirth);

                SqlParameter IdOut = new SqlParameter();
                IdOut.ParameterName = "@Out";
                IdOut.SqlDbType = System.Data.SqlDbType.Int;
                IdOut.Direction = System.Data.ParameterDirection.Output;
                cmd.InsertCommand.Parameters.Add(IdOut);

                con.Open();
                cmd.InsertCommand.ExecuteNonQuery();
                status = IdOut.Value.ToString();


                return true;

            }
            catch (Exception ex)
            {

                status = 0.ToString();
                return false;
            }
            finally
            {
                con.Close();
            }


        }
        public bool Update(int id, Students s)
        {

            try
            {
                SqlCommand cmd = new SqlCommand("usp_Update", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Name", s.Name);
                cmd.Parameters.AddWithValue("@Gender", s.Gender);
                cmd.Parameters.AddWithValue("@Age", s.Age);
                cmd.Parameters.AddWithValue("@DateOfBirth", s.DateOfBirth);
                cmd.Parameters.AddWithValue("@Id", id);

                con.Open();
                cmd.ExecuteNonQuery();

                return true;

            }
            catch (Exception ex)
            {

                return false;
            }
            finally
            {
                con.Close();
            }
            



        }
        public bool Delete(int id)
        {

            try
            {
                SqlCommand cmd = new SqlCommand("delete from Students where id=@Id", con);
                cmd.Parameters.AddWithValue("@Id", id);

                con.Open();
                cmd.ExecuteNonQuery();

                return true;

            }
            catch (Exception ex)
            {

                return false;
            }
            finally
            {
                con.Close();
            }




        }
        public Students Details(int id)
        {
            Students ss = new Students();

            try
            {
                SqlCommand cmd = new SqlCommand("select * from Students where id=@Id", con);
                cmd.Parameters.AddWithValue("@Id", id);

                con.Open();
                SqlDataReader Reader=cmd.ExecuteReader();

                while (Reader.Read())
                {
                    ss.Name = Reader["Name"].ToString();
                    ss.Gender = Reader["Gender"].ToString();
                    ss.Age = (int)Reader["Age"];
                    ss.DateOfBirth = (DateTime)Reader["DateOfBirth"];
                }

                

            }
            catch (Exception ex)
            {

                
            }
            finally
            {
                con.Close();
                
            }

            return ss;
        }



    }
            
}