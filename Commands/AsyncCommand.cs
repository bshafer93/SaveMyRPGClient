using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SaveMyRPGClient.Commands;
namespace SaveMyRPGClient.Commands
{


    public interface IAsyncCommand : ICommand
    {
        IEnumerable<Task> RunningTasks { get; }
        bool CanExecute();
        Task ExecuteAsync();
    }

    public class AsyncCommand: IAsyncCommand
    {
        private readonly ObservableCollection<Task> runningTasks;
        public IEnumerable<Task> RunningTasks { get => runningTasks; }
        protected AsyncCommand() 
        { 
            runningTasks = new ObservableCollection<Task>();
            runningTasks.CollectionChanged += OnRunningTasksChanged;
        }

        private void OnRunningTasksChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            CommandManager.InvalidateRequerySuggested();
        }

        public event EventHandler CanExecuteChanged 
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        
        }

         bool ICommand.CanExecute(object? parameter)
        {
            return CanExecute();
        }
         async void ICommand.Execute(object? parameter)
        {
           Task runningTask = ExecuteAsync();

            runningTasks.Add(runningTask);
            try
            {
                await runningTask;
            }
            finally
            {
                runningTasks.Remove(runningTask);
            }
        }

        public virtual bool CanExecute() { return true; }
        public virtual Task ExecuteAsync() { return Task.FromResult<object>(null); }
    }
}
