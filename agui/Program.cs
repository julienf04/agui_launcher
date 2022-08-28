using AutoGUI;
using System.Diagnostics;

namespace agui
{
    class Program
    {
        private const string _COMMAND_CPOS = "cpos",
            _COMMAND_DELAY = "delay",
            _COMMAND_KEY = "key",
            _COMMAND_CGETPOS = "cgetpos",
            _COMMAND_HELP = "help",
            _COMMAND_LOOPSTART = "loopstart",
            _COMMAND_LOOPEND = "loopend",
            _COMMAND_CCLICKL = "cclickl",
            _COMMAND_CCLICKR = "cclickr",
            _COMMAND_CCLICKUPL = "cclickupl",
            _COMMAND_CCLICKUPR = "cclickupr",
            _COMMAND_CCLICKDOWNL = "cclickdownl",
            _COMMAND_CCLICKDOWNR = "cclickdownr",
            _COMMAND_CWHEELHOR = "cwheelhor",
            _COMMAND_CWHEELVER = "cwheelver";

        private const ConsoleKey _KEY_NEXT_STATEMENT = ConsoleKey.N,
            _KEY_BREAK_LOOP = ConsoleKey.Q,
            _KEY_QUIT_PROGRAM = ConsoleKey.Escape;
        private const ConsoleModifiers _MODIFIER_NEXT_STATEMENT = ConsoleModifiers.Control,
            _MODIFIER_BREAK_LOOP = ConsoleModifiers.Control,
            _MODIFIER_QUIT_PROGRAM = 0;
        private const string _STRING_NEXT_STATEMENT = "Ctrl+N",
            _STRING_BREAK_LOOP = "Ctrl+Q",
            _STRING_QUIT_PROGRAM = "ESC";

        private static CommandLine _commandLine;
        private static int _loopStartIndex;
        private static bool _breakLoop = false;


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

        private static void Cgetpos(string param)
        {
            Point pos = Cursor.GetPos();
            Console.WriteLine("Pos: X=" + pos.X + ", Y=" + pos.Y);
        }

        private static void Help(string param)
        {
            Console.WriteLine("-- Manipulate Cursor and Keyboard sequentialy");

            Console.WriteLine("- Controls:");
            Console.WriteLine($"   Press {_STRING_NEXT_STATEMENT} to go to the next statment immediately.");
            Console.WriteLine($"   Press {_STRING_BREAK_LOOP} to break the loop at the next loopend statement found.");
            Console.WriteLine($"   Press {_STRING_QUIT_PROGRAM} to finalize the program immediately.");

            Console.WriteLine("- Commands:");

            Console.WriteLine($"   '{_COMMAND_HELP}': Get help about this program.");
            Console.WriteLine($"    Example: 'agui {_COMMAND_HELP}'. Prints extra info about this program.");

            Console.WriteLine($"   '{_COMMAND_CPOS}=[value]': Abreviation of cursor pos. Indicates X and Y position in pixels and cursor will go there.");
            Console.WriteLine($"    Example: 'agui {_COMMAND_CPOS}=350,100'. Cursor will go to position X=350 and Y=100 in pixels.");

            Console.WriteLine($"   '{_COMMAND_CCLICKL}': Abreviation of cursor click left. Do left click at the current position.");
            Console.WriteLine($"    Example: 'agui {_COMMAND_CCLICKL}'. Do left click at the curent position.");

            Console.WriteLine($"   '{_COMMAND_CCLICKR}': Abreviation of cursor click right. Do right click at the current position.");
            Console.WriteLine($"    Example: 'agui {_COMMAND_CCLICKR}'. Do right click at the curent position.");

            Console.WriteLine($"   '{_COMMAND_CCLICKUPL}': Abreviation of cursor click up left. Up left click at the current position.");
            Console.WriteLine($"    Example: 'agui {_COMMAND_CCLICKUPL}'. Up left click at the curent position.");

            Console.WriteLine($"   '{_COMMAND_CCLICKUPR}': Abreviation of cursor click up right. Up right click at the current position.");
            Console.WriteLine($"    Example: 'agui {_COMMAND_CCLICKUPR}'. Up right click at the curent position.");

            Console.WriteLine($"   '{_COMMAND_CCLICKDOWNL}': Abreviation of cursor click down left. Down left click at the current position.");
            Console.WriteLine($"    Example: 'agui {_COMMAND_CCLICKDOWNL}'. Down left click at the curent position.");

            Console.WriteLine($"   '{_COMMAND_CCLICKDOWNR}': Abreviation of cursor click down right. Down right click at the current position.");
            Console.WriteLine($"    Example: 'agui {_COMMAND_CCLICKDOWNR}'. Down right click at the curent position.");

            Console.WriteLine($"   '{_COMMAND_CWHEELHOR}=[value]': Abreviation of cursor wheel horizontal. Moves the horizontal wheel of the mouse the given value.");
            Console.WriteLine($"    Example: 'agui {_COMMAND_CWHEELHOR}=30'. Moves the horizontal wheel of the mouse 30 units.");

            Console.WriteLine($"   '{_COMMAND_CWHEELVER}=[value]': Abreviation of cursor wheel vertical. Moves the vertical wheel of the mouse the given value.");
            Console.WriteLine($"    Example: 'agui {_COMMAND_CWHEELVER}=60'. Moves the vertical wheel of the mouse 60 units.");

            Console.WriteLine($"   '{_COMMAND_DELAY}=[value]': Indicates a delay in the sequency, in miliseconds.");
            Console.WriteLine($"    Example: 'agui {_COMMAND_DELAY}=1000'. Thread sleeps 1000 miliseconds.");

            Console.WriteLine($"   '{_COMMAND_KEY}=\"[value]\": Indicates a string to be printed by keyboard.");
            Console.WriteLine($"    Example: 'agui {_COMMAND_KEY}=\"hello world!\"'. Keyboard press given keys.");

            Console.WriteLine($"   '{_COMMAND_LOOPSTART}': The start a loop.");
            Console.WriteLine($"    Example: 'agui {_COMMAND_LOOPSTART} {_COMMAND_CCLICKL} {_COMMAND_LOOPEND}'. Do left click until {_STRING_BREAK_LOOP} key is pressed.");

            Console.WriteLine($"   '{_COMMAND_LOOPEND}': Go to the last loopstart sentence until {_STRING_BREAK_LOOP} key is pressed.");
            Console.WriteLine($"    Example: 'agui {_COMMAND_LOOPSTART} {_COMMAND_CCLICKL} {_COMMAND_LOOPEND}'. Do left click until {_STRING_BREAK_LOOP} key is pressed.");

            Console.WriteLine($"   '{_COMMAND_CGETPOS}': Abreviation of cursor get pos. Get current cursor position and print it.");
            Console.WriteLine($"    Example: 'agui {_COMMAND_CGETPOS}'. Prints the current cursor position.");

            Console.WriteLine();
            Console.WriteLine("- Example of how to use this program:");
            Console.WriteLine($"    'agui {_COMMAND_LOOPSTART} {_COMMAND_CPOS}=200,150 {_COMMAND_CCLICKL} {_COMMAND_DELAY}=500 {_COMMAND_KEY}=\"Hello world!\" {_COMMAND_DELAY}=1000 {_COMMAND_CPOS}=800,500 {_COMMAND_LOOPEND}'");
            Console.WriteLine("     The previous command causes this sequence:");
            Console.WriteLine("      1: Start a loop.");
            Console.WriteLine("      2: Set cursor position to X=200, Y=150.");
            Console.WriteLine("      3: Do click.");
            Console.WriteLine("      4: Wait 500 miliseconds.");
            Console.WriteLine("      5: Keyboard press Hello world!");
            Console.WriteLine("      6: Wait 1000 miliseconds.");
            Console.WriteLine("      7: Set cursor position to X=800, Y=500");
            Console.WriteLine($"      8: Go to 1 until {_STRING_BREAK_LOOP} key is pressed (to break the loop) or {_STRING_QUIT_PROGRAM} is pressed (to finalize the program immediately).");
        }

