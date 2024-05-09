using Newtonsoft.Json;
using System.Text;

namespace Choco.Config
{
    public class ConfigJsonReader
    {
        private readonly string ConfigFilePath = "./Config/config.json";
        //Declare our Token & Prefix properties of this class
        public string? discordToken { get; set; }
        public string? discordPrefix { get; set; }

        public async Task ReadJSON() //This method must be run asynchronously
        {
            //Initialise a StreamReader
            using StreamReader sr = new(ConfigFilePath, new UTF8Encoding(false));

            //Read the JSON file
            string json = await sr.ReadToEndAsync();

            //Deserialize into a JSONStruct object
            JSONStruct? obj = JsonConvert.DeserializeObject<JSONStruct>(json);

            if (obj == null)
            {
                Program.Client?.Logger.LogCritical($"[ConfigJsonReader.Critical] - can't deserialize json file {ConfigFilePath}");
                throw new Exception($"Can't read file {ConfigFilePath}");
            }

            //Set the properties
            this.discordToken = obj.token;
            this.discordPrefix = obj.prefix;
        }
    }

    internal sealed class JSONStruct
    {
        public string? token { get; set; }
        public string? prefix { get; set; }
    }
}
