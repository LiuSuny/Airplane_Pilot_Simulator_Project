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
        private List<Dispatcher> dispatchers;// Список текущих диспетчеров
        private int currentSpeed { get; set; }// Текущая скорость
        private int currentHeight { get; set; }// Текущая Высота
        private int totalPenalty { get; set; }// Общая сумма штрафных очков
        private bool IsSpeedGained { get; set; }// Показывает, набрана ли максимальная скорость
        private bool IsFlyBegin { get; set; }// Показывает, начался ли полет

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
        // Функция для добавления диспечтера
        public void AddDispatcher(string name)
        {
            Dispatcher d = new Dispatcher(name);
            ChangeEvent += d.DisplayFlightInfo;// Подписка на событие
            dispatchers.Add(d);// Добавение в список

            Console.WriteLine($"Dispatcher {name} add!\a");
            
        }
        // Функция для удаление dispatcher
        public void DeleteDispatcher(int position)
        {
            if (dispatchers.Count == 0)// Если в списке пусто
            {
                Console.WriteLine("first added dispatcher!");
                Console.Beep();
            }
            else if (position == -1)
            {
                Console.WriteLine("Change.\a");
                return;
            }
            else if (position >= 0 && position <= dispatchers.Count - 1)
            {
                ChangeEvent -= dispatchers[position].DisplayFlightInfo;// Отписка от события
                Console.WriteLine($"Dispatcher {dispatchers[position].Name} deleted!\a");
                totalPenalty += dispatchers[position].Penalty;// Сохранение штрафных очков, полученных от удаляемого dispatcher
                dispatchers.RemoveAt(position);// Удаление из списка
            }
            else
            {
                Console.WriteLine("No such dispatcher exists!");
                Console.Beep();
            }
        }
        // Функция для вывода всех диспетчеровна экран
        public void PrintDispatchers()
        {
            Console.WriteLine();
            Console.WriteLine("0. Change");
            foreach (Dispatcher i in dispatchers)
                Console.WriteLine($"{dispatchers.IndexOf(i) + 1}. {i.Name}");
        }

        public void Fly()
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
                    DeleteDispatcher(Convert.ToInt32(Console.ReadLine()) - 1);
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
                if (dispatchers.Count >= 2 && CurrentSpeed >= 50)// Управление самолетом dispatcherми начинается
                {
                    Console.WriteLine();
                    if (!IsFlyBegin)// Оповещение о начале полета
                        Console.WriteLine("The flight has begun!\a");
                    IsFlyBegin = true;

                    // В процессе полета самолет автоматически сообщает
                    // всем dispatcherм все изменения в скорости
                    // и высоте полета с помощью делегатов
                    ChangeEvent(CurrentSpeed, CurrentHeight);
                    if (CurrentSpeed == 1000)
                    {
                        IsSpeedGained = false;
                        Console.WriteLine("\nYou have reached maximum speed. Your task is to land the plane!\a");
                    }
                    else if (IsSpeedGained && CurrentSpeed <= 50)// Управление самолетом dispatcherми прекращается
                    {
                        Console.WriteLine("\nThe flight has ended!\a");
                        // Перебор всех диспетчеров в коллекции и суммирование 
                        // всех штрафныех очков в общую сумму
                        foreach (Dispatcher i in dispatchers)
                        {
                            totalPenalty += i.Penalty;
                            Console.WriteLine($"{i.Name}: {i.Penalty}");
                        }

                        Console.WriteLine($"Total number of penalty points: {totalPenalty}\a");

                        break;// Выход из цикла
                    }
                }
                
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Speed: {CurrentSpeed} km/h Height: {CurrentHeight} m");
               
            } while (true);
        }
    }
}
