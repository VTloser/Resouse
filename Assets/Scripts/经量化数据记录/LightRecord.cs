using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightRecord : MonoBehaviour
{
    // 暂时可记录的 Input框
    public InputField[] Input;
    void Start()
    {
        for (int i = 0; i < Input.Length; i++)
        {
            Debug.Log(Input[i].name);
            if (!string.IsNullOrEmpty(PlayerPrefs.GetString(Input[i].name)))
            {
                Input[i].text = PlayerPrefs.GetString(Input[i].name);
            }
            int tmp = i;
            Input[tmp].onValueChanged.AddListener(_ =>
            {
                PlayerPrefs.SetString(Input[tmp].name, Input[tmp].text);
            });
        }
    }

    

}
