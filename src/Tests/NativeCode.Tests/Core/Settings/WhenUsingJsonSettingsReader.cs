namespace NativeCode.Tests.Core.Settings
{
    using System;
    using System.Text;

    using NativeCode.Core.Settings;

    using Xunit;

    public class WhenUsingJsonSettingsReader
    {
        private const string JsonSample = "{ \"Simple\": \"string\", \"Array\": [0], \"Object\": { \"Number\": 20 } }";

        [Fact]
        public void ShouldReadJsonFile()
        {
            // Arrange
            var sut = new JsonSettings();
            sut.Load(JsonSample, Encoding.UTF8);
            var value = sut.GetValue<long>("Object.Number");
            Assert.Equal(20, value);
            Assert.Equal("string", sut.GetValue<string>("Simple"));
            Assert.Equal(1, sut.GetValue<object[]>("Array").Length);
            Assert.Null(sut["nothing"]);
            Assert.Null(sut.GetValue<Guid?>("NotExist"));
            Assert.Equal(Guid.Empty, sut.GetValue<Guid>("NotExist"));
            sut.SetValue("Settings.Test", "test");
            Assert.Equal("test", sut.GetValue<string>("Settings.Test"));
        }
    }
}