using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SentimentAnalysis.Aws.Api.Services;

namespace SentimentAnalysis.Aws.Api.Controllers
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
        public IActionResult DetectLenguage()
        {
            var lenguage = _sentimentAnalysisService.DetectDomainLenguage();
            return Ok(lenguage);
        }

        [HttpGet("sentiment")]
        public IActionResult DetectSentiment()
        {
            var lenguage = _sentimentAnalysisService.DetectSentiment();
            return Ok(lenguage);
        }

        [HttpGet("batchsentiment")]
        public IActionResult DetectSentimentBatch()
        {
            var response = _sentimentAnalysisService.DetectSentimentBatch();
            return Ok(response);
        }

        [HttpGet("keyphrases")]
        public IActionResult DetectKeyPhrases()
        {
            var response = _sentimentAnalysisService.DetectKeyPhrases();
            return Ok(response);
        }
    }
}