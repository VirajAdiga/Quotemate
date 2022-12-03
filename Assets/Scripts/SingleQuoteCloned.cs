
using System;
using UnityEngine;

[Serializable]
public class SingleQuoteCloned 
{
   public System.Object Data { 
       get { 
           return data; 
        } 
    }

    System.Object data;

    public SingleQuoteCloned(System.Object inData)
    {
        data = inData;
    }
}
