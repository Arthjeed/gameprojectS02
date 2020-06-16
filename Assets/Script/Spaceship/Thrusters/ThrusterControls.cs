using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ThrusterControls : MonoBehaviour, IPunObservable
{
    public float rotateAngleSpeed = 10;
    public float minAngle = 15;
    public float maxAngle = 165;
    private AudioSource audioData;
    private Vector2 position;
    private Vector2 localPosition;
    private GameObject parent;
    private SpaceShip ship;
    private float angle = 90;
    private float rayon = 100;
    private ParticleSystem particle;
    private ParticleSystem.MainModule psmain;
    private float startSize = 1.3f;
    private float minSize = 0.5f;
    [SerializeField]
    private ParticleSystem indicator;

    void Awake()
    {
        position = transform.position;
        localPosition = transform.localPosition;
        parent = transform.parent.gameObject;
        ship = parent.GetComponent<SpaceShip>();
        rayon = Vector2.Distance(position, parent.transform.position);
        particle = GetComponent<ParticleSystem>();
        audioData = GetComponent<AudioSource>();
        psmain = particle.main;
        psmain.startSize = minSize;
        // particle.Stop();
    }

    void OnEnable()
    {
        indicator.Play();
        StartCoroutine(StopParticleSystem(indicator, 1.5f));
    }

    IEnumerator StopParticleSystem(ParticleSystem particleSystem, float time)
    {
        yield return new WaitForSeconds(time);
        particleSystem.Stop();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(ship.transform.position);
            stream.SendNext(ship.speed);
        }
        else
        {
            ship.transform.position = (Vector3)stream.ReceiveNext();
            ship.speed = (float)stream.ReceiveNext();
        }
    }

    void Update()
    {
        position = transform.position;
        localPosition = transform.localPosition;
        // float x = Input.GetAxis("Horizontal");
        // if (x != 0)
        //     RotateThruster(x);
        ActivateThruster();
    }

    public void CheckParticles()
    {
        float size = ship.speed * startSize;
        if (ship.speed * startSize < minSize)
            size = minSize;
        psmain.startSize = size;
    }


    public void PlayParticle()
    {
        if (particle.isStopped)
            particle.Play();
    }

    public void StopParticle()
    {
        if (particle.isPlaying)
            particle.Stop();
    }

    void RotateThruster(float inputValue) {
        Vector2 parentPos = parent.transform.position;
        // angle = Mathf.Atan2(parentPos.y - position.y, parentPos.x - position.x) * (180 / Mathf.PI);
        float angleAbs = Mathf.Abs(angle);
        //print(angle);
        if (angleAbs >= minAngle && angleAbs <= maxAngle)
        {
            angle = angle + (inputValue * Time.deltaTime * rotateAngleSpeed);
            float x = Mathf.Cos(angle * Mathf.Deg2Rad) * rayon;
            float y = Mathf.Sin(angle * Mathf.Deg2Rad) * rayon;
            transform.RotateAround(parentPos, Vector3.forward, inputValue * rotateAngleSpeed * Time.deltaTime);
            transform.localPosition = new Vector2(x, y);
        } 
        else
        {
            angleAbs = Mathf.Clamp(angleAbs, minAngle, maxAngle);
            angle = angleAbs * Mathf.Sign(angle);
            // float rayon = Vector2.Distance(position, parentPos);
            // angleAbs = Mathf.Clamp(angleAbs, minAngle, maxAngle);
            // float x = rayon * Mathf.Cos(Mathf.Sign(angle) * angleAbs * Mathf.Deg2Rad);
            // float y = rayon * Mathf.Sin(Mathf.Sign(angle) * angleAbs * Mathf.Deg2Rad);
            // print(new Vector2(x, y));
            // transform.position = new Vector2(x, y);
        }
    }

    void ActivateThruster()
    {
        if (Input.GetButton("Jump"))
        {
            if (!audioData.isPlaying) {
                audioData.Play();
            }
            ship.Accelerate();
        } else {
            if (audioData.isPlaying){
                audioData.Stop();
            }
        }
        if (Input.GetButton("Action1"))
            ship.SlowDown();
    }
}
