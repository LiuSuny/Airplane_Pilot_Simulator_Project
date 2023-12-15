using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CSharp_Project_Airplane_Simulator
{
    [Serializable]
    public class Dispatcher
    {
    
        public string name { get; set; }// name of our dispatcher
        public int penalty { get; set; }// Penalty point counter
        private int WeatherAdjustment;// Adjusting weather conditions
        private static Random rnd;
        private int totalPenalty { get; set; }

        public string Name
        {
            get { return name; }
            set
            {
                if (!string.IsNullOrEmpty(value) && value.All(char.IsLetter)) //checking if user input number instead of string
                {
                    name = value;
                }
                else
                {
                    throw new Incompatible("Invalid input for Dispatcher name. Please enter a valid string.");
                }
            }
        }

        // Add a method to retrieve the total penalty
        public int TotalPenalty
        {
            get { return totalPenalty; }
            set 
            { 
                totalPenalty = value;
            }
        }

        public int Penalty
        {
            get { return penalty; }
            set
            {
                penalty = value;
            }
        }
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
            Console.WriteLine($"Dispathcer: {Name} - Recommended flight altitude: {flightAltitude} M.");

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
                Console.ForegroundColor = ConsoleColor.DarkRed;
                throw new PlaneCrushed("The plane crashed");// exception on plane crashed
               
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
                totalPenalty += Penalty;
                Console.WriteLine($"{Name}: {Penalty}");
                Console.WriteLine($"biggest number of penalty points: {TotalPenalty}\a");
                Console.ForegroundColor = ConsoleColor.DarkRed;
                throw new Incompatible("Unfit to fly");
            }
        }
    }
}
