using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomizedObjectList
{
    public List<string> GetRandomizedItem(List<string> itemList, int correctAnswers, int totalAnswer, string recognitionString)
    {
        List<string> selectedItem = new List<string>();
        for (int i = 0; i < correctAnswers; i++)
        {
            selectedItem.Add(recognitionString);
        }
        for (int i = correctAnswers; i < totalAnswer; i++)
        {
            int randomSelection = UnityEngine.Random.Range(0, itemList.Count);
            string selectedCharacter = itemList[randomSelection];
            selectedItem.Add(selectedCharacter);
        }
        var random = new System.Random();
        var randomized = selectedItem.OrderBy(item => random.Next());
        selectedItem = randomized.ToList();
        return selectedItem;
    }
}
