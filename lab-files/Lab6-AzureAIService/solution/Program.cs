using System;
using Azure;
using Azure.AI.TextAnalytics;

string[] customerFeedback = {
    "I like it",
    "It doesn't work for my company, but it seems like it would benefit others",
    "I didn't find it very useful",
    "My company will be using this immediately. It's amazing!",
    "I can't unsee this. Sad.",
    "I wish I had discovered this sooner.",
};

// Create a Text Analytics client
var client = new TextAnalyticsClient(new Uri(""), new AzureKeyCredential(""));

int positive = 0, neutral = 0, negative = 0, mixed = 0;

foreach (string comment in customerFeedback)
{
    // Analyze the sentiment of the comment
    DocumentSentiment sentiment = client.AnalyzeSentiment(comment);

    // Output the comment and its sentiment
    Console.WriteLine($"Comment: {comment}");
    Console.WriteLine($"Sentiment: {sentiment.Sentiment}");

    // Increment the appropriate sentiment counter
    switch (sentiment.Sentiment)
    {
        case TextSentiment.Positive:
            positive++;
            break;
        case TextSentiment.Neutral:
            neutral++;
            break;
        case TextSentiment.Negative:
            negative++;
            break;
        case TextSentiment.Mixed:
            mixed++;
            break;
    }
}

// Output the sentiment summary
Console.WriteLine($"Positive comments: {positive}");
Console.WriteLine($"Neutral comments: {neutral}");
Console.WriteLine($"Negative comments: {negative}");
Console.WriteLine($"Mixed comments: {mixed}");