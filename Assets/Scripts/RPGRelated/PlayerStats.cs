using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public PlayerScript playerScript;
    public int totalHealth;
    public int currentHealth;
    public int totalStamina;
    public int currentStamina;
    public int playerAttack;
    public int playerDefense;
    public int totalEXP;
    public int currentEXP;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
         * when current EXP > totalEXP
         * level up
         * increase stats
         * restore health
         * currentEXP - totalEXP
         * totalEXP += expCurve
         * 
         */
    }
}
