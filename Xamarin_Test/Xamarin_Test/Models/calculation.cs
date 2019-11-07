using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Xamarin_Test.Models
{
    public class calculation
    {
        [PrimaryKey, AutoIncrement]
        public int OperationID { get; set; }
        public string OperationResult { get; set; }
        public DateTime Date { get; set; }
    }
}
