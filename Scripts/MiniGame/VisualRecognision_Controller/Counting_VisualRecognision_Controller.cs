using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Counting_VisualRecognision_Controller : MonoBehaviour
{
    public AnimatedCharacterView animatedCharacter;
    public List<MiniGame> availableMiniGames;
    public RectTransform videoPlayerRoot;
    public RectTransform gameViewRoot;
    public RectTransform gameView;
    private string recognisionObject;
    private YoutubePlayer youtubePlayer;
    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        videoPlayerRoot.gameObject.SetActive(false);
        int r = Random.Range(2, 8);
        animatedCharacter.Initialize(r.ToString());
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
    }
    MiniGame SelectMiniGame()
    {
        int randomSelect = Random.Range(0, availableMiniGames.Count);
        return availableMiniGames[randomSelect];
    }
}
