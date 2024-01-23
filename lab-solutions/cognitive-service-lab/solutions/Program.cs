using System;
using System.Collections.Generic;
using System.IO;
using Azure;
using Azure.AI.TextAnalytics;

class Program
{
    static void Main(string[] args)
    {
        // Create a TextAnalytics client
        string textAnalyticsKey = "";
        string textAnalyticsEndpoint = "";
        var textAnalyticsClient = new TextAnalyticsClient(new Uri(textAnalyticsEndpoint), new AzureKeyCredential(textAnalyticsKey));

        List<string> questions = new List<string>()
        {
            "Rate your job satisfaction on a scale of 1 to 10.",
            "Rate your work-life balance on a scale of 1 to 10.",
            "Rate your relationship with your manager on a scale of 1 to 10.",
            "Rate your satisfaction with your salary on a scale of 1 to 10.",
            "Rate your overall satisfaction with your work environment on a scale of 1 to 10."
        };

        List<string> results = new List<string>();

        foreach (var question in questions)
        {
            Console.WriteLine(question);
            string response = Console.ReadLine();
            int rating;
            while (!int.TryParse(response, out rating) || rating < 1 || rating > 10)
            {
                Console.WriteLine("Invalid input. Please enter a number between 1 and 10.");
                response = Console.ReadLine();
            }

            // Analyze sentiment
            DocumentSentiment sentiment = textAnalyticsClient.AnalyzeSentiment(question);
            results.Add($"Question: {question} - Rating: {rating} - Sentiment: {sentiment.Sentiment}");
        }

        File.WriteAllLines("survey_results.txt", results);

        Console.WriteLine("Thank you for completing the survey!");
    }
}