using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.UI;
using UnityEngine.EventSystems;

public class OpenInventoryButton : MonoBehaviour, IPointerClickHandler
{
    public GameObject bagReference;

    private Bag bag;
    //public Sprite full, empty;
    public int inventoryIndex;
    BagScript bagScript;
    void Start()
    {
        bagScript = bagReference.GetComponent<BagScript>();
    }
    public Bag MyBag
    {
        get
        {
            return bag;
        }
       set
        {
            /*if (value != null)
            {
                GetComponent<Image>().sprite = full;
            }
            else
            {
                GetComponent<Image>().sprite = empty;
            }*/
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
            bagScript.OpenClose();
            if (Inventory.MyInstance.FromSlot != null && HandScript.MyInstance.MyMoveable != null && HandScript.MyInstance.MyMoveable is Bag)
            {
                //bag.MyBagScript.OpenClose();
                /*
                Bag tmp = (Bag)HandScript.MyInstance.MyMoveable;
                tmp.MyInventoryButton = this;
                tmp.Use();
                //MyBag = tmp;
                HandScript.MyInstance.Drop();
                Inventory.MyInstance.FromSlot = null;*/
            }
        }
        /*else if (bag!= null) //if bag is equipped (which it should always be)
        {
            bag.MyBagScript.OpenClose();
        }
        /*else if (Input.GetButton)
        {

        }*/
    }
}
