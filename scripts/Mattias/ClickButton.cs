using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Image current_img; // holds reference to image
    [SerializeField] private Sprite on_sprite, off_sprite, pressed_sprite; // reference to three sprites

    private int button_status;

    public void OnPointerDown(PointerEventData eventData) // on click
    {
        current_img.sprite = pressed_sprite; // change sprite of img
    }
    
    public void OnPointerUp(PointerEventData eventData)
    {
        if (button_status == 0) // change img sprite depending on switch status
        {
            button_status = 1;
            current_img.sprite = on_sprite;
        } 
        else
        {
            button_status = 0;
            current_img.sprite = off_sprite;
        }
    }

    void Start() // start in off
    {
        button_status = 0;
        current_img.sprite = off_sprite;
    }

    


}
