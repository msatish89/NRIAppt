using System;
using System.Collections.Generic;
using System.Text;

namespace NRIApp.LocalService.Features.Models
{
    public class BizService
    {
        private LS_AD_SERVICES_DATA _service;
        public BizService(LS_AD_SERVICES_DATA service)
        {
            _service = service;
        }
        public string Mainmenu
        {
            get
            {
                return _service.mainmenu;
            }
        }
        public string Submenu
        {
            get
            {
                return _service.submenu;
            }
        }
        public string TypeID
        {
            get
            {
                return _service.mainmenuid;
            }
        }
        public string Originalprice
        {
            get
            {
                return _service.originalprice;
            }
        }

        public bool OriginalpriceVisible
        {
            get
            {
                return _service.originalpricevisible;
            }
        }
        public LS_AD_SERVICES_DATA Room
        {
            get => _service;
        }
    }
}
