using System.Collections.Generic;
using UnityEngine;
public class VisualRecognision_Controller : MonoBehaviour
{
    public List<MiniGame> availableMiniGames;
    
    public RectTransform gameViewRoot;
    
    private string videoURL;
    private string recognisionObject;
    private YoutubePlayer youtubePlayer;


    private void Start()
    {
        youtubePlayer = FindObjectOfType<YoutubePlayer>();
        recognisionObject = GameManager.instance.selectedMiniGameDetails.AlphabetToPlay;
        videoURL = GameManager.instance.selectedMiniGameDetails.VideoToPlay;
        Initialize();
        
    }
    private void Initialize()
    {
        var miniGame = SelectMiniGame();
        GameObject go = Instantiate(miniGame.gameObject, gameViewRoot);
        go.GetComponent<MiniGame>().MiniGameSettings = new MiniGameSetting(recognisionObject, videoURL);
        go.GetComponent<MiniGame>().InitializeGame();
    }

    public void SkipVideo()
    {
        youtubePlayer.Stop();
        youtubePlayer.OnVideoFinished.Invoke();
        FindObjectOfType<AudioManager>().Play("click");
        //youtubePlayer.loadingContent.SetActive(false);
    }
    MiniGame SelectMiniGame()
    {
        int randomSelect = Random.Range(0, availableMiniGames.Count);
        return availableMiniGames[randomSelect];
    }


}
