using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text; 

namespace ConsoleApp.Test202203
{
    public class MyAesHelper
    {
        public static void AseTest()
        {
            AesHelperTest helper = new AesHelperTest(true);

            var str = "123";
            //var strBytes = Encoding.Default.GetBytes(str);
            //var aaBytes = helper.Encrypt(strBytes);
            //var bbBytes = helper.Decrypt(aaBytes);
            //var str2 = Encoding.Default.GetString(bbBytes);


            var strEn = helper.Encrypt(str);
            var strEn = helper.Decrypt(strEn);


        }
    }
}
