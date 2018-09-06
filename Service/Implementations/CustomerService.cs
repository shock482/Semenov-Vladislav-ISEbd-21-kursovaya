using Models;
using Service.BindingModel;
using Service.Interfaces;
using Service.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.ImplementationsList
{
    public class CustomerService : ICustomerService
    {
        private MMFdbContext context;

        public CustomerService(MMFdbContext context)
        {
            this.context = context;
        }

        public void AddElement(CustomerBindingModel model)
        {
            Customer element = context.Customers.FirstOrDefault(rec => rec.CustomerFIO == model.CustomerFIO);
            if (element != null)
            {
                throw new Exception("Уже есть клиент с таким именем");
            }
            context.Customers.Add(new Customer
            {
                Id = model.Id,
                CustomerFIO = model.CustomerFIO,
                Mail = model.Mail,
                CustomerPassword = model.CustomerPassword,
                Entrys = null,
            });
            context.SaveChanges();
        }

        public void DelElement(int id)
        {
            Customer element = context.Customers.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                context.Customers.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        public CustomerViewModel GetElement(int id)
        {
            Customer element = context.Customers.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new CustomerViewModel
                {
                    Id = element.Id,
                    CustomerFIO = element.CustomerFIO,
                    Mail = element.Mail,
                    CustomerPassword = element.CustomerPassword
                };
            }
            throw new Exception("Элемент не найден");
        }

        public List<CustomerViewModel> GetList()
        {
            List<CustomerViewModel> result = context.Customers.Select(rec => new CustomerViewModel
            {
                Id = rec.Id,
                CustomerFIO = rec.CustomerFIO,
                Mail = rec.Mail,
                CustomerPassword = rec.CustomerPassword
            })
                .ToList();
            return result;
        }

        public void UpdElement(CustomerBindingModel model)
        {
            Customer element = context.Customers.FirstOrDefault(rec =>
                                    rec.CustomerFIO == model.CustomerFIO && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть клиент с таким ФИО");
            }
            element = context.Customers.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.CustomerFIO = model.CustomerFIO;
            element.Id = model.Id;
            element.Mail = model.Mail;
            element.CustomerPassword = model.CustomerPassword;
            context.SaveChanges();
        }

        public string GenerateLogin(string fio)
        {
            char split = ' ';
            string firstName = fio.Substring(0, fio.IndexOf(split));

            fio = fio.Substring(fio.IndexOf(split) + 1);

            string name = fio.Substring(0, fio.IndexOf(split));

            string namePath = string.Empty;

            int position = 1;

            while (true)
            {
                if (name.Length > 0)
                {
                    namePath += name.First();
                    name = name.Substring(1);
                }
                else
                {
                    position++;
                }
                string login = firstName + "." + namePath + ((position > 1) ? position + "" : "");

                Customer Customer = context.Customers.FirstOrDefault(rec => rec.CustomerFIO.Equals(login));

                if (Customer == null)
                {
                    return login;
                }
            }
        }
    }
}
