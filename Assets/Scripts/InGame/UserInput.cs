using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class UserInput : MonoBehaviour
{
    public GameObject slot1;
    private float zOffset = 0.01f;
    private Solitaire solitaire;
    private float timer;
    private float doubleClickTime = 0.3f;
    private int clickCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        slot1 = this.gameObject;
        solitaire = FindObjectOfType<Solitaire>();
    }

    // Update is called once per frame
    void Update()
    {
        DoubleClickTime();
        GetMouseClick();
    }

    void DoubleClickTime()
    {
        if (clickCount == 1)
        {
            timer += Time.deltaTime;
        }

        if (clickCount == 3)
        {
            timer = 0;
            clickCount = 1;
        }

        if (timer > doubleClickTime)
        {
            timer = 0;
            clickCount = 0;
        }
    }

    bool DoubleClick()
    {
        if (timer < doubleClickTime && clickCount == 2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void GetMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clickCount++;
            //Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mouseZPosition));
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit)
            {
                //Que se ha colisionado
                if (hit.collider.CompareTag("Deck"))
                {
                    Deck();
                    AudioManager.instance.PlaySFX("Click");
                }
                else if (hit.collider.CompareTag("Card"))
                {
                    Card(hit.collider.gameObject);
                    AudioManager.instance.PlaySFX("Click");
                }
                else if (hit.collider.CompareTag("Top"))
                {
                    Top(hit.collider.gameObject);
                }
                else if (hit.collider.CompareTag("Bottom"))
                {
                    Bottom(hit.collider.gameObject);
                }
            }
        }
    }

    void Deck()
    {
        //He clickado en el deck
        solitaire.DealFromDeck();
        slot1 = this.gameObject;
    }

    void Card(GameObject selected)
    {
        if (selected != null && slot1 != null)
        {
            if (!selected.GetComponent<Selectable>().faceUp)
            {
                if (!Blocked(selected))
                {
                    selected.GetComponent<Selectable>().faceUp = true;
                    slot1 = this.gameObject;
                }
            }
            else if (selected.GetComponent<Selectable>().inDeckPile)
            {
                //Ver si ha sido pulsada mas de una vez para hacer el doble click

                if (!Blocked(selected))
                {
                    if (slot1 == selected) //Si se ha clickado 2 veces la misma carta
                    {
                        if (DoubleClick())
                        {
                            //Stackea automaticamente
                            AutoStack(selected);
                        }
                    }
                    else
                    {
                        slot1 = selected;
                    }
                }
            }
            else
            {
                if (slot1 == this.gameObject)
                {
                    slot1 = selected;
                }
                else if (slot1 != selected)
                {
                    if (Stackable(selected))
                    {
                        Stack(selected);
                    }
                    else
                    {
                        slot1 = selected;
                    }
                }
                else if (slot1 == selected) //Si se ha clickado 2 veces la misma carta
                {
                    if (DoubleClick())
                    {
                        //Stackea automaticamente
                        AutoStack(selected);
                    }
                }


            }
        }
        else
        {
            return;
        }


    }
    void Top(GameObject selected)
    {
        if (slot1.CompareTag("Card"))
        {
            if (slot1.GetComponent<Selectable>().value == 1)
            {
                Stack(selected);
            }
        }
    }
    void Bottom(GameObject selected)
    {
        if (slot1.CompareTag("Card"))
        {
            if (slot1.GetComponent<Selectable>().value == 13)
            {
                Stack(selected);
            }
        }
    }

    bool Stackable(GameObject selected)
    {
        Selectable s1 = slot1.GetComponent<Selectable>();
        Selectable s2 = selected.GetComponent<Selectable>();

        if (!s2.inDeckPile)
        {
            if (s2.top)
            {
                if (s1.suit == s2.suit || (s1.value == 1 && s2.suit == null))
                {
                    if (s1.value == s2.value + 1)
                    {
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (s1.value == s2.value - 1)
                {
                    bool card1red = true;
                    bool card2red = true;
                    if (s1.suit == "T" || s1.suit == "P")
                    {
                        card1red = false;
                    }
                    if (s2.suit == "T" || s2.suit == "P")
                    {
                        card2red = false;
                    }
                    if (card1red == card2red)
                    {
                        return false;
                    }
                    else
                    {
                        if (HasNoChildren(s2.gameObject))
                        {
                            return true;
                        }
                    }
                }
            }
        }
        return false;

    }

    void Stack(GameObject selected)
    {
        Selectable s1 = slot1.GetComponent<Selectable>();
        Selectable s2 = selected.GetComponent<Selectable>();

        float yOffset = 0.075f;

        if (!s2.top && s1.value == 13)
        {
            yOffset = -0.075f;
        }
        if (s2.top)
        {
            yOffset = 0f;
        }


        slot1.transform.position = new Vector3(selected.transform.position.x, selected.transform.position.y - yOffset, selected.transform.position.z - zOffset);
        slot1.transform.parent = selected.transform;
        if (s1.inDeckPile)
        {
            solitaire.tripsOnDisplay.Remove(slot1.name);
            solitaire.deck.Remove(slot1.name);
        }
        else if (s1.top && s2.top && s1.value == 1)
        {
            solitaire.topPos[s1.row].GetComponent<Selectable>().value = 0;
            solitaire.topPos[s1.row].GetComponent<Selectable>().suit = null;
        }
        else if (s1.top)
        {
            solitaire.topPos[s1.row].GetComponent<Selectable>().value = s1.value - 1;
        }
        else
        {
            solitaire.bottoms[s1.row].Remove(slot1.name);
        }
        s1.inDeckPile = false;
        s1.row = s2.row;

        if (s2.top)
        {
            solitaire.topPos[s1.row].GetComponent<Selectable>().value = s1.value;
            solitaire.topPos[s1.row].GetComponent<Selectable>().suit = s1.suit;
            s1.top = true;
            FindObjectOfType<ScoreManager>().HasWon();
        }
        else
        {
            s1.top = false;
        }
        //FindObjectOfType<ScoreManager>().HasWon();
        slot1 = this.gameObject;
    }

    bool Blocked(GameObject selected)
    {
        Selectable s2 = selected.GetComponent<Selectable>();
        if (s2.inDeckPile == true)
        {
            if (s2.name == solitaire.tripsOnDisplay.Last())
            {
                return false;
            }
            else
            {
                //print(s2.name + " is blocked by " + solitaire.tripsOnDisplay.Last());
                return true;
            }
        }
        else
        {
            if (s2.name == solitaire.bottoms[s2.row].Last())
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }

    void AutoStack(GameObject selected)
    {
        for (int i = 0; i < solitaire.topPos.Length; i++)
        {
            Selectable stack = solitaire.topPos[i].GetComponent<Selectable>();
            if (selected.GetComponent<Selectable>().value == 1) //Si es un as
            {
                if (solitaire.topPos[i].GetComponent<Selectable>().value == 0)//Si hay posiciones libres arriba
                {
                    slot1 = selected;
                    Stack(stack.gameObject); //pon el as en la que este mas a la izquierda
                    break;
                }
            }
            else
            {
                if ((solitaire.topPos[i].GetComponent<Selectable>().suit == slot1.GetComponent<Selectable>().suit) && (solitaire.topPos[i].GetComponent<Selectable>().value == slot1.GetComponent<Selectable>().value - 1))
                {
                    //si no tiene ninguna carta debajo suyo
                    if (HasNoChildren(slot1))
                    {
                        slot1 = selected;

                        //Intentar encontrar si hay algun sitio que cumpla con la condicion de stackearse
                        string lastCardName = stack.suit + stack.value.ToString();
                        if (stack.value == 1)
                        {
                            lastCardName = stack.suit + "A";
                        }
                        else if (stack.value == 11)
                        {
                            lastCardName = stack.suit + "J";
                        }
                        else if (stack.value == 12)
                        {
                            lastCardName = stack.suit + "Q";
                        }
                        else if (stack.value == 13)
                        {
                            lastCardName = stack.suit + "K";
                        }

                        GameObject lastCard = GameObject.Find(lastCardName);
                        Stack(lastCard);
                        //FindObjectOfType<ScoreManager>().HasWon();
                        break;
                    }

                }
            }
        }

    }

    bool HasNoChildren(GameObject card)
    {
        int i = 0;
        foreach (Transform child in card.transform)
        {
            i++;
        }

        if (i == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}

