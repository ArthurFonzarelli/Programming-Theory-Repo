using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceObject : MonoBehaviour
{
    protected GameManager _gameManager;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    // ABSTRACTION
    public void Explode(Vector3 pos, float scale)
    {
        _gameManager.GenerateExplosion(pos, scale);
        Destroy(gameObject);
    }

    public virtual void FireCurrentWeapon()
    {
        // Override - some objects may not be able to fire weapons
    }
}
