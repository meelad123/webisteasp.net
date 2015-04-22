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
                    Activity_DTO newA = new Activity_DTO();
                    if (!(dar["Aktivitet"] is DBNull))
                        newA._activity = dar["Aktivitet"] as string;
                    else newA._activity = "Finns ej";
                    newA._date = Convert.ToDateTime(dar["Date"]); //visa bara datum, tar bort tiden.
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
        #endregion
    }
}
