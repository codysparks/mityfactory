using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateItem : MonoBehaviour
{
    public GameObject prefabItem;

    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {        
    }

    public void ItemInstance() {
        Vector3 startPosition = GameObject.Find("Item Create Position").transform.position;
        Instantiate(prefabItem, new Vector3(startPosition.x, startPosition.y, startPosition.z), Quaternion.identity);
    }
}
