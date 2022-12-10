using Firma.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma.Models.BusinessLogic
{
    public class DatabaseClass
    {
        protected FakturyEntities Db { get; set; }
        
        public DatabaseClass(FakturyEntities db)
        {
            Db = db;
        }
    }
}
