using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScreenColor : MonoBehaviour
{
    // Start is called before the first frame update
    public Image D;
    public RawImage SmapleSpace;
    public int x;
    public int y;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            // StartCoroutine(CaptureScreenshot());
            StartCoroutine(Color_Select());
    }
    IEnumerator CaptureScreenshot()
    {
        //只在每一帧渲染完成后才读取屏幕信息
        yield return new WaitForEndOfFrame();

        Texture2D m_texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        // 读取Rect范围内的像素并存入纹理中
        m_texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        // 实际应用纹理
        m_texture.Apply();

        Color color = m_texture.GetPixel((int)Input.mousePosition.x, (int)Input.mousePosition.y);

        D.color = color;
    }
    IEnumerator Color_Select()
    {
        //只在每一帧渲染完成后才读取屏幕信息
        yield return new WaitForEndOfFrame();

        Texture2D m_texture = SmapleSpace.texture as Texture2D;
        // 读取Rect范围内的像素并存入纹理中

        // 读取Rect范围内的像素并存入纹理中
        m_texture.ReadPixels(new Rect(0, 0, m_texture.width, m_texture.height), 0, 0);
        // 实际应用纹理
        m_texture.Apply();

        Color color = m_texture.GetPixel(x, y);
        D.color = color;
    }



}
