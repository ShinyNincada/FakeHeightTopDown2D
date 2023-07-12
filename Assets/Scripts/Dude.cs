using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dude : MonoBehaviour
{
    public Transform dudeGun;
    public SpriteRenderer dudeGunSprite;
    //Public params
    public float movementSpeed = 15f;
    public int fps = 30;
    
    //Local params
    Vector2 inputVector;
    int currentSpriteIndex;
    float animTimer = 0;
    Vector2 mousePoint;
    Vector2 mouseWorldPoint;
    //Components
    SpriteRenderer _renderer;
    [SerializeField] List<Sprite> _sprite;
    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        dudeGunSprite = dudeGun.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        RotateGun();
        Animate();
        Flip();
    }

    private void Flip()
    {
        mouseWorldPoint  = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if(mouseWorldPoint.x > transform.position.x){
            _renderer.flipX = false;
            dudeGunSprite.flipY = false;
        }
        else{
             _renderer.flipX = true;
            dudeGunSprite.flipY = true;
            
        }
    }

    private void RotateGun()
    {
        mousePoint = Input.mousePosition;

        Vector3 objectPos = Camera.main.WorldToScreenPoint(dudeGun.position);
        mousePoint.x = mousePoint.x - objectPos.x;
        mousePoint.y = mousePoint.y - objectPos.y;

        float angle = Mathf.Atan2(mousePoint.y, mousePoint.x) * Mathf.Rad2Deg;
        dudeGun.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    private void Movement()
    {
        inputVector.x = Input.GetAxis("Horizontal");
        inputVector.y = Input.GetAxis("Vertical");

        if(inputVector != Vector2.zero)
        {
            transform.position += (Vector3)inputVector.normalized * movementSpeed * Time.deltaTime;
        }
    }

    private void Animate()
    {
        if(inputVector != Vector2.zero){
            if(animTimer > 1f / fps){
                _renderer.sprite = _sprite[currentSpriteIndex];

                currentSpriteIndex++;
                
                if(currentSpriteIndex >= _sprite.Count){
                    currentSpriteIndex = 0;
                }

                animTimer = 0;
            }

             animTimer += Time.deltaTime;
        }

        else{
            _renderer.sprite = _sprite[0];
            currentSpriteIndex = 0;
            animTimer = 0;
        }
    }
    
}
