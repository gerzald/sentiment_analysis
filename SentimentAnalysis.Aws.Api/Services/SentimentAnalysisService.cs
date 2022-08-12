using Amazon.Comprehend;
using Amazon.Comprehend.Model;
using Microsoft.AspNetCore.ResponseCompression;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace SentimentAnalysis.Aws.Api.Services
{
    public class SentimentAnalysisService
    {
        private readonly string EN_GOOD = "Very good, you are the best my friend";
        private readonly string EN_BAD = "this is the saddest thing that i saw ever";
        private readonly string ES_GOOD = "buena atencion, recomendados";
        private readonly string ES_BAD = "atencion muy lenta";

        private readonly AmazonComprehendClient _comprehendClient;

        public SentimentAnalysisService()
        {
            string accessKey = "AKIA43PAFJRH3ACISVFL";
            string secretKey = "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";
            _comprehendClient = new AmazonComprehendClient(accessKey, secretKey, Amazon.RegionEndpoint.USEast1);
        }

        public object DetectDomainLenguage()
        {
            string cadena = ES_GOOD;

            DetectDominantLanguageRequest detectDomainLenguage = new DetectDominantLanguageRequest()
            {
                Text = cadena
            };

            DetectDominantLanguageResponse detectDominantLanguageResponse = _comprehendClient.DetectDominantLanguageAsync(detectDomainLenguage).Result;

            return detectDominantLanguageResponse.Languages[0];
        }

        public object DetectSentiment()
        {
            string cadena = EN_BAD;



            DetectSentimentRequest detectSentimentRequest = new DetectSentimentRequest()
            {
                Text = cadena,
                LanguageCode = "en"
            };

            DetectSentimentResponse detectSentimentResponse = _comprehendClient.DetectSentimentAsync(detectSentimentRequest).Result;

            return detectSentimentResponse;
        }


        public object DetectSentimentBatch()
        {
            List<string> textList = new List<string>()
            {
                "El Martes 30 de marzo EEUU divulga la lista Engel, de funcionarios corruptos, narcos y ligados al crimen organizado, que va a pasar si en esa lista mencionan a Yanni, a Papi, a Mel y muchos más... se van a rasgar las vestiduras los fariseos o al fin tendremos justicia?",
                "Vergüenza es ser cómplice de asesinatos al fomentar el lavado de activos , confesar y cumplir 3 años de cárcel con novio cubano en USA, sin pagar el crimen en donde lo cometió y ser cómplice del robo de elecciones, eso si debería dar pena......su judío mafioso ese es NARCO",
                "Es el guason Juan Ramón Martínez, si que son chistosos ustedes!",
                "Fuck, solo analistas 'imparciales' entrevistaron",
                "Solo peste apoyando delincuentes hablando de Luis, malditas bestias descaradas"
            };

            BatchDetectSentimentRequest batchDetectSentimentRequest = new BatchDetectSentimentRequest()
            {
                TextList = textList,
                LanguageCode = "es",
            };

            BatchDetectSentimentResponse batchDetectSentimentResponse = _comprehendClient.BatchDetectSentimentAsync(batchDetectSentimentRequest).Result;

            var response = new List<object>();
            foreach (var item in batchDetectSentimentResponse.ResultList)
            {                
                response.Add(new { 
                    item.Sentiment,
                    Text=textList[item.Index]
                });
            }

            return response;
        }

        public List<string> DetectKeyPhrases()
        {
            string cadena = "Alimento a base de leche, bajo en grasa, contiene 24 vitaminas, minerales y nutrientes esenciales para apoyar las necesidades nutricionales de las mujeres embarazadas y en periodo de lactancia.";

            DetectKeyPhrasesRequest detectKeyPhrasesRequest = new DetectKeyPhrasesRequest()
            {
                LanguageCode = "es",
                Text = cadena
            };


            DetectKeyPhrasesResponse detectKeyPhrasesResponse = _comprehendClient.DetectKeyPhrasesAsync(detectKeyPhrasesRequest).Result;

            List<string> keyphrases = new List<string>();
            
            foreach (var item in detectKeyPhrasesResponse.KeyPhrases)
            {
                keyphrases.Add(item.Text);
            }

            var http  = detectKeyPhrasesResponse.HttpStatusCode;

            return keyphrases;
        }
    }
}
