using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class gridMap : MonoBehaviour
{
    private const float screenWidth = 870;
    private const float screenHeight = 376;
    public int width;
    public int height;
    public float cellSize;
    private Image mImage;
    private int[,] gridArray;
    public GameObject pixelButton;
    public GameObject parentObject;
    public GameObject pixel;
    public Sprite[] spriteArray;


    public void gridMake(int width, int height, float cellSize){
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;

        gridArray = new int[width,height];
        var pixelTransform = pixel.GetComponent<RectTransform> ();
        pixelTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, cellSize);
        pixelTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, cellSize);
        
        var transform = pixelButton.GetComponent<RectTransform>();
        transform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, cellSize);
        transform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, cellSize);
        
        Vector3 where = new Vector3(25.0f,25.0f, 0.0f);
        for(int x=0; x<gridArray.GetLength(0); x++){
            for(int y=0; y<gridArray.GetLength(1); y++){
            
               GameObject ab = Instantiate(pixelButton,where, Quaternion.identity);
               ab.name = x + " " + y; 
                ab.transform.SetParent(parentObject.transform);
               where.y += cellSize;
            }
            where.y =25.0f;
            where.x += cellSize;
        }
        
    }

    private Vector3 GetWorldPosition(int x, int y){
        return new Vector3(x,y) * cellSize;
    }
    private GameObject onClick(){
        EventSystem currentEvent = EventSystem.current;
        GameObject selectedBtn = currentEvent.currentSelectedGameObject;
        Debug.Log(selectedBtn.name);
        return selectedBtn;
    }  

    public void selectTeal (){
        mImage = pixel.GetComponent<Image>();
        mImage.sprite = spriteArray[0];
    }
    public void selectBlue(){
        mImage = pixel.GetComponent<Image>();
        mImage.sprite = spriteArray[1];
    }

    public void selectGreen (){
        mImage = pixel.GetComponent<Image>();
        mImage.sprite = spriteArray[2];
    }

    public void selectOrange(){
        mImage = pixel.GetComponent<Image>();
        mImage.sprite = spriteArray[3];
    }

    public void selectPink(){
        mImage = pixel.GetComponent<Image>();
        mImage.sprite = spriteArray[4];
    }

    public void selectRed (){
        mImage = pixel.GetComponent<Image>();
        mImage.sprite = spriteArray[5];
    }

    public void selectYellow (){
        mImage = pixel.GetComponent<Image>();
        mImage.sprite = spriteArray[6];
    }

    public void selectBrown (){
        mImage = pixel.GetComponent<Image>();
        mImage.sprite = spriteArray[7];
    }
    public void selectSkin(){
        mImage = pixel.GetComponent<Image>();
        mImage.sprite = spriteArray[8];
    }

    public void selectDgray (){
        mImage = pixel.GetComponent<Image>();
        mImage.sprite = spriteArray[9];
    }

    public void selectBlack(){
        mImage = pixel.GetComponent<Image>();
        mImage.sprite = spriteArray[10];
    }

    public void selectLgray(){
        mImage = pixel.GetComponent<Image>();
        mImage.sprite = spriteArray[11];
    }

    public void selectPurple (){
        mImage = pixel.GetComponent<Image>();
        mImage.sprite = spriteArray[12];
    }

    public void selectRose (){
        mImage = pixel.GetComponent<Image>();
        mImage.sprite = spriteArray[13];
    }

     public void backButton () {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -2);
     }

    public void putPixel (){
        GameObject button = onClick();
        RectTransform rectTransform = button.GetComponent<RectTransform>();
        Vector2 anchoredPositiom = rectTransform.anchoredPosition;
        GameObject gb = GameObject.Find("0 0");
        RectTransform rt = gb.GetComponent<RectTransform> ();
        anchoredPositiom -= rt.anchoredPosition;
        anchoredPositiom.x += +cellSize/2 + 9;
        anchoredPositiom.y += +cellSize/2 + 9;
        GameObject pix = Instantiate(pixel, anchoredPositiom, Quaternion.identity);
        pix.transform.SetParent(parentObject.transform);
    }
    
    private void Start (){
        gridMake(width,height,cellSize);
        
    }
}