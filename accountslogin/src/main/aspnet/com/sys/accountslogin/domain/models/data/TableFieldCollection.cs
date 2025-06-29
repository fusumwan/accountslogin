using System.Collections.Generic;
using System.Linq;


namespace accountslogin.src.main.aspnet.com.sys.accountslogin.domain.models.data
{
    public class TableFieldCollection
    {
        private List<TableField> fields = new List<TableField>();

        public void AddField(string name, string dataType, string inputType = "")
        {
            var field = new TableField(name, dataType, inputType);
            fields.Add(field);
        }

        public List<TableField> GetFields()
        {
            return fields;
        }

        public string FindDataType(string name)
        {
            var field = fields.FirstOrDefault(f => f.GetName() == name);
            return field?.GetDataType();
        }
    }
}
