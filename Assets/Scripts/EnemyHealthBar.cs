using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Slider e_slider;
    [SerializeField] private Camera e_camera;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;



    public void UpdateHealthBar(float currentValue)
    {
        e_slider.value = currentValue;
    }


    // Update is called once per frame
    void Update()
    {
        transform.rotation = e_camera.transform.rotation;
        transform.position = target.position + offset;
    }

    void Awake()
    {
        if (e_camera == null)
        {
            e_camera = Camera.main;
        }
    }

}
