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
        decimal[] coasts = new decimal[] { 200m, 200.78m };
        decimal[] counts = new decimal[] { 1m, 5m, 0.3m, 1.7m };

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
            Preparation();

            SimpleTest();
        }

        public void Preparation()                                                                        //Функция подготовки к тестам
        {
            ecrCtrl.Service.SetParameter(Native.CmdExecutor.ParameterCode.AbortDocFontSize, "51515");    //отключение печати чека
            if ((ecrCtrl.Info.Status & Fw16.Ecr.GeneralStatus.DocOpened) > 0)                            //закрыть документ если открыт
            {
                ecrCtrl.Service.AbortDoc();
            }
            if ((ecrCtrl.Info.Status & Fw16.Ecr.GeneralStatus.ShiftOpened) > 0)                          //закрыть смену если открыта
            {
                ecrCtrl.Shift.Close(nameOerator);
            }
        }

        public void TestingClosedShift()
        {
            ecrCtrl.Shift.Close(nameOerator);
            ecrCtrl.Shift.BeginCorrection(nameOerator, Fw16.Model.ReceiptKind.Income);
        }

        public void SimpleTest()                                                                    //функция прогона по всем видам товаров и оплат
        {
            ecrCtrl.Shift.Open(nameOerator);                                                        //открытие смены для этого теста
            for (int ReceptKind = 1; ReceptKind < 5; ReceptKind++)
            {
                var document = ecrCtrl.Shift.BeginReceipt(nameOerator, (Fw16.Model.ReceiptKind)ReceptKind, new
                {
                    Taxation = Fs.Native.TaxationType.Agro,
                    CustomerAddress = "adress@mail.ru",
                    SenderAddress = "sender@mail.ru"
                });
                Fw16.Ecr.ReceiptEntry receiptEntry;
                for (int i = 0; i < 48; i++)
                {
                    //создание товара
                    receiptEntry = document.NewItemCosted(i.ToString(), "tovar " + i, counts[i / 12], (Native.CmdExecutor.VatCodeType)((i / 2 % 6) + 1), coasts[i % 2]);
                    document.AddEntry(receiptEntry);                                                //добавления товара в чек
                    textBox1.Text += "Добавлен " + "tovar " + i + "\r\n";
                }
                decimal balance = Math.Round(document.Total / 8, 2);                                //Сумма разделённая на количество типов оплаты.
                for (int i = 7; i > 0; i--)
                {
                    Math.Round(document.AddPayment((Native.CmdExecutor.TenderCode)i, balance));     //оплата всеми способами кроме нала
                }
                balance = document.Total - document.TotalaPaid;                                     //вычисление остатка суммы для оплаты 
                document.AddPayment((Native.CmdExecutor.TenderCode)0, balance);                     //оплата наличнми
                document.Complete();

            }
            for (int ReceptKind = 1; ReceptKind < 5; ReceptKind++)
            {
                var document = ecrCtrl.Shift.BeginCorrection(nameOerator, (Fw16.Model.ReceiptKind)ReceptKind);
                for (int i = 0; i < 7; i++)                                                         //перебор возврата средств всеми способами, целове и дробная суммы
                {
                    document.AddTender((Native.CmdExecutor.TenderCode)(i / 2), coasts[i % 2]);
                }

                for (int i = 0; i < 7; i++)                                                         //перебор налоговых ставок
                {
                    document.AddAmount((Fw16.Model.VatCode)((i / 2) % 6 + 1), coasts[i % 2]);
                }
                document.Complete();                                                                //закрытие смены

            }
            ecrCtrl.Shift.Close(nameOerator);                                                       //закрытие смены этого теста
            //MessageBox.Show("complete");
        }
    }
}
