using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VertexScript : MonoBehaviour
{
    public int vertDegree;
    public Text vertInfoBox;

    private bool isLocked;
    public Color curColor;
    SpriteRenderer curRend;
    Vector3 textPos;

    // Start is called before the first frame update
    void Start()
    {
        vertDegree = 0;
        isLocked = true;
        curColor = Color.red;
        curRend = this.GetComponent<SpriteRenderer>();
        vertInfoBox.color = Color.black;
    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<SpriteRenderer>().color = curColor;
        vertInfoBox.transform.position = this.gameObject.transform.position;
    }

    void OnMouseDown()
    {
        isLocked = false;
    }

    void OnMouseDrag()
    {
        if (!isLocked)
        {
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
        }
    }

    private void OnMouseUp()
    {
        isLocked = true;
    }

    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            Destroy(this.gameObject);
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            curColor = Color.blue;
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            curColor = Color.white;
        }
        else if (Input.GetKeyDown(KeyCode.Y))
        {
            curColor = Color.yellow;
        }
        else if (Input.GetKeyDown(KeyCode.M))
        {
            curColor = Color.magenta;
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            curColor = Color.cyan;
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            curColor = Color.red;
        }
    }
}
