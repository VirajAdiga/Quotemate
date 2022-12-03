/*
PoolOfSingleQuoteObject creates the object pool upto required size and manages giving and getting back the pool
items
*/

using UnityEngine;

public class PoolOfSingleQuoteObject : MonoBehaviour
{
    private  int mPoolSize = 15;     //The pool size
    [SerializeField] 
    private GameObject mSingleQuotePrefab=null;       //Object pool item

    private int mHead = 0;          //Head of the object pool

    private void Awake() 
    {
        //Creating the object pool upto specified size
        mHead = 0;
        for(int i = 0; i < mPoolSize; i++) 
        {
            GameObject poolObject = Instantiate(mSingleQuotePrefab) as GameObject;
            poolObject.transform.SetParent(this.transform);
            poolObject.SetActive(false);
        }
    }

    //Method to return an item from the pool
    public GameObject ItemBorrow()
    {
        if(mHead >= mPoolSize) 
        {
            return null;
        }
        mHead++;
        return this.transform.GetChild(0).gameObject;
    }

    //Method to get back the given item to the pool
   public void ItemReturn(GameObject inPoolItem)
   {
        if(inPoolItem==null)return;
        if(mHead <= 0) 
        {
            return;
        }
        mHead--;
        inPoolItem.SetActive(false);
        inPoolItem.transform.SetParent(this.transform);
   }
}
