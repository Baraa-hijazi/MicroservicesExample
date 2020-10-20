using System.Collections.Generic;

namespace NoteTakingService.Core.Dtos
{
    public class PagedResultDto<T>
    {
        public int TotalCount { set; get; }
        public IList<T> Result { set; get; }
    }
}
