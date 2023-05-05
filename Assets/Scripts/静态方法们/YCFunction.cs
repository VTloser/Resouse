using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System;

namespace ySuperman
{
    public class YCFunction : MonoBehaviour
    {
        public ToggleGroup PLA;
        public Dropdown PDR;
        private List<float> ZZ = new List<float>();
        private Dictionary<int, char> op = new Dictionary<int, char>();
        private float[] XX;
        private Material Q;

        private void Awake()
        {
            XX = ZZ.ToArray();
            ZZ = XX.ToList();

            ZZ.Add(9); ZZ.Add(1); ZZ.Add(8); ZZ.Add(3); ZZ.Add(7); ZZ.Add(5); ZZ.Add(4); ZZ.Add(6);
            ZZ.Bubble(false);
            ZZ.ForEach(_ => { Debug.Log(_); });

            op.Add(1, 'a'); op.Add(2, 'b'); op.Add(3, 'c'); op.Add(4, 'd');

            XX.Max();
            ZZ.Min();

            Q.SetMaterialRenderingMode(ySuperman.RenderingMode.Opaque);
        }

        private void Update()
        {
            Debug.Log(PLA.Selected());
            Debug.Log(PDR.Drop_Value());

        }
    }

    public static class ySuperman
    {
        #region UI相关内容


        #region ToggleGroup

        /// <summary>
        /// 获取ToggleGroup选择的Toogle
        /// </summary>
        /// <param name="op"></param>
        /// <returns></returns>
        public static GameObject Selected(this ToggleGroup op)
        {
            foreach (var item in op.ActiveToggles())
            {
                if (item.isOn == true)
                    return item.gameObject;
            }
            return null;
        }

        #endregion ToggleGroup


        #region 下拉框当前选择
        /// <summary>
        /// 获取下拉框当前选择的是哪一个
        /// </summary>
        /// <param name="op"></param>
        /// <returns></returns>
        public static string Drop_Value(this Dropdown op)
        {
            return op.GetComponent<Dropdown>().options[op.GetComponent<Dropdown>().value].text;
        }
        #endregion


        #region 检查首行是否是标点符号

        private static IList<UILineInfo> MExpalinTextLine;
        private static System.Text.StringBuilder MExplainText = null;
        private static readonly string strRegex = @"(\！|\？|\，|\。|\,|\.|\）|\：|\“|\‘|\、|\；|\+|\-)";

        public static Text BiaoDian(this Text text)
        {
            MonoBehaviour Mono = text.transform.GetComponent<MonoBehaviour>();
            Mono.StartCoroutine(MClearUpExplainMode(text, text.text));
            return null;
        }
        public static IEnumerator<WaitForSeconds> MClearUpExplainMode(Text _component, string _text)
        {
            _component.text = _text;
            //如果直接执行下边方法的话，那么_component.cachedTextGenerator.lines将会获取的是之前text中的内容，而不是_text的内容，所以需要等待一下
            yield return new WaitForSeconds(0.001f);
            MExpalinTextLine = _component.cachedTextGenerator.lines;
            //需要改变的字符序号
            int mChangeIndex = -1;
            MExplainText = new System.Text.StringBuilder(_component.text);
            for (int i = 1; i < MExpalinTextLine.Count; i++)
            {
                //首位是否有标点
                bool _b = Regex.IsMatch(_component.text[MExpalinTextLine[i].startCharIdx].ToString(), strRegex);
                //bool _B = Regex.IsMatch(_component.text[MExpalinTextLine[i].startCharIdx -1].ToString(), strRegex);
                if (_b)
                {
                    mChangeIndex = MExpalinTextLine[i].startCharIdx - 1;
                    MExplainText.Insert(mChangeIndex, "\n");
                    //MExplainText.Replace(_component.text[MExpalinTextLine[i].startCharIdx].ToString(), "★");
                    //if (_B)
                    //{
                    // mChangeIndex = MExpalinTextLine[i].startCharIdx - 2;
                    // MExplainText.Insert(mChangeIndex, "\n");
                    //}
                    //else
                    //{
                    // mChangeIndex = MExpalinTextLine[i].startCharIdx - 1;
                    // MExplainText.Insert(mChangeIndex, "\n");
                    //}
                    //".(</color>|<color=#\\w{6}>|" + markList + "
                }
            }
            _component.text = MExplainText.ToString();
            //_component.text = _text;
        }



        #endregion


        #endregion


        #region 查找子物体

