namespace MVM.Model
{
    public enum SimulationTypes
    {
        AddEditDataAllInUiThread,
        AddEditDataUiInUiThread,
        AddEditDataParallel,
        AddEditParallelWithLock
    }
}