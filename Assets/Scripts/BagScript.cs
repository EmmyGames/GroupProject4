using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagScript : MonoBehaviour
{
    //creates bag and slots
    public GameObject slotPrefab;

    private CanvasGroup canvasGroup;

    private List<SlotScript> slots = new List<SlotScript>();
    public bool isOpen
    {
        get
        {
            return canvasGroup.alpha > 0; //if zero then it is closed
        }
    }
    public int MyEmptySlotCount
    {
        get
        {
            int count = 0;
            foreach (SlotScript slot in MySlots)
            {
                if (slot.IsEmpty)
                {
                        count++;
                }
            }
            return count;
        }
    }
    public List<SlotScript> MySlots
    {
        get
        {
            return slots;
        }
    }

    public void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void AddSlots(int slotCount)
    {
        for (int i = 0; i < slotCount; i++)
        {
            SlotScript slot = Instantiate(slotPrefab, transform).GetComponent<SlotScript>();
            slots.Add(slot); //all slots added to list
        }
    }

    public bool AddItem(Item item)
    {
        foreach (SlotScript slot in slots) //run through all slots belonging to this bag
        {
            if (slot.IsEmpty)
            {
                slot.AddItem(item);
                return true; 
            }
        }
        return false; //if bag is full then make false
    }
    public void OpenClose()
    {
        canvasGroup.alpha = canvasGroup.alpha > 0 ? 0 : 1; //if canvas group is 0 set to 0, else set to 1??
        canvasGroup.blocksRaycasts = canvasGroup.blocksRaycasts == true ? false : true; //if raycast blocking is true then set to false, else set to true
    }
    
}
