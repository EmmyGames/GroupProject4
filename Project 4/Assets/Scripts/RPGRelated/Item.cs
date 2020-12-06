using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class Item : ScriptableObject, IMoveable, IDescribable
{
    public Sprite icon;

    //public new string name;
    /*[SerializeField]
    public int attack;
    [SerializeField]
    public int durability;
    [SerializeField]
    public int defense;*/

    private SlotScript slot;
    private CharacterButton characterButton;
    public int stackSize;

    [SerializeField]
    private string title;

    public Quality quality;
    public Sprite MyIcon
    {
        get
        {
            return icon;
        }
    }
    public int MyStackSize
    {
        get
        {
            return stackSize;
        }
    }
    public SlotScript MySlot
    {
        get
        {
            return slot;
        }
        set
        {
            slot = value;
        }
    }
    public Quality MyQuality
    {
        get
        {
            return quality;
        }
    }
    public string MyTitle
    {
        get
        {
            return title;
        }
    }
    public CharacterButton MyCharacterButton
    {
        get
        {
            return characterButton;
        }
        set
        {
            MySlot = null;
            characterButton = value;
        }
    }
    public virtual string GetDescription()
    {
        return string.Format("<color={0}>{1}</color>", QualityColor.MyColors[MyQuality], MyTitle);
    }
    public void Remove()
    {
        if (MySlot != null)
        {
            MySlot.RemoveItem(this);
        }
    }
}
