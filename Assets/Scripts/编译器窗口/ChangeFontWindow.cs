/*
 * 作者：魏宇辰
 * 
 * 1、修改所有文字字体 包括Inputfiled
 * 2、修改制定Canvas字体
 * 3、检查所有Canvas是否合理
 * 4、记录当前物体及子物体的开关状态
 * 5、子物体重命名
 * 6、批量修改文字Text
 * 
 * 
 */

#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

namespace Lemon_Tool
{
    public class ChangeFontWindow : EditorWindow
    {
        public GameObject OP;

#region 属性
        [MenuItem("Tools/更换字体")]
        public static void Open()
        {
            EditorWindow.GetWindow(typeof(ChangeFontWindow)); //打开窗口
        }

        /// <summary>  要更换的Canvas   </summary>  /// 
        Canvas Change_Canvas;
        static Canvas toChange_Canvas;

        /// <summary>  要更换的字体  </summary>  /// 
        Font toChange;
        static Font toChangeFont;

        /// <summary>  要更换的字体的类型  </summary>  /// 
        FontStyle toFontStyle;
        static FontStyle toChangeFontStyle;

        /// <summary>  记录要保存的父物体  </summary>  /// 
        GameObject In_Cord;
        static GameObject toIn_Cord;

        /// <summary>  记录错误的字体  </summary>  /// 
        GameObject Error;
        static GameObject toError;

        /// <summary>  记录要重命名的父物体  </summary>  /// 
        GameObject In_ReName;
        static GameObject toReName;

        /// <summary>  记录要重命名的格式  </summary>  /// 
        string A;
        static GameObject _A;

        /// <summary>  记录要改变文字的父节点  </summary>  /// 
        GameObject In_TextReName;
        static GameObject toTextReName;


        /// <summary>  批量添加脚本  </summary>  /// 
        GameObject In_BatchScripy;
        static GameObject toIBatchScripy;


        /// <summary>  解决文本标点符号开头  </summary>  /// 
        GameObject In_punctuation;
        static GameObject toIpunctuation;


        #endregion


        #region GUI
        void OnGUI()
        {

            Change_Canvas = (Canvas)EditorGUILayout.ObjectField("Canvas:", Change_Canvas, typeof(Canvas), true, GUILayout.MinWidth(100f));
            toChange_Canvas = Change_Canvas;

            toChange = (Font)EditorGUILayout.ObjectField("字体:", toChange, typeof(Font), true, GUILayout.MinWidth(100f));
            toChangeFont = toChange;

            toFontStyle = (FontStyle)EditorGUILayout.EnumPopup("字体类型:", toFontStyle, GUILayout.MinWidth(100f));
            toChangeFontStyle = toFontStyle;


            if (GUILayout.Button("检查字体"))
            {
                Check_Text();
            }

            if (GUILayout.Button("检查所有字体"))
            {
                Check_Text_All();
            }

            Error = (GameObject)EditorGUILayout.ObjectField("错误的字体:", Error, typeof(GameObject), true, GUILayout.MinWidth(300f));
            Error = Tmp;

            if (GUILayout.Button("更换"))
            {
                Change_One();
            }
            if (GUILayout.Button("更改所有字体"))
            {
                Change_All();
            }

            GUILayout.Label("\n");
            GUILayout.Label("记录一个物体及其子物体的开关状态");

            In_Cord = (GameObject)EditorGUILayout.ObjectField("记录的父物体:", In_Cord, typeof(GameObject), true, GUILayout.MinWidth(100f));
            toIn_Cord = In_Cord;

            if (GUILayout.Button("记录"))
            {
                Record();
            }
            if (GUILayout.Button("还原"))
            {
                Back();
            }
            GUILayout.Label("\n");
            GUILayout.Label("按照父物体节点，以'_'为分界线，按照层次结构对第一层子物体进行重命名");
            In_ReName = (GameObject)EditorGUILayout.ObjectField("重命名的父物体:", In_ReName, typeof(GameObject), true, GUILayout.MinWidth(100f));
            toReName = In_ReName;
            if (GUILayout.Button("子物体重命名"))
            {
                ReName();
            }
            GUILayout.Label("\n");
            GUILayout.Label("修改Text文字，修改方式为全替换");
            In_TextReName = (GameObject)EditorGUILayout.ObjectField("要更换文字的父节点:", In_TextReName, typeof(GameObject), true, GUILayout.MinWidth(100f));
            toTextReName = In_TextReName;

            if (GUILayout.Button("更改Text文字"))
            {
                Text_ReName();
            }

            GUILayout.Label("\n");
            GUILayout.Label("解决文本标点符号开头问题");
            In_punctuation = (GameObject)EditorGUILayout.ObjectField("要解决标点符号的父节点:", In_punctuation, typeof(GameObject), true, GUILayout.MinWidth(100f));
            toIpunctuation = In_punctuation;

            if (GUILayout.Button("文本标点符号"))
            {
                TExt_Punctuation();
            }
            if (GUILayout.Button("文本标点符号还原为Text"))
            {
                TExt_Punctuation_Assist();
            }

            if (GUILayout.Button("退出"))
            {
                Close();
            }
        }
#endregion


#region 方法


