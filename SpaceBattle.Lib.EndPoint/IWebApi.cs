using System.Net;
using CoreWCF;
using CoreWCF.OpenApi.Attributes;
using CoreWCF.Web;

namespace SpaceBattle.Lib.EndPoint;

[ServiceContract]
[OpenApiBasePath("/api")]
internal interface IWebApi
{
    [OperationContract]
    [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "/order")]
    [OpenApiTag("Order Contract")]
    [OpenApiResponse(ContentTypes = new[] { "application/json" }, Description = "Success", StatusCode = HttpStatusCode.OK, Type = typeof(Order))]
    Order OrderBodyEcho(
        [OpenApiParameter(ContentTypes = new[] { "application/json" }, Description = "param description.")] 
        Order param
        );

}
