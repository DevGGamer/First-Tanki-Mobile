using UnityEngine;

public class HPBar : MonoBehaviour
{
    [SerializeField] private Color colorA;
    [SerializeField] private Color colorB;

    private void Update()
    {
        transform.LookAt(Camera.main.transform);
    }

    public void ChangeValue(float value)
    {
        transform.localScale = new Vector3(value, 1, 1);
        GetComponent<SpriteRenderer>().color = Color.Lerp(colorB, colorA, value);
    }
}
