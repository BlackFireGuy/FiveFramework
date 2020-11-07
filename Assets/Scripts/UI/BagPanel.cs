using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagPanel : BasePanel
{
    Button openBag;
    public GameObject bag;
    public GameObject slotGrid;
    public Text information;
    public Button use;

    public Inventory playerInventory;

    bool isBagOpen = false;
    //------------------------
    HealthBar healthBar;
    private void Start()
    {
        openBag = this.GetControl<Button>("Open Bag");
        openBag.onClick.AddListener(OpenBag);

        InventoryManager.instance.slotGrid = slotGrid;
        InventoryManager.instance.itemInformation = information;

        use.onClick.AddListener(UseItem);
    }

    private void Update()
    {
        if (healthBar != null)
        {
            healthBar = FindObjectOfType<HealthBar>();
        }
    }
    //使用物体
    private void UseItem()
    {
        int id = InventoryManager.instance.itemID;
        if (playerInventory.itemList[id] == null) return;
        
        if(healthBar != null)
        {
            healthBar.UpdateHealth(FindObjectOfType<PlayerController>().health);
        }
         
        //实现对应效果
        FuckingYou(playerInventory.itemList[id]);
        //去掉对应ID的物品或者物品数量-1
        DeleteItem(id);

        //使用物品时保存玩家信息和仓库信息
        GameSaveManager.instance.SaveGame();
    }

    private void FuckingYou(ItemData itemData)
    {
        //Debug.Log(itemData.magic);
        PlayerInfoManager.instance.UseSomthing(itemData);
    }

    private void DeleteItem(int id)
    {
        if (playerInventory.itemList[id].itemHeld > 1)
        {
            playerInventory.itemList[id].itemHeld -= 1;
        }
        else
        {
            playerInventory.itemList[id] = new ItemData();
            information.text = "";
        }
        InventoryManager.RefreshItem();
    }
    public void OpenBag()
    {
        isBagOpen = !isBagOpen;
        if (isBagOpen) Time.timeScale = 0;
        else Time.timeScale = 1;
        bag.SetActive(isBagOpen);

        InventoryManager.RefreshItem();
    }

}