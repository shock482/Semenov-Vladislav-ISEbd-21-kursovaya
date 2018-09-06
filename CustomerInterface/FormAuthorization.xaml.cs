using Models;
using Service;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using Unity;
using Unity.Attributes;

namespace CustomerInterface
{
    /// <summary>
    /// Логика взаимодействия для FormAuthorization.xaml
    /// </summary>
    public partial class FormAuthorization : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }
        string AdminLogin = "admin";
        string AdminPassword = "admin";
        private MMFdbContext context;
        public FormAuthorization(MMFdbContext context)
        {
            InitializeComponent();
            this.context = context;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (textBoxFIO.Text == AdminLogin && textBoxPass.Password == AdminPassword)
            {
                Close();
                var form = Container.Resolve<AdminInterface.FormMain>();
                form.ShowDialog();
            }

            if (string.IsNullOrEmpty(textBoxFIO.Text))
            {
                MessageBox.Show("Заполните ФИО", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPass.Password))
            {
                MessageBox.Show("Заполните пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            SqlConnection conn = new SqlConnection(@"Data Source=localhost\SQLEXPRESS;Initial Catalog=MMFshopTable;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from Customers where CustomerFIO = '" + textBoxFIO.Text + "' and CustomerPassword = '" + textBoxPass.Password + "'", conn);
            SqlDataReader dt;
            dt = cmd.ExecuteReader();
            int count = 0;

            while (dt.Read())
            {
                count += 1;

            }
            if (count == 1)
            {
                Customer element = context.Customers.FirstOrDefault(client => client.CustomerFIO == textBoxFIO.Text & client.CustomerPassword == textBoxPass.Password);
                int id = element.Id;
               
                var form = Container.Resolve<FormMain>();
                form.Id = id;
                form.ShowDialog();


            }
            else
            {
                MessageBox.Show("Неверные данные!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
