using Newtonsoft.Json;
using System.Collections.Generic;

namespace AppRootStack.Models
{
    public class Name
    {
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("first")]
        public string First { get; set; }
        [JsonProperty("last")]
        public string Last { get; set; }

        public string Names => $"{Title} {First} {Last}";
    }

    public class Picture
    {
        [JsonProperty("large")]
        public string Large { get; set; }

        [JsonProperty("medium")]
        public string Medium { get; set; }

        [JsonProperty("thumbnail")]
        public string Thumbnail { get; set; }
    }

    public class Result
    {
        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("name")]
        public Name Name { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("picture")]
        public Picture Picture { get; set; }
    }

    public class Info
    {
        [JsonProperty("seed")]
        public string Seed { get; set; }

        [JsonProperty("results")]
        public int Results { get; set; }

        [JsonProperty("page")]
        public int Page { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }
    }

    public class Data
    {
        [JsonProperty("results")]
        public List<Result> Results { get; set; }

        [JsonProperty("info")]
        public Info Info { get; set; }
    }
}
