using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    public float rotateSpeed = 200f;
    float mx = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // ���� ���°� ������ �ߡ� ������ ���� ������ �� �ְ� �Ѵ�.
        if (GameManager.Instance.gState != GameManager.GameState.Run)
        {
            return;
        }

        float x = Input.GetAxis("Mouse X");

        mx += x * rotateSpeed * Time.deltaTime;


        transform.eulerAngles = new Vector3(0, mx, 0);
    }
}
