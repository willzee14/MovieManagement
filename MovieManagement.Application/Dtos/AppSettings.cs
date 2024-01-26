using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#pragma warning disable

namespace MovieManagement.Application.Dtos
{
    public class AppSettings
    {        
        public string Secret { get; set; }
        public string LogPath { get; set; }
    }
}
