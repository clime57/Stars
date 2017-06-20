using UnityEditor;
using UnityEngine;
using System.IO;

public class SplitTexture : EditorWindow
{
	public Texture2D src;

	private const int PaddingInEditor = 3;

    [MenuItem("Window/SplitTexture")]
	public static void ShowWindow()
	{
        EditorWindow.GetWindow(typeof(SplitTexture));
	}

	void OnGUI()
	{
		EditorGUI.LabelField(new Rect(PaddingInEditor, PaddingInEditor, 100, 20), "Select a Texture");
		src = (Texture2D)EditorGUI.ObjectField(new Rect(120, PaddingInEditor, position.width - 120, 16), src, typeof(Texture2D), true);


		if (src != null) {
			float scaleFact = Mathf.Min(1.0f, position.width*1.0f/src.width, (position.height - 110)/3/src.height);
			int width = (int)(src.width * scaleFact);
			int height = (int)(src.height * scaleFact);

			EditorGUI.LabelField(new Rect(PaddingInEditor, 25, 100, 15),"PreView:");
			EditorGUI.DrawTextureTransparent(new Rect(PaddingInEditor, 40, width, height),src);

			//EditorGUI.LabelField(new Rect(PaddingInEditor, 45 + height, 100, 15),"RGB:");
			//EditorGUI.DrawPreviewTexture(new Rect(PaddingInEditor, 60 + height, width, height),src);

            EditorGUI.LabelField(new Rect(PaddingInEditor, 45 + height, 100, 15), "Aphla:");
            EditorGUI.DrawTextureAlpha(new Rect(PaddingInEditor, 60 + height, width, height), src);

            //EditorGUI.LabelField(new Rect(PaddingInEditor, 65 + height*2, 100, 15),"Aphla:");
            //EditorGUI.DrawTextureAlpha(new Rect(PaddingInEditor, 80 + height*2, width, height),src);


			if(GUI.Button(new Rect(PaddingInEditor,position.height - 120, position.width-PaddingInEditor*2,20),"Split")) {
				splitTexture();
			}
		}
	}

	private void splitTexture()
	{
		Texture2D RGBTex = new Texture2D(src.width,src.height,TextureFormat.RGB24,false);
		Texture2D aphlaTex = new Texture2D(src.width,src.height,TextureFormat.RGB24,false);


		Color[] c = src.GetPixels();
		RGBTex.SetPixels(c);
		for (int i = 0 ;i < c.Length; i++) {
			c[i].r = c[i].a;
			c[i].g = c[i].a;
			c[i].b = c[i].a; 
		}
		aphlaTex.SetPixels(c); 
	
		byte[] RGBTexData = RGBTex.EncodeToPNG();
		byte[] aphlaTexData = aphlaTex.EncodeToPNG();
		string path = AssetDatabase.GetAssetPath(src);
		path = path.Substring(0,path.LastIndexOf("."));
		if (RGBTexData != null) {
			//File.WriteAllBytes(path + "_RGB.png", RGBTexData);
		}
		if (aphlaTexData != null) {
			File.WriteAllBytes(path + "_A.png", aphlaTexData);
		}

		DestroyImmediate(RGBTex);
		DestroyImmediate(aphlaTex);
		AssetDatabase.Refresh();
		EditorUtility.FocusProjectWindow();
		Selection.activeObject = src;
	}
}
