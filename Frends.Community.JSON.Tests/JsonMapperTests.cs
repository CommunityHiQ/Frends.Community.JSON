using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Threading;

namespace Frends.Community.Json.Tests
{
    [TestFixture]
    public class JsonMapperTests
    {
        JsonMapperInput _testInput;
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
            _testInput = new JsonMapperInput
            {
                InputJson = _testJson,
                JsonMap = _testMap
            };
        }

        [Test]
        public void TransformShouldAllowJTokenAsInput()
        {
            _testInput.InputJson = JToken.Parse(_testJson);
            JsonTasks.JsonMapper(_testInput, new CancellationToken());
        }

        [Test]
        public void TransformShouldAllowStringAsInput()
        {
            JsonTasks.JsonMapper(_testInput, new CancellationToken());
        }

        [Test]
        public void TransformMapsStringData()
        {
            var result = JsonTasks.JsonMapper(_testInput, new CancellationToken());

            var fullName = result.ToJson()["FullName"].Value<string>();

            Assert.AreEqual("Veijo Frends", fullName);
        }

        [Test]
        public void TransformMapsNumbersCorrectly()
        {
            var result = JsonTasks.JsonMapper(_testInput, new CancellationToken());

            var age = result.ToJson()["Age"];

            Assert.AreEqual(JTokenType.Integer, age.Type);
            Assert.AreEqual(30, age.Value<int>());
        }

        [Test]
        public void TransformationMapsBoolValueCorrectly()
        {
            var result = JsonTasks.JsonMapper(_testInput, new CancellationToken());

            var breething = result.ToJson()["StillBreething"];

            Assert.AreEqual(JTokenType.Boolean, breething.Type);
            Assert.AreEqual(false, breething.Value<bool>());
        }

        [Test]
        [Ignore("JUST.net does not work with Json which root element is array type")]
        public void TransformWorksWithArrayRootElement()
        {
            _testInput.InputJson = @"[{""key"":""first element""},{""key"":""second element""}]";
            _testInput.JsonMap = @"{""firstElement"":""#valueof($.[0].key)""}";

            JsonTasks.JsonMapper(_testInput, new CancellationToken());
        }


        [Test]
        public void InvalidJsonInputThrowsException()
        {
            _testInput.InputJson = @"{ foo baar";

            Assert.Throws<FormatException>(() => JsonTasks.JsonMapper(_testInput, new CancellationToken()));
        }

        [Test]
        public void InvalidJsonMapThrowsException()
        {
            _testInput.JsonMap = @"{""age"":""#valuof($.age)"", ""foo}";

            Assert.Throws<Exception>(() => JsonTasks.JsonMapper(_testInput, new CancellationToken()));
        }

    }
}
