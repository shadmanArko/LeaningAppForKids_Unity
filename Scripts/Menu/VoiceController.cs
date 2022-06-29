using System.Collections.Generic;
using UnityEngine;
using Crosstales.RTVoice;
using Crosstales.RTVoice.Model;
using Crosstales.RTVoice.Model.Enum;

public class VoiceController : MonoBehaviour
{
    public static VoiceController instance;

    [SerializeField] string voiceName;

    public VoiceAlias voices;
    [Range(0f, 3f)]
    public float rate = 1f;
    [Range(0f, 2f)]
    public float pitch = 1f;
    [Range(0f, 1f)]
    public float volume = 1f;
    [Tooltip("Fallback gender for the text.")] public Gender gender = Gender.UNKNOWN;

    public List<string> voiceNameDropOptions = new List<string>();
    Crosstales.RTVoice.Tool.SpeechText speechText;
    public Voice Voice
    {
        get
        {
            Voice result = Speaker.Instance.VoiceForName(voiceName) ?? Speaker.Instance.VoiceForGender(gender, "en");

            return result;
        }
    }
    public string VoiceName
    {
        get => voiceName;

    }
    /// <summary>Speech rate of the speaker in percent (range: 0-3).</summary>
    public float Rate
    {
        get => rate;
        set => rate = Mathf.Clamp(value, 0, 3);
    }

    /// <summary>Speech pitch of the speaker in percent (range: 0-2).</summary>
    public float Pitch
    {
        get => pitch;
        set => pitch = Mathf.Clamp(value, 0, 2);
    }

    /// <summary>Volume of the speaker in percent (range: 0-1).</summary>
    public float Volume
    {
        get => volume;
        set => volume = Mathf.Clamp01(value);
    }

    private void Start()
    {
        if (instance != null)
        {
            DestroyImmediate(this.gameObject);
            return;
        }
            

        instance = this;
        DontDestroyOnLoad(this.gameObject);
        
    }
    public void VoiceNamePick(int dropdown) => voiceName = voiceNameDropOptions[dropdown];
    public void Speak(string textToSpeech) => Speaker.Instance.Speak(textToSpeech, null, Voice, true, rate, pitch, volume);
    public void GenderPick(int dropdown)
    {
        gender = dropdown switch
        {
            1 => Gender.MALE,
            2 => Gender.FEMALE,
            _ => Gender.UNKNOWN,
        };
    }   
}
