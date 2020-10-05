using System.Collections;

using UnityEngine;
using SimpleJSON;
using UnityEngine.Networking;
using TMPro;


public class LoadJ : MonoBehaviour
{
    public TMP_InputField inputTxt;
    public TMP_Text countryTxt;
    public TMP_Text continentTxt;
    public TMP_Text cityTxt;
    public TMP_Text latTxt;
    public TMP_Text longTxt;
    public TMP_Text timezoneTxt;
    public TMP_Text currencyTxt;
    public TMP_Text dratioTxt;
    public TMP_Text symbolTxt;

    public void GetJdata()
    {
        StartCoroutine(RequestWebService());
    }

    IEnumerator RequestWebService()
    {
        string getDataUrl = "http://www.geoplugin.net/json.gp?ip=" + inputTxt.text;
        print(getDataUrl);

        using (UnityWebRequest webData = UnityWebRequest.Get(getDataUrl))
        {
            yield return webData.SendWebRequest();
            if (webData.isNetworkError || webData.isHttpError)
            {
                print("---------------- ERROR ----------------");
                print(webData.error);
            }
            else
            {
                if (webData.isDone)
                {
                    JSONNode jsonData = JSON.Parse(System.Text.Encoding.UTF8.GetString(webData.downloadHandler.data));

                    if (jsonData == null)
                    {
                        print("---------------- NO DATA ----------------");
                    }
                    else
                    {
                        print("---------------- JSON DATA ----------------");
                        print("jsonData.Count:" + jsonData.Count);
                        countryTxt.text = jsonData["geoplugin_countryName"];
                        continentTxt.text = jsonData["geoplugin_continentName"];

                        cityTxt.text = jsonData["geoplugin_city"];
                        latTxt.text = jsonData["geoplugin_latitude"];

                        longTxt.text = jsonData["geoplugin_longitude"];
                        timezoneTxt.text = jsonData["geoplugin_timezone"];

                        currencyTxt.text = jsonData["geoplugin_currencyCode"];
                        dratioTxt.text = jsonData["geoplugin_currencyConverter"];

                        symbolTxt.text = jsonData["geoplugin_currencySymbol"];
                        
                    }
                }
            }
        }
    }
}
