using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailService.Model
{
    public class SendEmail
    {

        public required List<string> Email { get; set; }
        public required string Body { get; set; }
        public required string Subject { get; set; }
        public object? Other { get; set; }
    }
}