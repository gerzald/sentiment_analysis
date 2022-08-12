using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SentimentAnalysis.Gcp.Api.Services;

namespace SentimentAnalysis.Gcp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TextAnalysisController : ControllerBase
    {
        private readonly SentimentAnalysisService _sentimentAnalysisService;

        public TextAnalysisController(SentimentAnalysisService sentimentAnalysisService)
        {
            _sentimentAnalysisService = sentimentAnalysisService;
        }


        [HttpGet]
        public IActionResult DetectSentiment()
        {
            var response = _sentimentAnalysisService.DetectSentiment();
            return Ok(response);
        }
    }
}