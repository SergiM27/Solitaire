using UnityEngine;
using UnityEngine.UI;
public class BackgroundMenuQuads : MonoBehaviour
{
    public float v;
    public float i;
    private Transform cardsFather;
    public Sprite[] image;
    // Use this for initialization
    void Start()
    {
        cardsFather = GameObject.Find("CardsBackground").gameObject.transform;
        this.gameObject.transform.SetParent(cardsFather);
        this.gameObject.name = "CardMenu";
        v = Random.Range(0.020f, 0.065f);
        i = Random.Range(1.0f, 4.0f);
        this.gameObject.SetActive(false);
        int r = Random.Range(0, image.Length);
        gameObject.GetComponent<SpriteRenderer>().sprite = image[r];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.gameObject.transform.Translate(new Vector3(0.0f,v, 0.0f));
        this.gameObject.transform.Rotate(new Vector3(0.0f, i, 0.0f));
    }
}
