using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RendererSortingLayer : MonoBehaviour {

    public string sortingLayerName;
    public int sortingOrder;

    // Use this for initialization
    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.sortingLayerName = sortingLayerName;
        renderer.sortingOrder = sortingOrder;
    }
}
