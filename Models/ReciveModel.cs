using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ChusyKidsAPI.Model
{
    public class Parameters
    {
        public DateTime date { get; set; }

        [JsonProperty("date.original")]
        public string DateOriginal { get; set; }

        [JsonProperty("no-input")]
        public int? NoInput { get; set; }

        [JsonProperty("no-match")]
        public int? NoMatch { get; set; }
    }

    public class Text
    {
        public List<string> text { get; set; }
    }

    public class FulfillmentMessage
    {
        public Text text { get; set; }
    }

    public class OutputContext
    {
        public string name { get; set; }
        public int lifespanCount { get; set; }
        public Parameters parameters { get; set; }
    }

    public class Intent
    {
        public string name { get; set; }
        public string displayName { get; set; }
        public bool endInteraction { get; set; }
    }

    public class ReciveModel
    {
        public string queryText { get; set; }
        public string action { get; set; }
        public Parameters parameters { get; set; }
        public bool allRequiredParamsPresent { get; set; }
        public string fulfillmentText { get; set; }
        public List<FulfillmentMessage> fulfillmentMessages { get; set; }
        public List<OutputContext> outputContexts { get; set; }
        public Intent intent { get; set; }
        public int intentDetectionConfidence { get; set; }
        public string languageCode { get; set; }
    }


}