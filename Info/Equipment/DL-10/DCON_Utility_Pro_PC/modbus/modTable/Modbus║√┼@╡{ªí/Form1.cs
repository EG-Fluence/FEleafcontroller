using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;

namespace ICPDAS
{
    public partial class Form1 : Form
    {
        private static string modPath = "D:\\martin\\TestV\\DCON_Utility_Pro\\modbus\\";
        private static string rfilePath = "D:\\martin\\TestV\\DCON_Utility_Pro\\modbus\\ModbusTable.cs";
        private static string wfilePath = "D:\\martin\\TestV\\DCON_Utility_Pro\\modbus\\w_ModbusTable.cs";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void OnIOConfigEvent(CIO io, int grade, string title, string message)
        {
            if (Utility.objList.Count > 0)
            {
                foreach (IPlugin frm in Utility.objList)
                {
                    CIO fio = frm.GetIO();
                    if (fio.usePort == io.usePort && fio.useBaudrate == io.useBaudrate && fio.module.ID == io.module.ID && fio.useProtocol == io.useProtocol && fio.netAddress == io.netAddress && fio.useChecksum == io.useChecksum && fio.useParity == io.useParity && fio.useDataBit == io.useDataBit && fio.useStopBit == io.useStopBit)
                    {
                        frm.OnConfigEvent(grade, title, message);
                    }

                    //frm.Update();
                    //this.Text = 
                }
            }

        }


        private void OnExceptionMessage(string exceptionSource, string msg)
        {
            if (Utility.objList.Count > 0)
            {
                foreach (IPlugin frm in Utility.objList)
                {

                    //this.Text = 
                }
            }
        }
    }
}
