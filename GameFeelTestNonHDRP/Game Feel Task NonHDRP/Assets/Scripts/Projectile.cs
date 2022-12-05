using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class Projectile : MonoBehaviour
{
    private RocketPool rocketPoolScript;
    private UIController uIController;

    private GameObject fireTrail;
    public GameObject lineTrail;
    public GameObject fireTrailStylized;
    public float speed;
    public Rigidbody _RB;
    public float waitTime;

    private bool restart = false;

    public GameObject explosion1;
    public GameObject explosion2;
    public GameObject explosion3;
    private GameObject currentExplosion;

    private void Awake()
    {
        uIController = GameObject.FindGameObjectWithTag("Main Canvas").GetComponent<UIController>();
        _RB = GetComponent<Rigidbody>();
        speed = uIController.uiRocketSpeed;
        fireTrail = transform.GetChild(0).gameObject;
        rocketPoolScript = GameObject.Find("GameController").GetComponent<RocketPool>();
        
    }

    void Update()
    {
        speed = uIController.uiRocketSpeed;
        waitTime -= Time.deltaTime;
        if (waitTime <= 0)
        {
            if (gameObject.tag == "Rocket3")
            {
                fireTrail.SetActive(true);
            }

            if(gameObject.tag == "Rocket2")
            {
                lineTrail.SetActive(true);
            }

            if (gameObject.tag == "Rocket1")
            {
                fireTrailStylized.SetActive(true);
            }

            if (uIController.canSeeTrails)
            {
                if (gameObject.tag == "Rocket1")
                {
                    fireTrailStylized.SetActive(true);
                }
                if (gameObject.tag == "Rocket2")
                {
                    lineTrail.SetActive(true);
                }

                if (gameObject.tag == "Rocket3")
                {
                    fireTrail.SetActive(true);
                }
            }
            else
            {
                fireTrailStylized.SetActive(false);
                lineTrail.SetActive(false);
                fireTrail.SetActive(false);
            }


            _RB.velocity = new Vector3(0, 0, -speed);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {

            if (gameObject.tag == "Rocket1")
            {
                currentExplosion = Instantiate(explosion1, transform.position, Quaternion.identity, null);
                Destroy(currentExplosion, 10f);
                _RB.velocity = new Vector3(0, 0, 0);
                gameObject.transform.position = rocketPoolScript.rocketPoolPosVec1;
                gameObject.GetComponent<Projectile>().enabled = false;

                fireTrailStylized.SetActive(false);

                CameraShaker.Instance.ShakeOnce(4f, 4f, 0.1f, 1f);
            }

            if (gameObject.tag == "Rocket2")
            {
                currentExplosion = Instantiate(explosion2, transform.position, Quaternion.identity, null);
                Destroy(currentExplosion, 10f);
                _RB.velocity = new Vector3(0, 0, 0);
                gameObject.transform.position = rocketPoolScript.rocketPoolPosVec2;
                gameObject.GetComponent<Projectile>().enabled = false;

                lineTrail.SetActive(false);

                StartCoroutine(WaitForExplosionShake2());
            }

            if (gameObject.tag == "Rocket3")
            {
                currentExplosion = Instantiate(explosion3, transform.position, Quaternion.identity, null);
                Destroy(currentExplosion, 10f);
                _RB.velocity = new Vector3(0, 0, 0);
                gameObject.transform.position = rocketPoolScript.rocketPoolPosVec3;
                gameObject.GetComponent<Projectile>().enabled = false;

                fireTrail.SetActive(false);

                StartCoroutine(WaitForExplosionShake3());
            }
        }
    }

    public IEnumerator WaitForExplosionShake2()
    {
        CameraShaker.Instance.ShakeOnce(2f, 4f, 0.1f, 4f);
        yield return new WaitForSeconds(2.1f);
        CameraShaker.Instance.ShakeOnce(10f, 6f, 0.1f, 4f);
    }

    public IEnumerator WaitForExplosionShake3()
    {
        CameraShaker.Instance.ShakeOnce(2f, 4f, 0.1f, 1f);
        yield return new WaitForSeconds(0.7f);
        CameraShaker.Instance.ShakeOnce(10f, 6f, 0.1f, 4f);
    }
}
