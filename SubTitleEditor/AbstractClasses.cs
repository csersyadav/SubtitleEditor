using System;
using System.Collections.Generic;

namespace SubTitleEditor
{
    abstract class absTitle
    {
        public String FileName;
        abstract public void updateFile(int hour, int min, int sec, int value);
        abstract public List<String> searchData(string text);
    }
    class SubTitleFactory
    {
        internal static absTitle getTitleClass(string fileName)
        {
            if (fileName.Substring(fileName.Length - 3).ToLower() == "srt")
            {
                return new SrtClass(fileName);
            }
            else
            {
                return new SubClass(fileName);

            }

        }
    }
}