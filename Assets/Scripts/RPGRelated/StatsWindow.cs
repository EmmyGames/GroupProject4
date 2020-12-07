using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StatsWindow : MonoBehaviour
{
    public CharacterButton meleeButton;
    public CharacterButton rangeButton;
    public CharacterButton shieldButton;
    public Text meleeText;
    public Text rangeText;

    //replace with your script
    public int playerAttack;
    public int playerDefense;

    //keep for this script
    public int currentMeleeAttack = 0;
    public int currentDefense = 0;
    public int currentRangeAttack = 0;

    public int weaponAttack;

    public Slider shieldDur;
    public Slider meleeDur;
    public Slider rangeDur;

    public int currentShield;
    public int currentMelee;
    public int currentRange;

    void Start()
    {
        currentMeleeAttack = playerAttack;
        currentDefense = playerDefense;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("melee attack" + meleeButton.getWeapon().attack + "durability is" + meleeButton.getWeapon().durability);
        //meleeButton.getWeapon();
        Debug.Log("Range attack" + rangeButton.getWeapon().attack);
        //Debug.Log("Shield defense" + shieldButton.getWeapon().defense);
        ChangeText();
    }

    public void ChangeText()
    {
        currentMeleeAttack += meleeButton.getWeapon().attack;
        currentDefense += shieldButton.getWeapon().defense;
        currentRangeAttack += rangeButton.getWeapon().attack;


        meleeText.text = "Strength: " + currentMeleeAttack + "\n" + "Defense: " + currentDefense; 
        rangeText.text = "Strength: " + currentRangeAttack + "\n" + "Defense: " + currentDefense;
    }

    public void LessenDurability()
    {
        shieldDur.value = currentShield;
        meleeDur.value  =currentMelee;
        rangeDur.value = currentRange;
    }

}
