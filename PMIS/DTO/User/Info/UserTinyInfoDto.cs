using Generic.Base.Handler.Map;
using PMIS.DTO.IndicatorValue.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMIS.DTO.User.Info
{
    public class UserTinyInfoDto
    {
        public async Task<UserTinyInfoDto> extraMapFromBaseModel(PMIS.Models.User baseModel)
        {
            await GenericMapHandlerFactory.GetMapper(GenericMapHandlerFactory.MappingMode.Auto).Map(baseModel, this);
            return this;
        }
        public int Id { get; set; }

        public string UserName { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;

        public string FullName { get; set; } = null!;


        public string? Phone { get; set; }

        public string? Description { get; set; }

       
    }
}
