using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SubTitleEditor
{

    class SrtClass : absTitle
    {
       internal List<SrtData> lstData = new List<SrtData>();
       internal class SrtData
        {
            public int FNumber { get; set; }
            public TimeSpan FFrame { get; set; }
            public TimeSpan TFrame { get; set; }
            public List<String> Text = new List<string>();

            internal void ParseTimeLine(string line)
            {
                String[] t = line.Split(' ');
                this.FFrame = TimeSpan.Parse(t[0].Replace(',','.'));
                this.TFrame = TimeSpan.Parse(t[2].Replace(',', '.'));
            }
        }

        public SrtClass(String FileName)
        {
            this.FileName = FileName;
            using (StreamReader sr = new StreamReader(FileName))
            {
                string line = string.Empty;
                while ((line = sr.ReadLine()) != null)
                {
                    SrtData objData = new SrtData();
                    objData.FNumber = int.Parse(line);
                    line = sr.ReadLine();
                    objData.ParseTimeLine(line);
                    while ((line = sr.ReadLine()) != null && !line.Equals(""))
                    {
                        objData.Text.Add(line);
                    }
                    lstData.Add(objData); //adding each line from the .txt to the lineList
                }
            }
        }

        public override List<string> searchData(String text)
        {
            List<string>strout=new List<string>();
            foreach(SrtData sc in lstData)
            {
                int i = sc.Text.FindIndex(x => x.ToLower().Contains(text.ToLower()));
                if (i>-1)
                {
                    strout.Add(sc.FFrame.ToString());
                    strout.Add(sc.TFrame.ToString());
                    strout.Add(sc.Text[i].ToString());
                    return strout;
                }
            }
            return strout;
        }

        public override void updateFile(int hour, int min, int sec,int value)
        {
            TimeSpan tsvalue = new TimeSpan(0,hour,min,sec, value);
            List<String> ls = new List<string>();
            foreach (SrtData sc in lstData)
            {
                sc.FFrame=sc.FFrame.Add(tsvalue);
                sc.TFrame = sc.TFrame.Add(tsvalue);
                ls.Add(sc.FNumber.ToString());
                ls.Add(sc.FFrame.ToString().TrimEnd('0').Replace('.', ',') + " --> " + sc.TFrame.ToString().TrimEnd('0').Replace('.', ','));
                foreach (string s in sc.Text)
                    ls.Add(s);
                ls.Add("");
            }
            File.WriteAllLines(FileName, ls);        
        }
    }
}
