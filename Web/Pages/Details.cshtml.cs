﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using InternationalWagesManager.Models;

namespace Web.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly InternationalWagesManager.Models.MyDbContext _context;

        public DetailsModel(InternationalWagesManager.Models.MyDbContext context)
        {
            _context = context;
        }

      public Employee Employee { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }
            else 
            {
                Employee = employee;
            }
            return Page();
        }
    }
}
