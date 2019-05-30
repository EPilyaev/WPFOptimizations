using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MVM.Model;

namespace MVM.ViewModel
{
    public class ItemsContainer : ViewModelBase, IItemsContainer
    {
        private readonly Random _random = new Random();
        
        public ItemsContainer()
        {
            Items = new ObservableCollection<Item>();
        }

        public ObservableCollection<Item> Items { get; }
        public virtual object ItemsLock { get; } = null;

        public void AddSomeItems(List<Item> items)
        {
            foreach (var item in items)
            {
                Items.Add(item);
            }
        }

        public void EditSomeItems(List<Item> items)
        {
            if (Items.Count == 0) return;
            
            foreach (var item in items)
            {
                EditItem(item,_random.Next(Items.Count));
            }
        }

        public void RemoveSomeItems(int countItems)
        {
            if (Items.Count == 0) return;

            for (int i = 0; i < countItems; i++)
            {
                if (Items.Count > 0)
                {
                    Items.RemoveAt(_random.Next(Items.Count));
                }
                else
                {
                    return;
                }
            }
        }

        private void EditItem(Item editedState, int position)
        {
            var item = Items[position];

            item.Info = editedState.Info;
            item.Name = editedState.Name;
            item.Status = editedState.Status;
            item.Price = editedState.Price;
            item.Quantity = editedState.Quantity;
            item.Store = editedState.Store;
        }
        
        public Item GenerateRandomItem()
        {
            return new Item
            {
                Info = "SomeInfo " + _random.Next(10000),
                Name = "SomeName " + _random.Next(10000),
                Price = _random.NextDouble() * 100000,
                Quantity = _random.Next(100000),
                Status = (ItemStatus) _random.Next(4),
                Store = "SomeStore " + _random.Next(10000)
            };
        }
    }
}