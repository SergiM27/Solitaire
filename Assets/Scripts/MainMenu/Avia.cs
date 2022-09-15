using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Avia : MonoBehaviour
{

    private string avia;
    private bool done;
    public TextMeshProUGUI aviaText;
    private List<BackgroundMenuQuads> bgCards = new List<BackgroundMenuQuads>();
    public Sprite aviaSpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        avia = "LA MILLOR AVIA DEL MON";
        done = false;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            if (Input.GetKey(KeyCode.V))
            {
                if (Input.GetKey(KeyCode.I))
                {
                    if (done == false)
                    {
                        aviaText.text = avia;
                        aviaText.fontSize = 43;
                        SetAvia();
                        done = true;
                    }
                }
            }
        }
    }


    void SetAvia()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().sprite = aviaSpriteRenderer;
            transform.GetChild(i).gameObject.transform.localScale = new Vector3(0.084f, 0.084f, 0.084f);
        }  
    }
}
