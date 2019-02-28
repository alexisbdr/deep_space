using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameUtils {

    public static string formatLargeNumber(double num)
    {
        string modifier = "";
        if (num > 1000)
        {
            num = System.Math.Floor(num);
            num /= 1000;
            modifier = "K";
        }
        else
        {
            return System.Math.Floor(num).ToString();
        }
        if (num > 1000)
        {
            num = System.Math.Floor(num);
            num /= 1000;
            modifier = "M";
        }
        if (num > 1000)
        {
            num = System.Math.Floor(num);
            num /= 1000;
            modifier = "B";
        }
        if (num > 1000)
        {
            num = System.Math.Floor(num);
            num /= 1000;
            modifier = "T";
        }
        if (num > 1000)
        {
            num = System.Math.Floor(num);
            num /= 1000;
            modifier = "Q";
        }
        if (num.ToString().Length > 4)
        {
            if (num.ToString().Substring(3,1) == ".")
            {
                return num.ToString().Substring(0, 3) + modifier;
            }
            return num.ToString().Substring(0,4) + modifier;
        }
        return num.ToString() + modifier;
    }
}
