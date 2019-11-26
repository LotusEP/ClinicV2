using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClinicV2.Models
{
    public class Criteria
    {
        public int CriteriaID { get; set; }
        public string clinicName { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }


        public static List<Criteria> GetReqList(int ID)
        {
            List<Criteria> listofReq = new List<Criteria>();

            string connString;
            MySqlConnection cnn;
            //connString = @"Data Source=clinicserver1.database.windows.net;Initial Catalog=Patient;User ID=Lotus;Password=Server1@pass;Connect Timeout=60;Encrypt=True;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";


            connString = @"Server=clinicsystemdb.cfkpw0ap0abf.us-east-1.rds.amazonaws.com;user id=Lotusep5ep; Pwd=Pat123forsell; database=ClinicSysDB";

            cnn = new MySqlConnection(connString);

            MySqlDataReader rdr = null;

            cnn.Open();
            if (ID != -10)
            {
                string sql = "Select Criteria.Name, CriteriaOption.CriteriaValue From ClinicCriteria " +
                       "Join Criteria on Criteria.CriteriaID = ClinicCriteria.CriteriaID " +
                       "Join CriteriaOption on CriteriaOption.CriteriaOptionID = ClinicCriteria.CriteriaOptionID " +
                       "Where ClinicID = " + ID;

                MySqlCommand cmd = new MySqlCommand(sql, cnn);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {

                    listofReq.Add(new Criteria
                    {

                        Name = rdr.GetValue(0).ToString(),
                        Value = rdr.GetValue(1).ToString()


                    });
                }


            }
            else
            {
                string sql = "Select Clinic.Name, Criteria.Name, CriteriaOption.CriteriaValue From ClinicCriteria " +
                "Join Clinic On Clinic.ClinicID = ClinicCriteria.ClinicID " +
                "Join Criteria on Criteria.CriteriaID = ClinicCriteria.CriteriaID " +
                "Join CriteriaOption on CriteriaOption.CriteriaOptionID = ClinicCriteria.CriteriaOptionID ORDER BY Clinic.Name";

                MySqlCommand cmd = new MySqlCommand(sql, cnn);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {

                    listofReq.Add(new Criteria
                    {
                        clinicName = rdr.GetValue(0).ToString(),
                        Name = rdr.GetValue(1).ToString(),
                        Value = rdr.GetValue(2).ToString()


                    }) ;
                }


            }




            cnn.Close();


            return listofReq;
        }

        public void CreateCriteria()
        { 
        }

    }
}