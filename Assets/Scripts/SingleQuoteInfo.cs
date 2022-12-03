/*
This script is to help single quote object to copy the text contents between desired components
*/


using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SingleQuoteInfo : MonoBehaviour
{
    [SerializeField]
    private InputField mUserEditedText=null;    //This is where user types his text
    [SerializeField]
    private TextMeshProUGUI mToBeAttachedTo=null;   //This is where the quote is displayed
    private int mIdOfTheSingleQuote;    //ID of the Single quote object
    public int ID{
       get{
           return mIdOfTheSingleQuote;
       }
       set{
           mIdOfTheSingleQuote=value;
       }
    }
    private float mHeightOfTheComponent;    //Height of the prefab
    public float ItemHeight{
       get{
           return mHeightOfTheComponent;
       }
    }
   private void Awake(){
       mHeightOfTheComponent=gameObject.GetComponent<RectTransform>().sizeDelta.y;
   }

    //Method to copy desired text as quotes
   public void GetTheTextAndAttach(GameObject inSingleQuoteObject,int inIndex){
            if(inSingleQuoteObject==null)return;
            inSingleQuoteObject.name=inIndex.ToString();
            inSingleQuoteObject.GetComponent<SingleQuoteInfo>().ID=inIndex;

            if(SaveAndLoadManager.saveAndLoadManager.mQuoteAndId.ContainsKey(inIndex+1))
            mToBeAttachedTo.text=SaveAndLoadManager.saveAndLoadManager.mQuoteAndId[inIndex+1];

            else
            mToBeAttachedTo.text=ConfigManager.sConfigManager.mListOfSingleQuotes[inIndex].quote;
    }

    //Method to copy text from input field to quote and save it
    public void CopyEditedTextToTextMeshPro(){
        string userText=mUserEditedText.text.ToString();
        mToBeAttachedTo.text=userText;
        int idOfTheQuote=gameObject.GetComponent<SingleQuoteInfo>().ID+1;
        SaveAndLoadManager.saveAndLoadManager.SaveTheDataOfParticularID(idOfTheQuote,userText);

        if(SaveAndLoadManager.saveAndLoadManager.mQuoteAndId.ContainsKey(idOfTheQuote))
        SaveAndLoadManager.saveAndLoadManager.mQuoteAndId[idOfTheQuote]=userText;
        else
        SaveAndLoadManager.saveAndLoadManager.mQuoteAndId.Add(idOfTheQuote,userText);
    }
}
