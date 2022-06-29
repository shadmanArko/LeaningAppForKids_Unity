using UnityEngine;
public enum Grade
{
    Age2, Age3, Age4, Kindergarten, Grade1, Grade2

}
public abstract class LibraryItemSelection : MonoBehaviour
{
    public abstract Grade grade { get; set; }
    public abstract void SpawnButton();
}
