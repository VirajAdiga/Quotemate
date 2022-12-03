/*
This script is to carry out the process of object pooling. This script has the refernce of the 
PoolOfSingleQuotesHandler which handles the process of object pooling
*/

using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    
    [SerializeField] 
    private PoolOfSingleQuotesHandler mPool=null; 
    private int mDataCount=0;    //Number of data items required at run time which is the number of single quote objects count

    void Start()
    {
        mDataCount=ConfigManager.sConfigManager.mListOfSingleQuotes.Count;

        SingleQuoteCloned[] dataSingleQuote = new SingleQuoteCloned[mDataCount];
        for(int item=0;item<mDataCount;item++)
        dataSingleQuote[item]= new SingleQuoteCloned(item + 1);

        mPool.Setup(dataSingleQuote);
    }
}
