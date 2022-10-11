using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputHandler : MonoBehaviour
{
   
   private LineRenderer currentLineRenderer;
   public GameObject Brush; 
   public GameObject Dot;
   public GameObject Eraser;
   private GameObject follower;
   public GameObject parentObject;
   private bool followerPresent;
   private Vector2 lastPosition;
   private char toolFlag = 'a';
   private char temp;
   private bool ssFlag = false;
   public Vector3 scaleChange = new Vector3(-0.01f, -0.01f, -0.01f);
   
   public void backButton () {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -1);
     }
    public void selectPencil () {
        toolFlag = 'p';
        Vector2 mousePosition = returnMousePosition ();
        if(followerPresent){
          Destroy(follower);
        }
        follower = Instantiate(Brush,mousePosition, Quaternion.identity);
        follower.tag = "follow";
        followerPresent = true ;
     }
     public void selectEraser () {
        toolFlag = 'e';
        Vector2 mousePosition = returnMousePosition ();
        if(followerPresent){
          Destroy(follower);
        }
        follower = Instantiate(Eraser,mousePosition, Quaternion.identity);
        follower.tag = "follow";
        followerPresent = true ;
     }
     public void selectDot () {
        toolFlag = 'd';
        Vector2 mousePosition = returnMousePosition ();
        if(followerPresent){
          Destroy(follower);
        }
        follower = Instantiate(Dot,mousePosition, Quaternion.identity);
        follower.tag = "follow";
        followerPresent = true ;
     }
     public void selectLine () {
        toolFlag = 'l';
        Vector2 mousePosition = returnMousePosition ();
        if(followerPresent){
          Destroy(follower);
        }
        follower = Instantiate(Brush,mousePosition, Quaternion.identity);
        follower.tag = "follow";
        followerPresent = true ;
     }
   public static Vector2 returnMousePosition (){
        Vector3 vec = getMousePsoitionWithZ(Input.mousePosition, Camera.main);
        Vector2 mousePosition = new Vector2(vec.x, vec.y);
        return mousePosition;

   }

   public static Vector3 getMousePsoitionWithZ(Vector3 screenPosition, Camera worldCamera){
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;

   }

   public void takeScreenCapture(){
      follower.SetActive(false);
      ssFlag = true;
   }

   public void screenCaptureFinish(){
      ssFlag = false;
      follower.SetActive(true);
   }
     private void handleScroll () {
          Vector2 scrollData = Input.mouseScrollDelta;
        if((int)scrollData.y != 0){
          GameObject.FindGameObjectWithTag("follow").transform.localScale += scaleChange*scrollData.y;
          GameObject.FindGameObjectWithTag("brush").transform.localScale += scaleChange*scrollData.y;
          var lr = Brush.GetComponent<LineRenderer>();
          lr.startWidth += scrollData.y* 0.1f;
          Debug.Log(scrollData.y);
        }
     }

     private void handleClick (Vector2 mousePosition) {
      if(toolFlag == 'd'){
          if(Input.GetMouseButtonDown(0)){
            Debug.Log(mousePosition.x + " " + mousePosition.y);
            GameObject go = Instantiate(Dot, mousePosition , Quaternion.identity);
            go.transform.SetParent(parentObject.transform);
        }
      }
     }
    
     private void lineEraser (Vector2 mousePosition){
        Vector2 instantPosition;
      if (toolFlag == 'e'){

        if(Input.GetMouseButtonDown(0)){
          GameObject liner = Instantiate(Brush);
         // temp = GameObject.FindGameObjectWithTag("brush").transform.localScale;
         // handleClick(mousePosition);
          currentLineRenderer = liner.GetComponent <LineRenderer>();
        // currentLineRenderer.startWidth = temp.x * 10;
        float alpha = 1.0f;
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(Color.white, 0.0f), new GradientColorKey(Color.white, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 1.0f), new GradientAlphaKey(alpha, 1.0f) }
        );
          currentLineRenderer.colorGradient = gradient;
          currentLineRenderer.SetPosition(0, mousePosition);
          currentLineRenderer.SetPosition(1, mousePosition);
        }
        else if(Input.GetMouseButton(0)){
          instantPosition =returnMousePosition();
              if (lastPosition != instantPosition) 
          {
              addPoint(instantPosition);
              lastPosition = instantPosition;
          }
        }
        else 
        {
          currentLineRenderer = null;
          
        }
      }

     }


     private void lineDrawer (Vector2 mousePosition){
      Vector2 instantPosition;
      if (toolFlag == 'p'){
        if(Input.GetMouseButtonDown(0)){
          GameObject liner = Instantiate(Brush);
         // temp = GameObject.FindGameObjectWithTag("brush").transform.localScale;
         // handleClick(mousePosition);
          currentLineRenderer = liner.GetComponent <LineRenderer>();
        // currentLineRenderer.startWidth = temp.x * 10;
          currentLineRenderer.SetPosition(0, mousePosition);
          currentLineRenderer.SetPosition(1, mousePosition);
        }
        else if(Input.GetMouseButton(0)){
          instantPosition =returnMousePosition();
          
              if (lastPosition != instantPosition) 
          {
              addPoint(instantPosition);
              lastPosition = instantPosition;
          }
        }
        else 
        {
          currentLineRenderer = null;
          
        }
      }
     }

     private void addPoint(Vector2 pointPosition){
        currentLineRenderer.positionCount++;
        int positionIndex = currentLineRenderer.positionCount - 1;
        currentLineRenderer.SetPosition(positionIndex, pointPosition);
     }
   
  

   private void Start(){
        Vector2 mousePosition = returnMousePosition ();
        selectPencil();

   }

   private void Update(){
      Vector2 mousePosition = returnMousePosition ();
      if(followerPresent == false){
        follower = Instantiate(Brush,mousePosition, Quaternion.identity);
        follower.tag = "follow";
        followerPresent = true ;
      }
      if (ssFlag == false){
       GameObject.FindGameObjectWithTag("follow").transform.position = mousePosition;
      }
        handleClick (mousePosition);
        lineDrawer(mousePosition);
        lineEraser(mousePosition);
        handleScroll ();
       
   }
}
