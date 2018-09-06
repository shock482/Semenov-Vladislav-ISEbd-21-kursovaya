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
    /// Логика взаимодействия для FormOrders.xaml
    /// </summary>
    public partial class FormOrders : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        private readonly IOrderService service;

        public int Id { set { id = value; } }

        private int id;
        public FormOrders(IOrderService service)
        {
            InitializeComponent();
            Loaded += FormOrders_Load;
            this.service = service;
        }

        private void FormOrders_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                List<OrderViewModel> list = service.GetList(id);
                if (list != null)
                {
                    dataGridViewOrders.ItemsSource = list;
                    dataGridViewOrders.Columns[0].Visibility = Visibility.Hidden;
                    dataGridViewOrders.Columns[1].Width = DataGridLength.Auto;
                    dataGridViewOrders.Columns[3].Visibility = Visibility.Hidden;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void buttonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridViewOrders.SelectedItem != null)
            {
                var form = Container.Resolve<FormOrder>();
                form.Id = ((OrderViewModel)dataGridViewOrders.SelectedItem).Id;
                if (form.ShowDialog() == true)
                    LoadData();
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormOrder>();
            form.ID = id;
            if (form.ShowDialog() == true)
                LoadData();
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridViewOrders.SelectedItem != null)
            {
                if (MessageBox.Show("Удалить запись?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {

                    int id = ((OrderViewModel)dataGridViewOrders.SelectedItem).Id;
                    try
                    {
                        service.DelElement(id);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    LoadData();
                }
            }
        }
    }
}
