using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using System.IO;

public class screensnap : MonoBehaviour
{
   public int captureWidth;
   public int captureHeight;

   public GameObject hideGameObject;
   public GameObject hideGameObject2;
   private GameObject[] follower;

   public string folder;
   private GameObject canvasFind;
   private Canvas m_Canvas;
   

   // private variables 
   private Rect rect;
   private RenderTexture renderTexture;
   private Texture2D screenShot;
   private int counter = 0 ;

   private string fileNamer (int width , int height){

        System.IO.Directory.CreateDirectory(folder);
        string mask = string.Format("ScreenShot_{0}x{1}*.png", width, height);
        counter= Directory.GetFiles(folder,mask,SearchOption.TopDirectoryOnly).Length;
    
        var filename = string.Format("{0}/screen_{1}x{2}_{3}.png",folder,width,height,counter);

        counter++;

        return filename;
   }

   public void changeToCamera(){
          canvasFind = GameObject.Find("Canvas");
          m_Canvas =  canvasFind.GetComponent<Canvas>();
          m_Canvas.renderMode = RenderMode.ScreenSpaceCamera;
   } 

   public void changeToOverlay(){
          canvasFind = GameObject.Find("Canvas");
          m_Canvas =  canvasFind.GetComponent<Canvas>();
          m_Canvas.renderMode = RenderMode.ScreenSpaceOverlay;
   }

   public void captureScreen (){
        if (hideGameObject != null ) hideGameObject.SetActive(false);
        
        if (hideGameObject2 != null ) hideGameObject2.SetActive(false);

        
        //create textures and rendertexture

        rect = new Rect(0,0,captureWidth,captureHeight);
        renderTexture = new RenderTexture(captureWidth,captureHeight, 24);
        screenShot = new Texture2D(captureWidth,captureHeight,TextureFormat.RGB24,false);

        //get Camera
        Camera camera = this.GetComponent<Camera>(); // NOTE: added because there was no reference to camera in original script; must add this script to Camera
        camera.targetTexture = renderTexture;
        camera.Render();

        RenderTexture.active = renderTexture;
        screenShot.ReadPixels(rect, 0, 0);

        camera.targetTexture = null;
        RenderTexture.active = null;

        string filename = fileNamer((int)rect.width, (int)rect.height);
        byte[] fileHeader = null;
        byte[] fileData = null;
        fileData = screenShot.EncodeToPNG();

        
        // create file and write optional header with image bytes
        var f = System.IO.File.Create(filename);
        if (fileHeader != null) f.Write(fileHeader, 0, fileHeader.Length);
        f.Write(fileData, 0, fileData.Length);
        f.Close();
        Debug.Log(string.Format("Wrote screenshot {0} of size {1}", filename, fileData.Length));
 
        // unhide objects
        if (hideGameObject != null) hideGameObject.SetActive(true);
        if (hideGameObject2 != null) hideGameObject2.SetActive(true);
        
        
   }
}
