using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Geomatric_VisualRecognision_Controller : MonoBehaviour
{
    public AnimatedCharacterView animatedCharacter;
    public List<MiniGame> availableMiniGames;
    public RectTransform videoPlayerRoot;
    public RectTransform gameViewRoot;
    public RectTransform gameView;
    private string videoURL;
    private string recognisionObject;
    private string textToSpeach;
    private YoutubePlayer youtubePlayer;
    private void Start()
    {
        recognisionObject = GameManager.instance.selectedMiniGameDetails.AlphabetToPlay;
        videoURL = GameManager.instance.selectedMiniGameDetails.VideoToPlay;
        Initialize();
    }
    MiniGame SelectMiniGame()
    {
        int randomSelect = Random.Range(0, availableMiniGames.Count);
        return availableMiniGames[randomSelect];
    }
    private void Initialize()
    {
        VideoPreview();

        //InitializedMiniGame();

    }
    private void InitializedMiniGame()
    {
        gameView.gameObject.SetActive(true);
        var miniGame = SelectMiniGame();
        GameObject g = GameObject.Instantiate(miniGame.gameObject, gameViewRoot);
        g.GetComponent<MiniGame>().MiniGameSettings = new MiniGameSetting(recognisionObject);
        g.GetComponent<MiniGame>().InitializeGame();
    }
    private void VideoPreview()
    {
        youtubePlayer = GameObject.FindObjectOfType<YoutubePlayer>();
        youtubePlayer.PreLoadVideo(videoURL);
        youtubePlayer.OnVideoReadyToStart.RemoveAllListeners();
        youtubePlayer.OnVideoReadyToStart.AddListener(() => {
            youtubePlayer.Play();
        });
        youtubePlayer.OnVideoFinished.RemoveAllListeners();
        youtubePlayer.OnVideoFinished.AddListener(() => {
            videoPlayerRoot.gameObject.SetActive(false);
            InitializedMiniGame();
        });
    }
    private void AnimatedCharacter()
    {
        textToSpeach = "This is " + recognisionObject;
        videoPlayerRoot.gameObject.SetActive(false);
        animatedCharacter.Initialize($"Fruits/{recognisionObject}", textToSpeach);
        animatedCharacter.OnAnimationFinished.RemoveAllListeners();
        animatedCharacter.OnAnimationFinished.AddListener(() =>
        {
            InitializedMiniGame();
        });
    }
    public void SkipVideo()
    {
        youtubePlayer.Stop();
        youtubePlayer.OnVideoFinished.Invoke();
        FindObjectOfType<AudioManager>().Play("click");
        //youtubePlayer.loadingContent.SetActive(false);
    }
    public void LoadScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync("Library_Scene");
    }

    public void EnableVideoPanel()
    {
        videoPlayerRoot.gameObject.SetActive(true);
    }
}
