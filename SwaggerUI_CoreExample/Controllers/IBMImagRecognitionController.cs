using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using IBM.Cloud.SDK.Core.Authentication.Iam;
using IBM.Watson.VisualRecognition.v3;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SwaggerUI_CoreExample.Models;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SwaggerUI_CoreExample.Controllers
{
    [Route("api/[controller]")]
    public class IBMImagRecognitionController : Controller
    {
        private IConfiguration _configuration;
        private string _apiKey;
        private string _ibmServiceUrl;
        private string _versionDate;
        public IBMImagRecognitionController(IConfiguration configuration)
        {
            _configuration = configuration;
            _apiKey = _configuration["IbmWatson:ApiKey"];
            _ibmServiceUrl = _configuration["IbmWatson:Url"];
            _versionDate = _configuration["IbmWatson:Version"];
        }

        [HttpPost]
        [Route("/recongnize-image")]
        [SwaggerOperation("VisualRecognizeImage")]
        [SwaggerResponse(200, Type = typeof(Class), Description = "Image Score")]
        public IActionResult VisualRecognizeImage(IFormFile file)
        {
            IamAuthenticator authenticator = new IamAuthenticator(apikey: _apiKey);

            VisualRecognitionService visualRecognition = new VisualRecognitionService(_versionDate, authenticator);
            visualRecognition.SetServiceUrl(_ibmServiceUrl);
            byte[] fileBytes;
            var ms = new MemoryStream();

            file.CopyTo(ms);
            fileBytes = ms.ToArray();
            string s = Convert.ToBase64String(fileBytes);

            var result = visualRecognition.Classify(
                imagesFilename: file.FileName,
                imagesFileContentType: file.ContentType,
                imagesFile: ms
                );

            ms.Close();

            var response = JsonConvert.DeserializeObject<FaceRecognitionResponse>(result.Response);
            var classes = response.images.SelectMany(x => x.classifiers).SelectMany(y => y.classes).OrderByDescending(x => x.score);

            return Ok(classes);
        }

        [HttpPost]
        [Route("/recongnize-image-of-foodtype")]
        [SwaggerOperation("VisualRecognizeImageFoodClassifier")]
        [SwaggerResponse(200, Type = typeof(Class), Description = "Image score of food type")]
        public IActionResult VisualRecognizeImageFoodClassifier(IFormFile file)
        {
            IamAuthenticator authenticator = new IamAuthenticator(apikey: _apiKey);

            VisualRecognitionService visualRecognition = new VisualRecognitionService(_versionDate, authenticator);
            visualRecognition.SetServiceUrl(_ibmServiceUrl);
            byte[] fileBytes;
            var ms = new MemoryStream();

            file.CopyTo(ms);
            fileBytes = ms.ToArray();
            string s = Convert.ToBase64String(fileBytes);

            var result = visualRecognition.Classify(
                imagesFilename: file.FileName,
                imagesFileContentType: file.ContentType,
                imagesFile: ms,
                classifierIds: new List<string>()
                    {
                        "food"
                    }
                );

            ms.Close();

            var response = JsonConvert.DeserializeObject<FaceRecognitionResponse>(result.Response);
            var classes = response.images.SelectMany(x => x.classifiers).SelectMany(y => y.classes).OrderByDescending(x => x.score);

            return Ok(classes);
        }
    }
}
