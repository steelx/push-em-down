using System.Collections;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private float TimeElapsed = 0f;
    private readonly float RunningTime = 6.0f;
    private float Speed = 4f;
    private Coroutine ReverseSpeedCoroutine = null;

    private IEnumerator Start()
    {
        enabled = false;
        yield return new WaitForSeconds(Random.Range(0, RunningTime));
        enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        TimeElapsed += Time.deltaTime;

        if (TimeElapsed >= RunningTime)
        {
            if (ReverseSpeedCoroutine == null) ReverseSpeedCoroutine = StartCoroutine("ReverseSpeed");
        }
        else
        {
            transform.Translate(0, Speed * Time.deltaTime, 0);
        }
    }

    private IEnumerator ReverseSpeed()
    {
        yield return new WaitForSeconds(RunningTime / 2f);

        TimeElapsed = 0f;
        Speed = -Speed;
        ReverseSpeedCoroutine = null;
    }
}
