using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helper
{
    public static int AddInt(int num1, int num2, int max)
    {
        int result = 0;
        if (num1 + num2 > max)
            result = max;
        else
            result = num1 + num2;
        return result;
    }

}
