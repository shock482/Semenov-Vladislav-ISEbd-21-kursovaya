using Service.BindingModel;
using Service.Interfaces;
using Service.ViewModel;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Unity;
using Unity.Attributes;

namespace CustomerInterface
{
    /// <summary>
    /// Логика взаимодействия для FormOrder.xaml
    /// </summary>
    public partial class FormOrder : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        public int Id { set { id = value; } }
        public int ID { set { iD = value; } }
        private readonly IOrderService service;

        private int? id;
        private int iD;
        private List<OrderFurnitureViewModel> orderFurnitures;

        public FormOrder(IOrderService service)
        {
            InitializeComponent();
            this.service = service;
            Loaded += FormOrder_Load;
        }

        private void FormOrder_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    OrderViewModel view = service.GetElement(id.Value);
                    if (view != null)
                    {
                        textBoxName.Text = view.OrderName;
                        textBoxPrice.Text = view.Price.ToString();
                        orderFurnitures = view.OrderFurnitures;
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
                orderFurnitures = new List<OrderFurnitureViewModel>();
        }

        private void LoadData()
        {
            try
            {
                if (orderFurnitures != null)
                {
                    dataGridViewOrder.ItemsSource = null;
                    dataGridViewOrder.ItemsSource = orderFurnitures;
                    dataGridViewOrder.Columns[0].Visibility = Visibility.Hidden;
                    dataGridViewOrder.Columns[1].Visibility = Visibility.Hidden;
                    dataGridViewOrder.Columns[2].Visibility = Visibility.Hidden;
                    dataGridViewOrder.Columns[3].Width = DataGridLength.Auto;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Summ()
        {
            decimal sum = 0;
            for (int i = 0; i < orderFurnitures.Count; ++i)
            {
                sum = sum + orderFurnitures[i].Price;
            }
            textBoxPrice.Text = sum.ToString();
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormOrderFurniture>();
            if (form.ShowDialog() == true)
            {
                if (form.Model != null)
                {
                    if (id.HasValue)
                        form.Model.OrderId = id.Value;
                    orderFurnitures.Add(form.Model);
                }
                LoadData();
                Summ();

            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridViewOrder.SelectedItem != null)
            {
                if (MessageBox.Show("Удалить запись?", "Внимание",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        orderFurnitures.RemoveAt(dataGridViewOrder.SelectedIndex);
                        Summ();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    LoadData();
                }
            }

        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (orderFurnitures == null || orderFurnitures.Count == 0)
            {
                MessageBox.Show("Заполните услуги", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                List<OrderFurnitureBindingModel> productComponentBM = new List<OrderFurnitureBindingModel>();
                for (int i = 0; i < orderFurnitures.Count; ++i)
                {
                    productComponentBM.Add(new OrderFurnitureBindingModel
                    {
                        Id = orderFurnitures[i].Id,
                        OrderId = orderFurnitures[i].OrderId,
                        FurnitureId = orderFurnitures[i].FurnitureId,
                        Price = orderFurnitures[i].Price,
                    });
                }
                if (id.HasValue)
                {
                    service.UpdElement(new OrderBindingModel
                    {
                        Id = id.Value,
                        CustomerID = iD,
                        OrderName = textBoxName.Text,
                        Price = Convert.ToDecimal(textBoxPrice.Text),
                        OrderFurnitures = productComponentBM
                    });
                }
                else
                {
                    service.AddElement(new OrderBindingModel
                    {
                        OrderName = textBoxName.Text,
                        CustomerID = iD,
                        Price = Convert.ToDecimal(textBoxPrice.Text),
                        OrderFurnitures = productComponentBM
                    });
                }
                MessageBox.Show("Сохранение прошло успешно", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
