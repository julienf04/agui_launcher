using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace agui
{
    /// <summary>
    /// Storage a key and its modifier
    /// </summary>
    internal struct KeyInfo
    {
        public ConsoleKey key;
        public ConsoleModifiers modifiers;

        public KeyInfo(ConsoleKey key, ConsoleModifiers modifiers)
        {
            this.key = key;
            this.modifiers = modifiers;
        }

        public override string ToString()
        {
            StringBuilder s = new StringBuilder();
            if (modifiers.HasFlag(ConsoleModifiers.Control))
                s.Append("Ctrl+");
            if (modifiers.HasFlag(ConsoleModifiers.Shift))
                s.Append("Shift+");
            if (modifiers.HasFlag(ConsoleModifiers.Alt))
                s.Append("Alt+");

            s.Append(Enum.GetName(key));

            return s.ToString();
        }
    }
}
