using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using MVM.Model;
using MVM.ViewModel;

namespace MVM.Simulation
{
    public sealed class AddEditDataParallelSimulation : AddEditSimulationBase
    {
        public AddEditDataParallelSimulation(IList<IItemsContainer> itemsContainerViewModels,
            SimulationSettings settings)
            : base(itemsContainerViewModels, settings)
        {
        }

        protected override async Task SimulateAsync()
        {
            while (true)
            {
                TaskCancellation.Token.ThrowIfCancellationRequested();

                await Task.Delay(SimulationDelayMs, TaskCancellation.Token);

                WhatToDo whatToDo = WhatToDoNext();

                ConcurrentStack<List<Item>> itemsStack = new ConcurrentStack<List<Item>>();
                
                Parallel.ForEach(ItemsContainerViewModels, itemsContainer =>
                {
                    DoHardWork(ItemsPerIteration);

                    if (whatToDo == WhatToDo.Remove) return;
                    
                    var items = new List<Item>();
                    
                    for (int i = 0; i < ItemsPerIteration; i++)
                    {
                        items.Add(itemsContainer.GenerateRandomItem());
                    }
                    
                    itemsStack.Push(items);
                    
                });

                foreach (var itemsContainer in ItemsContainerViewModels)
                {
                    InvokeInUIContext(() =>
                    {
                        itemsStack.TryPop(out var items);
                        switch (whatToDo)
                        {
                            case WhatToDo.Add:
                                itemsContainer.AddSomeItems(items);
                                break;

                            case WhatToDo.Edit:
                                itemsContainer.EditSomeItems(items);
                                break;

                            case WhatToDo.Remove:
                                itemsContainer.RemoveSomeItems(ItemsPerIteration);
                                break;
                        }
                    });
                }
            }

            // ReSharper disable once FunctionNeverReturns
        }
    }
}