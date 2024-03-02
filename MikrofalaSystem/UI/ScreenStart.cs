using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikrofalaSystem.UI
{
    public class ScreenStart(ScreenViewer caller) : ScreenSelOptions(caller,
            new string[] { 
                "Wbudowane ustawienia",
                "Własne ustawienia",
                "Niestandardowe podgrzewanie"
            }
        )
    {
        public override void AcceptOption(int option)
        {
            switch (option)
            {
                case 0:
                    caller.AddToStack(new ScreenBuiltinPresets(caller));
                    break;
                case 1:
                    caller.AddToStack(new ScreenPresets(caller));
                    break;
                case 2:
                    caller.AddToStack(new ScreenCustomMode(caller));
                    break;
            }
        }

        public override void Draw()
        {
            Console.WriteLine("Mikrofala");
            Console.WriteLine("---------------");
            DrawOptionsAndSel();
        }

        public override void TakeInput(ConsoleKeyInfo consoleKeyInfo)
        {
            HandleListKey(consoleKeyInfo);
        }
    }
}
