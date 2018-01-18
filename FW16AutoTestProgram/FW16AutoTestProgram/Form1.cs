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
        public int[] counters = new int[22];
        public int[] emptyRegisters = new int[] { 6, 8,20, 30,40, 50, 56, 57, 58, 59, 70, 76, 77, 78, 79, 90, 100,110, 126, 127, 128, 129, 190, 195, 196, 197, 198, 199, 207, 208, 209, 217, 218, 219, 227, 228, 229 };

        public Form1()
        {
            InitializeComponent();
            ecrCtrl = new EcrCtrl();
        }

        private void ConnectButtonClick(object sender, EventArgs e)
        {
            ConnectToFW();
        }

        void ConnectToFW(int serialPort = 1, int baudRate = 57600)
        {
            try
            {
                ecrCtrl.Init(serialPort, baudRate);
            }
            catch (EcrException excep)
            {
                ecrCtrl.Reconnect();
                System.Diagnostics.Debug.Write(excep.Message);
            }
            catch (System.IO.IOException excep)
            {
                MessageBox.Show(excep.Message);
            }
            catch (System.UnauthorizedAccessException excep)
            {
                MessageBox.Show(excep.Message);
            }
        }
        void ShowInformation()
        {

        }

        void getCounterData(int column)
        {
            dataGridView2.RowCount = 22;
            for (ushort i = 1; i <= 22; i++)
            {
                dataGridView2.Rows[i - 1].Cells[column].Value = ecrCtrl.Info.GetCounter(i);
                dataGridView2.Rows[i - 1].Cells[0].Value = i;
            }
        }

        void getRegisterrData(int column)
        {//236
            for (ushort i = 1; i <= 236; i++)
            {/*
                dataGridView1.Rows[i - 1].Cells[column].Value = ecrCtrl.Info.GetRegister(i);
                dataGridView1.Rows[i - 1].Cells[0].Value = i;
                */
                if(Array.IndexOf(emptyRegisters,i)==-1)dataGridView1.Rows.Add(i, "", ecrCtrl.Info.GetRegister(i));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            getCounterData(2);
            getRegisterrData(2);
        }
    }
}
