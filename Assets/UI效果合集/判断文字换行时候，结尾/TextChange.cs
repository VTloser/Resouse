/*******************************************************************************
* 作者名称：魏宇辰
* 版本 v1.0.0
* 修改记录：解决Unity原生UI无法判断首行是否是标点符号
* 描述：
* 直接使用不支持打字机效果
******************************************************************************/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.Text.RegularExpressions;



public class TextChange : Text
{
    /// <summary>
    /// 用于匹配标点符号（正则表达式）
    /// </summary> 
    //private readonly string strRegex = @"\p{P}"; //启用这个将过滤所有标点，下面的可以选自需要避免的标点
    private readonly string strRegex = @"(\！|\？|\，|\。|\,|\.|\）|\：|\“|\‘|\、|\；|\+|\-)";
    /// <summary>
    /// /// 用于存储text组件中的内容
    /// /// </summary>
    private System.Text.StringBuilder MExplainText = null;
    /// /// <summary>
    /// /// 用于存储text生成器中的内容
    /// /// </summary> 
    /// 
    private IList<UILineInfo> MExpalinTextLine;
    protected override void OnPopulateMesh(VertexHelper toFill)
    {

        base.OnPopulateMesh(toFill);
        StartCoroutine(MClearUpExplainMode(this, text));

    }
    /// <summary> 
    /// 整理文字。确保首字母不出现标点
    /// /// </summary>
    /// /// <param name="_component">text组件</param>
    /// /// <param name="_text">需要填入text中的内容</param>
    /// /// <returns></returns>
    IEnumerator MClearUpExplainMode(Text _component, string _text)
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

}
