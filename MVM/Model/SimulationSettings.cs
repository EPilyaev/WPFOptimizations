using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MVM.Model
{
    public sealed class SimulationSettings : INotifyPropertyChanged
    {
        private int _initialItemsCount;
        private int _itemsPerIteration;
        private int _simulationDelayMs;

        public SimulationSettings(int initialItemsCount, int itemsPerIteration, int simulationDelayMs)
        {
            InitialItemsCount = initialItemsCount;
            ItemsPerIteration = itemsPerIteration;
            SimulationDelayMs = simulationDelayMs;
        }

        public int InitialItemsCount
        {
            get => _initialItemsCount;
            set
            {
                _initialItemsCount = value;
                OnPropertyChanged(nameof(InitialItemsCount));
            }
        }

        public int ItemsPerIteration
        {
            get => _itemsPerIteration;
            set
            {
                _itemsPerIteration = value;
                OnPropertyChanged(nameof(ItemsPerIteration));
            }
        }

        public int SimulationDelayMs
        {
            get => _simulationDelayMs;
            set
            {
                _simulationDelayMs = value;
                OnPropertyChanged(nameof(SimulationDelayMs));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}