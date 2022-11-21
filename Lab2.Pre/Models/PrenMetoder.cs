using System.Data;
using System.Data.SqlClient;
using System;
using System.Xml.Linq;

namespace Lab2.Pre.Models
{
    public class PrenMetoder
    {
        public PrenMetoder() { }

        public List<Pren> SelectPrenLista(out string errormsg)
        {
            SqlConnection dbConnection = new SqlConnection();

            dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DBPren;Integrated Security=True"; // <- gå in på properties på databasen, under connection string

            //"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DBPren;Integrated Security=True

            String selectSQL = "SELECT * FROM [tbl_prenumerant]";

            SqlCommand dbCommand = new SqlCommand(selectSQL, dbConnection);

            SqlDataAdapter adapter = new SqlDataAdapter(dbCommand);
            DataSet test = new DataSet();

            SqlDataReader reader = null;
                                    
            

            List<Pren> plist = new List<Pren>();
            errormsg = "";

            // Exekvera SQL-strängen
            try
            {
                dbConnection.Open();
                //adapter.Fill(test, "tbl_prenumerant");

                //int count = 0;
                //int i = 0;
                //count = test.Tables["tbl_prenumerant"].Rows.Count;

                //if (count > 0)
                //{
                //    while (i < count){
                //        Pren p = new Pren();
                //        //p.pr_prennr = Convert.ToInt16(test.Tables["tbl_prenumerant"].Rows[i]["pr_prennr"]);
                //        //p.pr_fnamn = test.Tables["tbl_prenumerant"].Rows[i]["pr_fnamn"].ToString();
                //        //p.pr_efternamn = test.Tables["tbl_prenumerant"].Rows[i]["pr_efternamn"].ToString();
                //        //p.pr_telefonnr = test.Tables["tbl_prenumerant"].Rows[i]["pr_telefonnr"].ToString();
                //        //p.pr_utadress = test.Tables["tbl_prenumerant"].Rows[i]["pr_utadress"].ToString();
                //        //p.pr_ort = test.Tables["tbl_prenumerant"].Rows[i]["pr_ort"].ToString();
                //        //p.pr_personnr = test.Tables["tbl_prenumerant"].Rows[i]["pr_personnr"].ToString();
                //        p.pr_postnr = Convert.ToInt32(test.Tables["tbl_prenumerant"].Rows[i]["pr_postnr"]);    




                //        i++;
                //        plist.Add(p);
                //    } errormsg = "";
                //    return plist;
                //}
                //else
                //{
                //    errormsg = "Det blev fel";
                //    return (null);
                //}
                reader = dbCommand.ExecuteReader();
                while (reader.Read())
                {
                    Pren p = new Pren();
                    p.pr_prennr = Convert.ToInt16(reader["pr_prennr"]);
                    p.pr_fnamn = reader["pr_fnamn"].ToString();
                    p.pr_telefonnr = reader["pr_telefonnr"].ToString();
                    p.pr_utadress = reader["pr_utadress"].ToString();
                    p.pr_postnr = Convert.ToInt32(reader["pr_postnr"]);
                    p.pr_ort = reader["pr_ort"].ToString();
                    p.pr_personnr = reader["pr_personnr"].ToString();
                    p.pr_efternamn = reader["pr_efternamn"].ToString();
                    
                    
                    



                    plist.Add(p);
                   }
                    reader.Close();
                    return plist;
                }
            catch (Exception e)
            {
                errormsg = e.Message;
                return null;
            }
            finally
            {
                dbConnection.Close();
            }
        }

        public Pren SelectPrenumerant(out string errormsg, int id)
        {
            SqlConnection dbConnection = new SqlConnection();

            dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DBPren;Integrated Security=True;Pooling=False"; // <- gå in på properties på databasen, under connection string

            String selectSQL = "SELECT * FROM [tbl_prenumerant] WHERE [pr_prennr] = @id;";

            SqlCommand dbCommand = new SqlCommand(selectSQL, dbConnection);
            dbCommand.Parameters.Add("id", SqlDbType.Int).Value = id;

            SqlDataReader reader = null;

            errormsg = "";

            Pren p = new Pren();

            // Exekvera SQL-strängen
            try
            {
                dbConnection.Open();
                reader = dbCommand.ExecuteReader();
                while (reader.Read())
                {
                    p.pr_prennr = Convert.ToInt16(reader["pr_prennr"]);
                    p.pr_personnr = reader["pr_personnr"].ToString();
                    p.pr_fnamn = reader["pr_fnamn"].ToString();
                    p.pr_efternamn = reader["pr_efternamn"].ToString();
                    p.pr_telefonnr = reader["pr_telefonnr"].ToString();
                    p.pr_utadress = reader["pr_utadress"].ToString();
                    p.pr_postnr = Convert.ToInt32(reader["pr_postnr"]);
                    p.pr_ort = reader["pr_ort"].ToString();

                }


                reader.Close();
                return p;
            }
            catch (Exception e)
            {
                errormsg = e.Message;
                return null;
            }
            finally
            {
                dbConnection.Close();
            }
        }

