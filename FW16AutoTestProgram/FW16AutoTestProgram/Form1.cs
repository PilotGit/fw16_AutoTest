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
        string nameOerator = "test program";    //имя касира 

        Fw16.Ecr.ReceiptEntry[] receiptEntry = new Fw16.Ecr.ReceiptEntry[20];

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
                ShowInformation();
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
                label_stats_connect.Text = "ККТ: подключено";
                label_version.Text = "Версия прошивки :" + ecrCtrl.Info.FactoryInfo.FwBuild;
                label_firmware.Text = "Код firmware:" + ecrCtrl.Info.FactoryInfo.FwType;
                label_id.Text = "Серийный номер ККТ:" + ecrCtrl.Info.EcrInfo.Id;
                //MessageBox.Show(Convert.ToString( ecrCtrl.Info.));
        }

        private void BeginTest(object sender, EventArgs e)
        { 
            if((ecrCtrl.Info.Status & Fw16.Ecr.GeneralStatus.DocOpened) > 0)
            {
                ecrCtrl.Service.AbortDoc();
            }
            if((ecrCtrl.Info.Status & Fw16.Ecr.GeneralStatus.ShiftOpened) > 0)
            {
                ecrCtrl.Shift.Close(nameOerator);
            }
            SimpleTest();
        }

        public void TestingClosedShift()
        {
            ecrCtrl.Shift.Close(nameOerator);
            ecrCtrl.Shift.BeginCorrection(nameOerator, Fw16.Model.ReceiptKind.Income);
        }

        public void SimpleTest()
        {
            ecrCtrl.Shift.Open(nameOerator);
            var document = ecrCtrl.Shift.BeginReceipt(nameOerator, Fw16.Model.ReceiptKind.Income, new
            {
                Taxation = Fs.Native.TaxationType.Agro,
                CustomerAddress = "adress@mail.ru",
                SenderAddress = "sender@mail.ru"
            });
            for (int i = 0; i < 6; i++)
            {
                receiptEntry[i] = document.NewItemCosted("1", "tovar", 1m, (Native.CmdExecutor.VatCodeType) (i+1), 200);
                document.AddEntry(receiptEntry[i]);
            }
            for (int i = 5; i >= 0; i--)
            {
                document.AddPayment((Native.CmdExecutor.TenderCode)i, 200);
            }
            document.Complete();
            MessageBox.Show("complete");
        }
    }
}
