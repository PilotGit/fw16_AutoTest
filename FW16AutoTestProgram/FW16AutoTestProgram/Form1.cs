using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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
            //byte[] b = ecrCtrl.Commands.BeginOfdRead();
            //MessageBox.Show(b.ToString());
            //b = ecrCtrl.Commands.NextOfdRead();
            //MessageBox.Show(b.ToString());
            //Microsoft.SPOT.Hardware.ComputeCRC("df");
            //byte[] b = BitConverter.GetBytes((UInt16)22);
            //UInt16 s = BitConverter.ToUInt16(b, 0);
            
            //BinaryReader streamReader = new BinaryReader(File.OpenRead("file.fnc"));
            //    StreamWriter w = new StreamWriter(File.Create("f.bin"));
            //int i = streamReader.Read(b,0,1);
            //int k = 0;
            //while (i != 0)
            //{
            //    w.Write($"{BitConverter.ToUInt16(b, 0):X2} {(k%10==9?"\n":"")}");
            //    k++;
            //    i = streamReader.Read(b, 0, 1);
            //}
            //streamReader.Close();
            //w.Close();
            STLV sad = new TLV(3,3);
            Type type = sad.GetType();
            if(sad is TLV)
            {
                
                MessageBox.Show("rt");
            }
        }
        
    }

    class STLV
    {
        protected UInt16 tag;
        protected UInt16 lenght;

        public STLV(ushort tag, ushort lenght)
        {
            this.tag = tag;
            this.lenght = lenght;
        }
    }

    class TLV:STLV
    {
        public byte[] value;

        public TLV(ushort tag, ushort lenght) :base(tag, lenght)
        {

        }
    }

    class TLS : STLV
    {
        List<STLV> value = new List<STLV>();

        public TLS(ushort tag, ushort lenght) : base(tag, lenght)
        {
            
        }

        public STLV GetParent()
        {
            return this;
        }

        public int SetValue(ushort tag, ushort lenght)
        {
            value.Add(new TLV(tag, lenght));
            return 0;
        }
    }
}
