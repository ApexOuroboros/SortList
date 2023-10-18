using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BubbleSort : MonoBehaviour
{
    public static void Bubble(ref int[] array)
    {

        int n = array.Length;
        int temp;

        bool swapped = true;

        while(swapped)
        {

            swapped= false;
            for(int i = 0; i < n - 1; i++)
            {

                if (array[i] > array[i + 1])
                {

                    temp = array[i];
                    array[i] = array[i + 1];
                    array[i + 1] = temp;
                    swapped = true;

                }

            }

        }

    }
}
