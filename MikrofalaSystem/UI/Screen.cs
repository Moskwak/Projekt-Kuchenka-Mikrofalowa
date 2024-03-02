using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikrofalaSystem.UI
{
    public abstract class Screen(ScreenViewer caller)
    {
        protected ScreenViewer caller = caller;

        protected bool HandleEscQuit(ConsoleKeyInfo key)
        {
            if (key.Key == ConsoleKey.Escape)
            {
                caller.RemoveFromStack(this);
                return true;
            }
            return false;
        }

        public abstract void TakeInput(ConsoleKeyInfo consoleKeyInfo);
        public abstract void Draw();
    }
}
