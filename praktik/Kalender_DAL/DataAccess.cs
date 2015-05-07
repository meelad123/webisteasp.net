using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kalender_DTO;
using System.Data.SqlClient;

namespace Kalender_DAL
{
    public class DataAccess
    {
        #region DA for Activities

        public static List<Activity_DTO> getAllActivities()
        {
            string SQL = "SELECT * FROM Kalender";
            List<Activity_DTO> results = new List<Activity_DTO>();
            string _connectionString = DataSource.GetConnectionString("kalender");
            SqlConnection con = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(SQL, con);
            try
            {
                con.Open();
                SqlDataReader dar = cmd.ExecuteReader();
                while (dar.Read())
                {
                    if (Convert.ToDateTime(dar["Date"]) >= DateTime.Now)
                    {
                        Activity_DTO newA = new Activity_DTO();
                        if (!(dar["Aktivitet"] is DBNull))
                            newA._activity = dar["Aktivitet"] as string;
                        else newA._activity = "Finns ej";
                        newA._date = Convert.ToDateTime(dar["Date"]);
                        if (!(dar["Arrangor"] is DBNull))
                            newA._arranger = dar["Arrangor"] as string;
                        else newA._arranger = "Finns ej";
                        if (!(dar["Ort"] is DBNull))
                            newA._ort = dar["Ort"] as string;
                        else newA._ort = "Finns ej";
                        if (!(dar["Namn"] is DBNull))
                            newA._name = dar["Namn"] as string;
                        else newA._name = "Finns ej";
                        if (!(dar["Tel"] is DBNull))
                            newA._tel = dar["Tel"] as string;
                        else newA._tel = "Finns ej";
                        if (!(dar["Email"] is DBNull))
                            newA._email = dar["Email"] as string;
                        else newA._email = "Finns ej";
                        if (!(dar["Hemsida"] is DBNull))
                            newA._hemsida = dar["Hemsida"] as string;
                        else newA._hemsida = "Finns ej";
                        if (!(dar["MerInfo"] is DBNull))
                            newA._merinfo = dar["MerInfo"] as string;
                        else newA._merinfo = "Finns ej";
                        results.Add(newA);
                    }
                }
            }
            catch (Exception er)
            {
                throw er;
            }
            finally
            {
                con.Close();
            }
            return results;
        }

        public static List<string> getActivityName()
        {
            string SQL = "SELECT * FROM Aktivitet";
            List<string> results = new List<string>();
            string _connectionString = DataSource.GetConnectionString("kalender");
            SqlConnection con = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(SQL, con);
            try
            {
                con.Open();
                SqlDataReader dar = cmd.ExecuteReader();
                while (dar.Read())
                {
                    string newA;
                    if (!(dar["Activitet"] is DBNull))
                        newA = Convert.ToString(dar["Activitet"]).Trim();
                    else newA = "Finns ej";
                    results.Add(newA);
                }
            }
            catch (Exception er)
            {
                throw er;
            }
            finally
            {
                con.Close();
            }
            return results;
        }

        public static void insertActivity(Activity_DTO a) 
        {
            string SQLInsertToBook = "INSERT INTO Kalender([Date],[Aktivitet],[Arrangor],[Ort],[Namn],[Tel],[Email],[Hemsida],[MerInfo]) " +
                            "Values ('" + a._date + "', '" + a._activity + "','" + a._arranger + "','" + a._ort + "','" + a._name + "','" + a._tel
                            + "','" + a._email + "','" + a._hemsida + "','" + a._merinfo + "' ) ";
            string _connectionString = DataSource.GetConnectionString("kalender");
            SqlConnection con = new SqlConnection(_connectionString);
            SqlCommand cmdInsertB = new SqlCommand(SQLInsertToBook, con);
            try
            {
                con.Open();
                cmdInsertB.ExecuteNonQuery();
            }
            catch (Exception er)
            {
                throw er;
            }
            finally
            {
                con.Close();
            }
        }

