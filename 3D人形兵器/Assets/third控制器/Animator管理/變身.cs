using UnityEngine;

public class 變身 : MonoBehaviour
{
    public GameObject 主1;
    public GameObject 主2;
    public vThirdPersonCamera Camera;
    public static bool 變身bo;
    void Update()
    {
        切換();
    }
    private void 切換()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            變身bo = !變身bo;
            if (!主1.activeInHierarchy)
            {
                主1.transform.position = 主2.transform.position;
                主1.transform.eulerAngles = 主2.transform.eulerAngles;
            }
            else if (!主2.activeInHierarchy)
            {
                主2.transform.position = 主1.transform.position;
                主2.transform.eulerAngles = 主1.transform.eulerAngles;
            }
            主1.SetActive(!主1.activeInHierarchy);
            主2.SetActive(!主2.activeInHierarchy);

            if (主1.activeInHierarchy)
            {
                Camera.SetTarget(主1.transform);
            }
            else if (主2.activeInHierarchy)
            {
                Camera.SetTarget(主2.transform);
            }
        }
    }
}
