using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SwaggerUI_CoreExample.Models;
using Swashbuckle.AspNetCore.Annotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SwaggerUI_CoreExample.Controllers
{
    [Route("api/[controller]")]
    public class SwaggerUIExampleController : Controller
    {
        
        /// <summary>
        /// Get Operation
        /// </summary>
        /// <remarks>
        /// Sample value of message
        /// 
        ///     Hi Sukhpinder
        ///     
        /// </remarks>
        /// <param name="message"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/get/{message}")]
        [SwaggerOperation("GetOperation")]
        [SwaggerResponse(200,Type =typeof(string),Description ="Get operation")]
        [SwaggerResponse(500, Type = typeof(string), Description = "Internal Server Error")]
        [SwaggerResponse(404, Type = typeof(string), Description = "Method Not Found")]
        public IActionResult GetOperation([FromRoute] string message)
        {
            return  Ok("Hello from get method. Message : "+message);
        }


        /// <summary>
        /// Post Operation
        /// </summary>
        /// <remarks>
        /// Sample value of message
        /// 
        ///     POST /Todo
        ///     {
        ///        "variable1": "Hi",
        ///        "variable2": "Sukhpinder"
        ///     }
        ///     
        /// </remarks>
        /// <param name="message"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/post")]
        [SwaggerOperation("PostOperation")]
        [SwaggerResponse(200, Type = typeof(string), Description = "Post operation")]
        [SwaggerResponse(500, Type = typeof(string), Description = "Internal Server Error")]
        [SwaggerResponse(404, Type = typeof(string), Description = "Method Not Found")]
        public IActionResult PostOperation([FromBody] PostModel message)
        {
            return Ok("Hello from post method. Message : " + message.variable1);
        }

        /// <summary>
        /// Put Operation
        /// </summary>
        /// <remarks>
        /// Sample value of message
        /// 
        ///     Hi Sukhpinder
        ///     
        /// </remarks>
        /// <param name="message"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("/put/{message}")]
        [SwaggerOperation("PutOperation")]
        [SwaggerResponse(200, Type = typeof(string), Description = "Put operation")]
        [SwaggerResponse(500, Type = typeof(string), Description = "Internal Server Error")]
        [SwaggerResponse(404, Type = typeof(string), Description = "Method Not Found")]
        public IActionResult PutOperation([FromRoute] string message)
        {
            return Ok("Hello from put method. Message : " + message);
        }

        /// <summary>
        /// Delete Operation
        /// </summary>
        /// <remarks>
        /// Sample value of message
        /// 
        ///     Hi Sukhpinder
        ///     
        /// </remarks>
        /// <param name="message"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("/delete/{message}")]
        [SwaggerOperation("DeleteOperation")]
        [SwaggerResponse(200, Type = typeof(string), Description = "Delete operation")]
        [SwaggerResponse(500, Type = typeof(string), Description = "Internal Server Error")]
        [SwaggerResponse(404, Type = typeof(string), Description = "Method Not Found")]
        public IActionResult DeleteOperation([FromRoute] string message)
        {
            return Ok("Hello from delete method. Message : " + message);
        }

    }
}
