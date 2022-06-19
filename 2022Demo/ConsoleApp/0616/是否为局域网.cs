using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp._0616
{
    public class 是否为局域网
    {
        public bool IsPrivateNetwork(string ipv4Address)
        {
            if (IPAddress.TryParse(ipv4Address, out _))
            {
                if (ipv4Address.StartsWith("192.168.") || ipv4Address.StartsWith("10."))
                {
                    return true;
                }
                if (ipv4Address.StartsWith("172."))
                {
                    string seg2=ipv4Address[4..7];
                    if (seg2.EndsWith(".") && string.Compare(seg2,"16.") >= 0 && string.Compare(seg2, "31.") <= 0)
                    {
                        return true;
                    }
                }

            }
            return false;
        }
    }
}
