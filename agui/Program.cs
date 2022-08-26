using AutoGUI;

namespace agui
{
    class Program
    {
        private const string _CPOS_KEY = "cpos",
                             _DELAY_KEY = "delay",
                             _KEY_KEY = "key",
                             _CGETPOS_KEY = "cgetpos",
                             _HELP_KEY = "help",
                             _LOOPSTART_KEY = "loopstart",
                             _LOOPEND_KEY = "loopend",
                             _CCLICKL_KEY = "cclickl",
                             _CCLICKR_KEY = "cclickr",
                             _CCLICKUPL_KEY = "cclickupl",
                             _CCLICKUPR_KEY = "cclickupr",
                             _CCLICKDOWNL_KEY = "cclickdownl",
                             _CCLICKDOWNR_KEY = "cclickdownr",
                             _CWHEELHOR_KEY = "cwheelhor",
                             _CWHEELVER_KEY = "cwheelver";


        private static void Cpos(string param)
        {
            string[] nums = param.Split(',');
            Cursor.SetPos(int.Parse(nums[0]), int.Parse(nums[1]));
        }

        private static void Delay(string param)
        {
            Thread.Sleep(int.Parse(param));
        }

        private static void Key(string param)
        {
            string s = param.Substring(1, param.Length - 2);
            Keyboard.Set(s);
        }

        private static void Cgetpos()
        {
            Point pos = Cursor.GetPos();
            Console.WriteLine("Pos: X=" + pos.X + ", Y=" + pos.Y);
        }

        private static void Help()
        {
            Console.WriteLine("-- Manipulate Cursor and Keyboard sequentialy");
            Console.WriteLine("- Commands:");

            Console.WriteLine("   [help]: Get help about this program");
            Console.WriteLine("    Example: 'agui help'. Prints extra info about this program");

            Console.WriteLine("   [cpos]: Abreviation of cursor pos. Indicates X and Y position in pixels and cursor will go there");
            Console.WriteLine("    Example: 'agui cpos=350,100'. Cursor will go to position X=350 and Y=100 in pixels");

            Console.WriteLine("   [cclickl]: Abreviation of cursor click left. Do left click at the current position");
            Console.WriteLine("    Example: 'agui cclickl'. Do left click at the curent position");

            Console.WriteLine("   [cclickr]: Abreviation of cursor click right. Do right click at the current position");
            Console.WriteLine("    Example: 'agui cclickr'. Do right click at the curent position");

            Console.WriteLine("   [cclickupl]: Abreviation of cursor click up left. Up left click at the current position");
            Console.WriteLine("    Example: 'agui cclickupl'. Up left click at the curent position");

            Console.WriteLine("   [cclickupr]: Abreviation of cursor click up right. Up right click at the current position");
            Console.WriteLine("    Example: 'agui cclickupr'. Up right click at the curent position");

            Console.WriteLine("   [cclickdownl]: Abreviation of cursor click down left. Down left click at the current position");
            Console.WriteLine("    Example: 'agui cclickdownl'. Down left click at the curent position");

            Console.WriteLine("   [cclickdownr]: Abreviation of cursor click down right. Down right click at the current position");
            Console.WriteLine("    Example: 'agui cclickdownl'. Down right click at the curent position");

            Console.WriteLine("   [cwheelhor]: Abreviation of cursor wheel horizontal. Moves the horizontal wheel of the mouse the given value");
            Console.WriteLine("    Example: 'agui cwheelhor=30'. Moves the horizontal wheel of the mouse 30 units");

            Console.WriteLine("   [cwheelver]: Abreviation of cursor wheel vertical. Moves the vertical wheel of the mouse the given value");
            Console.WriteLine("    Example: 'agui cwheelhor=60'. Moves the vertical wheel of the mouse 60 units");

            Console.WriteLine("   [delay]: Indicates a delay in the sequency, in miliseconds.");
            Console.WriteLine("    Example: 'agui delay=1000'. Thread sleeps 1000 miliseconds");

            Console.WriteLine("   [key]: Indicates a string to be printed by keyboard");
            Console.WriteLine("    Example: 'agui key=\"hello world!\"'. Keyboard press given keys");

            Console.WriteLine("   [loopstart]: The start a loop");
            Console.WriteLine("    Example: 'agui loopstart cclick loopend'. Do click until ESC key is pressed");

            Console.WriteLine("   [loopend]: Go to the last loopstart sentence until ESC key is pressed");
            Console.WriteLine("    Example: 'agui loopstart cclick loopend'. Do click until ESC key is pressed");

            Console.WriteLine("   [cgetpos]: Abreviation of cursor get pos. Get current cursor position and print it");
            Console.WriteLine("    Example: 'agui cgetpos'. Prints the current cursor position");

            Console.WriteLine();
            Console.WriteLine("- Example of how to use this program:");
            Console.WriteLine("    'agui loopstart cpos=200,150 cclick delay=500 key=\"Hello world!\" delay=1000 cpos=800,500 loopend'");
            Console.WriteLine("     The previous command causes this sequence:");
            Console.WriteLine("      1: Start a loop");
            Console.WriteLine("      2: Set cursor position to X=200, Y=150");
            Console.WriteLine("      3: Do click");
            Console.WriteLine("      4: Wait 500 miliseconds");
            Console.WriteLine("      5: Keyboard press Hello world!");
            Console.WriteLine("      6: Wait 1000 miliseconds");
            Console.WriteLine("      7: Set cursor position to X=800, Y=500");
            Console.WriteLine("      8: Go to 1 until ESC key is pressed");
        }

