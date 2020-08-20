using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;

namespace Frends.Community.JsonMapper.Tests
{
    [TestFixture]
    public class JsonMapperTests
    {
        InputProperties _testInput;
        private const string _testJson =
@"
{
    ""firstName"": ""Veijo"",
    ""lastName"": ""Frends"",
    ""age"": 30,
    ""retired"": false
}
";
        private const string _testMap =
@"
{
    ""FullName"": ""#xconcat(#valueof($.firstName), ,#valueof($.lastName))"",
    ""Age"" : ""#valueof($.age)"",
    ""StillBreething"": ""#valueof($.retired)""
}
";
        [SetUp]
        public void TestSetup()
        {
            _testInput = new InputProperties
            {
                InputJson = _testJson,
                JsonMap = _testMap
            };
        }

        [Test]
        public void TransformShouldAllowJTokenAsInput()
        {
            _testInput.InputJson = JToken.Parse(_testJson);
            var result = JsonMapperTask.Transform(_testInput);
        }

        [Test]
        public void TransformShouldAllowStringAsInput()
        {
            var result = JsonMapperTask.Transform(_testInput);
        }

        [Test]
        public void TransformMapsStringData()
        {
            var result = JsonMapperTask.Transform(_testInput);

            var fullName = result.ToJson()["FullName"].Value<string>();

            Assert.AreEqual("Veijo Frends", fullName);
        }

        [Test]
        public void TransformMapsNumbersCorrectly()
        {
            var result = JsonMapperTask.Transform(_testInput);

            var age = result.ToJson()["Age"];

            Assert.AreEqual(JTokenType.Integer, age.Type);
            Assert.AreEqual(30, age.Value<int>());
        }

        [Test]
        public void TransformationMapsBoolValueCorrectly()
        {
            var result = JsonMapperTask.Transform(_testInput);

            var breething = result.ToJson()["StillBreething"];

            Assert.AreEqual(JTokenType.Boolean, breething.Type);
            Assert.AreEqual(false, breething.Value<bool>());
        }

        [Test]
        [Ignore("JUST.net does not work with JSON which root element is array type")]
        public void TransformWorksWithArrayRootElement()
        {
            _testInput.InputJson = @"[{""key"":""first element""},{""key"":""second element""}]";
            _testInput.JsonMap = @"{""firstElement"":""#valueof($.[0].key)""}";

            var result = JsonMapperTask.Transform(_testInput);
        }


        [Test]
        public void InvalidJsonInputThrowsException()
        {
            _testInput.InputJson = @"{ foo baar";

            Assert.Throws<FormatException>(() => JsonMapperTask.Transform(_testInput));
        }

        [Test]
        public void InvalidJsonMapThrowsException()
        {
            _testInput.JsonMap = @"{""age"":""#valuof($.age)"", ""foo}";

            Assert.Throws<Exception>(() => JsonMapperTask.Transform(_testInput));
        }

    }
}
