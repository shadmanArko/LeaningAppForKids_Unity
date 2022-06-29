using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using UnityEngine.SceneManagement;
public class MenuBookSelectionItem_Controller : MonoBehaviour
{
    public GameObject bookButtonPrefab;
    public RectTransform bookButtonPrefabRoot;
    public TextMeshProUGUI titleText;
    [SerializeField]
    private string title;
    string path;
    // Start is called before the first frame update
    void Start()
    {
#if UNITY_IPHONE
        path = Application.dataPath + "/Raw";
#endif

#if UNITY_ANDROID
        path = "jar:file://" + Application.dataPath + "/Resources/Books";
#endif

#if UNITY_STANDALONE || UNITY_EDITOR
        path = Application.dataPath + "/Resources/Books";
#endif
        string AssetsFolderPath = Application.dataPath;
        string destinationPath = AssetsFolderPath + "/Resources/Books";

        Debug.Log("dataPath : " + destinationPath);

        titleText.text = title;

        string bookPath = $"{destinationPath}/{title}";

        if (!Directory.Exists(bookPath)) return;
        DirectoryInfo directoryInfos = new DirectoryInfo(bookPath);
        FileInfo[] allBooks = directoryInfos.GetFiles();

        foreach (var book in allBooks)
        {
            GameObject g = GameObject.Instantiate(bookButtonPrefab, bookButtonPrefabRoot);
            string bookFolderName = Path.GetFileNameWithoutExtension(book.FullName);
            g.GetComponentInChildren<TextMeshProUGUI>().text = bookFolderName;
            g.GetComponentInChildren<Image>().sprite = Resources.Load<Sprite>($"Books/{title}/{bookFolderName}/1") as Sprite;
            g.GetComponent<Button>().onClick.AddListener(() =>
            {
                GameManager.instance.selectedbookReaderDetails = new BookReaderDetails(bookFolderName, title);
                FindObjectOfType<AudioManager>().Play("click");
                FindObjectOfType<AudioManager>().Stop("theme");
                SceneManager.LoadScene("BookReader");
            });
        }
    }
}
