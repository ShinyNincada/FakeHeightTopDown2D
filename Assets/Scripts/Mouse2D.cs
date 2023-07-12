using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse2D : MonoBehaviour
{
    [SerializeField] Camera _cam;
    SpriteRenderer _sprite;

    private void Start() {
        _sprite = GetComponent<SpriteRenderer>();
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouseWorldPos = _cam.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;
        transform.position = mouseWorldPos;

        if(Input.GetMouseButton(0)){
            _sprite.color = Color.black;
        }
        else{
            _sprite.color = Color.white;
        }
    }
}
