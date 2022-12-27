using System.Net;

public class Response<T>
{
    public T Data { get; set; }
    public string Message { get; set; }
    public int StatusCode {get; set;}

    public Response(T data){
        StatusCode = 200;
        Data = data;
    }
    public Response(HttpStatusCode statuscode,string message){
        StatusCode = (int)statuscode;
        Message = message;
    }

}
