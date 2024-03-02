using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikrofalaSystem.UI
{
    public class ScreenAddNew : ScreenSelOptions
    {
        Setting newSetting = new("",600,60);
        bool editingPreset = false;

        public ScreenAddNew(ScreenViewer caller, Setting s = null) 
            : base(caller, new string[] {})
        {
            newSetting = s != null ? s : newSetting;
            editingPreset = s != null;
            UpdateList(GenOptionsList());
        }

        public IEnumerable<string> GenOptionsList()
        {
            return new string[]
            {
                "Nazwa: " + newSetting.potrawa,
                "Moc: " + newSetting.moc + "W",
                "Czas: " + newSetting.czas + "s",
                "Zapisz"
            };
        }

        public override void AcceptOption(int option)
        {
            if (sel == 3)
            {
                if (!editingPreset)
                {
                    caller.dbConnection.dbSettings.Add(newSetting);
                }
                caller.dbConnection.SaveChanges();
                caller.RemoveFromStack(this);
            }
        }

        public override void Draw()
        {
            Console.WriteLine("Nowa konfiguracja");
            Console.WriteLine("-----------------");
            DrawOptionsAndSel();
        }

        public override void TakeInput(ConsoleKeyInfo consoleKeyInfo)
        {
            HandleEscQuit(consoleKeyInfo);
            if (!HandleListKey(consoleKeyInfo))
            {
                if (sel == 0)
                {
                    switch (consoleKeyInfo.Key)
                    {
                        case ConsoleKey.Backspace:
                            if (newSetting.potrawa.Length > 0)
                            {
                                newSetting.potrawa = newSetting.potrawa[0..^1];
                            }
                            break;
                        default:
                            if (consoleKeyInfo.KeyChar != 0)
                            {
                                newSetting.potrawa += consoleKeyInfo.KeyChar;
                            }
                            break;
                    }
                }
                else if (sel == 1)
                {
                    switch (consoleKeyInfo.Key)
                    {
                        case ConsoleKey.LeftArrow:
                            newSetting.moc = Math.Max(newSetting.moc - 50, 50);
                            break;
                        case ConsoleKey.RightArrow:
                            newSetting.moc = Math.Min(newSetting.moc + 50, 1200);
                            break;
                    }
                }
                else if (sel == 2)
                {
                    switch (consoleKeyInfo.Key)
                    {
                        case ConsoleKey.Backspace:
                            newSetting.czas /= 10;
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
                            newSetting.czas *= 10;
                            newSetting.czas += int.Parse(consoleKeyInfo.KeyChar + "");
                            break;
                    }
                }
            }
            UpdateList(GenOptionsList());
        }
    }
}
