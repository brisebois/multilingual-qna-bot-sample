using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics.Models;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using MultilingualQnA.Properties;
using MultilingualQnA.Services;

namespace MultilingualQnA.Dialogs
{
    [Serializable]
    public class QnADialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);

            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            if (await result is Activity activity)
            {
                var language = await DetectedMessageLanguage(activity);

                if (language.Iso6391Name == "en")
                {
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
                    await context.Forward(new QnADialogEn(), Resume, activity);
                }
                else if (language.Iso6391Name == "fr")
                {
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("fr");
                    await context.Forward(new QnADialogFr(), Resume, activity);
                }
                else
                {
                    await context.PostAsync(string.Format(Resources.DontUnderstandThatLanguage));
                }

                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
            }
            else
            {
                context.Wait(MessageReceivedAsync);
            }
        }

        private static async Task<DetectedLanguage> DetectedMessageLanguage(Activity activity)
        {
            var client = new TextAnalyticsAPI
            {
                AzureRegion = AzureRegions.Eastus2,
                SubscriptionKey = "e6dcf50d8ea74655b38e4ab369750681"
            };

            var query = new TextAnalysisQuery(activity.Text);
            var language = await query.Execute(client);
            return language;
        }

        private Task Resume(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            context.Done(new object());

            return Task.CompletedTask;
        }
    }
}