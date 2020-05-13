
using System;
using System.IO;
using System.Text;
using Amazon.Lambda.Core;
using Amazon.Lambda.S3Events;
using Amazon.Lambda.TestUtilities;
using FaceRekognition;
using FaceRekognition.Core;
using Xunit;

namespace FakeRekognition.Tests
{
    public class FunctionTest
    {
        private MemoryStream LoadJsonTestFile(string filename)
        {
            var json = File.ReadAllText(filename);
            return new MemoryStream(Encoding.UTF8.GetBytes(json));
        }

        [Theory]
        [InlineData(typeof(Amazon.Lambda.Serialization.SystemTextJson.LambdaJsonSerializer))]
        [InlineData(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]
        public void S3EventTest(Type serializerType)
        {
            var serializer = Activator.CreateInstance(serializerType) as ILambdaSerializer;
            using (var fileStream = LoadJsonTestFile("s3-event.json"))
            {
                var s3Event = serializer.Deserialize<S3Event>(fileStream);

                var result = Function.FunctionHandler(s3Event, new TestLambdaContext());

                Assert.Equal(Messages.FUNCTION_SUCCESS, result);
            }
        }
    }
}
