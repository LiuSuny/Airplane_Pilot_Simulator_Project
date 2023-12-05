using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace CSharp_Project_Airplane_Simulator
{
    class AirplaneCrushed : Exception
    {
        public AirplaneCrushed(string message)
            : base(message)
        { }
    }
    class Unsuitable : Exception
    {
        public Unsuitable(string message)
            : base(message)
        { }
    }

    public class Program
    {
        static string Name;
        static int pos;
        static void Main(string[] args)
        {
            try
            {
                // В процессе тренировки пилотов самолета используется только один объект самолета
                
                
                Airplane plane = new Airplane();
                plane.Fly();
                //plane.AddDispatcher(Name);
                //plane.DeleteDispatcher(pos);
                

            }
            catch (AirplaneCrushed ac)
            {
                Console.WriteLine(ac.Message);
                Console.Beep();
            }
            catch (Unsuitable u)
            {
                Console.WriteLine(u.Message);
                Console.Beep();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
