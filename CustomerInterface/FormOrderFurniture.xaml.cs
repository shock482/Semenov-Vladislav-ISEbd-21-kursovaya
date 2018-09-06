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
    /// Логика взаимодействия для FormOrderFurniture.xaml
    /// </summary>
    public partial class FormOrderFurniture : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        public OrderFurnitureViewModel Model { set { model = value; } get { return model; } }

        private readonly IFurnitureService service;

        private OrderFurnitureViewModel model;

        public FormOrderFurniture(IFurnitureService service)
        {
            InitializeComponent();
            Loaded += FormOrderFurniture_Load;
            comboBoxFurniture.SelectionChanged += comboBoxFurniture_SelectedIndexChanged;
            comboBoxFurniture.SelectionChanged += new SelectionChangedEventHandler(comboBoxFurniture_SelectedIndexChanged);
            this.service = service;
        }

        private void FormOrderFurniture_Load(object sender, EventArgs e)
        {
            List<FurnitureViewModel> list = service.GetList();
            try
            {
                if (list != null)
                {
                    comboBoxFurniture.DisplayMemberPath = "FurnitureName";
                    comboBoxFurniture.SelectedValuePath = "Id";
                    comboBoxFurniture.ItemsSource = list;
                    comboBoxFurniture.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (model != null)
            {
                comboBoxFurniture.IsEnabled = false;
                foreach (FurnitureViewModel item in list)
                {
                    if (item.FurnitureName == model.FurnitureName)
                    {
                        comboBoxFurniture.SelectedItem = item;
                    }
                }
                textBoxPrice.Text = model.Price.ToString();
            }
        }

        private void CalcSum()
        {
            if (comboBoxFurniture.SelectedItem != null)
            {
                try
                {
                    int id = ((FurnitureViewModel)comboBoxFurniture.SelectedItem).Id;
                    FurnitureViewModel product = service.GetElement(id);
                    textBoxPrice.Text = product.Price.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void textBoxPrice_TextChanged(object sender, EventArgs e)
        {
            CalcSum();
        }

        private void comboBoxFurniture_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalcSum();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (comboBoxFurniture.SelectedItem == null)
            {
                MessageBox.Show("Выберите услугу", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                if (model == null)
                {
                    model = new OrderFurnitureViewModel
                    {
                        FurnitureId = Convert.ToInt32(comboBoxFurniture.SelectedValue),
                        FurnitureName = comboBoxFurniture.Text,
                        Price = Convert.ToDecimal(textBoxPrice.Text)
                    };
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
