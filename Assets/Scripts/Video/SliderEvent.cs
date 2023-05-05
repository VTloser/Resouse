using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// 继承 拖拽接口
/// </summary>
public class SliderEvent : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    public VideoControl toPlayVideo;        // 视频播放的脚本

    /// <summary>
    /// 当前的 Slider 比例值转换为当前的视频播放时间
    /// </summary>
    private void SetVideoTimeValueChange()
    {
        toPlayVideo.videoPlayer.time = toPlayVideo.videoTimeSlider.value * toPlayVideo.videoPlayer.clip.length;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        toPlayVideo.CanFllow = false;
        toPlayVideo.videoPlayer.Pause();
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        SetVideoTimeValueChange();
        toPlayVideo.videoPlayer.Play();

        Invoke("GoFllow", 1f);

    }
    void GoFllow()
    {
        toPlayVideo.CanFllow = true;
    }

}
