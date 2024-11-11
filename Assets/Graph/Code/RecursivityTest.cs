using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecursivityTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int accumulator = 0;
        for (int i = 0; i <= 4; i++)
        {
            accumulator++;
        }
        Debug.Log("Acumulation " + accumulator);

        //Recursivity iteration
        Debug.Log("recursivity acumulation" + Accumulation(0));
    }

    protected int  Accumulation(int value)
    {
        if (value <= 4)
        {
            return Accumulation(value + 1);
        }
        else
        {
            return value;
        }
    }

    ////opttion 2
    //protected int Accumulation(int value)
    //{
    //    int result = value;
    //    if (value <= 4)
    //    {
    //        return Accumulation(++value);
    //    }        
    //        return value;        
    //}
}
