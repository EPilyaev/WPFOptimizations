using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MVM.Model;
using MVM.ViewModel;

namespace MVM.Simulation
{
    public class AddEditSimulationBase : IDisposable
    {
        protected readonly int SimulationDelayMs;
        protected readonly CancellationTokenSource TaskCancellation = new CancellationTokenSource();
        protected readonly IList<IItemsContainer> ItemsContainerViewModels;
        protected readonly int ItemsPerIteration;
        private WhatToDo _currentTodo = WhatToDo.Remove;

        public AddEditSimulationBase(IList<IItemsContainer> itemsContainerViewModels, SimulationSettings settings)
        {
            ItemsContainerViewModels = itemsContainerViewModels;
            ItemsPerIteration = settings.ItemsPerIteration;
            SimulationDelayMs = settings.SimulationDelayMs;
            
            foreach (var itemsContainer in itemsContainerViewModels)
            {
                List<Item> items = Enumerable.Range(0, settings.InitialItemsCount)
                    .Select(i=> itemsContainer.GenerateRandomItem()).ToList();
                
                itemsContainer.AddSomeItems(items);
            }
        }

        public void BeginSimulation()
        {
            Task.Factory.StartNew(async () =>
            {
                try
                {
                    await SimulateAsync();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            });
        }

        public void EndSimulation()
        {
            TaskCancellation.Cancel();
        }

        protected virtual async Task SimulateAsync()
        {
            while (true)
            {
                TaskCancellation.Token.ThrowIfCancellationRequested();
                await Task.Delay(1000);
                Debug.WriteLine("Simulating hard work");
            }
            // ReSharper disable once FunctionNeverReturns
        }

        protected void DoHardWork(int milliseconds)
        {
            SpinWait.SpinUntil(()=>false, milliseconds);
        }

        protected void InvokeInUIContext(Action action)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(action);
        }
        
        
        protected WhatToDo WhatToDoNext()
        {
            
            if (_currentTodo == WhatToDo.Remove) 
                _currentTodo = 0;
            else
                _currentTodo++;

            return _currentTodo;
        }
        
        

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                TaskCancellation.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}