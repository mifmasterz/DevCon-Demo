using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TinyCsvParser.Mapping;

namespace DevCon.Covid19Web.Data
{
    public class Covid19Summary
    {
        public int No { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public DateTime LastUpdate { get; set; }
        public float Confirmed { get; set; }
        public float Death { get; set; }
        public float Recovered { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
    }
    public class Covid19Info
    {
        public int Sno { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public DateTime LastUpdate { get; set; }
        public float Confirmed { get; set; }
        public float Death { get; set; }
        public float Recovered { get; set; }
    }

    public class Covid19InfoMapping : CsvMapping<Covid19Info>
    {
        public Covid19InfoMapping()
            : base()
        {
            MapProperty(0, x => x.Sno);
            MapProperty(1, x => x.CreatedDate);
            MapProperty(2, x => x.Province);
            MapProperty(3, x => x.Country);
            MapProperty(4, x => x.LastUpdate);
            MapProperty(5, x => x.Confirmed);
            MapProperty(6, x => x.Death);
            MapProperty(7, x => x.Recovered);

        }
    }
}
/*
 Sno,Date,Province/State,Country,Last Update,Confirmed,Deaths,Recovered
 */
