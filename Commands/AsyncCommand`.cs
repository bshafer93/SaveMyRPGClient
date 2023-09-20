using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Threading.Tasks;
using System.Windows.Input;
namespace SaveMyRPGClient.Commands
{


    public interface IAsyncCommand<in T> : ICommand
    {
        IEnumerable<Task> RunningTasks { get; }
        bool CanExecute(T parameter);
        Task ExecuteAsync(T parameter);
    }

    public class AsyncCommand<T> : IAsyncCommand<T>
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
            return CanExecute((T)parameter);
        }
        async void ICommand.Execute(object? parameter)
        {
            Task runningTask = ExecuteAsync((T)parameter);

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

        public virtual bool CanExecute(T parameter) { return true; }
        public virtual Task ExecuteAsync(T parameter) { return Task.FromResult<object>(null); }
    }
}
