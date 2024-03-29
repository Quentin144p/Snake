﻿using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class SnakeController : MonoBehaviour
{
    private Vector2Int currentInput;
    private bool isAlive = true;
    private bool fruitEaten;
    private int eatenFruits;
    [SerializeField] float timeBeforeMove;
    private Rigidbody2D rb;
    private Transform tail;
    public GameObject tailPrefab;
    public GameObject canvasPrefab;
    private ScoreDisplayer scoreDisplayer;

    private void Start()
    {
        fruitEaten = true;
        GameObject Tail = new GameObject("Tail");
        tail = Tail.transform;
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(Move());
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            var getInput = context.ReadValue<Vector2>();
            var lastInput = getInput;
            currentInput = Vector2Int.RoundToInt(getInput);
        }
    }

    private IEnumerator Move()
    { 
        while (isAlive == true)
        {
            var emptySpace = rb.position;
            rb.MovePosition(emptySpace + currentInput);
            yield return new WaitForEndOfFrame();
            foreach (Transform child in tail)
            {
                var lastPosition = child.transform.position;
                child.transform.position = emptySpace;
                emptySpace = lastPosition;
            }
            AddTailPart(emptySpace);
            yield return new WaitForSeconds(timeBeforeMove);
        }
    }

    public void AddTailPart(Vector2 newPartPosition)
    {
        if (fruitEaten == false)
        {
            return;
        }
        else
        {
            var instantiateTail = Instantiate(tailPrefab, newPartPosition, Quaternion.identity);
            instantiateTail.transform.parent = tail;
            fruitEaten = false;
        }
    }

    public void EatFruit()
    {
        fruitEaten = true;
        eatenFruits++;
    }

    public void Die()
    {
        if (isAlive == false)
        {
            return;
        }
        isAlive = false;
        Instantiate(canvasPrefab);
        scoreDisplayer = FindObjectOfType<ScoreDisplayer>();
        scoreDisplayer.SetScore(eatenFruits);
        Destroy(GameObject.Find("Tail"));
        Destroy(this.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "PlayZone")
        {
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "tailPrefab")
        {
            Die();
        }
    }
}
