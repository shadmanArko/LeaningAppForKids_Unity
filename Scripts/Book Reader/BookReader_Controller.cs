using UnityEngine;
using UnityEngine.SceneManagement;
public class BookReader_Controller : MonoBehaviour
{
    public Book book;
    private string bookPath;
    private const string destinationPath = "Assets/Resources/Books";
    private void Start()
    {

        BookReaderDetails bookDetails = GameManager.instance.selectedbookReaderDetails;

        Sprite[] pagesCount = Resources.LoadAll<Sprite>($"Books/{bookDetails.bookCategoryName}/{bookDetails.bookName}");
        book.bookPages = new Sprite[pagesCount.Length];
        for (int i = 0; i < pagesCount.Length; i++)
        {
            Sprite page = Resources.Load<Sprite>($"Books/{bookDetails.bookCategoryName}/{bookDetails.bookName}/{i + 1}");
            book.bookPages[i] = page;
        }
        //book.bookPages = new Sprite[] { };
        // book.bookPages = pages;
        book.gameObject.SetActive(true);


    }

    public void BackButton()
    {
        FindObjectOfType<AudioManager>().Play("click");
        FindObjectOfType<AudioManager>().Play("theme");
        SceneManager.LoadSceneAsync("Library_Scene");
    }
}
