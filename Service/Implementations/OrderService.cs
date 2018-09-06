using Models;
using Service.BindingModel;
using Service.Interfaces;
using Service.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.ImplementationsList
{
    public class OrderService : IOrderService
    {
        private MMFdbContext context;

        public OrderService(MMFdbContext context)
        {
            this.context = context;
        }

        public List<OrderViewModel> GetList(int id)
        {
            List<OrderViewModel> result = context.Orders.Where(rec => rec.CustomerID == id)
                .Select(rec => new OrderViewModel
                {
                    Id = rec.Id,
                    OrderName = rec.OrderName,
                    CustomerID = rec.CustomerID,
                    Price = rec.Price,
                    OrderFurnitures = context.OrderFurnitures
                            .Where(recPC => recPC.OrderId == rec.Id)
                            .Select(recPC => new OrderFurnitureViewModel
                            {
                                Id = recPC.Id,
                                OrderId = recPC.OrderId,
                                FurnitureId = recPC.FurnitureId,
                                FurnitureName = recPC.Furniture.FurnitureName,
                                Price = recPC.Price
                            })
                            .ToList()
                })
                .ToList();
            return result;
        }

        public OrderViewModel GetElement(int id)
        {
            Order element = context.Orders.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new OrderViewModel
                {
                    Id = element.Id,
                    OrderName = element.OrderName,
                    CustomerID = element.CustomerID,
                    Price = element.Price,
                    OrderFurnitures = context.OrderFurnitures
                            .Where(recPC => recPC.OrderId == element.Id)
                            .Select(recPC => new OrderFurnitureViewModel
                            {
                                Id = recPC.Id,
                                OrderId = recPC.OrderId,
                                FurnitureId = recPC.FurnitureId,
                                FurnitureName = recPC.Furniture.FurnitureName,
                                Price = recPC.Price
                            })
                            .ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(OrderBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {

                    Order element = context.Orders.FirstOrDefault(rec => rec.OrderName == model.OrderName);
                    element = new Order
                    {
                        OrderName = model.OrderName,
                        CustomerID = model.CustomerID,
                        Price = model.Price
                    };
                    context.Orders.Add(element);
                    context.SaveChanges();

                    var groupFurnitures = model.OrderFurnitures
                                                .GroupBy(rec => rec.FurnitureId)
                                                .Select(rec => new
                                                {
                                                    FurnitureId = rec.Key,
                                                    Price = rec.Sum(r => r.Price)
                                                });

                    foreach (var groupFurniture in groupFurnitures)
                    {
                        context.OrderFurnitures.Add(new OrderFurniture
                        {
                            OrderId = element.Id,
                            FurnitureId = groupFurniture.FurnitureId,
                            Price = groupFurniture.Price
                        });
                        context.SaveChanges();
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

        public void UpdElement(OrderBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Order element = context.Orders.FirstOrDefault(rec =>
                                        rec.OrderName == model.OrderName && rec.Id != model.Id);
                    if (element != null)
                    {
                        throw new Exception("Уже есть заказ с таким названием");
                    }
                    element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    element.OrderName = model.OrderName;
                    element.Price = model.Price;
                    context.SaveChanges();


                    var compIds = model.OrderFurnitures.Select(rec => rec.FurnitureId).Distinct();
                    var updateFurnitures = context.OrderFurnitures
                                                    .Where(rec => rec.OrderId == model.Id &&
                                                        compIds.Contains(rec.FurnitureId));
                    foreach (var updateFurniture in updateFurnitures)
                    {
                        updateFurniture.Price = model.OrderFurnitures
                                                        .FirstOrDefault(rec => rec.Id == updateFurniture.Id).Price;
                    }
                    context.SaveChanges();
                    context.OrderFurnitures.RemoveRange(
                                        context.OrderFurnitures.Where(rec => rec.OrderId == model.Id &&
                                                                            !compIds.Contains(rec.FurnitureId)));
                    context.SaveChanges();

                    var groupFurnitures = model.OrderFurnitures
                                                .Where(rec => rec.Id == 0)
                                                .GroupBy(rec => rec.FurnitureId)
                                                .Select(rec => new
                                                {
                                                    FurnitureId = rec.Key,
                                                    Price = rec.Sum(r => r.Price)
                                                });
                    foreach (var groupFurniture in groupFurnitures)
                    {
                        OrderFurniture elementPC = context.OrderFurnitures
                                                .FirstOrDefault(rec => rec.OrderId == model.Id &&
                                                                rec.FurnitureId == groupFurniture.FurnitureId);
                        if (elementPC != null)
                        {
                            elementPC.Price += groupFurniture.Price;
                            context.SaveChanges();
                        }
                        else
                        {
                            context.OrderFurnitures.Add(new OrderFurniture
                            {
                                OrderId = model.Id,
                                FurnitureId = groupFurniture.FurnitureId,
                                Price = groupFurniture.Price
                            });
                            context.SaveChanges();
                        }
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

        public void DelElement(int id)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Order element = context.Orders.FirstOrDefault(rec => rec.Id == id);
                    if (element != null)
                    {

                        context.OrderFurnitures.RemoveRange(
                                            context.OrderFurnitures.Where(rec => rec.OrderId == id));
                        context.Orders.Remove(element);
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
