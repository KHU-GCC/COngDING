using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Go_nextpage : MonoBehaviour
{
    private int clickcount = 0;
    public GameObject openingscene;
    public GameObject firstscene;
    public GameObject secondscene;
    public GameObject thirdscene;
    public GameObject fourthscene;
    public GameObject lastscene;
   
 
    void Start()
    {
        
    }

  
    void Update()
    {
        if(Input.GetMouseButtonDown(0)) {
            clickcount++;
        }
        if(clickcount == 1) {
            openingscene.SetActive(false);
        }
        if(clickcount == 2) {
            Destroy(firstscene);
        }
        if(clickcount == 3) {
            Destroy(secondscene);
        }
        if(clickcount == 4) {
            Destroy(thirdscene);
        }
        if(clickcount == 5) {
            Destroy(fourthscene);
        }
        if(clickcount == 6) {
            Destroy(lastscene);
            SceneManager.LoadScene("SelectStage");
        }
    }
}
