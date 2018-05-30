using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics.Models;

namespace MultilingualQnA.Services
{
    public class TextAnalysisQuery
    {
        private readonly string text;

        public TextAnalysisQuery(string text)
        {
            this.text = text;
        }

        public async Task<DetectedLanguage> Execute(ITextAnalyticsAPI client)
        {
            var id = Guid.NewGuid().ToString();
            var input = new Input(id, text);
            var documents = new List<Input> {input};
            var batchInput = new BatchInput(documents);
            var result = await client.DetectLanguageAsync(batchInput);

            // More than one language could be matched at the same max score...
            // this can cause some confusion

            var detectedLanguages = result.Documents.First()
                .DetectedLanguages;

            var detectedLanguage = detectedLanguages.Where(dl => dl.Score.HasValue 
                          && new[] { "en", "fr" }.Contains(dl.Iso6391Name))
                .OrderByDescending(dl => dl.Score)
                .FirstOrDefault();

            return detectedLanguage;
        }
    }
}