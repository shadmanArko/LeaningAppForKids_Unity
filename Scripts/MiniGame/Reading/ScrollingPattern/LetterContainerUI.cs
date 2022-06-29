using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public enum Type
{
    alphabet, number
}
public class LetterContainerUI : MonoBehaviour
{
    public RectTransform ScrollingPanel;
    public ScrollRect ScrollRect;
    public RectTransform Center;
    public Button UpButton;
    public Button DownButton;
    public Type types;
    [HideInInspector] public UIVerticalScroller VerticalScroller;
    private int arrayLength;
    private ScrollingPatternController scrollingPattern;
    public void Initialize(GameObject instantiatPrefab)
    {
        arrayLength = types == Type.alphabet ? CharacterData.letters.Count : CharacterData.numericals.Count;
        GameObject[] letterButtons = new GameObject[arrayLength];

        for (int i = 0; i < arrayLength; i++)
        {
            GameObject clone = Instantiate(instantiatPrefab, ScrollingPanel);
            string s = types == Type.alphabet ? CharacterData.letters[i] : CharacterData.numericals[i];
            clone.GetComponentInChildren<TextMeshProUGUI>().text = "" + s;
            clone.name = "Button_" + s;
            clone.AddComponent<CanvasGroup>();
            letterButtons[i] = clone;
        }
        VerticalScroller = new UIVerticalScroller(Center, Center, ScrollRect, letterButtons);
        VerticalScroller.Start();
        scrollingPattern = GameObject.FindObjectOfType<ScrollingPatternController>();
        ScrollRect.onValueChanged.AddListener((val) =>
        {
            scrollingPattern.ScrollUpdate();
        });
        ButtonAction();
    }

    private void ButtonAction()
    {
        UpButton.onClick.AddListener(VerticalScroller.ScrollUp);
        DownButton.onClick.AddListener(VerticalScroller.ScrollDown);

        Debug.Log("dipok button action");
    }

    public void ButtonClick()
    {

       /*VerticalScroller.ScrollUp();
        Debug.Log("dipok button clicked");*/
    }

    public void ScrollUp()
    {
        VerticalScroller.ScrollUp();
    }

    public void ScrollDown()
    {
        VerticalScroller.ScrollDown();
    }
}
