using UnityEngine;

public class LogRegAnimController : MonoBehaviour
{
    public static LogRegAnimController instance;

    public bool checkIfTabActiveLog;
    public bool checkIfTabActiveReg;
    public Animator animLog;
    public Animator animReg;

    void Start()
    {
        
    }

    void Update()
    {
        if(checkIfTabActiveLog == true)
        {
            animLog.SetBool("Selected", true);
            animReg.SetBool("Normal", true);
        }

        if (checkIfTabActiveReg == true)
        {
            animReg.SetBool("Selected", true);
            animLog.SetBool("Normal", true);
        }
    }
}
