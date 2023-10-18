using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LinearSearch : MonoBehaviour
{
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
}
