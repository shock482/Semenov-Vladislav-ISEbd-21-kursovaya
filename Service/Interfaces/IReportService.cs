using Service.BindingModel;
using Service.ViewModel;
using System.Collections.Generic;

namespace Service.Interfaces
{
    public interface IReportService
    {
        void SaveFurniturePriceDocx(ReportBindingModel model);

        void SaveFurniturePriceExcel(ReportBindingModel model);

        List<CustomerEntrysModel> GetCustomerEntrys(ReportBindingModel model, int id);

        void SaveCustomerEntrys(ReportBindingModel model, int id);
    }
}
