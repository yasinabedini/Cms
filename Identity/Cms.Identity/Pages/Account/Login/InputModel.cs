// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.

using System.ComponentModel.DataAnnotations;

namespace Cms.Identity.Pages.Login;

public class InputModel
{
    [Required]
    public string? PhoneNumber { get; set; }
    [Required]
    public string? Password { get; set; }
    public bool RememberLogin { get; set; }
    public string? ReturnUrl { get; set; }
    public string? Button { get; set; }
}