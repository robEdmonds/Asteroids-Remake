using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailSortingLayer : MonoBehaviour {

    public TrailRenderer trail;
    
    public string sortingLayerName;
    public int sortingOrder;
    
    // Use this for initialization
    void Start()
    {
        trail.sortingLayerName = sortingLayerName;
        trail.sortingOrder = sortingOrder;
    }
}
