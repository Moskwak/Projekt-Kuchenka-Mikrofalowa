using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikrofalaSystem.UI
{
    public class ScreenBuiltinPresets : ScreenSelOptions
    {
        public ScreenBuiltinPresets(ScreenViewer caller) : base(caller, new string[] {})
        {
            UpdateList(GenOptionsList());
        }

        bool updateNextRender = false;
        List<Setting> currentList = new List<Setting>
        {
            new Setting("Rozmrażanie", 160, 360),
            new Setting("Podgrzewanie", 650, 120),
            new Setting("Gotowanie na parze", 700, 300),
            new Setting("Mięso", 800, 120),
            new Setting("Kotlet", 1000, 50),
        };

        public IEnumerable<string> GenOptionsList()
        {
            return (from x in currentList
                    select x.potrawa + $"({x.czas}s, {x.moc}W)");
        }

        public override void AcceptOption(int option)
        {

            caller.RemoveFromStack(this);
            //caller.AddToStack(new ScreenInProgress(currentList.ElementAt(option), caller));
            caller.AddToStack(new ScreenCustomMode(caller, currentList.ElementAt(option)));
        }

        public override void Draw()
        {
            if (updateNextRender)
            {
                UpdateList(GenOptionsList());
                updateNextRender = false;
            }
            Console.WriteLine("Wbudowane ustawienia");
            Console.WriteLine("----------\n");
            DrawOptionsAndSel();
        }

        public override void TakeInput(ConsoleKeyInfo consoleKeyInfo)
        {
            HandleEscQuit(consoleKeyInfo);
            HandleListKey(consoleKeyInfo);
        }
    }
}
