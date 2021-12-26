using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeScript : MonoBehaviour
{
    [SerializeField] private LineRenderer edgePrefab;
    [SerializeField] private Transform[] endpoints;
    private bool haveData;

    private void Start()
    {
        edgePrefab = this.GetComponent<LineRenderer>();
        edgePrefab.widthMultiplier = .05f;
        edgePrefab.positionCount = 2;
        endpoints = new Transform[2];
        haveData = false;
    }

    private void Update()
    {
        if(endpoints[0] == null || endpoints[1] == null)
        {
            ClapThisEdge();
        }
        else if (haveData)
        {
            edgePrefab.SetPosition(0, endpoints[0].position);
            edgePrefab.SetPosition(1, endpoints[1].position);
        }
    }

    public void DrawEdge(Transform[] inEP)
    {
        if (inEP[0] == null || inEP[1] == null)
        {
            Destroy(this.gameObject);
            return;
        }

        edgePrefab.positionCount = inEP.Length;
        this.endpoints = inEP;
        edgePrefab.SetPosition(0, endpoints[0].position);
        edgePrefab.SetPosition(1, endpoints[1].position);
        haveData = true;
    }

    public void ClapThisEdge()
    {
        Destroy(this.gameObject);
    }

    private void OnMouseOver()
    {
        if(Input.GetKeyDown(KeyCode.D))
        {
            ClapThisEdge();
        }
    }
}
