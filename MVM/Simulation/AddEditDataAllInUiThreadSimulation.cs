using System.Collections.Generic;
using System.Threading.Tasks;
using MVM.Model;
using MVM.ViewModel;

namespace MVM.Simulation
{
    public sealed class AddEditDataAllInUiThreadSimulation : AddEditSimulationBase
    {
        public AddEditDataAllInUiThreadSimulation(IList<IItemsContainer> itemsContainerViewModels,
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
                
                InvokeInUIContext(() =>
                {
                    foreach (var itemsContainer in ItemsContainerViewModels)
                    {
                        DoHardWork(ItemsPerIteration);

                        List<Item> items = new List<Item>();
                        
                        switch (whatToDo)
                        {
                            case WhatToDo.Add:
                                for (int i = 0; i < ItemsPerIteration; i++)
                                {
                                    items.Add(itemsContainer.GenerateRandomItem());
                                }
                                itemsContainer.AddSomeItems(items);
                                break;

                            case WhatToDo.Edit:
                                for (int i = 0; i < ItemsPerIteration; i++)
                                {
                                    items.Add(itemsContainer.GenerateRandomItem());
                                }
                                itemsContainer.EditSomeItems(items);
                                break;

                            case WhatToDo.Remove:
                                itemsContainer.RemoveSomeItems(ItemsPerIteration);
                                break;
                        }
                    }
                });

            }
            // ReSharper disable once FunctionNeverReturns
        }
    }
}