using Generic.Service.Normal.Composition.Contract;
using PMIS.DTO.Category;
using PMIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.Services.Contract
{
    public interface ICategoryService : IGenericNormalService<Category, CategoryAddRequestDto, CategoryAddResponseDto, CategoryEditRequestDto, CategoryEditResponseDto, CategoryDeleteRequestDto, CategoryDeleteResponseDto, CategorySearchResponseDto>
    {       
    }
}
