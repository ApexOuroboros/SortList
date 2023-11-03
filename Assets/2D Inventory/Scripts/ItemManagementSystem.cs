using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class ItemManagementSystem : MonoBehaviour
{
    [SerializeField]
    List<Item> fullItemList = new List<Item>();
    [SerializeField]
    List<Item> inventoryItemList = new List<Item>();

    
    [SerializeField]
    GameObject itemPrefab;

    [SerializeField]
    Transform inventoryTransform;

    public TMP_InputField searchBar;

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

    // bubble sort works

    //******* bubble weight ********
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

    public void SortBubWeightButton()
    {
        Debug.Log("start: " + Time.time * 1000);
        inventoryItemList = BubbleWeight(inventoryItemList);
        Debug.Log("end: " + Time.time * 1000);

        InitialiseInventoryItemList();
        
    }

    //******** bubble name ********

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

    public void SortBubNameButton()
    {
        Debug.Log("start: " + Time.time * 1000);
        inventoryItemList = BubbleName(inventoryItemList);
        Debug.Log("end: " + Time.time * 1000);

        InitialiseInventoryItemList();
        
    }

    //******** merge sort ********

    // merge sort does not work

    //******** merge weight ********
    List<Item> SortWeight(List<Item> unsorted)
    {

        if(unsorted.Count <= 1)
        {
            return unsorted;
        }

        var middle = unsorted.Count / 2;
        var left = unsorted.GetRange(0, middle);
        var right = unsorted.GetRange(middle, unsorted.Count - middle);

        left = SortWeight(left);
        right = SortWeight(right);

        return MergeWeight(left, right);

    }

    List<Item> MergeWeight(List<Item> left, List<Item> right)
    {

        var result = new List<Item>();
        int leftItems = 0;
        int rightItems = 0;

        while (leftItems < left.Count && rightItems < right.Count)
        {

            if (left[leftItems].Weight < right[rightItems].Weight)
            {

                result.Add(left[leftItems]);
                leftItems++;

            }
            else
            {
                result.Add(right[rightItems]);
                rightItems++;
            }
            
        }

        while(leftItems < left.Count)
        {
            result.Add(left[leftItems]);
            leftItems++;
        }

        while(rightItems < right.Count)
        {
            result.Add(right[rightItems]);
            rightItems++;
        }

        return result;
        
    }

    public void SortMergWeightButton()
    {
        Debug.Log("start: " + Time.time * 1000);
        inventoryItemList = SortWeight(inventoryItemList);
        Debug.Log("end: " + Time.time * 1000);

        InitialiseInventoryItemList();

    }


    //******** merge name ********

    public List<Item> SortName(List<Item> unsorted)
    {
        if (unsorted.Count <= 1)
        {
            return unsorted;
        }

        var middle = unsorted.Count / 2;
        var left = unsorted.GetRange(0, middle);
        var right = unsorted.GetRange(middle, unsorted.Count - middle);

        left = SortName(left);
        right = SortName(right);

        return MergeName(left, right);

        //string.Compare(left[0].Name, left[0 + 1].Name) > 0
    }

    public List<Item> MergeName(List<Item> left, List<Item> right)
    {
        var result = new List<Item>();
        int leftItems = 0;
        int rightItems = 0;

        while (leftItems < left.Count && rightItems < right.Count)
        {

            if (string.Compare(left[leftItems].Name, left[leftItems + 1].Name) > 0)
            {

                result.Add(left[leftItems]);
                leftItems++;

            }
            else
            {
                result.Add(right[rightItems]);
                rightItems++;
            }

        }

        while (leftItems < left.Count)
        {
            result.Add(left[leftItems]);
            leftItems++;
        }

        while (rightItems < right.Count)
        {
            result.Add(right[rightItems]);
            rightItems++;
        }

        return result;
    }

    public void SortMergNameButton()
    {
        Debug.Log("start: " + Time.time * 1000);
        inventoryItemList = SortName(inventoryItemList);
        Debug.Log("end: " + Time.time * 1000);

        InitialiseInventoryItemList();

    }


    //*********************************
    //******** searching items ********
    //*********************************


    //******** linear search ********

    // linear search works

    public static bool LinearSearch(List<Item> inventory, string value)
    {
        Debug.Log("Linear Search on : " + value);
        
        for (int i = 0; i < inventory.Count; i++)
        {

            if (string.Compare(inventory[i].Name, value) == 0)
            {
                Debug.Log("Linear Search returns : true");
                return true;
            }
        }

        Debug.Log("Linear Search returns : false");
        return false;

    }

    public void LinButton()
    {
        Debug.Log("start: " + Time.time * 1000);
        LinearSearch(inventoryItemList, searchBar.text);
        Debug.Log("end: " + Time.time * 1000);

        InitialiseInventoryItemList();

    }

    //******** binary search ********

    // binary search does not work

    public static bool BinarySearch(List<Item> items, string target)
    {

        Debug.Log("Binary Search on : " + target);

        var left = 0;
        var right = items.Count - 1;

        while(left <= right)
        {
            var mid = (left + right) / 2;

            if(string.Compare(items[mid].Name, target) == 0)
            {
                Debug.Log("Binary Search returns : true");
                return true;
            }
            else if (string.Compare(items[mid].Name, target) < 0)
            {
                left = mid + 1;
            }
            else
            {
                right = mid - 1;
            }

        }
        Debug.Log("Binary Search returns : false");
        return false;
    }
 
    public void BinButton()
    {

        BinarySearch(inventoryItemList, searchBar.text);

        InitialiseInventoryItemList();

    }


    //*********************************
    //******** random items ***********
    //*********************************

    public void RandomButton()
    {
        for(int i = 0; i < 100; i++)
        {

            System.Random random = new System.Random();
            int randomItems = random.Next(0, fullItemList.Count);
            inventoryItemList.Add(fullItemList[randomItems]);

        }

        InitialiseInventoryItemList();

    }

    //*********************************
    //******** clear inventory ********
    //*********************************

    // clear button works

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

    //***********************
    //******** Other ********
    //***********************

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
