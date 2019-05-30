using System.Collections.Generic;
using MVM.Model;
using MVM.Simulation;

namespace MVM.ViewModel
{
    public sealed class MainViewModel : ViewModelBase
    {
        private bool _isSimulating;
        private AddEditSimulationBase _currentSimulation;
        private List<IItemsContainer> _allItemsContainers;
        private IItemsContainer _firstItemsContainer;
        private IItemsContainer _secondItemsContainer;
        private IItemsContainer _thirdItemsContainer;

        public MainViewModel()
        {
            SimulateCommand = new RelayCommand(SimulateCommandExecute, o => !IsSimulating);
            StopSimulationCommand = new RelayCommand(StopSimulationCommandExecute, o => IsSimulating);
        }

        public RelayCommand SimulateCommand { get; }
        public RelayCommand StopSimulationCommand { get; }
        public SimulationTypes SimulationType { get; set; }

        public IItemsContainer FirstItemsContainer
        {
            get => _firstItemsContainer;
            set
            {
                _firstItemsContainer = value;
                OnPropertyChanged(nameof(FirstItemsContainer));
            }
        }

        public IItemsContainer SecondItemsContainer
        {
            get => _secondItemsContainer;
            set
            {
                _secondItemsContainer = value;
                OnPropertyChanged(nameof(SecondItemsContainer));
            }
        }

        public IItemsContainer ThirdItemsContainer
        {
            get => _thirdItemsContainer;
            set
            {
                _thirdItemsContainer = value;
                OnPropertyChanged(nameof(ThirdItemsContainer));
            }
        }

        public SimulationSettings SimulationSettings { get; }
            = new SimulationSettings(20, 100, 1000);

        public bool IsSimulating
        {
            get => _isSimulating;
            set
            {
                _isSimulating = value;
                OnPropertyChanged(nameof(IsSimulating));
            }
        }

        private void SimulateCommandExecute(object obj)
        {
            
            IsSimulating = true;

            switch (SimulationType)
            {
                case SimulationTypes.AddEditDataAllInUiThread:
                    CreateItemContainers();
                    _currentSimulation = new AddEditDataAllInUiThreadSimulation(_allItemsContainers, SimulationSettings);
                    break;
                case SimulationTypes.AddEditDataUiInUiThread:
                    CreateItemContainers();
                    _currentSimulation = new AddEditDataUiInUiThreadSimulation(_allItemsContainers, SimulationSettings);
                    break;
                case SimulationTypes.AddEditDataParallel:
                    CreateItemContainers();
                    _currentSimulation = new AddEditDataParallelSimulation(_allItemsContainers, SimulationSettings);
                    break;
                case SimulationTypes.AddEditParallelWithLock:
                    CreateItemContainersWithLock();
                    _currentSimulation = new AddEditParallelWithLockSimulation(_allItemsContainers, SimulationSettings);
                    break;
                default:
                    _currentSimulation = new AddEditSimulationBase(null, SimulationSettings);
                    break;
            }

            _currentSimulation.BeginSimulation();
        }

        private void CreateItemContainers()
        {
            FirstItemsContainer = new ItemsContainer();
            SecondItemsContainer = new ItemsContainer();
            ThirdItemsContainer = new ItemsContainer();

            _allItemsContainers = new List<IItemsContainer>
                {FirstItemsContainer, SecondItemsContainer, ThirdItemsContainer};
        }
        
        private void CreateItemContainersWithLock()
        {
            FirstItemsContainer = new ItemsContainerWithLock();
            SecondItemsContainer = new ItemsContainerWithLock();
            ThirdItemsContainer = new ItemsContainerWithLock();

            _allItemsContainers = new List<IItemsContainer>
                {FirstItemsContainer, SecondItemsContainer, ThirdItemsContainer};
        }

        private void StopSimulationCommandExecute(object obj)
        {
            _currentSimulation.EndSimulation();
            IsSimulating = false;
        }
    }
}