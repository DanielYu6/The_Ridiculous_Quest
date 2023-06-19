using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform LeftHand, RightHand;
    [SerializeField] private Material[] colors;
    [SerializeField] private Material[] enemyColors;
    [SerializeField] Collider collider;
    [SerializeField] MeshRenderer renderer, clone;
    [SerializeField] Transform table;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = table.InverseTransformPoint(renderer.transform.position);
        position.x = -position.x;
        position.z = -position.z;
        clone.transform.localPosition = position;
    }

    public void Grabbed()
    {
        if (Vector3.Distance(collider.transform.position, LeftHand.position) < Vector3.Distance(collider.transform.position, RightHand.position))
        {
            collider.gameObject.layer = 6;
            SetColor(0);
        } else
        {
            collider.gameObject.layer = 7;
            SetColor(1);
        }
    }
    public void SetColor(int color)
    {
        renderer.sharedMaterial = colors[color];
        clone.sharedMaterial = enemyColors[color];
    }
    public void SetNextColor()
    {
        renderer.sharedMaterial = renderer.sharedMaterial == colors[0] ? colors[1] : colors[0];
        clone.sharedMaterial = renderer.sharedMaterial == colors[0] ? enemyColors[1] : enemyColors[0];
    }
}
