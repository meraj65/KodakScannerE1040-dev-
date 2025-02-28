using System;
using System.Diagnostics;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using TwainDotNet;
using TwainDotNet.WinFroms;
using WIA;

namespace KodakScannerE1040.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private static DeviceManager _deviceManager;
        private static DeviceInfo _scannerDevice;

              
        [HttpPost("start-session")]
        public IActionResult StartSession()
        {
            try
            {
                _deviceManager = new DeviceManager();
                _scannerDevice = null;

                for (int i = 1; i <= _deviceManager.DeviceInfos.Count; i++)
                {
                    if (_deviceManager.DeviceInfos[i].Type == WiaDeviceType.ScannerDeviceType)
                    {
                        _scannerDevice = _deviceManager.DeviceInfos[i];
                        break;
                    }
                }

                if (_scannerDevice == null)
                {
                    return NotFound(new { message = "No scanner found." });
                }

                return Ok(new { message = "Scanner session initialized." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error initializing scanner session", error = ex.Message });
            }
        }

    
        [HttpPost("end-session")]
        public IActionResult EndSession()
        {
            try
            {
                _deviceManager = null;
                _scannerDevice = null;

                return Ok(new { message = "Scanner session ended." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error ending scanner session", error = ex.Message });
            }
        }

    }
}
