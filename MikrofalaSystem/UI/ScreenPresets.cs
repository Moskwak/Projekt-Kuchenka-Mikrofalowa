using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikrofalaSystem.UI
{
    public class ScreenPresets : ScreenSelOptions
    {
        public ScreenPresets(ScreenViewer caller) : base(caller, new string[] {})
        {
            UpdateList(GenOptionsList());
        }

        bool updateNextRender = false;
        IEnumerable<Setting> currentList;

        public IEnumerable<string> GenOptionsList()
        {
            currentList = caller.dbConnection.dbSettings.ToList();
            return (from x in currentList
                    select x.potrawa + $"({x.czas}s, {x.moc}W)").Append("+ Nowy");
        }

        public override void AcceptOption(int option)
        {
            if (option == currentList.Count())
            {
                caller.AddToStack(new ScreenAddNew(caller));
                updateNextRender = true;
            } else
            {
                caller.RemoveFromStack(this);
                caller.AddToStack(new ScreenInProgress(currentList.ElementAt(option), caller));
            }
        }

        public override void Draw()
        {
            if (updateNextRender)
            {
                UpdateList(GenOptionsList());
                updateNextRender = false;
            }
            Console.WriteLine("Gotowe ustawienia");
            Console.WriteLine("----------\n");
            DrawOptionsAndSel();
            Console.WriteLine("\n[DEL] Usuń");
            Console.WriteLine("[Insert] Edytuj");
        }

        public override void TakeInput(ConsoleKeyInfo consoleKeyInfo)
        {
            HandleEscQuit(consoleKeyInfo);
            if (!HandleListKey(consoleKeyInfo))
            {
                switch (consoleKeyInfo.Key)
                {
                    case ConsoleKey.Delete:
                        if (sel != currentList.Count())
                        {
                            caller.dbConnection.dbSettings.Remove(currentList.ElementAt(sel));
                            caller.dbConnection.SaveChanges();
                            updateNextRender = true;
                        }
                        break;
                    case ConsoleKey.Insert:
                        if (sel != currentList.Count())
                        {
                            caller.AddToStack(new ScreenAddNew(caller, currentList.ElementAt(sel)));
                            updateNextRender = true;
                        }
                        break;
                }
            }
        }
    }
}