        static GameObject Tmp;
        static int i;

        /// <summary>
        /// 检查所有字体
        /// </summary>
        public void Check_Text_All()
        {
            ///   获取所有的 canvas   ///
            Canvas[] canvas = FindObjectsOfType<Canvas>();
            Tmp = null;
            i = 0;
            for (int i = 0; i < canvas.Length; i++)
            {
                foreach (var item in canvas[i].GetComponentsInChildren<Transform>(true))
                {
                    if (item.GetComponent<Text>() != null)
                    {
                        if (item.GetComponent<Text>().font != toChangeFont)
                        {
                            Debug.LogError("发现不符合要求字,第<color=red>" + i + "</color>个是：" + item.gameObject.name);
                            Tmp = item.gameObject;
                            i++;
                        }
                    }
                }
                if (i != 0)
                {
                    Debug.Log("共发现<color=red>" + i + "</color>个不符合的字体");
                }
                if (Tmp == null)
                {
                    Debug.Log("没有发现不符合要求的字体");
                }
            }
        }

        /// <summary>
        /// 检查字体
        /// </summary>
        public void Check_Text()
        {
            Tmp = null;
            i = 0;
            if (!toChange_Canvas)
            {
                Debug.LogError("请指定Canvas");
            }
            else
            {
                foreach (var item in toChange_Canvas.GetComponentsInChildren<Transform>(true))
                {
                    if (item.GetComponent<Text>() != null)
                    {
                        if (item.GetComponent<Text>().font != toChangeFont)
                        {
                            Debug.Log("发现不符合要求字体：<color=red>" + item.gameObject + "</color");
                            Tmp = item.gameObject;
                            i++;
                        }
                    }
                }
                if (i != 0)
                {
                    Debug.Log("共发现<color=red>" + i + "</color>个不符合的字体");
                }
                if (Tmp == null)
                {
                    Debug.Log("没有发现不符合要求的字体");
                }
            }
        }

        /// <summary>
        /// 更换所有的字体
        /// </summary>
        /// 
        public void Change_All()
        {
            ///   获取所有的 canvas   ///
            Canvas[] canvas = FindObjectsOfType<Canvas>();
            if (canvas.Length == 0 || canvas == null)
            {
                Debug.Log("场景中没有Canvas");
                return;
            }
            for (int k = 0; k < canvas.Length; k++)
            {
                Transform[] tArray = canvas[k].GetComponentsInChildren<Transform>(true);
                for (int i = 0; i < tArray.Length; i++)
                {
                    Text t = tArray[i].GetComponent<Text>();
                    if (t)
                    {
                        Undo.RecordObject(t, t.gameObject.name);
                        t.font = toChangeFont;
                        t.fontStyle = toChangeFontStyle;
                        EditorUtility.SetDirty(t);
                    }
                }
            }
            Debug.Log("全部更换完成");
        }
        /// <summary>
        /// 更换某一个Canvas下的
        /// </summary>
        public void Change_One()
        {
            ///   获取所有的 canvas   ///
            if (!toChange_Canvas)
            {
                Debug.LogError("请选择Canvas");
                return;
            }
            Transform[] tArray = toChange_Canvas.GetComponentsInChildren<Transform>(true);
            for (int i = 0; i < tArray.Length; i++)
            {
                Text t = tArray[i].GetComponent<Text>();
                if (t)
                {
                    //这个很重要，如果没有这个代码，unity是不会察觉到编辑器有改动的，自然设置完后直接切换场景改变是不被保存
                    //的  如果不加这个代码  在做完更改后 自己随便手动修改下场景里物体的状态 在保存就好了 
                    Undo.RecordObject(t, t.gameObject.name);
                    t.font = toChangeFont;
                    t.fontStyle = toChangeFontStyle;
                    //相当于让他刷新下 不然unity显示界面还不知道自己的东西被换掉了  还会呆呆的显示之前的东西
                    EditorUtility.SetDirty(t);
                }
            }
            Debug.Log("<color=red>" + toChange_Canvas.name + "</color>更换完成");
        }

