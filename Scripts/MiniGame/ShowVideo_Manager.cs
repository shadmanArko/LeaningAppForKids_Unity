using UnityEngine;

public class ShowVideo_Manager : MonoBehaviour
{
    public static ShowVideo_Manager Instance;
    public RectTransform videoPlayerRoot;
    public RectTransform gameView;
    public AnimatedCharacterView animatedCharacter;
    [HideInInspector] public YoutubePlayer youtubePlayer;




    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(gameObject);
    }

    public void ShowVideo(string videoURL)
    {

        //Invoke( "VideoPanel", 2f);
        youtubePlayer = GameObject.FindObjectOfType<YoutubePlayer>();
        youtubePlayer.PreLoadVideo(videoURL);
        youtubePlayer.OnVideoReadyToStart.RemoveAllListeners();
        youtubePlayer.OnVideoReadyToStart.AddListener(() =>
        {
            youtubePlayer.Play();
        });
    }

    public void ShowAnimation(string recognisionObject = "", string instruction = "")
    {
        //Debug.Log($"recognisionObject = {recognisionObject} && instruction =  {instruction}");
        videoPlayerRoot.gameObject.SetActive(false);
        if (instruction == "")
            animatedCharacter.Initialize(recognisionObject);
        else
            animatedCharacter.Initialize(recognisionObject, instruction);
    }
    public void StartGame() => gameView.gameObject.SetActive(true);

    public void VideoPanel()
    {
        videoPlayerRoot.gameObject.SetActive(true);
    }



}
