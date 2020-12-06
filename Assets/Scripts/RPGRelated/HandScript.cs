using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HandScript : MonoBehaviour
{
    private static HandScript instance;

    public static HandScript MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<HandScript>();
            }
            return instance;
        }
    }
    public IMoveable MyMoveable { get; set; } //current moveable

    private Image icon; //icon we are moving around

    public Vector3 offset; //offset to mouse icon off mouse
    void Start()
    {
        icon = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        icon.transform.position = Input.mousePosition + offset;

        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject() && MyInstance.MyMoveable != null)
        {
            DeleteItem();
        }
        
    }

    public void TakeMoveable(IMoveable moveable) //take moveable in hand so that we can move it around
    {
        this.MyMoveable = moveable;
        icon.sprite = moveable.MyIcon;
        icon.color = Color.white; //keeping icon not transparent
    }

    public IMoveable Put()
    {
        IMoveable tmp = MyMoveable;
        MyMoveable = null;
        icon.color = new Color(0, 0, 0, 0);
        return tmp;
    }
    public void Drop()
    {
        MyMoveable = null;
        icon.color = new Color(0, 0, 0, 0);
        Inventory.MyInstance.FromSlot = null;
    }

    public void DeleteItem()
    {
        if (MyMoveable is Item)
        {
            Item item = (Item)MyMoveable;
            if (item.MySlot != null)
            {
                item.MySlot.Clear();
            }
            else if (item.MyCharacterButton != null)
            {
                item.MyCharacterButton.DequipWeapon();
            }
        }
        Drop();
        Inventory.MyInstance.FromSlot = null;
    }
}
