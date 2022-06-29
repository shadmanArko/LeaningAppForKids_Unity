using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Michsky.UI.ModernUIPack;

public class ItemSelection_Controller : MonoBehaviour
{

    public GameObject buttonPrefab;

    public RectTransform spawnRectTransform;

    public TextMeshProUGUI titleText;

    [SerializeField]
    private MenuSubcatigories menuSubcatigories;
   
    public void Init(string title, List<MenuItemProp> menuItems)
    {
        titleText.text = title;

        SpawnButton(menuItems);
    }


    private void SpawnButton(List<MenuItemProp> item)
    {
        for (int i = 0; i < item.Count; i++)
        {
            GameObject g = Instantiate(buttonPrefab, spawnRectTransform);
            LevelLoader levelLoader =  g.GetComponent<LevelLoader>();
            levelLoader.isText = item[i].ItemImage == null;
            levelLoader.RecognitionImage.sprite = item[i].ItemImage;
            levelLoader.Init(item[i].ItemName, item[i].SceneName, item[i].VideoURL, item[i].RecognitionItemName);
          
        }
    
    }

}
