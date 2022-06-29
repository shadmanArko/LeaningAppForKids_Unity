using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.Events;

public class AnimatedCharacterView : MonoBehaviour
{
    public TextMeshProUGUI recognisionObject;
    public TextMeshProUGUI GameInstruction;

    public Image recognisionColor;
    public Image animatedCharacter;

    public RectTransform InstructionContainer;

    public UnityEvent OnAnimationFinished;

    private Color currentRecognisionColor;
    private Image currentRecognisionImage;

    private string currentRecognisionObject;
    private string textToAnnounce;

    private void Setup()
    {
        currentRecognisionObject = "";
        currentRecognisionColor = Color.white;

        recognisionObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(-800f, 0f);
        recognisionObject.gameObject.SetActive(false);

        recognisionColor.GetComponent<RectTransform>().anchoredPosition = new Vector2(-800f, 0f);
        recognisionColor.gameObject.SetActive(false);

        recognisionColor.GetComponent<RectTransform>().anchoredPosition = new Vector2(-800f, 0f);
        recognisionColor.gameObject.SetActive(false);

        InstructionContainer.anchoredPosition = new Vector2(-2000f, 0f);
        InstructionContainer.gameObject.SetActive(false);

        animatedCharacter.GetComponent<RectTransform>().anchoredPosition = new Vector2(1000f, 0f);
    }
    public void Initialize(string recognisionChar)
    {
        RecognisionCHar(recognisionChar);
        
        AnimateObject(recognisionObject.transform);
    }
    public void Initialize(string recognisionChar, string annoujncedText)
    {
        RecognisionCHar(recognisionChar);
        InstructionContainer.gameObject.SetActive(true);
        AnimateObject(recognisionObject.transform, annoujncedText);
    }
    private void RecognisionCHar(string recognisionChar)
    {
        Setup();
        gameObject.SetActive(true);
        recognisionObject.gameObject.SetActive(true);
        currentRecognisionObject = recognisionChar;
        if (PlayerPrefs.GetString("selectedLanguage") == "English")
        {
            textToAnnounce = recognisionChar;
        }
        else
        {
            textToAnnounce = VoiceData.banglaAlphabetDictionary[recognisionChar];
        }
        recognisionObject.text = recognisionChar;
    }
    private void AnimateObject(Transform objectToAnimate)
    {
        objectToAnimate.GetComponent<RectTransform>().DOAnchorPosX(100f, 1f).OnComplete(() => {
            VoiceController.instance.Speak(textToAnnounce);
            animatedCharacter.GetComponent<RectTransform>().DOAnchorPosX(0, 0.7f).OnComplete(() => {
                AnnounceObject();
            });
        });
    }
    private void AnimateObject(Transform objectToAnimate, string instruction)
    {
        VoiceController.instance.Speak(textToAnnounce);
        objectToAnimate.GetComponent<RectTransform>().DOAnchorPosX(100f, 1f).OnComplete(() => {
            
            VoiceController.instance.Speak("This is " + textToAnnounce);
            animatedCharacter.GetComponent<RectTransform>().DOAnchorPosX(0, 1.5f).OnComplete(() => {
                
                objectToAnimate.GetComponent<RectTransform>().DOAnchorPosX(-800, 2f).OnComplete(() => {
                    
                    animatedCharacter.GetComponent<RectTransform>().DOAnchorPosX(1000, 0.7f).OnComplete(() => {
                        GameInstruction.text = instruction;
                        InstructionContainer.DOAnchorPosX(0, 2f).OnComplete(() => {
                            VoiceController.instance.Speak(instruction);
                            AnnounceObject();
                        });

                    });
                });
            });
        });
    }
    private void AnnounceObject()
    {
        //VoiceController.instance.Speak(textToAnnounce);
        StartCoroutine(FinalizeAnimation());
    }
    IEnumerator FinalizeAnimation()
    {
        yield return new WaitForSeconds(5f);
        OnAnimationFinished?.Invoke();
        gameObject.SetActive(false);
        Setup();
    }
}