        private struct TransformClild
        {
            public Transform parent;
            public string goName;
        }

        private static Dictionary<TransformClild, Transform> TranChild = new Dictionary<TransformClild, Transform>();

        /// <summary>
        /// 查找所有子物体
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="goName"></param>
        /// <returns></returns>
        public static Transform XuYiFindChild(this Transform parent, string goName)
        {
            TransformClild ClildItem = new TransformClild();
            ClildItem.parent = parent;
            ClildItem.goName = goName;

            if (TranChild.ContainsKey(ClildItem) && TranChild[ClildItem])
                return TranChild[ClildItem];
            var go = _XuYiFingChild(parent, goName);
            if (go)
            {
                TranChild.Add(ClildItem, go);
            }
            return go;
        }

        public static Transform XuYiNotFiadChild(this Transform parent, string goName)
        {
            return _XuYiFingChild(parent, goName);
        }

        /// <summary>
        /// 查找非自身节点内容
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="goName"></param>
        /// <returns></returns>
        private static Transform _XuYiFingChild(this Transform parent, string goName)
        {
            //超找自身的孩子
            var child = parent.Find(goName);
            if (child != null) return child;
            //如果没有在查找后代中哥有没有这个对象
            for (int i = 0; i < parent.childCount; i++)
            {
                child = parent.GetChild(i);
                var go = _XuYiFingChild(child, goName);
                if (go != null)
                {
                    return go;
                }
            }
            return null;
        }

        #endregion 查找子物体


        #region 字典方法

        /// <summary>    /// 获取字典中的键    /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="K"></typeparam>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static T[] GetKeys<T, K>(this Dictionary<T, K> dic)
        {
            T[] t = new T[dic.Keys.Count];
            dic.Keys.CopyTo(t, 0);
            return t;
        }

        /// <summary>    /// 获取字典中的值    /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="K"></typeparam>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static K[] GetValues<T, K>(this Dictionary<T, K> dic)
        {
            K[] k = new K[dic.Values.Count];
            dic.Values.CopyTo(k, 0);
            return k;
        }

        #endregion 字典方法


        #region 修改轴

        /// <summary>    /// 更改Position中X的值    /// </summary>
        /// <param name="target"></param>
        /// <param name="x"></param>
        /// <param name="isWord"></param>
        public static Transform SetPositionX(this Transform target, float x, bool isWord = false)
        {
            if (isWord)
            {
                target.position = new Vector3(x, target.position.y, target.position.z);
            }
            else
            {
                target.localPosition = new Vector3(x, target.localPosition.y, target.localPosition.z);
            }
            return target;
        }

        /// <summary>    /// 更改Position中Y的值    /// </summary>
        /// <param name="target"></param>
        /// <param name="x"></param>
        /// <param name="isWord"></param>
        public static Transform SetPositionY(this Transform target, float y, bool isWord = false)
        {
            if (isWord)
            {
                target.position = new Vector3(target.position.x, y, target.position.z);
            }
            else
            {
                target.localPosition = new Vector3(target.localPosition.x, y, target.localPosition.z);
            }
            return target;
        }

        /// <summary>    /// 更改Position中Z的值    /// </summary>
        /// <param name="target"></param>
        /// <param name="x"></param>
        /// <param name="isWord"></param>
        public static Transform SetPositionZ(this Transform target, float z, bool isWord = false)
        {
            if (isWord)
            {
                target.position = new Vector3(target.position.x, target.position.y, z);
            }
            else
            {
                target.localPosition = new Vector3(target.localPosition.x, target.localPosition.y, z);
            }
            return target;
        }

        /// <summary>    /// 更改EulerAngles中Z的值    /// </summary>
        /// <param name="target"></param>
        /// <param name="x"></param>
        /// <param name="isWord"></param>
        public static Transform SetEulerAnglesZ(this Transform target, float z, bool isWord = false)
        {
            if (isWord)
            {
                target.eulerAngles = new Vector3(target.eulerAngles.x, target.eulerAngles.y, z);
            }
            else
            {
                target.localEulerAngles = new Vector3(target.localEulerAngles.x, target.localEulerAngles.y, z);
            }
            return target;
        }

