using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] float _rotationSpeed;
    [SerializeField] float _horizontalSpeed;
    [SerializeField] float _speedVariability;

    private float actualSpeed;

    [SerializeField] SceneManager _sceneManager;

    void Awake()
    {
        _sceneManager = FindObjectOfType<SceneManager>();

        actualSpeed = _horizontalSpeed + Random.Range(-_speedVariability, _speedVariability);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, _rotationSpeed * Time.deltaTime);
        transform.position += new Vector3(actualSpeed * Time.deltaTime, 0, 0);

        // This makes them go in a circle for some reason (A: because it is translating along local direction, which is being rotated)
        //transform.Translate(Vector3.left * _horizontalSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Laser")
        {
            _sceneManager.GenerateExplosion(transform.position, 1.0f);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
