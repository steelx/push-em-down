using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletDecal;

    private readonly float speed = 50f;
    private readonly float secondsToDestroy = 3f;

    public Vector3 target { get; set; }
    public bool hit { get; set; }

    //private void Awake() { }

    private void OnEnable()
    {
        Destroy(gameObject, secondsToDestroy);
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        // checks if target was Collided against nothing
        // in this case if it fired in air
        if (!hit && Vector3.Distance(transform.position, target) < 0.01f)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // create bullet decal
        ContactPoint contact = other.GetContact(0);
        Vector3 offset = contact.normal * 0.0001f;
        GameObject.Instantiate(bulletDecal, contact.point + offset, Quaternion.LookRotation(contact.normal));
        Destroy(gameObject);
    }

}
