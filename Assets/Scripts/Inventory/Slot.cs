using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public int slotID;//空格ID = 物品ID
    ItemData slotItem;
    public Image slotImage;

    public Text slotNum;
    public string slotInfo;

    public GameObject itemSlot;

    /// <summary>
    /// 点击slot 调用InventoryManager的更新函数，更新物体信息和物体ID
    /// </summary>
    public void ItemOnClick()
    {
        InventoryManager.UpdateItemInfo(slotInfo, slotID);
    }

    public void SetupSlot(ItemData item)
    {
        if (item == null)
        {
            itemSlot.SetActive(false);
            return;
        }
        
        slotItem = item;
        slotImage.sprite = item.itemImage;
        slotInfo = item.itemInfo;
        if (item.itemName == ""|| item.itemName == null)
        {
            slotNum.text = "";
            slotImage.color = new Color(0, 0, 0, 0);
        }
        else
        {
            slotNum.text = item.itemHeld.ToString();
        }
            

    }
 
}
