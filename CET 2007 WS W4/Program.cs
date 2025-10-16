using System;

namespace CET2007WSW4
{
    class Program
    {

        static void Main(string[] args)
        {
            string[] tempname = { "Temperature", "Humidity", "Motion" };
            string[] rawread = { "22.5", "bad_data", "-5.0" };
            SensorReading[] readings = new SensorReading[tempname.Length]; //stores the readings in an array
            SensorReading reading = new SensorReading("Temperature", 22.5);
            Console.WriteLine(reading);

            for (int i = 0; i < tempname.Length; i++)
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
            new Sensor("Motion", 0, 1, false),
            new Sensor("Pressure", 950, 1050, true),
            new Sensor("Light", 0, 1000, true)
            };
            double[] sampleReadings = new double[] { 22.5, 120.0, 1.0, 800.0, 500.0 };

            for (int i = 0; i < sensors.Length; i++)
            {
                try
                {
                    Console.WriteLine($"Sensor: {sensors[i].Name}");
                    sensors[i].ValidateReading(sampleReadings[i]);
                    Console.WriteLine("Reading OK.\n");
                }
                catch (SensorOfflineException ex)
                {
                    Console.WriteLine($"[SensorOfflineException] {ex.Message}");
                }
                catch (ReadingOutOfRangeException ex)
                {
                    Console.WriteLine($"[ReadingOutOfRangeException] {ex.Message}");
                    Console.WriteLine($"Attempted Value: {ex.AttemptedValue}, Min: {ex.Min}, Max: {ex.Max}");
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine($"[ArgumentOutOfRangeException] {ex.Message}");
                }
                catch (IndexOutOfRangeException ex)
                {
                    Console.WriteLine($"[IndexOutOfRangeException] {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[General Exception] {ex.Message}");
                }
                finally
                {
                    Console.WriteLine($"{sensors[i].Name} Sensor Monitoring pass complete");
                }
            }


            Console.WriteLine("Testing exception highrarchy. Press enter to clear the console");
            Console.WriteLine("Now testing exception highrarchy.");


            try
            {   //triggers reading out of range
                Sensor tempSensor = new Sensor("Temperature", -20, 50, true);

                //-- TESTING EXCEPTION, COMMENT THE 3 VALIDATION PARTS OUT, LEAVING ONE TO SEE WHAT EACH ONE SAYS
                
                tempSensor.ValidateReading(999); //too high of a temp

                //triggers sensor offline exception
                Sensor motionSensor = new Sensor("Motion", 0, 1, false);
                //motionSensor.ValidateReading(1); //sensor offline

                sensors[5].ValidateReading(22.5); // not in index

            }
            catch (ReadingOutOfRangeException ex)
            {

                Console.WriteLine($"[ReadingOutOfRangeException] {ex.Message}");
                Console.WriteLine($"Attempted Value: {ex.AttemptedValue}, Min: {ex.Min}, Max: {ex.Max}");
            }
            catch (SensorOfflineException ex)
            {
                Console.WriteLine($"[SensorOfflineException] {ex.Message}");
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine($"[IndexOutOfRangeException] {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[General Exception] {ex.Message}");
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

            public Sensor(string name, double min, double max, bool isOnline)
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
                    throw new ReadingOutOfRangeException($"Reading {value} is outside range [{Min} .. {Max}] for {Name}.", value, Min, Max );
                }
            }
        }



        public class SensorOfflineException : Exception
        {
            public SensorOfflineException(string message) : base(message) { }
        }

        public class ReadingOutOfRangeException : Exception
        {
            public double AttemptedValue { get; }
            public double Min { get; }
            public double Max { get; }
            public ReadingOutOfRangeException(string message, double attemptedValue, double min, double max)
                : base(message)
            {
                AttemptedValue = attemptedValue;
                Min = min;
                Max = max;
            }
        }

    }
}
