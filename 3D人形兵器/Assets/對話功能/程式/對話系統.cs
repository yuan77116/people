using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class 對話系統 : MonoBehaviour
{
    public static 對話系統 ins;
    public 任務管理 任務管理;
    public 存放對話資料 存放date;
    [Range(0,1)]
    public float 速度=0.1f;
    public GameObject 圖示;
    public Text 名稱;
    public Text 內容;
    private CanvasGroup CanvasGroup;
    public KeyCode 按鍵 = KeyCode.Mouse0;
    [Range(0,1)]
    public float 音效音量=1;
    private AudioSource 音效;
    public AudioClip aud;
    private void Start()
    {
        對話系統.ins = GameObject.Find("對話系統").GetComponent<對話系統>();
        CanvasGroup = transform.GetChild(0).GetComponent<CanvasGroup>();
        音效 = GetComponent<AudioSource>();
        播放旁白();
    }
    private void 播放旁白()
    {
        StartCoroutine(修改內容(存放date.對話內容));
    }
    public IEnumerator 修改內容(string[] 對話名稱)
    {
        CanvasGroup.alpha = 1;     //透明度0>1
        名稱.text = 存放date.對話者名稱;
        內容.text = "";     //清空

        for (int i = 0; i < 對話名稱.Length; i++)   //每個段落i
        {
            for (int j = 0; j < 對話名稱[i].Length; j++)   //每個段落中的文字j
            {
                //print(存放date.對話內容[i][j]);
                內容.text += 對話名稱[i][j];   //i段落中的每個文字j
                音效.PlayOneShot(aud, 音效音量);
                yield return new WaitForSeconds(速度);  //出字速度
            }

            圖示.SetActive(true);

            //等待按下按鍵，null每一針時間
            while (!Input.GetKeyDown(按鍵))    //while達成一直執行  等待玩家完成按鈕，使用null為每針的時間
            {
                yield return null;
            }

            內容.text = "";   //清空
            圖示.SetActive(false);

            if(i== 對話名稱.Length - 1)  //結束關閉介面
            {
                if(對話名稱== 存放date.結束內容)
                {
                    CanvasGroup.alpha = 0;
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                    //Time.timeScale = 1;
                }
                else
                {
                    CanvasGroup.alpha = 0;
                    任務管理.任務進行中();
                }
            }
        }
    }
}
