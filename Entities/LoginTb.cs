using System;
using System.Collections.Generic;

namespace JJK.Entities;

public partial class LoginTb
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool RememberMe { get; set; }
}
