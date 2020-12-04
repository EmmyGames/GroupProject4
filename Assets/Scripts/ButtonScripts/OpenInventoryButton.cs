using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OpenInventoryButton : MonoBehaviour, IPointerClickHandler
{
    private Bag bag;

    private Sprite full, empty;
    private int inventoryIndex;
    //private Sprite full, empty; - not doing full/empty
    public Bag MyBag
    {
        get
        {
            return bag;
        }
        set
        {
            if (value != null)
            {
                GetComponent<Image>().sprite = full;
            }
            else
            {
                GetComponent<Image>().sprite = empty;
            }
            bag = value;
        }
    }

    public int MyInventoryIndex
    {
        get
        {
            return inventoryIndex;
        }

        set
        {
            inventoryIndex = value;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (Inventory.MyInstance.FromSlot != null && HandScript.MyInstance.MyMoveable != null && HandScript.MyInstance.MyMoveable is Bag)
            {
                Bag tmp = (Bag)HandScript.MyInstance.MyMoveable;
                tmp.MyInventoryButton = this;
                tmp.Use();
                MyBag = tmp;
                HandScript.MyInstance.Drop();
                Inventory.MyInstance.FromSlot = null;
            }
        }
        /*else if (Input.GetButton)
        {

        }*/
    }
}
