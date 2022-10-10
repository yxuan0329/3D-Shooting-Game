using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Bullet velocity
    public float bulletSpeed = 10;
    public float Ai = 10;

    // Gun
    public GameObject gun;

    // bullet prefab
    public GameObject bulletPrefab;

    [Range(0.01f, 1f)]
    public float speedH = 1.0f;
    [Range(0.01f, 1f)]
    public float speedV = 1.0f;

    GameManager gm;
    float yaw = 0.0f;
    float pitch = 0.0f;

    // pop audio when shooting
    private AudioSource popAudioSrc;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindObjectOfType<GameManager>();
        popAudioSrc = GetComponent<AudioSource>();
    }

    void Update()
    {
        transform.eulerAngles = new Vector3(pitch, yaw, 0f);
    }

    void OnFire()
    {
        // spawn a new bullet
        GameObject newBullet = Instantiate(bulletPrefab);

        // pass the game manager
        newBullet.GetComponent<BulletController>().gm = gm;

        // position will be that of the gun
        newBullet.transform.position = gun.transform.position;

        // get rigid body
        Rigidbody bulletRb = newBullet.GetComponent<Rigidbody>();

        // give the bullet velocity
        bulletRb.velocity = gun.transform.forward * bulletSpeed;
    }

    void OnLook(InputValue value)
    {
        var delta = value.Get<Vector2>();

        yaw += speedH * delta.x;
        pitch -= speedV * delta.y;

        pitch = Mathf.Clamp(pitch, -90f, 90f);
    }

    void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Debug.Log("Application is focussed");
        }
        else
        {
            Debug.Log("Application lost focus");
        }
    }
}
