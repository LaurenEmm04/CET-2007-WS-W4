using System;

namespace CET2007WSW4
{
    class Program
    {

        static void Main(string[] args)
        {
            string[] tempname = {"Temperature", "Humidity", "Motion" };
            string[] rawread = {"22.5", "bad_data", "-5.0"};
            SensorReading[] readings = new SensorReading[tempname.Length]; //stores the readings in an array
            SensorReading reading = new SensorReading("Temperature", 22.5);
            Console.WriteLine(reading);



            for (int i = 0; i < tempname.Length; i++ )
            {
                Console.WriteLine(tempname[i]);

                double value;

                try
                {
                    value = Convert.ToDouble(rawread[i]);  //converitng raw string to number
                }
                catch (FormatException) //if not able to (like bad_data)
                {
                    value = 0.0; //set it to 0.0
                }
                //Console.WriteLine($"Converted {tempname[i]} reading: {value}"); //test
                if (value < 0) //if value is less than 0 (negative)
                {
                    value = 0.0; //set it to 0.0
                }
                readings[i] = new SensorReading(tempname[i], value);
                

            }
            for (int i = 0; i < readings.Length; i++)
            {
                Console.WriteLine(readings[i]);
            }

            for (int i = 0; i < readings.Length; i++)
            {
                Console.WriteLine(readings[i]);
            }

            Sensor[] sensors = new Sensor[]
            {
            new Sensor("Temperature", -20, 50, true),
            new Sensor("Humidity", 0, 100, true),
            new Sensor("Motion", 0, 1, false)
            };
            double[] sampleReadings = new double[] { 22.5, 120.0, 1.0 };

            for (int i =0; i<sensors.Length; i++)
            {
                Console.WriteLine($"Checking {sensors[i].Name} with value {sampleReadings[i]}");
                try
                {
                    sensors[i].ValidateReading(sampleReadings[i]);
                    Console.WriteLine($"{sensors[i].Name} reading is good");
                }
                catch (SensorOfflineException)
                {
                    Console.WriteLine($"{sensors[i].Name} sensor offline.");
                }
                catch (ReadingOutOfRangeException)
                {
                    Console.WriteLine($"{sensors[i].Name} reading out of range. Value = {sampleReadings[i]}");
                }
            }

        }




        public class SensorReading
        {
            public string Name { get; set; }
            public double Value { get; set; }

            public SensorReading(string name, double value)
            {
                Name = name;
                Value = value;
            }

            public override string ToString()
            {
                return $"Sensor: {Name} - Value: {Value}";
            }
        }
        



        public class Sensor
        {
            public string Name { get; set; }
            public double Min { get; set; }
            public double Max { get; set; }
            public bool IsOnline { get; set; }

            public Sensor (string name, double min, double max, bool isOnline)
            {
                Name = name;
                Min = min;
                Max = max;
                IsOnline = isOnline;
            }
            public void ValidateReading(double value)
            {
                if (!IsOnline)
                {
                    throw new SensorOfflineException("Sensor " + Name + " is offline.");
                }

                if (value < Min || value > Max)
                {
                    throw new ReadingOutOfRangeException("Reading " + value + " is outside range [" + Min + " .. " + Max + "] for " + Name + ".");
                }
            }
}



public class SensorOfflineException : Exception
        {
            public SensorOfflineException(string message) : base(message) { }
        }

        public class ReadingOutOfRangeException : Exception
        {
            public ReadingOutOfRangeException(string message) : base(message) { }
        }
    }
}
