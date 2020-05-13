using System;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Rekognition;
using Amazon.Rekognition.Model;
using FaceRekognition.Core;

namespace FaceRekognition.Handlers
{
    public class ImageHandler
    {
        private readonly AmazonRekognitionClient _rekognitionClient;

        public ImageHandler(AmazonRekognitionClient rekognitionClient)
        {
            _rekognitionClient = rekognitionClient;
        }
        
        public async Task<bool> ProcessAsync(string bucketName, string fileName)
        {
            // form the request
            var detectRequest = new DetectFacesRequest
            {
                Image = new Image { S3Object = new S3Object { Bucket = bucketName, Name = fileName } }
            };

            // detect any possible faces
            var response = await _rekognitionClient.DetectFacesAsync(detectRequest);

            if (response == null)
            {
                throw new ApplicationException(Messages.RESPONSE_NULL);
            }

            if (response.FaceDetails.Any())
            {
                return true;
            }

            return false;
        }
    }
}