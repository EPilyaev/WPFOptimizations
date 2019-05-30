using System.Collections.Generic;
using System.Threading.Tasks;
using MVM.Model;
using MVM.ViewModel;

namespace MVM.Simulation
{
    public sealed class AddEditDataUiInUiThreadSimulation : AddEditSimulationBase
    {
        public AddEditDataUiInUiThreadSimulation(IList<IItemsContainer> itemsContainerViewModels,
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

                foreach (var itemsContainer in ItemsContainerViewModels)
                {
                    DoHardWork(ItemsPerIteration);

                    var items = new List<Item>();

                    if (whatToDo == WhatToDo.Add || whatToDo == WhatToDo.Edit)
                    {
                        for (int i = 0; i < ItemsPerIteration; i++)
                        {
                            items.Add(itemsContainer.GenerateRandomItem());
                        }
                    }

                    InvokeInUIContext(() =>
                    {
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