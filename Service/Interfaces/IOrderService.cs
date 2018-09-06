using Service.BindingModel;
using Service.ViewModel;
using System.Collections.Generic;

namespace Service.Interfaces
{
    public interface IOrderService
    {
        List<OrderViewModel> GetList(int id);

        OrderViewModel GetElement(int id);

        void AddElement(OrderBindingModel model);

        void UpdElement(OrderBindingModel model);

        void DelElement(int id);
    }
}
