using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    [SerializeField] int maxQueuedFrames;
    [SerializeField] int vSyncCount;
    [SerializeField] int targetFrameRate;
    [SerializeField] Explosion _explosion;

    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.maxQueuedFrames = maxQueuedFrames;
        QualitySettings.vSyncCount = vSyncCount;
        Application.targetFrameRate = targetFrameRate;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //    Instantiate(_explosion);
        
    }

    public void GenerateExplosion(Vector3 position, float size = 1.0f)
    {
        Explosion newExplosion = Instantiate(_explosion, position, Quaternion.identity);
        newExplosion.transform.localScale = new Vector3(size, size, 1.0f);
    }
}
