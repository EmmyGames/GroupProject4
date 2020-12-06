using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Bag", menuName = "Items/Bag", order = 1)]
public class Bag : Item, IUseable
{
    [SerializeField]
    private int slots;

    [SerializeField]
    private GameObject bagPrefab;
    
    public BagScript MyBagScript { get; set; }

    public OpenInventoryButton MyInventoryButton { get; set; }
    public int Slots //getting slots
    {
        get
        {
            return slots;
        }
    }

    public Sprite MyICon => throw new System.NotImplementedException();

    public void Initailize(int slots) //initalize bag with set slots
    {
        this.slots = slots;
    }
    
    public void Use()
    {
        MyBagScript = Instantiate(bagPrefab, Inventory.MyInstance.transform).GetComponent<BagScript>() ;
        MyBagScript.AddSlots(slots);
        Inventory.MyInstance.SetBag(this);
    }

    public override string GetDescription()
    {
        return base.GetDescription() + string.Format("\n{0} slot bag", slots);
    }
}
