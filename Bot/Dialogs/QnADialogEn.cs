using System.Threading.Tasks;
using Microsoft.Bot.Builder.CognitiveServices.QnAMaker;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace MultilingualQnA.Dialogs
{
    [QnAMaker(subscriptionKey: "<QnAMaker Subscription Key>",
              knowledgebaseId: "<QnA Maker KB ID>",
              scoreThreshold: 0.5D,
              top: 3)]
    public class QnADialogEn : QnAMakerDialog
    {
        protected override Task DefaultWaitNextMessageAsync(IDialogContext context, IMessageActivity message, QnAMakerResults result)
        {
            return base.DefaultWaitNextMessageAsync(context, message, result);
        }

        protected override bool IsConfidentAnswer(QnAMakerResults qnaMakerResults)
        {
            return base.IsConfidentAnswer(qnaMakerResults);
        }

        protected override Task QnAFeedbackStepAsync(IDialogContext context, QnAMakerResults qnaMakerResults)
        {
            return base.QnAFeedbackStepAsync(context, qnaMakerResults);
        }

        protected override Task RespondFromQnAMakerResultAsync(IDialogContext context, IMessageActivity message, QnAMakerResults result)
        {
            return base.RespondFromQnAMakerResultAsync(context, message, result);
        }
    }
}