using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Project_Airplane_Simulator
{
    public class MiniMenu
    {
        int miniPointMenu { get; set; }
        public IEnumerable<string> InsertMiniMenu; 
       
        public int MiniPointMenu
        {
            get { return miniPointMenu; }
            set
            {
                miniPointMenu = value; 
            }
        }

        public MiniMenu() { } //default ctor

        public MiniMenu( IEnumerable<string> insertMiniMenu)
        {
            InsertMiniMenu = insertMiniMenu;
        }

        void ShowMenu(IEnumerable<string> InsertMiniMenu, int printMenuPoint, int X, int Y)
        {
            //int X = 42, Y = 13;
            for (int i = 0; i < InsertMiniMenu.Count(); i++)
            {
                Console.SetCursorPosition(X, Y + i);
                if (i == printMenuPoint)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.CursorLeft = X; Console.CursorTop = Y + i;
                //Console.Write($"{InsertMiniMenu[i]}");
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;

        }
        public int ClassClass(int startX = 0, int startY = 0)
        {
            int menuPoint = 0;
            ConsoleKey key = ConsoleKey.Spacebar;

            do
            {
                ShowMenu(InsertMiniMenu, menuPoint, startX, startY);
                key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.DownArrow || key == ConsoleKey.S)
                {
                    if (menuPoint < InsertMiniMenu.Count())
                    {
                        menuPoint++;
                    }
                    if (menuPoint == InsertMiniMenu.Count())
                    {
                        menuPoint = 0;
                    }
                }
                if (key == ConsoleKey.UpArrow || key == ConsoleKey.W)
                {
                    if (menuPoint >= 0)
                    {
                        menuPoint--;
                    }
                    if (menuPoint == -1)
                    {
                        menuPoint = InsertMiniMenu.Count() - 1;
                    }
                }
                if (key == ConsoleKey.Enter)
                {
                    return menuPoint;
                }


            } while (key != ConsoleKey.Escape);

            return -1;

        }
    }
}
