using AfterJackClampDropEntry.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace AfterJackClampDropEntry.Repository
{
    public class DataRepo
    {
        string connString = Properties.Settings.Default.conStr;

        string sqlString = "";
        SqlCommand mySqlCommand = new SqlCommand();

        SqlDataAdapter sqlAdapter = new SqlDataAdapter();
        DataSet ds = new DataSet();
        public static DataTable dt;

        //TODO: convert to stored procedure?    
        /// <summary>
        /// Get after jack clamp drops
        /// </summary>
        /// <returns></returns>
        public DataTable GetAfterjacks(DateTime entryDate, string room)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {

                if (room == "E" || room == "F")
                {
                    sqlString = "SELECT " +
                            "   ID " +
                            "   ,AfterJackID " +
                            "	,EntryDate " +
                            "	,Crew " +
                            "	,Room " +
                            "	,Pot " +
                            "	,[1] A1,[2] A2,[3] A3,[4] A4,[5] A5,[6] A6,[7] A7,[8] A8,[9] A9,[10] A10,[11] A11,[12] A12,[13] A13,[14] A14,[15] A15,[16] A16,[17] A17,[18] A18,[19] A19,[20] A20,[21] A21,[22] A22,[23] A23,[24] A24 " +
                            "FROM ( " +
                            "	SELECT  " +
                            "       main.ID  " +
                            "       ,AfterJackID " +
                            "		,EntryDate " +
                            "		,Crew " +
                            "		,Room " +
                            "		,Pot " +
                            "		,AnodeNum " +
                            "		,ClampDrop " +
                            "	FROM dbo.tblAfterJackMain main " +
                            "	LEFT JOIN dbo.tblAfterJackDetail detail " +
                            "	ON main.ID = detail.AfterJackID " +
                            "   WHERE EntryDate = '" + entryDate + "' " +
                            "       AND Room = '" + room + "' " +
                            ") as c " +
                            "PIVOT " +
                            "(SUM(ClampDrop) " +
                            "	FOR AnodeNum IN ([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],[18],[19],[20],[21],[22],[23],[24]) " +
                            ") as ClampDrops " +
                            "ORDER BY Room, Pot";
                }
                else
                {
                    sqlString = "SELECT " +
                            "   ID " +
                            "   ,AfterJackID " +
                            "	,EntryDate " +
                            "	,Crew " +
                            "	,Room " +
                            "	,Pot " +
                            "	,[1] A1,[2] A2,[3] A3,[4] A4,[5] A5,[6] A6,[7] A7,[8] A8,[9] A9,[10] A10,[11] A11,[12] A12,[13] A13,[14] A14,[15] A15,[16] A16,[17] A17,[18] A18 " +
                            "FROM ( " +
                            "	SELECT  " +
                            "       main.ID  " +
                            "       ,AfterJackID " +
                            "		,EntryDate " +
                            "		,Crew " +
                            "		,Room " +
                            "		,Pot " +
                            "		,AnodeNum " +
                            "		,ClampDrop " +
                            "	FROM dbo.tblAfterJackMain main " +
                            "	LEFT JOIN dbo.tblAfterJackDetail detail " +
                            "	ON main.ID = detail.AfterJackID " +
                            "   WHERE EntryDate = '" + entryDate + "' " +
                            "       AND Room = '" + room + "' " +
                            ") as c " +
                            "PIVOT " +
                            "(SUM(ClampDrop) " +
                            "	FOR AnodeNum IN ([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12],[13],[14],[15],[16],[17],[18]) " +
                            ") as ClampDrops " +
                            "ORDER BY Room, Pot";
                }
                

                mySqlCommand.CommandText = sqlString;

                mySqlCommand.Connection = conn;
                sqlAdapter.SelectCommand = mySqlCommand;

                sqlAdapter.Fill(ds);

                dt = ds.Tables[0].Copy();

                return dt;
            }
        }

        public void UpdateAfterJack(AfterJack afterJack)
        {
            sqlString = "INSERT INTO dbo.tblAfterJackMain (EntryDate, Crew, Room, Pot) VALUES " +
                        "(@entryDate, @crew, @room, @pot)";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand(sqlString, conn);
                try
                {
                    conn.Open();
                    cmd.Parameters.Add("@entryDate", SqlDbType.DateTime);
                    cmd.Parameters["@entryDate"].Value = afterJack.EntryDate.Date;
                    cmd.Parameters.Add("@crew", SqlDbType.Char);
                    cmd.Parameters["@crew"].Value = afterJack.Crew;
                    cmd.Parameters.Add("@room", SqlDbType.Char);
                    cmd.Parameters["@room"].Value = afterJack.Room;
                    cmd.Parameters.Add("@pot", SqlDbType.Int);
                    cmd.Parameters["@pot"].Value = afterJack.Pot;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void UpdateAfterJackDetail(AfterJackDetail[] afterJackDetails)
        {

            for (var i = 0; i < afterJackDetails.Length; i++)
            {
                // if the record exists, update
                if (CheckForRecord(afterJackDetails[i].AfterJackID, afterJackDetails[i].AnodeNum) == 0)
                {
                    sqlString = "INSERT INTO dbo.tblAfterJackDetail (AfterJackID, AnodeNum, ClampDrop) VALUES " +
                                "(@afterJackID, @anodeNum, @clampDrop)";
                }
                else  // else insert
                {
                    sqlString = "UPDATE dbo.tblAfterJackDetail SET ClampDrop = @clampDrop " +
                                "WHERE AfterJackID = @afterJackID AND AnodeNum = @anodeNum";
                }

                using (SqlConnection conn = new SqlConnection(connString))
                {
                    SqlCommand cmd = new SqlCommand(sqlString, conn);
                    try
                    {
                        conn.Open();
                        cmd.Parameters.Add("@afterJackID", SqlDbType.Int);
                        cmd.Parameters["@afterJackID"].Value = afterJackDetails[i].AfterJackID;
                        cmd.Parameters.Add("@anodeNum", SqlDbType.Int);
                        cmd.Parameters["@anodeNum"].Value = afterJackDetails[i].AnodeNum;
                        cmd.Parameters.Add("@clampDrop", SqlDbType.Int);
                        cmd.Parameters["@clampDrop"].Value = afterJackDetails[i].ClampDrop;
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        private void DeleteAfterJackMain(int id)
        {
            sqlString = "DELETE FROM dbo.tblAfterJackMain WHERE ID = @id";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand(sqlString, conn);
                try
                {
                    conn.Open();
                    cmd.Parameters.Add("@id", SqlDbType.Int);
                    cmd.Parameters["@id"].Value = id;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void DeleteAfterJackDetail(int afterjackID)
        {


            sqlString = "DELETE FROM dbo.tblAfterJackDetail WHERE AfterJackID = @afterJackID";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand(sqlString, conn);
                try
                {
                    conn.Open();
                    cmd.Parameters.Add("@afterJackID", SqlDbType.Int);
                    cmd.Parameters["@afterJackID"].Value = afterjackID;
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void DeleteAfterJacks(int id, int afterJackID)
        {
            if (afterJackID > 0)
            {
                DeleteAfterJackDetail(afterJackID);
            }

            DeleteAfterJackMain(id);            
        }

        private int CheckForRecord(int afterJackID, int anodeNum)
        {
            int numRecords = 0;
            string sql = "SELECT COUNT(*) FROM dbo.tblAfterJackDetail WHERE AfterJackID = @afterJackID " +
                         "AND AnodeNum = @anodeNum";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add("@afterJackID", SqlDbType.Int);
                cmd.Parameters["@afterJackID"].Value = afterJackID;
                cmd.Parameters.Add("@anodeNum", SqlDbType.Int);
                cmd.Parameters["@anodeNum"].Value = anodeNum;
                
                try
                {
                    conn.Open();
                    numRecords = (int)cmd.ExecuteScalar();
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return numRecords;
        }


    }
}