using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

public class UIDrog : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    public bool BeConrol;
    public Axis_Control control = new Axis_Control();
    public GameObject[] Add;
    public GameObject[] Sub;
    public GameObject Mid;
    public GameObject[] Side;
    float X_Current;
    float Y_Current;

    public float Screen_BuChang = 1920.0f / Screen.width;
    public void OnBeginDrag(PointerEventData eventData)
    {
        Screen_BuChang = 1920.0f / Screen.width;
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (Input.mousePosition.x <= Screen.width && Input.mousePosition.y <= Screen.height && Input.mousePosition.x >= 0 && Input.mousePosition.x >= 0)
        {
            Debug.Log(Input.mousePosition.x);
            if (BeConrol)
            {
                switch (control)
                {
                    case Axis_Control.X:
                        this.transform.position = new Vector3(Input.mousePosition.x, this.transform.position.y, this.transform.position.z);
                        Mid.transform.position = new Vector3(Input.mousePosition.x, Mid.transform.position.y, Mid.transform.position.z);
                        X_Current = this.transform.position.x;
                        Add.ToList().ForEach(_ => { _.GetComponent<RectTransform>().sizeDelta = new Vector2(X_Current * Screen_BuChang, _.GetComponent<RectTransform>().sizeDelta.y); });
                        Sub.ToList().ForEach(_ => { _.GetComponent<RectTransform>().sizeDelta = new Vector2((Screen.width - X_Current) * Screen_BuChang, _.GetComponent<RectTransform>().sizeDelta.y); });
                        break;
                    case Axis_Control.Y:
                        this.transform.position = new Vector3(this.transform.position.x, Input.mousePosition.y, this.transform.position.z);
                        Mid.transform.position = new Vector3(Mid.transform.position.x, Input.mousePosition.y, Mid.transform.position.z);
                        Y_Current = this.transform.position.y;
                        Add.ToList().ForEach(_ => { _.GetComponent<RectTransform>().sizeDelta = new Vector2(_.GetComponent<RectTransform>().sizeDelta.x, Y_Current * Screen_BuChang); });
                        Sub.ToList().ForEach(_ => { _.GetComponent<RectTransform>().sizeDelta = new Vector2(_.GetComponent<RectTransform>().sizeDelta.x, (Screen.height - Y_Current) * Screen_BuChang); });
                        break;
                    default:
                        break;
                }
            }
            else
            {
                this.transform.position = Input.mousePosition;
                X_Current = this.transform.position.x;
                Y_Current = this.transform.position.y;
                Add[0].GetComponent<RectTransform>().sizeDelta = new Vector2((Screen.width - X_Current) * Screen_BuChang, (Screen.height - Y_Current) * Screen_BuChang);
                Add[1].GetComponent<RectTransform>().sizeDelta = new Vector2(X_Current * Screen_BuChang, (Screen.height - Y_Current) * Screen_BuChang);
                Add[2].GetComponent<RectTransform>().sizeDelta = new Vector2((Screen.width - X_Current) * Screen_BuChang, Y_Current * Screen_BuChang);
                Add[3].GetComponent<RectTransform>().sizeDelta = new Vector2(X_Current * Screen_BuChang, Y_Current * Screen_BuChang);
                Side[0].transform.position = new Vector3(Input.mousePosition.x, Side[0].transform.position.y, Side[0].transform.position.z);
                Side[1].transform.position = new Vector3(Side[1].transform.position.x, Input.mousePosition.y, Side[1].transform.position.z);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        // Debug.Log("=======");
    }
}

public enum Axis_Control
{
    X,
    Y
}