        public int UpdatePrenumerant(out string error, Pren p)
        {
            // Skapa SQL-connection
            SqlConnection dbConnection = new SqlConnection();

            // Koppling mot SQL Server
            dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DBPren;Integrated Security=True;Pooling=False"; // <- gå in på properties på databasen, under connection string

            // SQL-sträng
            String updateSQL = "UPDATE [tbl_prenumerant] SET [pr_fnamn] = @fnamn, [pr_efternamn] = @efternamn, [pr_telefonnr] = @telefonnr, [pr_ort] = @ort, [pr_postnr] = @postnr, [pr_utadress] = @utadress WHERE [pr_prennr] = @id";

            // Lägg till en user
            SqlCommand dbCommand = new SqlCommand(updateSQL, dbConnection);

            dbCommand.Parameters.Add("fnamn", SqlDbType.NVarChar, 50).Value = p.pr_fnamn;
            dbCommand.Parameters.Add("efternamn", SqlDbType.NVarChar, 50).Value = p.pr_efternamn;
            dbCommand.Parameters.Add("telefonnr", SqlDbType.NVarChar, 50).Value = p.pr_telefonnr;
            dbCommand.Parameters.Add("ort", SqlDbType.NVarChar, 50).Value = p.pr_ort;
            dbCommand.Parameters.Add("postnr", SqlDbType.Int).Value = p.pr_postnr;
            dbCommand.Parameters.Add("id", SqlDbType.Int).Value = p.pr_prennr;
            dbCommand.Parameters.Add("utadress", SqlDbType.NVarChar, 50).Value = p.pr_utadress;

            // Exekvera SQL-strängen
            try
            {
                dbConnection.Open();
                int i = 0;
                i = dbCommand.ExecuteNonQuery();
                if (i == 1) { error = ""; }
                else { error = "Det uppdaterades inte i databasen."; }
                return (i);
            }
            catch (Exception e)
            {
                error = e.Message;
                return 0;
            }
            finally
            {
                dbConnection.Close();
            }
        }

        public int InsertPrenumerant(Pren pren, out string errormsg)
        {
            // Skapa SQL-connection
            SqlConnection dbConnection = new SqlConnection();



            // Koppling mot SQL Server
            dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DBPren;Integrated Security=True;Pooling=False"; // <- gå in på properties på databasen, under connection string



            // SQL-sträng
            String insertSQL = "INSERT INTO [tbl_prenumerant] ([pr_prennr], [pr_personnr], [pr_fnamn], [pr_efternamn], [pr_telefonnr], [pr_utadress], [pr_postnr], [pr_ort]) values (@prennr, @personnr, @fnamn, @efternamn, @telefonnr, @utadress, @postnr, @ort)";
                 
            // Lägg till en user
            SqlCommand dbCommand = new SqlCommand(insertSQL, dbConnection);
            dbCommand.Parameters.Add("prennr", SqlDbType.Int).Value = pren.pr_prennr;
            dbCommand.Parameters.Add("personnr", SqlDbType.NVarChar, 15).Value = pren.pr_personnr;
            dbCommand.Parameters.Add("fnamn", SqlDbType.NVarChar, 50).Value = pren.pr_fnamn;
            dbCommand.Parameters.Add("efternamn", SqlDbType.NVarChar, 50).Value = pren.pr_efternamn;
            dbCommand.Parameters.Add("telefonnr", SqlDbType.NVarChar, 50).Value = pren.pr_telefonnr;
            dbCommand.Parameters.Add("ort", SqlDbType.NVarChar, 50).Value = pren.pr_ort;
            dbCommand.Parameters.Add("postnr", SqlDbType.Int).Value = pren.pr_postnr;            
            dbCommand.Parameters.Add("utadress", SqlDbType.NVarChar).Value = pren.pr_utadress;
                        

            // Exekvera SQL-strängen
            try
            {
                dbConnection.Open();
                int i = 0;
                i = dbCommand.ExecuteNonQuery();
                if (i == 1) { errormsg = ""; }
                else { errormsg = "Det skapades inte en prenumerant i databasen."; }
                return (i);
            }
            catch (Exception e)
            {
                errormsg = e.Message;
                return 0;
            }
            finally
            {
                dbConnection.Close();
            }
        }
    }
}
