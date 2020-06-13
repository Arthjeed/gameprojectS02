using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerSpawn : MonoBehaviour
{
    public GameObject destroyer;
    public GameObject head;
    public GameObject body;
    public GameObject tail;

    [Range(0,100)]
    public float speed;
    
    private int _totalChunks = 0;
    private bool _oneTime = true;
    
    private Vector3 _targetCoreClosestPoint;
    //private Vector3 _position;
    private Transform _childTransform;
    
    private GameObject chunks;

    void Start()
    {
    }

    //chunks.transform.position - chunks.transform.GetChild(0).GetChild(0).GetComponent<Renderer>().bounds.size
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && _oneTime) {
            _oneTime = false;
            //head = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            head = Instantiate(head, transform.position, transform.rotation);
            head.transform.SetParent(destroyer.transform);
            for (_totalChunks = 0; _totalChunks < 50; _totalChunks++){
                chunks = Instantiate(body, transform.position, transform.rotation);
                chunks.transform.SetParent(destroyer.transform);
            }
            GameObject tailchunk = Instantiate(tail, transform.position, transform.rotation);
            tailchunk.transform.SetParent(destroyer.transform);
        }
        if (Input.GetKey(KeyCode.RightArrow))
			head.transform.Rotate(0.0f, 2.0f, 0.0f, Space.Self);
		if (Input.GetKey(KeyCode.LeftArrow))
			head.transform.Rotate(0.0f, -2.0f, 0.0f, Space.Self);					
        if (Input.GetKey(KeyCode.UpArrow)) {
            head.transform.position += head.transform.forward * speed * Time.deltaTime;
        }

        if (_oneTime == false) {
            for (_totalChunks = 1; _totalChunks < 52; _totalChunks++){
                //_position = destroyer.transform.GetChild(_totalChunks).transform.position;
                _childTransform = destroyer.transform.GetChild(_totalChunks).transform;
                _targetCoreClosestPoint = destroyer.transform.GetChild(_totalChunks - 1).GetChild(0).GetChild(0).GetComponent<CircleCollider2D>().bounds.ClosestPoint(_childTransform.position);

                if ((Vector3.Distance(_targetCoreClosestPoint, _childTransform.position) > 
                Vector3.Distance(_targetCoreClosestPoint, _childTransform.position + _childTransform.forward * speed * Time.deltaTime))
                 && (Vector3.Distance(_targetCoreClosestPoint, _childTransform.position + _childTransform.forward * speed * Time.deltaTime) > 0.0f))
                    _childTransform.position += _childTransform.forward * (speed) * Time.deltaTime;
                
                _childTransform.rotation = Quaternion.LookRotation(new Vector3(destroyer.transform.GetChild(_totalChunks - 1).transform.position.x - _childTransform.position.x, 
                destroyer.transform.GetChild(_totalChunks - 1).transform.position.y - _childTransform.position.y, 0), Vector3.back).normalized;
                
            }
        }
    }
}
