using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Solitaire : MonoBehaviour
{
    public Sprite[] cardFaces;
    public GameObject cardPrefab;
    public GameObject[] bottomPos;
    public GameObject[] topPos;
    public GameObject deckButton;

    public SpriteRenderer deckButtonSpriteRenderer;
    public Sprite deckButtonSprite;
    public Sprite deckButtonNullSprite;

    public static string[] suits = new string[] { "T", "R", "C", "P" };
    public static string[] values = new string[] { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K"};
    public List<string>[] bottoms;
    public List<string>[] tops;
    public List<string> tripsOnDisplay = new List<string>();
    public List<List<string>> deckTrips = new List<List<string>>();

    private List<string> bottom0 = new List<string>();
    private List<string> bottom1 = new List<string>();
    private List<string> bottom2 = new List<string>();
    private List<string> bottom3 = new List<string>();
    private List<string> bottom4 = new List<string>();
    private List<string> bottom5 = new List<string>();
    private List<string> bottom6 = new List<string>();

    private int trips;
    private int tripsRemainder;
    private int deckLocation;

    public List<string> deck;
    public List<string> discardPile = new List<string>();
    public Button resetButton;
    // Start is called before the first frame update
    void Start()
    {
        bottoms = new List<string>[] { bottom0, bottom1, bottom2, bottom3, bottom4, bottom5, bottom6 };
        resetButton.enabled = false;
        Invoke("PlayCards", 1.0f);
    }

    public void PlayCards()
    {
        foreach (List<string> card in bottoms)
        {
            card.Clear();
        }
        deck = GenerateDeck();
        Shuffle(deck);

        /*Testear las cartas del deck
        foreach (string card in deck)
        {
            print(card);
        }*/
        SolitaireSort();
        StartCoroutine(SolitaireDeal());
        AudioManager.instance.PlaySFX("Cards");
        SortDeck();
    }

    public static List <string> GenerateDeck()
    {
        List<string> newDeck = new List<string>();

        foreach (string s in suits)
        {
            foreach (string v in values)
            {
                newDeck.Add(s + v);
            }
        }

        return newDeck;
    }

    void Shuffle <T>(List <T> list)
    {
        System.Random random = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    IEnumerator SolitaireDeal()
    {
        for (int i=0; i < 7; i++)
        {
            float yOffset = 0;
            float zOffset = 0.005f;
            foreach (string card in bottoms[i])
            {
                yield return new WaitForSeconds(0.01f);
                GameObject newCard = Instantiate(cardPrefab, new Vector3(bottomPos[i].transform.position.x, bottomPos[i].transform.position.y - yOffset, bottomPos[i].transform.position.z - zOffset), Quaternion.identity,bottomPos[i].transform );
                newCard.name = card;
                newCard.GetComponent<Selectable>().row = i;
                if(card==bottoms[i][bottoms[i].Count - 1])
                {
                    newCard.GetComponent<Selectable>().faceUp = true;
                }           
                yOffset += 0.05f;
                zOffset += 0.005f;
                discardPile.Add(card);
            }
        }
        foreach(string card in discardPile)
        {
            if (deck.Contains(card))
            {
                deck.Remove(card);
            }
        }
        discardPile.Clear();
        resetButton.enabled = true;
        deckButton.GetComponent<BoxCollider2D>().enabled = true;

    }

    void SolitaireSort()
    {
        for (int i=0; i < 7; i++)
        {
            for(int j=i; j<7; j++)
            {
                bottoms[j].Add(deck.Last<string>());
                deck.RemoveAt(deck.Count - 1);
            }
        }
    }

    public void SortDeck()
    {
        trips = deck.Count / 3;
        tripsRemainder = deck.Count % 3;
        deckTrips.Clear();
        int modifier = 0;
        for (int i=0; i < trips; i++)
        {
            List<string> myTrips = new List<string>();
            for(int j=0; j < 3; j++)
            {
                myTrips.Add(deck[j + modifier]);
            }
            deckTrips.Add(myTrips);
            modifier = modifier + 3;
        }
        if (tripsRemainder != 0)
        {
            List<string> myRemainders = new List<string>();
            modifier = 0;
            for (int k=0; k< tripsRemainder; k++)
            {
                myRemainders.Add(deck[deck.Count - tripsRemainder + modifier]);
                modifier++;
            }
            deckTrips.Add(myRemainders);
            trips++;
        }
        deckLocation = 0;
    }

    public void DealFromDeck()
    {
        foreach (Transform child in deckButton.transform)
        {
            if (child.CompareTag("Card"))
            {
                deck.Remove(child.name);
                discardPile.Add(child.name);
                Destroy(child.gameObject);
            }

        }
        if (deck.Count > 3)
        {
            deckButtonSpriteRenderer.sprite = deckButtonSprite;
        }
        else
        {
            deckButtonSpriteRenderer.sprite = deckButtonNullSprite;
        }
        if (deckLocation < trips)
        {
            tripsOnDisplay.Clear();
            float offsetX = 0.4f;
            float offsetZ = -0.2f;
            foreach (string card in deckTrips[deckLocation])
            {
                GameObject newTopCard = Instantiate(cardPrefab, new Vector3(deckButton.transform.position.x + offsetX, deckButton.transform.position.y, deckButton.transform.position.z + offsetZ), Quaternion.identity, deckButton.transform);
                offsetX = offsetX + 0.095f;
                offsetZ = offsetZ - 0.2f;
                newTopCard.name = card;
                tripsOnDisplay.Add(card);
                newTopCard.GetComponent<Selectable>().faceUp = true;
                newTopCard.GetComponent<Selectable>().inDeckPile = true;

            }
            deckLocation++;
        }
        else
        {
            deckButtonSpriteRenderer.sprite = deckButtonSprite;
            RestackTopDeck();
        }
    }

    void RestackTopDeck()
    {
        deck.Clear();
        foreach (string card in discardPile)
        {
            deck.Add(card);
        }
        discardPile.Clear();
        SortDeck();
    }
}
