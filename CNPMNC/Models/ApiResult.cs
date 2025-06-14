using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CNPMNC.Models
{
     public class ApiResponse<T>
     {
          public int Status { get; set; }
          public string Message { get; set; }
          public ApiStruct<T> Data { get; set; }
     }
     public class ApiResponseDetail<T>
     {
          public int Status { get; set; }
          public string Message { get; set; }
          public T Data { get; set; } // ⚠️ Không phải ApiStruct<T>
     }


     public class ApiStruct<T>
     {
          public List<T> Data { get; set; }
          public PaginationModel Pagination { get; set; }
     }

     public class PaginationModel
     {
          public int Page { get; set; }
          public int PageSize { get; set; }
          public int TotalItems { get; set; }
     }
}
