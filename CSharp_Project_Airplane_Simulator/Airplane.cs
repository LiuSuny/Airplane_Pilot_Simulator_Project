using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using System.Xml.Linq;

namespace CSharp_Project_Airplane_Simulator
{
    //[Serializable]
    public class Airplane 
    {
        private delegate void DispatcherDelegate(int speed, int height);
        private List<Dispatcher> dispatchers;// List of current dispatchers
        public int currentSpeed { get; set; }// Current speed
        public int currentHeight { get; set; }// current height of flight
        private int alltotalPenalty  { get; set; }// total sum of voliation commited
        private bool IsSpeedGained { get; set; }//Indicates whether maximum speed has been reached
        private bool IsFlyBegin { get; set; }// Indicates whether the flight has started
        private int positionX;
        private int positionY;

        private event DispatcherDelegate ChangeEventForDespacther;

       
        public int CurrentHeight
        {
            get { return currentHeight; }
            set
            {
                if(value <0)
                {
                    Console.WriteLine("Plane is park and not flying");
                }
                currentHeight = value;
            }
        }

        public int CurrentSpeed
        {
            get { return currentSpeed; }
            set
            {
                if (value < 0)
                {
                    Console.WriteLine("Plane is not moving");
                }
                currentSpeed = value;
            }
        }
       
        public Airplane()
        {
            dispatchers = new List<Dispatcher>();
            CurrentSpeed = 0;
            CurrentHeight = 0;
            alltotalPenalty = 0;
            IsSpeedGained = false;
            IsFlyBegin = true;

            positionX = Console.WindowWidth / 2;
            positionY = Console.WindowHeight / 2;
        }
       
        //dispatcher addition function
        public void SignInDispatcher(string Nam)
        {
            Dispatcher dispatch = new Dispatcher(Nam);
            ChangeEventForDespacther += dispatch.DisplayFlightInfo;// subscribing to event
            dispatchers.Add(dispatch);// Adding to the list

            //Next we display our added dispatcher 
            
            Console.WriteLine($"Dispatcher: {Nam} added!\a");

        }
        // Function for removing dispatcher
        public void EraseDispatcher(int pos) //DeleteDispatcher
        {
            if (dispatchers.Count == 0)// checking if list isEmpty
            {
                Console.WriteLine("first added dispatcher!");
                Console.Beep(); //added little bit of beep sound once name is added to our project
            }
            else if (pos == -1) 
            {
                Console.WriteLine("Change.\a");
                return;
            }
            else if (pos >= 0 && pos <= dispatchers.Count - 1) //trying to unsubscribe to event
            {
                ChangeEventForDespacther -= dispatchers[pos].DisplayFlightInfo;// Unsubscribe from an event
                Console.WriteLine($"Dispatcher {dispatchers[pos].Name} deleted!\a");
                // Next we are try save penalty points received from the dispatcher being erase from list
                 alltotalPenalty += dispatchers[pos].Penalty;
                dispatchers.RemoveAt(pos);// Removing from the list
                
            }
            else
            {
                //If you enter wrong number you recieve no dispatcher exist with beep sound
                Console.WriteLine("No such dispatcher exists!");
                Console.Beep();
            }
        }
        // Next we created Function displaying all dispatchers on the screen 
        public void PrintDispatchers()
        {
            Console.WriteLine();
            Console.WriteLine("0. Change");
            foreach (Dispatcher item in dispatchers)
            {
                Console.WriteLine($"{dispatchers.IndexOf(item) + 1}. {item.Name}");
            }
        }

