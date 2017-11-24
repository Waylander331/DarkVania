using UnityEngine;

public abstract class BaseAI : MonoBehaviour {

    public int Health;

    public float MovementSpeed;

    public int size;

    public abstract void DetectPlayer();

}
