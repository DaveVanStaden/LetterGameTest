using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterTextSelector : MonoBehaviour
{
    public string ObjectLetter;
    // Start is called before the first frame update
    void Start()
    {
        ObjectLetter = this.gameObject.name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
