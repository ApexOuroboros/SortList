using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BinarySearch : MonoBehaviour
{
    
    public static int Binary(int[] inventory, int value)
    {

        int min = 0;
        int max = inventory.Length - 1;

        while(min <= max)
        {

            int mid = (min + max) / 2;
            if (inventory[mid] == value)
            {

                return mid;

            }

        }

        return -1;
    }

}
