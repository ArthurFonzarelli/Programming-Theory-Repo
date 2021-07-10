using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] Animator _animator;
    private float time = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        _animator.Play(0);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (_animator.GetCurrentAnimatorStateInfo(0).length < time)
            Destroy(gameObject);
    }
}
