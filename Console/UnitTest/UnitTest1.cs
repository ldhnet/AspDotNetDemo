using Framework.Utility;
using Framework.Utility.Extensions;
using Framework.Utility.Helper;
using Framework.Utility.Security;
using Shouldly;
using System;
using Xunit;

namespace UnitTest
{
    public class UnitTest1
    {
        [Fact]
        public void AesHelperTest()
        {
            string key = new AesHelper().Key;
            Convert.FromBase64String(key).Length.ShouldBe(32);

            AesHelper aes = new AesHelper();
            string source = "admin";

       

            source.CheckNotNull("source");

            var aaa=  aes.Encrypt(source);

            var bbb = aes.Decrypt(source);

             
            //Ω‚√‹Ticket
            var ccc = SecurityHelper.Base64Decrypt(bbb);
        }
        [Fact]
        public void Encrypt_Decrypt_Test()

        {
            AesHelper aes = new AesHelper();
            string source = "admin";

            //byte[]
            byte[] sourceBytes = source.ToBytes();
            byte[] enBytes = aes.Encrypt(sourceBytes);
            aes.Decrypt(enBytes).ShouldBe(sourceBytes);

            //string
            string enstr = aes.Encrypt(source);
            aes.Decrypt(enstr).ShouldBe(source);

            aes = new AesHelper(true);

            //byte[]
            enBytes = aes.Encrypt(sourceBytes);
            aes.Decrypt(enBytes).ShouldBe(sourceBytes);

            //string
            enstr = aes.Encrypt(source);
            aes.Decrypt(enstr).ShouldBe(source);
        }

        [Fact]
        public void Encrypt_Decrypt_File_Test()
        {
            string file = @"OSharp.Tests.dll", enFile = "OSharp.Tests_en.dll", deFile = "OSharp.Tests_de.dll";
            AesHelper aes = new AesHelper();
            aes.EncryptFile(file, enFile);
            aes.DecryptFile(enFile, deFile);

            file.ShouldFileMd5Be(deFile);
            enFile.ShouldFileMd5NotBe(deFile);
        }
    }
}