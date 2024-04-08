using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class TimerContronller : MonoBehaviour
{
    [Range(0,2f)]public float defaultTimeScale;// 默认时间速度
    [Header("子弹时间")]
    [SerializeField,Range(0f,2f)] float Timer; // 子弹时间速度

    [SerializeField] private float TimeRecoverDuration;//子弹时间的持续时间
    private GUIStyle lableStyle;
    private UnityEvent recoverTime;
    private void Awake()
    {
        Time.timeScale = defaultTimeScale;
    }

    public void Start()
    {
        lableStyle = new GUIStyle();
        lableStyle.fontSize = 20;
        lableStyle.normal.textColor = Color.white;
        
    }

    public void BulletTime()
    {
        Time.timeScale = Timer;
        StartCoroutine(nameof(TimeRecoverCoroutine));
    }
    private void OnGUI()
    {
        GUI.Label(new Rect(10,10,100,80),"SchaleTime : " + Time.timeScale,lableStyle);
    }

    IEnumerator TimeRecoverCoroutine()
    {
        float ratio = 0f;
        while(ratio < 1f) 
        {
            ratio += Time.unscaledDeltaTime / TimeRecoverDuration;
            Time.timeScale = Mathf.Lerp(Timer,defaultTimeScale,ratio);
            yield return null;// 等待到下一帧执行
        }
        //Time.timeScale = defaultTimeScale;
    }
}
