using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    // Start is called before the first frame update
    private float timeBetweenRestarts;
    private float timer;
    void Start()
    {
        timeBetweenRestarts = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
    }

    public void RestartGame()
    {
        if(timer > timeBetweenRestarts)
        {
            //Evitar bug carta seleccionada
            FindObjectOfType<UserInput>().slot1 = FindObjectOfType<UserInput>().gameObject;
            //Encontrar todas las cartas
            UpdateSprite[] cards = FindObjectsOfType<UpdateSprite>();
            foreach (UpdateSprite card in cards)
            {
                Destroy(card.gameObject);
            }
            //Limpiar valores
            ClearTopValues();

            //Barajar de nuevo
            FindObjectOfType<Solitaire>().PlayCards();
            timer = 0;
            if (GameManager.gameMode == 2)
            {
                FindObjectOfType<Timer>().time = 0;
            }
            FindObjectOfType<Solitaire>().deckButtonSpriteRenderer.sprite = FindObjectOfType<Solitaire>().deckButtonSprite;
            GameManager.gameOver = false;

        }
      
    }

    void ClearTopValues()
    {
        Selectable[] selectables = FindObjectsOfType<Selectable>();
        foreach (Selectable selectable in selectables)
        {
            if (selectable.CompareTag("Top"))
            {
                selectable.suit = null;
                selectable.value = 0;
            }
        }
    }
}
