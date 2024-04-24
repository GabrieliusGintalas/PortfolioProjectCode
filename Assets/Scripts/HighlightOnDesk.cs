using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EPOOutline;

public class HighlightOnDesk : MonoBehaviour
{
    [SerializeField] private BoxCollider boxCollider;
    private Outlinable outlinable;
    

    void Awake(){
        outlinable = GetComponent<Outlinable>();
        outlinable.enabled = false;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
