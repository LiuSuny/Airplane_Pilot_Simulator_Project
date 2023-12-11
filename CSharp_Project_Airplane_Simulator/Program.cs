using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace CSharp_Project_Airplane_Simulator
{
    class AirplaneCrushed : Exception
    {
        public AirplaneCrushed(string message) : base(message){ }
           
    }
    class Unsuitable : Exception
    {
        public Unsuitable(string message): base(message) { }
    }

    public class Program
    {
        static string Name;
        static int pos;
        static void Main(string[] args)
        {
            try
            { 
                // In the process of training aircraft pilots, only one aircraft object is used
                Airplane plane = new Airplane();
              
                //plane.AddDispatcher(Name);
                //plane.DeleteDispatcher(pos);
               // string path = @"C:\Users\User\Desktop\Academy3\tmp10.txt";
                plane.FlyOver();

                //using (FileStream file = new FileStream(path, FileMode.Append))//OpenORCreate
                //{
                //    byte[] temp = Encoding.Default.GetBytes(plane.Fly());
                //    //file.Seek(10, SeekOrigin.Begin);
                //    file.Write(temp, 0, temp.Length);
                //    Console.WriteLine("Ok");
                //}

                //StreamWriter sw = new StreamWriter(path);
                //foreach(Airplane item in plane)
                //{
                //    sw.WriteLine(item.Fly());
                //}

            }
            catch (AirplaneCrushed ex)
            {
                Console.WriteLine(ex.Message);
                Console.Beep();
            }
            catch (Unsuitable eu)
            {
                Console.WriteLine(eu.Message);
                Console.Beep();
            }
            catch (Exception eg)
            {
                Console.WriteLine(eg.Message);
            }

        }
    }
}
