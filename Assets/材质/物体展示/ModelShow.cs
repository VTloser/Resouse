using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ySuperman;

public class ModelShow : MonoBehaviour
{
    public GameObject Model_Show;
    public GameObject ShowTag;
    

    private void OnGUI()
    {

        if (GUILayout.Button("改变材质"))
        {
            Model_Show.ChangeAllMaterToTransparency();
        }

        if (GUILayout.Button("改回来"))
        {
            Model_Show.ChangeAllMaterToBack();
        }

        if (GUILayout.Button("展示一个"))
        {
            Model_Show.ChangeAllMaterToTransparency();
            ShowTag.ShowObject();
        }

    }

}
