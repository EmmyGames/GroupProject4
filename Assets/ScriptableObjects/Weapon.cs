using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType {Melee, Range, Shield}

[CreateAssetMenu(fileName = "New Weapon", menuName = "Items/Weapon", order = 2)]
public class Weapon : Item //since of type item it is a scriptable object
{
    [SerializeField]
    private WeaponType weaponType;

    public new string name;

    public int attack;

    public int durability;

    public int defense; 

    internal WeaponType MyWeaponType
    {
        get
        {
            return weaponType;
        }
    }

    public override string GetDescription() //weapon is of type item and in item (an interface) and the get description in item is the default description to this is overriding the other one to make it mean this instead
    {
        string stats = string.Empty;
        /*if (name != null)
        {
            stats += string.Format("\n + {0}", name);
        }*/
        //stats += string.Format("\n ", name);
        if (attack > 0)
        {
            stats += string.Format("\n + {0} attack", attack);
        }
        if (durability > 0)
        {
            stats += string.Format("\n + {0} durability", durability); //bracket represents where the white durability amount will go
        }
        if (defense > 0)
        {
            stats += string.Format("\n + {0} defense", defense);
        }

        return base.GetDescription() + stats;
    }

    public void Equip()
    {
        CharacterPanel.MyInstance.EquipWeapon(this);
    }
}
