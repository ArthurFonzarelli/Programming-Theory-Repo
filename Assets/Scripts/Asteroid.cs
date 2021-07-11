using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
// Asteroid inherits from SpaceObject class
public class Asteroid : SpaceObject
{
    [SerializeField] float _rotationSpeed;
    [SerializeField] float _horizontalSpeed;
    [SerializeField] float _speedVariability;

    private float actualSpeed;

    //[SerializeField] SceneManager _sceneManager;

    void Start()
    {
        actualSpeed = _horizontalSpeed + Random.Range(-_speedVariability, _speedVariability);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, _rotationSpeed * Time.deltaTime);
        transform.position += new Vector3(actualSpeed * Time.deltaTime, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Laser")
        {
            Destroy(collision.gameObject);
            Explode(transform.position, 1.0f);
        }
    }
}
