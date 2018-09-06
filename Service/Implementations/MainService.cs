using Models;
using Service.BindingModel;
using Service.Interfaces;
using Service.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;

namespace Service.ImplementationsList
{
    public class MainService : IMainService
    {
        private MMFdbContext context;

        public MainService(MMFdbContext context)
        {
            this.context = context;
        }

        public List<EntryViewModel> GetList(int id)
        {
            List<EntryViewModel> result = context.Entrys
                .Select(rec => new EntryViewModel
                {
                    Id = rec.Id,
                    CustomerId = rec.CustomerId,
                    OrderId = rec.OrderId,
                    DateCreate = SqlFunctions.DateName("dd", rec.DateCreate) + " " +
                                SqlFunctions.DateName("mm", rec.DateCreate) + " " +
                                SqlFunctions.DateName("yyyy", rec.DateCreate),
                    Status = rec.Status.ToString(),
                    Sum = rec.Sum,
                    SumPay = rec.SumPay,
                    DateVisit = rec.DateVisit,
                    CustomerFIO = rec.Customer.CustomerFIO,
                    OrderName = rec.Order.OrderName,
                })
                .ToList();
            return result;
        }

        public void CreateEntry(EntryBindingModel model)
        {
            context.Entrys.Add(new Entry
            {
                CustomerId = model.CustomerId,
                OrderId = model.OrderId,
                DateCreate = DateTime.Now,
                DateVisit = model.DataVisit,
                Sum = model.Sum,
                Status = PaymentState.Не_оплачен
            });
            context.SaveChanges();
        }

        public void PayEntry(EntryBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {

                    Entry element = context.Entrys.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    element.SumPay = model.SumPay;
                    element.Status = PaymentState.Оплачен;
                    context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void PayPartEntry(EntryBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {

                    Entry element = context.Entrys.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    element.SumPay = model.SumPay;
                    element.Status = PaymentState.Оплачен_частично;
                    context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void DelElement(int id)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Entry element = context.Entrys.FirstOrDefault(rec => rec.Id == id);
                    if (element != null)
                    {
                        context.Entrys.Remove(element);
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Элемент не найден");
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
