using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.DTO
{
    public class CreateCategoryDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class UpdateCategoryDto : CreateCategoryDto
    {
        public int Id { get; set; }
    }

    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
