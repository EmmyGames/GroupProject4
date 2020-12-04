using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Bag", menuName = "Weapons/Bag", order = 1)]
public class Bag : Item, IUseable
{
    private int slots;

    [SerializeField]
    protected GameObject bagPrefab;
    
    public BagScript MyBagScript { get; set; }

    public OpenInventoryButton MyInventoryButton { get; set; }
    public int Slots
    {
        get
        {
            return slots;
        }
    }

    public Sprite MyICon => throw new System.NotImplementedException();

    public void Initailize(int slots)
    {
        this.slots = slots;
    }
    
    public void Use()
    {
        MyBagScript = Instantiate(bagPrefab, Inventory.MyInstance.transform).GetComponent<BagScript>() ;
        //MyBagScript.AddSlots(slots);
        //Inventory.MyInstance.AddBag(this);
    }
}
