using UnityEngine;
using TMPro;
public class ParentsSignUpController : MonoBehaviour
{
    public TMP_InputField NameInput;
    public TMP_InputField PasswordInput;
    public TMP_InputField ConfirmPassInput;

    public void SignUpButton()
    {
        bool signUp = NameInput.text != string.Empty
            && PasswordInput.text != string.Empty
            && ConfirmPassInput.text != string.Empty;
        if (!signUp || PasswordInput.text != ConfirmPassInput.text) return;

        HomeMenuManager.Instance.ParentSignInPanel.SetActive(true);
        this.gameObject.SetActive(false);
        HomeMenuManager.Instance.storedData.ParentName = NameInput.text;
        HomeMenuManager.Instance.storedData.ParentPassword = PasswordInput.text;
    }
}
