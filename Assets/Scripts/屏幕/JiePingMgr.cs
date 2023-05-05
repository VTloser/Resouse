using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using DG.Tweening;
// 截取屏幕


public class JiePingMgr : MonoBehaviour
{
    // Use this for initialization
    public Camera camera;   // 相机
    public Image  image;    //输出到图像
    public int    PicName;  //保存名字

    Texture2D screenShot;

    int width  = 960;
    int height = 540;
    private Rect CutRect = new Rect(0f, 0, 1f, 1f);

    public static JiePingMgr Instance;
    void Start()
    {
        //RenderGoTex();
        Instance = this;
    }
    private void OnGUI()
    {
        if (GUILayout.Button("Cam"))
            CamJieTu();
        if (GUILayout.Button("Tu"))
            BaoCunTuPian();
    }

    [ContextMenu("Cam截图")]
    public void CamJieTu()
    {
        // 创建一个RenderTexture对象  
        RenderTexture rt = new RenderTexture(960, 540, 24);
        // 临时设置相关相机的targetTexture为rt, 并手动渲染相关相机  
        camera.targetTexture = rt;
        camera.Render();
        //ps: --- 如果这样加上第二个相机，可以实现只截图某几个指定的相机一起看到的图像。  
        //ps: camera2.targetTexture = rt;  
        //ps: camera2.Render();  
        //ps: -------------------------------------------------------------------  

        // 激活这个rt, 并从中中读取像素。  

        RenderTexture.active = rt;

        screenShot = new Texture2D(rt.width, rt.height, TextureFormat.RGB24, false);

        screenShot.ReadPixels(new Rect(0, 0, screenShot.width, screenShot.height), 0, 0);// 注：这个时候，它是从RenderTexture.active中读取像素  
        

        // 重置相关参数，以使用camera继续在屏幕上显示
  
        camera.targetTexture = null;
        //ps: camera2.targetTexture = null;  
        RenderTexture.active = null; // JC: added to avoid errors  
        GameObject.Destroy(rt);

        Sprite sprite = Sprite.Create(screenShot, new Rect(0, 0, screenShot.width, screenShot.height),Vector3.zero);

        image.sprite = sprite;
        screenShot.Apply();
        
    } 


   [ContextMenu("截图保存")]
    public void BaoCunTuPian()
    {
        byte[] bytes = screenShot.EncodeToPNG();

        PicName++;
        if (File.Exists(Application.dataPath + "/" + "StreamingAssets/" + PicName.ToString() + ".png"))
        {
            Debug.Log("该图片名称已经存在了，请重新输入");
            return;
        }
        string filename = Application.dataPath + "/StreamingAssets/" + PicName.ToString() + ".png";

        File.WriteAllBytes(filename, bytes);

#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
#endif
    }

   public void OpenTuPian(GameObject obj)
   {
       if (obj.transform.localScale.x > 0.5f)
           obj.transform.DOScale(Vector3.zero, 0.5f);
       else
           obj.transform.DOScale(Vector3.one, 0.5f);
   }
}
