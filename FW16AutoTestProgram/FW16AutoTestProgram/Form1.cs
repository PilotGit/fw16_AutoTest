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
                label_stats_connect.Text = "ККТ: подключено";
                label_version.Text = "Версия прошивки :" + ecrCtrl.Info.FactoryInfo.FwBuild;
                label_firmware.Text = "Код firmware:" + ecrCtrl.Info.FactoryInfo.FwType;
                label_id.Text = "Серийный номер ККТ:" + ecrCtrl.Info.EcrInfo.Id;
                //MessageBox.Show(Convert.ToString( ecrCtrl.Info.));
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

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
