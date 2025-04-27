using Microsoft.AspNetCore.Mvc;
using DeviceControl.DataAccess;
using DeviceControl.Entities;

namespace DeviceControl.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeviceController : ControllerBase
    {
        private readonly DeviceRepository _deviceRepository = new DeviceRepository();

        [HttpGet]
        public IActionResult GetDevices()
        {
            var devices = _deviceRepository.GetAllDevices();
            return Ok(devices);
        }

        [HttpPost]
        public IActionResult AddDevice(Device device)
        {
            _deviceRepository.AddDevice(device);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateDevice(string id, Device updatedDevice)
        {
            updatedDevice.Id = id;
            _deviceRepository.UpdateDevice(updatedDevice);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDevice(string id)
        {
            _deviceRepository.DeleteDevice(id);
            return Ok();
        }
    }
}