using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberToString : MonoBehaviour {
    
    public static string GetString(float Float) {
        string number = "" + Float;

        if(Float > 1000)
            number = Mathf.Floor(Float / 1000) + "K";

        if (Float > 1000000)
            number = Mathf.Floor(Float / 1000000) + "M";

        if (Float > 1000000000)
            number = Mathf.Floor(Float / 1000000000) + "B";

        return number;
    }

}