        public static UserDTO checkUser(UserDTO u)
        {
            UserDTO toreturn;
            string SQL = "SELECT * FROM users where username='" + u._username + "';";
            string _connectionString = DataSource.GetConnectionString("kalender");
            SqlConnection con = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(SQL, con);
            try
            {
                con.Open();
                SqlDataReader dar = cmd.ExecuteReader();
                if (dar.HasRows)
                {
                    UserDTO newUser = new UserDTO();
                    while (dar.Read())
                    {
                        newUser._username = dar["userName"].ToString();
                        newUser._password = dar["password"].ToString();
                        newUser._type = dar["role"].ToString();
                        newUser.userID = dar["userID"].ToString();
                        newUser.passwordSalt = dar["passwordSalt"].ToString();
                    }
                    toreturn = newUser;
                }
                else
                    toreturn = null;
            }
            catch (Exception er)
            {
                throw er;
            }
            finally
            {
                con.Close();
            }
            return toreturn;
        }

        public static void createUser(UserDTO u)
        {
            string SQL = "INSERT INTO [dbo].[users]([userID],[userName],[password],[passwordSalt],[role]) " +
                         "VALUES" + " ('" + u.userID + "', '" + u._username + "', '" + u._password + "', '" + u.passwordSalt + "', '" + u._type + "');";
            string _connectionString = DataSource.GetConnectionString("kalender");
            SqlConnection con = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(SQL, con);

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception er)
            {
                throw er;
            }
            finally
            {
                con.Close();
            }
        }

        public static Activity_DTO getActivityByName(string aNamn, DateTime aDatum)
        {
            Activity_DTO a = new Activity_DTO();
            string SQL = "SELECT * FROM Kalender" +
                " WHERE Kalender.Arrangor = " + "'" + aNamn + "'" + "AND Kalender.Date= '" + aDatum + "';";
            string _connectionString = DataSource.GetConnectionString("kalender");
            SqlConnection con = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(SQL, con);
            try
            {
                con.Open();
                SqlDataReader dar = cmd.ExecuteReader();
                while (dar.Read())
                {
                    a._activity = dar["Aktivitet"] as string;
                    a._arranger = dar["Arrangor"] as string;
                    a._date = Convert.ToDateTime(dar["Date"]);
                    a._email = dar["Email"] as string;
                    a._hemsida = dar["Hemsida"] as string;
                    a._merinfo = dar["MerInfo"] as string;
                    a._name = dar["Namn"] as string;
                    a._ort = dar["Ort"] as string;
                    a._tel = dar["Tel"] as string;
                }
            }
            catch (Exception er)
            {
                throw er;
            }
            finally
            {
                con.Close();
            }
            return a;

        }

        public static void saveActivity(Activity_DTO b)
        {
            string SQL = "UPDATE Kalender SET Date='" +
                b._date + "', Aktivitet='" + b._activity + "', Arrangor='"
                + b._arranger + "', Ort='" + b._ort +
                "', Namn='" + b._name + "', Tel='" + b._tel + "', Email='" + b._email +
                "', Hemsida='" + b._hemsida + "', MerInfo='" + b._merinfo + "' WHERE Date='" + b._date +
                "' AND Arrangor='" + b._arranger + "';";
            string _connectionString = DataSource.GetConnectionString("kalender");
            SqlConnection con = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(SQL, con);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception er)
            {
                throw er;
            }
            finally
            {
                con.Close();
            }
        }

        public static void removeActivity(Activity_DTO b)
        {
            string SQL = "DELETE FROM Kalender" +
                         " WHERE Date='" + b._date +
                        "' AND Arrangor='" + b._arranger + "';";
            string _connectionString = DataSource.GetConnectionString("kalender");
            SqlConnection con = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(SQL, con);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception er)
            {
                throw er;
            }
            finally
            {
                con.Close();
            }
        }
        #endregion
    }
}
