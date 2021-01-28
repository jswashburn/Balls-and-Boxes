using UnityEngine;

public class DamageArea : MonoBehaviour
{
    public delegate void DamageAction();
    public static event DamageAction OnBoxEnteredDamageArea;

    void OnCollisionEnter2D(Collision2D other)
    {
        var box = other.gameObject.GetComponent<Box>();
        if (box == null) return;
        OnBoxEnteredDamageArea?.Invoke();
        box.Die();
    }
}
