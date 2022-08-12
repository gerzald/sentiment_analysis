using Google.Cloud.Language.V1;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SentimentAnalysis.Gcp.Api.Services
{
    public class SentimentAnalysisService
    {
        
        public object DetectSentiment()
        {
            LanguageServiceClient client = LanguageServiceClient.Create();

            string mal_comentario = "Muy mal servicio de parte de la cocina";
            string buen_comentario = "excelente comida";

            AnalyzeSentimentRequest analyzeSentimentRequest = new AnalyzeSentimentRequest()
            {
                Document = new Document()
                {
                    Content = "Que friega con eso. Aquí el único que miente es usted, dejamos de creer en usted hace mucho tiempo demasiadas mentiras ya deje de echarle la culpa a otros asuma su responsabilidad como caballero si le queda algo de dignidad",
                    Type = Document.Types.Type.PlainText
                }
            };

            AnalyzeSentimentResponse response = client.AnalyzeSentiment(analyzeSentimentRequest);
            
            return response;
        }
    }
}
