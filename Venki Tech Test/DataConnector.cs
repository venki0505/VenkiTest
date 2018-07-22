using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using Venki_Tech_Test.Models;

namespace Venki_Tech_Test
{
    //TODO > IF time permits, Implement Interfaces/Static Methods to reduce the redundent code in this file
    public class DataConnector
    {
        public SqlConnection sqlConn;
        public SqlCommand sqlCmd;
        public SqlDataReader sqlReader;

        //Gets dynamic path for the DB realtive path
        // |DataDirectory| value is set in the Global.ascx.cs file
        // TODO > Get Connection String either from web.config or from encrypted DB
        public DataConnector()
        {            
              sqlConn = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB; AttachDbFilename=|DataDirectory|\\VenkiTestDB.mdf;;Integrated Security=True");                                          
        }

        // Gets All available PROGRAMS from DB
        public List<String> GetProgramNames()
        {
            List<String> listProg = new List<String>();

            sqlCmd = new SqlCommand("GetPrograms",sqlConn);
            sqlCmd.CommandType = CommandType.StoredProcedure;
         
            using (sqlConn)
            {
                sqlConn.Open();
                sqlReader = sqlCmd.ExecuteReader();

                while (sqlReader.Read())
                {
                    listProg.Add(sqlReader.GetString(0));
                }
            }
            return listProg;
        }
        
        // Gets All available STATIONS from DB
        public List<Station> GetAllStations()
        {
            List<Station> listStations= new List<Station>();

            sqlCmd = new SqlCommand("GetAllStations", sqlConn);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            using (sqlConn)
            {
                sqlConn.Open();
                sqlReader = sqlCmd.ExecuteReader();
                
                while (sqlReader.Read())
                {
                    Station stn = new Station()
                    {
                        STATION_ID = Convert.ToInt32(sqlReader["STATION_ID"]),
                        STATION_NAME = Convert.ToString( sqlReader["STATION_NAME"])
                    };
                    listStations.Add(stn);

                }
            }
            return listStations;
        }

        // Gets All available CELLS from DB
        public List<Cell> GetAllCell()
        {
            List<Cell> listCells = new List<Cell>();

            sqlCmd = new SqlCommand("[GetAllCells]", sqlConn);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            using (sqlConn)
            {
                sqlConn.Open();
                sqlReader = sqlCmd.ExecuteReader();

                while (sqlReader.Read())
                {
                    Cell stn = new Cell()
                    {
                        CELL_ID = Convert.ToInt32(sqlReader["CELL_ID"]),
                        CELL = Convert.ToString(sqlReader["CELL"])
                    };
                    listCells.Add(stn);
                }
            }
            return listCells;
        }

        // Gets All available MARKETS from DB
        public List<Market> GetAllMarkets()
        {
            List<Market> listMarkets = new List<Market>();

            sqlCmd = new SqlCommand("[GetAllMarkets]", sqlConn);
            sqlCmd.CommandType = CommandType.StoredProcedure;

            using (sqlConn)
            {
                sqlConn.Open();
                sqlReader = sqlCmd.ExecuteReader();

                while (sqlReader.Read())
                {
                    Market stn = new Market()
                    {
                        MARKET_ID = Convert.ToInt32(sqlReader["MARKET_ID"]),
                        MARKET_NAME = Convert.ToString(sqlReader["MARKET_NAME"])
                    };
                    listMarkets.Add(stn);
                }
            }
            return listMarkets;
        }
        // Gets All assigned PROGRMAS for a given STATION 
        public List<Program> GetProgramNamesByStation( int stationNbr)
        {
            List<Program> listPrograms = new List<Program>();

            sqlCmd = new SqlCommand("GetProgramsByStation", sqlConn);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add(new SqlParameter("@StationNbr", stationNbr));

            using (sqlConn)
            {
                sqlConn.Open();
                sqlReader = sqlCmd.ExecuteReader();

                while (sqlReader.Read())
                {
                    Program stn = new Program()
                    {
                        STATION_ID = Convert.ToInt32(sqlReader["STATION_ID"]),
                        PROGRAM_ID = Convert.ToInt32(sqlReader["PROGRAM_ID"]),
                        PROGRAM_NAME = Convert.ToString(sqlReader["PROGRAM_NAME"]),
                        FLIGHT_DATE = Convert.ToDateTime(sqlReader["FLIGHT_DATE"])
                    };
                    listPrograms.Add(stn);
                }
            }
            return listPrograms;
        }

        //Checks if a combination of Cell and Market exists in the Market_POP table or NOT 
        public bool IsMarketPopExists(int cellId, int marketId)
        {
            int count = 0;
            try { 
            
                sqlCmd = new SqlCommand("ValidateMarketsPop", sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add(new SqlParameter("@CellId", cellId));
                sqlCmd.Parameters.Add(new SqlParameter("@MarketId", marketId));

                using (sqlConn)
                {
                    sqlConn.Open();
                    count = (int)sqlCmd.ExecuteScalar();
                }

            }
            catch (Exception e)
            {
                string s = e.Message;
            }

            if (count > 0)
                return false;
            else
                return true;

        }

        //Inserts new Cell and market Combinations into DB
        public bool UpdateMarketPop(int cellId, int marketId)
        {
            int count = 0;
            try
            {
                sqlCmd = new SqlCommand("UpdateMarketPop", sqlConn);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add(new SqlParameter("@CellId", cellId));
                sqlCmd.Parameters.Add(new SqlParameter("@MarketId", marketId));

                using (sqlConn)
                {
                    sqlConn.Open();
                    count = (int)sqlCmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                string s = e.Message;
            }

            return count > 0;
        }
        
    }
}