using Models;
using Service;
using Service.BindingModel;
using Service.Interfaces;
using System;
using System.Linq;
using System.Windows;
using Unity;
using Unity.Attributes;

namespace CustomerInterface
{
    /// <summary>
    /// Логика взаимодействия для FormPayEntry.xaml
    /// </summary>
    public partial class FormPayEntry : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        private MMFdbContext context;

        public int Id { set { id = value; } }

        private readonly IMainService serviceM;

        private int id;

        public FormPayEntry(IMainService serviceM, MMFdbContext context)
        {
            InitializeComponent();
            Loaded += FormPayEntry_Load;
            this.serviceM = serviceM;
            this.context = context;
        }

        private void FormPayEntry_Load(object sender, EventArgs e)
        {
            try
            {
                if (id == 0)
                {
                    MessageBox.Show("Не указан заказ", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    Close();
                }

                Entry element = context.Entrys.FirstOrDefault(kl => kl.Id == id);
                decimal summ = element.Sum;
                decimal payment = element.SumPay;
                decimal zadol = summ - payment;

                textBoxSumm.Text = summ.ToString();
                textBoxSumpay.Text = payment.ToString();
                textBoxZadol.Text = zadol.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Entry element = context.Entrys.FirstOrDefault(kl => kl.Id == id);
            decimal sumpay = element.SumPay;

            if (textBoxSum.Text == null)
            {
                MessageBox.Show("Введите сумму оплаты", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                if (sumpay + Convert.ToDecimal(textBoxSum.Text) > element.Sum)
                {
                    MessageBox.Show("Сумма оплаты больше суммы заказа", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                if (sumpay + Convert.ToDecimal(textBoxSum.Text) < element.Sum)
                {
                    serviceM.PayPartEntry(new EntryBindingModel
                    {
                        Id = id,
                        SumPay = sumpay + Convert.ToDecimal(textBoxSum.Text)
                    });
                    MessageBox.Show("Сохранение прошло успешно", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                    DialogResult = true;
                    Close();
                }
                if (sumpay + Convert.ToDecimal(textBoxSum.Text) == element.Sum)
                {
                    serviceM.PayEntry(new EntryBindingModel
                    {
                        Id = id,
                        SumPay = sumpay + Convert.ToDecimal(textBoxSum.Text)
                    });
                    MessageBox.Show("Сохранение прошло успешно", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                    DialogResult = true;
                    Close();
                }
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
