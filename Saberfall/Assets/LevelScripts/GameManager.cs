using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager { get; private set; }

    public UnitHealth _playerHealth = new UnitHealth(100, 100); // Create player with 100 health

    void Awake()
    {
        // Make sure this is a singleton class
        if(gameManager != null && gameManager != this) Destroy(this);
        else gameManager = this;
    }
}
