using Service.BindingModel;
using Service.Interfaces;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using Unity;
using Unity.Attributes;

namespace CustomerInterface
{
    /// <summary>
    /// Логика взаимодействия для FormReg.xaml
    /// </summary>
    public partial class FormReg : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        public int Id { set { id = value; } }

        private readonly ICustomerService service;

        private int? id;

        public FormReg(ICustomerService service)
        {
            InitializeComponent();
            this.service = service;
        }



        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxFIO.Text))
            {
                MessageBox.Show("Заполните ФИО", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPass.Password))
            {
                MessageBox.Show("Придумайте пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxMail.Text))
            {
                MessageBox.Show("Введите свою почту", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (textBoxPass.Password != textBoxPassRepeat.Password)
            {
                MessageBox.Show("Пароли должны совпадать", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                string fio = textBoxFIO.Text;
                string mail = textBoxMail.Text;
                string pass = textBoxPass.Password;
                if (!string.IsNullOrEmpty(mail))
                {
                    if (!Regex.IsMatch(mail, @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$"))
                    {
                        MessageBox.Show("Неверный формат для электронной почты", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    if (pass.Length > 8)
                    {
                        MessageBox.Show("Пароль должен содержать меньше 8 символов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
                if (id.HasValue)
                {
                    service.UpdElement(new CustomerBindingModel
                    {
                        Id = id.Value,
                        CustomerFIO = textBoxFIO.Text,
                        CustomerPassword = pass,
                        Mail = mail
                    });
                }
                else
                {
                    service.AddElement(new CustomerBindingModel
                    {
                        CustomerFIO = textBoxFIO.Text,
                        CustomerPassword = pass,
                        Mail = mail
                    });
                }
                MessageBox.Show("Регистрация прошла успешно", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                MessageBox.Show(ex.InnerException.Message);
            }

            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
