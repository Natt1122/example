using System;
using System.Collections.Generic;

namespace UserManage.Model;

public partial class User
{
    public int Id { get; set; }

    public string? Fullname { get; set; }

    public string? Email { get; set; }

    public int? IdRol { get; set; }

    public string? Password { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? Registerdate { get; set; }

    public virtual Rol? IdRolNavigation { get; set; }
}
