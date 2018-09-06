using Service.BindingModel;
using Service.ViewModel;
using System.Collections.Generic;

namespace Service.Interfaces
{
    public interface IMainService
    {
        List<EntryViewModel> GetList(int id);

        void CreateEntry(EntryBindingModel model);

        void PayEntry(EntryBindingModel model);
        void PayPartEntry(EntryBindingModel model);

        void DelElement(int id);
    }
}
