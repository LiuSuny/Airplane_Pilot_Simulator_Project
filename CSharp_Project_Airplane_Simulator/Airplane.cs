using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharp_Project_Airplane_Simulator
{
    public class Airplane
    {
        private List<Dispatcher> dispatchers;// List of current dispatchers
        private int currentSpeed { get; set; }// Current speed
        private int currentHeight { get; set; }// current height of flight
        private int totalPenalty { get; set; }// total sum of voliation commited
        private bool IsSpeedGained { get; set; }//Indicates whether maximum speed has been reached
        private bool IsFlyBegin { get; set; }// Indicates whether the flight has started

        private delegate void ChangeDelegate(int speed, int height);
        private event ChangeDelegate ChangeEvent;

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
            totalPenalty = 0;
            IsSpeedGained = false;
            IsFlyBegin = true;
        }
        //dispatcher addition function
        public void AddDispatcher(string name)
        {
            Dispatcher d = new Dispatcher(name);
            ChangeEvent += d.DisplayFlightInfo;// subscribing to event
            dispatchers.Add(d);// Adding to the list

            //Next we display our added dispatcher 
            Console.WriteLine($"Dispatcher: {name} added!\a");
            
        }
        // Function for removing dispatcher
        public void EraseDispatcher(int position) //DeleteDispatcher
        {
            if (dispatchers.Count == 0)// checking if list isEmpty
            {
                Console.WriteLine("first added dispatcher!");
                Console.Beep(); //added little bit of beep sound once name is added to our project
            }
            else if (position == -1) 
            {
                Console.WriteLine("Change.\a");
                return;
            }
            else if (position >= 0 && position <= dispatchers.Count - 1) //trying to unsubscribe to event
            {
                ChangeEvent -= dispatchers[position].DisplayFlightInfo;// Unsubscribe from an event
                Console.WriteLine($"Dispatcher {dispatchers[position].Name} deleted!\a");
                // Next we are try save penalty points received from the dispatcher being erase from list
                totalPenalty += dispatchers[position].Penalty;
                dispatchers.RemoveAt(position);// Removing from the list
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
            //TimeToSleep();

            Console.SetCursorPosition(35, 9);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("The pilot's task is to take off the plane" + 
              "once it reach maximum (1000 km/h) speed, and then land the plane.");         
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\nRightArrow - increase the aircraft speed by 50," +
                "\nLeftArrow - decrease the aircraft speed by 50," +
                "\nShift + RightArrow - increase the aircraft speed by 150," +
                "\nShift + LeftArrow - decrease the aircraft speed by 150");
            Console.ResetColor();
            
            Console.SetCursorPosition(60, 17);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            

            Console.SetCursorPosition(63, 18);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n\nUpArrow - increase the aircraft altitude by 250," +
                "\nDownArrow - decrease the aircraft altitude by 250," +
                "\nShift + UpArrow - increase the aircraft altitude by 500 ," +
                "\nShift + DownArrow - decrease the aircraft altitude by 500.\n");

           
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.SetCursorPosition(70, 25);
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("\n+(plus) - you can add a new dispatcher: \n-(minus)- delete the dispatcher");
           
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey();
                if (key.Key == ConsoleKey.OemPlus || key.Key == ConsoleKey.Add)
                {
                    Console.Write($"\nEnter Name of dispatcher: ");
                    AddDispatcher(Console.ReadLine());
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
                    Console.WriteLine();
                    //Checking on flight notification has begin
                    if (!IsFlyBegin)// 
                        Console.WriteLine("The flight has started!\a");
                    IsFlyBegin = true;

                    // Using event delegate, during the flight, the aircraft automatically reports
                    // to all dispatchers all changes in speed flight altitude using delegates
                    ChangeEvent(CurrentSpeed, CurrentHeight);
                    if (CurrentSpeed == 1000)
                    {
                        IsSpeedGained = false;
                        //break;
                        Console.WriteLine("\nYou have reached maximum speed. Your task is to land the plane!\a");
                    }
                    else if (IsSpeedGained && CurrentSpeed <= 50)// Control of the aircraft by dispatchers stops
                    {
                        Console.WriteLine("\nThe flight has ended!\a");
                        // using iterator to go  through all the dispatchers in the collection and add 
                        // all the penalty points into a total amount
                        foreach (Dispatcher i in dispatchers)
                        {
                            totalPenalty += i.Penalty;
                            Console.WriteLine($"{i.Name}: {i.Penalty}");
                        }
                        Console.WriteLine($"Total number of penalty points: {totalPenalty}\a");
                        //next we exit the foreach loop
                        break;
                    }
                }
                
                Console.ForegroundColor = ConsoleColor.Yellow;
              Console.WriteLine($"Speed: {CurrentSpeed} km/h Height: {CurrentHeight} m");
               
            } while (true);
           
        }
    }
}
