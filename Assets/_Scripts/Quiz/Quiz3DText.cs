using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Quiz3DText : MonoBehaviour
{

    [SerializeField] private GameObject _charcter;
    [SerializeField] private Material _solidMaterial;
    [SerializeField] private Material _transparentMaterial;
    private GameObject _player;
    

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }
    
    private void Update()
    {
        _charcter.transform.LookAt(_player.transform.position.With(y: _charcter.transform.position.y));
        _charcter.transform.Rotate(Vector3.up, 180);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _charcter.GetComponent<MeshRenderer>().material = _transparentMaterial;
            _charcter.GetComponent<MeshRenderer>().material.DOFade(0, 1).Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Sequence sequence = DOTween.Sequence();
            sequence
                .Append(_charcter.GetComponent<MeshRenderer>().material.DOFade(1, 1))
                .OnComplete(() => _charcter.GetComponent<MeshRenderer>().material = _solidMaterial);
            sequence.Play();
        }
    }
}