        private static void LoopStart(string param)
        {
            _loopStartIndex = _commandLine.CurrentParamsIndex;
        }

        private static void LoopEnd(string param)
        {
            if (!_breakLoop)
                _commandLine.CurrentParamsIndex = _loopStartIndex;
        }

        private static void Cclickl(string param)
        {
            Cursor.LeftClick();
        }

        private static void Cclickr(string param)
        {
            Cursor.RightClick();
        }

        private static void Cclickupl(string param)
        {
            Cursor.LeftClickUp();
        }

        private static void Cclickupr(string param)
        {
            Cursor.RightClickUp();
        }

        private static void Cclickdownl(string param)
        {
            Cursor.LeftClickDown();
        }

        private static void Cclickdownr(string param)
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

        private static void NextStatementInput()
        {
            _commandLine.GoToNextStatement();
        }

        private static void BreakLoopInput()
        {
            _breakLoop = true;
        }

        private static void QuitProgramInput()
        {
            Process.GetCurrentProcess().Kill();
        }

        static void Main(string[] args)
        {
            if (args.Length <= 0)
            {
                Console.WriteLine($"This app should be used through CMD, giving some arguments. For more info, type 'agui {_COMMAND_HELP}'");
                Console.WriteLine($"Press any key to continue...");
                Console.ReadKey(true);
                return;
            }

            _commandLine = new CommandLine(args, "=", true,
                new Dictionary<string, Action<string>>()
                {
                    { _COMMAND_CPOS, Cpos },
                    { _COMMAND_DELAY, Delay },
                    { _COMMAND_KEY, Key },
                    { _COMMAND_CGETPOS, Cgetpos },
                    { _COMMAND_HELP, Help },
                    { _COMMAND_LOOPSTART, LoopStart },
                    { _COMMAND_LOOPEND, LoopEnd },
                    { _COMMAND_CCLICKL, Cclickl },
                    { _COMMAND_CCLICKR, Cclickr },
                    { _COMMAND_CCLICKUPL, Cclickupl },
                    { _COMMAND_CCLICKUPR, Cclickupr },
                    { _COMMAND_CCLICKDOWNL, Cclickdownl },
                    { _COMMAND_CCLICKDOWNR, Cclickdownr },
                    { _COMMAND_CWHEELHOR, Cwheelhor },
                    { _COMMAND_CWHEELVER, Cwheelver },
                });

            Input consoleInput = new Input(true,
                new Dictionary<KeyInfo, Action>()
                {
                    { new KeyInfo(_KEY_NEXT_STATEMENT, _MODIFIER_NEXT_STATEMENT), NextStatementInput },
                    { new KeyInfo(_KEY_BREAK_LOOP, _MODIFIER_BREAK_LOOP), BreakLoopInput },
                    { new KeyInfo(_KEY_QUIT_PROGRAM, _MODIFIER_QUIT_PROGRAM), QuitProgramInput }
                });

            consoleInput.RunInputAsync();
            _commandLine.Invoke();
        }
    }
}