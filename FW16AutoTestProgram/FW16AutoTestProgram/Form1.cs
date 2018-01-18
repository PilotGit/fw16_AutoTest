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

        public EcrCtrl ecrCtrl;                 //подключение к ККТ
        public int[] counters = new int[22];    //массив счётчиков
        public int[] registers = new int[236];  //массив регистров
        //public int[] emptyRegisters = new int[] { 6, 8, 20, 30, 40, 50, 56, 57, 58, 59, 70, 76, 77, 78, 79, 90, 100, 110, 126, 127, 128, 129, 190, 195, 196, 197, 198, 199, 207, 208, 209, 217, 218, 219, 227, 228, 229 };

        public Form1()
        {
            InitializeComponent();
            ecrCtrl = new EcrCtrl();
        }

        private void ConnectButtonClick(object sender, EventArgs e)
        {
            ConnectToFW();
        }
        //функция подключения/переподключения к ККТ
        void ConnectToFW(int serialPort = 1, int baudRate = 57600)
        {
            try
            {
                ecrCtrl.Init(serialPort, baudRate);             //Подключчение по порту и частоте
            }
            catch (EcrException excep)
            {
                ecrCtrl.Reconnect();                            //Переподключение в случае попытки повторного подключения
                System.Diagnostics.Debug.Write(excep.Message);
            }
            catch (System.IO.IOException excep)
            {
                MessageBox.Show(excep.Message);                 //вывод ошибки порта
            }
            catch (System.UnauthorizedAccessException excep)
            {
                MessageBox.Show(excep.Message);                 //вывод ошибки занятости порта
            }
        }
        void ShowInformation()
        {

        }

        void getCounterData()
        {
            dataGridView2.RowCount = 1;
            for (ushort i = 1; i <= 22; i++)
            {
                //dataGridView2.Rows[i - 1].Cells[column].Value = ecrCtrl.Info.GetCounter(i);
                //dataGridView2.Rows[i - 1].Cells[0].Value = i;
                try
                {
                    dataGridView2.Rows.Add(i, "", ecrCtrl.Info.GetCounter(i));  //Добавление строки в таблицу с данными счётчиков
                }
                catch { }
            }
            dataGridView2.Rows.Remove(dataGridView2.Rows[0]);
        }

        void getRegisterrData()
        {
            dataGridView1.RowCount = 1;
            for (ushort i = 1; i <= 236; i++)
            {
                //dataGridView1.Rows[i - 1].Cells[column].Value = ecrCtrl.Info.GetRegister(i);
                //dataGridView1.Rows[i - 1].Cells[0].Value = i;
                //if(Array.IndexOf(emptyRegisters,i)==-1)dataGridView1.Rows.Add(i, "", ecrCtrl.Info.GetRegister(i));
                try
                {
                    dataGridView1.Rows.Add(i, "", ecrCtrl.Info.GetRegister(i)); //добавление строки в тбалицу с данными регистров
                }
                catch { }
            }
            dataGridView1.Rows.Remove(dataGridView1.Rows[0]);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            getCounterData();
            getRegisterrData();
        }
    }
}
