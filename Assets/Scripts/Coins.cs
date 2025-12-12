using UnityEngine;

public class Coins : MonoBehaviour
{
    [SerializeField] private int coins;
    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.tag == "Coin")
        {
            AddCoins();
            Destroy(collision2D.gameObject);
        }
    }
    private void AddCoins()
    {
        coins += 1;
        Debug.Log("Coin count updated: " + coins);
    }



}
