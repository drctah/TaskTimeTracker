﻿using System;
using System.Diagnostics;
using System.Windows.Input;

namespace TaskTimeTracker.Client.Ui.Commands {
  /// <summary>
  /// A simple relay Command for easy use of the Command pattern.
  /// </summary>
  /// <remarks></remarks>
  [Serializable]
  public class RelayCommand : ICommand {
    private readonly Action<object> execute;
    private readonly Predicate<object> canExecute;

    /// <summary>
    /// Initializes a new instance of the <see cref="RelayCommand"/> class.
    /// </summary>
    /// <param name="execute">The execute.</param>
    /// <remarks></remarks>
    public RelayCommand(Action<object> execute)
      : this(execute, null) {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="RelayCommand"/> class.
    /// </summary>
    /// <param name="execute">The execute.</param>
    /// <param name="canExecute">The can execute.</param>
    /// <remarks></remarks>
    public RelayCommand(Action<object> execute, Predicate<object> canExecute) {
      if (execute == null) {
        throw new ArgumentNullException("execute");
      }

      this.execute = execute;
      this.canExecute = canExecute;
    }

    /// <summary>
    /// Defines the method that determines whether the command can execute in its current state.
    /// </summary>
    /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
    /// <returns>true if this command can be executed; otherwise, false.</returns>
    /// <remarks></remarks>
    [DebuggerStepThrough]
    public bool CanExecute(object parameter) {
      if (this.canExecute == null) {
        return true;
      }

      return this.canExecute(parameter);
    }

    /// <summary>
    /// Occurs when changes occur that affect whether or not the command should execute.
    /// </summary>
    /// <remarks></remarks>
    public event EventHandler CanExecuteChanged {
      add { CommandManager.RequerySuggested += value; }
      remove { CommandManager.RequerySuggested -= value; }
    }

    /// <summary>
    /// Defines the method to be called when the command is invoked.
    /// </summary>
    /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
    /// <remarks></remarks>
    public void Execute(object parameter) {
      this.execute(parameter);
    }
  }
}