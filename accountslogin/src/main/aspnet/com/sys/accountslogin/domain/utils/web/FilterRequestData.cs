using System.Collections.Generic;
namespace accountslogin.src.main.aspnet.com.sys.accountslogin.domain.utils.web
{
    using System.Collections.Generic;

    public class FilterRequestData
    {
        public string Hql { get; private set; }
        public Dictionary<string, List<string>> DataValues { get; private set; }

        public FilterRequestData(string hql = "", Dictionary<string, List<string>> dataValues = null)
        {
            Hql = hql;
            DataValues = dataValues ?? new Dictionary<string, List<string>>();
        }

        public string GetHql()
        {
            return Hql;
        }

        public void SetHql(string hql)
        {
            Hql = hql;
        }

        public Dictionary<string, List<string>> GetDataValues()
        {
            return DataValues;
        }

        public void SetDataValues(Dictionary<string, List<string>> dataValues)
        {
            DataValues = dataValues;
        }
    }

}
