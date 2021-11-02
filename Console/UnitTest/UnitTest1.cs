using System;
using WebMVC.Security;
using Xunit;

using Shouldly;

namespace UnitTest
{
    public class UnitTest1
    {
        [Fact]
        public void AesHelperTest()
        {
            string key = new AesHelper().Key;
            Convert.FromBase64String(key).Length.ShouldBe(32);
        }

    }
}