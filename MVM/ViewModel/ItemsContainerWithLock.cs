using System.Windows.Data;

namespace MVM.ViewModel
{
    public class ItemsContainerWithLock : ItemsContainer
    {
        public ItemsContainerWithLock()
        {
            BindingOperations.CollectionRegistering += BindingOperationsOnCollectionRegistering;
        }

        private void BindingOperationsOnCollectionRegistering
            (object sender, CollectionRegisteringEventArgs e)
        {
            if (ReferenceEquals(e.Collection, Items))
            {
                BindingOperations.EnableCollectionSynchronization(Items, ItemsLock);
            }
        }
        public override object ItemsLock { get; } = new object();
    }
}