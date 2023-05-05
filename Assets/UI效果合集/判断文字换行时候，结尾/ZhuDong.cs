using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using ySuperman;
public class ZhuDong : MonoBehaviour
{
    public Text D;
    private IList<UILineInfo> MExpalinTextLine;
    private System.Text.StringBuilder MExplainText = null;
    private readonly string strRegex = @"(\！|\？|\，|\。|\,|\.|\）|\：|\“|\‘|\、|\；|\+|\-)";



    private void Awake()
    {
        //D.text = "效果跟大嫂说啊大大打算打大打算，";
        //D.BiaoDian();
    }



    public void ZD()
    { 
        StartCoroutine(MClearUpExplainMode(D, D.text)); 
    }
    

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
