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
    /// Логика взаимодействия для FormCreateEntry.xaml
    /// </summary>
    public partial class FormCreateEntry : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }


        private readonly IOrderService serviceZ;

        private readonly IMainService serviceM;

        public int Id { set { id = value; } }

        private int id;

        public FormCreateEntry(IOrderService serviceZ, IMainService serviceM)
        {
            InitializeComponent();
            Loaded += FormCreateEntry_Load;
            comboBoxOrder.SelectionChanged += comboBoxOrder_SelectedIndexChanged;
            comboBoxOrder.SelectionChanged += new SelectionChangedEventHandler(comboBoxOrder_SelectedIndexChanged);

            this.serviceZ = serviceZ;
            this.serviceM = serviceM;
        }

        private void FormCreateEntry_Load(object sender, EventArgs e)
        {
            try
            {

                List<OrderViewModel> listZ = serviceZ.GetList(id);
                if (listZ != null)
                {
                    comboBoxOrder.DisplayMemberPath = "OrderName";
                    comboBoxOrder.SelectedValuePath = "Id";
                    comboBoxOrder.ItemsSource = listZ;
                    comboBoxOrder.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CalcSum()
        {
            if (comboBoxOrder.SelectedItem != null)
            {
                try
                {
                    int id = ((OrderViewModel)comboBoxOrder.SelectedItem).Id;
                    OrderViewModel product = serviceZ.GetElement(id);
                    textBoxSum.Text = product.Price.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void textBoxSum_TextChanged(object sender, EventArgs e)
        {
            CalcSum();
        }

        private void comboBoxOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalcSum();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {

            if (comboBoxOrder.SelectedItem == null)
            {
                MessageBox.Show("Выберите заказ", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (datePickerDay.SelectedDate == null)
            {
                MessageBox.Show("Выберите предпочтительную дату доставки", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (datePickerDay.SelectedDate <= DateTime.Now.Date)
            {
                MessageBox.Show("Дата доставки должна быть позже текущего дня", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                serviceM.CreateEntry(new EntryBindingModel
                {
                    CustomerId = id,
                    OrderId = ((OrderViewModel)comboBoxOrder.SelectedItem).Id,
                    Sum = Convert.ToDecimal(textBoxSum.Text),
                    DataVisit = datePickerDay.SelectedDate.ToString(),
                    

                });
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
