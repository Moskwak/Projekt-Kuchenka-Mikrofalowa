using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikrofalaSystem.UI
{
    public class ScreenCustomMode : ScreenSelOptions
    {
        Setting? targetSetting = null;
        int time = 60;
        int heat = 600;

        public IEnumerable<string> GenOptionsList() => new string[]
        {
            "Czas: " + time + "s " + $"({time/60}m {time%60}s)",
            "Moc: " + heat + "W" + (sel == 1 ? " [<- / ->]" : ""),
            "Start"
        };

        public ScreenCustomMode(ScreenViewer caller, Setting? target = null) 
            : base(caller, new string[] {})
        {
            if (target != null)
            {
                this.targetSetting = target;
                this.time = target.czas;
                this.heat = target.moc;
            }
            UpdateList(GenOptionsList());
        }

        public override void AcceptOption(int option)
        {
            switch (option)
            {
                case 2:
                    caller.RemoveFromStack(this);
     caller.AddToStack(new ScreenInProgress(new Setting(targetSetting?.potrawa, heat, time), 
         caller, targetSetting?.czas));
                    break;
            }
        }

        public override void Draw()
        {
            Console.WriteLine(targetSetting != null ? targetSetting.potrawa : "Tryb niestandardowy");
            Console.WriteLine("---------------");
            DrawOptionsAndSel();
        }

        public override void TakeInput(ConsoleKeyInfo consoleKeyInfo)
        {
            HandleEscQuit(consoleKeyInfo);
            if (!HandleListKey(consoleKeyInfo))
            {
                switch (consoleKeyInfo.Key)
                {
                    case ConsoleKey.Backspace:
                        if (sel == 0)
                        {
                            time /= 10;
                        }
                        break;
                    case ConsoleKey.D0:
                    case ConsoleKey.D1:
                    case ConsoleKey.D2:
                    case ConsoleKey.D3:
                    case ConsoleKey.D4:
                    case ConsoleKey.D5:
                    case ConsoleKey.D6:
                    case ConsoleKey.D7:
                    case ConsoleKey.D8:
                    case ConsoleKey.D9:
                        if (sel == 0)
                        {
                            time *= 10;
                            time += int.Parse(consoleKeyInfo.KeyChar + "");
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        if (sel == 1)
                        {
                            heat = Math.Max(50, heat - 50);
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        if (sel == 1)
                        {
                            heat = Math.Min(1200, heat + 50);
                        }
                        break;
                }
            }
            UpdateList(GenOptionsList());
        }
    }
}
