using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text.Json;
using System.Numerics;
using System.Threading;

namespace CSharp_Project_Airplane_Simulator
{
    class PlaneCrushed : Exception
    {
        public PlaneCrushed(string message) : base(message){ }
           
    }
    class Incompatible: Exception
    {
        public Incompatible(string message): base(message) { }
    }
   
    public class Program
    {
       
        static void Main(string[] args)
        {

            //MiniMenu menu = new MiniMenu(
            //    new List<String>{ "Enter to Start Training Airplane Pilot ", 
            //        "Enter Escape to Exit" });
            //menu.MiniPointMenu = 0;
            //menu.ClassClass();

            Airplane plane = new Airplane();
            plane.LoadDispatchersDetailsFromFile("dispatchers.txt");
            try
            {
                // In the process of training aircraft pilots, only one aircraft object is used
               
               plane.FlyOver();

            }
            catch (PlaneCrushed ex)
            {
                Console.WriteLine(ex.Message);
                Console.Beep();
            }
            catch (Incompatible eu)
            {
                Console.WriteLine(eu.Message);
                Console.Beep();
            }
            catch (Exception eg)
            {
                Console.WriteLine(eg.Message);
            }
            plane.SaveDispatchersDetailsToFile("dispatchers.txt");

        }
    }
}
