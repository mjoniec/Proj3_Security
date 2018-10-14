using Newtonsoft.Json;
using System;
//using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Access.Model
{
    [Table("request")]
    public class Request
    {
        //[JsonIgnore]
        [JsonProperty("id")]
        public int Id { get; set; }


        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        //[System.ComponentModel.DataAnnotations.Key/*, DatabaseGenerated(DatabaseGeneratedOption.None), JsonProperty("ix")*/]
        [JsonProperty("ix")]
        public int Index { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("visits")]
        public int? Visits { get; set; }

        [JsonProperty("data")]
        public DateTime Date { get; set; }
    }
}
