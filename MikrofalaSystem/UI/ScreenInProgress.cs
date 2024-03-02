using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikrofalaSystem.UI
{
    public class ScreenInProgress(Setting op, ScreenViewer caller, int? targetTime = null) : Screen(caller)
    {
        DateTime startTime = DateTime.Now;

        public override void Draw()
        {
            TimeSpan timeSpan = DateTime.Now - startTime;
            int nextS = op.czas - (int)timeSpan.TotalSeconds;

            if (nextS >= 0)
            {
                Console.WriteLine("MIKROFALA PRACUJE");
                Console.WriteLine("---");
                if (op.potrawa != null)
                {
                    Console.WriteLine(op.potrawa);
                    Console.WriteLine("---");
                }
                Console.WriteLine($"{(nextS / 60+"").PadLeft(2, '0')} : {(nextS % 60+"").PadLeft(2, '0')}");
                Console.WriteLine($"{op.moc}W\n");
                Console.WriteLine("[ESC] Stop");
            } else
            {
                Console.WriteLine("KONIEC");
                if (targetTime != null && op.czas >= targetTime + 60)
                {
                    Console.WriteLine("Potrawa spalona");
                }
                Console.WriteLine("[ESC] aby powrócić");
            }
        }

        public override void TakeInput(ConsoleKeyInfo consoleKeyInfo)
        {
            if (consoleKeyInfo.Key == ConsoleKey.Escape)
            {
                caller.RemoveFromStack(this);
            }
        }
    }
}
