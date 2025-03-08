using Microsoft.AspNetCore.Mvc;
using WIA;
using CommonDialog = WIA.CommonDialog;


namespace KodakScannerE1040.Controllers
{
    [Route("[controller]api")]
    [ApiController]
    public class ScannerController : ControllerBase
    {
        private readonly string query;
        private readonly string FormatType; 
        private readonly string FileName;
        private readonly string OutPutFormat;
        private readonly int Pixel;
        private readonly int Dpi;
        private readonly int ColorMode;


        public ScannerController()
        {
            query = "SELECT * FROM Win32_PnPEntity WHERE Description LIKE '%Fax%' OR Description LIKE '%Scanner%'";
            FormatType = "image/tiff";
            OutPutFormat = "image/tiff";
            Pixel = 8;
            Dpi = 200;
            ColorMode = 1;
            FileName = "ScannedFile.tiff";
        }

        [HttpGet("base64_scan")]
        public IActionResult Base64Scan()
        {
            var response = new
            {
                stage = "",
                status = "",
                message = "",
                scannedContentBase64 = ""
            };

            try
            {
                // ✅ Step 1: Get the scanner device
                response = new { stage = "GET_SCANNER", status = "", message = "", scannedContentBase64 = "" };

                DeviceManager deviceManager = new DeviceManager();
                DeviceInfo scannerDevice = null;

                for (int i = 1; i <= deviceManager.DeviceInfos.Count; i++) // WIA uses 1-based index
                {
                    if (deviceManager.DeviceInfos[i].Type == WiaDeviceType.ScannerDeviceType)
                    {
                        scannerDevice = deviceManager.DeviceInfos[i];
                        break;
                    }
                }
                if (scannerDevice == null)
                {
                    return BadRequest(new { stage = "GET_SCANNER", status = "ERROR", message = "No scanner found!", scannedContentBase64 = "" });
                }

                // ✅ Step 2: Connect to the scanner
                response = new { stage = "CONNECT_TO_SCANNER", status = "", message = "", scannedContentBase64 = "" };

                Device scanner = scannerDevice.Connect();

                // ✅ Step 3: Start scanning
                response = new { stage = "GET_SCANNED_IMAGE", status = "", message = "", scannedContentBase64 = "" };

                Item scanItem = scanner.Items[1]; // The first item is usually the scanner

                SetScannerProperty(scanItem.Properties, 6146, ColorMode); // Color mode: 1 = B/W, 2 = Grayscale, 3 = Color
                SetScannerProperty(scanItem.Properties, 6147, Dpi); // DPI (Resolution)
                SetScannerProperty(scanItem.Properties, 6148, Pixel); // Bits per pixel

                CommonDialog dialog = new CommonDialog();
                ImageFile imageFile = (ImageFile)dialog.ShowTransfer(scanItem, FormatType, false);

                byte[] imageBytes = (byte[])imageFile.FileData.get_BinaryData();

                // ✅ Convert byte array to Base64 string
                string base64String = Convert.ToBase64String(imageBytes);

                return Ok(new
                {
                    stage = "GET_SCANNED_IMAGE",
                    status = "SUCCESS",
                    message = "Scanning completed successfully.",
                    scannedContentBase64 = base64String
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    stage = response.stage,
                    status = "ERROR",
                    message = ex.Message,
                    scannedContentBase64 = ""
                });
            }
        }

        //Binary_scan
        [HttpGet("binary_scan")]
        public IActionResult BinaryScan()
        {
            var response = new
            {
                stage = "",
                status = "",
                message = "",
                scannedContentBinary = new byte[0] // Placeholder for binary data
            };

            try
            {
                // ✅ Step 1: Get the scanner device
                response = new { stage = "GET_SCANNER", status = "", message = "", scannedContentBinary = new byte[0] };

                DeviceManager deviceManager = new DeviceManager();
                DeviceInfo scannerDevice = null;

                for (int i = 1; i <= deviceManager.DeviceInfos.Count; i++) // WIA uses 1-based index
                {
                    if (deviceManager.DeviceInfos[i].Type == WiaDeviceType.ScannerDeviceType)
                    {
                        scannerDevice = deviceManager.DeviceInfos[i];
                        break;
                    }
                }
                if (scannerDevice == null)
                {
                    return BadRequest(new { stage = "GET_SCANNER", status = "ERROR", message = "No scanner found!", scannedContentBinary = new byte[0] });
                }

                // ✅ Step 2: Connect to the scanner
                response = new { stage = "CONNECT_TO_SCANNER", status = "", message = "", scannedContentBinary = new byte[0] };

                Device scanner = scannerDevice.Connect();

                // ✅ Step 3: Start scanning
                response = new { stage = "GET_SCANNED_IMAGE", status = "", message = "", scannedContentBinary = new byte[0] };

                Item scanItem = scanner.Items[1]; // The first item is usually the scanner

                SetScannerProperty(scanItem.Properties, 6146, ColorMode); // Color mode: 1 = B/W, 2 = Grayscale, 3 = Color
                SetScannerProperty(scanItem.Properties, 6147, Dpi); // DPI (Resolution)
                SetScannerProperty(scanItem.Properties, 6148, Pixel); // Bits per pixel

                CommonDialog dialog = new CommonDialog();
                ImageFile imageFile = (ImageFile)dialog.ShowTransfer(scanItem, FormatType, false);

                byte[] imageBytes = (byte[])imageFile.FileData.get_BinaryData();

                return Ok(new
                {
                    stage = "GET_SCANNED_IMAGE",
                    status = "SUCCESS",
                    message = "Scanning completed successfully.",
                    scannedContentBinary = imageBytes
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    stage = response.stage,
                    status = "ERROR",
                    message = ex.Message,
                    scannedContentBinary = new byte[0]
                });
            }
        }

