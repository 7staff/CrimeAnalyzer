using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace CrimeAnalyzer

{
    public class CrimeStatistics
    {

        public int robbery;
        public int assault;
        public int property;
        public int burglary;
        public int theft;
        public int vehicle;
        public int year;
        public int murder;
        public int rape;
        public int population;
        public int violentcrime;

        public CrimeStatistics(int population, int year, int rape, int murder, int violentcrime, int robbery, int assault, int theft, int burglary, int property, int vehicle)

        {
            this.robbery = robbery;
            this.assault = assault;
            this.property = property;
            this.burglary = burglary;
            this.theft = theft;
            this.vehicle = vehicle;
            this.year = year;
            this.population = population;
            this.violentcrime = violentcrime;
            this.murder = murder;
            this.rape = rape;

        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            String crimelist;
            String Files;
            List<CrimeStatistics> list = new List<CrimeStatistics>();
            int count = 0;
            if (args.Length != 2)
            {
                Console.WriteLine("An Error has occurred.");
                Environment.Exit(-1);
            }
            crimelist = args[0];
            if (File.Exists(crimelist) == false)
            {
                Console.WriteLine("File cannot be found. ");
                Environment.Exit(-1);
            }
            using (var sr = new StreamReader(crimelist))
            {
                string name = sr.ReadLine();
                var hValues = name.Split(',');

                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    var values = line.Split(',');
                    int year = Convert.ToInt32(values[0]);
                    int population = Convert.ToInt32(values[1]);
                    int violentcrime = Convert.ToInt32(values[2]);
                    int murder = Convert.ToInt32(values[3]);
                    int rape = Convert.ToInt32(values[4]);
                    int robbery = Convert.ToInt32(values[5]);
                    int assault = Convert.ToInt32(values[6]);
                    int property = Convert.ToInt32(values[7]);
                    int burglary = Convert.ToInt32(values[8]);
                    int theft = Convert.ToInt32(values[9]);
                    int vehicle = Convert.ToInt32(values[10]);

                    list.Add(new crimeStatistics(year, population, violentcrime, rape, murder, robbery, assault, property, burglary, theft, vehicle));
                }


            }

            string report = "";
            var years = from CrimeStatistics in list select CrimeStatistics.year;
            foreach (var x in years)
            {
                count++;
            }

            var Murders = from CrimeStatistics in list where CrimeStatistics.murder < 15000 select CrimeStatistics.year;
            var Robberies = from CrimeStatistics in list where CrimeStatistics.robbery > 500000 select new { CrimeStatistics.year, CrimeStatistics.robbery };
            var Violence = from CrimeStatistics in list where CrimeStatistics.year == 2010 select CrimeStatistics.violentcrime;
            var Capita = from CrimeStatistics in list where CrimeStatistics.year == 2010 select CrimeStatistics.population;
            double v = 0;
            double c = 0;
            foreach (var x in Violence)
            {
                v = (double)x;
            }

            foreach (var x in Capita)
            {
                c = (double)x;
            }
            double VCPC = v / c;
            var murder1 = from CrimeStatistics in list select CrimeStatistics.murder;
            double murder2 = 0;
            foreach (var x in murder1)
            {
                murder2 += x;
            }

            double AvgMurder = murder2 / count;

            var murder94 = from CrimeStatistics in list where CrimeStatistics.year >= 1994 && CrimeStatistics.year <= 1997 select CrimeStatistics.murder;
            double Murder9497 = 0;
            int murder94count = 0;
            foreach (var x in murder94)
            {
                Murder9497 += x;
                murder94count++;
            }

            double Murder19941997 = Murder9497 / murder94count;

            var murder2010 = from CrimeStatistics in list where CrimeStatistics.year >= 2010 && CrimeStatistics.year <= 2013 select CrimeStatistics.murder;
            double Murder8 = 0;
            int murder2010count = 0;
            foreach (var x in murder2010)
            {
                Murder8 += x;
                murder2010count++;
            }

            double Murder20102013 = Murder8 / murder2010count;
            var minthefts = from CrimeStatistics in list where CrimeStatistics.year >= 1999 && CrimeStatistics.year <= 2004 select CrimeStatistics.theft;
            int mintheftsanswer = minthefts.Min();
            var maxthefts = from CrimeStatistics in list where CrimeStatistics.year >= 1999 && CrimeStatistics.year <= 2004 select CrimeStatistics.theft;
            int maxtheftsanswer = maxthefts.Max();
            var MVT = from CrimeStatistics in list select new { CrimeStatistics.year, CrimeStatistics.vehicle };
            int MVTanswer = 0;
            int temp = 0;

            foreach (var x in MVT)
            {

                if (x.vehicle > temp)
                {
                    MVTanswer = x.year;
                    temp = x.vehicle;
                }
            }
            report += "The Range Of Years Include " + years.Min() + " - " + years.Max() + " (" + count + " years) \n";
            report += "Years Murders Per Year < 15000: ";
            foreach (var x in Murders)
            {
                report += x + " ";
            }
            report += "\n";

            report += "Robberies Per Year > 500000: ";
            foreach (var x in Robberies)
            {
                report += string.Format("{0} = {1}, ", x.year, x.robbery);
            }
            report += "\n";
            report += "Violent Crime Per Capita Rate (2010): " + VCPC + "\n";
            report += "Average Murder Per Year (Across All Years): " + AvgMurder + "\n";
            report += "Average Murder Per Year (1994 To 1997): " + Murder19941997 + "\n";
            report += "Average Murder Per Year (2010 To 2013): " + Murder20102013 + "\n";
            report += "Minimum Thefts Per Year (1999 To 2004): " + mintheftsanswer + "\n";
            report += "Maximum Thefts Per Year (1999 To 2004): " + maxtheftsanswer + "\n";
            report += "Year Of Highest Number Of Motor Vehicle Thefts: " + MVTanswer + "\n";
            Files = "Output.txt";
            StreamWriter sw = new StreamWriter(Files);
            try
            {
                sw.WriteLine(report);
            }
            catch (Exception x)
            {
                Console.WriteLine("Exception: " + x.Message);
            }
            finally
            {
                Console.WriteLine("Executing.");

                sw.Close();

            }
        }
    }
}

