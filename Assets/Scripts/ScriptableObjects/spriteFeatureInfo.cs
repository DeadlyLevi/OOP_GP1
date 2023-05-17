using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class spriteFeatureInfo : MonoBehaviour
{
    public int id;
    public bool isInInventory;
    public Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }
    private void Update()
    {
        //Vector2 v2 = Random.insideUnitCircle * 100f;
        //Vector3 v3 = new Vector3(v2.x, 0, v2.y);
        //Debug.DrawLine(transform.position, transform.position + v3, Color.red);
    }

    public void ChangeColor(Color color)
    {
        image.color = color;
    }
}
