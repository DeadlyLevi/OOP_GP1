using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;


public class FCanvasManager : MonoBehaviour
{
    private static FCanvasManager _instance;
    public static FCanvasManager Instance { get => _instance; private set => _instance = value; }

    public Text selectedFeature;

    public GameObject spriteFeature;

    public GameObject allFGrid;
    public GameObject selFGrid;

    public GameObject allFCanvasRef;
    public GameObject selFTextBox;
    public LayerMask layer;

    bool bCanvasIsOpen;

    List<RaycastResult> results;
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }

    }

    private void Update()
    {
        selectedFeature.text = FManager.Instance.GetSelectedFeatureName();
        HandleFMenuCanvas();

        if(bCanvasIsOpen)
            HandleFSelection();

        if(FManager.Instance.selMyF.Count == 0)
        {
            selFTextBox.SetActive(false);
        }
        else
        {
            selFTextBox.SetActive(true);
        }
    }

    public void AddFeatureInGrid(int Id, Sprite Sprite)
    {
        GameObject newSpriteObject = Instantiate(spriteFeature, allFGrid.transform);
        newSpriteObject.GetComponent<spriteFeatureInfo>().id = Id;
        newSpriteObject.GetComponent<Image>().sprite = Sprite;
    }

    public void HandleFMenuCanvas()
    {
        if(Input.GetButtonDown(GameConstants.k_OpenFMenu))
        {
            savedTimeScale = Time.timeScale;
        }
        else if (Input.GetButton(GameConstants.k_OpenFMenu))
        {
            allFCanvasRef.SetActive(true);
            SlowTimeOnMenuOpen(true);
            bCanvasIsOpen = true;
        }
        else if (Input.GetButtonUp(GameConstants.k_OpenFMenu))
        {
            allFCanvasRef.SetActive(false);
            SlowTimeOnMenuOpen(false);
            bCanvasIsOpen = false;
        }
    }

    float savedTimeScale;
    void SlowTimeOnMenuOpen(bool bSlowActive)
    {
        if(bSlowActive)
        {
            Time.timeScale = 0.1f;
        }
        else
        {
            Time.timeScale = savedTimeScale;
        }
    }



    RaycastHit hit;
    spriteFeatureInfo firstHitSprite;
    spriteFeatureInfo secondHitSprite;
    void HandleFSelection()
    {
        //Vector3 mousePos = Input.mousePosition / allFCanvasRef.GetComponent<Canvas>().scaleFactor;
        //mousePos.z = mousePos.z - 500;

        //Debug.DrawLine(mousePos, mousePos + Vector3.forward * 1000f, Color.red);

        
        //if (Input.GetMouseButtonDown(0))
        //{
        //    firstHitSprite = null;
        //    secondHitSprite = null;
        //    if (Physics.Raycast(mousePos, mousePos + Vector3.forward, out hit, 1000f, layer))
        //    {
                
        //        firstHitSprite = hit.transform.GetComponent<spriteFeatureInfo>();
        //    }
        //}

        //if (Input.GetMouseButtonUp(0) && firstHitSprite != null)
        //{
        //    if (Physics.Raycast(mousePos, mousePos + Vector3.forward, out hit, 1000f, layer))
        //    {
        //        Debug.Log("SecondHit");
        //        secondHitSprite = hit.transform.GetComponent<spriteFeatureInfo>();
        //        if (firstHitSprite.isInInventory && secondHitSprite.isInInventory)
        //        {
        //            SwapSprites();
        //        }
        //        else if(!firstHitSprite.isInInventory && secondHitSprite.isInInventory)
        //        {
        //            EquipSprite();
        //        }
        //    }
        //    else if(firstHitSprite.isInInventory)
        //    {
        //        EmptySprite();
        //    }
        //}
    }

    void SwapSprites()
    {
        spriteFeatureInfo tmp;
        tmp = firstHitSprite;
        firstHitSprite = secondHitSprite;
        secondHitSprite = tmp;
    }

    void EmptySprite()
    {
        firstHitSprite.ChangeColor(Color.white);
    }

    void EquipSprite()
    {
        secondHitSprite = firstHitSprite;
    }

    public void AddButton()
    {

    }
}
