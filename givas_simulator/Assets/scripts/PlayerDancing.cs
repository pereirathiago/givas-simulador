using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDancing : MonoBehaviour
{
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        DancingTwerk();
        HeadSpinning();
    }

    void DancingTwerk()
    {
        if (!anim.GetBool("walking"))
        {
            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                anim.SetTrigger("isDancingTwerk");
                anim.SetBool("dancing-twerk", true);
                Invoke("FinishDancingTwerk", 15f);
            }
        } else
        {
            anim.SetBool("dancing-twerk", false);
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
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                StartCoroutine(SpinHeadAfterDelay(0.25f));
            }
        }
    }

    private void FinishDancingTwerk()
    {
        anim.SetBool("dancing-twerk", false);
    }
}
