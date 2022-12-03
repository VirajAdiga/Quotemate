/*
This script is to parse json data and write it into a file while saving
*/

using Newtonsoft.Json;

[System.Serializable]
public class SingleQuoteSave
{
    [JsonProperty(PropertyName="q")]
    public string Quote{
        get;
        set;
    }
    [JsonProperty(PropertyName="i")]
    public int Id{
        get;
        set;
    }
}