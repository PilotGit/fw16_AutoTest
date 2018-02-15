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
                MessageBox.Show(excep.Message);                 //вывод ошибки неверного порта
            }
            catch (System.UnauthorizedAccessException excep)
            {
                MessageBox.Show(excep.Message);                 //вывод ошибки доступа порта
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ecrCtrl.Commands.BeginOfdRead().
        }
    }
}
