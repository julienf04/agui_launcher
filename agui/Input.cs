using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace agui
{
    /// <summary>
    /// Manage inputs
    /// </summary>
    internal class Input
    {
        private const int _MILISECONDS_SLEEP_BETWWEEN_INPUTS = 200;

        private Dictionary<KeyInfo, Action> _actionsAndKeys; // Keys and its actions given by user
        private CancellationTokenSource _tokenSource; // Token to cancel tasks if necessary
        private bool _continueInputTask = false; // True if the task has to continue, otherwise false


        public Input(bool writeDebug, Dictionary<KeyInfo, Action> actionsAndKeys)
        {
            WriteDebug = writeDebug;
            _actionsAndKeys = actionsAndKeys;
        }

        /// <summary>
        /// Task that get input and call actions attached
        /// </summary>
        private void _RunInputAsync()
        {
            while (_continueInputTask)
            {
                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey(true);
                KeyInfo keyInfo = new KeyInfo(consoleKeyInfo.Key, consoleKeyInfo.Modifiers);

                if (WriteDebug)
                    Console.WriteLine(keyInfo);

                if (_actionsAndKeys.ContainsKey(keyInfo))
                    _actionsAndKeys[keyInfo]();

                Thread.Sleep(_MILISECONDS_SLEEP_BETWWEEN_INPUTS);
                while (Console.KeyAvailable)
                    Console.ReadKey(true);
            }
        }

        /// <summary>
        /// Start the task to receive and call actions
        /// </summary>
        public void RunInputAsync()
        {
            if (!_continueInputTask)
            {
                _continueInputTask = true;
                _tokenSource = new CancellationTokenSource();
                Task.Run(_RunInputAsync, _tokenSource.Token);
            }
        }

        /// <summary>
        /// Cancel the task that receive and call actions, if is running
        /// </summary>
        public void CancelInputTask()
        {
            if (_continueInputTask)
            {
                _continueInputTask = false;
                _tokenSource.Cancel();
                _tokenSource.Dispose();
            }
        }

        /// <summary>
        /// If true, write in console the arguments; otherwise, not
        /// </summary>
        public bool WriteDebug { get; set; }
    }
}
