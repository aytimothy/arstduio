using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PictureRenderer : MonoBehaviour {
    public new Camera camera => arCamera.camera;
    public ARCamera arCamera => sessionManager.cam;
    public ARSessionManager sessionManager;

    /// <summary>
    /// Force renders the specified camera, saves the image it rendered to a file.
    /// </summary>
    /// <param name="camera">The Unity Camera to render.</param>
    /// <param name="filePath">The full path to the file you want to write the image data to.</param>
    public static void SaveToFile(Camera camera, string filePath, ImageFileFormat fileFormat = ImageFileFormat.PNG, bool saveToCameraRoll = true) {
        RenderTexture currentRenderTexture = RenderTexture.active;
        RenderTexture cameraRenderTexture = camera.targetTexture;
        RenderTexture bufferRenderTexture = new RenderTexture(Screen.width, Screen.height, 32);
        RenderTexture.active = bufferRenderTexture;
        camera.targetTexture = RenderTexture.active;
        camera.Render();
        Texture2D image = new Texture2D(bufferRenderTexture.width, bufferRenderTexture.height);
        image.ReadPixels(new Rect(0, 0, bufferRenderTexture.width, bufferRenderTexture.height), 0, 0);
        image.Apply();
        RenderTexture.active = currentRenderTexture;
        camera.targetTexture = cameraRenderTexture;

        byte[] bytes = null;
        string[] pathSplit = filePath.Split(new char[2] { '/', '\\' });
        string fileName = pathSplit[pathSplit.Length - 1];
        switch (fileFormat) {
            case ImageFileFormat.PNG:
                if (saveToCameraRoll) {
                    NativeToolkit.SaveImage(image, fileName, "png");
                    break;
                }
                bytes = image.EncodeToPNG();
                break;
            case ImageFileFormat.JPG:
                if (saveToCameraRoll) {
                    NativeToolkit.SaveImage(image, fileName, "jpg");
                    break;
                }
                bytes = image.EncodeToJPG();
                break;
            case ImageFileFormat.EXR:
                bytes = image.EncodeToEXR();
                break;
            case ImageFileFormat.TGA:
                bytes = image.EncodeToTGA();
                break;
        }
        Destroy(image);
        Destroy(bufferRenderTexture);
        if (bytes != null) {
            File.WriteAllBytes(filePath, bytes);
        }
    }

    public static void SaveToFileAsync(Camera camera, string filePath) {
        throw new NotImplementedException();
    }

    public void SaveToFile(string filePath) {
        if (camera == null) {
            Debug.LogError("Cannot find a camera to render from!");
            return;
        }
        SaveToFile(camera, filePath);
    }

    public void SaveToFileAsync(string filePath) {
        if (camera == null) {
            Debug.LogError("Cannot find a camera to render from!");
            return;
        }
        SaveToFileAsync(camera, filePath);
    }
}

public enum ImageFileFormat {
    PNG,
    JPG,
    EXR,
    TGA
}