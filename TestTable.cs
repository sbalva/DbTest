using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLToolkit.DataAccess;

namespace DBTest
{
    [TableName("TestTable")]
    public class TestTable
    {
        [PrimaryKey]
        public Guid Id;
        public byte[] TestData;
        public DateTime DateIn;
        public DateTime LastDateIn;

        public TestTable()
            : this(null)
        { 
        }

        public TestTable(byte[] _data)
            : this(Guid.NewGuid(), _data, DateTime.Now, DateTime.Now)
        { 
        }

        public TestTable(Guid _id, byte[] _data)
            : this(_id, _data, DateTime.Now, DateTime.Now)
        { 
        }

        public TestTable(Guid _id, byte[] _data, DateTime _dateIn, DateTime _lastDateIn)
        {
            this.Id = _id;
            this.TestData = _data;
            this.DateIn = _dateIn;
            this.LastDateIn = _lastDateIn;
        }
    }
}
