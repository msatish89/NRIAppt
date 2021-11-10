using System;
using System.Collections.Generic;
using System.Text;

namespace NRIApp.LocalService.Features.Models
{
    public class BizFeatures
    {
        private LS_PAYMENT_FEATURES _service;
        public BizFeatures(LS_PAYMENT_FEATURES service)
        {
            _service = service;
        }
        public string Header
        {
            get
            {
                return _service.head;
            }
        }
        public string Desc
        {
            get
            {
                return _service.desc;
            }
        }
       
        

       
        public LS_PAYMENT_FEATURES Room
        {
            get => _service;
        }
    }
}
