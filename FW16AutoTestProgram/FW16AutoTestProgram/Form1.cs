using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Fw16;

namespace FW16AutoTestProgram
{
    public partial class Form1 : Form
    {

        public EcrCtrl ecrCtrl;

        public Form1()
        {
            InitializeComponent();
            ecrCtrl = new EcrCtrl();
        }

        private void ConnectButtonClick(object sender, EventArgs e)
        {
            try
            {
                ecrCtrl.Init();
            }
            catch (EcrException excep)
            {
                ecrCtrl.Reconnect();
            }
            catch(System.IO.IOException excep)
            {
                MessageBox.Show(excep.Message);
            }
        }
    }
}
