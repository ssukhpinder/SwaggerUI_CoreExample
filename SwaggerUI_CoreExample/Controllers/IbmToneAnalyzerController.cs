using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using IBM.Cloud.SDK.Core.Authentication.Iam;
using IBM.Watson.ToneAnalyzer.v3;
using IBM.Watson.ToneAnalyzer.v3.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Swashbuckle.AspNetCore.Annotations;

namespace SwaggerUI_CoreExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IbmToneAnalyzerController : ControllerBase
    {
        private IConfiguration _configuration;
        private string _apiKey;
        private string _ibmServiceUrl;
        private string _versionDate;
        public IbmToneAnalyzerController(IConfiguration configuration)
        {
            _configuration = configuration;
            _apiKey = _configuration["IbmWatson:ApiKey"];
            _ibmServiceUrl = _configuration["IbmWatson:Url"];
            _versionDate = _configuration["IbmWatson:Version"];
        }

        [HttpPost]
        [Route("/tone-recognize/{text}")]
        [SwaggerOperation("ToneRecognize")]
        [SwaggerResponse(200, Type = typeof(string), Description = "Tone Recognize")]
        public IActionResult ToneRecognize([FromRoute][Required()] string text)
        {
            IamAuthenticator authenticator = new IamAuthenticator(apikey: _apiKey);

            ToneAnalyzerService toneAnalyzer = new ToneAnalyzerService(_versionDate, authenticator);
            toneAnalyzer.SetServiceUrl(_ibmServiceUrl);

            ToneInput toneInput = new ToneInput()
            {
                Text = text
            };

            var result = toneAnalyzer.Tone(
                toneInput: toneInput
            );
            return Ok(result?.Response);
        }
    }
}
