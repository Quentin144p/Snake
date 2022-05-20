using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class SnakeController : MonoBehaviour
{
    private Vector2Int currentInput;
    private bool isAlive = true;
    [SerializeField] float timeBeforeMove;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(Move());
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            var getInput = context.ReadValue<Vector2>();
            currentInput = Vector2Int.RoundToInt(getInput);
        }
    }

    private IEnumerator Move()
    { 
        while (isAlive == true)
        {
            var emptySpace = rb.position;
            rb.MovePosition(emptySpace + currentInput);
            yield return new WaitForSeconds(timeBeforeMove);
        }
    }
}