        // Next we created Function that fly our airplane with display
        public void FlyOver()
        {
            int TimeToSleep = 1500;

            Console.SetCursorPosition(15, 7);
            Console.ForegroundColor = ConsoleColor.White;
            Thread.Sleep(TimeToSleep);
            Console.WriteLine("The pilot's task is to take off the plane" + 
              " once it reach maximum (1000 km/h) speed, and then land the plane.");
           // Thread.CurrentThread.Priority = ThreadPriority.BelowNormal;
            Console.ResetColor();
            Thread.Sleep(TimeToSleep);
           

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Thread.Sleep(TimeToSleep);
            Console.WriteLine("\n\nRightArrow - increase the aircraft speed by 50," +
                "\nLeftArrow - decrease the aircraft speed by 50," +
                "\nShift + RightArrow - increase the aircraft speed by 150," +
                "\nShift + LeftArrow - decrease the aircraft speed by 150");
            Console.ResetColor();
            
            Console.SetCursorPosition(60, 17);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Thread.Sleep(TimeToSleep);
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            

            Console.SetCursorPosition(63, 18);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Thread.Sleep(TimeToSleep);
            Console.WriteLine("\n\nUpArrow - increase the aircraft altitude by 250," +
                "\nDownArrow - decrease the aircraft altitude by 250," +
                "\nShift + UpArrow - increase the aircraft altitude by 500 ," +
                "\nShift + DownArrow - decrease the aircraft altitude by 500.\n");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Thread.Sleep(TimeToSleep);
            Console.WriteLine("Press any key to continue...");
            Console.ResetColor();

            Console.ReadKey();
            Console.SetCursorPosition(70, 25);
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\n+(plus) - you can add a new dispatcher: \n-(minus)- delete the dispatcher");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.ResetColor();
            ConsoleKeyInfo key;
            string Count;
            do
            {
                key = Console.ReadKey();
                if (key.Key == ConsoleKey.OemPlus || key.Key == ConsoleKey.Add)
                {
                    Console.Write($"\nEnter Name of dispatcher: ");
                    SignInDispatcher(Console.ReadLine());
                }
                else if (key.Key == ConsoleKey.OemMinus || key.Key == ConsoleKey.Subtract)
                {
                    PrintDispatchers();
                    Console.Write($"Enter the number of the dispatcher you want to delete: ");
                    EraseDispatcher(Convert.ToInt32(Console.ReadLine()) - 1);
                }
                if ((key.Modifiers & ConsoleModifiers.Shift) != 0)
                {
                    if (key.Key == ConsoleKey.RightArrow) CurrentSpeed += 150;
                    else if (key.Key == ConsoleKey.LeftArrow) CurrentSpeed -= 150;
                    else if (key.Key == ConsoleKey.UpArrow) CurrentHeight += 500;
                    else if (key.Key == ConsoleKey.DownArrow) CurrentHeight -= 500;
                }
                else
                {
                    if (key.Key == ConsoleKey.RightArrow) CurrentSpeed += 50;
                    else if (key.Key == ConsoleKey.LeftArrow) CurrentSpeed -= 50;
                    else if (key.Key == ConsoleKey.UpArrow) CurrentHeight += 250;
                    else if (key.Key == ConsoleKey.DownArrow) CurrentHeight -= 250;

                }
                //Control of the aircraft by dispatchers begins
                if (dispatchers.Count >= 2 && CurrentSpeed >= 50)
                {
                    //Console.WriteLine();
                    //Checking on flight notification has begin
                    if (!IsFlyBegin)//
                    {
                        Console.WriteLine("The flight has started!\a");
                        IsFlyBegin = true;
                    }
                    // Using event delegate, during the flight, the aircraft automatically reports
                    // to all dispatchers all changes in speed flight altitude using delegates
                    ChangeEventForDespacther(CurrentSpeed, CurrentHeight);
                    if (CurrentSpeed == 1000)
                    {
                        IsSpeedGained = true;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("\nYou have reached maximum speed. Your task is to land the plane!\a");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.ResetColor();
                        //break;
                    }
                     if (IsSpeedGained && CurrentSpeed <= 50)// Control of the aircraft by dispatchers stops
                     {
                        Console.WriteLine("\nThe flight has ended!\a");
                        // using iterator to go  through all the dispatchers in the collection and add 
                        // all the penalty points into a total amount
                        foreach (Dispatcher item in dispatchers)
                        {
                            alltotalPenalty += item.Penalty;
                            Console.WriteLine($"{item.Name}: {item.Penalty}");
                        }
                        Console.WriteLine($"Total number of penalty points: {alltotalPenalty}\a");
                        //next we exit the foreach loop
                        break; 
                        
                     }
                }
                
                Console.ForegroundColor = ConsoleColor.Yellow;
                //Console.SetCursorPosition(5, 2);
                Console.WriteLine($"\nSpeed: {CurrentSpeed} km/h Height: {CurrentHeight} m");
                // Console.SetCursorPosition(5, 2);

            } while (true);
        }

