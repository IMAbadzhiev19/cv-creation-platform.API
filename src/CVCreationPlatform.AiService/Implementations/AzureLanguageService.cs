using Azure;
using Azure.AI.TextAnalytics;
using CVCreationPlatform.AiService.Contracts;
using Data.Data;
using Azure.Search.Documents;
using Azure.Search.Documents.Models;
using Microsoft.Extensions.Configuration;
using System.Text.Json.Nodes;
using Newtonsoft.Json;

namespace CVCreationPlatform.AiService.Implementations;

public class AzureLanguageService : IAzureLanguageService
{
    private readonly IConfiguration _configuration;
    private readonly ApplicationDbContext _context;
    private readonly TextAnalyticsClient _textAnalyticsClient;
    private readonly SearchClient _searchClient;

    public AzureLanguageService(IConfiguration configuration, ApplicationDbContext context)
    {
        this._configuration = configuration;
        this._context = context;

        this._textAnalyticsClient = new TextAnalyticsClient(
                new System.Uri(this._configuration["Azure:TextAnalytics:Endpoint"]),
                new AzureKeyCredential(this._configuration["Azure:TextAnalytics:Key"])
            );

        this._searchClient = new SearchClient(
                new System.Uri($"https://{this._configuration["Azure:CognitiveSearch:ServiceName"]}.search.windows.net"),
                this._configuration["Azure:CognitiveSearch:IndexName"],
                new AzureKeyCredential(this._configuration["Azure:CognitiveSearch:Key"])
            );
    }

    public async Task<string> ExtractKeyPhrasesAsync(string texts)
    {
        var keyPhrases = await _textAnalyticsClient.ExtractKeyPhrasesAsync(texts);
        return string.Join(", ", keyPhrases.Value);
    }

    public async Task<List<string>> SuggestSkillsAsync(string jobPositions)
    {
        SearchResults<JsonObject> response = await this._searchClient.SearchAsync<JsonObject>(jobPositions);
        var rnd = new Random();

        var skills = new List<string>();
        foreach(var res in response.GetResults())
        {
            var skillsArray = JsonConvert.DeserializeObject<List<string>>(res.Document["Skills"]!.ToString());
            skills.AddRange(skillsArray);
        }

        var newSkillsList = new List<string>();
        for(int i = 0; i < skills.Count; i++)
        {
            var pos = rnd.Next(0, skills.Count);
            newSkillsList.Add(skills[pos]);
        }

        return newSkillsList;
    }
}