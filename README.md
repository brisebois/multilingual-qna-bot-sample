# Multilingual Q&A Bot Sample

![sample diagram](./qna-sample-diagram.png)

This sample was built to demonstrate an approach to detecting the language of a question of forwarding it to the correct QnA Knowldgebase.

Each message is received and sent to the Text Analytics Congnitive Service. From the response, we select the language with the highest score and use it to forward the users question the the correct QnA Knowledgebase.


To deploy this sample, from the [Azure Portal](http://portal.azure.com) create 
- [Web App](https://docs.microsoft.com/en-us/azure/app-service/) This will be the deployment target for this sample (multilingual qna bot)
- [Application Insights](https://docs.microsoft.com/en-us/azure/application-insights/) and place the instrumentation key in **Application Settings** for the Web App (multilingual qna bot)
- [Text Analytics API](https://azure.microsoft.com/en-us/services/cognitive-services/text-analytics/). This Cognitive Service is used for Language detection. Add the Region and primary key from the **Keys** blade and update the **SubscriptionKey** in **Application Settings** for the Web App (multilingual qna bot)
```
APPINSIGHTS_INSTRUMENTATIONKEY = GUID
TextAnalyticsApiRegion = Eastus2
TextAnalyticsApiKey = KEY
```

- [QnA Maker Service](https://docs.microsoft.com/en-us/azure/cognitive-services/QnAMaker/Overview/overview)
	- Make 2 instances, this will create a Web App, Azure Search and Application Insights for each
		- French
		- English

Then, create, train and **publish** two KBs at [qnamaker.ai](https://qnamaker.ai/). 
From this service, you need to collect the subscription key and individual KB IDs and Endpoint Host Names. 
These values need to be set in the **Application Settings** for the Web App (multilingual qna bot)

```
EN-QnaEndpointHostName = URL
EN-QnaKnowledgebaseId = GUID
EN-QnaSubscriptionKey = GUID

FR-QnaEndpointHostName = URL
FR-QnaKnowledgebaseId = GUID
FR-QnaSubscriptionKey = GUID
```

Active Learning is enabled by default. This feature uses user input to learn question variations and to simplify thier addition knowledge base.
To activate this feature set the **top** parameter of the QnAMakerDialog to a number that is greater than 1.

> The active learning process kicks in after every 50 feedbacks sent to the service via the Train API. 

The QnAMakerDialog now does the following:
- Get the TopN matches from the QnA service for every query above the threshold set.
- If the top result confidence score is significantly more than the rest of the results, show only the top answer.
- If the TopN results have similar confidence scores, then show the prompt dialog with TopN questions.
- Once the user selects the right question that matches intent, show the answer for that corresponding question.
- This selection also triggers a feedback into the QnAMaker service via the Train API, described below. 

> Remember that the learnt QnAs and the alterations need to be published explicitly by the developer, to impact the production endpoint. 

Finally, from the [Azure Portal](http://portal.azure.com), create a [Bot Channels Registration](https://docs.microsoft.com/en-us/bot-framework/bot-service-quickstart-registration). From this step, collect the Bot ID, Microsoft App ID and Microsoft App Password.
These values need to be set in the **Application Settings** for the Web App (multilingual qna bot)

```
BotId = BOT-ID
MicrosoftAppId = GUID
MicrosoftAppPassword = SECRET
```

Now publish the sample to the previously created Web Appand you should be albe to test your bot from the **Test in Web Chat** blade located in the **Bot Channels Registration** resource.

## More
- [Microsoft Bot Builder](https://github.com/Microsoft/BotBuilder) GitHub Repository
  - [Bot Builder Samples](https://github.com/Microsoft/BotBuilder/tree/master/CSharp/Samples)
- [Samples for Common Bot Framework Scenarios](https://github.com/Microsoft/AzureBotServices-scenarios)
- Community example of that can be built : [QnAMakerDialog](https://github.com/garypretty/botframework/tree/master/QnAMakerDialog)
