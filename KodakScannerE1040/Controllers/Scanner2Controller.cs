using System.Drawing.Imaging;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NTwain.Data;
using NTwain;
using System.Runtime.InteropServices;

namespace KodakScannerE1040.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Scanner2Controller : ControllerBase
    {
        //private TwainSession _twain;
        //private AutoResetEvent _scanCompleteEvent = new AutoResetEvent(false);
        //private List<Bitmap> _scannedImages = new List<Bitmap>();

        //public Scanner2Controller()
        //{
        //    _twain = new TwainSession(TWIdentity.CreateFromAssembly(DataGroups.Image, Assembly.GetExecutingAssembly()));
        //    _twain.Open();
        //}

        //[HttpGet("scan")]
        //public IActionResult Scan()
        //{
        //    try
        //    {
        //        // 1️⃣ Get the first available scanner
        //        var scanner = _twain.FirstOrDefault();
        //        if (scanner == null)
        //        {
        //            return BadRequest("No scanner found.");
        //        }

        //        // 2️⃣ Open the scanner
        //        scanner.Open();

        //        // 3️⃣ Enable Feeder (ADF) & Duplex Mode
        //        var feederCap = scanner.Capabilities.CapFeederEnabled;
        //        if (feederCap.CanSet) feederCap.SetValue(BoolType.True);

        //        var duplexCap = scanner.Capabilities.CapDuplexEnabled;
        //        if (duplexCap.CanSet) duplexCap.SetValue(BoolType.True);

        //        _scannedImages.Clear(); // Reset scanned images list

        //        _twain.DataTransferred += (s, e) =>
        //        {
        //            try
        //            {
        //                if (e.NativeData == IntPtr.Zero)
        //                {
        //                    Console.WriteLine("⚠ No image data received from scanner.");
        //                    return;
        //                }

        //                Console.WriteLine(e);

        //                using (Bitmap scannedImage = Image.FromHbitmap(e.NativeData))
        //                {
        //                    if (scannedImage == null)
        //                    {
        //                        Console.WriteLine("⚠ Scanned image is null.");
        //                        return;
        //                    }

        //                    _scannedImages.Add(new Bitmap(scannedImage));
        //                    Console.WriteLine("✅ Image scanned successfully.");
        //                }
        //            }
        //            catch (ExternalException ex)
        //            {
        //                Console.WriteLine($"❌ GDI+ error while processing scanned image: {ex.Message}");
        //            }
        //            catch (Exception ex)
        //            {
        //                Console.WriteLine($"❌ Unexpected error: {ex.Message}");
        //            }
        //        };




        //        // 5️⃣ Enable scanner (Start Scanning)
        //        //_twain.CurrentSource.Capabilities.ICapXferMech.SetValue(XferMech.Buffered);
        //        //_twain.CurrentSource.Capabilities.ICapXferMech.SetValue(XferMech.Memory);

        //        _twain.CurrentSource.Enable(SourceEnableMode.NoUI, false, IntPtr.Zero);
        //        // 6️⃣ Wait for all pages to be scanned
        //        _scanCompleteEvent.WaitOne(10000); // Wait for 10 seconds



        //        // 7️⃣ Convert images to multi-page TIFF
        //        if (_scannedImages.Count == 0)
        //        {
        //            return BadRequest("No images scanned.");
        //        }

        //        byte[] tiffBytes = ConvertToMultiPageTiff(_scannedImages);
        //        return File(tiffBytes, "image/tiff", "ScannedFile.tiff");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { message = "Scanning failed.", error = ex.Message });
        //    }
        //    finally
        //    {
        //        // 8️⃣ Always close the scanner
        //        _twain.CurrentSource?.Close();
        //    }
        //}

        //private byte[] ConvertToMultiPageTiff(List<Bitmap> images)
        //{
        //    using (var ms = new MemoryStream())
        //    {
        //        Encoder encoder = Encoder.SaveFlag;
        //        EncoderParameters encoderParams = new EncoderParameters(1);
        //        ImageCodecInfo tiffCodec = ImageCodecInfo.GetImageEncoders()
        //            .FirstOrDefault(c => c.FormatID == ImageFormat.Tiff.Guid);

        //        if (tiffCodec == null)
        //            throw new Exception("TIFF codec not found.");

        //        images[0].Save(ms, tiffCodec, null);
        //        encoderParams.Param[0] = new EncoderParameter(encoder, (long)EncoderValue.MultiFrame);

        //        for (int i = 1; i < images.Count; i++)
        //        {
        //            images[0].SaveAdd(images[i], encoderParams);
        //        }

        //        encoderParams.Param[0] = new EncoderParameter(encoder, (long)EncoderValue.Flush);
        //        images[0].SaveAdd(encoderParams);

        //        return ms.ToArray();
        //    }
        //}

    }
}
