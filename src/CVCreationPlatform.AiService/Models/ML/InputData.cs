namespace CVCreationPlatform.AiService.Models.ML;

public class InputData
{
    public List<string> columns {  get; set; }
    public List<string> index { get; set; }
    public List<List<object>> data { get; set; }
}