        //Method for saving our dispatcher to file using filestream writer 
        public void SaveDispatchersDetailsToFile(string fileName)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(fileName))
                {
                    foreach (var dispatcher in dispatchers)
                    {
                        writer.WriteLine($"{dispatcher.Name},{dispatcher.Penalty}");
                    }
                    writer.WriteLine($"TotalPenalty : {alltotalPenalty}");
                }
                Console.WriteLine("Dispatchers saved to file.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving dispatchers to file: {ex.Message}");
            }

        }

        //Method for loading our dispatcher to file using filestream reader 
        public void LoadDispatchersDetailsFromFile(string fileName)
        {
            try
            {
                if (File.Exists(fileName))
                {
                    dispatchers.Clear();
                    alltotalPenalty = 0;
                    using (StreamReader reader = new StreamReader(fileName))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            string[] parts = line.Split(',');
                            if (parts.Length == 2)
                            {
                                string name = parts[0];
                                int penalty = int.Parse(parts[1]);
                                Dispatcher dispatcher = new Dispatcher(name);
                                dispatcher.Penalty = penalty;
                                dispatchers.Add(dispatcher);
                                alltotalPenalty += penalty;
                            }
                            else if (parts.Length == 2 && parts[0] == "TotalPenalty")
                            {
                                Dispatcher dispatcheer = new Dispatcher("");
                                alltotalPenalty = int.Parse(parts[1]);
                            }
                        }
                    }
                    Console.WriteLine("Dispatchers loaded from file.");
                }
                else
                {
                    Console.WriteLine("File not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading dispatchers from file: {ex.Message}");
            }
        }

        public void Move(int deltaX, int deltaY)
        {
            positionX += deltaX;
            positionY += deltaY;
            DrawAirplane();
        }
        public void DrawAirplane()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);

            // Display dispatchers
            Console.WriteLine("Dispatchers:");
            foreach (var dispatcher in dispatchers)
            {
                Console.WriteLine($" - {dispatcher.Name}");
            }

            // Display airplane
            Console.SetCursorPosition(positionX, positionY);
            Console.Write(">");
        }

        //public void FlyWithGraphics()
        //{
        //    Console.CursorVisible = false;

        //    do
        //    {
        //        ConsoleKeyInfo key = Console.ReadKey(true);

        //        switch (key.Key)
        //        {
        //            case ConsoleKey.UpArrow:
        //                Move(0, -1);
        //                break;
        //            case ConsoleKey.DownArrow:
        //                Move(0, 1);
        //                break;
        //            case ConsoleKey.LeftArrow:
        //                Move(-1, 0);
        //                break;
        //            case ConsoleKey.RightArrow:
        //                Move(1, 0);
        //                break;
        //            case ConsoleKey.OemPlus:
        //            case ConsoleKey.Add:
        //                Console.Write($"\nEnter Name of dispatcher: ");
        //                AddDispatcher(Console.ReadLine());
        //                break;
        //            case ConsoleKey.OemMinus:
        //            case ConsoleKey.Subtract:
        //                PrintDispatchers();
        //                Console.Write($"Enter the number of the dispatcher you want to delete: ");
        //                EraseDispatcher(Convert.ToInt32(Console.ReadLine()) - 1);
        //                break;
        //        }

        //        DrawAirplane();
        //    } while (true);
        //}

    }
}
