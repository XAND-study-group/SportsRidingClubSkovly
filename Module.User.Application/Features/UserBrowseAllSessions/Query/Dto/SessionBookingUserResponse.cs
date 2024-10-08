﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.User.Application.Features.UserBrowseAllSessions.Query.Dto
{
    public class SessionBookingUserResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}