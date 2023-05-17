using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;


public class FeatureCanvasManager : MonoBehaviour
{
    private static FeatureCanvasManager _instance;
    public static FeatureCanvasManager Instance { get => _instance; private set => _instance = value; }

    public Text selectedFeature;

    public GameObject spriteFeature;

    public GameObject allFGrid;
    public GameObject selFGrid;

    public GameObject allFCanvasRef;
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
        selectedFeature.text = FeatureManager.Instance.GetSelectedFeatureName();
        HandleFMenuCanvas();

        if(bCanvasIsOpen)
            HandleFSelection();
    }

    public void AddFeatureInGrid(int Id)
    {
        GameObject newSprite = Instantiate(spriteFeature, selFGrid.transform);
        newSprite.GetComponent<spriteFeatureInfo>().id = Id;
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
        Vector3 mousePos = Input.mousePosition / allFCanvasRef.GetComponent<Canvas>().scaleFactor;
        mousePos.z = mousePos.z - 500;

        Debug.DrawLine(mousePos, mousePos + Vector3.forward * 1000f, Color.red);

        Debug.Log("Im Here");
        if (Input.GetMouseButtonDown(0))
        {
            firstHitSprite = null;
            secondHitSprite = null;
            if (Physics.Raycast(mousePos, mousePos + Vector3.forward, out hit, 1000f, layer))
            {
                Debug.Log("FirstHit");
                firstHitSprite = hit.transform.GetComponent<spriteFeatureInfo>();
            }
        }

        if (Input.GetMouseButtonUp(0) && firstHitSprite != null)
        {
            if (Physics.Raycast(mousePos, mousePos + Vector3.forward, out hit, 1000f, layer))
            {
                Debug.Log("SecondHit");
                secondHitSprite = hit.transform.GetComponent<spriteFeatureInfo>();
                if (firstHitSprite.isInInventory && secondHitSprite.isInInventory)
                {
                    SwapSprites();
                }
                else if(!firstHitSprite.isInInventory && secondHitSprite.isInInventory)
                {
                    EquipSprite();
                }
            }
            else if(firstHitSprite.isInInventory)
            {
                EmptySprite();
            }
        }
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
