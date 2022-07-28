using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignText : MonoBehaviour {

    public GameObject unmerged_text;
    public GameObject merged_text;
    bool merged = true;
    int count = 0;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Merge"))
        {
            if(count >= 1)
            {
                merged_text.SetActive(false);
                unmerged_text.SetActive(false);
                return;
            }
            if (merged == false)
            {
                unmerged_text.SetActive(true);
                merged_text.SetActive(false);
                merged = true;
                count += 1;
            }
            else
            {
                merged_text.SetActive(true);
                unmerged_text.SetActive(false);
                merged = false;
                count += 1;
            }

        }
    }
}
