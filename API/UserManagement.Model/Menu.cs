using System;
using System.Collections.Generic;

namespace UserManage.Model;

public partial class Menu
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Url { get; set; }

    public virtual ICollection<MenuRol> MenuRols { get; set; } = new List<MenuRol>();
}
