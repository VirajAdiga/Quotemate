/*
Config Manager handles deserialization of json file into objects. This script has an array of all single
quote objects which has public access. The class has only one instance as it uses singleton
*/


using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json; 

public class ConfigManager : MonoBehaviour
{
    private SingleQuote[] mSingleQuotes;    //SingleQuote array to deserialize json
    [HideInInspector]
    public List<SingleQuote> mListOfSingleQuotes;   //Public accessible list of SingleQuote objects
    
    private static ConfigManager sConfigManagerInstance;
    
    private ConfigManager(){            //Singleton

    }

    public static ConfigManager sConfigManager{
        get{
            return sConfigManagerInstance;
        }
    }


    private void Awake(){
        sConfigManagerInstance=this;

        //Loading json data to SigleQuote array
        TextAsset textAsset=Resources.Load("quotes") as TextAsset;
        string jsonText=textAsset.ToString();
        mSingleQuotes=JsonConvert.DeserializeObject<SingleQuote[]>(jsonText);

        //Adding SingleQuote objects to public accessible list
        for(int quoteObject=0;quoteObject<mSingleQuotes.Length;quoteObject++)
        mListOfSingleQuotes.Add(mSingleQuotes[quoteObject]);
    }
}
