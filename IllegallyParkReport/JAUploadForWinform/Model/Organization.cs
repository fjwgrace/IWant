using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAUploadForWinform.Model
{
    class Organization
    {
        public string OrgCode { get; set; }
        public string OrgName { get; set; }
        public List<Camera> SubCameras { get; set; }
    }
}
