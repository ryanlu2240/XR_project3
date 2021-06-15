using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalDis : MonoBehaviour
{

    // private WaitForSeconds shotDuration = new WaitForSeconds(5.0f);

	public GameObject positiontag;
    public GameObject tar;
	public GameObject UI;
    bool show=false;
    // float t=0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!show){
            float dist = Vector3.Distance(tar.transform.position, positiontag.transform.position);
            // Debug.Log(dist);
            if(dist <= 1.0f ){
                UI.SetActive(true);
                // StartCoroutine (ShotEffect());
            }     
            else{
                UI.SetActive(false);
            }       
        }
    }

    // private IEnumerator ShotEffect()
    // {
    //     // 播放音效
    //     //unAudio.Play ();

    //     // 显示射击轨迹
    //     show=true;
    //     Debug.Log("active");
    //     UI.SetActive(true);

    //     yield return shotDuration;

    //     // 等待结束后隐藏轨迹
    //     show=false;
    //     Debug.Log("disactive");
    //     UI.SetActive(false);
    // }
}
