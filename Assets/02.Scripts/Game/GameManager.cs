using UnityEngine;

public class GameManager : MonoBehaviour
{
    private EGameState _state = EGameState.Ready;
    public EGameState State => _state;


}
