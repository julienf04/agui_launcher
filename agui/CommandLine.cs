using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace agui
{
    /// <summary>
    /// Class that process command line arguments
    /// </summary>
    internal class CommandLine
    {
        private string[] _args; // Arguments to process
        private string _separator; // Separate key and value from an argument
        private Dictionary<string, Action<string>> _keysAndMethods; // Storage keys and methods given by the user

        private bool _goToNextArgument = false; // Break current action invoked and go to the next argument immediately
        private int _currentActionIndex = -1; // Index of the current action that is being invoked
        private List<KeyValuePair<int, Action>> _actionsAndIndex = new List<KeyValuePair<int, Action>>(); // Actions that their keys were found in args

        public CommandLine(string[] args, string separator, bool writeDebug, Dictionary<string, Action<string>> keysAndMethods)
        {
            // Save variables
            _args = args;
            _separator = separator;
            _keysAndMethods = keysAndMethods;
            WriteDebug = writeDebug;

            // Find keys and save their actions
            for (int i = 0; i < _args.Length; i++)
            {
                string[] keyParams = _args[i].Split(_separator, 2); // 0 is key, 1 is params

                Action<string> action;
                if (_keysAndMethods.TryGetValue(keyParams[0], out action))
                    _actionsAndIndex.Add(
                        new KeyValuePair<int, Action>(i, () => action(keyParams.Length >= 2 ? keyParams[1] : ""))
                        );
            }
        }

        /// <summary>
        /// Invoke the actions that their keys are found in arguments
        /// </summary>
        public void Invoke()
        {
            for (_currentActionIndex = 0; _currentActionIndex < _actionsAndIndex.Count; _currentActionIndex++)
            {
                if (WriteDebug)
                    Console.WriteLine(_args[_actionsAndIndex[_currentActionIndex].Key]);

                _goToNextArgument = false;

                CancellationTokenSource tokenSource = new CancellationTokenSource();
                Task task = Task.Run(_actionsAndIndex[_currentActionIndex].Value, tokenSource.Token);

                SpinWait.SpinUntil(() => task.IsCompleted || _goToNextArgument);

                if (_goToNextArgument)
                    tokenSource.Cancel();
            }

            _currentActionIndex = -1;
        }

        /// <summary>
        /// Break the current action and go to the next statement immediately
        /// </summary>
        public void GoToNextStatement()
        {
            _goToNextArgument = true;
        }


        /// <summary>
        /// Index of the current action that is being invoked. Value is -1 if nothing action is being executing
        /// </summary>
        public int CurrentParamsIndex
        {
            get => _currentActionIndex;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("value");
                else if (_currentActionIndex >= 0)                
                    _currentActionIndex = value;
            }
        }

        /// <summary>
        /// Arguments given in the constructor
        /// </summary>
        public string[] Args { get => (string[])_args.Clone(); }

        /// <summary>
        /// String that separate the key argument and its parameter
        /// </summary>
        public string Separator { get => _separator; }

        /// <summary>
        /// Copy of the dictionary of keys and actions given in constructor
        /// </summary>
        public Dictionary<string, Action<string>> KeysAndMethods { get => new Dictionary<string, Action<string>>(_keysAndMethods); }

        /// <summary>
        /// If true, write in console the arguments; otherwise, not
        /// </summary>
        public bool WriteDebug { get; set; }
    }
}
