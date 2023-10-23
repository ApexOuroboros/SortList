using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SearchManager : MonoBehaviour
{
    public GameObject searchBar;

    public GameObject inventory;

    public GameObject[] itemList;

    public int itemTotal;

    // Start is called before the first frame update
    void Start()
    {

        itemTotal = inventory.transform.childCount;

        itemList = new GameObject[itemTotal];

        for (int i = 0; i < itemTotal; i++)
        {
            itemList[i] = inventory.transform.GetChild(i).gameObject;
        }

    }

    /*
    public static int Linear(int[]items, int totalItems)
    {

        for (int i = 0; i < items.Length; i++)
        {

            if (items[i] == totalItems)
            {

                return i;

            }
        }

        return -1;
    }
    */

    public void Search()
    {

        string searchText = searchBar.GetComponent<TMP_InputField>().text;
        int searchLength = searchText.Length;

        int searchedItems = 0;

        foreach (GameObject i in itemList)
        {
            searchedItems += 1;

            if (i.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Length >= searchLength)
            {
                if (searchText.ToLower() == i.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text.Substring(0,searchLength).ToLower())
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

}
