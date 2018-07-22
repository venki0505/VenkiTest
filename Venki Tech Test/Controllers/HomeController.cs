using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;

using Venki_Tech_Test.Models;
using Venki_Tech_Test.ViewModels;
using Venki_Tech_Test;

namespace Venki_Tech_Test.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //Load Default values 
            //Q2 > Station Drop Down values
            StationsVM stnVM = new StationsVM();
            stnVM.allStations = this.GetAllStations();

            return View(stnVM);
        }        

        //Q1 >  This Task is called by the UI get all the available Program Names and formats them as required
        public async Task<String> ProcessProgramName()
        {
            //DataConnector dataConn = new DataConnector();
            String clean = String.Empty; String original = String.Empty;

            List<String> allPrgNames = this.GetProgramNames();

            foreach (String prgName in allPrgNames)
            {
                clean += "'" + prgName.Replace("'", "''") + "',";
                original += prgName + ",";
            }          
            return Convert.ToString("<br /><br /><b>Original Data = </b>" + original + "<br /><br />" 
                    + "<b>Updated Data = </b>" + clean);
        }

        //Q2 >  This ActionResult returna partial View with Program Names per selected Station.
        public ActionResult GetProgramNamesByStation(int stationNbr)
        {
            StationsVM stnVM = new StationsVM();
            stnVM.allPrograms = this.GetAllPrograms(stationNbr);

            return PartialView("~/Views/Shared/_AllPrograms.cshtml", stnVM);
        }

        //Q3 > This  has no UI, just on the click on the button we will load data based on below logic
        //      1. Get All Market_pop, Cell and Market enitites (as data is less we can grab all at once, if otherwise not advisable)
        //      2. Loop thru Cell items
        //      3. Do a nested Loop thru all the Market items > here confirm for each Cell to Market Combination previously exists or not

        public async Task<bool> PopulateMarketPop(int insertRowCOunt)
        {
            bool isUpdateSucess = false;
            List<Cell> allCells = this.GetAllCell();
            List<Market> allMarkets = this.GetAllMarkets();

            int counter = 0;           
                //Below I have used both for and foreach, we can use either one both do almost the smae thing 
                for (int i = 0; i < allCells.Count; i++)
                {
                    foreach (Market mrk in allMarkets)                
                    {   
                        //Verify if we have a entry for the same Cell/market ID combo  in Market_pop table or not 
                        if (this.IsMarketPopExists(allCells[i].CELL_ID, mrk.MARKET_ID))
                        {
                            if (counter < insertRowCOunt) {                        
                                isUpdateSucess =  this.UpdateMarketPop(allCells[i].CELL_ID, mrk.MARKET_ID);
                                if (isUpdateSucess) counter++;
                            }
                        }
                        else {                        
                            // DO NOTHING                         
                        }
                    }
                }            
            return isUpdateSucess;
        }


        #region Helpers 
        //TODO > re-do all the below

        //Question 1
        public List<String> GetProgramNames()
        {
            DataConnector dataConn = new DataConnector();
            return dataConn.GetProgramNames();
        }

        //Question 2
        public List<Station> GetAllStations()
        {
            DataConnector dataConn = new DataConnector();
            return dataConn.GetAllStations();
        }

        public List<Program> GetAllPrograms(int stationNbr)
        {
            DataConnector dataConn = new DataConnector();
            return dataConn.GetProgramNamesByStation(stationNbr);
        }

        //Question 3
        public List<Cell> GetAllCell()
        {
            DataConnector dataConn = new DataConnector();
            return dataConn.GetAllCell();
        }
        public List<Market> GetAllMarkets()
        {
            DataConnector dataConn = new DataConnector();
            return dataConn.GetAllMarkets();
        }
        
        public bool IsMarketPopExists(int cellId, int marketId)
        {
            DataConnector dataConn = new DataConnector();
            return dataConn.IsMarketPopExists(cellId, marketId);
        }
        public bool UpdateMarketPop(int cellId, int marketId)
        {
            DataConnector dataConn = new DataConnector();
            return dataConn.UpdateMarketPop(cellId, marketId);
        }
        

        #endregion
    }
}