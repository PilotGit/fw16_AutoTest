﻿using System;
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
        decimal[] coasts = new decimal[] { 200m, 200.37m };
        decimal[] counts = new decimal[] { 1m, 5m, 0.39m, 1.73m };

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
                MessageBox.Show(excep.Message);                 //вывод ошибки неверного порта
            }
            catch (System.UnauthorizedAccessException excep)
            {
                MessageBox.Show(excep.Message);                 //вывод ошибки доступа порта
            }

        }
        void ShowInformation()
        {
            statsConnectL.Text = "ККТ: подключено";
            versionL.Text = "Версия прошивки: " + ecrCtrl.Info.FactoryInfo.FwBuild;
            firmwareL.Text = "Код firmware: " + ecrCtrl.Info.FactoryInfo.FwType;
            idL.Text = "Серийный номер ККТ: " + ecrCtrl.Info.EcrInfo.Id;
            modelL.Text ="Модель: "+ecrCtrl.Info.EcrInfo.Model;
            //MessageBox.Show(Convert.ToString( ecrCtrl.Info.));
        }

        private void BeginTest(object sender, EventArgs e)
        {
            Preparation();
            SimpleTest();
        }

        public void Preparation()                                                                        //Функция подготовки к тестам
        {
            RequestRegisters();
            RequestCounters();
            ecrCtrl.Service.SetParameter(Native.CmdExecutor.ParameterCode.AbortDocFontSize, "51515");    //отключение печати чека
            if ((ecrCtrl.Info.Status & Fw16.Ecr.GeneralStatus.DocOpened) > 0)
            {
                ecrCtrl.Service.AbortDoc();                                                             //закрыть документ если открыт
            }
            if ((ecrCtrl.Info.Status & Fw16.Ecr.GeneralStatus.ShiftOpened) > 0)
            {
                ecrCtrl.Shift.Close(nameOerator);                                                       //закрыть смену если открыта
            }
        }

        public void SimpleTest()                            //функция прогона по всем видам чеков и чеков коррекции
        {
            ecrCtrl.Shift.Open(nameOerator);                //открытие смены для этого теста
            TestReceipt();                                  //вызов функции тестирования чека
            TestCorrection();                               //вызов функции тестирования чека коррекции
            TestNonFiscal();                                //вызов функции нефискального документа
            ecrCtrl.Shift.Close(nameOerator);               //закрытие смены этого теста

            RequestRegisters();
            RequestCounters();

            LogTB.Text += "Завершено тестирование SimpleTest \r\n";     //логирование
        }

        private void TestNonFiscal()                                                                //тест нефискального документа
        {
            for (int nfdType = 1; nfdType < 4; nfdType++)                                           //Перебор типов нефиксальных документов
            {
                var document = ecrCtrl.Shift.BeginNonFiscal((Native.CmdExecutor.NFDocType)nfdType); //открытие нефиксального документа
                for (int i = 0; i < 14 && nfdType < 3; i++)                                         //
                {
                    var tender = new Fw16.Model.Tender();
                    tender.Amount = coasts[i / 7];
                    tender.Code = (Native.CmdExecutor.TenderCode)(i % 7);
                    document.AddTender(tender);
                }
                document.PrintText("Тестовый текст теста текстовго нефиксального документа");
                document.Complete(Native.CmdExecutor.DocEndMode.Default);                                                                //закрытие нефиксального документа
                LogTB.Text += "Оформлен нефиксальный документ типа " + (Native.CmdExecutor.NFDocType)nfdType + "\r\n";
            }
        }

        private void TestCorrection()
        {
            for (int ReceptKind = 1; ReceptKind < 4; ReceptKind += 2)
            {
                var document = ecrCtrl.Shift.BeginCorrection(nameOerator, (Fw16.Model.ReceiptKind)ReceptKind);
                decimal sum = 0;
                for (int i = 0; i < 7; i++)                                                                         //перебор возврата средств всеми способами, целове и дробная суммы
                {
                    document.AddTender((Native.CmdExecutor.TenderCode)(i / 2), coasts[i % 2]);
                    sum += coasts[i % 2];
                }
                for (int i = 0; i < 5; i++)                                                                         //перебор налоговых ставок
                {
                    document.AddAmount((Fw16.Model.VatCode)((i / 2) + 1), Math.Round(sum/6,2));
                }
                document.AddAmount(Fw16.Model.VatCode.NoVat, sum - Math.Round(sum / 6, 2) * 5);
                document.Complete();                                                                                //закрытие чека корректировки
                LogTB.Text += "Оформлен чек коррекции типа " + (Fw16.Model.ReceiptKind)ReceptKind + "\r\n";         //логирование
            }
        }

        private void TestReceipt()
        {
            for (int ReceptKind = 1; ReceptKind < 5; ReceptKind++)
            {
                var document = ecrCtrl.Shift.BeginReceipt(nameOerator, (Fw16.Model.ReceiptKind)ReceptKind, new
                {
                    Taxation = Fs.Native.TaxationType.Agro,     //налогообложение по умолчанию
                    CustomerAddress = Properties.Settings.Default.customEmail,        //адрес получателя
                    SenderAddress = Properties.Settings.Default.senderEmail            //адрес отправтеля
                });
                Fw16.Ecr.ReceiptEntry receiptEntry;
                for (int i = 0; i < 48; i++)
                {
                    //создание товара
                    receiptEntry = document.NewItemCosted(i.ToString(), "tovar " + i, counts[i / 12], (Native.CmdExecutor.VatCodeType)((i / 2 % 6) + 1), coasts[i % 2]);
                    document.AddEntry(receiptEntry);                                                //добавления товара в чек
                    //textBox1.Text += "Добавлен " + "tovar " + i + "\r\n";
                }
                decimal balance = Math.Round(document.Total / 8, 2);                                //Сумма разделённая на количество типов оплаты.
                for (int i = 7; i > 0; i--)
                {
                    Math.Round(document.AddPayment((Native.CmdExecutor.TenderCode)i, balance));     //оплата всеми способами кроме нала
                }
                balance = document.Total - document.TotalaPaid;                                     //вычисление остатка суммы для оплаты 
                document.AddPayment((Native.CmdExecutor.TenderCode)0, balance);                     //оплата наличнми
                RequestRegisters(160, 181);                                                         //запрос регистров по открытому документу
                document.Complete();
                LogTB.Text += "Оформлен чек типа " + (Fw16.Model.ReceiptKind)ReceptKind + "\r\n";     //логирование
            }
        }

        public void RequestRegisters(ushort startIndex = 0, ushort endIndex = 0)      //запрос значений всех регистров / начиная с индекса / в диапозоне [startIndex,endIndex) 
        {
            endIndex = endIndex > 0 ? endIndex : Properties.Settings.Default.CountRegisters;
            for (ushort i = startIndex; i < endIndex; i++)
            {
                try
                {
                    ecrCtrl.Info.GetRegister(i);
                }
                catch (Exception)
                {
                    LogTB.Text += "Не удолось получить доступ к регистру №" + i+"\r\n";
                }
            }
            LogTB.Text += "Запрошены данные с регистров с " + startIndex + " по " + endIndex + "\r\n";     //логирование
        }

        public void RequestCounters(ushort startIndex = 1, ushort endIndex = 0)        //запрос значений всех счётчиков / начиная с индекса / в диапозоне [startIndex,endIndex)
        {
            endIndex = endIndex > 0 ? endIndex : Properties.Settings.Default.CountCounters;
            for (ushort i = startIndex; i < endIndex; i++)
            {
                try
                {
                    ecrCtrl.Info.GetCounter(i);
                }
                catch (Exception)
                {
                    LogTB.Text += "Не удолось получить доступ к счётчику №" + i+"\r\n";
                }
            }
            LogTB.Text += "Запрошены данные с счётчиков с " + startIndex + " по " + endIndex + "\r\n";     //логирование
        }
    }
}
