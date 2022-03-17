using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Transform cameraPosition;
    public List<Node> reachableNode = new List<Node>();
    // Start is called before the first frame update
    [SerializeField]
    private Collider2D col;
    void Start()
    {
        col = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