        [HttpGet("base64_grayscale")]
        public IActionResult Base64GrayscaleScan()
        {
            var response = new
            {
                stage = "",
                status = "",
                message = "",
                scannedContentBase64 = ""
            };

            try
            {
                // ✅ Step 1: Get the scanner device
                response = new { stage = "GET_SCANNER", status = "", message = "", scannedContentBase64 = "" };

                DeviceManager deviceManager = new DeviceManager();
                DeviceInfo scannerDevice = null;

                for (int i = 1; i <= deviceManager.DeviceInfos.Count; i++) // WIA uses 1-based index
                {
                    if (deviceManager.DeviceInfos[i].Type == WiaDeviceType.ScannerDeviceType)
                    {
                        scannerDevice = deviceManager.DeviceInfos[i];
                        break;
                    }
                }
                if (scannerDevice == null)
                {
                    return BadRequest(new { stage = "GET_SCANNER", status = "ERROR", message = "No scanner found!", scannedContentBase64 = "" });
                }

                // ✅ Step 2: Connect to the scanner
                response = new { stage = "CONNECT_TO_SCANNER", status = "", message = "", scannedContentBase64 = "" };

                Device scanner = scannerDevice.Connect();

                // ✅ Step 3: Start scanning
                response = new { stage = "GET_SCANNED_IMAGE", status = "", message = "", scannedContentBase64 = "" };

                Item scanItem = scanner.Items[1]; // The first item is usually the scanner

                SetScannerProperty(scanItem.Properties, 6146, 2); // Color mode: 1 = B/W, 2 = Grayscale, 3 = Color
                SetScannerProperty(scanItem.Properties, 6147, Dpi); // DPI (Resolution)
                SetScannerProperty(scanItem.Properties, 6148, Pixel); // Bits per pixel

                CommonDialog dialog = new CommonDialog();
                ImageFile imageFile = (ImageFile)dialog.ShowTransfer(scanItem, FormatType, false);

                byte[] imageBytes = (byte[])imageFile.FileData.get_BinaryData();

                // ✅ Convert byte array to Base64 string
                string base64String = Convert.ToBase64String(imageBytes);

                return Ok(new
                {
                    stage = "GET_SCANNED_IMAGE",
                    status = "SUCCESS",
                    message = "Scanning completed successfully in grayscale.",
                    scannedContentBase64 = base64String
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    stage = response.stage,
                    status = "ERROR",
                    message = ex.Message,
                    scannedContentBase64 = ""
                });
            }
        }

        /// ✅ Helper Method: Set Scanner to Color Mode
        private void SetScannerProperties(Item scanItem)
        {
            const int WIA_IPS_CUR_INTENT = 6146; // WIA property for intent (color mode)
            const int WIA_IPS_DATA_TYPE = 4103;  // WIA property for data type (color)

            try
            {
                // ✅ Set color mode
                SetWIAProperty(scanItem.Properties, WIA_IPS_CUR_INTENT, 1); // 1 = Color

                // ✅ Ensure the scanner is set to color
                SetWIAProperty(scanItem.Properties, WIA_IPS_DATA_TYPE, 2); // 2 = RGB Color
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error setting scanner properties: {ex.Message}");
            }
        }

        /// ✅ Helper Method: Set WIA Property
        /// ✅ Helper Method: Set WIA Property Safely
        private void SetWIAProperty(IProperties properties, int propertyID, int value)
        {
            try
            {
                foreach (Property property in properties)
                {
                    if (property.PropertyID == propertyID)
                    {
                        property.set_Value(value);
                        return; // Exit after setting the property
                    }
                }
                Console.WriteLine($"Warning: Property {propertyID} not found on this scanner.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to set property {propertyID}: {ex.Message}");
            }
        }


        private void SetScannerProperty(IProperties properties, int propertyID, int value)
        {
            try
            {
                foreach (Property property in properties)
                {
                    Console.WriteLine($"Property ID: {property.PropertyID}, Name: {property.Name}");
                    if (property.PropertyID == propertyID)
                    {
                        property.set_Value(value);
                        return;
                    }
                }
                Console.WriteLine($"Warning: Property {propertyID} not found on this scanner.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to set property {propertyID}: {ex.Message}");
            }
        }

    }
}
