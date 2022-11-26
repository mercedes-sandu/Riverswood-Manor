using UnityEngine;

public class Painting : MonoBehaviour, IInteractable
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="interactor"></param>
    /// <returns></returns>
    public bool Interact(Interactor interactor)
    {
        Debug.Log("Interacting with painting");
        return true;
    }
}