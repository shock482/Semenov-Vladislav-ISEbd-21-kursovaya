using Models;
using Service.BindingModel;
using Service.Interfaces;
using Service.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.ImplementationsList
{
    public class FurnitureService : IFurnitureService
    {
        private MMFdbContext context;

        public FurnitureService(MMFdbContext context)
        {
            this.context = context;
        }

        public List<FurnitureViewModel> GetList()
        {
            List<FurnitureViewModel> result = context.Furnitures
                .Select(rec => new FurnitureViewModel
                {
                    Id = rec.Id,
                    FurnitureName = rec.FurnitureName,
                    Price = rec.Price
                })
                .ToList();
            return result;
        }

        public FurnitureViewModel GetElement(int id)
        {
            Furniture element = context.Furnitures.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new FurnitureViewModel
                {
                    Id = element.Id,
                    FurnitureName = element.FurnitureName,
                    Price = element.Price
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(FurnitureBindingModel model)
        {
            Furniture element = context.Furnitures.FirstOrDefault(rec => rec.FurnitureName == model.FurnitureName);
            if (element != null)
            {
                throw new Exception("Уже есть мебель с таким названием");
            }
            context.Furnitures.Add(new Furniture
            {
                FurnitureName = model.FurnitureName,
                Price = model.Price
            });
            context.SaveChanges();
        }

        public void UpdElement(FurnitureBindingModel model)
        {
            Furniture element = context.Furnitures.FirstOrDefault(rec =>
                                        rec.FurnitureName == model.FurnitureName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть мебель с таким названием");
            }
            element = context.Furnitures.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.FurnitureName = model.FurnitureName;
            element.Price = model.Price;
            context.SaveChanges();
        }

        public void DelElement(int id)
        {
            Furniture element = context.Furnitures.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                context.Furnitures.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
