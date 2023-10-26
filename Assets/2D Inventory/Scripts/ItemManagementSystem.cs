using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class ItemManagementSystem : MonoBehaviour
{
    [SerializeField]
    List<Item> fullItemList = new List<Item>();
    [SerializeField]
    List<Item> inventoryItemList = new List<Item>();

    //[SerializeField]
    //List<Item> sortedItemList = new List<Item>();

    [SerializeField]
    GameObject itemPrefab;

    [SerializeField]
    Transform inventoryTransform;


    // Start is called before the first frame update
    void Start()
    {

        DefineItems();
        InitialiseFullItemList();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DefineItems()
    {
        fullItemList.Add(new Item("Axe", 3.0f));
        fullItemList.Add(new Item("Bandage", 0.4f));
        fullItemList.Add(new Item("Crossbow", 4.0f));
        fullItemList.Add(new Item("Dagger", 0.8f));
        fullItemList.Add(new Item("Emerald", 0.2f));
        fullItemList.Add(new Item("Fish", 2.0f));
        fullItemList.Add(new Item("Gems", 0.3f));
        fullItemList.Add(new Item("Hat", 0.6f));
        fullItemList.Add(new Item("Ingot", 5.0f));
        fullItemList.Add(new Item("Junk", 1.2f));
    }

    void InitialiseFullItemList()
    {
        GameObject gameObject;

        for (int i = 0; i < fullItemList.Count; i++)
        {
            gameObject = Instantiate(itemPrefab, transform);
            gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = fullItemList[i].Name;
            gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = fullItemList[i].Weight.ToString();
            gameObject.GetComponent<Button>().AddEventListener(i, ItemClicked);
        }
    }

    void InitialiseInventoryItemList()
    {
        ClearInventoryItemList();
        GameObject gameObject;

        for (int i = 0; i < inventoryItemList.Count; i++)
        {
            gameObject = Instantiate(itemPrefab, inventoryTransform);
            gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = inventoryItemList[i].Name;
            gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = inventoryItemList[i].Weight.ToString();
            gameObject.GetComponent<Button>().AddEventListener(i, InventoryItemClicked);
        }
    }

    //bubble sort

    public void SortBubNameButton()
    {
        Debug.Log("start: " + Time.time * 1000);
        inventoryItemList = BubbleName(inventoryItemList);
        Debug.Log("end: " + Time.time * 1000);
        InitialiseInventoryItemList();
        
    }

    public void SortBubWeightButton()
    {

        inventoryItemList = BubbleWeight(inventoryItemList);
        InitialiseInventoryItemList();
        
    }

    List<Item> BubbleWeight(List<Item> list)
    {
        int n = list.Count;
        Item temp;

        bool swapped = true;

        while (swapped)
        {

            swapped = false;
            for (int i = 0; i < n - 1; i++)
            {

                if (list[i].Weight > list[i + 1].Weight)
                {

                    temp = list[i];
                    list[i].Weight = list[i + 1].Weight;
                    list[i + 1] = temp;
                    swapped = true;

                }

            }

        }

        return list;

    }

    List<Item> BubbleName(List<Item> list)
    {
        int n = list.Count;
        Item temp;

        bool swapped = true;

        while (swapped)
        {

            swapped = false;
            for (int i = 0; i < n - 1; i++)
            {
                
                if (string.Compare(list[i].Name, list[i + 1].Name) > 0)
                {

                    temp = list[i];
                    list[i].Name = list[i + 1].Name;
                    list[i + 1] = temp;
                    swapped = true;

                }

            }

        }

        return list;

    }

    //merge sort

    public void SortMergNameButton()
    {


    }

    public void SortMergWeightButton()
    {


    }



    void ClearInventoryItemList()
    {
        foreach (Transform child in inventoryTransform)
        {
            GameObject.Destroy(child.gameObject);
            
        }
        
    }

    void ItemClicked(int index)
    {
        Debug.Log("Item Cicked: " + index + ". " + fullItemList[index].Name + " (" + fullItemList[index].Weight + ")");
        AddItemToInventory(index);

    }

    void InventoryItemClicked(int index)
    {
        Debug.Log("Item Cicked: " + index + ". " + inventoryItemList[index].Name + " (" + inventoryItemList[index].Weight + ")");
    }

    void AddItemToInventory(int index)
    {
        Item item = new Item(fullItemList[index].Name, fullItemList[index].Weight);
        inventoryItemList.Add(item);

        InitialiseInventoryItemList();
    }

}

public static class ButtonExtension
{
    public static void AddEventListener<T>(this Button button, T param, Action<T> OnClick)
    {
        button.onClick.AddListener(delegate () { OnClick(param); });
    }
}

public class Item
{
    public string Name { get; set; }
    public float Weight { get; set; }

    public Item(string name, float weight)
    {
        Name = name;
        Weight = weight;
    }
}
