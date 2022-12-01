using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Common.DataContexts
{
    public class DataContextsHub
    {
        public DBContext dbContext;
        public CSVContext csvContext;
        public DataContextsHub(DBContext dBContext, CSVContext cSVContext)
        {
            dbContext = dBContext;
            csvContext = cSVContext;
        }

    }
}
