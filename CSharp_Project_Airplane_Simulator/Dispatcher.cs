using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Project_Airplane_Simulator
{
    public class Dispatcher
    {
    
        public string Name { get; set; }// name of our dispatcher
        public int Penalty { get; set; }// Penalty point counter
        private int WeatherAdjustment;// Adjusting weather conditions
        private static Random rnd;

        //public string Name
        //{
        //    get{ return name; }
        //    set
        //    {
        //        if(value.Any(char.IsDigit))
        //        {   
        //            Console.WriteLine("Name can't contain numbers");
        //            Name = value.ToString();
        //        }
        //        name = value;
        //    }
        //}

        //public int Penalty
        //{
        //    get { return penalty; }
        //    set
        //    {
        //        if (value > 0)
        //        {
        //            Console.WriteLine("No flight penalty");
        //        }
        //        penalty = value;
        //    }
        //}
        public Dispatcher(string name)
        {
            Name = name;
            rnd = new Random();
            WeatherAdjustment = rnd.Next(-200, 200);
            Penalty = 0;
        }
        // Function for displaying recommended flight altitude
        public void DisplayFlightInfo(int speed, int height)
        {
            int flightAltitude = 7 * speed - WeatherAdjustment;// The recommended flight altitude is calculated using these formular

            int divergence;// Difference between recommended and current height
            if (height > flightAltitude)
            {
                // Subtract the smaller from the larger
                divergence = height - flightAltitude;
            }
            else divergence = flightAltitude - height;
            Console.WriteLine($"Dispathcer: {Name} - Recommended flight altitude: {flightAltitude} = M.");

            if (speed > 1000)// Exceeding the maximum speed
            {
                Penalty += 100;
                Console.WriteLine($"Dispathcer: {Name} - Reduce speed immediately!");
                Console.Beep();
                Console.WriteLine($"Penality: {Penalty}");
            }
            if (divergence >= 300 && divergence < 600)
            {
                Penalty += 25;
                Console.WriteLine($"Penality: {Penalty}");
            }
            else if (divergence >= 600 && divergence < 1000)
            {
                Penalty += 50;
                Console.WriteLine($"Penality: {Penalty}");
            }
            else if (divergence >= 1000 || (speed <= 0 && height <= 0))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                throw new AirplaneCrushed("The plane crashed");// exception on plane crashed
               
            }  Console.ResetColor();
            // switch(divergence)
            //{
            //    case 300:
            //    case 600:  Penalty += 25; Console.WriteLine($"Penality {Penalty}"); break;
            //    case 601:
            //    case 999: Penalty += 50; Console.WriteLine($"Penality {Penalty}"); break;
            //    case 1000: speed = 0; height = 0; throw new AirplaneCrushed("The plane crashed"); break;
            //    default:
            //        Console.WriteLine("wrong figure"); break;
            //}
            if (Penalty >= 1000)
            {
                throw new Unsuitable("Unfit to fly");// Throws an "Unairworthy" exception
            }
        }
    }
}
