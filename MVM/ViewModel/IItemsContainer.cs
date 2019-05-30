using System.Collections.Generic;
using System.Collections.ObjectModel;
using MVM.Model;

namespace MVM.ViewModel
{
    public interface IItemsContainer
    {
        ObservableCollection<Item> Items { get; }
        object ItemsLock { get; }
        void AddSomeItems(List<Item> items);
        void EditSomeItems(List<Item> items);
        void RemoveSomeItems(int countItems);
        Item GenerateRandomItem();
    }
}