using System;
using System.Threading.Tasks;
using Microsoft.Azure;
using Microsoft.Bot.Builder.CognitiveServices.QnAMaker;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using MultilingualQnA.Properties;

namespace MultilingualQnA.Dialogs
{

    [Serializable]
    public class QnADialogEn : QnAMakerDialog
    {
        public QnADialogEn() : base(new QnAMakerService(new QnAMakerAttribute(
            CloudConfigurationManager.GetSetting("EN-QnaSubscriptionKey"),
            CloudConfigurationManager.GetSetting("EN-QnaKnowledgebaseId"),
            Resources.NotFound,
            0.3d,
            3,
            CloudConfigurationManager.GetSetting("EN-QnaEndpointHostName"))))
        {
        }
        protected override Task DefaultWaitNextMessageAsync(IDialogContext context, IMessageActivity message,
            QnAMakerResults result)
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

        protected override Task RespondFromQnAMakerResultAsync(IDialogContext context, IMessageActivity message,
            QnAMakerResults result)
        {
            return base.RespondFromQnAMakerResultAsync(context, message, result);
        }
    }
}