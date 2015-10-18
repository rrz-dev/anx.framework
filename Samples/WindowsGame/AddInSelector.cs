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
using ANX.Framework.NonXNA.SoundSystem;

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

            foreach (IRenderSystemCreator renderSystemCreator in AddInSystemFactory.Instance.GetAvailableCreators<IRenderSystemCreator>())
            {
                cbRenderSystem.Items.Add(renderSystemCreator.Name);
            }
            cbRenderSystem.SelectedItem = AddInSystemFactory.Instance.GetDefaultCreator<IRenderSystemCreator>().Name;

            foreach (var providerName in InputDeviceFactory.Instance.Providers.Select((x) => x.Key))
            {
                cbInputSystem.Items.Add(providerName);
            }
            cbInputSystem.SelectedItem = InputDeviceFactory.Instance.Providers.Select((x) => x.Key).First();

            foreach (ISoundSystemCreator soundSystemCreator in AddInSystemFactory.Instance.GetAvailableCreators<ISoundSystemCreator>())
            {
                cbAudioSystem.Items.Add(soundSystemCreator.Name);
            }
            cbAudioSystem.SelectedItem = AddInSystemFactory.Instance.GetDefaultCreator<ISoundSystemCreator>().Name;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
    }
}
