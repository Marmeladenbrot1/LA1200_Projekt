using System;
using System.Collections.Generic;
using System.Text;
using static System.Console;
using System.Threading;

namespace RandomZahl3
{
    class TextAnimationen
    {
        public static void Blinkeffect(string Text, int blinkCount = 5, int onTime = 500, int offTime = 200)
        {
            CursorVisible = false;

            for (int i = 0; i < blinkCount; i++)
            {
                WriteLine(Text);
                Thread.Sleep(onTime);
                Clear();
                Thread.Sleep(offTime);
            }
            CursorVisible = true;
        }
        public static void WriteAnimation(string text, int delay = 40)
        {
            for (int i = 0; i < text.Length; i++)
            {
                Write(text[i]);
                Thread.Sleep(delay);
            }
        }
    }
}
