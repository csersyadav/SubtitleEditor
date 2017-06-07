using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SubTitleEditor
{
    class SubClass : absTitle
    {
      internal  class SubData
        {
            public int FFrame { get; set; }
            public int TFrame { get; set; }
            public String Text { get; set; }
            public SubData(String fFrame, String tFrame, String text)
            {
                this.FFrame = Int32.Parse(fFrame);
                this.TFrame = Int32.Parse(tFrame);
                this.Text = text;
            }
        }
        internal List<SubData> lstData = new List<SubData>();

        public SubClass(String FileName)
        {
            this.FileName = FileName;
            using (StreamReader sr = new StreamReader(FileName))
            {
                string line = string.Empty;
                while ((line = sr.ReadLine()) != null)
                {
                    lstData.Add(Parse(line)); //adding each line from the .txt to the lineList
                }
            }
        }

        private SubData Parse(string line)
        {
            String[] x = line.Split("{}".ToCharArray());
            return new SubData(x[1], x[3], x[4]);
        }

        //public override void MoveForward(int value)
        //{
        //    List<String> ls = new List<string>();
        //    foreach (SubData sc in lstData)
        //    {
        //        ls.Add("{" + (sc.FFrame + value) + "}{" + (sc.TFrame + value) + "}" + sc.Text);
        //    }
        //    File.WriteAllLines(FileName, ls);
        //}

        public override List<string> searchData(String text)
        {
            List<string> strout = new List<string>();
            foreach (SubData sc in lstData)
            {
                if (sc.Text.Contains(text))
                {
                    strout.Add(sc.FFrame.ToString());
                    strout.Add(sc.TFrame.ToString());
                    strout.Add(sc.Text.ToString());
                    return strout;
                }
            }
            return strout;
        }
        public override void updateFile(int hour, int min, int sec, int value)
        {
            List<String> ls = new List<string>();
            foreach (SubData sc in lstData)
            {
                ls.Add("{" + (sc.FFrame + value) + "}{" + (sc.TFrame + value) + "}" + sc.Text);
            }
            File.WriteAllLines(FileName, ls);
        }
    }
}

