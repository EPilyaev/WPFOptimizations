using System.ComponentModel;
using System.Runtime.CompilerServices;
using MVM.Properties;

namespace MVM.Model
{
    public sealed class Item : INotifyPropertyChanged
    {
        private string _store;
        private string _name;
        private int _quantity;
        private double _price;
        private string _info;
        private ItemStatus _status;

        public string Store
        {
            get => _store;
            set
            {
                if(_store == value) return;
                
                _store = value;
                OnPropertyChanged(nameof(Store));
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                if (_name == value) return;
                
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public int Quantity
        {
            get => _quantity;
            set
            {
                if(_quantity == value) return;
                
                _quantity = value;
                OnPropertyChanged(nameof(Quantity));
            }
        }

        public double Price
        {
            get => _price;
            set
            {
                // ReSharper disable once CompareOfFloatsByEqualityOperator
                if(_price == value) return;
                
                _price = value;
                OnPropertyChanged(nameof(Price));
            }
        }

        public string Info
        {
            get => _info;
            set
            {
                if(_info == value) return;
                
                _info = value;
                OnPropertyChanged(nameof(Info));
            }
        }

        public ItemStatus Status
        {
            get => _status;
            set
            {
                if(_status == value) return;
                
                _status = value;
                OnPropertyChanged(nameof(Status));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}