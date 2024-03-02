using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikrofalaSystem.UI
{
    public class ScreenViewer
    {
        public DB dbConnection = new DB();
        private List<Screen> screenStack = [];

        public void AddToStack(Screen screen)
        {
            screenStack.Add(screen);
        }
        public void RemoveFromStack(Screen screen)
        {
            screenStack.Remove(screen);
        }

        public void RunUntilDone()
        {
            while (screenStack.Count != 0)
            {
                Screen a = screenStack.Last();
                Console.Clear();
                a.Draw();
                if (a is ScreenInProgress)
                {
                    if (Console.KeyAvailable)
                    {
                        a.TakeInput(Console.ReadKey());
                    } else
                    {
                        Thread.Sleep(1000);
                    }
                }
                else
                {
                    a.TakeInput(Console.ReadKey());
                }
            }
        }
    }
}
