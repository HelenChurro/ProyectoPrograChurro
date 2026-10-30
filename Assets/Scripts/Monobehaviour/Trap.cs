using UnityEngine;

public class Trap : MonoBehaviour, Interactable
{
    [SerializeField] private ParticleSystem vfx;

    public void Interact()
    {
        vfx.Play();
    }
}
