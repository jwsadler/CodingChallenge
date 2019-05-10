using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace CodingChallenge.API.BusinessLogic.Models.Oxford
{
    public class OxfordResponseModel
    {
        [JsonProperty(PropertyName = "id")] public string Id { get; set; }

        [JsonProperty(PropertyName = "metadata")]
        public OxfordMetaData MetaData { get; set; }

        [JsonProperty(PropertyName = "results")]
        public List<OxfordResult> Results { get; set; }

        public string Definitions { get
        {
            var strResult = string.Empty;
            foreach (var result in Results)
            foreach (var lexicalEntry in result.LexicalEntries)
            foreach (var entry in lexicalEntry.Entries)
            foreach (var sense in entry.Senses)
            foreach (var definition in sense.Definitions)
                strResult += $"{definition}\r\n";
            return strResult;
        } }

        public string FormattedDefinitions
        {
            get
            {
                var strResult = "<h2>Definitions</h2><ol>";
                foreach (var result in Results)
                foreach (var lexicalEntry in result.LexicalEntries)
                foreach (var entry in lexicalEntry.Entries)
                foreach (var sense in entry.Senses)
                foreach (var definition in sense.Definitions)
                    strResult += "<li>"+$"{definition}"+"</li>";
                return strResult+"</ol>";
            }
        }


        public List<string> TotalWords =>
            Definitions.Split(
                new[] {' ', ',', ';', '.', '!', '"', '(', ')', '?', '\r', '\n'},
                StringSplitOptions.RemoveEmptyEntries).ToList();

        public int TotalWordCount =>
            TotalWords.Count;

        public List<WordCount> WordCounts => TotalWords.GroupBy(p => p).Select(x => new WordCount {Count = x.Count(), Word = x.Key}).OrderBy(p => p.Count).ToList();
    }

    public class OxfordMetaData
    {
        [JsonProperty(PropertyName = "operation")]
        public string Operation { get; set; }

        [JsonProperty(PropertyName = "provider")]
        public string Provider { get; set; }

        [JsonProperty(PropertyName = "schema")]
        public string Schema { get; set; }
    }

    public class OxfordResult
    {
        [JsonProperty(PropertyName = "id")] public string Id { get; set; }

        [JsonProperty(PropertyName = "language")]
        public string Language { get; set; }

        [JsonProperty(PropertyName = "lexicalEntries")]
        public List<OxfordLexicalEntry> LexicalEntries { get; set; }
    }

    public class OxfordLexicalEntry
    {
        [JsonProperty(PropertyName = "entries")]
        public List<OxfordEntry> Entries { get; set; }
    }

    public class OxfordEntry
    {
        [JsonProperty(PropertyName = "etymologies")]
        public List<string> Etymologies { get; set; }

        [JsonProperty(PropertyName = "senses")]
        public List<OxfordSense> Senses { get; set; }
    }

    public class OxfordSense
    {
        [JsonProperty(PropertyName = "definitions")]
        public List<string> Definitions { get; set; }
    }

    public class WordCount
    {
        public string Word { get; set; }
        public int Count { get; set; }
    }
}