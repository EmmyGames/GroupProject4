using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterPanel : MonoBehaviour
{
    private static CharacterPanel instance;

    public CanvasGroup canvasGroup;

    public CharacterButton melee, range, shield;

    public CharacterButton MySelectedButton { get; set; }

    public static CharacterPanel MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<CharacterPanel>();
            }

            return instance;
        }
    }
    public void OpenClose()
    {
        if (canvasGroup.alpha <= 0)
        {
            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1;
        }
        else
        {
            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = 0;
        }
    }

    public void EquipWeapon(Weapon weapon)
    {
        switch (weapon.MyWeaponType)
        {
            case WeaponType.Melee:
                melee.EquipWeapon(weapon);
                break;
            case WeaponType.Range:
                range.EquipWeapon(weapon);
                break;
            case WeaponType.Shield:
                shield.EquipWeapon(weapon);
                break;

        }
    }
}
