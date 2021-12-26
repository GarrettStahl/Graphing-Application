using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphingScript : MonoBehaviour
{
    
    public Text vertBox;
    public Text edgeBox;
    public GameObject VertexPrefab;
    public GameObject edgeDraw;
    public Button helpButton;
    public Text helpBox;
    public Text vertInfoBox;

    private KeyCode[] vertSeq;
    private int seqIndex;
    private int endIndex;
    private Transform[] endpoints;
    private List<Transform[]> edgeTrans;
    List<GameObject> currentVertices;
    List<GameObject> currentEdges;
    int vertCount, edgeCount;
    private string vertDefault;
    
    // Start is called before the first frame update
    void Start()
    {
        currentVertices = new List<GameObject>();
        currentEdges = new List<GameObject>();
        vertSeq = new KeyCode[] { KeyCode.V, KeyCode.Mouse0 };
        seqIndex = 0;
        endpoints = new Transform[2];
        endIndex = 0;
        edgeTrans = new List<Transform[]>();
        vertBox.text = "n = " + currentVertices.Count;
        edgeBox.text = "m = " + currentEdges.Count;
        vertCount = 0;
        edgeCount = 0;
        helpButton.GetComponentInChildren<Text>().text = "Help";
        helpButton.onClick.AddListener(DisplayHelp);
        helpBox.text = "V + left click: create vertex: " +
            "Hover over vert + E (then again) to create edge: " +
            "Hover over vert + D to delete vert: " +
            "Hover over vert + B/W/Y/M/C/R to change color: " +
            "Click and drag vertex to reposition";
        helpBox.enabled = false;
        vertInfoBox.text = "Hover over vertex to see info about it";
        vertDefault = "Hover over vertex to see info about it";
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(vertSeq[seqIndex]))
        {
            if (++seqIndex == vertSeq.Length)
            {
                CreateNewVertex();
                seqIndex = 0;
            }
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            CreateEdge();
        }

        UpdateEdges();
        UpdateTextBoxes();
        UpdateVertAndEdge();
        ShowVertInfo();
    }

    // Instantiates a vertex into the world and adds it onto our vertex list.
    void CreateNewVertex()
    {
        GameObject curVert = Instantiate(VertexPrefab, Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1)), Quaternion.identity);
        currentVertices.Add(curVert);
        vertCount++;
    }

    private void CreateEdge()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

        if (hit.collider != null)
        {
            endpoints[endIndex] = hit.collider.gameObject.transform;

            if (endpoints[0] != null && endpoints[1] != null)
            {
                GameObject curEdge = Instantiate(edgeDraw);
                curEdge.GetComponent<EdgeScript>().DrawEdge(endpoints);
                currentEdges.Add(curEdge);
                edgeTrans.Add(new Transform[]{endpoints[0], endpoints[1] });
                endpoints[0].GetComponent<VertexScript>().vertDegree += 1;
                endpoints[1].GetComponent<VertexScript>().vertDegree += 1;
                endpoints[0] = null;
                endpoints[1] = null;
                endIndex = 0;
            }
            else
            {
                endIndex++;
            }
        }
        edgeCount++;
    }

    private void UpdateEdges()
    {
        if (currentEdges.Count == edgeTrans.Count) 
        {
            for (int i = 0; i < currentEdges.Count; i++)
            {
                if (currentEdges[i] != null && edgeTrans[i] != null)    
                {
                    currentEdges[i].GetComponent<EdgeScript>().DrawEdge(edgeTrans[i]);
                }
                else if(currentEdges[i] != null)
                {
                    currentEdges[i].GetComponent<EdgeScript>().ClapThisEdge();
                }
            }
        }
    }

    private void UpdateTextBoxes()
    {
        vertBox.text = ("n = " + vertCount);
        edgeBox.text = ("m = " + edgeCount);
    }

    private void UpdateVertAndEdge()
    {
        vertCount = 0;
        edgeCount = 0;

        for (int i = 0; i < currentVertices.Count; i++)
        {
            if (currentVertices[i] != null)
            {
                vertCount++;
            }
        }

        for(int i = 0; i < currentEdges.Count; i++)
        {
            if (currentEdges[i] != null)
            {
                edgeCount++;
            }
        }
    }

    void DisplayHelp()
    {
        helpBox.enabled = true;
    }

    void ShowVertInfo()
    {
        GameObject curVert;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

        if (hit.collider != null)
        {   
            curVert = hit.collider.gameObject;
            vertInfoBox.text = "Degree: " + curVert.GetComponent<VertexScript>().vertDegree;
        }
        else
        {
            vertInfoBox.text = vertDefault;
        }
    }
}