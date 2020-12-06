using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void ItemCountChanged(Item item);
public class Inventory : MonoBehaviour
{

    public event ItemCountChanged itemCountChangedEvent;
    private static Inventory instance;

    private SlotScript fromSlot;
    //Bag bag;
    BagScript MyBagScript;
    public static Inventory MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Inventory>();
            }
            return instance;
        }
    }

    //private List<Bag> bags = new List<Bag>();
    private Bag bag;
    public GameObject Thisbag;

    public OpenInventoryButton[] inventoryButton;

    public Item[] items;

    public void Start()
    {
        MyBagScript = Thisbag.GetComponent<BagScript>();
    }
    public int MyEmptySlotCount
    {
        get
        {
            int count = 0;
            count += MyBagScript.MyEmptySlotCount;
            /*foreach (Bag bag in bags)
            {
                count += bag.MyBagScript.MyEmptySlotCount;
            }*/
            return count;
        }
    }
    public int MyTotalSlotCount
    {
        get
        {
            int count = 0;
            count += MyBagScript.MySlots.Count;
            /*foreach (Bag bag in bags)
            {
                count += bag.MyBagScript.MySlots.Count;
            }*/
            return count;
        }
    }
    public int MyFullSlotCount
    {
        get
        {
            return MyTotalSlotCount - MyEmptySlotCount;
        }
    }
    public SlotScript FromSlot
    {
        get
        {
            return fromSlot;
        }
        set
        {
            fromSlot = value;
            if (value != null)
            {
                fromSlot.MyIcon.color = Color.grey;
            }
        }
    }

   private void Awake()
    {
        //Bag bag = ScriptableObject.CreateInstance<Bag>();
        Bag bag = (Bag)Instantiate(items[0]); //this number has to be what number the bag is in the items list

        bag.Initailize(18); //how many slots to initialize
        bag.Use();
    }
    private void Update()
    {
        /* (Input.GetButtonDown("UseBag")) //J
        {
            Bag bag = (Bag)Instantiate(items[8]); //adds bag to the already made bag slots
            bag.Initailize(8);
            bag.Use();
        }
        if (Input.GetButtonDown("AddItem")) //k- used for devugging for adding a bag ot invnetory
        {
            Bag bag = (Bag)Instantiate(items[8]); //remove both lines??
            bag.Initailize(20);
            AddItem(bag);
        }*/
        if (Input.GetButtonDown("AddWeapon"))  //h- add weapon to inventory
        {
            Debug.Log("AddWeapon to Inventory");
            AddItem((Weapon)Instantiate(items[1]));
            AddItem((Weapon)Instantiate(items[2]));
            AddItem((Weapon)Instantiate(items[3]));
            AddItem((Weapon)Instantiate(items[4]));
            AddItem((Weapon)Instantiate(items[5]));
            AddItem((Weapon)Instantiate(items[6]));
            AddItem((Weapon)Instantiate(items[7]));
            AddItem((Weapon)Instantiate(items[8]));
            AddItem((Weapon)Instantiate(items[9]));
        }
    }
    /*public void AddBag(Bag bag)
    {
        foreach (OpenInventoryButton openInventoryButton in inventoryButton)
        {
            if (openInventoryButton.MyBag == null)
            {
                openInventoryButton.MyBag = bag;
                //bags.Add(bag);
                bag.MyInventoryButton = openInventoryButton;
                bag.MyBagScript.transform.SetSiblingIndex(openInventoryButton.MyInventoryIndex);
                break;
            }
        }
    }*/

    /*public void AddBag(Bag bag, OpenInventoryButton openInventoryButton)
    {
        bags.Add(bag);
        openInventoryButton.MyBag = bag;
        bag.MyBagScript.transform.SetSiblingIndex(openInventoryButton.MyInventoryIndex);
    }*/

    public bool AddItem(Item item) //adds item to inventory
    {
        if (item.MyStackSize > 0)
        {
            if (PlaceInStack(item))
            {
                return true;
            }
        }
        return PlaceInEmpty(item);
    }

    public void SetBag(Bag bag)
    {
        this.bag = bag;
    }

    private bool PlaceInEmpty(Item item)
    {
        //foreach (Bag bag in bags)//Checks all bags
        //
        if (bag.MyBagScript.AddItem(item)) //Tries to add the item
        {
            OnItemCountChanged(item);
            return true; //It was possible to add the item
        }
        //if (bag.MyBagScript.AddItem(item)) //Tries to add the item
        //{
        //Debug.Log("Try to add item");
        //    OnItemCountChanged(item);
        //    return true; //It was possible to add the item
        //}
        //}
        return false;
    }
    private bool PlaceInStack(Item item)
    {
        //foreach (Bag bag in bags)//Checks all bags
        //{
            foreach (SlotScript slots in MyBagScript.MySlots) //Checks all the slots on the current bag
            {
                if (slots.StackItem(item)) //Tries to stack the item
                {
                    OnItemCountChanged(item);
                    return true; //It was possible to stack the item
                }
            }
        //}

        return false; //It wasn't possible to stack the item
    }
    public void OpenClose()
    {
        //Checks if any bags are closed

        bag.MyBagScript.OpenClose();

        bool closedBag = bag.MyBagScript.IsOpen;
        Debug.Log(closedBag);

        //If closed bag == true, then open all closed bags
        //If closed bag == false, then close all open bags

        //foreach (Bag bag in bags)
       // {
            //if (bag.MyBagScript.IsOpen != closedBag)
            //{
            //    bag.MyBagScript.OpenClose();
            //}
        //}
    }
    public Stack<IUseable> GetUseables(IUseable type)
    {
        Stack<IUseable> useables = new Stack<IUseable>();

        //foreach (Bag bag in bags)
        //{
            foreach (SlotScript slot in MyBagScript.MySlots)
            {
                if (!slot.IsEmpty && slot.MyItem.GetType() == type.GetType())
                {
                    foreach (Item item in slot.MyItems)
                    {
                        useables.Push(item as IUseable);
                    }
                }
            }
        //}

        return useables;
    }

    public int GetItemCount(string type)
    {
        int itemCount = 0;

        //foreach (Bag bag in bags)
        //{
            foreach (SlotScript slot in MyBagScript.MySlots)
            {
                if (!slot.IsEmpty && slot.MyItem.MyTitle == type)
                {
                    itemCount += slot.MyItems.Count;
                }
            }
        //}

        return itemCount;

    }
    public Stack<Item> GetItems(string type, int count)
    {
        Stack<Item> items = new Stack<Item>();

            foreach (SlotScript slot in bag.MyBagScript.MySlots)
            {
                if (!slot.IsEmpty && slot.MyItem.MyTitle == type)
                {
                    foreach (Item item in slot.MyItems)
                    {
                        items.Push(item);

                        if (items.Count == count)
                        {
                            return items;
                        }
                    }
                }
            }
        return items;
    }

    public void OnItemCountChanged(Item item)
    {
        if (itemCountChangedEvent != null)
        {
            itemCountChangedEvent.Invoke(item);
        }
    }




}
