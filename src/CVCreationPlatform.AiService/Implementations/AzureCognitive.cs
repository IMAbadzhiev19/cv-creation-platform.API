using Azure;
using Azure.AI.TextAnalytics;
using CVCreationPlatform.AiService.Contracts;
using Data.Data;
using Microsoft.Extensions.Configuration;

namespace CVCreationPlatform.AiService.Implementations;

public class AzureCognitive : IAzureCognitive
{
    private readonly IConfiguration _configuration;
    private readonly ApplicationDbContext _context;
    private readonly TextAnalyticsClient _textAnalyticsClient;

    public AzureCognitive(IConfiguration configuration, ApplicationDbContext context)
    {
        this._configuration = configuration;
        this._context = context;

        var endpoint = this._configuration["Azure:CognitiveTextAnalytics:Endpoint"];
        var apiKey = configuration["Azure:CognitiveTextAnalytics:APIKey"];
        var credentials = new AzureKeyCredential(apiKey);

        this._textAnalyticsClient = new TextAnalyticsClient(new System.Uri(endpoint), credentials);
    }

    public Task<List<string>> GenerateSkillsForUser(string text)
    {
        throw new NotImplementedException();
    }
}