﻿using System;
using System.Windows.Input;

namespace Calculator.ViewModels
{
    public class ActionCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Action action;
        public ActionCommand(Action action)
        {
            this.action = action;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public void Execute(object parameter)
        {
            action();
        }
    }
}
