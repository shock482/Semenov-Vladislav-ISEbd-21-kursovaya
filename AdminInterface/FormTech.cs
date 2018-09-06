using Service.BindingModel;
using Service.Interfaces;
using Service.ViewModel;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;
using Unity.Attributes;

namespace AdminInterface
{
    public partial class FormTech : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        public int Id { set { id = value; } }

        private readonly IFurnitureService service;

        private int? id;

        private List<OrderFurnitureViewModel> zakazFurnitures;

        public FormTech(IFurnitureService service)
        {
            InitializeComponent();
            this.service = service;
        }

        private void FormFurniture_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    FurnitureViewModel view = service.GetElement(id.Value);
                    if (view != null)
                    {
                        textBoxName.Text = view.FurnitureName;
                        textBoxPrice.Text = view.Price.ToString();
                        //zakazFurnitures = view.OrderFurnitures;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                zakazFurnitures = new List<OrderFurnitureViewModel>();
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPrice.Text))
            {
                MessageBox.Show("Заполните цену", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (id.HasValue)
                {
                    service.UpdElement(new FurnitureBindingModel
                    {
                        Id = id.Value,
                        FurnitureName = textBoxName.Text,
                        Price = Convert.ToInt32(textBoxPrice.Text),
                    });
                }
                else
                {
                    service.AddElement(new FurnitureBindingModel
                    {
                        FurnitureName = textBoxName.Text,
                        Price = Convert.ToInt32(textBoxPrice.Text),
                    });
                }
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
