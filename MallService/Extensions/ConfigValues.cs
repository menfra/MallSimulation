using DataAcess.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MallService.Extensions
{
    public static class ConfigValues
    {
        public static string MallOpenCloseDuration { get; set; }
        public static States MallOpenedStatus { get; set; }
        public static bool FirstTime { get; set; } = true;
        public static Capacity InMallCustomers { get; set; }
    }
}
