using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
public class Button_Mgr : MonoBehaviour
{
    public static Button_Mgr Instance;

    public List<Sprite> temp = new List<Sprite>();

    private void Awake()
    {
        Instance = this;
        FindObjectsOfType<Button_Stay>().ToList().ForEach(_ =>
        {
            temp.Add(_.GetComponent<Image>().sprite);
        });
    }


    public void dd()
    {
        for (int i = 0; i < FindObjectsOfType<Button_Stay>().Length; i++)
        {
            FindObjectsOfType<Button_Stay>()[i].GetComponent<Image>().sprite = temp[i];
        }

    }
}
