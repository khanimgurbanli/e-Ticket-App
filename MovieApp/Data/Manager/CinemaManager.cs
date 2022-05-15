using eTickets.Data.Base;
using eTickets.Data.Context;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Data.Manager
{
    public class CinemaManager : EntityBaseRepository<Cinema>, ICinemaService
    {
        public CinemaManager(AppDBContext context) : base(context) { }
    }
}
