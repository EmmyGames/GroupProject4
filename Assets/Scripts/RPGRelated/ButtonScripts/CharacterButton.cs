using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {

    [SerializeField]
    private WeaponType weaponryType;

    private Weapon equippedWeapon;

    public VFXController vfxControl;

    public StatsWindow statsWindow;

    public Weapon getWeapon()
    {
        return this.equippedWeapon;
    }

    public Image icon;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left) //if we left click
        {
            if (HandScript.MyInstance.MyMoveable is Weapon)
            {
                Weapon tmp = (Weapon)HandScript.MyInstance.MyMoveable;
                
                if (tmp.MyWeaponType == weaponryType) //if weapon type is same as this weapon type then equip it
                {
                    EquipWeapon(tmp);
                }
                UIManager.MyInstance.RefreshTooltip(tmp);
            }
            else if (HandScript.MyInstance.MyMoveable == null && equippedWeapon != null)
            {
                HandScript.MyInstance.TakeMoveable(equippedWeapon);
                CharacterPanel.MyInstance.MySelectedButton = this;
                icon.color = Color.grey;
            }
        }
    }

    public void EquipWeapon(Weapon weapon)
    {
        //Debug.Log("Equipping " + equippedWeapon.MyTitle);
        weapon.Remove();
        if(equippedWeapon != null)
        {
            if (equippedWeapon != weapon)
            {
                weapon.MySlot.AddItem(equippedWeapon);
            }
        }
        icon.enabled = true;
        icon.sprite = weapon.MyIcon;
        icon.color = Color.white;
        this.equippedWeapon = weapon; //reference to equipped weapon
        this.equippedWeapon.MyCharacterButton = this;
        StartCoroutine(vfxControl.Play());
        //StatsWindow.ChangeText();
        if (HandScript.MyInstance.MyMoveable == (weapon as IMoveable))
        {
            HandScript.MyInstance.Drop();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (equippedWeapon != null)
        {
            UIManager.MyInstance.ShowTooltip(new Vector2(0, 0), transform.position, equippedWeapon);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.MyInstance.HideTooltip();
    }
    public void DequipWeapon()
    {
        icon.color = Color.white;
        icon.enabled = false;

        /*if (gearSocket != null && equippedWeapon.MyAnimationClips != null)
        {
            gearSocket.Dequip();
        }*/
        equippedWeapon.MyCharacterButton = null;
        equippedWeapon = null;
    }
}
