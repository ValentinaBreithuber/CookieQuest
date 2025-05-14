using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
    using TMPro;

public class CookieManager : MonoBehaviour
{
    public int cookieCount;
    public TMP_Text cookieText;
    public GameManager gManag;

    void Awake() {
    }
    void Update()
    {
        cookieText.text = "Points: "+gManag.cookieCount.ToString() + "/7";
    }
}
