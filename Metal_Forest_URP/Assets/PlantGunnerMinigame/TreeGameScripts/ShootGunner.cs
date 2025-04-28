using MetalForest;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootGunner : MonoBehaviour
{
    private Arduino arduino;
    private ArduinoInputManager inputManager;
    public Rigidbody bulletPrefab;
    public float shootSpeed = 30,timeSpeed,timeBeforeShoot;
    public Transform spawnPnt;
    float clock;
    bool canShoot;
    // Start is called before the first frame update
    void Start()
    {
        arduino = Arduino.arduinoInstance;
        inputManager = ArduinoInputManager.inputInstance;
    }

    // Update is called once per frame
    void Update()
    {
        clock += Time.deltaTime * timeSpeed;

        if (inputManager.GetRotatary3Button == 0)
        {
            //print(clock);
            if (canShoot)
            {
                ShootProjectile();
            }
            
        }
        if (clock > timeBeforeShoot)
        {
            canShoot = true;
            clock = 0;
        }
        else
        {
            canShoot = false;
        }
    }

    void ShootProjectile()
    {
        var projectile = Instantiate(bulletPrefab, spawnPnt.position, spawnPnt.rotation);

        //Shoot the Bullet in the forward direction of the player
        projectile.velocity = -spawnPnt.right * shootSpeed;
    }
}