        private static void Loop(Action[] actions)
        {
            WINAPI.GetAsyncKeyState(WINAPI.ESC_KEY);
            do
            {
                for (int i = 0; i < actions.Length; i++)
                    actions[i]();
            } while (WINAPI.GetAsyncKeyState(WINAPI.ESC_KEY) == 0);
        }

        private static void Cclickl()
        {
            Cursor.LeftClick();
        }

        private static void Cclickr()
        {
            Cursor.RightClick();
        }

        private static void Cclickupl()
        {
            Cursor.LeftClickUp();
        }

        private static void Cclickupr()
        {
            Cursor.RightClickUp();
        }

        private static void Cclickdownl()
        {
            Cursor.LeftClickDown();
        }

        private static void Cclickdownr()
        {
            Cursor.RightClickDown();
        }

        private static void Cwheelhor(string param)
        {
            Cursor.HorizontalWheel(int.Parse(param));
        }

        private static void Cwheelver(string param)
        {
            Cursor.VerticalWheel(int.Parse(param));
        }

        static void Main(string[] args)
        {
            List<Action> actionsSequence = new List<Action>();
            bool loopStart = false;

            for (int i = 0; i < args.Length; i++)
            {
                string[] keyParams = args[i].Split('=', 3); // 0 is key, 1 is params


                switch (keyParams[0])
                {
                    case _CPOS_KEY:
                        Cpos(keyParams[1]);
                        if (loopStart)
                            actionsSequence.Add(() => Cpos(keyParams[1]));
                        break;

                    case _DELAY_KEY:
                        Delay(keyParams[1]);
                        if (loopStart)
                            actionsSequence.Add(() => Delay(keyParams[1]));
                        break;

                    case _KEY_KEY:
                        Key(keyParams[1]);
                        if (loopStart)
                            actionsSequence.Add(() => Key(keyParams[1]));
                        break;

                    case _CGETPOS_KEY:
                        Cgetpos();
                        if (loopStart)
                            actionsSequence.Add(() => Cgetpos());
                        break;

                    case _HELP_KEY:
                        Help();
                        if (loopStart)
                            actionsSequence.Add(() => Help());
                        break;

                    case _LOOPSTART_KEY:
                        loopStart = true;
                        break;

                    case _LOOPEND_KEY:
                        if (loopStart)
                        {
                            Loop(actionsSequence.ToArray());
                            loopStart = false;
                            actionsSequence.Clear();
                        }
                        break;

                    case _CCLICKL_KEY:
                        Cclickl();
                        if (loopStart)
                            actionsSequence.Add(() => Cclickl());
                        break;

                    case _CCLICKR_KEY:
                        Cclickr();
                        if (loopStart)
                            actionsSequence.Add(() => Cclickr());
                        break;

                    case _CCLICKUPL_KEY:
                        Cclickupl();
                        if (loopStart)
                            actionsSequence.Add(() => Cclickupl());
                        break;

                    case _CCLICKUPR_KEY:
                        Cclickupr();
                        if (loopStart)
                            actionsSequence.Add(() => Cclickupr());
                        break;

                    case _CCLICKDOWNL_KEY:
                        Cclickdownl();
                        if (loopStart)
                            actionsSequence.Add(() => Cclickdownl());
                        break;

                    case _CCLICKDOWNR_KEY:
                        Cclickdownr();
                        if (loopStart)
                            actionsSequence.Add(() => Cclickdownr());
                        break;

                    case _CWHEELHOR_KEY:
                        Cwheelhor(keyParams[1]);
                        if (loopStart)
                            actionsSequence.Add(() => Cwheelhor(keyParams[1]));
                        break;

                    case _CWHEELVER_KEY:
                        Cwheelver(keyParams[1]);
                        if (loopStart)
                            actionsSequence.Add(() => Cwheelver(keyParams[1]));
                        break;
                }
            }
        }
    }
}