using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
public class VideoControl : MonoBehaviour
{
    public Text videoTimeText;          // 视频的当前时间 Text
    public Slider videoTimeSlider;      // 视频的时间 Slider
    public VideoPlayer videoPlayer;

    public Button Stop;        //暂停按钮
    public Button Resume;       //继续按钮
    public Button Restart;       //重新开始按钮

    public Button SlowDown;      //慢放
    public Button FastForward;   //快进

    // 当前视频的总时间值和当前播放时间值的参数
    private int currentHour;
    private int currentMinute;
    private int currentSecond;

    public bool CanFllow = true;


    // Use this for initialization
    void Start()
    {
        //清空文本
        videoTimeText.text = "";

        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        videoPlayer.SetTargetAudioSource(0, this.GetComponent<AudioSource>());
        videoPlayer.playOnAwake = false;
        videoPlayer.IsAudioTrackEnabled(0);

        Stop.onClick.AddListener(() => videoPlayer.Pause());
        Resume.onClick.AddListener(() => videoPlayer.Play());
        Restart.onClick.AddListener(() => { videoPlayer.Stop(); videoPlayer.Play(); videoTimeSlider.value = 0; });

        SlowDown.onClick.AddListener(() => { videoPlayer.playbackSpeed -= 0.2f; });
        FastForward.onClick.AddListener(() => { videoPlayer.playbackSpeed += 0.2f; });


    }

    // Update is called once per frame
    void Update()
    {
        ShowVideoTime();
    }

    /// <summary>
    /// 显示当前视频的时间
    /// </summary>
    private void ShowVideoTime()
    {
        // 当前的视频播放时间
        currentHour   = (int)videoPlayer.time / 3600;
        currentMinute = (int)(videoPlayer.time - currentHour * 3600) / 60;
        currentSecond = (int)(videoPlayer.time - currentHour * 3600 - currentMinute * 60);
        // 把当前视频播放的时间显示在 Text 上
        videoTimeText.text = string.Format("{0:D2}:{1:D2}", currentMinute, currentSecond);
        if (CanFllow)
            videoTimeSlider.value = (float)(videoPlayer.time / videoPlayer.clip.length);


    }

}