        /// <summary>    /// 更改EulerAngles中X的值    /// </summary>
        /// <param name="target"></param>
        /// <param name="x"></param>
        /// <param name="isWord"></param>
        public static Transform SetEulerAnglesX(this Transform target, float x, bool isWord = false)
        {
            if (isWord)
            {
                target.eulerAngles = new Vector3(x, target.eulerAngles.y, target.eulerAngles.z);
            }
            else
            {
                target.localEulerAngles = new Vector3(x, target.localEulerAngles.y, target.localEulerAngles.z);
            }
            return target;
        }

        /// <summary>    /// 更改Position中Y的值    /// </summary>
        /// <param name="target"></param>
        /// <param name="x"></param>
        /// <param name="isWord"></param>
        public static Transform SetEulerAnglesY(this Transform target, float y, bool isWord = false)
        {
            if (isWord)
            {
                target.eulerAngles = new Vector3(target.position.x, y, target.position.z);
            }
            else
            {
                target.localEulerAngles = new Vector3(target.localEulerAngles.x, y, target.localEulerAngles.z);
            }
            return target;
        }

        #endregion 修改轴


        #region 通过数据添加曲线

        public static void Add_Seriey()
        {
            //this.GetComponent<GetCSV_2>().GetCSV_("ZhuDong_Demo.txt");
            //this.SetDelay(2, () =>
            //{
            //    Debug.Log(this.GetComponent<GetCSV_2>().Array[0][0]);

            //    LineChart.GetComponent<LineChart>().AddSerie(SerieType.Line, "Line_A");

            //    for (int i = 1; i < this.GetComponent<GetCSV_2>().Array.Length; i++)
            //    {
            //        if (this.GetComponent<GetCSV_2>().Array[i].Length < 2)
            //        {
            //            continue;
            //        }
            //        LineChart.GetComponent<LineChart>().AddData("Line_A", float.Parse(this.GetComponent<GetCSV_2>().Array[i][0]), float.Parse(this.GetComponent<GetCSV_2>().Array[i][1]));
            //    }
            //});
        }

        #endregion 通过数据添加曲线


        #region 排序算法

        /// <summary>
        ///  冒泡排序  基本有序
        /// </summary>
        /// <param name="op"></param>
        /// <param name="Dir"></param>
        /// <returns></returns>
        public static List<float> Bubble(this List<float> op, bool Dir = true)
        {
            for (int i = 0; i < op.Count; i++)
            {
                var Flag = true;
                for (int j = i + 1; j < op.Count; j++)
                {
                    if (op[i] > op[j])
                    {
                        var Q = op[j];
                        op[j] = op[i];
                        op[i] = Q;
                        Flag = false;
                    }
                }
                if (Flag)
                {
                    return op;
                }
                Debug.Log(op[i]);
            }
            return op;
        }

        #endregion 排序算法


        #region 更换材质球渲染方式

        public enum RenderingMode
        {
            Opaque,
            Cutout,
            Fade,
            Transparent,
        }

        public static void SetMaterialRenderingMode(this Material material, RenderingMode renderingMode)
        {
            switch (renderingMode)
            {
                case RenderingMode.Opaque:
                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                    material.SetInt("_ZWrite", 1);
                    material.DisableKeyword("_ALPHATEST_ON");
                    material.DisableKeyword("_ALPHABLEND_ON");
                    material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    material.renderQueue = -1;
                    break;

                case RenderingMode.Cutout:
                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                    material.SetInt("_ZWrite", 1);
                    material.EnableKeyword("_ALPHATEST_ON");
                    material.DisableKeyword("_ALPHABLEND_ON");
                    material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    material.renderQueue = 2450;
                    break;

                case RenderingMode.Fade:
                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    material.SetInt("_ZWrite", 0);
                    material.DisableKeyword("_ALPHATEST_ON");
                    material.EnableKeyword("_ALPHABLEND_ON");
                    material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    material.renderQueue = 3000;
                    break;

                case RenderingMode.Transparent:
                    material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                    material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    material.SetInt("_ZWrite", 0);
                    material.DisableKeyword("_ALPHATEST_ON");
                    material.DisableKeyword("_ALPHABLEND_ON");
                    material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                    material.renderQueue = 3000;
                    break;
            }
        }

        // 将改物体下的所有物体的材质球改成半透明
        public static void ChangeAllMaterToTransparency(this GameObject gameObject)
        {
            foreach (var item in gameObject.GetComponentsInChildren<MeshRenderer>(true))
            {
                foreach (var it in item.materials)
                {
                    Color A = it.GetColor("_Color");
                    it.SetMaterialRenderingMode(RenderingMode.Fade);

                    //it.SetColor("_Color", new Color(A.r, A.b, A.b, 255 / 255f));  //笑死 精度丢失会导致颜色出现问题 直接改变颜色Alpha

                    A.a = 0.35f;
                    it.SetColor("_Color", A);
                }
            }
        }

