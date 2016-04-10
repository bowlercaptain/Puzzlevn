using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//[ExecuteInEditMode]//later, once this doesn't suck.
class AutoGrid : MonoBehaviour
{
    /// <summary>
    /// Resizes children so there are this many columns if non-zero. Otherwise will just deal with weird sizes. EVERYTHING IS LEFT-ALIGNED.
    /// </summary>
    public int forceColumns = 0;

    public float wPad=0;
    public float vPad=0;
    public UnityEngine.UI.ScrollRect view;

//    public bool lockWidth;//THIS SCRIPT ONLY DOES VERTICAL GRIDS OF FIXED WIDTH NYEAH
    void Awake()
    {
        //view.enabled = false;//this is dumb
    }


    void Start()
    {
        RectTransform cacheRectT = GetComponent<RectTransform>();
        cacheRectT.offsetMax = new Vector2(cacheRectT.offsetMax.x,0);
        cacheRectT.offsetMin = new Vector2(cacheRectT.offsetMin.x, 10000);
        float nextRowBottom = 0;
        float nextColumnLeft = 0;
        float thisrowtop = 0;
        int itemsThisColumn = 0;
        if (forceColumns == 0)
        {
            RectTransform[] children = GetComponentsInChildren<RectTransform>();
            for (int i = 1; i < children.Length; i++)
            {
                RectTransform child = children[i];
           
                Debug.Log(child.name);
                if(nextColumnLeft + child.rect.width + wPad > cacheRectT.rect.width)
                {
                    Debug.Log("yes!");
                    itemsThisColumn = 0;
                    nextColumnLeft = 0;
                    thisrowtop = nextRowBottom + vPad;
                }
                float nextColumnCenter = nextColumnLeft + wPad + child.rect.width / 2;
                float thisRowCenter = thisrowtop - child.rect.height / 2;
                child.anchoredPosition = new Vector2(nextColumnCenter, thisRowCenter);
                nextRowBottom = Mathf.Min(nextRowBottom, thisrowtop - child.rect.height);
                nextColumnLeft = nextColumnCenter + child.rect.width / 2;
                itemsThisColumn++;
                
                //yield return new WaitForSeconds(1f);
                //child.anchoredPosition = new Vector2(nextColumnLeft + (child.rect.width / 2), nextRowBottom - (child.rect.height / 2));
                //nextRowBottom -= child.rect.height;
                //GetComponent<RectTransform>().offsetMin = new Vector2(0, nextRowBottom);

                //if free columns
                //loop:
                //if not child(.rect.)width + wpad + nextcolumnleft < parentwidth
                //thisrowtop = nextrowbottom + vpad
                //nextcolumnleft = 0;
                //place item in row next
                //top = thisrowtop
                //left = nextcolumnleft + wpad
                //nextrowbottom = lowest (new child bottom, nextrowbottom)
                //nextcolumnleft = right
                //else (fixed columns)
                //add in padding later
                //loop:
                //maintain column number
                //while in column, resize and place based on parent width and padding
                //calculate nextrowbottom with maxes as above

            }
            cacheRectT.offsetMin = new Vector2(0, nextRowBottom);
        }
        //view.enabled = true;
    }  
}
