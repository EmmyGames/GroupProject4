using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public GameObject tooltip;
    private Text tooltipText;

    [SerializeField]
    private RectTransform tooltipRect;
    public static UIManager MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();
            }
            return instance;
        }
    }

    public CharacterPanel characterPanel;
    void Start()
    {
        tooltipText = tooltip.GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("CharacterMenu"))
        {
            characterPanel.OpenClose();
            Inventory.MyInstance.OpenClose();
            Debug.Log("pressing m");
        }
        if (Input.GetButtonDown("OpenCloseBag"))
        {
            Debug.Log("I am trying to close this now");
            //Inventory.MyInstance.OpenClose();
        }
    }

    public void UpdateStackSize(IClickable clickable) //updates stacksize number
    {
        if (clickable.MyCount > 1) //If slot has more than one item on it
        {
            clickable.MyStackText.text = clickable.MyCount.ToString();
            clickable.MyStackText.color = Color.white;
            clickable.MyIcon.color = Color.white;
        }
        else //If it only has 1 item on it
        {
            clickable.MyStackText.color = new Color(0, 0, 0, 0);
            clickable.MyIcon.color = Color.white;
        }
        if (clickable.MyCount == 0) //If slot is empty, hide icon
        {
            clickable.MyIcon.color = new Color(0, 0, 0, 0);
            clickable.MyStackText.color = new Color(0, 0, 0, 0);
        }
    }
    public void ClearStackCount(IClickable clickable)
    {
        clickable.MyStackText.color = new Color(0, 0, 0, 0);
        clickable.MyIcon.color = Color.white;
    }
    public void ShowTooltip(Vector2 pivot, Vector3 position, IDescribable description)
    {
        tooltipRect.pivot = pivot;
        tooltip.SetActive(true);
        tooltip.transform.position = position;
        tooltipText.text = description.GetDescription();
    }
    public void HideTooltip()
    {
        tooltip.SetActive(false);
    }
    public void RefreshTooltip(IDescribable description)
    {
        tooltipText.text = description.GetDescription();
    }
}
