using System.Drawing.Imaging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NTwain;
using Microsoft.AspNetCore.Mvc;
using NTwain;
using NTwain.Data;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace KodakScannerE1040.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScanController : ControllerBase
    {

        //private readonly TwainSession _twain;

        //public ScanController()
        //{
        //    _twain = new TwainSession(Twain32.CurrentAppId);
        //}

        //[HttpGet("scan")]
        //public IActionResult ScanDocument()
        //{
        //    try
        //    {
        //        _twain.Open();
        //        var scanner = _twain.FirstOrDefault(s => s.Name.Contains("Your Scanner Name"));
        //        if (scanner == null)
        //        {
        //            return NotFound("Scanner not found.");
        //        }

        //        _twain.CurrentSource = scanner;
        //        scanner.Open();
        //        scanner.Capabilities.XferCount.Set(-1); // Allow multiple pages
        //        scanner.Capabilities.FeederEnabled.Set(true);
        //        scanner.Capabilities.DuplexEnabled.Set(true); // Enable duplex scanning
        //        scanner.Enable(SourceEnableMode.NoUI, false, IntPtr.Zero);

        //        List<Image> images = new List<Image>();

        //        scanner.ImageTransferred += (s, e) =>
        //        {
        //            using (var stream = new MemoryStream(e.ImageData))
        //            {
        //                images.Add(Image.FromStream(stream));
        //            }
        //        };

        //        scanner.Acquire();

        //        if (images.Count > 0)
        //        {
        //            var tiffPath = Path.Combine(Path.GetTempPath(), $"scanned_{Guid.NewGuid()}.tiff");
        //            SaveAsTiff(images, tiffPath);
        //            var fileBytes = System.IO.File.ReadAllBytes(tiffPath);
        //            return File(fileBytes, "image/tiff", "scanned_document.tiff");
        //        }

        //        return BadRequest("No images scanned.");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Error: {ex.Message}");
        //    }
        //    finally
        //    {
        //        _twain.Close();
        //    }
        //}

        //private void SaveAsTiff(List<Image> images, string filePath)
        //{
        //    if (images.Count == 0) return;

        //    var encoderInfo = ImageCodecInfo.GetImageEncoders()[3]; // TIFF Encoder
        //    var encoderParams = new EncoderParameters(1) { Param = new[] { new EncoderParameter(Encoder.Compression, (long)EncoderValue.CompressionLZW) } };

        //    images[0].Save(filePath, encoderInfo, encoderParams);
        //    for (int i = 1; i < images.Count; i++)
        //    {
        //        encoderParams.Param[0] = new EncoderParameter(Encoder.SaveFlag, (long)EncoderValue.FrameDimensionPage);
        //        images[i].SaveAdd(new EncoderParameters(1) { Param = new[] { new EncoderParameter(Encoder.SaveFlag, (long)EncoderValue.MultiFrame) } });
        //    }
        //}

    }
}
