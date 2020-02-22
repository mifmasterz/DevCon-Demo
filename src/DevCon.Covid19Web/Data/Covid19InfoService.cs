using DevCon.Covid19Web.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TinyCsvParser;

namespace DevCon.Covid19Web.Data
{
    public class Covid19InfoService
    {
        public static readonly string CsvDataPath = FileHelpers.GetAbsolutePath("Files/2019_nCoV_data.csv");
        public Task<List<Covid19Summary>> GetSummaryAsync()
        {

            var tsk = Task<List<Covid19Summary>>.Factory.StartNew(() =>
            {
                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("EN-us");
                var datas = new List<Covid19Summary>();
                if (File.Exists(CsvDataPath))
                {
                    try
                    {
                        CsvParserOptions csvParserOptions = new CsvParserOptions(true, ',');
                        Covid19InfoMapping csvMapper = new Covid19InfoMapping();
                        CsvParser<Covid19Info> csvParser = new CsvParser<Covid19Info>(csvParserOptions, csvMapper);
                        var result = new List<Covid19Info>();
                        csvParser.ReadFromFile(CsvDataPath, Encoding.UTF8).ToList().ForEach(x => result.Add(x.Result));

                        var Provinces = (from x in result
                                         select x.Province).Distinct().ToList();
                        int counter = 0;
                        foreach (var province in Provinces)
                        {
                            counter++;
                            var sumData = result.Where(x => x.Province == province);
                            var loc = GeoHelpers.GetLocationFromAddress($"{province}, {sumData.FirstOrDefault().Country}");
                            var newItem = new Covid19Summary()
                            {
                                No = counter,
                                Confirmed = sumData.Sum(x => x.Confirmed),
                                Death = sumData.Sum(x => x.Death),
                                Recovered = sumData.Sum(x => x.Recovered),
                                LastUpdate = sumData.OrderByDescending(x => x.LastUpdate).FirstOrDefault().LastUpdate,
                                Country = sumData.FirstOrDefault().Country,
                                Province = province,
                                Lat = loc.lat,
                                Lon = loc.lon
                            };
                            datas.Add(newItem);

                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                        return datas;
                    }
                    

                }
                else
                    throw new Exception("there is no csv file in the path");
                return datas;
            });

            return tsk;
        }
    }
}
