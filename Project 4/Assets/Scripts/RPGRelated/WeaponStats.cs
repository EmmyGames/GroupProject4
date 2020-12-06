using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using TMPro;
using UnityEngine.UI;

public class WeaponStats : MonoBehaviour
{
    public Weapon weapon;
    public Text AttackValue;
    public Text DurabilityValue;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            Debug.Log(other.gameObject.name);
            AttackValue.text = "Attack: " + weapon.attack.ToString();
            DurabilityValue.text = "Durability: " + weapon.durability.ToString();
        }
    }
} 