         // 将改物体下的所有物体的材质球还原成原来的模样
        public static void ChangeAllMaterToBack(this GameObject gameObject)
        {
            foreach (var item in gameObject.GetComponentsInChildren<MeshRenderer>(true))
            {
                foreach (var it in item.materials)
                {
                    Color A = it.GetColor("_Color");
                    it.SetMaterialRenderingMode(RenderingMode.Opaque);

                    //it.SetColor("_Color", new Color(A.r, A.b, A.b, 255 / 255f));  //笑死 精度丢失会导致颜色出现问题 直接改变颜色Alpha

                    A.a = 1f;
                    it.SetColor("_Color", A);
                }
            }
        }


        //修改一个物体的材质球变为原样
        public static void ShowObject(this GameObject gameObject)
        {
            foreach (var it in gameObject.GetComponent<MeshRenderer>().materials)
            {
                Color A = it.GetColor("_Color");
                it.SetMaterialRenderingMode(RenderingMode.Opaque);

                //it.SetColor("_Color", new Color(A.r, A.b, A.b, 255 / 255f));  //笑死 精度丢失会导致颜色出现问题 直接改变颜色Alpha

                A.a = 1f;
                it.SetColor("_Color", A);
            }
        }
        #endregion 更换材质球渲染方式


        #region 获取组件
        public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
        {
            if (gameObject.GetComponent<T>() != null)
                return gameObject.GetComponent<T>();
            else
                return gameObject.AddComponent<T>();

            //return gameObject.GetComponent<T>() ?? gameObject.AddComponent<T>();
        }

        public static bool HasComponent<T>(this GameObject gameObject) where T : Component
        {
            return gameObject.GetComponent<T>() != null;
        }

        #endregion


        #region Transfrom
        /// <summary>
        /// Makes the given game objects children of the transform.
        /// </summary>
        /// <param name="transform">Parent transform.</param>
        /// <param name="children">Game objects to make children.</param>
        public static void AddChildren(this Transform transform, GameObject children)
        {
            children.transform.parent = transform;
        }

        /// <summary>
        /// Makes the game objects of given components children of the transform.
        /// </summary>
        /// <param name="transform">Parent transform.</param>
        /// <param name="children">Components of game objects to make children.</param>
        public static void AddChildren(this Transform transform, Component[] children)
        {
            Array.ForEach(children, child => child.transform.parent = transform);
        }

        /// <summary>
        /// Sets the position of a transform's children to zero.
        /// </summary>
        /// <param name="transform">Parent transform.</param>
        /// <param name="recursive">Also reset ancestor positions?</param>
        public static void ResetChildPositions(this Transform transform, bool recursive = false)
        {
            foreach (Transform child in transform)
            {
                child.position = Vector3.zero;
                child.localEulerAngles = Vector3.zero;

                if (recursive)
                {
                    child.ResetChildPositions(recursive);
                }
            }
        }

        /// <summary>
        /// Sets the layer of the transform's children.
        /// </summary>
        /// <param name="transform">Parent transform.</param>
        /// <param name="layerName">Name of layer.</param>
        /// <param name="recursive">Also set ancestor layers?</param>
        public static void SetChildLayers(this Transform transform, string layerName, bool recursive = false)
        {
            var layer = LayerMask.NameToLayer(layerName);
            SetChildLayersHelper(transform, layer, recursive);
        }

        static void SetChildLayersHelper(Transform transform, int layer, bool recursive)
        {
            foreach (Transform child in transform)
            {
                child.gameObject.layer = layer;

                if (recursive)
                {
                    SetChildLayersHelper(child, layer, recursive);
                }
            }
        }

        /// <summary>
        /// Sets the x component of the transform's position.
        /// </summary>
        /// <param name="x">Value of x.</param>
        public static void SetX(this Transform transform, float x)
        {
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }

        /// <summary>
        /// Sets the y component of the transform's position.
        /// </summary>
        /// <param name="y">Value of y.</param>
        public static void SetY(this Transform transform, float y)
        {
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
        }

        /// <summary>
        /// Sets the z component of the transform's position.
        /// </summary>
        /// <param name="z">Value of z.</param>
        public static void SetZ(this Transform transform, float z)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, z);
        }
        #endregion


    }
}