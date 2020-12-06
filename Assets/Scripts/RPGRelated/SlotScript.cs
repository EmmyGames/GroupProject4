using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SlotScript : MonoBehaviour, IPointerClickHandler, IClickable, IPointerEnterHandler, IPointerExitHandler
{
    private ObservableStack<Item> items = new ObservableStack<Item>(); //to be able to "stack" items

    public Image icon;
    //public Image cover;
    public Text stackSize;

    public BagScript MyBag { get; set; }

    //public int MyIndex { get; set; }
    public bool IsEmpty
    {
        get
        {
            return items.Count == 0; //if 0 then nothing is in it
        }
    }
    public bool IsFull
    {
        get
        {
            if (IsEmpty || MyCount < MyItem.MyStackSize)
            {
                return false;
            }
            return true;
        }
    }
    public Item MyItem
    {
        get
        {
            if (!IsEmpty)
            {
                return MyItems.Peek();
            }
            return null;
        }
    }
    public Image MyIcon
    {
        get
        {
            return icon;
        }
        set
        {
            icon = value;
        }
    }
    public int MyCount
    {
        get { return MyItems.Count; }
    }
    public Text MyStackText
    {
        get
        {
            return stackSize;
        }
    }
    public ObservableStack<Item> MyItems
    {
        get
        {
            return items;
        }
    }
    /*public Image MyCover
    {
        get
        {
            return cover;
        }
    }*/
    private void Awake()
    {
        //Assigns all the event on our observable stack to the updateSlot function
        MyItems.OnPop += new UpdateStackEvent(UpdateSlot);
        MyItems.OnPush += new UpdateStackEvent(UpdateSlot);
        MyItems.OnClear += new UpdateStackEvent(UpdateSlot);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (Inventory.MyInstance.FromSlot == null && !IsEmpty) //If we don't have something to move
            {
                if (HandScript.MyInstance.MyMoveable != null)
                {
                    /*if (HandScript.MyInstance.MyMoveable is Bag)
                    {
                        if (MyItem is Bag)
                        {
                            Inventory.MyInstance.SwapBags(HandScript.MyInstance.MyMoveable as Bag, MyItem as Bag);
                        }
                    }*/
                    if (HandScript.MyInstance.MyMoveable is Weapon)
                    {
                        if (MyItem is Weapon && (MyItem as Weapon).MyWeaponType == (HandScript.MyInstance.MyMoveable as Weapon).MyWeaponType)
                        {
                            (MyItem as Weapon).Equip();

                            HandScript.MyInstance.Drop();
                        }
                    }
                }
                else
                {
                    HandScript.MyInstance.TakeMoveable(MyItem as IMoveable);
                    Inventory.MyInstance.FromSlot = this;
                }
            }
            else if (Inventory.MyInstance.FromSlot == null && IsEmpty)
            {
                /*if (HandScript.MyInstance.MyMoveable is Bag)
                {
                    //Dequips a bag from the inventory
                    Bag bag = (Bag)HandScript.MyInstance.MyMoveable;

                    //Makes sure we cant dequip it into itself and that we have enough space for the items from the dequipped bag
                    if (bag.MyBagScript != MyBag && Inventory.MyInstance.MyEmptySlotCount - bag.MySlotCount > 0)
                    {
                        AddItem(bag);
                        bag.MyInventoryButton.RemoveBag();
                        HandScript.MyInstance.Drop();
                    }
                }*/
                if (HandScript.MyInstance.MyMoveable is Weapon)
                {
                    Weapon weapon = (Weapon)HandScript.MyInstance.MyMoveable;
                    CharacterPanel.MyInstance.MySelectedButton.DequipWeapon();
                    AddItem(weapon);
                    HandScript.MyInstance.Drop();
                }
            }
            else if (Inventory.MyInstance.FromSlot != null)//If we have something to move
            {
                //We will try to do diffrent things to place the item back into the inventory
                if (PutItemBack() || MergeItems(Inventory.MyInstance.FromSlot) || SwapItems(Inventory.MyInstance.FromSlot) || AddItems(Inventory.MyInstance.FromSlot.MyItems))
                {
                    HandScript.MyInstance.Drop();
                    Inventory.MyInstance.FromSlot = null;
                }
            }
        }
        if (eventData.button == PointerEventData.InputButton.Right && HandScript.MyInstance.MyMoveable == null)//If we rightclick on the slot
        {
            UseItem();
        }
    }
    public bool  AddItem(Item item)
    {
        items.Push(item);
        icon.sprite = item.MyIcon;
        icon.color = Color.white; //set alpha back to being seeable
        item.MySlot = this;
        return true;
    }
    public bool AddItems(ObservableStack<Item> newItems)
    {
        if (IsEmpty || newItems.Peek().GetType() == MyItem.GetType())
        {
            int count = newItems.Count;

            for (int i = 0; i < count; i++)
            {
                if (IsFull)
                {
                    return false;
                }

                AddItem(newItems.Pop());
            }
            return true;
        }
        return false;
    }
    public void RemoveItem(Item item)
    {
        if (!IsEmpty)
        {
            Inventory.MyInstance.OnItemCountChanged(MyItems.Pop());
        }
    }
    public void Clear()
    {
        int initCount = MyItems.Count;
        //MyCover.enabled = false;
        if (initCount > 0)
        {
            for (int i = 0; i < initCount; i++)
            {
                Inventory.MyInstance.OnItemCountChanged(MyItems.Pop());
            }
        }
    }
    public void UseItem()
    {
        if (MyItem is IUseable)
        {
            (MyItem as IUseable).Use();
        }
        else if (MyItem is Weapon)
        {
            (MyItem as Weapon).Equip();
        }
    }
    public bool StackItem(Item item)
    {
        if (!IsEmpty && item.name == MyItem.name && MyItems.Count < MyItem.MyStackSize)
        {
            MyItems.Push(item);
            item.MySlot = this;
            return true;
        }
        return false;
    }
    private bool PutItemBack()
    {
        if (Inventory.MyInstance.FromSlot == this)
        {
            Inventory.MyInstance.FromSlot.MyIcon.enabled = true;
            return true;
        }
        return false;
    }

    private bool SwapItems(SlotScript from)
    {
        if (IsEmpty)
        {
            return false;
        }
        if (from.MyItem.GetType() != MyItem.GetType() || from.MyCount+MyCount > MyItem.MyStackSize)
        {
            //Copy all the items we need to swap from A
            ObservableStack<Item> tmpFrom = new ObservableStack<Item>(from.MyItems);

            //Clear Slot a
            from.MyItems.Clear();
            //All items from slot b and copy them into A
            from.AddItems(MyItems);

            //Clear B
            MyItems.Clear();
            //Move the items from ACopy to B
            AddItems(tmpFrom);

            return true;
        }
        return false;
    }

    private bool MergeItems(SlotScript from)
    {
        if (IsEmpty)
        {
            return false;
        }
        if (from.MyItem.GetType() == MyItem.GetType() && !IsFull && from.MyItem.MyTitle == MyItem.MyTitle)
        {
            //How many free slots do we have in the stack
            int free = MyItem.MyStackSize - MyCount;

            for (int i = 0; i < free; i++)
            {
                AddItem(from.MyItems.Pop());
            }
            return true;
        }
        return false;
    }

    private void UpdateSlot()
    {
        UIManager.MyInstance.UpdateStackSize(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //We need to show tooltip
        if (!IsEmpty)
        {
            UIManager.MyInstance.ShowTooltip(new Vector2(1, 0),transform.position, MyItem);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.MyInstance.HideTooltip();
    }
}
