using Service.BindingModel;
using Service.Interfaces;
using System;
using System.Windows;
using Microsoft.Reporting.WinForms;
using Unity;
using Unity.Attributes;
using System.Net.Mail;
using System.Net;
using Models;
using Service;
using System.Linq;

namespace CustomerInterface
{
    /// <summary>
    /// Логика взаимодействия для FormCustomerEntrys.xaml
    /// </summary>
    public partial class FormCustomerEntrys : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        private readonly IReportService service;

        private readonly ICustomerService serviceK;

        private MMFdbContext context;

        public int Id { set { id = value; } }

        private int id;

        public FormCustomerEntrys(IReportService service, ICustomerService serviceK, MMFdbContext context)
        {
            InitializeComponent();
            this.service = service;
            this.serviceK = serviceK;
            this.context = context;
        }

        public void SendMail()
        {
            Customer element = context.Customers.FirstOrDefault(kl => kl.Id == id);
            string mail = element.Mail;

            MailAddress from = new MailAddress("LabWork15kafIs@gmail.com", "Furniture Shop");
            MailAddress to = new MailAddress(mail);
            MailMessage m = new MailMessage(from, to);
            m.Subject = "Отчет об оплате";
            m.Body = "<h2></h2>";
            m.Attachments.Add(new Attachment("C://Users/user/Desktop/payment.pdf"));
            m.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential("LabWork15kafIs@gmail.com", "passlab15");
            smtp.EnableSsl = true;
            smtp.Send(m);
            Console.Read();
            MessageBox.Show("Письмо отправлено", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void buttonMake_Click(object sender, EventArgs e)
        {
            if (dateTimePickerFrom.SelectedDate >= dateTimePickerTo.SelectedDate)
            {
                MessageBox.Show("Дата начала должна быть меньше даты окончания", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                reportViewer.LocalReport.ReportEmbeddedResource = "CustomerInterface.ReportCustomerEntry.rdlc";
                ReportParameter parameter = new ReportParameter("ReportParameterPeriod",
                                            "c " + dateTimePickerFrom.SelectedDate.ToString() +
                                            " по " + dateTimePickerTo.SelectedDate.ToString());
                reportViewer.LocalReport.SetParameters(parameter);


                var dataSource = service.GetCustomerEntrys(new ReportBindingModel
                {
                    DateFrom = dateTimePickerFrom.SelectedDate,
                    DateTo = dateTimePickerTo.SelectedDate
                }, id);
                ReportDataSource source = new ReportDataSource("DataSetEntrys", dataSource);
                reportViewer.LocalReport.DataSources.Add(source);

                reportViewer.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void buttonMail_Click(object sender, RoutedEventArgs e)
        {
            if (dateTimePickerFrom.SelectedDate >= dateTimePickerTo.SelectedDate)
            {
                MessageBox.Show("Дата начала должна быть меньше даты окончания", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                service.SaveCustomerEntrys(new ReportBindingModel
                {
                    FileName = "C://Users/user/Desktop/payment.pdf",
                    DateFrom = dateTimePickerFrom.SelectedDate,
                    DateTo = dateTimePickerTo.SelectedDate
                }, id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            SendMail();
        }


    }
}
