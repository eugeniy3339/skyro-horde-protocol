using System;
using UnityEngine;

namespace HordeProtocol.Gameplay
{
    public interface ICommand
    {
        /// <summary>
        /// Excecutes command if cant to
        /// </summary>
        /// <returns>Returns true if excecuted and false if isnt</returns>
        bool Execute();
        bool CanExecute();
        event Action<ICommand> onCommandExecuted;
    }
}
