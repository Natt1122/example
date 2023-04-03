using System;
using System.Collections.Generic;

namespace UserManage.Model;

public partial class Rol
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<MenuRol> MenuRols { get; } = new List<MenuRol>();

    public virtual ICollection<User> Users { get; } = new List<User>();
}
