using System.Collections.Generic;
using UnityEngine;
public class GoodHabit_VisualRecognision_Controller : MonoBehaviour
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
    private void Initialize()
    {
        textToSpeach = "This is " + recognisionObject;
        videoPlayerRoot.gameObject.SetActive(false);
        animatedCharacter.Initialize($"Fruits/{recognisionObject}", textToSpeach);
        animatedCharacter.OnAnimationFinished.RemoveAllListeners();
        animatedCharacter.OnAnimationFinished.AddListener(() =>
        {
            gameView.gameObject.SetActive(true);
            var miniGame = SelectMiniGame();

            GameObject g = GameObject.Instantiate(miniGame.gameObject, gameViewRoot);
            g.GetComponent<MiniGame>().MiniGameSettings = new MiniGameSetting(recognisionObject);
            g.GetComponent<MiniGame>().InitializeGame();

        });
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
