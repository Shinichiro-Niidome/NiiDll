using System;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using System.IO;
using Windows.Graphics.Imaging;
using Windows.Media.Ocr;
using System.Drawing;

namespace NiiDll
{
    /// <summary>
    /// <para>Windows搭載OCRのラップクラス</para>
    /// </summary>
  //  public class Win10OCR
  //  {
  //      // ==================================================
  //      // 定義
  //      // ==================================================

  //      // 変数(パディングに注意)
  //      // ------------------------------

  //      private OcrEngine _Engine = OcrEngine.TryCreateFromUserProfileLanguages();

		//// ==================================================
		//// 文字解析
		//// ==================================================

		//// 関数
		//// ------------------------------

		///// <summary>
		///// <para>画像から文字を解析する</para>
		///// </summary>
		///// <param name="imageFilePath">画像ファイルパス</param>
		///// <returns>文字</returns>
		//public async Task<string> analysisText(string imageFilePath)
  //      {
  //          var image = await loadImage(imageFilePath);

  //          var result = await _Engine.RecognizeAsync(image);

  //          return result.Text;
  //      }

  //      /// <summary>
  //      /// <para>画像を読み込む</para>
  //      /// </summary>
  //      /// <param name="imageFilePath">画像ファイルパス</param>
  //      /// <returns>画像</returns>
  //      private async Task<SoftwareBitmap> loadImage(string imageFilePath)
  //      {
  //          // 読み込み
  //          var fs = File.OpenRead(imageFilePath);
  //          var buf = new byte[fs.Length];
  //          fs.Read(buf, 0, (int)fs.Length);
  //          var mem = new MemoryStream(buf);
  //          mem.Position = 0;

  //          var stream = await convertToRandomAccessStream(mem);
  //          var bitmap = await loadImage(stream);
  //          return bitmap;
  //      }

  //      /// <summary>
  //      /// <para>MemoryStream⇒IRandomAccessStreamに変換する</para>
  //      /// </summary>
  //      /// <param name="memoryStream">ストリーム</param>
  //      /// <returns>IRandomAccessStreamストリーム</returns>
  //      private async Task<IRandomAccessStream> convertToRandomAccessStream(MemoryStream memoryStream)
  //      {
  //          var randomAccessStream = new InMemoryRandomAccessStream();
  //          var outputStream = randomAccessStream.GetOutputStreamAt(0);
  //          var dw = new DataWriter(outputStream);
  //          var task = new Task(() => dw.WriteBytes(memoryStream.ToArray()));
  //          task.Start();
  //          await task;
  //          await dw.StoreAsync();
  //          await outputStream.FlushAsync();
  //          return randomAccessStream;
  //      }

  //      /// <summary>
  //      /// <para>画像を読み込む</para>
  //      /// </summary>
  //      /// <param name="stream">ストリーム</param>
  //      /// <returns>画像</returns>
  //      private async Task<SoftwareBitmap> loadImage(IRandomAccessStream stream)
  //      {
  //          var decoder = await BitmapDecoder.CreateAsync(stream);
  //          var bitmap = await decoder.GetSoftwareBitmapAsync(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Premultiplied);
  //          return bitmap;
  //      }

  //      // ==================================================
  //      // 画像加工
  //      // ==================================================

  //      // 関数
  //      // ------------------------------

  //      public string editImage(string filePath)
  //      {
  //          string copyedFilePath = filePath.Replace(".PNG", "_copy.PNG");

  //          if (File.Exists(copyedFilePath))
  //          {
  //              File.Delete(copyedFilePath);
  //          }

  //          Bitmap bmp = new Bitmap(filePath);

  //          var height = bmp.Height;
  //          var width = bmp.Width;

  //          for (int h = 0; h < height; h++)
  //          {
  //              for (int w = 0; w < width; w++)
  //              {
  //                  var pix = bmp.GetPixel(w, h);

  //                  Color writeColor = Color.Red;

  //                  if ((pix.R == 26) && (pix.G == 13) && (pix.B == 171))
  //                  {
  //                      bmp.SetPixel(w, h, writeColor);
  //                  }
  //                  // 26 13 171
  //              }
  //          }

  //          bmp.Save(copyedFilePath);

  //          return copyedFilePath;
  //      }

  //      // ==================================================
  //      // 画像分割
  //      // ==================================================

  //      // 関数
  //      // ------------------------------

  //  }
}
