using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class Tools : MonoBehaviour
{
#if UNITY_EDITOR
    [MenuItem("GameObject/Lemon/子物体重命名", priority = 1)]
    static void RenameInit()
    {
        /// <summary>
        /// 当前选中的物体
        /// </summary>
        GameObject[] selectionGameObjects = new GameObject[] { };

        /// <summary>
        /// 根节点
        /// </summary>
        GameObject root = null;

        selectionGameObjects = Selection.gameObjects;
        //只能选中一个物体
        if (selectionGameObjects.Length != 1)
        {
            root = null;
            return;
        }
        root = selectionGameObjects[0];
        ChangeName(root.transform);
    }

    [MenuItem("GameObject/Lemon/更换字体", priority = 0)]
    static void TextInit()
    {
        /// <summary>
        /// 当前选中的物体
        /// </summary>
        GameObject[] selectionGameObjects = new GameObject[] { };

        /// <summary>
        /// 根节点
        /// </summary>
        GameObject root = null;

        selectionGameObjects = Selection.gameObjects;
        //只能选中一个物体
        if (selectionGameObjects.Length != 1)
        {
            root = null;
            return;
        }
        root = selectionGameObjects[0];
        Change_All(root.transform);
    }

    [MenuItem("GameObject/Lemon/按钮添加音效", priority = 2)]
    static void Btn_Sound()
    {
        /// <summary>
        /// 当前选中的物体
        /// </summary>
        GameObject[] selectionGameObjects = new GameObject[] { };

        /// <summary>
        /// 根节点
        /// </summary>
        GameObject root = null;

        selectionGameObjects = Selection.gameObjects;
        //只能选中一个物体
        if (selectionGameObjects.Length != 1)
        {
            root = null;
            return;
        }
        root = selectionGameObjects[0];
        Add_sound(root.transform);
    }

    #region 方式实现

    public static void Add_sound(Transform Tra)
    {
        int CountNumber = 0;
        if (Resources.Load<AudioClip>("Button_Sound"))
        {
            Debug.LogError("没有找到音效,请确定Resource下Button_Sound");
            return;
        }
        GameObject Temp = GameObject.Find("[Button_Sound]") ?? new GameObject("[Button_Sound]");
        AudioSource ADS = Temp.GetComponent<AudioSource>() != null ? Temp.GetComponent<AudioSource>() : Temp.AddComponent<AudioSource>();
        ADS.clip = Resources.Load<AudioClip>("Button_Sound");
        ADS.playOnAwake = false;
        if (Tra.transform.GetComponentsInChildren<Button>().Length == 0)
        {
            Debug.LogError("当前物体下没有按钮，请重新选择");
            return;
        }
        for (int i = 0; i < Tra.transform.GetComponentsInChildren<Button>().Length; i++)
        {
            Undo.RecordObject(Tra.transform.GetComponentsInChildren<Button>()[i].gameObject, Tra.transform.GetComponentsInChildren<Button>()[i].gameObject.name);

            Debug.Log(Tra.transform.GetComponentsInChildren<Button>()[i].name);
            Tra.transform.GetComponentsInChildren<Button>()[i].onClick.AddListener(()=> { DDDD(); });
            CountNumber++;
        }
        Debug.Log("<Color=red>音效添加成功,共影响" + Tra.name + "下" + CountNumber + "个物体</Color>");
    }

    public static void DDDD()
    {
        Debug.Log("嗡嗡嗡");
    }
    /// <summary>
    /// 修改子物体名字
    /// </summary>
    /// <param name="transform"></param>
    public static void ChangeName(Transform transform)
    {
        int CountNumber = 0;
        if (transform.transform.childCount == 0)
        {
            Debug.LogError("当前物体下没有子物体，请重新选择");
            return;
        }
        for (int i = 0; i < transform.transform.childCount; i++)
        {
            Undo.RecordObject(transform.transform.GetChild(i).gameObject, transform.transform.GetChild(i).gameObject.name);
            transform.transform.GetChild(i).name = transform.gameObject.name.Split('_')[0] + '_' + i;
            EditorUtility.SetDirty(transform.transform.GetChild(i).gameObject);
            CountNumber++;
        }
        Debug.Log("<Color=red>重命名成功,共命名" + transform.name + "下" + CountNumber + "个物体</Color>");
    }
    public static void Change_All(Transform transform)
    {

        int CountNumber = 0;
        Font New_font = Resources.Load<Font>("Fonts/simhei");
        if (!New_font)
        {
            Debug.LogError("未找到字体,请重新检查");
            return;
        }

        ///更换当前Canvas下的所有Test///
        Text[] Array = transform.GetComponentsInChildren<Text>(true);
        if (Array.Length == 0)
        {
            Debug.LogError("当前物体下没有字体,请重新选择");
            return;
        }
        for (int i = 0; i < Array.Length; i++)
        {
            Text t = Array[i].GetComponent<Text>();
            if (t)
            {
                Undo.RecordObject(t, t.gameObject.name);
                //需要将字体放在Resources文件下
                t.font = New_font;
                EditorUtility.SetDirty(t);
                CountNumber++;
            }
        }
        Debug.Log("<Color=red>替换成功,共替换" + transform.name + "下" + CountNumber + "处</Color>");
    }
    #endregion
#endif
}
