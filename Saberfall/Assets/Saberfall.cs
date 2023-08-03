using UnityEngine;

// DRIVER PROGRAM
// Will start Saberfall by rendering the Start Menu
public class Saberfall : MonoBehaviour
{
    [SerializeField] private MenuController game;

    void Start() => game.startGame();
}
