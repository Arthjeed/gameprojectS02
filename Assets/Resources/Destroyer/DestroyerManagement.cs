using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DestroyerManagement : MonoBehaviour
{
    public GameObject head;
    public GameObject body;
    public GameObject tail;

    [Range(0, 200)]
    public float speed;
    public int chunkNumber = 50;

    /////////////////////////////////////////////////////////////////
    public GameObject spaceship;

    [Range(60, 100)]
    public float distance;

    [Range(100, 500)]
    public float distanceMax;

    [Range(1,100)]
    public float rotateSpeed;

    [SerializeField]
    private float secondPerShoot;

    [SerializeField]
    private LineRenderer lineRendererA;
    [SerializeField]
    private LineRenderer lineRendererB;

    [SerializeField]
    private bool DebugTangente = false;
    private bool attackState = true;

    private int choosingWichTangente = 0;

    private bool nextShoot = true;

    private bool dead = false;

    private List<GameObject> listChunks = new List<GameObject>();
    private List<GameObject> listBody = new List<GameObject>();


    ///////////////////////////////////////////////////////////////
    
    private Vector3 _targetCoreClosestPoint;
    private Transform _childTransform;
    private Transform _previousChildTransform;
    private bool _created = true;

    // Update is called once per frame

    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && _created) {
            _created = false;
            head = PhotonNetwork.Instantiate("PhotonPrefabs/Head Variant", transform.position, transform.rotation);
            listChunks.Add(head);
            listBody.Add(head.transform.GetChild(0).GetChild(0).gameObject);
            head.transform.SetParent(transform);
            for (int chunk = 0; chunk < chunkNumber; chunk++){
                GameObject chunks = PhotonNetwork.Instantiate("PhotonPrefabs/Body Variant", transform.position, transform.rotation);
                listChunks.Add(chunks);
                listBody.Add(chunks.transform.GetChild(0).GetChild(0).gameObject);
                chunks.transform.SetParent(transform);
            }
            GameObject tailchunk = PhotonNetwork.Instantiate("PhotonPrefabs/Tail Variant", transform.position, transform.rotation);
            listChunks.Add(tailchunk);
            listBody.Add(tailchunk.transform.GetChild(0).GetChild(0).gameObject);
            tailchunk.transform.SetParent(transform);
        }

        if (_created == false) {
            destroyerMovementHead();
            destroyerBodyFollowingList();
            if (nextShoot == true) {
                nextShoot = false;
                StartCoroutine(manageShoot());
            }

        }
    }
    
    bool findTangents(Vector2 center, float r, Vector2 point, ref Vector2 tanPosA, ref Vector2 tanPosB)
        {
            point -= center;

            float pointMagnitude = point.magnitude;

            if (pointMagnitude <= r) {
                return false;
            }

            float a = r * r / pointMagnitude;
            float q = r * (float)System.Math.Sqrt((pointMagnitude * pointMagnitude) - (r * r)) / pointMagnitude;

            Vector2 pN = point / pointMagnitude;
            Vector2 pNP = new Vector2(-pN.y, pN.x);
            Vector2 va = pN * q;

            tanPosA = va + pNP * q;
            tanPosB = va - pNP * q;
            
            tanPosA += center;
            tanPosB += center;

            return true;
        }

    void destroyerMovementHead()
    {
        Vector2 tanPosA = Vector2.zero;
        Vector2 tanPosB = Vector2.zero;

        bool foundTangents = findTangents((Vector2) spaceship.transform.position, distance, (Vector2) head.transform.position, ref tanPosA, ref tanPosB);

        if (DebugTangente) {
            Vector2 tanExtrapolateA = tanPosA + (tanPosA - (Vector2)head.transform.position);
            Vector2 tanExtrapolateB = tanPosB + (tanPosB - (Vector2)head.transform.position);

            lineRendererA.SetPosition(0, head.transform.position);
            lineRendererA.SetPosition(1, tanExtrapolateA);
            lineRendererB.SetPosition(0, head.transform.position);
            lineRendererB.SetPosition(1, tanExtrapolateB);
        }
        if (dead == true) {
            head.transform.position += head.transform.forward * speed * Time.deltaTime;
            return ;
        }

        if (Vector2.Distance(head.transform.position, spaceship.transform.position) > distance + 20 && attackState == true)
        {
            Vector2 tanPos;
            if (choosingWichTangente == 0)
                tanPos = tanPosA;
            else
                tanPos = tanPosB;
            if (tanPos != (Vector2)head.transform.position) {
                Quaternion lookRotation = Quaternion.LookRotation(new Vector2(tanPos.x - head.transform.position.x, tanPos.y - head.transform.position.y), Vector3.back).normalized;
                head.transform.rotation = Quaternion.RotateTowards(head.transform.rotation, lookRotation, rotateSpeed * Time.deltaTime);
            }
            head.transform.position += head.transform.forward * speed * Time.deltaTime;
        }
        else if (Vector2.Distance(head.transform.position, spaceship.transform.position) > distanceMax && attackState == false){
            choosingWichTangente = Random.Range(0, 2);
            attackState = true;
        }
        else {
            attackState = false;
            head.transform.position += head.transform.forward * speed * Time.deltaTime;
        }
    }
    void destroyerBodyFollowingList()
    {
        for (int chunk = 1; chunk < chunkNumber + 2; chunk++){
            _previousChildTransform = listChunks[chunk - 1].transform;
            _childTransform = listChunks[chunk].transform;
            _targetCoreClosestPoint = listBody[chunk - 1].GetComponent<CircleCollider2D>().bounds.ClosestPoint(_childTransform.position);

            if ((Vector3.Distance(_targetCoreClosestPoint, _childTransform.position) > 
            Vector3.Distance(_targetCoreClosestPoint, _childTransform.position + _childTransform.forward * (speed + 0) * Time.deltaTime)))
                _childTransform.position += _childTransform.forward * (speed + 0) * Time.deltaTime;
                
            if (_previousChildTransform.position != _childTransform.position)
                _childTransform.rotation = Quaternion.LookRotation(new Vector3(_previousChildTransform.position.x - _childTransform.position.x, 
                _previousChildTransform.position.y - _childTransform.position.y, 0), Vector3.back).normalized;  
        }
    }

    bool isAlive() {
        for (int chunk = 0; chunk < chunkNumber + 2; chunk++) {
            if (listBody[chunk].GetComponent<DestroyerBodyBehavior>().isAlive())
                return true;
        }
        return false;
    }

   IEnumerator manageShoot()
   {
        if (_created == false && dead == false) {
            int randomNbr = 0;
           do
            {
                if (isAlive() == false) {
                    dead = true;
                    break;
                }
                randomNbr = Random.Range(0, chunkNumber + 2);
            } while (!listBody[randomNbr].GetComponent<DestroyerBodyBehavior>().isAlive());
            if (isAlive() == true)
                listBody[randomNbr].GetComponent<DestroyerBodyBehavior>().shoot(spaceship.transform.position);
        }
        yield return new WaitForSeconds(secondPerShoot);
        nextShoot = true;
   }

}
