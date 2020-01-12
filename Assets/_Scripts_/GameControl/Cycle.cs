using System.Collections;
using UnityEngine;

public class Cycle : MonoBehaviour
{
    public Color colorDawn;
    public Color colorDay;
    public Color colorDusk;
    public Color colorEnd;

    public float duration = 1.0F;

    private void Start()
    {
        //StartCoroutine(ChangeSkyBox());
    }

    void Update()
    {
        float lerp = Mathf.PingPong(Time.time, duration) / duration;
        RenderSettings.skybox.SetColor("_Tint", Color.Lerp(colorDawn, colorDay, lerp));
    }

    private IEnumerator ChangeSkyBox()
    {
        yield return new WaitForSeconds(10);
        StartCoroutine(ChangeSkyBox());
    }
}
