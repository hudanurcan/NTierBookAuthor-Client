using Project.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Contracts.Models.ResponseModels.Tags
{
    public class TagResponseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DataStatus Status { get; set; }

    }
}
