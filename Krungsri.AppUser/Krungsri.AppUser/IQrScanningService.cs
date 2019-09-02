using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Krungsri.AppUser
{
    public interface IQrScanningService
    {
        Task<string> ScanAsync();
    }
}
