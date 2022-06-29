using UnityEngine;
using System.Collections.Generic;

public class PointedItem_Controller : MonoBehaviour
{
	public List<Vector3> collectedItemPosition = new List<Vector3>();
    int insertObject = 0;
	public void TakePosition(GameObject go)
    {
        insertObject++;
        if (insertObject <= collectedItemPosition.Count)
        {
            go.transform.SetParent(gameObject.transform);
            go.transform.position = collectedItemPosition[insertObject - 1];
            go.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            
        }
    }
}
