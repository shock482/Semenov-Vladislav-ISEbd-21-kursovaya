using Service.BindingModel;
using Service.ViewModel;
using System.Collections.Generic;

namespace Service.Interfaces
{
    public interface IFurnitureService
    {
        List<FurnitureViewModel> GetList();

        FurnitureViewModel GetElement(int id);

        void AddElement(FurnitureBindingModel model);

        void UpdElement(FurnitureBindingModel model);

        void DelElement(int id);
    }
}
