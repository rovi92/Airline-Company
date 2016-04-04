using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compagniaAerea
{
    public class DatabaseManager
    {
        private static DatabaseManager instance;
        private myDatabaseUniboAirlineDataContext myDatabase;

        private DatabaseManager() {
             myDatabase = new myDatabaseUniboAirlineDataContext();
        }

        public static DatabaseManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DatabaseManager();
                }
                return instance;
            }

        }



        public  myDatabaseUniboAirlineDataContext getDb()
        {
            return this.myDatabase; 
        }

      
    }
    
}
 
