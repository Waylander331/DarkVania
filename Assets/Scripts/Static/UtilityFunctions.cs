using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilityFunctions {

   static GameManager gm;

    public static  string LeadingZero(int n)
    {
        return n.ToString().PadLeft(2, '0');
    }

            
}
