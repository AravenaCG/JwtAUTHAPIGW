using System;
using System.Collections.Generic;

namespace JwtAuthAPI.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string? User { get; set; }

    public string? Password { get; set; }

    public string? Email { get; set; }

    public string? Rol { get; set; }
}
