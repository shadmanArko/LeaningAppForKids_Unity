using UnityEngine;
public enum LoginState
{
    ParentLogin,
    kidsCreate,
    KidsLogin,
    KidsPage
}
public class StoredData
{
    public const string P_password = "parentPassword";
    public const string P_name = "parentsName";
    private string _parentName;
    public string ParentName
    {
        get
        {
            if (!PlayerPrefs.HasKey("parentsName"))
                PlayerPrefs.SetString("parentsName", "");
            _parentName = PlayerPrefs.GetString("parentsName");
            return _parentName;
        }
        set => PlayerPrefs.SetString(P_name, value);
    }
    private string _parentPassword;
    public string ParentPassword
    {
        get
        {
            if (!PlayerPrefs.HasKey("parentPassword"))
                PlayerPrefs.SetString("parentPassword", "");
            _parentPassword = PlayerPrefs.GetString("parentPassword");
            return _parentPassword;
        }
        set => PlayerPrefs.SetString(P_password, value);
    }
    private int _loginStates;
    public int LoginStates
    {
        get
        {
            if (!PlayerPrefs.HasKey("loginState"))
                PlayerPrefs.SetInt("loginState", (int)LoginState.ParentLogin);
            _loginStates = PlayerPrefs.GetInt("loginState");
            return _loginStates;
        }
        set { PlayerPrefs.SetInt("loginState", value); }
    }
}
