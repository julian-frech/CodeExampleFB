using System;
using BoersenDatenService1.Interfaces;

namespace BoersenDatenService2.ApiProperties
{
    public class Propertyfinancialmodelingprep: IProperties
    {
        public string[] PassArguments(string[] PassArguments)
        {
            string[] array = { PassArguments[0], PassArguments[1], PassArguments[2], PassArguments[3], PassArguments[4] };
            return array;
        }

        public Propertyfinancialmodelingprep()
        {
        }
    }
}
