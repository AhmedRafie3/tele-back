using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class RetuenAuth
    {
        public string Token {  get; set; }
        public string Message {  get; set; }
        public int UserRole {  get; set; }
    }
}
