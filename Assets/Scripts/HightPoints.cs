using UnityEngine;
using System.Collections;

public class HightPoints : MonoBehaviour {

    public float maxDifference = 5f;

    private float tallestHight;
    private float lowestHight;
    private float greatestDistance;

    void Update()
    {
        LayerMask GroundOnly = BuildMenu.Instance.GroundOnly;

        if(transform.FindChild("HightPoints"))
        {
            GameObject HightPointsObj = transform.FindChild("HightPoints").gameObject as GameObject;
            int hightPoints = HightPointsObj.transform.GetChildCount();

            float[] heights = new float[hightPoints];

            for(int i = 0; i < hightPoints; i++)
            {
                GameObject point = HightPointsObj.transform.GetChild(i).gameObject as GameObject;

                RaycastHit hit;
                if(Physics.Raycast(point.transform.position, Vector3.down, out hit, Mathf.Infinity, GroundOnly))
                {
                    heights[i] = hit.point.y;
                }
            }

            tallestHight = 0f;
            lowestHight = 100f;

            for(int j = 0; j < hightPoints; j++)
            {
                if (heights[j] > tallestHight)
                    tallestHight = heights[j];

                if (heights[j] < lowestHight)
                    lowestHight = heights[j];
            }

            greatestDistance = tallestHight - lowestHight;

            if(greatestDistance > maxDifference)
            {
                BuildMenu.canBuildUnit = false;
                Debug.Log("Can not Build");
            }
            else
            {
                BuildMenu.canBuildUnit = true;
                Debug.Log("Can Build");
            }

        }
        else
        {
            BuildMenu.canBuildUnit = true;
        }
    }

}
