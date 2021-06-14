using UnityEngine;

namespace XR.Break
{
    public class RingTarget : Target
    {
        private bool positiveSideStart;
        bool isAnswer;
        GameObject Box;

        private void OnTriggerEnter(Collider other)
        {
            if (active)
            {
                positiveSideStart = Vector3.Dot((other.transform.position - transform.position).normalized, transform.forward) > 0;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (active)
            {
                bool positiveSideEnd = Vector3.Dot((other.transform.position - transform.position).normalized, transform.forward) > 0;

                // We started on one side of the sphere collider but passed through the ring to the other side on exit
                if (positiveSideStart != positiveSideEnd)
                {
                    if (isAnswer) {
                        ScoreManager.Instance.AddScore(2 * DEFAULT_SCORE);
                        OnCapture?.Invoke();
                        Box.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
                    } else {
                        OnCapture?.Invoke();
                        Box.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                    }
                    
                    // if (isAnswer!=null)
                    // {
                    //     this.transform.parent.gameObject.SetActive(false);
                    // }
                    // else
                    // {
                    //     Debug.Log("NULL ANS");
                    // }
                    Release();
                }
            }
        }
    }
}
