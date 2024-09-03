using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FurnaceManager : MonoBehaviour
{
    public InventorySlot fuelSlot;
    public InventorySlot oreSlot;
    public InventorySlot productSlot;
    public Item ironBarItem;
    public Item goldBarItem;
    public InventoryManager inventoryManager;
    public float smeltingTime = 10.0f;

    private int time;
    private int productAmount;
    private Item currentProductItem;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            StartSmelting();
        }
    }

    void StartSmelting()
    {
        string inventoryContents;
        if (CheckOreAndFuel(out inventoryContents))
        {
            StartCoroutine(Smelt());
        }
    }

    bool CheckOreAndFuel(out string inventoryContents)
    {
        inventoryContents = "";
        if (fuelSlot == null || oreSlot == null)
        {
            Debug.Log("Fuel slot or Ore slot is null");
            return false;
        }

        InventoryItem fuelSlotItem = fuelSlot.GetComponentInChildren<InventoryItem>();
        InventoryItem oreSlotItem = oreSlot.GetComponentInChildren<InventoryItem>();

        if (fuelSlotItem == null || oreSlotItem == null)
        {
            Debug.Log("Missing fuel or ore item");
            return false;
        }

        if ((oreSlotItem.item.itemName == "IronOre" || oreSlotItem.item.itemName == "GoldOre") && fuelSlotItem.item.itemName == "Stick")
        {
            if (fuelSlotItem.count >= 3)
            {
                if (oreSlotItem.item.itemName == "IronOre")
                {
                    currentProductItem = ironBarItem;
                    productAmount = (int)Mathf.Ceil(oreSlotItem.count * 0.5f);
                }
                else if (oreSlotItem.item.itemName == "GoldOre")
                {
                    currentProductItem = goldBarItem;
                    productAmount = (int)Mathf.Ceil(oreSlotItem.count * 0.3f);
                }

                if (oreSlotItem.count >= 3 && oreSlotItem.count < 7)
                {
                    time = 5;
                }
                else if (oreSlotItem.count >= 70 && oreSlotItem.count < 90)
                {
                    time = 10;
                }
                else if (oreSlotItem.count >= 90 && oreSlotItem.count < 100)
                {
                    time = 15;
                }
                else if (oreSlotItem.count == 100)
                {
                    time = 20;
                }

                return true;
            }
            else
            {
                Debug.Log("Not enough fuel");
                return false;
            }
        }
        else
        {
            Debug.Log("Invalid items for smelting");
            return false;
        }
    }

    IEnumerator Smelt()
    {
        InventoryItem fuelSlotItem = fuelSlot.GetComponentInChildren<InventoryItem>();
        InventoryItem oreSlotItem = oreSlot.GetComponentInChildren<InventoryItem>();
        if (fuelSlotItem == null || oreSlotItem == null)
        {
            Debug.Log("No fuel or ore to smelt");
            yield break;
        }

        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(time / 5);

            if (fuelSlotItem != null)
            {
                fuelSlotItem.count--;
                if (fuelSlotItem.count <= 0)
                {
                    Destroy(fuelSlotItem.gameObject);
                    fuelSlotItem = null;
                }
                else
                {
                    fuelSlotItem.RefreshCount();
                }
            }
            if (oreSlotItem != null)
            {
                oreSlotItem.count--;
                if (oreSlotItem.count <= 0)
                {
                    Destroy(oreSlotItem.gameObject);
                    oreSlotItem = null;
                }
                else
                {
                    oreSlotItem.RefreshCount();
                }
            }

            InventoryItem productItem = productSlot.GetComponentInChildren<InventoryItem>();
            if (productItem == null)
            {
                inventoryManager.SpawnNewItem(currentProductItem, productSlot);
                productItem = productSlot.GetComponentInChildren<InventoryItem>();
            }
            else
            {
                productItem.count += productAmount;
                productItem.RefreshCount();
            }
        }
    }

    public void CreateNewObject()
    {
        StartSmelting();
    }
}
