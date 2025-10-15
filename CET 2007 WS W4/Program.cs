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
    }
}
