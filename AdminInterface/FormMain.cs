using Service.Interfaces;
using System;
using System.Windows.Forms;
using Unity;
using Unity.Attributes;

namespace AdminInterface
{
    public partial class FormMain : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        private readonly IMainService service;

        public FormMain(IMainService service)
        {
            InitializeComponent();
            this.service = service;
        }

        //private void добавитьToolStripMenuItem_Click(object sender, System.EventArgs e)
        //{
        //    var form = Container.Resolve<FormFurnitures>();
        //    form.ShowDialog();
        //}

        //private void клиентыToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    var form = Container.Resolve<FormCustomers>();
        //    form.ShowDialog();
        //}

        //private void услугиToolStripMenuItem_Click(object sender, EventArgs e)
        //{

        //}

        private void button1_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormTechs>();
            form.ShowDialog();
        }

        private void ButtonCustomers_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormCustomers>();
            form.ShowDialog();
        }
    }
}
