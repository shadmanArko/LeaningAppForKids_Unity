using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ParentsSignInController : MonoBehaviour
{
    public TMP_InputField UserNameInput;
    public TMP_InputField PasswordInput;
    public Button ConfirmButton;
    public Sprite ConfirmSprite;

    private HomeMenuManager homeMenuManager;
    // Start is called before the first frame update
    void Start()
    {
        homeMenuManager = HomeMenuManager.Instance;
        //UserNameInput.text = homeMenuManager.storedData.ParentName;
    }

    public void SignInButton()
    {
        bool signIn = UserNameInput.text != string.Empty && PasswordInput.text != string.Empty;
        if (UserNameInput.text != PlayerPrefs.GetString("parentsName")
            && PasswordInput.text != PlayerPrefs.GetString("parentPassword"))
        {
            //return;
        }


        if (signIn)
        {
            PlayerPrefs.SetString("parentsName", UserNameInput.text);
            PlayerPrefs.SetString("parentPassword", PasswordInput.text);
            homeMenuManager.KidsCreatePanel.SetActive(signIn);
            homeMenuManager.storedData.LoginStates = (int)LoginState.kidsCreate;
        }

        

       
    }

    public void ChangeButtonColor()
    {
        ConfirmButton.GetComponent<Image>().sprite = ConfirmSprite;
        ConfirmButton.GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
    }
}
