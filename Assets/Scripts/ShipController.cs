using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
// ShipController inherits from SpaceObject class
public class ShipController : SpaceObject
{
    // ENCAPSULATION
    private float speed = 5;
    public float Speed
    {
        get => speed;
        set
        {
            if ((value > 0) && (value < 10))
            {
                speed = value;
            }
        }

    }

    public SpriteRenderer sprites;
    public Sprite[] spriteList;
    public Sprite[] spriteSpinningList;

    [SerializeField] float shipAnimSpeed;
    [SerializeField] float shipSpinSpeed;

    public int index;
    public int size1;
    public int size2;

    public int curState;
    public int prevState;
    public float timer;

    public bool isSpinKeyPressed;
    public bool spinComplete;
    public bool isStartDirectionUp;

    // Weapons
    [SerializeField] Laser _wpnLaserPrefab;
    [SerializeField] GameObject _laserSource;
    [SerializeField] GameObject _activeProjectiles;

    // Start is called before the first frame update
    void Start()
    {
        curState = 0;
        prevState = 0;

        index = 0;
        timer = 0;

        size1 = spriteList.Length;
        size2 = spriteSpinningList.Length;

        isSpinKeyPressed = false;
        spinComplete = false;
        isStartDirectionUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            FireCurrentWeapon();

        UpdatePlayerState();
    }

    // POLYMORPHISM
    public override void FireCurrentWeapon()
    {
        Laser newLaser = Instantiate(_wpnLaserPrefab, _laserSource.transform.position, Quaternion.identity);
        newLaser.transform.parent = _activeProjectiles.transform;
    }

    // ABSTRACTION
    private void UpdatePlayerState()
    {
        float movX = Input.GetAxis("Horizontal");
        float movY = Input.GetAxis("Vertical");
        transform.Translate(movX * Time.deltaTime * speed, movY * Time.deltaTime * speed, 0);

        // Check for execution of spin maneuver
        if (Input.GetMouseButton(1) && (!isSpinKeyPressed))
        {
            curState = 1;
            isSpinKeyPressed = true;
            timer = 0;

            // Determine player vertical motion direction at start; use this to determine how sprites are cycled through
            if (movY < 0)
                isStartDirectionUp = false;
            else
                isStartDirectionUp = true;
        }

        // Check for default state or spin maneuver complete
        if (!Input.GetMouseButton(1)/*Input.GetKey("space")*/ && (curState == 1) && spinComplete)
        {
            curState = 0;
            timer = 0;
            //index = 0;
            isSpinKeyPressed = false;
            spinComplete = false;
            isStartDirectionUp = false;
        }

        // Normal maneuvering
        if (curState == 0)
        {
            timer += Time.deltaTime;

            if (timer >= shipAnimSpeed)
            {
                timer = 0;
                index++;
                //index = isStartDirectionUp ? index + 1 : index - 1;
                //if (index >= size2)
                //    index = 0;
            }

            // Gradual rotation
            if (movY >= 0.3)
                index = 2;
            else if (movY >= 0.1 && movY < 0.3)
                index = 1;
            else if (movY <= -0.3)
                index = spriteSpinningList.Length - 2;
            else if (movY <= -0.1 && movY > -0.3)
                index = spriteSpinningList.Length - 1;
            else
                index = 0;

            //sprites.sprite = spriteSpinningList[index];
        }

        // Spinning
        else if (curState == 1)
        {
            // Show other animation
            timer += Time.deltaTime;

            if (timer >= shipSpinSpeed)
            {
                timer = 0;
                index = isStartDirectionUp ? index + 1 : index - 1;

                if (index >= size2)
                {
                    index = 0;
                }
                else if (index < 0)
                {
                    index = size2 - 1;
                }
                //else
                //spinComplete = false;

                // Completion of spin logic: depending on if player is going up or down, figure out which frame to end spin on
                // Going up
                if (movY > 0)
                {
                    if (index == 1)
                        spinComplete = true;
                    else
                        spinComplete = false;
                }
                else if (movY < 0)
                {
                    if (index == spriteSpinningList.Length - 1)
                        spinComplete = true;
                    else
                        spinComplete = false;
                }
                else
                {
                    if (index == 0)
                        spinComplete = true;
                    else
                        spinComplete = false;
                }
            }

            //sprites.sprite = spriteSpinningList[index];
        }

        if (index < 0)
        {
            index = spriteSpinningList.Length;
        }
        else if (index >= spriteSpinningList.Length)
        {
            index = 0;
        }
        sprites.sprite = spriteSpinningList[index];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Determine if hit by asteroid
        if (collision.gameObject.tag == "Asteroid Large")
        {
            Explode(transform.position, 0.5f);
            _gameManager.StartGameOverTransition();
        }
    }
}
