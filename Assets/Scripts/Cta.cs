using System.Collections;
using UnityEngine;

public class Cta : MonoBehaviour
{

    [SerializeField] private GameObject terms;

    private void Start()
    {
    }

    private void OnEnable()
    {
    }



    private void OnMouseDown()
    {
        terms.SetActive(true);
        gameObject.SetActive(false);
    }

}
