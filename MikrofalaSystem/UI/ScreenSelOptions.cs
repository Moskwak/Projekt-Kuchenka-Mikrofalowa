using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikrofalaSystem.UI
{
    public abstract class ScreenSelOptions(ScreenViewer caller, IEnumerable<string> options) : Screen(caller)
    {
        public int sel = 0;

        public void DrawOptionsAndSel()
        {
            int i = 0;
            foreach (string a in options)
            {
                Console.WriteLine((i++ == sel ? ">" : " ") + " " + a);
            }
        }

        public abstract void AcceptOption(int option);

        public void UpdateList(IEnumerable<string> newOpts)
        {
            options = newOpts;
        }

        public bool HandleListKey(ConsoleKeyInfo key)
        {
            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    sel = sel-- == 0 ? options.Count() - 1 : sel;
                    break;
                case ConsoleKey.DownArrow:
                    sel++;
                    sel %= options.Count();
                    break;
                case ConsoleKey.Enter:
                    AcceptOption(sel);
                    break;
                default:
                    return false;
            }
            return true;
        }
    }
}
