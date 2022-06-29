using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPicker : MonoBehaviour
{
    public List<Brick> Bricks;
    public List<int> Chances;

    int Choose(List<int> Bricks)
    {

        float total = 0;

        foreach(float elem in Bricks)
        {
            total += elem;
        }


        float randomPoint = Random.value * total;

        for(int i = 0; i < Bricks.Count; i++)
        {
            if(randomPoint < Bricks[i])
            {
                return i;
            }
            else
            {
                randomPoint -= Bricks[i];
            }
        }

        return Bricks.Count - 1;
    }
}
