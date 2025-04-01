using System;
using System.Reactive;
using Avalonia.ReactiveUI;
using Avalonia.Threading;
using ReactiveUI;
using SetApp.Models;

namespace SetApp.ViewModels
{
    public class MainWindowViewModel : ReactiveObject
    {
        private Set<string> _set;
        private string _newItem = string.Empty;
        private string _itemToRemove = string.Empty;
        private string _itemToCheck = string.Empty;
        private bool _containsItem;
        private string _elementsString = string.Empty;

        public string ElementsString
        {
            get => _elementsString;
            private set => this.RaiseAndSetIfChanged(ref _elementsString, value);
        }

        public string NewItem
        {
            get => _newItem;
            set => this.RaiseAndSetIfChanged(ref _newItem, value);
        }

        public string ItemToRemove
        {
            get => _itemToRemove;
            set => this.RaiseAndSetIfChanged(ref _itemToRemove, value);
        }

        public string ItemToCheck
        {
            get => _itemToCheck;
            set => this.RaiseAndSetIfChanged(ref _itemToCheck, value);
        }

        public bool ContainsItem
        {
            get => _containsItem;
            private set => this.RaiseAndSetIfChanged(ref _containsItem, value);
        }

        public ReactiveCommand<Unit, Unit> AddCommand { get; }
        public ReactiveCommand<Unit, Unit> RemoveCommand { get; }
        public ReactiveCommand<Unit, Unit> ClearCommand { get; }
        public ReactiveCommand<Unit, Unit> CheckCommand { get; }

        public int Count => _set.Count;
        public bool IsEmpty => _set.IsEmpty;

        public MainWindowViewModel()
        {
            _set = new Set<string>();

            AddCommand = ReactiveCommand.Create(AddItem, outputScheduler: AvaloniaScheduler.Instance);
            RemoveCommand = ReactiveCommand.Create(RemoveItem, outputScheduler: AvaloniaScheduler.Instance);
            ClearCommand = ReactiveCommand.Create(ClearSet, outputScheduler: AvaloniaScheduler.Instance);
            CheckCommand = ReactiveCommand.Create(CheckItem, outputScheduler: AvaloniaScheduler.Instance);

            UpdateElementsString();
        }

        private void AddItem()
        {
            if (!string.IsNullOrEmpty(NewItem))
            {
                _set.Add(NewItem);
                NewItem = ""; 
                UpdateElementsString();
            }
        }

        private void RemoveItem()
        {
            if (!string.IsNullOrEmpty(ItemToRemove))
            {
                _set.Remove(ItemToRemove);
                ItemToRemove = ""; 
                UpdateElementsString();
            }
        }

        private void ClearSet()
        {
            _set.Clear();
            UpdateElementsString();
        }

        private void CheckItem()
        {
            if (!string.IsNullOrEmpty(ItemToCheck))
            {
                ContainsItem = _set.Contains(ItemToCheck);
            }
            else
            {
                ContainsItem = false;
            }
        }

        private void UpdateElementsString()
        {
            Dispatcher.UIThread.InvokeAsync(() =>
            {
                ElementsString = string.Join(", ", _set.ToArray());
                this.RaisePropertyChanged(nameof(Count));
                this.RaisePropertyChanged(nameof(IsEmpty));
            });
        }
    }
}