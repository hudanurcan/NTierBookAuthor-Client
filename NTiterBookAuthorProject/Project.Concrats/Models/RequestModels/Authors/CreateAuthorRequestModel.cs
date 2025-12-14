using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Contracts.Models.RequestModels.Authors
{
    public class CreateAuthorRequestModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
