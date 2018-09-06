﻿using Models;
using Service;
using Service.BindingModel;
using Service.Interfaces;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Windows;
using Unity;
using Unity.Attributes;

namespace CustomerInterface
{
    /// <summary>
    /// Логика взаимодействия для FormSendMail.xaml
    /// </summary>
    public partial class FormSendMail : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        private MMFdbContext context;

        private readonly IMainService service;

        private readonly IReportService reportService;

        public int Id { set { id = value; } }

        private int id;

        private string file = "";

        private string mail = "";

        public FormSendMail(IMainService service, IReportService reportService, MMFdbContext context)
        {
            InitializeComponent();
            this.service = service;
            this.reportService = reportService;
            this.context = context;
        }

        void SendMail()
        {
            Customer element = context.Customers.FirstOrDefault(kl => kl.Id == id);
            if (textBoxMail.Text != "")
            {
                mail = textBoxMail.Text.ToString();
            }
            else
            {
                mail = element.Mail;
            }
            if (checkBoxWord.IsChecked == true)
            {
                file = "C://Users/user/Desktop/Price.docx";
            }
            else
            {
                file = "E://Price.xlsx";
            }

            if (!Regex.IsMatch(mail, @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$"))
            {
                MessageBox.Show("Неверный формат для электронной почты", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MailAddress from = new MailAddress("LabWork15kafIs@gmail.com", "Furniture Shop");
            MailAddress to = new MailAddress(mail);
            MailMessage m = new MailMessage(from, to);
            m.Subject = "Прайс мебели";
            m.Body = "<h2></h2>";
            m.Attachments.Add(new Attachment(file));
            m.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential("LabWork15kafIs@gmail.com", "passlab15");
            smtp.EnableSsl = true;
            smtp.Send(m);
            Console.Read();
            MessageBox.Show("Письмо отправлено", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void buttonMail_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (checkBoxWord.IsChecked == false && checkBoxExcel.IsChecked == false)
                {
                    MessageBox.Show("Выберите формат", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (checkBoxWord.IsChecked == true)
                {
                    reportService.SaveFurniturePriceDocx(new ReportBindingModel
                    {
                        FileName = "C://Users/user/Desktop/Price.docx"
                    });
                    SendMail();
                }
                if (checkBoxExcel.IsChecked == true)
                {
                    checkBoxWord.IsChecked = false;
                    reportService.SaveFurniturePriceExcel(new ReportBindingModel
                    {
                        // FileName = "C://Users/user/Desktop/Price.xlsx"
                        FileName = "E:Price.xlsx"
                    });
                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SendMail();
        }
    }
}
