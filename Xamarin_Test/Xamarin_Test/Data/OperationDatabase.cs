using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Xamarin_Test.Models;

namespace Xamarin_Test.Data
{
    public class OperationDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public OperationDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<calculation>().Wait();
        }

        public Task<List<calculation>> GetOperationAsync()
        {
            return _database.Table<calculation>().ToListAsync();
        }

        public Task<calculation> GetOperationAsync(int id)
        {
            return _database.Table<calculation>()
                            .Where(i => i.OperationID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveOperationAsync(calculation Cal)
        {
            if (Cal.OperationID != 0)
            {
                return _database.UpdateAsync(Cal);
            }
            else
            {
                return _database.InsertAsync(Cal);
            }
        }

        public Task<int> DeleteOperationAsync(calculation Cal)
        {
            return _database.DeleteAsync(Cal);
        }
    }
}
