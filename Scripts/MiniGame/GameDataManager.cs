using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance;
    private List<MiniGame> miniGames;
    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }
        else { Destroy(this.gameObject); }
    }

    public void RegisterMiniGame(List<MiniGame> games)
    {

    }
}
