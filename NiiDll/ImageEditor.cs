using System;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using System.IO;
using Windows.Graphics.Imaging;
using Windows.Media.Ocr;
using System.Drawing;

namespace NiiDll
{
    class ImageEditor
    {
        /// <summary>
        /// <para>画像ファイルを読み込む</para>
        /// </summary>
        /// <param name="imageFilePath">画像ファイルパス</param>
        /// <returns>画像情報(SoftwareBitmap)</returns>
        private async Task<SoftwareBitmap> loadImage(string imageFilePath)
        {
            // 読み込んでbyte配列に格納
            var fs = File.OpenRead(imageFilePath);

            var mem = new MemoryStream();
            fs.CopyTo(mem);

            var byteArray = mem.ToArray();

            fs.Close();

            // IRandomAccessStreamを生成
            // DrawWriterを介して、byte配列を出力ストリームに載せる
            // FlushAsyncでrandomAccessStreamに反映
            var randomAccessStream = new InMemoryRandomAccessStream();
            var outputStream = randomAccessStream.GetOutputStreamAt(0);

            var dw = new DataWriter(outputStream);
            dw.WriteBytes(byteArray);
            await dw.StoreAsync();

            await outputStream.FlushAsync();

            // 画像情報化
            var decorder = await BitmapDecoder.CreateAsync(randomAccessStream);
            var bitmap = await decorder.GetSoftwareBitmapAsync(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Premultiplied);

            return bitmap;
        }
    }
}
