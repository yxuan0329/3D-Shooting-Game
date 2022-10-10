using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //UI objects

public class Menu : MonoBehaviour
{
    public GameObject GameMenu;
    public Button MenuOkButton;
    // public Button btn = MenuOkButton.GetComponent<Button>();

    // Start is called before the first frame update
    void Start()
    {
        // MenuOkButton.onClick.AddListener(OnClick);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Pressed primary button.");
            Destroy(GameMenu);
            Destroy(gameObject);
        }
    }


}
