using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    ItemManagementSystem itemManagementSystem;

    [SerializeField]
    Transform inventoryTransform;

    public void ClearInventoryItemList()
    {
        foreach (Transform child in inventoryTransform)
        {
            GameObject.Destroy(child.gameObject);
        }
        
    }
}
