using NUnit.Framework;
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

    public GameObject searchBar;
    public GameObject[] itemList;
    public GameObject inventory;

    public int itemTotal;


    // Start is called before the first frame update
    void Start()
    {

        DefineItems();
        InitialiseFullItemList();
        InitialiseLinSearch();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //********************************
    //******** defining items ********
    //********************************

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

    //********************************
    //******** sorting items *********
    //********************************

    //******** bubble sort ********

    public void SortBubNameButton()
    {
        Debug.Log("start: " + Time.time * 1000);
        inventoryItemList = BubbleName(inventoryItemList);
        Debug.Log("end: " + Time.time * 1000);

        InitialiseInventoryItemList();
        
    }

    public void SortBubWeightButton()
    {
        Debug.Log("start: " + Time.time * 1000);
        inventoryItemList = BubbleWeight(inventoryItemList);
        Debug.Log("end: " + Time.time * 1000);

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
                    list[i] = list[i + 1];
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
                    list[i] = list[i + 1];
                    list[i + 1] = temp;
                    swapped = true;

                }

            }

        }

        return list;

    }

    //******** merge sort ********

    public void SortMergWeightButton()
    {

        inventoryItemList = MergeWeight(inventoryItemList);

        InitialiseInventoryItemList();

    }

    private List<Item> MergeWeight(List<Item> inventoryItemList)
    {
        throw new NotImplementedException();
    }

    public void SortMergNameButton()
    {

        inventoryItemList = MergeName(inventoryItemList);

        InitialiseInventoryItemList();

    }

    private List<Item> MergeName(List<Item> inventoryItemList)
    {
        throw new NotImplementedException();
    }

    public List<Item> SortWeight(List<Item> unsorted)
    {

        if(unsorted.Count <= 1)
        {
            return unsorted;
        }

        var left = new List<Item>();
        var right = new List<Item>();
        var middle = unsorted.Count / 2;

        for (var i = 0; i < middle; i++)
        {
            left.Add(unsorted[i]);
        }

        for (var i = 0; i < unsorted.Count; i++)
        {
            right.Add(unsorted[i]);
        }

        left = SortWeight(left);
        right = SortWeight(right);

        return MergeWeight(left, right);

    }

    public List<Item> MergeWeight(List<Item> left, List<Item> right)
    {

        var result = new List<Item>();

        while (left.Count > 0 || right.Count > 0)
        {

            if (left.Count > 0 && right.Count > 0)
            {

                if (left[0].Weight < right[0].Weight)
                {
                    result.Add(left[0]);
                    left.Remove(left[0]);
                }
                else
                {
                    result.Add(right[0]);
                    right.Remove(right[0]);
                }
            }
            else if (left.Count > 0)
            {
                result.Add(left[0]);
                result.Remove(left[0]);
            }
            else if (right.Count > 0)
            {
                result.Add(right[0]);
                result.Remove(right[0]);
            }
        }
        return result;
        
    }

    public List<Item> SortName(List<Item> unsorted)
    {
        if (unsorted.Count <= 1)
        {
            return unsorted;
        }

        var left = new List<Item>();
        var right = new List<Item>();
        var middle = unsorted.Count / 2;

        for (var i = 0; i < middle; i++)
        {
            left.Add(unsorted[i]);
        }

        for (var i = 0; i < unsorted.Count; i++)
        {
            right.Add(unsorted[i]);
        }

        left = SortName(left);
        right = SortName(right);

        return MergeName(left, right);
    }

    public List<Item> MergeName(List<Item> left, List<Item> right)
    {
        var result = new List<Item>();

        while (left.Count > 0 || right.Count > 0)
        {

            if (left.Count > 0 && right.Count > 0)
            {

                if (string.Compare(left[0].Name, left[0 + 1].Name) > 0)
                {
                    result.Add(left[0]);
                    left.Remove(left[0]);
                }
                else
                {
                    result.Add(right[0]);
                    right.Remove(right[0]);
                }
            }
            else if (left.Count > 0)
            {
                result.Add(left[0]);
                result.Remove(left[0]);
            }
            else if (right.Count > 0)
            {
                result.Add(right[0]);
                result.Remove(right[0]);
            }
        }
        return result;
    }

    //*********************************
    //******** searching items ********
    //*********************************

    void InitialiseLinSearch()
    {
        itemTotal = inventory.transform.childCount;

        itemList = new GameObject[itemTotal];

        for (int i = 0; i < itemTotal; i++)
        {
            itemList[i] = inventory.transform.GetChild(i).gameObject;
        }
    }

    public void LinButton()
    {
        
        InitialiseInventoryItemList();

        
        string searchText = searchBar.GetComponent<TMP_InputField>().text;
        int searchLength = searchText.Length;

        int searchedItems = 0;

        IList list = inventoryItemList;
        for (int i1 = 0; i1 < list.Count; i1++)
        {
            GameObject i = (GameObject)list[i1];
            searchedItems += 1;

            if (i.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Length >= searchLength)
            {
                if (searchText.ToLower() == i.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Substring(0, searchLength).ToLower())
                {
                    i.SetActive(true);
                }
                else
                {
                    i.SetActive(false);
                }
            }
        }
        

    }

    public void BinButton()
    {

    }

    /*
    public static int Linear(int[] inventory, int value)
    {

        for (int i = 0; i < inventory.Length; i++)
        {

            if (inventory[i] == value)
            {

                return i;

            }
        }

        return -1;
    }
    */

    void ClearInventoryItemList()
    {
        foreach (Transform child in inventoryTransform)
        {
            GameObject.Destroy(child.gameObject);
           
        }
        
    }

    public void ClearButton()
    {
        foreach (Transform child in inventoryTransform)
        {
            GameObject.Destroy(child.gameObject);

        }
        inventoryItemList.Clear();
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
