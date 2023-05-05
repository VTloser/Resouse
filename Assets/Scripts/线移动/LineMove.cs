using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineMove : MonoBehaviour
{
    public GameObject[] Line_Pos;
    private Vector3 Old_Pos;

    void Start()
    {
        Old_Pos = Line_Pos[Line_Pos.Length - 1].transform.position;
    }

    void Update()
    {
        if (Vector3.Distance(Line_Pos[5].transform.position, Old_Pos) != 0)
        {
            float x = Line_Pos[5].transform.position.x - Old_Pos.x;
            float y = Line_Pos[5].transform.position.y - Old_Pos.y;
            float z = Line_Pos[5].transform.position.z - Old_Pos.z;

            for (float i = 1; i < Line_Pos.Length - 1; i++)
            {
                Line_Pos[(int)i].transform.position = new Vector3(Line_Pos[(int)i].transform.position.x + (x) * i / (Line_Pos.Length - 1), Line_Pos[(int)i].transform.position.y + (y) * i / (Line_Pos.Length - 1), Line_Pos[(int)i].transform.position.z + (z) * i / (Line_Pos.Length - 1));
            }
            //Line_Pos[4].transform.position = new Vector3(Line_Pos[4].transform.position.x + (x) * 0.8f, Line_Pos[4].transform.position.y + (y) * 0.8f, Line_Pos[4].transform.position.z + (z) * 0.8f);
            //Line_Pos[3].transform.position = new Vector3(Line_Pos[3].transform.position.x + (x) * 0.6f, Line_Pos[3].transform.position.y + (y) * 0.6f, Line_Pos[3].transform.position.z + (z) * 0.6f);
            //Line_Pos[2].transform.position = new Vector3(Line_Pos[2].transform.position.x + (x) * 0.4f, Line_Pos[2].transform.position.y + (y) * 0.4f, Line_Pos[2].transform.position.z + (z) * 0.4f);
            //Line_Pos[1].transform.position = new Vector3(Line_Pos[1].transform.position.x + (x) * 0.2f, Line_Pos[1].transform.position.y + (y) * 0.2f, Line_Pos[1].transform.position.z + (z) * 0.2f);
        }
        Old_Pos = Line_Pos[5].transform.position;
    }
}
