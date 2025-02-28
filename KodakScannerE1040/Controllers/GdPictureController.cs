using GdPicture14;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WIA;

namespace KodakScannerE1040.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GdPictureController : ControllerBase
    {
//        private readonly GdPictureImaging _gdpictureImaging;

//        public GdPictureController()
//        {
//            _gdpictureImaging = new GdPictureImaging();
//        }

//        [HttpGet("tiff_scan_color")]
//        public IActionResult TiffScanColor()
//        {
//            try
//            {
//                IntPtr WINDOW_HANDLE = IntPtr.Zero;

//                // Select and open the scanner
//                _gdpictureImaging.TwainSelectSource(WINDOW_HANDLE);
//                _gdpictureImaging.TwainOpenDefaultSource(WINDOW_HANDLE);

//                // Optional: Hide UI
//                _gdpictureImaging.TwainSetHideUI(true);

//                // Scan the document
//                int imageId = _gdpictureImaging.TwainAcquireToGdPictureImage(WINDOW_HANDLE);
//                if (imageId == 0)
//                {
//                    return BadRequest("Scanning failed.");
//                }

//                // Save scanned image to MemoryStream instead of file
//                using (MemoryStream ms = new MemoryStream())
//                {
////                    _gdpictureImaging.SaveAsStream(imageId, ms, TiffCompression.TiffCompressionAUTO
////);

//                    // Release resources
//                    _gdpictureImaging.ReleaseGdPictureImage(imageId);
//                    _gdpictureImaging.TwainCloseSource();

//                    // Return TIFF file as a response
//                    return File(ms.ToArray(), "image/tiff", "ScannedDocument.tiff");

//                }
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, new { message = "Error scanning document.", error = ex.Message });
//            }
//        }

    }
}
