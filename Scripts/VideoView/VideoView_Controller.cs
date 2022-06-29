using UnityEngine;
using UnityEngine.SceneManagement;

public class VideoView_Controller : MonoBehaviour
{
    YoutubePlayer youtubePlayer;
    private string videoURL;
    // Start is called before the first frame update
    void Start()
    {
        videoURL = GameManager.instance.selectedpreviewVideoDetails.videoToPlay;
        youtubePlayer = GameObject.FindObjectOfType<YoutubePlayer>();
        youtubePlayer.PreLoadVideo(videoURL); youtubePlayer = GameObject.FindObjectOfType<YoutubePlayer>();
        youtubePlayer.PreLoadVideo(videoURL);
        youtubePlayer.OnVideoReadyToStart.RemoveAllListeners();
        youtubePlayer.OnVideoReadyToStart.AddListener(() => {
            youtubePlayer.Play();
        });
    }

    public void BackButton()
    {
        FindObjectOfType<AudioManager>().Play("click");
        FindObjectOfType<AudioManager>().Play("theme");
        SceneManager.LoadSceneAsync("Library_Scene");
    }
}
