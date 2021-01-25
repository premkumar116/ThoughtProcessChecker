using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleApp
{
    class Program
    {
        static void Main(string[] args)

        {
            List<Driver> drivers = new List<Driver>()
            {                 
                 new Driver(){ DriverName="Alex" , Trips =new List<Trip>(){ new Trip() { StartTime="12:01"  , StopTime="13:16" , MilesDriven=42.0} } },
                 new Driver(){ DriverName="Dan" , Trips =new List<Trip>(){ new Trip() { StartTime="07:15"  , StopTime="07:45" , MilesDriven=17.3},
                                                             new Trip() { StartTime = "06:12", StopTime = "06:32", MilesDriven = 21.8 } } },
                 new Driver(){ DriverName="Bob" , Trips =new List<Trip>(){ new Trip()  { StartTime=""  , StopTime="" , MilesDriven=0.0d} } },
            };

            double totalmilesDriven = 0.0d;
            double totalTimesTaken = 0.0d;
            double speed = 0.0d;
            foreach (var driver in drivers)
            {
                foreach (var trip in driver.Trips)
                {
                    string timediff= Program.TimeDiff(trip.StartTime,trip.StopTime);
                    totalTimesTaken += TotalTimeTohours(timediff);
                    totalmilesDriven += trip.MilesDriven;                                        
                }
                speed = CalculateAvgSpeed(totalmilesDriven, totalTimesTaken);
                totalmilesDriven = Math.Round(totalmilesDriven);
                if (speed != 0)
                {
                    Console.WriteLine($"{driver.DriverName} {totalmilesDriven} miles @ {speed} mph");
                }
                else
                {
                    Console.WriteLine($"{driver.DriverName} {totalmilesDriven} miles");
                }
                totalTimesTaken = 0.0d;
                totalmilesDriven = 0.0d;
                speed = 0.0d;
            }
            Console.ReadLine();
        }
        public static string TimeDiff(string sTimeFrom, string sTimeTo)
        {
            DateTime dFrom;
            DateTime dTo;
            string timeDiff = "";
            if (DateTime.TryParse(sTimeFrom, out dFrom) && DateTime.TryParse(sTimeTo, out dTo))
            {
                TimeSpan TS = dTo - dFrom;
                int hour = TS.Hours;
                int mins = TS.Minutes;
                int secs = TS.Seconds;
                timeDiff = hour.ToString("00") + ":" + mins.ToString("00");
            }
            return timeDiff;
        }
        public static double CalculateAvgSpeed(double milesdriven , double Timetaken)
        {
            double avgSpeed = 0.0d;
            if (Timetaken != 0)
            {
                avgSpeed = Math.Round(milesdriven / Timetaken);
            }
            else
            {
                return 0;
            }                
            return avgSpeed;
        }

        public static double TotalTimeTohours( string Timestaken)
        {
            double hoursActual = 0.0d;
            if (!string.IsNullOrWhiteSpace(Timestaken))
            {
                DateTime Totaltime = DateTime.ParseExact(Timestaken, "HH:mm", CultureInfo.InvariantCulture);
                double hours = Totaltime.Hour;
                double minutes = Totaltime.Minute;
                hoursActual = hours + (minutes / 60);
            }
            else
            {
                return 0;
            }              
            return hoursActual;
        }
    }
}

