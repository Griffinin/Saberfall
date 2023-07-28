using UnityEngine;

// DRIVER PROGRAM
// Will start Saberfall by rendering the Start Menu
public class SaberfallProg : MonoBehaviour
{
    [SerializeField] private MenuController game;

    void Start() => game.startGame();
}
