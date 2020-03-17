using System;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;

public class WebCamScreenShot : MonoBehaviour
{
    public static int fileCounter = 0;

    WebCamTexture webCamTexture;

    [Header("Make sure path exists")]
    public string path = "C:/temp/UnityScreenShots";

    [Header("File name example: image_0001.png")]
    public string fileNamePrefix = "image_";

    void Start()
    {
        var devices = WebCamTexture.devices;
        
        if (devices.Length < 1) throw new System.Exception("No webcams was found");
        
        var device = devices[0];
        
        webCamTexture = new WebCamTexture(device.name);
        webCamTexture.requestedFPS = 30;
        webCamTexture.requestedWidth = 320;
        webCamTexture.requestedHeight = 240;
        webCamTexture.Play();
        
        if (webCamTexture.width < 1 || webCamTexture.height < 1) throw new System.Exception("Invalid resolution");
    }

    public static string PrependZeros(int digit, int length)
    {
        var str = digit.ToString();
        while (str.Length < length) str = "0" + str;
        return str;
    }

    public void SaveToPNG()
    {
        string image_path = path + $"/{ fileNamePrefix + PrependZeros(fileCounter, 5) + fileCounter }.png";

        byte[] data = ScreenshotWebcam( webCamTexture );

        File.WriteAllBytes(image_path, data);

        fileCounter ++ ;
    }
    
    static byte[] ScreenshotWebcam(WebCamTexture wct)
    {
        Texture2D colorTex = new Texture2D(wct.width, wct.height, TextureFormat.RGBA32, false);

        byte[] colorByteData = Color32ArrayToByteArray(wct.GetPixels32());
        
        colorTex.LoadRawTextureData(colorByteData);
        colorTex.Apply();
        
        return colorTex.EncodeToPNG();
    }

    static byte[] Color32ArrayToByteArray(Color32[] colors)
    {
        if (colors == null || colors.Length == 0) return null;

        int lengthOfColor32 = Marshal.SizeOf(typeof(Color32));

        int length = lengthOfColor32 * colors.Length;

        byte[] bytes = new byte[length];

        GCHandle handle = default(GCHandle);

        try
        {
            handle = GCHandle.Alloc(colors, GCHandleType.Pinned);
            IntPtr ptr = handle.AddrOfPinnedObject();
            Marshal.Copy(ptr, bytes, 0, length);
        }
        finally
        {
            if (handle != default(GCHandle)) handle.Free();
        }

        return bytes;
    }
}
