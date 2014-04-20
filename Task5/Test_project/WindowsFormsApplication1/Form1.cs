using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataObjects.Entities;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private Controller _controller;
        private Controller Ctrl
        {
            get
            {
                if (_controller == null)
                {
                    _controller = new Controller();
                }
                return _controller;
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void connector_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;

            if (rb == null)
            {
                MessageBox.Show("Sender is not a RadioButton");
                return;
            }

            Controller.ConnecterType connecter = Controller.ConnecterType.ADO;
            if (adoRadio.Checked)
            {
                connecter = Controller.ConnecterType.ADO;
            }
            else if (myORMradio.Checked)
            {
                connecter = Controller.ConnecterType.MyORM;
            }
            Ctrl.ChosenConnecterType = connecter;
        }
        private void entity_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;

            if (rb == null)
            {
                MessageBox.Show("Sender is not a RadioButton");
                return;
            }

            Controller.EntityType tmp = Controller.EntityType.Article;
            if (articleRadio.Checked)
            {
                tmp = Controller.EntityType.Article;
            }
            else if (personRadio.Checked)
            {
                tmp = Controller.EntityType.Person;
            }
            Ctrl.ChosenEntityType = tmp;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Ctrl.GetAll(dataGridView1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int id = int.Parse(idBox.Text);
            Ctrl.Select(dataGridView1,id);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            object insertingObj;
            switch (Ctrl.ChosenEntityType)
            {
                case Controller.EntityType.Article:
                    break;
                case Controller.EntityType.Person:
                    break;
            }
            //Ctrl.Insert();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int id = int.Parse(idBox.Text);
            Ctrl.Delete(id);
        }
    }
}
