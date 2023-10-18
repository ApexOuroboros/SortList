using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;
using NUnit.Framework.Interfaces;

public class MergeSort : MonoBehaviour
{
    
    public List<int> Sort(List<int> unsorted)
    {

        if(unsorted.Count <= 1)
        {
            return unsorted;
        }

        var left = new List<int>();
        var right = new List<int>();
        int middle = unsorted.Count /2;

        for(int i = 0; i < unsorted.Count; i++)
        {

            left.Add(unsorted[i]);
        
        }

        for (int i = 0; i < unsorted.Count; i++)
        {

            right.Add(unsorted[i]);

        }

        left = Sort(left);
        right = Sort(right);

        return Merge(left, right);
    }

    public List<int> Merge(List<int> left, List<int> right)
    {

        var result = new List<int>();

        while(left.Count > 0 || right.Count > 0)
        {

            if(left.Count > 0 && right.Count > 0)
            {

                if (left[0] > right[0])
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
            else if(left.Count > 0)
            {

                result.Add(left[0]);

                result.Remove(left[0]);

            }
            else if(right.Count > 0)
            {

                result.Add(right[0]);

                result.Remove(right[0]);

            }

        }


        return result;
    }

}
