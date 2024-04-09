﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Comment_Analyzer.Services
{
    public class RelayCommand(Action<object> execute, Func<object, bool> canExecute = null) : ICommand
    {
        private readonly Action<object> execute = execute;
        private readonly Func<object, bool> canExecute = canExecute;


        public event EventHandler CanExecuteChanged 
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object? parameter) => this.canExecute == null || this.canExecute(parameter);

        public void Execute(object parameter) => this.execute(parameter);
    }
}
