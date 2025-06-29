namespace accountslogin.src.main.aspnet.com.sys.accountslogin.domain.models.data
{
    public class TableField
    {
        public string Name { get; private set; }
        public string DataType { get; private set; }
        public string InputType { get; private set; }

        public TableField(string name, string dataType, string inputType = "")
        {
            Name = name;
            DataType = dataType;
            InputType = inputType;
        }

        public string GetName()
        {
            return Name;
        }

        public string GetDataType()
        {
            return DataType;
        }

        public string GetInputType()
        {
            return InputType;
        }
    }

}
