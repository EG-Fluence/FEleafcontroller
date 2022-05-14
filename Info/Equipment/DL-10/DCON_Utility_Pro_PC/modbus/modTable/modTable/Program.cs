using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;

namespace modTable
{

    public class modCmdGroup : IComparable<modCmdGroup>

    {
        public List<string> id = new List<string>();
        public string cmdName;
        public Int16 funcode;
        public UInt16 refAddr;

      

        public int CompareTo(modCmdGroup comparePart)
        {
            // A null value means that this object is greater.
            if (comparePart == null)
                return 1;

            else
                return this.refAddr.CompareTo(comparePart.refAddr);
        }

    }

    class Program
    {
        private static string modPath = "D:\\martin\\TestV\\DCON_Utility_Pro\\modbus\\";
        private static string rfilePath ="D:\\martin\\TestV\\DCON_Utility_Pro\\modbus\\ModbusTable.cs";
        private static string wfilePath = "D:\\martin\\TestV\\DCON_Utility_Pro\\modbus\\w_ModbusTable.cs";
         
        
        static void Main(string[] args)
        {
           // Modify();

           // Append("CL204WF");
            //Load("7033");

            Console.Read();
        }


        // 將原本 code 裡面的 rawCmd 去除
        public static void Modify()
        {
            StreamReader rt = new StreamReader(rfilePath, System.Text.Encoding.Default);
            StreamWriter wt = new StreamWriter(wfilePath, false, System.Text.Encoding.Default);
            List<string> codeList = new List<string>();


            //while (!rt.EndOfStream)
            //{
            //    string line = rt.ReadLine().TrimEnd().TrimStart();
            //    if (line != string.Empty)
            //        codeList.Add(line);
            //}
            //rt.Close();

          
            rt = new StreamReader(rfilePath, System.Text.Encoding.Default);
            bool beginFlag = false;
            while (!rt.EndOfStream)
            {
                string line = rt.ReadLine();
                if (beginFlag == true)
                {

                    if (line.IndexOf("refAddr") >= 0)
                    {
                        if (line.IndexOf("//") >= 0)
                        {
                            line = line.Substring(0, line.IndexOf("//"));
                        }
                        line = line.Replace(";", "");
                        string[] ss = line.Split('=');
                        if (ss[1].IndexOf("0x") >= 0)
                            ss[1] = "65535";
                        else
                            ss[1] = (Convert.ToInt32(ss[1]) - 1).ToString();
                        line = ss[0] + " = " + ss[1] + ";";
                        codeList.Add(line);
                    }
                    else
                    {
                        codeList.Add(line);
                    }
                }
                else
                {
                    codeList.Add(line);
                }

                if (line.IndexOf("#region") >= 0)
                {
                    beginFlag = true;
                    // codeList.Add(line);
                }
                if (line.IndexOf("#endregion") >= 0)
                {
                    beginFlag = false;
                    //codeList.Add(line);
                }

            }
            rt.Close();
                

                //if (beginFlag == true)
                //{
                //    //if (line.IndexOf("functionCode") >= 0) // 看到 functionCode
                //    //{
                //    //    codeList.Add(line);
                //    //    if (line.IndexOf("0x46") >= 0)
                //    //    {
                //    //        line = rt.ReadLine();  // 處理 refAddr
                //    //        string[] ss = line.Split('=');
                //    //        line = ss[0] + " = " + "0" + ";";
                //    //        codeList.Add(line);
                //    //    }
                //    //    else  // 標準 modbus function code
                //    //    {
                //    //        line = rt.ReadLine();
                //    //        if(line.IndexOf ("
                //    //        if (line.IndexOf("//") >= 0)
                //    //            line = line.Substring(0, line.IndexOf("//"));
                //    //        line = line.Replace(";", "");
                //    //        string[] ss = line.Split('=');
                //    //        line = ss[0] + " = " + (Convert.ToInt32(ss[1]) - 1).ToString();
                //    //        codeList.Add(line);
                //    //    }
                //    //}
                //    //else
                //    //{
                //    //    codeList.Add(line);
                //    //}
                //}
                //else
                //{
                //    codeList.Add(line);
                //}

                //if (line.IndexOf("#region") >= 0)
                //{
                //    beginFlag = true;
                //    // codeList.Add(line);
                //}
                //if (line.IndexOf("#endregion") >= 0)
                //{
                //    beginFlag = false;
                //    //codeList.Add(line);
                //}
           // }

           // rt.Close();
            foreach (string s in codeList)
            {
                wt.WriteLine(s);
            }
            wt.Close();
            //if (codeList.Count > 0)
            //{
            //    foreach (string s in codeList)
            //    {
            //        if(s.IndexOf ("functionCode")>=0)
            //        {
            //            string [] ss = s.Split('=');
            //            if(ss[1].IndexOf("0x46")>=0)
            //            }
            //        wt.WriteLine(s);
            //    }
            //}
           // rt.Close();
            
        }

        public static void Append(string id)
        {
            StreamReader rt = new StreamReader(modPath + id +"_ModbusCode.txt", System.Text.Encoding.Default);
           
            string modTablePath = "D:\\martin\\DCON_Utility_V3\\Lib_Utility\\Protocol\\ModTable.cs";
            StreamReader mt = new StreamReader(rfilePath, System.Text.Encoding.Default);
            List<string> codeList = new List<string>();
            List<string> endCodeList = new List<string>();
          //  codeList.Add("#region " + id);
            while (!rt.EndOfStream)
            {
                string line = rt.ReadLine();
                
                    codeList.Add(line);
            }
           // codeList.Add("#endregion " + id);
            rt.Close();
            bool startFlag = false;
            while (!mt.EndOfStream)
            {
                string line = mt.ReadLine();
                endCodeList.Add(line);
                if (startFlag == false)
                {
                    if (line.IndexOf("switch (io.module.ID)") >= 0)
                    {
                        line = mt.ReadLine(); // 讀 {
                        endCodeList.Add(line);
                        foreach (string s in codeList)
                        {
                            endCodeList.Add(s);
                        }
                        startFlag = true;
                    }
                }
                
            }
          
            mt.Close();
            
            if (endCodeList.Count > 0)
            {
                StreamWriter wt = new StreamWriter(rfilePath, false, System.Text.Encoding.Default);
                foreach (string s in endCodeList)
                {
                    wt.WriteLine(s);
                }
                wt.Close();
            }
            
           
        }


        public static void Load(string id)
        {
           // StreamReader rt = new StreamReader(modPath + id + "_ModbusCode.txt", System.Text.Encoding.Default);
            string modTablePath = "D:\\martin\\DCON_Utility_V3\\Lib_Utility\\Protocol\\ModTable.cs";
            StreamReader rt = new StreamReader(modTablePath, System.Text.Encoding.Default);

            List<string> codeList = new List<string>();
            List<string> endCodeList = new List<string>();
            bool beginFlag = false;
            while (!rt.EndOfStream)
            {
                string line = rt.ReadLine();
                if (beginFlag == true)
                {

                    Console.WriteLine(line);
                    codeList.Add(line);
                    
                }
              

                if (line.IndexOf("#region") >= 0 && line.IndexOf(id)>=0)
                {
                    beginFlag = true;
                    // codeList.Add(line);
                }
                if (beginFlag==true && line.IndexOf("#endregion") >= 0)
                {
                    beginFlag = false;
                    break;
                    //codeList.Add(line);
                }

            }
            rt.Close();
        }
    }
}
