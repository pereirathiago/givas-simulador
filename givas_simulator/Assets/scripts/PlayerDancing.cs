using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDancing : MonoBehaviour
{
    private Animator anim;
    [SerializeField]
    private GameObject panelDances;

    private float timeDance;

    // Start is called before the first frame update
    void Start()
    {

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // DancingTwerk();
        // HeadSpinning();
        OpenPainelDance();
    }

    public void Dancing(string nameDance)
    {
        if (!anim.GetBool("walking"))
        {
            // se tiver mais colocar um switch case
            if(nameDance == "spinHead")
            {
                HeadSpinning();
            } else
            {
                anim.SetTrigger(nameDance);
                anim.SetBool("isDancing", true);
                Invoke("FinishDancing", timeDance);
            }

            panelDances.SetActive(false);
            Cursor.visible = false;
        } else
        {
            anim.SetBool("isDancing", false);
        }
    }

    public void TimeFinishDance(float timeDance)
    {
        this.timeDance = timeDance;
    }

    void OpenPainelDance()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            panelDances.SetActive(true);
            Cursor.visible = true;
        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            panelDances.SetActive(false);
            Cursor.visible = false;
        }
    }


   /* void HeadSpinning()
    {
        if (!anim.GetBool("walking"))
        {
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                anim.SetTrigger("isHeadSpinning");
                new WaitForSeconds(1f);
                anim.SetTrigger("isHeadSpinning");
            }
        }
    }*/

    private IEnumerator SpinHeadAfterDelay(float delay)
    {
        for(int i = 0; i < 30; i++)
        {
            anim.SetTrigger("isHeadSpinning");
            yield return new WaitForSeconds(delay);
        }
        anim.SetTrigger("isHeadSpinningEnd");
    }

    private void HeadSpinning()
    {
        if (!anim.GetBool("walking"))
        {
            StartCoroutine(SpinHeadAfterDelay(0.25f));
        }
    }

    private void FinishDancing()
    {
        anim.SetBool("isDancing", false);
    }
}
