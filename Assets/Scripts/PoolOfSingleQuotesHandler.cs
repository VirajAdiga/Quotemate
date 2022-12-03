/*
PoolOfSingleQuotesHandler handles updating the object pool during run time
*/

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PoolOfSingleQuotesHandler : MonoBehaviour,IDragHandler, IBeginDragHandler, IEndDragHandler
{

    [SerializeField] 
    private ScrollRect mScrollRect=null;
    [SerializeField] 
    private RectTransform mViewPortTransform=null;
    [SerializeField] 
    private RectTransform mDragDetectionTransform=null;  //GameObject to detect the drag of mouse
    [SerializeField] 
    private RectTransform mContentTransform=null;
    [SerializeField] 
    private PoolOfSingleQuoteObject mItemPool=null;
    private float mItemHeight=200 ;          //Height of the prefab object(already calculated)
    private int mBufferSize=6;       
    private int mTargetVisibleItemCount { 
        get { 
            return Mathf.Max(Mathf.CeilToInt(mViewPortTransform.rect.height / mItemHeight), 10); 
        } 
    }
    private int mTopItemOutOfView { 
        get { 
            return Mathf.CeilToInt(mContentTransform.anchoredPosition.y / mItemHeight); 
        } 
    }

    private float mDragDetectionAnchorPreviousY = 0;

    private SingleQuoteCloned[] mSingleQuotedata;
    private int mDataHead = 0;
    private int mDataTail = 0;

    private void Start(){
        mItemHeight=200;
        mBufferSize=2;
    }
    public void Setup(SingleQuoteCloned[] inData)
    {
        if(inData==null)return;
        mScrollRect.onValueChanged.AddListener(OnDragDetectionPositionChange);
        

        mSingleQuotedata = inData;

        mDragDetectionTransform.sizeDelta = new Vector2(mDragDetectionTransform.sizeDelta.x, mSingleQuotedata.Length * mItemHeight);

        for(int item=0;item<mTargetVisibleItemCount+mBufferSize;item++)
        {
            GameObject itemGO = mItemPool.ItemBorrow();
            itemGO.transform.SetParent(mContentTransform);
            itemGO.SetActive(true);
            itemGO.transform.localScale = Vector3.one;
            itemGO.GetComponent<SingleQuoteInfo>().GetTheTextAndAttach(itemGO,item);
            mDataTail++;
        }
    }
    public void OnDragDetectionPositionChange(Vector2 inDragNormalizePos)
    {
        float dragDelta = mDragDetectionTransform.anchoredPosition.y - mDragDetectionAnchorPreviousY;

        mContentTransform.anchoredPosition = new Vector2(mContentTransform.anchoredPosition.x, mContentTransform.anchoredPosition.y + dragDelta);

        UpdateContentBuffer();

        mDragDetectionAnchorPreviousY = mDragDetectionTransform.anchoredPosition.y;
    }

    public void OnDrag(PointerEventData inEventData)
    {
        
    }

    public void OnBeginDrag(PointerEventData inEventData)
    {
        mDragDetectionAnchorPreviousY = mDragDetectionTransform.anchoredPosition.y;
    }

    public void OnEndDrag(PointerEventData inEventData)
    {
       
    }

    //Method to update the single quote elements during run time
    void UpdateContentBuffer()
    {
        if(mTopItemOutOfView > mBufferSize)
        {
            if(mDataTail >= mSingleQuotedata.Length)
            {
                return;
            }
            GameObject refObj=mContentTransform.GetChild(0).gameObject;
            Transform firstChildTransform = mContentTransform.GetChild(0);
            firstChildTransform.SetSiblingIndex(mContentTransform.childCount - 1);
            firstChildTransform.gameObject.GetComponent<SingleQuoteInfo>().GetTheTextAndAttach(refObj,mDataTail);
            mContentTransform.anchoredPosition = new Vector2(mContentTransform.anchoredPosition.x, mContentTransform.anchoredPosition.y - firstChildTransform.gameObject.GetComponent<SingleQuoteInfo>().ItemHeight);
            mDataHead++;
            mDataTail++;
        }
        else if(mTopItemOutOfView < mBufferSize)
        {
            if(mDataHead <= -(mBufferSize+mTargetVisibleItemCount))
            {
                return;
            }
            GameObject refrenceobj=mContentTransform.GetChild(mContentTransform.childCount - 1).gameObject;
            Transform lastChildTransform = mContentTransform.GetChild(mContentTransform.childCount - 1);
            lastChildTransform.SetSiblingIndex(0);
            mDataHead--;
            mDataTail--;
            lastChildTransform.gameObject.GetComponent<SingleQuoteInfo>().GetTheTextAndAttach(refrenceobj,mDataTail);
            mContentTransform.anchoredPosition = new Vector2(mContentTransform.anchoredPosition.x, mContentTransform.anchoredPosition.y + lastChildTransform.gameObject.GetComponent<SingleQuoteInfo>().ItemHeight);

        }
    }
}
