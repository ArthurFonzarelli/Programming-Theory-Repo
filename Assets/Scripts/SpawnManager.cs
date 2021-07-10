using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] float _asteroidFrequency;
    [SerializeField] float _frequencyNoise;

    [SerializeField] float _upperBound;
    [SerializeField] float _lowerBound;

    [SerializeField] float _xOffset;

    [SerializeField] Asteroid _asteroidPrefab;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("SpawnAsteroid");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnAsteroid()
    {
        while (true)
        {
            Vector3 astPosition = new Vector3(_xOffset, Random.Range(_lowerBound, _upperBound), 0);
            Instantiate(_asteroidPrefab, astPosition, Quaternion.identity);
            yield return new WaitForSeconds(_asteroidFrequency + Random.Range(-_frequencyNoise, _frequencyNoise));
        }
    }
}
