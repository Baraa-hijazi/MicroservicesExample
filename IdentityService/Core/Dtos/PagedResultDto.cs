﻿using System.Collections.Generic;

namespace IdentityService.Core.DTOs
{
    public class PagedResultDto<T>
    {
        public int TotalCount { set; get; }
        public IList<T> Result { set; get; }
    }
}
