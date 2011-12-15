using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ANX.Framework.NonXNA;
using ANX.Framework;

namespace WindowsGame1
{
    public partial class AddInSelector : Form
    {
        public AddInSelector()
        {
            InitializeComponent();

            pictureBox1.Image = Resource1.Logo;
        }

        private void AddInSelector_Load(object sender, EventArgs e)
        {
            AddInSystemFactory.Instance.SortAddIns();

            foreach (IRenderSystemCreator renderSystemCreator in AddInSystemFactory.Instance.GetCreators<IRenderSystemCreator>())
            {
                cbRenderSystem.Items.Add(renderSystemCreator.Name);
            }
            cbRenderSystem.SelectedIndex = 0;

            foreach (IInputSystemCreator inputSystemCreator in AddInSystemFactory.Instance.GetCreators<IInputSystemCreator>())
            {
                cbInputSystem.Items.Add(inputSystemCreator.Name);
            }
            cbInputSystem.SelectedIndex = 0;

            foreach (ISoundSystemCreator soundSystemCreator in AddInSystemFactory.Instance.GetCreators<ISoundSystemCreator>())
            {
                cbAudioSystem.Items.Add(soundSystemCreator.Name);
            }
            cbAudioSystem.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
    }
}