        static List<Transform> tmp_Gam = new List<Transform>();
        static List<bool> tmp_Record = new List<bool>();

        /// <summary>
        /// 记录
        /// </summary>
        public void Record()
        {
            ///清空数据记录
            tmp_Gam.Clear();
            tmp_Record.Clear();

            tmp_Gam = toIn_Cord.GetComponentsInChildren<Transform>(true).ToList();
            // 记录状态
            for (int i = 0; i < tmp_Gam.Count; i++)
            {
                tmp_Record.Add(tmp_Gam[i].gameObject.activeSelf);
            }
            Debug.Log("已记录:<color=red>" + toIn_Cord.name + "</color>");
        }
        /// <summary>
        /// 还原
        /// </summary>
        /// 
        public void Back()
        {
            /// 临时记录的内容
            List<Transform> Tmp_Back = toIn_Cord.GetComponentsInChildren<Transform>(true).ToList();
            int p = 0;

            //还原状态
            for (int i = 0; i < Tmp_Back.Count; i++)
            {
                ///如果临时记录的和原来的一样
                if (Tmp_Back[i] == tmp_Gam[p])
                {
                    tmp_Gam[p].gameObject.SetActive(tmp_Record[p++]);
                }

            }
            Debug.Log("已还原");
        }
        /// <summary>
        /// 子物体重命名
        /// </summary>
        public void ReName()
        {
            for (int i = 0; i < toReName.transform.childCount; i++)
            {
                Undo.RecordObject(toReName.transform.GetChild(i).gameObject, toReName.transform.GetChild(i).gameObject.name);
                toReName.transform.GetChild(i).name = toReName.gameObject.name.Split('_')[0] + '_' + i;
                EditorUtility.SetDirty(toReName.transform.transform.GetChild(i).gameObject);
            }
        }
        int tmp_Count;
        public void Text_ReName()
        {
            tmp_Count = 0;
            Text[] op = toTextReName.transform.GetComponentsInChildren<Text>(true);

            for (int i = 0; i < op.Count(); i++)
            {
                if (op[i].text == " Fail")
                {
                    op[i].text = "   Fail";
                    tmp_Count++;
                    //相当于让他刷新下 不然unity显示界面还不知道自己的东西被换掉了  还会呆呆的显示之前的东西
                    EditorUtility.SetDirty(op[i]);
                }
            }
            Debug.Log("替换完成,共替换<color=red>" + tmp_Count + "</color>处");
        }

        string str = null;
        int FontSize;
        GameObject Temp_Record;
        public void TExt_Punctuation()
        {
            // 方案一 获取到文字，然后替换脚本然后在替换会Text
            //Text[] texts = toIpunctuation.GetComponentsInChildren<Text>(true);
            for (int i = 0; i < toIpunctuation.GetComponentsInChildren<Text>(true).Count(); i++)
            {
                str = toIpunctuation.GetComponentsInChildren<Text>(true)[i].text;
                FontSize = toIpunctuation.GetComponentsInChildren<Text>(true)[i].fontSize;
                Temp_Record = toIpunctuation.GetComponentsInChildren<Text>(true)[i].gameObject;
                DestroyImmediate(toIpunctuation.GetComponentsInChildren<Text>(true)[i]);
                Temp_Record.AddComponent<TextChange>().fontSize = FontSize;
                Temp_Record.GetComponent<TextChange>().text = str;
                str = Temp_Record.gameObject.GetComponent<TextChange>().text;

            }
            Debug.Log("<color=red>共替换" + toIpunctuation.GetComponentsInChildren<Text>(true).Count() + "个</color>");
        }
        public void TExt_Punctuation_Assist()
        {
            List<TextChange> list = toIpunctuation.GetComponentsInChildren<TextChange>(true).ToList();

            for (int i = 0; i < list.Count; i++)
            {
                str = list[i].text;
                FontSize = list[i].fontSize;
                Temp_Record = list[i].gameObject;
                DestroyImmediate(Temp_Record.GetComponent<TextChange>());
                Temp_Record.AddComponent<Text>().fontSize = FontSize;
                Temp_Record.GetComponent<Text>().text = str;
                str = Temp_Record.gameObject.GetComponent<Text>().text;
            }
            Debug.Log("<color=red>共替换回Text" + list.Count + "个</color>");
        }
        #endregion
    }




}
#endif