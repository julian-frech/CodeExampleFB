using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using BoersenDatenService2.ApiDataClasses;
using Newtonsoft.Json;

namespace BoersenDatenService1.Interfaces
{
    public interface IApi
    {
        Task<int> PassArgumentsToApiBulk(string[] target);

    }
}
