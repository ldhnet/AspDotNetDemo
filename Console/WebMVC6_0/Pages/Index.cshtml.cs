﻿using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebMVC6_0.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            _logger.LogInformation("11111");
        }
    }
}