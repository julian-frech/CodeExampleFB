using System;
using System.Collections.Generic;
using BoersenDatenService1.Interfaces;

namespace BoersenDatenService1.ApiProperties
{
    public class PropertyApiAlphAdv2: IProperties
    {
        public string Identifier { get; set; }
        public string TimeInterval { get; set; }
        public string DownloadSize { get; set; }
        public string TimeType { get; set; }
        public string[] PassingParameters = { "", "", "", "", "" };


        public string[] PassArguments(string[] PassArguments)
        {
            string[] array = { PassArguments[0], PassArguments[1], PassArguments[2], PassArguments[3], PassArguments[4] };
            return array;
        }

        public PropertyApiAlphAdv2()
        {
        }
    }
}
