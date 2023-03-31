
using Permission.Infra.Data.Models.Base;

namespace Permission.Infra.Data.Models
{
    public partial class Permission : BaseEntity
    {

        public string Name { get; set; }
        public string LastName { get; set; }
        public int PermissionTypeId { get; set; }
    }
}
