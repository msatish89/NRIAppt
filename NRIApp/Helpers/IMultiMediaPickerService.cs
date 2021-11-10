using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NRIApp.Helpers
{
    public interface IMultiMediaPickerService
    {
        Task PickPhotosAsync(string adid);
    }
}
