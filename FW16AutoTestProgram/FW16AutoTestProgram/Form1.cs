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
                System.Diagnostics.Debug.Write(excep.Message);
            }
            catch(System.IO.IOException excep)
            {
                MessageBox.Show(excep.Message);
            }
            CreateCheck();
        }
        private void CreateCheck()
        {
            //ecrCtrl.Shift.Open();
            var receipt = ecrCtrl.Shift.BeginReceipt("Касир", Fw16.Model.ReceiptKind.Income);
            receipt.AddEntry(receipt.NewItemPriced("1", "Товар 1", Native.CmdExecutor.VatCodeType.Vat18, 10.50m, 0.5m));
            receipt.AddPayment(Native.CmdExecutor.TenderCode.Cash, 10.25m);
            receipt.Complete();

        }
    }
}
