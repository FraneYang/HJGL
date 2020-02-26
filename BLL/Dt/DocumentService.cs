using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class DocumentService
    {
        public static Model.HJGLDB db = Funs.DB;

        /// <summary>
        /// 根据文档编号获取一个文档信息

        /// </summary>
        /// <param name="managerRuleCode">文档编号</param>
        /// <returns>一个文档实体</returns>
        public static Model.Dt_document GetDocumentByO_number(string o_number)
        {
            return Funs.DB.Dt_document.FirstOrDefault(x => x.O_number == o_number);
        }
    }
}
